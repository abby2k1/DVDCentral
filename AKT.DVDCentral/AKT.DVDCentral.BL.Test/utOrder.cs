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
    public class OrderManagerTest
    {
        [TestMethod()]
        public void InsertTest()
        {
            Order order = new Order();
            order.CustomerID = 1;
            order.OrderDate = DateTime.Now;
            order.ShipDate = DateTime.Now;

            int results = OrderManager.Insert(order, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Order order = OrderManager.LoadByID(1);
            order.ShipDate = DateTime.Now;
            int results = OrderManager.Update(order, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Order order = OrderManager.LoadByID(2);
            int results = OrderManager.Delete(order.ID, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void LoadTest()
        {
            Assert.AreEqual(3, OrderManager.Load().Count);
        }

        [TestMethod()]
        public void LoadByIDTest()
        {
            Assert.AreEqual(3, OrderManager.LoadByID(3).ID);
        }
    }
}