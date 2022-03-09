using System;
using System.Collections.Generic;

namespace AKT.DVDCentral.PL
{
    public partial class tblRating
    {
        public tblRating()
        {
            tblMovies = new HashSet<tblMovie>();
        }

        public int ID { get; set; }
        public string Description { get; set; } = null!;

        public virtual ICollection<tblMovie> tblMovies { get; set; }
    }
}
