using Microsoft.VisualStudio.TestTools.UnitTesting;
using AKT.DVDCentral.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AKT.DVDCentral.BL.Models;

namespace AKT.DVDCentral.BL.Test
{
    [TestClass()]
    public class RatingManagerTest
    {
        [TestMethod()]
        public void InsertTest()
        {
            Rating rating = new Rating();
            rating.Description = "test";

            int results = RatingManager.Insert(rating, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Rating rating = RatingManager.LoadByID(1);
            rating.Description = "test";
            int results = RatingManager.Update(rating, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Rating rating = RatingManager.LoadByID(2);
            int results = RatingManager.Delete(rating.ID, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void LoadTest()
        {
            Assert.AreEqual(3, RatingManager.Load().Count);
        }

        [TestMethod()]
        public void LoadByIDTest()
        {
            Assert.AreEqual(3, RatingManager.LoadByID(3).ID);
        }
    }
}