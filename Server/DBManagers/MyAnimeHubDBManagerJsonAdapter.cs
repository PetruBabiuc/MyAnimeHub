using CommonHelpers;
using DataClasses;
using InitialClientActions;
using Interfaces;
using System;
using System.Collections.Generic;

namespace DBManagers
{
    /// <summary>
    /// The adapter class between a client that wants to command an MyAnimeHubDBManager using only strings
    /// It requires the strings to encapsulate requests in JSON format
    /// </summary>
    public class MyAnimeHubDBManagerJsonAdapter : IMyAnimeHubDBManagerAdapter
    {
        /// <summary>
        /// The adaptee object
        /// </summary>
        private IMyAnimeHubDBManager _adaptee = new MyAnimeHubDBManager();

        /// <summary>
        /// The function that logs in or registers the user
        /// </summary>
        /// <param name="request">
        /// The JSON string encapsulating the request
        /// </param>
        /// <param name="result">
        /// The JSON string encapsulating the result
        /// </param>
        /// <returns>
        /// The result of the action encapsulated by the request parameter
        /// </returns>
        public InitialAction LoginOrRegister(string request, out string result)
        {
            Dictionary<string, string> dictionary = JsonUtilities.FromJson<Dictionary<string, string>>(request);
            string requestFunction = dictionary["requestFunction"];
            UserAccount account = JsonUtilities.FromJson<UserAccount>(dictionary["account"]);
            if (requestFunction == "Login")
            {
                if (Login(account, out result))
                    return InitialAction.LoginSuccessful;
                else
                    return InitialAction.LoginFailed;
            }
            else if (requestFunction == "RegisterUserAccount")
            {
                if (RegisterUserAccount(account, out result))
                    return InitialAction.RegistrationSuccessful;
                else
                    return InitialAction.RegistrationFailed; 
            }
            throw new ArgumentException();
        }

        /// <summary>
        /// The function used to process user's requests after logging in
        /// </summary>
        /// <param name="request">
        /// The JSON string encapsulating the request
        /// </param>
        /// <param name="result">
        /// The JSON string encapsulating the result
        /// </param>
        /// <returns>
        /// True if the request was valid, false otherwise
        /// </returns>
        public bool ProcessRequest(string request, out string result)
        {
            result = "{\"result\":\"Cerere invalida!\"}";
            try
            {
                UserAccount account;
                Dictionary<string, string> dictionary = JsonUtilities.FromJson<Dictionary<string, string>>(request);
                string requestFunction = dictionary["requestFunction"];
                switch (requestFunction)
                {
                    case "GetUserAccount":
                        result = JsonUtilities.ToJson(_adaptee.GetUserAccount(dictionary["user"], dictionary["pass"]));
                        return true;
                    case "UpdateProfile":
                        account = JsonUtilities.FromJson<UserAccount>(dictionary["account"]);
                        if (_adaptee.UpdateProfile(account))
                            result = "{\"result\":\"Succes!\"}";
                        else
                            result = "{\"result\":\"Nume utilizator sau nume deja utilizate!\"}";
                        return true;
                    case "AddOrReplaceReview":
                        Review review = JsonUtilities.FromJson<Review>(dictionary["review"]);
                        _adaptee.AddOrReplaceReview(review);
                        result = "{\"result\":\"Succes!\"}";
                        return true;
                    case "GetAnimeList":
                        List<Anime> animeList = _adaptee.GetAnimeList(dictionary["name"], int.Parse(dictionary["pageIndex"]));
                        result = JsonUtilities.ToJson(animeList);
                        return true;
                    case "GetNewsList":
                        List<News> newsList = _adaptee.GetNews(int.Parse(dictionary["pageIndex"]));
                        result = JsonUtilities.ToJson(newsList);
                        return true;
                    case "GetReviews":
                        List<Review> reviewsList = _adaptee.GetReviews(int.Parse(dictionary["animeId"]), int.Parse(dictionary["pageIndex"]));
                        result = JsonUtilities.ToJson(reviewsList);
                        return true;
                    case "GetSpecificAnime":
                        Anime anime = _adaptee.GetSpecificAnime(int.Parse(dictionary["animeId"]));
                        result = JsonUtilities.ToJson(anime);
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Function used to log in the user 
        /// </summary>
        /// <param name="account">
        /// The object encapsulating the user account's data
        /// </param>
        /// <param name="result">
        /// The JSON string that contains the result
        /// </param>
        /// <returns>
        /// True if the login succeded, false otherwise
        /// </returns>
        private bool Login(UserAccount account, out string result)
        {
            string user = account.UserName;
            string pass = account.Password;
            bool loggedIn = _adaptee.Login(user, pass);
            if (loggedIn)
                result = "{\"result\":\"Succes!\"}";
            else
                result = "{\"result\":\"Combinatia dintre nume si parola este invalida!\"}";
            return loggedIn;
        }

        /// <summary>
        /// Method that register an user account
        /// </summary>
        /// <param name="account">
        /// Object containing the data of the new account
        /// </param>
        /// <param name="result">
        /// The JSON string containing the result
        /// </param>
        /// <returns>
        /// True if the registration was successful, false otherwise
        /// </returns>
        private bool RegisterUserAccount(UserAccount account, out string result)
        {
            if(_adaptee.RegisterUserAccount(account))
            {
                result = "{\"result\":\"Succes!\"}";
                return true;
            }
            else
            {
                result = "{\"result\":\"Numele de utilizator sau numele sunt folosite deja!\"}";
                return false;
            }
        }
    }
}
