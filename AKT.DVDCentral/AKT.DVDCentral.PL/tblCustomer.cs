using System;
using System.Collections.Generic;

namespace AKT.DVDCentral.PL
{
    public partial class tblCustomer
    {
        public tblCustomer()
        {
            tblOrders = new HashSet<tblOrder>();
        }

        public int ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
        public string? Phone { get; set; }
        public int UserID { get; set; }

        public virtual tblUser User { get; set; } = null!;
        public virtual ICollection<tblOrder> tblOrders { get; set; }
    }
}
