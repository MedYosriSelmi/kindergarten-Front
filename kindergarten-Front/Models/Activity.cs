using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace kindergarten_Front.Models
{
    public class Activity
    {
        [Key]
        public int id { get; set; }
      
        public string name { get; set; }
        public string description { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
      
     
        public DateTime? DateOfActivity { get; set; }
       
        public string photo { get; set; }

        public User user { get; set; }
        public Kindergarten kindergarten { get; set; }
         public Category category { get; set; }
    }
}