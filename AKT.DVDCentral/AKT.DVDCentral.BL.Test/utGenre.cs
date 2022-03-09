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
    public class GenreManagerTest
    {
        [TestMethod()]
        public void InsertTest()
        {
            Genre genre = new Genre();
            genre.Description = "Documentary";

            int results = GenreManager.Insert(genre, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Genre genre = GenreManager.LoadByID(1);
            genre.Description = "Mockumentary";
            int results = GenreManager.Update(genre, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Genre genre = GenreManager.LoadByID(2);
            int results = GenreManager.Delete(genre.ID, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void LoadTest()
        {
            Assert.AreEqual(3, GenreManager.Load().Count);
        }

        [TestMethod()]
        public void LoadByIDTest()
        {
            Assert.AreEqual(3, GenreManager.LoadByID(3).ID);
        }
    }
}