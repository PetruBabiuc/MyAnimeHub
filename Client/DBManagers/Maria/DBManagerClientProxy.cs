/**************************************************************************
 *                                                                        *
 *  File:        DBManagerClientProxy.cs                                  *
 *  Copyright:   (c) 2022, Maria Lupu                                     *
 *  Description: Contains the GetSpecificAnime and GetUserAccount         *
 *               methods of the DBManagerClientProxy class.               *
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
    public partial class DBManagerClientProxy : IMyAnimeHubDBManager
    {
        /// <summary>
        /// Function used to fetch all the data about an anime
        /// </summary>
        /// <param name="animeId">
        /// Desired anime's ID
        /// </param>
        /// <returns>
        /// The object encapsulating the anime
        /// </returns>
        public Anime GetSpecificAnime(int animeId)
        {
            Anime result;
            string data;
            var dictionary = new Dictionary<string, string>();
            dictionary["requestFunction"] = "GetSpecificAnime";
            dictionary["animeId"] = animeId.ToString();
            data = JsonUtilities.ToJson(dictionary);
            _clientSocket.Send(data);
            data = _clientSocket.Receive();
            result = JsonUtilities.FromJson<Anime>(data);
            return result;
        }

        /// <summary>
        /// Method that fetches all the informations about the user profile
        /// </summary>
        /// <param name="user">
        /// The user name of the account
        /// </param>
        /// <param name="pass">
        /// The password of the account
        /// </param>
        /// <returns>
        /// An object that encapsulates all the informations about the profile
        /// </returns>
        public UserAccount GetUserAccount(string user, string pass)
        {
            UserAccount result;
            string data;
            var dictionary = new Dictionary<string, string>();
            dictionary["requestFunction"] = "GetUserAccount";
            dictionary["user"] = user;
            dictionary["pass"] = pass;
            data = JsonUtilities.ToJson(dictionary);
            _clientSocket.Send(data);
            data = _clientSocket.Receive();
            result = JsonUtilities.FromJson<UserAccount>(data);
            return result;
        }
    }
}
