using AKT.DVDCentral.BL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AKT.DVDCentral.BL.Test
{
    [TestClass()]
    public class UserManagerTest
    {
        [TestMethod()]
        public void InsertTest()
        {
            User user = new User();
            user.FirstName = "Abigail";
            user.LastName = "Thompson";
            user.Username = "abigail";
            user.Password = "abc123";
            int actual = UserManager.Insert(user, true);
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void LoginSuccededTest()
        {
            UserManager.Seed();
            Assert.IsTrue(UserManager.Login(new User { Username = "abigail", Password = "abc123" }));
            UserManager.DeleteAll();
        }

        [TestMethod]
        public void LoginFailedTest()
        {
            try
            {
                UserManager.Seed();
                UserManager.Login(new User { Username = "   ", Password = "bruh" });
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
            UserManager.DeleteAll();
        }

        [TestMethod]
        public void SeedTest()
        {
            UserManager.Seed();
            UserManager.DeleteAll();
        }

        [TestMethod]
        public void LoadTest()
        {
            UserManager.Seed();
            Assert.AreEqual(2, UserManager.Load().Count);
            UserManager.DeleteAll();
        }
    }
}