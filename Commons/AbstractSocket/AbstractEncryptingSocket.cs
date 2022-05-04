using CommonHelpers;
using System.Net.Sockets;


namespace Sockets
{
    /// <summary>
    /// Class used to send and received encrypted strings through Sockets
    /// </summary>
    public abstract class AbstractEncryptingSocket: AbstractSocket
    {
        /// <summary>
        /// Encryption key
        /// </summary>
        private static readonly string Key = "IngineriaProgramarii";

        /// <summary>
        /// Method used to receive a string
        /// </summary>
        /// <param name="socket">
        /// The Socket used for communication
        /// </param>
        protected override string Receive(Socket socket)
        {
            string encrypted = base.Receive(socket);
            return Cryptography.Decrypt(encrypted, Key);
        }

        /// <summary>
        /// Method used to send a string
        /// </summary>
        /// <param name="data">
        /// The string to be sent
        /// </param>
        /// <param name="socket">
        /// The Socket used for communication
        /// </param>
        protected override void Send(string data, Socket socket)
        {
            string encrypted = Cryptography.Encrypt(data, Key);
            base.Send(encrypted, socket);
        }
    }
}
