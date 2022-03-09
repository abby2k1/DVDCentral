using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AKT.DVDCentral.PL;
using System.Linq;

namespace AKT.DVDCentral.PL.Test
{
    [TestClass]
    public class utDirector
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

            var directors = dc.tblDirectors;

            actual = directors.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InsertTest()
        {
            int i;
            tblDirector newrow = new tblDirector();

            newrow.ID = -99;
            newrow.FirstName = "PlayStation";
            newrow.LastName = "Vita";

            dc.tblDirectors.Add(newrow);

            int rowsaffected = dc.SaveChanges();

            Assert.AreNotEqual(0, rowsaffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblDirector existingRow = dc.tblDirectors.Where(dt => dt.ID == 1).FirstOrDefault();

            if (existingRow != null)
            {
                existingRow.LastName = "Human";
                dc.SaveChanges();
            }

            tblDirector updatedRow = dc.tblDirectors.Where(dt => dt.ID == 1).FirstOrDefault();

            Assert.AreEqual(existingRow.LastName, updatedRow.LastName);
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblDirector existingRow = dc.tblDirectors.Where(dt => dt.ID == 1).FirstOrDefault();

            //foreign key dependents deletion
            tblMovie existingMovieRow = dc.tblMovies.Where(dt => dt.DirectorID == 1).FirstOrDefault();

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
                    existingMovieRow = dc.tblMovies.Where(dt => dt.DirectorID == 1).FirstOrDefault();
                }
                dc.tblDirectors.Remove(existingRow);
                dc.SaveChanges();
            }

            tblDirector deletedRow = dc.tblDirectors.Where(dt => dt.ID == 1).FirstOrDefault();

            Assert.IsNull(deletedRow);
        }
    }
}
