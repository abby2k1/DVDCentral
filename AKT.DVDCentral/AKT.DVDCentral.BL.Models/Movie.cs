using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKT.DVDCentral.BL.Models
{
    public class Movie
    {
        public int ID { get; set; }
        [DisplayName("Title")]
        public string Title { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("Cost")]
        public decimal Cost { get; set; }
        public int RatingID { get; set; }
        public int FormatID { get; set; }
        public int DirectorID { get; set; }
        [DisplayName("Quantity")]
        public int InStkQty { get; set; }
        [DisplayName("Image")]
        public string ImagePath { get; set; }
        [DisplayName("Rating")]
        public string RatingDesc { get; set; }
        [DisplayName("Format")]
        public string FormatDesc { get; set; }
        [DisplayName("Director")]
        public string DirectorName { get; set; }
    }
}
