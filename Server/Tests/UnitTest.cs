/**************************************************************************
 *                                                                        *
 *  File:        UnitTest.cs                                              *
 *  Copyright:   (c) 2022, Elena Chelarasu                                *
 *  Description: This is the file containing the class that tests the     *
 *               functionality of the server.                             *
 *                                                                        *
 **************************************************************************/
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonHelpers;
using DataClasses;
using System.Collections.Generic;
using Interfaces;
using DBManagers;
using InitialClientActions;

namespace Tests
{
    [TestClass]
    public class UnitTest
    {
        private static IMyAnimeHubDBManagerAdapter _dbManagerAdapter = new MyAnimeHubDBManagerJsonAdapter();
        private static string _resultString;
        private static InitialAction _loginResult;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            UserAccount account = new UserAccount();
            account.UserName = "admin";
            account.Password = "admin";
            var dict = new Dictionary<string, string>
            {
                {"account", JsonUtilities.ToJson(account) },
                {"requestFunction", "Login" }
            };
            _loginResult = _dbManagerAdapter.LoginOrRegister(JsonUtilities.ToJson(dict), out _resultString);
        }

        [TestMethod]
        public void EncryptDecrypt()
        {
            string pass = "pass";
            string rawText = "text";
            string encryptedText = Cryptography.Encrypt(rawText, pass);
            Assert.AreNotEqual(rawText, encryptedText);
            string decryptedText = Cryptography.Decrypt(encryptedText, pass);
            Assert.AreEqual(rawText, decryptedText);
        }

        [TestMethod]
        public void SerializeDeserialize()
        {
            UserAccount account = new UserAccount();
            account.BirthDate = "5/6/1999";
            account.Mail = "andreiflorinel@yahoo.ro";
            account.Name = "Georgel";
            account.UserName = "admin";
            account.Password = "admin";

            string json = JsonUtilities.ToJson(account);

            var dict = JsonUtilities.FromJson<Dictionary<string, string>>(json);
            Assert.AreEqual(dict["Name"], account.Name);
            Assert.AreEqual(dict["BirthDate"], account.BirthDate);
            Assert.AreEqual(dict["Mail"], account.Mail);
            Assert.AreEqual(dict["UserName"], account.UserName);
            Assert.AreEqual(dict["Password"], account.Password);

            UserAccount deserializedAccount = JsonUtilities.FromJson<UserAccount>(json);
            Assert.AreEqual(deserializedAccount.Name, account.Name);
            Assert.AreEqual(deserializedAccount.BirthDate, account.BirthDate);
            Assert.AreEqual(deserializedAccount.Mail, account.Mail);
            Assert.AreEqual(deserializedAccount.UserName, account.UserName);
            Assert.AreEqual(deserializedAccount.Password, account.Password);
        }

        [TestMethod]
        public void SuccessfulLogin()
        {
            Assert.AreEqual(InitialAction.LoginSuccessful, _loginResult);
            Assert.AreEqual(_resultString, "{\"result\":\"Succes!\"}");
        }

        [TestMethod]
        public void FailedLogin()
        {
            UserAccount account = new UserAccount();
            account.UserName = "adminnn";
            account.Password = "admin";
            var dict = new Dictionary<string, string>
            {
                {"account", JsonUtilities.ToJson(account) },
                {"requestFunction", "Login" }
            };
            IMyAnimeHubDBManagerAdapter dbManager = new MyAnimeHubDBManagerJsonAdapter();
            string resultString;
            InitialAction result = dbManager.LoginOrRegister(JsonUtilities.ToJson(dict), out resultString);
            Assert.AreEqual(InitialAction.LoginFailed, result);
            Assert.AreEqual(resultString, "{\"result\":\"Combinatia dintre nume si parola este invalida!\"}");
        }

        [TestMethod]
        public void GetUserAccount()
        {
            string data, result;
            var dictionary = new Dictionary<string, string>();
            dictionary["requestFunction"] = "GetUserAccount";
            dictionary["user"] = "admin";
            dictionary["pass"] = "admin";
            data = JsonUtilities.ToJson(dictionary);
            _dbManagerAdapter.ProcessRequest(data, out result);
            UserAccount resultAccount = JsonUtilities.FromJson<UserAccount>(result);
            Assert.AreEqual(resultAccount.Mail, null);
            Assert.AreEqual(resultAccount.Name, "Petru");
            Assert.AreEqual(resultAccount.UserName, "admin");
            Assert.AreEqual(resultAccount.Password, "admin");
            Assert.AreEqual(resultAccount.BirthDate, "6/7/1999");
        }
    }
}
