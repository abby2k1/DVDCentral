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
    public class DirectorManagerTest
    {
        [TestMethod()]
        public void InsertTest()
        {
            Director director = new Director();
            director.FirstName = "Test";
            director.FirstName = "Insert";

            int results = DirectorManager.Insert(director, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Director director = DirectorManager.LoadByID(1);
            director.FirstName = "Update";
            int results = DirectorManager.Update(director, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Director director = DirectorManager.LoadByID(2);
            int results = DirectorManager.Delete(director.ID, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void LoadTest()
        {
            Assert.AreEqual(3, DirectorManager.Load().Count);
        }

        [TestMethod()]
        public void LoadByIDTest()
        {
            Assert.AreEqual(3, DirectorManager.LoadByID(3).ID);
        }
    }
}