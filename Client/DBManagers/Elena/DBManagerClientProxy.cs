/**************************************************************************
 *                                                                        *
 *  File:        DBManagerClientProxy.cs                                  *
 *  Copyright:   (c) 2022, Elena Chelarasu                                *
 *  Description: This file defines the GetNews and GetReviews methods     *
 *               of the DBManagerClientProxy class.                       *
 *                                                                        *
 **************************************************************************/
using CommonHelpers;
using DataClasses;
using Interfaces;
using Sockets;
using System.Collections.Generic;

namespace DBManagers
{
    public partial class DBManagerClientProxy : IMyAnimeHubDBManager
    {
        /// <summary>
        /// Function used to obtain the news
        /// </summary>
        /// <param name="pageIndex">
        /// The index of the news page
        /// </param>
        /// <returns>
        /// A list of objects encapsulating news
        /// </returns>
        public List<News> GetNews(int pageIndex)
        {
            List<News> result;
            string data;
            var dictionary = new Dictionary<string, string>();
            dictionary["requestFunction"] = "GetNewsList";
            dictionary["pageIndex"] = pageIndex.ToString();
            data = JsonUtilities.ToJson(dictionary);
            _clientSocket.Send(data);
            data = _clientSocket.Receive();
            result = JsonUtilities.FromJson<List<News>>(data);
            return result;
        }

        /// <summary>
        /// Method that finds the reviews from the desired anime
        /// </summary>
        /// <param name="animeId">
        /// The ID of the anime whose reviews are wanted
        /// </param>
        /// <param name="pageIndex">
        /// The index of the reviews page
        /// </param>
        /// <returns>
        /// A list of objects encapsulating reviews
        /// </returns>
        public List<Review> GetReviews(int animeId, int pageIndex)
        {
            List<Review> result;
            string data;
            var dictionary = new Dictionary<string, string>();
            dictionary["requestFunction"] = "GetReviews";
            dictionary["animeId"] = animeId.ToString();
            dictionary["pageIndex"] = pageIndex.ToString();
            data = JsonUtilities.ToJson(dictionary);
            _clientSocket.Send(data);
            data = _clientSocket.Receive();
            result = JsonUtilities.FromJson<List<Review>>(data);
            return result;
        }
    }
}
