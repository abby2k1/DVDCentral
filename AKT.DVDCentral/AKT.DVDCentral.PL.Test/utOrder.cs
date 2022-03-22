using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AKT.DVDCentral.PL;
using System.Linq;
using System;

namespace AKT.DVDCentral.PL.Test
{
    [TestClass]
    public class utOrder
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

            var orders = dc.tblOrders;

            actual = orders.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InsertTest()
        {
            int i;
            tblOrder newrow = new tblOrder();

            newrow.ID = -99;
            newrow.UserID = 1;
            newrow.CustomerID = 1;
            newrow.OrderDate = DateTime.Now;
            newrow.ShipDate = DateTime.Now;

            dc.tblOrders.Add(newrow);

            int rowsaffected = dc.SaveChanges();

            Assert.AreNotEqual(0, rowsaffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblOrder existingRow = dc.tblOrders.Where(dt => dt.ID == 1).FirstOrDefault();

            if (existingRow != null)
            {
                existingRow.ShipDate = DateTime.Now;
                dc.SaveChanges();
            }

            tblOrder updatedRow = dc.tblOrders.Where(dt => dt.ID == 1).FirstOrDefault();

            Assert.AreEqual(existingRow.ShipDate, updatedRow.ShipDate);
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblOrder existingRow = dc.tblOrders.Where(dt => dt.ID == 1).FirstOrDefault();

            if (existingRow != null)
            {
                tblOrderItem existingOrderItemRow = dc.tblOrderItems.Where(dt => dt.OrderID == 1).FirstOrDefault();
                while (existingOrderItemRow != null)
                {
                    dc.tblOrderItems.Remove(existingOrderItemRow);
                    dc.SaveChanges();
                    existingOrderItemRow = dc.tblOrderItems.Where(dt => dt.OrderID == existingRow.ID).FirstOrDefault();
                }
                dc.tblOrders.Remove(existingRow);
                dc.SaveChanges();
            }

            tblOrder deletedRow = dc.tblOrders.Where(dt => dt.ID == 1).FirstOrDefault();

            Assert.IsNull(deletedRow);
        }

        [TestMethod]
        public void LoadByIDTest()
        {
            tblOrder row = dc.tblOrders.Where(dt => dt.ID == 1).FirstOrDefault();

            Assert.IsNotNull(row);
        }
    }
}
