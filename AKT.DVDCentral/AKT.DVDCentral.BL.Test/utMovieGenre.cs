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
    public class MovieGenreManagerTest
    {
        [TestMethod()]
        public void InsertTest()
        {
            MovieGenre movieGenre = new MovieGenre();
            movieGenre.MovieID = 1;
            movieGenre.GenreID = -99;

            int results = MovieGenreManager.Insert(movieGenre, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            MovieGenre movieGenre = new MovieGenre() { ID = 1, MovieID = -1, GenreID = -1 };
            int results = MovieGenreManager.Update(movieGenre, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            int results = MovieGenreManager.Delete(1, true);
            Assert.AreEqual(1, results);
        }
    }
}