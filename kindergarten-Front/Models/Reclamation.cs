using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kindergarten_Front.Models
{
    public class Reclamation
    {
        [Key]
        public int id { get; set; }
        [DataType(DataType.Date)]
        // [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]  
        public DateTime DateOfReclam { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public Status status { get; set; }
        public string photo { get; set; }
        public User user { get; set; }
        public Kindergarten kindergarten { get; set; }
    }
}