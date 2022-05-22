/**************************************************************************
 *                                                                        *
 *  File:        AbstractSocket.cs                                        *
 *  Copyright:   (c) 2022, Petru Babiuc                                   *
 *  Description: The class that encapsulates the boilerplate code of      *
 *               the communication endpoints.                             *
 *                                                                        *
 **************************************************************************/
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;


namespace Sockets
{
    /// <summary>
    /// The abstract class that encapsulates a Socket for communicating
    /// The class requires a file with the relative path Config/ServerInfo.cfg with the Socket port on the first line.
    /// </summary>
    public abstract class AbstractSocket
    {
        private static readonly string MessageTerminator = "\r\n";
        private static string ServerIp;
        private static int ServerPort;
        /// <summary>
        /// The encapsulated socket used for communicating
        /// </summary>
        protected Socket _socket;
        /// <summary>
        /// The IPEndPoint
        /// </summary>
        protected IPEndPoint _endPoint;

        /// <summary>
        /// The static constructor. It reads the server port from the first line of the file Config/ServerInfo.cfg
        /// </summary>
        static AbstractSocket()
        {
            try
            {
                string[] lines = System.IO.File.ReadAllLines(@"Config/ServerInfo.cfg");
                ServerIp = lines[0];
                ServerPort = int.Parse(lines[1]);
            }
            catch (Exception)
            {
                CloseApplication("Eroare la citirea informatiilor despre server!");
            }
        }

        /// <summary>
        /// The primary constructor.
        /// </summary>
        public AbstractSocket()
        {
            try
            {
                // Establish the local endpoint for the socket. 
                IPHostEntry ipHost = Dns.GetHostEntry(ServerIp);
                IPAddress ipAddr = ipHost.AddressList[0];
                _endPoint = new IPEndPoint(ipAddr, ServerPort);

                // Creation TCP/IP Socket using
                // Socket Class Constructor
                _socket = new Socket(ipAddr.AddressFamily,
                             SocketType.Stream, ProtocolType.Tcp);
            }
            catch (SocketException)
            {
                CloseApplication("Eroare la crearea capatului de conexiune (socket-ului)!");
            }
        }

        /// <summary>
        /// Method that shows the message in a MessageBox and closes the application.
        /// </summary>
        /// <param name="message">
        /// The message shown in the MessageBox
        /// </param>
        protected static void CloseApplication(string message)
        {
            MessageBox.Show(message);
            Application.Exit();
            Environment.Exit(1);
        }

        /// <summary>
        /// Method used to receive a string from the given Socket
        /// </summary>
        /// <param name="socket">
        /// Socket used for the communication
        /// </param>
        /// <returns>
        /// The bytes received and converted into a string
        /// </returns>
        protected virtual string Receive(Socket socket)
        {
            byte[] bytes = new Byte[1024];
            int numByte;
            string result = "";
            while (true)
            {
                numByte = socket.Receive(bytes);
                result += Encoding.ASCII.GetString(bytes, 0, numByte);
                if (result.Substring(result.Length - MessageTerminator.Length, MessageTerminator.Length) == MessageTerminator)
                    break;
            }
            return result.Substring(0, result.Length - MessageTerminator.Length);
        }

        /// <summary>
        /// Method used to send a string using the given Socket
        /// </summary>
        /// <param name="data">
        /// The string sent
        /// </param>
        /// <param name="socket">
        /// The socket used for communication
        /// </param>
        protected virtual void Send(string data, Socket socket)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(data + MessageTerminator);
            socket.Send(bytes);
        }

        /// <summary>
        /// Function used to close a socket
        /// </summary>
        /// <param name="socket">
        /// The socket which is closed
        /// </param>
        protected void CloseSocket(Socket socket)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}
