/**************************************************************************
 *                                                                        *
 *  File:        Program.cs                                               *
 *  Copyright:   (c) 2022, Petru Babiuc                                   *
 *  Description: The entry point of the server application.               *
 *                                                                        *
 **************************************************************************/
using Sockets;


namespace Server
{
    /// <summary>
    /// Main class
    /// </summary>
    class Program
    {
        /// <summary>
        /// Server's entry point (Main function)
        /// </summary>
        /// <param name="args">
        /// Command line arguments (unused at the moment)
        /// </param>
        static void Main(string[] args)
        {
            ServerSocket server = ServerSocket.Instance;
            server.StartServingAsynchronously();
        }
    }
}
