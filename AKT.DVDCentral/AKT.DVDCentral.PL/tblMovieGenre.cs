using System;
using System.Collections.Generic;

namespace AKT.DVDCentral.PL
{
    public partial class tblMovieGenre
    {
        public int ID { get; set; }
        public int MovieID { get; set; }
        public int GenreID { get; set; }

        public virtual tblGenre Genre { get; set; } = null!;
        public virtual tblMovie Movie { get; set; } = null!;
    }
}
