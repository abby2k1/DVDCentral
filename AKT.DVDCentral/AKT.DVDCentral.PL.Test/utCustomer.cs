using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AKT.DVDCentral.PL;
using System.Linq;
using System;

namespace AKT.DVDCentral.PL.Test
{
    [TestClass]
    public class utCustomer
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

            var customers = dc.tblCustomers;

            actual = customers.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InsertTest()
        {
            int i;
            tblCustomer newrow = new tblCustomer();

            newrow.ID = -99;
            newrow.UserID = 1;

            dc.tblCustomers.Add(newrow);

            int rowsaffected = dc.SaveChanges();

            Assert.AreNotEqual(0, rowsaffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblCustomer existingRow = dc.tblCustomers.Where(dt => dt.ID == 1).FirstOrDefault();

            if (existingRow != null)
            {
                existingRow.FirstName = "Amerie";
                dc.SaveChanges();
            }

            tblCustomer updatedRow = dc.tblCustomers.Where(dt => dt.ID == 1).FirstOrDefault();

            Assert.AreEqual(existingRow.FirstName, updatedRow.FirstName);
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblCustomer existingRow = dc.tblCustomers.Where(dt => dt.ID == 1).FirstOrDefault();

            if (existingRow != null)
            {
                tblOrder existingOrderRow = dc.tblOrders.Where(dt => dt.CustomerID == existingRow.ID).FirstOrDefault();
                while (existingOrderRow != null)
                {
                    tblOrderItem existingOrderItemRow = dc.tblOrderItems.Where(dt => dt.OrderID == existingOrderRow.ID).FirstOrDefault();
                    while (existingOrderItemRow != null)
                    {
                        dc.tblOrderItems.Remove(existingOrderItemRow);
                        dc.SaveChanges();
                        existingOrderItemRow = dc.tblOrderItems.Where(dt => dt.OrderID == existingOrderRow.ID).FirstOrDefault();
                    }
                    dc.tblOrders.Remove(existingOrderRow);
                    dc.SaveChanges();
                    existingOrderRow = dc.tblOrders.Where(dt => dt.CustomerID == existingRow.ID).FirstOrDefault();
                }
                dc.tblCustomers.Remove(existingRow);
                dc.SaveChanges();
            }

            tblCustomer deletedRow = dc.tblCustomers.Where(dt => dt.ID == 1).FirstOrDefault();

            Assert.IsNull(deletedRow);
        }

        [TestMethod]
        public void LoadByIDTest()
        {
            tblCustomer row = dc.tblCustomers.Where(dt => dt.ID == 1).FirstOrDefault();

            Assert.IsNotNull(row);
        }
    }
}
