/**************************************************************************
 *                                                                        *
 *  File:        DBManagerClientProxy.cs                                  *
 *  Copyright:   (c) 2022, Cezar Dondas                                   *
 *  Description: The implementations Login, RegisterUserAccount,          *
 *               UpdateProfile and GenericAccountRequest of the           *
 *               DBManagerClientProxyClass' methods.                      *
 *                                                                        *
 **************************************************************************/
using CommonHelpers;
using DataClasses;
using Interfaces;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DBManagers
{
    public partial class DBManagerClientProxy : IMyAnimeHubDBManager
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
