/**************************************************************************
 *                                                                        *
 *  File:        IMyAnimeHubDBManagerAdapter.cs                           *
 *  Copyright:   (c) 2022, Petru Babiuc                                   *
 *  Description: The interface of an adapter for the                      *
 *               IMyAnimeHubDBManager interface. The clients that would   *
 *               use the implementations of this class are those who      *
 *               would want to use only strings that encapsulate          *
 *               requests and and responses.                              *
 *                                                                        *
 **************************************************************************/
using InitialClientActions;

namespace Interfaces
{
    /// <summary>
    /// The interface of an adapter class used by a client that want to work only with strings
    /// </summary>
    public interface IMyAnimeHubDBManagerAdapter
    {
        /// <summary>
        /// Method that tries to regiter or log in the user.
        /// It may throw exceptions if the request is invalid.
        /// </summary>
        /// <param name="request">
        /// String containing the request
        /// </param>
        /// <param name="result">
        /// String that will be assigned with the result 
        /// </param>
        /// <returns>
        /// The action that the user performed
        /// </returns>
        InitialAction LoginOrRegister(string request, out string result);

        /// <summary>
        /// Method that processes client's request
        /// </summary>
        /// <param name="request">
        /// The string containing the request
        /// </param>
        /// <param name="result">
        /// String that will be assigned with the result 
        /// </param>
        /// <returns>
        /// True if the request was valid, false otherwise
        /// </returns>
        bool ProcessRequest(string request, out string result);
    }
}
