using System;
using System.Collections.Generic;

namespace AKT.DVDCentral.PL
{
    public partial class tblMovie
    {
        public tblMovie()
        {
            tblMovieGenres = new HashSet<tblMovieGenre>();
            tblOrderItems = new HashSet<tblOrderItem>();
        }

        public int ID { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Cost { get; set; }
        public int RatingID { get; set; }
        public int FormatID { get; set; }
        public int DirectorID { get; set; }
        public int InStkQty { get; set; }
        public string? ImagePath { get; set; }

        public virtual tblDirector Director { get; set; } = null!;
        public virtual tblFormat Format { get; set; } = null!;
        public virtual tblRating Rating { get; set; } = null!;
        public virtual ICollection<tblMovieGenre> tblMovieGenres { get; set; }
        public virtual ICollection<tblOrderItem> tblOrderItems { get; set; }
    }
}
