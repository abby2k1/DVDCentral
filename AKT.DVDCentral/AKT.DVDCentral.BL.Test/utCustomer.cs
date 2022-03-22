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
    public class CustomerManagerTest
    {
        [TestMethod()]
        public void InsertTest()
        {
            Customer customer = new Customer();
            customer.UserID = 1;

            int results = CustomerManager.Insert(customer, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Customer customer = CustomerManager.LoadByID(1);
            customer.FirstName = "AAAAAAAAAAAA";
            int results = CustomerManager.Update(customer, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Customer customer = CustomerManager.LoadByID(2);
            int results = CustomerManager.Delete(customer.ID, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void LoadTest()
        {
            Assert.AreEqual(3, CustomerManager.Load().Count);
        }

        [TestMethod()]
        public void LoadByIDTest()
        {
            Assert.AreEqual(3, CustomerManager.LoadByID(3).ID);
        }
    }
}