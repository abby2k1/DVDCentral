using AKT.DVDCentral.BL.Models;
using AKT.DVDCentral.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace AKT.DVDCentral.BL
{
    public static class OrderItemManager
    {
        public static int Insert(OrderItem orderItem, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblOrderItem row = new tblOrderItem();

                    row.ID = dc.tblOrderItems.Any() ? dc.tblOrderItems.Max(dt => dt.ID) + 1 : 1;
                    row.OrderID = orderItem.OrderID;
                    row.MovieID = orderItem.MovieID;
                    row.Quantity = orderItem.Quantity;
                    row.Cost = orderItem.Cost;

                    dc.tblOrderItems.Add(row);

                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                    orderItem.ID = row.ID;

                    return results;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int Update(OrderItem orderItem, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblOrderItem row = dc.tblOrderItems.Where(dt => dt.ID == orderItem.ID).FirstOrDefault();

                    row.OrderID = orderItem.OrderID;
                    row.MovieID = orderItem.MovieID;
                    row.Quantity = orderItem.Quantity;
                    row.Cost = orderItem.Cost;

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

                    tblOrderItem row = dc.tblOrderItems.Where(dt => dt.ID == id).FirstOrDefault();

                    dc.tblOrderItems.Remove(row);

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

        public static List<OrderItem> Load()
        {
            try
            {
                List<OrderItem> rows = new List<OrderItem>();

                DVDCentralEntities dc = new DVDCentralEntities();

                dc.tblOrderItems
                    .ToList()
                    .ForEach(dt => rows.Add(new OrderItem
                    {
                        ID = dt.ID,
                        OrderID = dt.OrderID,
                        MovieID = dt.MovieID,
                        Quantity = dt.Quantity,
                        Cost = dt.Cost
                    }));
                return rows;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static OrderItem LoadByID(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblOrderItem row = dc.tblOrderItems.FirstOrDefault(dt => dt.ID == id);

                    if (row != null)
                    {
                        return new OrderItem { 
                            ID = row.ID,
                            OrderID = row.OrderID,
                            MovieID = row.MovieID,
                            Quantity = row.Quantity,
                            Cost = row.Cost
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
