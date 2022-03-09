using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AKT.DVDCentral.PL;
using System.Linq;

namespace AKT.DVDCentral.PL.Test
{
    [TestClass]
    public class utMovie
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

            var movies = dc.tblMovies;

            actual = movies.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InsertTest()
        {
            int i;
            tblMovie newrow = new tblMovie();

            newrow.ID = -99;
            newrow.Title = "Test";
            newrow.Description = "How to test.";
            newrow.Cost = 79.99M;
            newrow.RatingID = 1;           
            newrow.FormatID = 1;
            newrow.DirectorID = 1;
            newrow.InStkQty = 1;

            dc.tblMovies.Add(newrow);

            int rowsaffected = dc.SaveChanges();

            Assert.AreNotEqual(0, rowsaffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblMovie existingRow = dc.tblMovies.Where(dt => dt.ID == 1).FirstOrDefault();

            if (existingRow != null)
            {
                existingRow.Description = "Test";
                dc.SaveChanges();
            }

            tblMovie updatedRow = dc.tblMovies.Where(dt => dt.ID == 1).FirstOrDefault();

            Assert.AreEqual(existingRow.Description, updatedRow.Description);
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblMovie existingRow = dc.tblMovies.Where(dt => dt.ID == 1).FirstOrDefault();

            if (existingRow != null)
            {
                tblMovieGenre existingMovieGenreRow = dc.tblMovieGenres.Where(dt => dt.MovieID == 1).FirstOrDefault();
                tblOrderItem existingOrderItemRow = dc.tblOrderItems.Where(dt => dt.MovieID == 1).FirstOrDefault();
                while (existingMovieGenreRow != null)
                {
                    dc.tblMovieGenres.Remove(existingMovieGenreRow);
                    dc.SaveChanges();
                    existingMovieGenreRow = dc.tblMovieGenres.Where(dt => dt.MovieID == existingRow.ID).FirstOrDefault();
                }
                while (existingOrderItemRow != null)
                {
                    dc.tblOrderItems.Remove(existingOrderItemRow);
                    dc.SaveChanges();
                    existingOrderItemRow = dc.tblOrderItems.Where(dt => dt.MovieID == existingRow.ID).FirstOrDefault();
                }               
                dc.tblMovies.Remove(existingRow);
                dc.SaveChanges();
            }

            tblMovie deletedRow = dc.tblMovies.Where(dt => dt.ID == 1).FirstOrDefault();

            Assert.IsNull(deletedRow);
        }
    }
}
