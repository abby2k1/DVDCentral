using AKT.DVDCentral.BL.Models;
using AKT.DVDCentral.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace AKT.DVDCentral.BL
{
    public static class OrderManager
    {
        public static int Insert(Order order, bool rollback = false )
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
                    order.ID = row.ID;

                    dc.tblOrders.Add(row);

                    if (order.OrderItems != null)
                    {
                        foreach (OrderItem oi in order.OrderItems)
                        {
                            oi.OrderID = row.ID;
                            tblOrderItem item = new tblOrderItem();
                            item.ID = dc.tblOrderItems.Any() ? dc.tblOrderItems.Max(dt => dt.ID) + 1 : 1;
                            item.OrderID = oi.OrderID;
                            item.MovieID = oi.MovieID;
                            item.Cost = oi.Cost;
                            item.Quantity = oi.Quantity;
                            oi.ID = item.ID;
                            dc.tblOrderItems.Add(item);
                        }
                    }
                    
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

        public static List<Order> LoadByCustomerID(int id)
        {
            return Load(id);
        }

        public static List<Order> Load(int? customerID = null)
        {
            try
            {
                List<Order> rows = new List<Order>();

                DVDCentralEntities dc = new DVDCentralEntities();

                var orders = (from o in dc.tblOrders
                              where o.CustomerID == customerID || customerID == null
                              orderby o.ID
                              select new
                              {
                                  ID = o.ID,
                                  CustomerID = o.CustomerID,
                                  UserID = o.UserID,
                                  OrderDate = o.OrderDate,
                                  ShipDate = o.ShipDate
                              }).ToList();

                orders.ForEach(o => rows.Add(new Models.Order
                {
                    ID = o.ID,
                    CustomerID = o.CustomerID,
                    UserID = o.UserID,
                    OrderDate = o.OrderDate,
                    ShipDate = o.ShipDate
                }));

                return rows;
            }
            catch (Exception ex)
            {
                throw ex;
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
