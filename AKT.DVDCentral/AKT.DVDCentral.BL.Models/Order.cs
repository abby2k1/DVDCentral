using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKT.DVDCentral.BL.Models
{
    public class Order
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserID { get; set; }
        public DateTime ShipDate { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
    }
}
