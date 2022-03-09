using System;
using System.Collections.Generic;

namespace AKT.DVDCentral.PL
{
    public partial class tblOrderItem
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int MovieID { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }

        public virtual tblMovie Movie { get; set; } = null!;
        public virtual tblOrder Order { get; set; } = null!;
    }
}
