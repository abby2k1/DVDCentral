using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AKT.DVDCentral.PL;
using System.Linq;

namespace AKT.DVDCentral.PL.Test
{
    [TestClass]
    public class utGenre
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

            var genres = dc.tblGenres;

            actual = genres.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InsertTest()
        {
            int i;
            tblGenre newrow = new tblGenre();

            newrow.ID = -99;
            newrow.Description = "Cooking/Baking";

            dc.tblGenres.Add(newrow);

            int rowsaffected = dc.SaveChanges();

            Assert.AreNotEqual(0, rowsaffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblGenre existingRow = dc.tblGenres.Where(dt => dt.ID == 1).FirstOrDefault();

            if (existingRow != null)
            {
                existingRow.Description = "Test";
                dc.SaveChanges();
            }

            tblGenre updatedRow = dc.tblGenres.Where(dt => dt.ID == 1).FirstOrDefault();

            Assert.AreEqual(existingRow.Description, updatedRow.Description);
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblGenre existingRow = dc.tblGenres.Where(dt => dt.ID == 1).FirstOrDefault();

            //foreign key dependents deletion
            tblMovieGenre existingMovieGenreRow = dc.tblMovieGenres.Where(dt => dt.GenreID == 1).FirstOrDefault();

            if (existingRow != null)
            {
                while (existingMovieGenreRow != null)
                {
                    tblOrderItem existingOrderItemRow = dc.tblOrderItems.Where(dt => dt.MovieID == existingMovieGenreRow.MovieID).FirstOrDefault();
                    while (existingOrderItemRow != null)
                    {
                        dc.tblOrderItems.Remove(existingOrderItemRow);
                        dc.SaveChanges();
                        existingOrderItemRow = dc.tblOrderItems.Where(dt => dt.MovieID == existingMovieGenreRow.MovieID).FirstOrDefault();
                    }

                    dc.tblMovieGenres.Remove(existingMovieGenreRow);
                    dc.SaveChanges();
                    existingMovieGenreRow = dc.tblMovieGenres.Where(dt => dt.GenreID == 1).FirstOrDefault();
                }
                dc.tblGenres.Remove(existingRow);
                dc.SaveChanges();
            }

            tblGenre deletedRow = dc.tblGenres.Where(dt => dt.ID == 1).FirstOrDefault();

            Assert.IsNull(deletedRow);
        }
    }
}
