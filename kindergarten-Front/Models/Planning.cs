using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kindergarten_Front.Models
{
    public class Planning
    {
        [Key]
        public int id { get; set; }
        [DataType(DataType.Date)]
        // [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]  
        public DateTime time { get; set; }
        
        public string destination { get; set; }
        public string departure { get; set; }

        public Kindergarten kindergarten { get; set; }
        public User user { get; set; }
    }
}