using System;
using System.Collections.Generic;

namespace AKT.DVDCentral.PL
{
    public partial class tblDirector
    {
        public tblDirector()
        {
            tblMovies = new HashSet<tblMovie>();
        }

        public int ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public virtual ICollection<tblMovie> tblMovies { get; set; }
    }
}
