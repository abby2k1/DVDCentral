using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKT.DVDCentral.BL.Models
{
    internal class Movie
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public int RatingID { get; set; }
        public int FormatID { get; set; }
        public int DirectorID { get; set; }
        public int InStockQty { get; set; }
        public string ImagePath { get; set; }
    }
}
