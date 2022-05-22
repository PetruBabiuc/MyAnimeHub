/**************************************************************************
 *                                                                        *
 *  File:        MyAnimeHubDBManagerJsonAdapter.cs                        *
 *  Copyright:   (c) 2022, Maria Lupu                                     *
 *  Description: The file where the functions Login and                   *
 *               RegisterUserAccount of the                               *
 *               MyAnimeHubDBManagerJsonAdapter class are defined.        *
 *                                                                        *
 **************************************************************************/
using CommonHelpers;
using DataClasses;
using InitialClientActions;
using Interfaces;
using System;
using System.Collections.Generic;

namespace DBManagers
{
    public partial class MyAnimeHubDBManagerJsonAdapter : IMyAnimeHubDBManagerAdapter
    {
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
            if (_adaptee.RegisterUserAccount(account))
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
