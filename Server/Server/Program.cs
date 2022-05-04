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
