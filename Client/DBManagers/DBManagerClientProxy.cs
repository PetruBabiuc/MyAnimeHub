using CommonHelpers;
using Interfaces;
using Sockets;
using System.Collections.Generic;
using DataClasses;
using System.Windows.Forms;

namespace DBManagers
{
    /// <summary>
    /// The proxy class used by the client to access the DBManager on the server
    /// It also shows the result messages from the server in MessageBoxes
    /// </summary>
    public class DBManagerClientProxy : IMyAnimeHubDBManager
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
            UserAccount account = new UserAccount();
            account.UserName = user;
            account.Password = pass;
            return GenericAccountRequest("Login", account);
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
            return GenericAccountRequest("RegisterUserAccount", account);
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
            return GenericAccountRequest("UpdateProfile", account);
        }

        /// <summary>
        /// Function containing the boilerplate code of all the functions
        /// That receive a refrence to an user account as parameter
        /// </summary>
        /// <param name="requestFunction">
        /// The function to be requested
        /// </param>
        /// <param name="account">
        /// The object that contains relevant informations about the account
        /// For the requested function
        /// </param>
        /// <returns>
        /// True if the request succeded, false otherwise
        /// </returns>
        private bool GenericAccountRequest(string requestFunction, UserAccount account)
        {
            string data;
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["requestFunction"] = requestFunction;
            dictionary["account"] = JsonUtilities.ToJson(account);
            data = JsonUtilities.ToJson(dictionary);
            _clientSocket.Send(data);
            data = _clientSocket.Receive();
            dictionary = JsonUtilities.FromJson<Dictionary<string, string>>(data);
            MessageBox.Show(dictionary["result"]);
            return dictionary["result"] == "Succes!";
        }
    }
}
