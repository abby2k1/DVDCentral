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
    public class MovieManagerTest
    {
        [TestMethod()]
        public void InsertTest()
        {
            Movie movie = new Movie();
            movie.Title = "Clarkson's Farm";
            movie.Description = "A show about farming with Jeremy Clarkson";
            movie.Cost = 1.99M;
            movie.RatingID = 1;
            movie.FormatID = 1;
            movie.DirectorID = 1;
            movie.InStkQty = 1;
            movie.ImagePath = "";

            int results = MovieManager.Insert(movie, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Movie movie = MovieManager.LoadByID(1);
            movie.Description = "Mockumentary about something or other";
            int results = MovieManager.Update(movie, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Movie movie = MovieManager.LoadByID(2);
            int results = MovieManager.Delete(movie.ID, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void LoadTest()
        {
            Assert.AreEqual(3, MovieManager.Load().Count);
        }

        [TestMethod()]
        public void LoadByIDTest()
        {
            Assert.AreEqual(3, MovieManager.LoadByID(3).ID);
        }
    }
}