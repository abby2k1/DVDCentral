using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AKT.DVDCentral.PL;
using System.Linq;

namespace AKT.DVDCentral.PL.Test
{
    [TestClass]
    public class utMovieGenre
    {
        protected DVDCentralEntities dc;
        protected IDbContextTransaction tx;

        [TestInitialize]
        public void TestInitialize()
        {
            dc = new DVDCentralEntities();
            tx = dc.Database.BeginTransaction();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            tx.Rollback();
            tx.Dispose();
            dc.Dispose();
        }

        [TestMethod]
        public void LoadTest()
        {
            int expected = 3;
            int actual = 0;

            var movieGenre = dc.tblMovieGenres;

            actual = movieGenre.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InsertTest()
        {
            int i;
            tblMovieGenre newrow = new tblMovieGenre();

            newrow.ID = -99;
            newrow.GenreID = 1;
            newrow.MovieID = 1;

            dc.tblMovieGenres.Add(newrow);

            int rowsaffected = dc.SaveChanges();

            Assert.AreNotEqual(0, rowsaffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblMovieGenre existingRow = dc.tblMovieGenres.Where(dt => dt.ID == 1).FirstOrDefault();

            if (existingRow != null)
            {
                existingRow.MovieID = existingRow.MovieID + 1;
                dc.SaveChanges();
            }

            tblMovieGenre updatedRow = dc.tblMovieGenres.Where(dt => dt.ID == 1).FirstOrDefault();

            Assert.AreEqual(existingRow.MovieID, updatedRow.MovieID);
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblMovieGenre existingRow = dc.tblMovieGenres.Where(dt => dt.ID == 1).FirstOrDefault();

            if (existingRow != null)
            {
                dc.tblMovieGenres.Remove(existingRow);
                dc.SaveChanges();
            }

            tblMovieGenre deletedRow = dc.tblMovieGenres.Where(dt => dt.ID == 1).FirstOrDefault();

            Assert.IsNull(deletedRow);
        }
    }
}
