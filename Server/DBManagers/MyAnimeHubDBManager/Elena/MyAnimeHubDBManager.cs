/**************************************************************************
 *                                                                        *
 *  File:        MyAnimeHubDBManager.cs                                   *
 *  Copyright:   (c) 2022, Elena Chelarasu                                *
 *  Description: The file where the methods AddOrReplaceReview,           *
 *               UserHasName, AddReview and GetNews are defined.          *
 *                                                                        *
 **************************************************************************/
using DataClasses;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;

namespace DBManagers
{
    public partial class MyAnimeHubDBManager : AbstractSQLiteDBManager, IMyAnimeHubDBManager
    {
        /// <summary>
        /// Function that adds a review in the data base
        /// </summary>
        /// <param name="review">
        /// The object that encapsulates review's data
        /// </param>
        public void AddOrReplaceReview(Review review)
        {
            // Mandatory fields must not be null
            if (review.Anime == null || review.Author == null || review.Content == null || review.Rating == 0 || review.Title == null)
                throw new ArgumentException();

            // If there is no user account with the review's author name
            // The user is probably not using the client application...
            if (!UserHasName(review.Author))
                throw new ArgumentException();

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["@anime"] = review.Anime;
            parameters["@author"] = review.Author;
            parameters["@content"] = review.Content;
            parameters["@rating"] = review.Rating;
            parameters["@title"] = review.Title;
            if (ReviewExists(review.Author, review.Anime))
                ReplaceReview(parameters);
            else
                AddReview(parameters);
        }

        /// <summary>
        /// Function used to check if there is an user with the given name
        /// </summary>
        /// <param name="name">
        /// The name
        /// </param>
        /// <returns>
        /// True if there is such an user, false otherwise
        /// </returns>
        private bool UserHasName(string name)
        {
            bool result;
            string query = @"SELECT COUNT(*) FROM users 
                            WHERE name=@name AND user_id=@userId";
            var dictionary = new Dictionary<string, object>();
            dictionary["@name"] = name;
            dictionary["@userId"] = _userId;
            using (SQLiteDataReader dataReader = ExecuteQuery(query, dictionary))
            {
                dataReader.Read();
                result = dataReader.GetInt32(0) == 1;
                dataReader.Close();
                return result;
            }
        }

        /// <summary>
        /// Function to add a review
        /// </summary>
        /// <param name="parameters">
        /// Dictionary used for the INSERT Prepared Statement
        /// </param>
        private void AddReview(Dictionary<string, object> parameters)
        {
            string command = @"INSERT INTO reviews(anime_id, user_id, title, content, rating) VALUES(
                                (SELECT a.anime_id FROM anime a WHERE a.name=@anime),
                                (SELECT u.user_id FROM users u WHERE u.name=@author),
                                @title, @content, @rating)";
            ExecuteNonQuery(command, parameters);
        }

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
            News news;
            int i = 0;
            List<News> result = new List<News>();
            string query = "SELECT title, description FROM news ORDER BY news_id DESC";
            using (SQLiteDataReader dataReader = ExecuteQuery(query))
            {
                SkipFirstNResults(dataReader, pageIndex * PageSize);
                // Inserting news from the requested page in the list
                while (i < PageSize && dataReader.Read())
                {
                    news = new News();
                    news.Title = dataReader.GetString(0);
                    news.Description = dataReader.GetString(1);
                    result.Add(news);
                    ++i;
                }

                dataReader.Close();
                return result;
            }
        }
    }
}
