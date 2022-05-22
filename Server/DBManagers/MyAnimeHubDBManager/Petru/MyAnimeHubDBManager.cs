/**************************************************************************
 *                                                                        *
 *  File:        MyAnimeHubDBManager.cs                                   *
 *  Copyright:   (c) 2022, Petru Babiuc                                   *
 *  Description: The file that contains the definitions of the            *
 *               RegisterUserAccount and GetUserAccount methods of the    *
 *               MyAnimeHubDBManager class.                               *
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
    /// <summary>
    /// The SQLiteDatabaseManaged of the MyAnimeHub server
    /// It requires the database to have the RELATIVE path: Persistence/MyAnimeHub.db
    /// </summary>
    public partial class MyAnimeHubDBManager : AbstractSQLiteDBManager, IMyAnimeHubDBManager
    {
        private static readonly string DbPath = $@"{Directory.GetCurrentDirectory()}\Persistence\MyAnimeHub.db";
        /// <summary>
        /// The (maximum) number of the elements on each reviews/news/anime page
        /// </summary>
        private static readonly int PageSize = 2;
        /// <summary>
        /// The ID of the user
        /// </summary>
        private int _userId;

        /// <summary>
        /// Primary constructor
        /// </summary>
        public MyAnimeHubDBManager() : base(DbPath) { }

        /// <summary>
        /// Method to register an account
        /// </summary>
        /// <param name="account">
        /// The object that encapsulates all the informations about the account
        /// </param>
        /// <returns>
        /// True if the registration was successful, false otherwise
        /// </returns>
        public bool RegisterUserAccount(UserAccount account)
        {
            // Mandatory fields must not be null
            if (account.UserName == null || account.Password == null || account.Name == null)
                throw new ArgumentException();

            // Dictionary parameterName to parameterValue
            // Ex: { "@user" to "admin" }
            // Used for SQLite Prepared Statements for improved speed and security
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            // List of strings containing which in which fields will be inserted
            // And which fields will be left 
            List<string> fields = new List<string>();

            // Populating the parameters dictionary and the fields list
            parameters["@user"] = account.UserName;
            fields.Add("user_name");
            parameters["@pass"] = account.Password;
            fields.Add("password");
            parameters["@name"] = account.Name;
            fields.Add("name");
            if (account.Mail != null)
            {
                parameters["@mail"] = account.Mail;
                fields.Add("mail");
            }
            if (account.BirthDate != null)
            {
                parameters["@birthDate"] = account.BirthDate;
                fields.Add("birth_date");
            }

            // String aggregating all the fields from the list of fields
            // Ex: 
            // Fields list: ["user_name", "password", "name"] 
            // Aggregating the strings in the list: "user_name, password, name"
            string fieldsString = fields.Aggregate((s1, s2) => $"{s1}, {s2}");

            // String aggregating all the parameters names from the keys of the parameters dictionary
            // Ex: 
            // Parameters dictionary: { "@user" to "...", "@pass" to "...", "@name" to "..." }
            // Extracting the parameters names (using "Keys" property): ["@user", "@pass", "@name"]
            // Aggregating all the parameters names: "@user, @pass, @name"
            string valuesString = parameters.Keys.Aggregate((s1, s2) => $"{s1}, {s2}");

            string command = $"INSERT INTO users({fieldsString}) VALUES({valuesString})";
            try
            {
                ExecuteNonQuery(command, parameters);
                return true;
            }
            catch (SQLiteException)
            {
                return false;
            }
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
            string query = @"SELECT user_name, password, name, mail, birth_date, user_id FROM users WHERE user_name=@user AND password=@pass";
            Dictionary<string, object> stringParameters = new Dictionary<string, object>();
            stringParameters["@user"] = user;
            stringParameters["@pass"] = pass;
            using (SQLiteDataReader dataReader = ExecuteQuery(query, stringParameters))
            {
                // It should not be possible for a client to request the information of a profile that doesn't exist
                if (!dataReader.Read())
                    throw new ArgumentException();

                // The user should be able to access only information about his profile.
                // If the requested profile has a different ID from his own's, the user is probably not using the client app
                if (dataReader.GetInt32(5) != _userId)
                    throw new ArgumentException();

                UserAccount result = new UserAccount();
                result.UserName = dataReader.GetString(0);
                result.Password = dataReader.GetString(1);
                result.Name = dataReader.GetString(2);
                if (!dataReader.IsDBNull(3))
                    result.Mail = dataReader.GetString(3);
                if (!dataReader.IsDBNull(4))
                    result.BirthDate = dataReader.GetString(4);
                return result;
            }
        }
    }
}