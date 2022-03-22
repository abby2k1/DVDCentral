using AKT.DVDCentral.BL.Models;
using AKT.DVDCentral.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace AKT.DVDCentral.BL
{
    public static class OrderManager
    {
        public static int Insert(Order order, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblOrder row = new tblOrder();

                    row.ID = dc.tblOrders.Any() ? dc.tblOrders.Max(dt => dt.ID) + 1 : 1;
                    row.CustomerID = order.CustomerID;
                    row.UserID = order.UserID;
                    row.OrderDate = order.OrderDate;
                    row.ShipDate = order.ShipDate;
                    
                    dc.tblOrders.Add(row);

                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                    order.ID = row.ID;

                    return results;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int Update(Order order, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblOrder row = dc.tblOrders.Where(dt => dt.ID == order.ID).FirstOrDefault();

                    row.CustomerID = order.CustomerID;
                    row.UserID = order.UserID;
                    row.OrderDate = order.OrderDate;
                    row.ShipDate = order.ShipDate;

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

                    tblOrder row = dc.tblOrders.Where(dt => dt.ID == id).FirstOrDefault();

                    tblOrderItem orderItemRow = dc.tblOrderItems.Where(dt => dt.OrderID == id).FirstOrDefault();
                    while (orderItemRow != null)
                    {
                        dc.tblOrderItems.Remove(orderItemRow);
                        dc.SaveChanges();
                        orderItemRow = dc.tblOrderItems.Where(dt => dt.OrderID == id).FirstOrDefault();
                    }

                    dc.tblOrders.Remove(row);

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

        public static List<Order> Load()
        {
            try
            {
                List<Order> rows = new List<Order>();

                DVDCentralEntities dc = new DVDCentralEntities();

                dc.tblOrders
                    .ToList()
                    .ForEach(dt => rows.Add(new Order
                    {
                        ID = dt.ID,
                        CustomerID = dt.CustomerID,
                        UserID = dt.UserID,
                        OrderDate = dt.OrderDate,
                        ShipDate = dt.ShipDate
            }));
                return rows;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static Order LoadByID(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblOrder row = dc.tblOrders.FirstOrDefault(dt => dt.ID == id);

                    if (row != null)
                    {
                        return new Order {
                            ID = row.ID,
                            CustomerID = row.CustomerID,
                            UserID = row.UserID,
                            OrderDate = row.OrderDate,
                            ShipDate = row.ShipDate
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
