using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using AKT.DVDCentral.PL;

/*
    Aesthetic preference:
        Changed Id to ID,
        Changed UserPass to Password,
        Changed UserName to Username,
        Changed tblMovie Quantity to InStkQty
    Non-aesthetic preference:
        Changed cost from double to decimal because storing monetary values using floating point datatypes leads to unexpected behavior
        ShipDate should be nullable because there is time between when an order is placed and when it is shipped, but I feel adding Nullable`1 to the DataTypes enum to prevent the fail
            "Assert.IsTrue failed. Errors: Failed: Data Type: tblOrder.ShipDate (Nullable`1 is not DateTime)"
            would make the test too lenient, so I will instead have to disallow null values because I do not know how else I would solve the problem with minimal change to these tests.
    -Abby
*/

namespace AKT.DVDCentral.PL.Test
{
    public enum DataTypes
    {
        String = 0,
        Double,
        Int32,
        Guid,
        DateTime,
        Decimal //added
    }

    [TestClass]
    public class utTableStructure
    {

        private bool CheckColumnDefinition(Type tableType,
                                          string columnName,
                                          DataTypes dataType,
                                          ref string message,
                                          ref string errmessage)
        {
            try
            {
                if (tableType.GetProperty(columnName) != null)
                {
                    var property = tableType.GetProperty(columnName);
                    if (property.PropertyType.Name.Equals(dataType.ToString()))
                    {
                        message += "Passed: " + tableType.Name + "." + columnName + " (" + dataType.ToString() + ")\r\n";
                        return true;
                    }
                    else
                    {
                        errmessage += "Failed: Data Type: "
                                    + tableType.Name
                                    + "." + columnName
                                    + " (" + property.PropertyType.Name
                                    + " is not "
                                    + dataType.ToString()
                                    + ")\r\n";

                        message += "Failed: Data Type: "
                                    + tableType.Name
                                    + "." + columnName
                                    + " (" + property.PropertyType.Name
                                    + " is not "
                                    + dataType.ToString()
                                    + ")\r\n";

                        return false;
                    }
                }
                else
                {
                    errmessage += "Failed: " + tableType.Name + "." + columnName + " does not exist.\r\n";
                    message += "Failed: " + tableType.Name + "." + columnName + " does not exist.\r\n";
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                errmessage += "Failed: " + tableType.Name + "." + columnName + " does not exist.\r\n";
                message += "Failed: " + tableType.Name + "." + columnName + " does not exist.\r\n";
                return false;
            }

        }

        public class ColumnInfo
        {
            public string Name { get; set; }
            public DataTypes DataType { get; set; }
            public string DataTypeDesc { get; set; }
            public ColumnInfo(string name, DataTypes dataType)
            {
                Name = name;
                DataType = dataType;
                DataTypeDesc = dataType.ToString();


            }
            public void setDataType(DataTypes dataType)
            {
                DataType = dataType;
            }

        }
        private class Structure
        {
            public string TableName { get; set; }
            public Type Type { get; set; }
            public List<ColumnInfo> ColumnInfos { get; set; }
        }

        private bool CheckCounts(Type tableType, ref string message, ref string errmessage)
        {
            try
            {

                const string connstr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AKT.DVDCentral.DB;Integrated Security=True";
                SqlConnection connection;
                SqlCommand command;

                connection = new SqlConnection();
                connection.ConnectionString = connstr;
                connection.Open();

                string ssql = "SELECT COUNT(*) FROM " + tableType.Name;
                command = new SqlCommand(ssql, connection);

                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                int rows = reader.GetInt32(0);
                reader.Close();

                if (rows > 0)
                {
                    message += "Passed: " + tableType.Name + " - Rows: " + rows + "\r\n";
                    return true;
                }
                else
                {
                    errmessage += "Failed: Missing Rows: "
                                   + tableType.Name
                                   + "\r\n";
                    message += "Failed: Missing Rows: "
                                   + tableType.Name
                                   + "\r\n";
                    return false;
                }


            }
            catch (System.Exception ex)
            {
                errmessage += "Failed: Get Count for " + tableType.Name + "\r\n";
                message += "Failed: Get Count for " + tableType.Name + "\r\n";
                return false;
            }
        }

        [TestMethod]
        public void CheckTableStructure()
        {
            string message = "Results:\r\n";
            string errMessage = "Errors:\r\n";
            bool results = true;
            bool countresults = true;
            List<Structure> structures = new List<Structure>();

            structures = ReadStructures();
            //if (structures.Count != 10)
            structures = CreateStructures();


            foreach (Structure structure1 in structures)
            {
                foreach (ColumnInfo column in structure1.ColumnInfos)
                {
                    results = CheckColumnDefinition(structure1.Type,
                                                    column.Name,
                                                    column.DataType,
                                                    ref message,
                                                    ref errMessage) ? results : false;
                }
                
                countresults = CheckCounts(structure1.Type, ref message, ref errMessage);
                message += "---------------------------------\r\n";
            }


            Debug.WriteLine(message);
            if (results && countresults)
            {
                Console.WriteLine("Congratulations!!  All Tests Passed.");
                //Assert.IsTrue(false, "Congratulations!!  All Tests Passed.");
            }

            Assert.IsTrue(results && countresults, errMessage);

        }

        private static List<Structure> ReadStructures()
        {
            List<Structure> structures = new List<Structure>();
            try
            {
                StreamReader sr = new StreamReader("structure.json");
                String data = sr.ReadToEnd();
                structures = JsonConvert.DeserializeObject<List<Structure>>(data);
                Debug.WriteLine(structures.Count + " structures found.");
                sr.Close();
            }
            catch (Exception)
            {
                Debug.WriteLine("structure.json not found.");
            }

            return structures;
        }

        private static List<Structure> CreateStructures()
        {
            List<Structure> structures = new List<Structure>();
            Structure structure = new Structure();

            structure.Type = typeof(tblMovie);
            structure.TableName = "tblMovie";
            structure.ColumnInfos = new List<ColumnInfo>();
            structure.ColumnInfos.Add(new ColumnInfo("ID", DataTypes.Int32));
            structure.ColumnInfos.Add(new ColumnInfo("Title", DataTypes.String));
            structure.ColumnInfos.Add(new ColumnInfo("Description", DataTypes.String));
            structure.ColumnInfos.Add(new ColumnInfo("Cost", DataTypes.Decimal));
            structure.ColumnInfos.Add(new ColumnInfo("RatingID", DataTypes.Int32));
            structure.ColumnInfos.Add(new ColumnInfo("DirectorID", DataTypes.Int32));
            structure.ColumnInfos.Add(new ColumnInfo("InStkQty", DataTypes.Int32));
            structure.ColumnInfos.Add(new ColumnInfo("ImagePath", DataTypes.String));
            structures.Add(structure);

            structure = new Structure();
            structure.Type = typeof(tblCustomer);
            structure.TableName = "tblCustomer";
            structure.ColumnInfos = new List<ColumnInfo>();
            structure.ColumnInfos.Add(new ColumnInfo("ID", DataTypes.Int32));
            structure.ColumnInfos.Add(new ColumnInfo("FirstName", DataTypes.String));
            structure.ColumnInfos.Add(new ColumnInfo("LastName", DataTypes.String));
            structure.ColumnInfos.Add(new ColumnInfo("Address", DataTypes.String));
            structure.ColumnInfos.Add(new ColumnInfo("City", DataTypes.String));
            structure.ColumnInfos.Add(new ColumnInfo("State", DataTypes.String));
            structure.ColumnInfos.Add(new ColumnInfo("Zip", DataTypes.String));
            structure.ColumnInfos.Add(new ColumnInfo("Phone", DataTypes.String));
            structure.ColumnInfos.Add(new ColumnInfo("UserID", DataTypes.Int32));
            structures.Add(structure);

            structure = new Structure();
            structure.Type = typeof(tblUser);
            structure.TableName = "tblUser";
            structure.ColumnInfos = new List<ColumnInfo>();
            structure.ColumnInfos.Add(new ColumnInfo("ID", DataTypes.Int32));
            structure.ColumnInfos.Add(new ColumnInfo("Username", DataTypes.String));
            structure.ColumnInfos.Add(new ColumnInfo("FirstName", DataTypes.String));
            structure.ColumnInfos.Add(new ColumnInfo("LastName", DataTypes.String));
            structure.ColumnInfos.Add(new ColumnInfo("Password", DataTypes.String));
            structures.Add(structure);

            structure = new Structure();
            structure.Type = typeof(tblOrderItem);
            structure.TableName = "tblOrderItem";
            structure.ColumnInfos = new List<ColumnInfo>();
            structure.ColumnInfos.Add(new ColumnInfo("ID", DataTypes.Int32));
            structure.ColumnInfos.Add(new ColumnInfo("OrderID", DataTypes.Int32));
            structure.ColumnInfos.Add(new ColumnInfo("MovieID", DataTypes.Int32));
            structure.ColumnInfos.Add(new ColumnInfo("Quantity", DataTypes.Int32));
            structure.ColumnInfos.Add(new ColumnInfo("Cost", DataTypes.Decimal));
            structures.Add(structure);

            structure = new Structure();
            structure.Type = typeof(tblFormat);
            structure.TableName = "tblFormat";
            structure.ColumnInfos = new List<ColumnInfo>();
            structure.ColumnInfos.Add(new ColumnInfo("ID", DataTypes.Int32));
            structure.ColumnInfos.Add(new ColumnInfo("Description", DataTypes.String));
            structures.Add(structure);

            structure = new Structure();
            structure.Type = typeof(tblRating);
            structure.TableName = "tblRating";
            structure.ColumnInfos = new List<ColumnInfo>();
            structure.ColumnInfos.Add(new ColumnInfo("ID", DataTypes.Int32));
            structure.ColumnInfos.Add(new ColumnInfo("Description", DataTypes.String));
            structures.Add(structure);

            structure = new Structure();
            structure.Type = typeof(tblGenre);
            structure.TableName = "tblGenre";
            structure.ColumnInfos = new List<ColumnInfo>();
            structure.ColumnInfos.Add(new ColumnInfo("ID", DataTypes.Int32));
            structure.ColumnInfos.Add(new ColumnInfo("Description", DataTypes.String));
            structures.Add(structure);

            structure = new Structure();
            structure.Type = typeof(tblDirector);
            structure.TableName = "tblDirector";
            structure.ColumnInfos = new List<ColumnInfo>();
            structure.ColumnInfos.Add(new ColumnInfo("ID", DataTypes.Int32));
            structure.ColumnInfos.Add(new ColumnInfo("FirstName", DataTypes.String));
            structure.ColumnInfos.Add(new ColumnInfo("LastName", DataTypes.String));
            structures.Add(structure);

            structure = new Structure();
            structure.Type = typeof(tblMovieGenre);
            structure.TableName = "tblMovieGenre";
            structure.ColumnInfos = new List<ColumnInfo>();
            structure.ColumnInfos.Add(new ColumnInfo("ID", DataTypes.Int32));
            structure.ColumnInfos.Add(new ColumnInfo("MovieID", DataTypes.Int32));
            structure.ColumnInfos.Add(new ColumnInfo("GenreID", DataTypes.Int32));
            structures.Add(structure);

            structure = new Structure();
            structure.Type = typeof(tblOrder);
            structure.TableName = "tblOrder";
            structure.ColumnInfos = new List<ColumnInfo>();
            structure.ColumnInfos.Add(new ColumnInfo("ID", DataTypes.Int32));
            structure.ColumnInfos.Add(new ColumnInfo("CustomerID", DataTypes.Int32));
            structure.ColumnInfos.Add(new ColumnInfo("UserID", DataTypes.Int32));
            structure.ColumnInfos.Add(new ColumnInfo("OrderDate", DataTypes.DateTime));
            structure.ColumnInfos.Add(new ColumnInfo("ShipDate", DataTypes.DateTime));
            structures.Add(structure);

            string serializedObject = JsonConvert.SerializeObject(structures);
            StreamWriter sw = File.CreateText("structure.json");
            sw.WriteLine(serializedObject);
            sw.Close();
            Debug.WriteLine("Rewrote structure.json.");
            return structures;
        }
    }
}
 