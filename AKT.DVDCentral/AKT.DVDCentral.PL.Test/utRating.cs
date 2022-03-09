using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AKT.DVDCentral.PL;
using System.Linq;

namespace AKT.DVDCentral.PL.Test
{
    [TestClass]
    public class utRating
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

            var ratings = dc.tblRatings;

            actual = ratings.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InsertTest()
        {
            int i;
            tblRating newrow = new tblRating();

            newrow.ID = -99;
            newrow.Description = "10/10 -IGN";

            dc.tblRatings.Add(newrow);

            int rowsaffected = dc.SaveChanges();

            Assert.AreNotEqual(0, rowsaffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblRating existingRow = dc.tblRatings.Where(dt => dt.ID == 1).FirstOrDefault();

            if (existingRow != null)
            {
                existingRow.Description = "Test";
                dc.SaveChanges();
            }

            tblRating updatedRow = dc.tblRatings.Where(dt => dt.ID == 1).FirstOrDefault();

            Assert.AreEqual(existingRow.Description, updatedRow.Description);
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblRating existingRow = dc.tblRatings.Where(dt => dt.ID == 1).FirstOrDefault();

            //foreign key dependents deletion
            tblMovie existingMovieRow = dc.tblMovies.Where(dt => dt.RatingID == 1).FirstOrDefault();

            if (existingRow != null)
            {
                while (existingMovieRow != null)
                {
                    tblMovieGenre existingMovieGenreRow = dc.tblMovieGenres.Where(dt => dt.MovieID == existingMovieRow.ID).FirstOrDefault();
                    while (existingMovieGenreRow != null)
                    {
                        dc.tblMovieGenres.Remove(existingMovieGenreRow);
                        dc.SaveChanges();
                        existingMovieGenreRow = dc.tblMovieGenres.Where(dt => dt.MovieID == existingMovieRow.ID).FirstOrDefault();
                    }
                    tblOrderItem existingOrderItemRow = dc.tblOrderItems.Where(dt => dt.MovieID == existingMovieRow.ID).FirstOrDefault();
                    while (existingOrderItemRow != null)
                    {
                        dc.tblOrderItems.Remove(existingOrderItemRow);
                        dc.SaveChanges();
                        existingOrderItemRow = dc.tblOrderItems.Where(dt => dt.MovieID == existingMovieRow.ID).FirstOrDefault();
                    }

                    dc.tblMovies.Remove(existingMovieRow);
                    dc.SaveChanges();
                    existingMovieRow = dc.tblMovies.Where(dt => dt.RatingID == 1).FirstOrDefault();
                }
                dc.tblRatings.Remove(existingRow);
                dc.SaveChanges();
            }

            tblRating deletedRow = dc.tblRatings.Where(dt => dt.ID == 1).FirstOrDefault();

            Assert.IsNull(deletedRow);
        }
    }
}
