using System;
using Interfaces;
using System.Net.Sockets;

namespace Sockets
{
    /// <summary>
    /// Class that handles the communication with the server
    /// </summary>
    public class ClientSocket : AbstractEncryptingSocket, IClientSocket
    {
        /// <summary>
        /// Constructor that establishes the connection to the server
        /// It displays a MessageBox with an error message and closes the application
        /// If the connection fails
        /// </summary>
        public ClientSocket() : base()
        {
            try
            {
                // Connect the socket to the remote endpoint. Catch any errors.  
                _socket.Connect(_endPoint);
            }
            catch (Exception)
            {
                CloseApplication("Nu s-a putut realiza conexiunea la server!");
            }
        }

        /// <summary>
        /// The function used to send a string message to the server
        /// </summary>
        /// <param name="data">
        /// The string to be sent
        /// </param>
        public void Send(string data)
        {
            try
            {
                Send(data, _socket);
            }
            catch (SocketException)
            {
                CloseApplication("Conexiunea la server a fost intrerupta...");
            }
        }

        /// <summary>
        /// The method called to receive a string message from the server
        /// </summary>
        /// <returns>
        /// The bytes sent by the server decoded into a string
        /// </returns>
        public string Receive()
        {
            try
            {
                return Receive(_socket);
            }
            catch (SocketException)
            {
                CloseApplication("Conexiunea la server a fost intrerupta...");
                // Dummy string as return
                // The application will close before the return
                return "";
            }
        }
    }
}
