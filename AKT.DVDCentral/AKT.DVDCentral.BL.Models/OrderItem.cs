using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKT.DVDCentral.BL.Models
{
    internal class OrderItem
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int MovieID { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
    }
}
