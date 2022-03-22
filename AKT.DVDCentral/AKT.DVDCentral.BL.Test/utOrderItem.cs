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
    public class OrderItemManagerTest
    {
        [TestMethod()]
        public void InsertTest()
        {
            OrderItem orderItem = new OrderItem();
            orderItem.OrderID = 1;
            orderItem.MovieID = 1;
            orderItem.Cost = 1m;
            orderItem.Quantity = 1;

            int results = OrderItemManager.Insert(orderItem, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            OrderItem orderItem = OrderItemManager.LoadByID(1);
            orderItem.Cost = -1m;
            int results = OrderItemManager.Update(orderItem, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            OrderItem orderItem = OrderItemManager.LoadByID(2);
            int results = OrderItemManager.Delete(orderItem.ID, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void LoadTest()
        {
            Assert.AreEqual(3, OrderItemManager.Load().Count);
        }

        [TestMethod()]
        public void LoadByIDTest()
        {
            Assert.AreEqual(3, OrderItemManager.LoadByID(3).ID);
        }
    }
}