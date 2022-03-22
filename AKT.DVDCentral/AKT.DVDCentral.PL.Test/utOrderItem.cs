using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AKT.DVDCentral.PL;
using System.Linq;

namespace AKT.DVDCentral.PL.Test
{
    [TestClass]
    public class utOrderItem
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

            var orderItems = dc.tblOrderItems;

            actual = orderItems.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InsertTest()
        {
            int i;
            tblOrderItem newrow = new tblOrderItem();

            newrow.ID = -99;
            newrow.OrderID = 1;
            newrow.MovieID = 1;
            newrow.Quantity = 1;
            newrow.Cost = 1m;

            dc.tblOrderItems.Add(newrow);

            int rowsaffected = dc.SaveChanges();

            Assert.AreNotEqual(0, rowsaffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblOrderItem existingRow = dc.tblOrderItems.Where(dt => dt.ID == 1).FirstOrDefault();

            if (existingRow != null)
            {
                existingRow.Quantity = -99;
                dc.SaveChanges();
            }

            tblOrderItem updatedRow = dc.tblOrderItems.Where(dt => dt.ID == 1).FirstOrDefault();

            Assert.AreEqual(existingRow.Quantity, updatedRow.Quantity);
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblOrderItem existingRow = dc.tblOrderItems.Where(dt => dt.ID == 1).FirstOrDefault();

            if (existingRow != null)
            {
                dc.tblOrderItems.Remove(existingRow);
                dc.SaveChanges();
            }

            tblOrderItem deletedRow = dc.tblOrderItems.Where(dt => dt.ID == 1).FirstOrDefault();

            Assert.IsNull(deletedRow);
        }
    }
}
