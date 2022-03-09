using System;
using System.Collections.Generic;

namespace AKT.DVDCentral.PL
{
    public partial class tblUser
    {
        public tblUser()
        {
            tblCustomers = new HashSet<tblCustomer>();
            tblOrders = new HashSet<tblOrder>();
        }

        public int ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }

        public virtual ICollection<tblCustomer> tblCustomers { get; set; }
        public virtual ICollection<tblOrder> tblOrders { get; set; }
    }
}
