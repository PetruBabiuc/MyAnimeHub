/**************************************************************************
 *                                                                        *
 *  File:        MyAnimeHubDBManager.cs                                   *
 *  Copyright:   (c) 2022, Cezar Dondas                                   *
 *  Description: This is the file containing implementations of the       *
 *               ReplaceReview, ReviewExists, SkipFirstNResults,          *
 *               GetAnimeList and GetSpecificAnime methods of the         *
 *               MyAnimeHubDBManager class.                               *
 *                                                                        *
 **************************************************************************/
using DataClasses;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace DBManagers
{
    public partial class MyAnimeHubDBManager : AbstractSQLiteDBManager, IMyAnimeHubDBManager
    {
        /// <summary>
        /// The function that replaces (updates) a review
        /// </summary>
        /// <param name="parameters">
        /// Dictionary used for the UPDATE Prepared Statement
        /// </param>
        private void ReplaceReview(Dictionary<string, object> parameters)
        {
            string command = @"UPDATE reviews SET title=@title, content=@content, rating=@rating WHERE
                            anime_id=(SELECT a.anime_id FROM anime a WHERE a.name=@anime)
                            AND user_id=(SELECT u.user_id FROM users u WHERE u.name=@author)";
            ExecuteNonQuery(command, parameters);
        }

        /// <summary>
        /// Method that checks if the user has already reviewed the anime
        /// </summary>
        /// <param name="author">
        /// User's name
        /// </param>
        /// <param name="anime">
        /// Anime's name
        /// </param>
        /// <returns>
        /// True is the user has already reviewed the anime, false otherwise
        /// </returns>
        private bool ReviewExists(string author, string anime)
        {
            bool result;
            string query = @"SELECT COUNT(*) 
                        FROM reviews r 
                        WHERE r.anime_id=
                            (SELECT a.anime_id FROM anime a WHERE a.name=@anime) 
                        AND r.user_id=
                            (SELECT u.user_id FROM users u where u.name=@author)";
            var parameters = new Dictionary<string, object>();
            parameters["@author"] = author;
            parameters["@anime"] = anime;
            using (SQLiteDataReader dataReader = ExecuteQuery(query, parameters))
            {
                dataReader.Read();
                result = dataReader.GetInt32(0) == 1;
                dataReader.Close();
                return result;
            }
        }

        /// <summary>
        /// Function used to skip the first n query results
        /// </summary>
        /// <param name="dataReader">
        /// The SQLiteDataReader used to read the query results
        /// </param>
        /// <param name="n">
        /// The number of skipped query results
        /// </param>
        private void SkipFirstNResults(SQLiteDataReader dataReader, int n)
        {
            // Skipping the first pageIndex pages
            int i = 0;
            while (i < n && dataReader.Read())
                ++i;
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
            Anime anime;
            int i = 0;
            List<Anime> result = new List<Anime>();
            string query = @"SELECT anime_id, name FROM anime WHERE LOWER(name) LIKE LOWER(@name)";
            var dictionary = new Dictionary<string, object>();
            dictionary["@name"] = $"%{name}%";
            using (SQLiteDataReader dataReader = ExecuteQuery(query, dictionary))
            {
                SkipFirstNResults(dataReader, pageIndex * PageSize);
                // Inserting anime from the requested page in the list
                while (i < PageSize && dataReader.Read())
                {
                    anime = new Anime();
                    anime.ID = dataReader.GetInt32(0);
                    anime.Name = dataReader.GetString(1);

                    result.Add(anime);
                    ++i;
                }

                dataReader.Close();
                return result;
            }
        }

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
            Anime anime = new Anime();
            string query = $@"SELECT anime_id, name, author, episodes_number, description, 
                                (SELECT AVG(rating) FROM reviews r WHERE r.anime_id={animeId}) 
                                FROM anime WHERE anime_id={animeId}";
            using (SQLiteDataReader dataReader = ExecuteQuery(query))
            {
                if (!dataReader.Read())
                    throw new ArgumentException();
                anime.ID = dataReader.GetInt32(0);
                anime.Name = dataReader.GetString(1);
                anime.Author = dataReader.GetString(2);
                if (!dataReader.IsDBNull(3))
                    anime.EpisodesNumber = dataReader.GetInt32(3);
                anime.Description = dataReader.GetString(4);
                if (!dataReader.IsDBNull(5))
                    anime.AverageRating = dataReader.GetFloat(5);
                dataReader.Close();
                return anime;
            }
        }
    }
}