/**************************************************************************
 *                                                                        *
 *  File:        DBManagerClientProxy.cs                                  *
 *  Copyright:   (c) 2022, Petru Babiuc                                   *
 *  Description: Contains AddOrReplaceReview and GetAnimeList             *
 *               methods of the DBManagerClientProxy class,               *
 *               as well as the private attribute _clientSocket.          *
 *                                                                        *
 **************************************************************************/

using CommonHelpers;
using DataClasses;
using Interfaces;
using Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBManagers
{
    /// <summary>
    /// The proxy class used by the client to access the DBManager on the server
    /// It also shows the result messages from the server in MessageBoxes
    /// </summary>
    public partial class DBManagerClientProxy : IMyAnimeHubDBManager
    {
        /// <summary>
        /// The Socket for communicating with the client
        /// </summary>
        private ClientSocket _clientSocket = new ClientSocket();

        /// <summary>
        /// Function that adds a review in the data base
        /// </summary>
        /// <param name="review">
        /// The object that encapsulates review's data
        /// </param>
        public void AddOrReplaceReview(Review review)
        {
            string data;
            var dictionary = new Dictionary<string, string>();
            dictionary["requestFunction"] = "AddOrReplaceReview";
            dictionary["review"] = JsonUtilities.ToJson(review);
            data = JsonUtilities.ToJson(dictionary);
            _clientSocket.Send(data);
            data = _clientSocket.Receive();
            dictionary = JsonUtilities.FromJson<Dictionary<string, string>>(data);
            MessageBox.Show(dictionary["result"]);
        }

        /// <summary>
        /// Method used to fetch the anime names and IDs
        /// </summary>
        /// <param name="name">
        /// The string that should be found case insensitively in the anime name
        /// </param>
        /// <param name="pageIndex">
        /// The index of the page
        /// </param>
        /// <returns>
        /// A list of objects encapsulating anime which have a name matching given name
        /// </returns>
        public List<Anime> GetAnimeList(string name, int pageIndex)
        {
            List<Anime> result;
            string data;
            var dictionary = new Dictionary<string, string>();
            dictionary["requestFunction"] = "GetAnimeList";
            dictionary["name"] = name;
            dictionary["pageIndex"] = pageIndex.ToString();
            data = JsonUtilities.ToJson(dictionary);
            _clientSocket.Send(data);
            data = _clientSocket.Receive();
            result = JsonUtilities.FromJson<List<Anime>>(data);
            return result;
        }
    }
}

