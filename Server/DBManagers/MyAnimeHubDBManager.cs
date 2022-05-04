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
    public class MyAnimeHubDBManager : AbstractSQLiteDBManager, IMyAnimeHubDBManager
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

        public MyAnimeHubDBManager() : base(DbPath) { }

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
            catch(SQLiteException ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Function that adds a review in the data base
        /// </summary>
        /// <param name="review">
        /// The object that encapsulates review's data
        /// </param>
        public void AddOrReplaceReview(Review review)
        {
            // Mandatory fields must not be null
            if (review.Anime == null || review.Author == null || review.Content == null || review.Rating == null || review.Title == null)
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
            using(SQLiteDataReader dataReader = ExecuteQuery(query, dictionary))
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
            using(SQLiteDataReader dataReader = ExecuteQuery(query, parameters))
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
