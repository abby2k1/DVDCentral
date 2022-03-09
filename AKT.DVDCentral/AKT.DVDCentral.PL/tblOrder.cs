using System;
using System.Collections.Generic;

namespace AKT.DVDCentral.PL
{
    public partial class tblOrder
    {
        public tblOrder()
        {
            tblOrderItems = new HashSet<tblOrderItem>();
        }

        public int ID { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserID { get; set; }
        public DateTime ShipDate { get; set; }

        public virtual tblCustomer Customer { get; set; } = null!;
        public virtual tblUser User { get; set; } = null!;
        public virtual ICollection<tblOrderItem> tblOrderItems { get; set; }
    }
}
