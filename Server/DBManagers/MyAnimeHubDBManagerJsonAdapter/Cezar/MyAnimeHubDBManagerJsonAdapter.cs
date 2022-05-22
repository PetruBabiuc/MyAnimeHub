/**************************************************************************
 *                                                                        *
 *  File:        MyAnimeHubDBManagerJsonAdapter.cs                        *
 *  Copyright:   (c) 2022, Cezar Dondas                                   *
 *  Description: The file that has the implementations of the             *
 *               LoginOrRegister method of the                            *
 *               MyAnimeHubDBManagerJsonAdapter class.                    *
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
    }
}