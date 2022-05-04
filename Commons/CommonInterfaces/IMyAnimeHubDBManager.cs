
using DataClasses;
using System.Collections.Generic;

namespace Interfaces
{
    /// <summary>
    /// The interface of the MyAnimeHub data base manager.
    /// It contains all the methods needed to operate with the database
    /// </summary>
    public interface IMyAnimeHubDBManager
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
        bool Login(string user, string pass);

        /// <summary>
        /// Method to register an account
        /// </summary>
        /// <param name="account">
        /// The object that encapsulates all the informations about the account
        /// </param>
        /// <returns>
        /// True if the registration was successful, false otherwise
        /// </returns>
        bool RegisterUserAccount(UserAccount account);

        /// <summary>
        /// The method used to update client's user profile
        /// </summary>
        /// <param name="account">
        /// The object that encapsulates all the new informations about the profile
        /// </param>
        /// <returns>
        /// True if the update was successful, false otherwise
        /// </returns>
        bool UpdateProfile(UserAccount account);

        /// <summary>
        /// Function that adds a review in the data base
        /// </summary>
        /// <param name="review">
        /// The object that encapsulates review's data
        /// </param>
        void AddOrReplaceReview(Review review);

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
        UserAccount GetUserAccount(string user, string pass);

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
        List<Anime> GetAnimeList(string name, int pageIndex);

        /// <summary>
        /// Function used to obtain the news
        /// </summary>
        /// <param name="pageIndex">
        /// The index of the news page
        /// </param>
        /// <returns>
        /// A list of objects encapsulating news
        /// </returns>
        List<News> GetNews(int pageIndex);

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
        List<Review> GetReviews(int animeId, int pageIndex);

        /// <summary>
        /// Function used to fetch all the data about an anime
        /// </summary>
        /// <param name="animeId">
        /// Desired anime's ID
        /// </param>
        /// <returns>
        /// The object encapsulating the anime
        /// </returns>
        Anime GetSpecificAnime(int animeId);
    }
}
