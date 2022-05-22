/**************************************************************************
 *                                                                        *
 *  File:        IClientSocket.cs                                         *
 *  Copyright:   (c) 2022, Maria Lupu                                     *
 *  Description: Defines the IClientSocket interface. It declares the     *
 *               minimal functionality of the classes that will be used   *
 *               by the DBManagerClientProxy to interoperate with the     *
 *               sever.                                                   *
 *                                                                        *
 **************************************************************************/

namespace Interfaces
{
    /// <summary>
    /// The interface of the client's connection to the server
    /// </summary>
    public interface IClientSocket
    {
        /// <summary>
        /// The function used to send a string message to the server
        /// </summary>
        /// <param name="data">
        /// The string to be sent
        /// </param>
        void Send(string data);

        /// <summary>
        /// The method called to receive a string message from the server
        /// </summary>
        /// <returns>
        /// The bytes sent by the server decoded into a string
        /// </returns>
        string Receive();
    }
}
