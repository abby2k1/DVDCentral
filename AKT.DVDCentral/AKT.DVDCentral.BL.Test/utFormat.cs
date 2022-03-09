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
    public class FormatManagerTest
    {
        [TestMethod()]
        public void InsertTest()
        {
            Format format = new Format();
            format.Description = "CED";

            int results = FormatManager.Insert(format, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Format format = FormatManager.LoadByID(1);
            format.Description = "DVD-RAM";
            int results = FormatManager.Update(format, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Format format = FormatManager.LoadByID(2);
            int results = FormatManager.Delete(format.ID, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void LoadTest()
        {
            Assert.AreEqual(3, FormatManager.Load().Count);
        }

        [TestMethod()]
        public void LoadByIDTest()
        {
            Assert.AreEqual(3, FormatManager.LoadByID(3).ID);
        }
    }
}