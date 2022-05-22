/**************************************************************************
 *                                                                        *
 *  File:        UnitTest.cs                                              *
 *  Copyright:   (c) 2022, Maria Lupu                                     *
 *  Description: This is the file ehere the source code of the unit       *
 *               tests is located.                                        *
 *                                                                        *
 **************************************************************************/
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interfaces;
using DBManagers;
using DataClasses;
using System.Text;

namespace Tests
{
    [TestClass]
    public class UnitTest
    {
        private static IMyAnimeHubDBManager _dbManager;
        private static bool _loginResult;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _dbManager = new DBManagerClientProxy();
            _loginResult = _dbManager.Login("admin", "admin");
        }

        [TestMethod]
        public void CorrectLogin()
        {
            Assert.AreEqual(true, _loginResult);
        }

        [TestMethod]
        public void InvalidLogin()
        {
            IMyAnimeHubDBManager _DBManagerProxy = new DBManagerClientProxy();
            Assert.AreEqual(false, _DBManagerProxy.Login("QQQQQQQ", "QQQQQQQQ"));
        }

        [TestMethod]
        public void NewsPage()
        {
            Assert.IsTrue(_dbManager.GetNews(0).Count>0);
        }

        [TestMethod]
        public void TooBigNewsPageIndex()
        {
            Assert.IsTrue(_dbManager.GetNews(5000).Count == 0);
        }

        [TestMethod]
        public void AnimePage()
        {
            Assert.IsTrue(_dbManager.GetAnimeList("", 0).Count > 0);
        }

        [TestMethod]
        public void TooBigAnimePageIndex()
        {
            Assert.IsTrue(_dbManager.GetAnimeList("", 5000).Count == 0);
        }

        [TestMethod]
        public void ReviewsPage()
        {
            Assert.IsTrue(_dbManager.GetReviews(1, 0).Count > 0);
        }

        [TestMethod]
        public void TooBigReviewsPageIndex()
        {
            Assert.IsTrue(_dbManager.GetReviews(1, 5000).Count == 0);
        }

        [TestMethod]
        public void SpecificAnime()
        {
            Anime anime = _dbManager.GetSpecificAnime(1);
            Assert.AreEqual(anime.EpisodesNumber, 500);
            Assert.AreEqual(anime.ID, 1);
            Assert.AreEqual(anime.Author, "Masashi Kishimoto");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidNameOfUserAccount()
        {
            UserAccount account = new UserAccount();
            account.Name = "12";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidUserName()
        {
            UserAccount account = new UserAccount();
            account.UserName = "12";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidBirthDate()
        {
            UserAccount account = new UserAccount();
            account.BirthDate = "1/13/2000";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidMail()
        {
            UserAccount account = new UserAccount();
            account.BirthDate = "florinel@abcd.kr";
        }

        [TestMethod]
        public void ValidUserAccount()
        {
            UserAccount account = new UserAccount();
            account.Name = "Ronaldinho99";
            account.Mail = "andreiflorinel@yahoo.ro";
            account.BirthDate = "5/6/1999";
            account.UserName = "admin68";
            account.Password = "admin68";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyReviewTitle()
        {
            Review review = new Review();
            review.Title = "";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReviewTitleTooLong()
        {
            Review review = new Review();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < 50; ++i)
                stringBuilder.Append("A");
            review.Title = stringBuilder.ToString();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyReviewContent()
        {
            Review review = new Review();
            review.Content = "";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReviewContentTooLong()
        {
            Review review = new Review();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < 1000; ++i)
                stringBuilder.Append("0123456789");
            stringBuilder.Append("X");
            review.Content = stringBuilder.ToString();
        }

        [TestMethod]
        public void ValidReview()
        {
            Review review = new Review();
            review.Anime = "Bleach";
            review.Author = "Florinel";
            review.Content = "adfsdfs";
            review.Title = "OK!";
        }
    }
}
