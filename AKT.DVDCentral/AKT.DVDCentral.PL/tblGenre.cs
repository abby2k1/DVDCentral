using System;
using System.Collections.Generic;

namespace AKT.DVDCentral.PL
{
    public partial class tblGenre
    {
        public tblGenre()
        {
            tblMovieGenres = new HashSet<tblMovieGenre>();
        }

        public int ID { get; set; }
        public string Description { get; set; } = null!;

        public virtual ICollection<tblMovieGenre> tblMovieGenres { get; set; }
    }
}
