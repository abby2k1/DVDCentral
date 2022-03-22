using AKT.DVDCentral.BL.Models;
using AKT.DVDCentral.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace AKT.DVDCentral.BL
{
    public static class CustomerManager
    {
        public static int Insert(Customer customer, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblCustomer row = new tblCustomer();

                    row.ID = dc.tblCustomers.Any() ? dc.tblCustomers.Max(dt => dt.ID) + 1 : 1;
                    row.FirstName = customer.FirstName;
                    row.LastName = customer.LastName;
                    row.Address = customer.Address;
                    row.City = customer.City;
                    row.State = customer.State;
                    row.Zip = customer.Zip;
                    row.Phone = customer.Phone;
                    row.UserID = customer.UserID;

                    dc.tblCustomers.Add(row);

                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                    customer.ID = row.ID;

                    return results;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int Update(Customer customer, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblCustomer row = dc.tblCustomers.Where(dt => dt.ID == customer.ID).FirstOrDefault();

                    row.FirstName = customer.FirstName;
                    row.LastName = customer.LastName;
                    row.Address = customer.Address;
                    row.City = customer.City;
                    row.State = customer.State;
                    row.Zip = customer.Zip;
                    row.Phone = customer.Phone;
                    row.UserID = customer.UserID;

                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                    return results;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int Delete(int id, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblCustomer row = dc.tblCustomers.Where(dt => dt.ID == id).FirstOrDefault();

                    tblOrder orderRow = dc.tblOrders.Where(dt => dt.CustomerID == id).FirstOrDefault();
                    while (orderRow != null)
                    {
                        tblOrderItem orderItemRow = dc.tblOrderItems.Where(dt => dt.OrderID == orderRow.ID).FirstOrDefault();
                        while (orderItemRow != null)
                        {
                            dc.tblOrderItems.Remove(orderItemRow);
                            dc.SaveChanges();
                            orderItemRow = dc.tblOrderItems.Where(dt => dt.OrderID == orderRow.ID).FirstOrDefault();
                        }

                        dc.tblOrders.Remove(orderRow);
                        dc.SaveChanges();
                        orderRow = dc.tblOrders.Where(dt => dt.CustomerID == id).FirstOrDefault();
                    }

                    dc.tblCustomers.Remove(row);

                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                    return results;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<Customer> Load()
        {
            try
            {
                List<Customer> rows = new List<Customer>();

                DVDCentralEntities dc = new DVDCentralEntities();

                dc.tblCustomers
                    .ToList()
                    .ForEach(dt => rows.Add(new Customer
                    {
                        ID = dt.ID,
                        FirstName = dt.FirstName,
                        LastName = dt.LastName,
                        Address = dt.Address,
                        City = dt.City,
                        State = dt.State,
                        Zip = dt.Zip,
                        Phone = dt.Phone,
                        UserID = dt.UserID
                    }));
                return rows;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static Customer LoadByID(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblCustomer row = dc.tblCustomers.FirstOrDefault(dt => dt.ID == id);

                    if (row != null)
                    {
                        return new Customer
                        {
                            ID = row.ID,
                            FirstName = row.FirstName,
                            LastName = row.LastName,
                            Address = row.Address,
                            City = row.City,
                            State = row.State,
                            Zip = row.Zip,
                            Phone = row.Phone,
                            UserID = row.UserID
                        };
                    }
                    else
                    {
                        throw new Exception("Row was not found.");
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
