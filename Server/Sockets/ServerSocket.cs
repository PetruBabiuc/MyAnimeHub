using DBManagers;
using InitialClientActions;
using Interfaces;
using System;
using System.Net.Sockets;
using System.Threading;

namespace Sockets
{
    /// <summary>
    /// The SINGLETON class that contains the MyAnimeHub server's presentation 
    /// It implements the singleton design pattern because the server should listen on only one port
    /// </summary>
    public class ServerSocket : AbstractEncryptingSocket
    {
        private static readonly int MaxClients = 20;
        private static readonly int MaxLoginTries = 2;
        
        /// <summary>
        /// Object used for making sure that only 1 thread creates the private static instance
        /// The other threads will just receive a reference to the instance
        /// </summary>
        private static readonly object PadLock = new object();

        /// <summary>
        /// Singleton's instance
        /// </summary>
        private static ServerSocket instance = null;

        /// <summary>
        /// The DataBaseManager adapter
        /// </summary>
        private IMyAnimeHubDBManagerAdapter _dbManager;

        private ServerSocket() : base()
        {
            // Using Bind() method we associate a
            // network address to the Server Socket
            // All client that will connect to this
            // Server Socket must know this network
            // Address
            _socket.Bind(_endPoint);

            _dbManager = new MyAnimeHubDBManagerJsonAdapter();

            // Using Listen() method we create
            // the Client list that will want
            // to connect to Server
            _socket.Listen(MaxClients);
        }

        /// <summary>
        /// ReadOnly property used to obtain singleton's instance
        /// </summary>
        public static ServerSocket Instance
        {
            get
            {
                // Double check locking
                if (instance == null)
                    lock (PadLock)
                        if (instance == null)
                            instance = new ServerSocket();
                return instance;
            }
        }

        /// <summary>
        /// The function that starts a thread where the server will accept client's connections
        /// and process their requests
        /// </summary>
        public void StartServingAsynchronously()
        {
            Thread thread = new Thread(ListenForever);
            thread.Start();
        }

        /// <summary>
        /// Static method that gets client's IP address
        /// </summary>
        /// <param name="clientSocket">
        /// The client
        /// </param>
        /// <returns>
        /// The IP address as a string
        /// </returns>
        private static string GetClientIPAddress(Socket clientSocket)
        {
            //return ((IPEndPoint)clientSocket.RemoteEndPoint).Address.ToString();
            return clientSocket.RemoteEndPoint.ToString();
        }

        /// <summary>
        /// Function that processes client's requests
        /// </summary>
        /// <param name="clientConnection">
        /// A reference to the Socket object encapsulating the client's connections
        /// It's type is "object" to be eligible to be executed as a new thread's target function
        /// </param>
        private void ServeClient(object clientConnection)
        {
            Socket clientSocket = (Socket)clientConnection;
            bool keepConnectionAlive = true;
            string request, result;
            string clientIp = GetClientIPAddress(clientSocket);
            // Login
            if (!Login(clientSocket))
            {
                Console.WriteLine($"Client {clientIp}'s login failed...");
                CloseSocket(clientSocket);
                return;
            }
            // Process requests
            while (keepConnectionAlive)
            {
                try
                {
                    request = Receive(clientSocket);
                    keepConnectionAlive = _dbManager.ProcessRequest(request, out result);
                    Send(result, clientSocket);
                }
                catch (SocketException)
                {
                    keepConnectionAlive = false;
                    Console.WriteLine($"Client {clientIp} disconnected...");
                }
                catch (Exception)
                {
                    keepConnectionAlive = false;
                    Console.WriteLine($"Client {clientIp}'s request is invalid! => Closing the connection!");
                }
            }
            CloseSocket(clientSocket);
        }

        /// <summary>
        /// Method that handles the communication with the client
        /// until the client logs in.
        /// The client can also register.
        /// If the client registers, the login tries number is reseted
        /// </summary>
        /// <param name="clientSocket">
        /// The Socket encapsulating the connection with the client
        /// </param>
        /// <returns>
        /// True if the client logged in successfully, false otherwise
        /// </returns>
        private bool Login(Socket clientSocket)
        {
            string clientIp = GetClientIPAddress(clientSocket);
            string dataRead, response;
            InitialAction action;
            int tries = 0;
            while (tries < MaxLoginTries)
            {
                try
                {
                    dataRead = Receive(clientSocket);
                    action = _dbManager.LoginOrRegister(dataRead, out response);
                    Send(response, clientSocket);
                    switch (action)
                    {
                        case InitialAction.LoginFailed:
                            ++tries;
                            Console.WriteLine($"Client {clientIp} failed logging in {tries} times");
                            break;
                        case InitialAction.LoginSuccessful:
                            return true;
                        // Failed registrations have no secondary effects
                        case InitialAction.RegistrationFailed:
                            break;
                        // Successful registrations reset the failed logins count
                        case InitialAction.RegistrationSuccessful:
                            tries = 1;
                            break;
                    }
                }
                catch(SocketException ex)
                {
                    Console.WriteLine($"Client {clientIp} disconnected...");
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Client {clientIp}'s request is invalid! => Closing the connection!");
                    return false;
                }
            }
            Console.WriteLine($"Client {clientIp} failed logging in!");
            return false;
        }

        /// <summary>
        /// Function containing an infinite loop in which the server is accepting new client connections
        /// And starts a new thread in which the client's requests are processed
        /// </summary>
        private void ListenForever()
        {
            Thread clientServeThread;
            Console.WriteLine("Waiting for connections...");
            while (true)
            {
                Socket clientSocket = _socket.Accept();
                Console.WriteLine($"Client connected: {GetClientIPAddress(clientSocket)}");
                clientServeThread = new Thread(ServeClient);
                clientServeThread.Start(clientSocket);
            }
        }
    }
}
