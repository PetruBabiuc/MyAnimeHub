/**************************************************************************
 *                                                                        *
 *  File:        MyAnimeHubDBManager.cs                                   *
 *  Copyright:   (c) 2022, Maria Lupu                                     *
 *  Description: The file where the functions GetReviews,                 *
 *               UpdateProfile and Login of the MyAnimeHubDBManager       *
 *               are defined.                                             *
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
        /// Function used to log in the client
        /// </summary>
        /// <param name="user">
        /// The user name of the client
        /// </param>
        /// <param name="pass">
        /// The password of the client
        /// </param>
        /// <returns>
        /// True if there is an user with the given credentials, false otherwise
        /// </returns>
        public bool Login(string user, string pass)
        {
            SQLiteDataReader dataReader;
            bool result;
            string query = @"SELECT user_id FROM users WHERE user_name=@user AND password=@pass";
            Dictionary<string, object> stringParameters = new Dictionary<string, object>();
            stringParameters["@user"] = user;
            stringParameters["@pass"] = pass;
            dataReader = ExecuteQuery(query, stringParameters);
            result = dataReader.Read();
            if (result)
                _userId = dataReader.GetInt32(0);
            dataReader.Close();
            return result;
        }

        /// <summary>
        /// The method used to update client's user profile
        /// </summary>
        /// <param name="account">
        /// The object that encapsulates all the new informations about the profile
        /// </param>
        /// <returns>
        /// True if the update was successful, false otherwise
        /// </returns>
        public bool UpdateProfile(UserAccount account)
        {
            // Mandatory fields must not be null
            if (account.UserName == null || account.Password == null || account.Name == null)
                throw new ArgumentException();

            // Dictionary parameterName to parameterValue
            // Ex: { "@user" to "admin" }
            // Used for SQLite Prepared Statements for improved speed and security
            Dictionary<string, object> parameters = new Dictionary<string, object>();


            // Populating the parameters dictionary
            parameters["@user"] = account.UserName;
            parameters["@pass"] = account.Password;
            parameters["@name"] = account.Name;
            string command = "UPDATE users SET user_name=@user, password=@pass, name=@name";
            if (account.Mail != null)
            {
                parameters["@mail"] = account.Mail;
                command += ", mail=@mail";
            }
            if (account.BirthDate != null)
            {
                parameters["@birthDate"] = account.BirthDate;
                command += ", birth_date=@birthDate";
            }
            parameters["@userId"] = _userId;
            command += " WHERE user_id=@userId";
            try
            {
                ExecuteNonQuery(command, parameters);
                return true;
            }
            catch (SQLiteException ex)
            {
                return false;
            }
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
            Review review;
            int i = 0;
            List<Review> result = new List<Review>();
            string query = @"SELECT r.title, r.content, r.rating, u.name                            
                            FROM reviews r JOIN users u USING(user_id)
                            WHERE r.anime_id=@animeId
                            ORDER BY r.review_id DESC";
            var dictionary = new Dictionary<string, object>();
            dictionary["@animeId"] = animeId;
            using (SQLiteDataReader dataReader = ExecuteQuery(query, dictionary))
            {
                SkipFirstNResults(dataReader, pageIndex * PageSize);
                // Inserting reviews from the requested page in the list
                while (i < PageSize && dataReader.Read())
                {
                    review = new Review();
                    review.Title = dataReader.GetString(0);
                    review.Content = dataReader.GetString(1);
                    review.Rating = dataReader.GetInt32(2);
                    review.Author = dataReader.GetString(3);
                    result.Add(review);
                    ++i;
                }

                dataReader.Close();
                return result;
            }
        }
    }
}