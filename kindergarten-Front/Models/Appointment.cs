using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kindergarten_Front.Models
{
    public class Appointment
    {
        [Key]
        public int id { get; set; }
        [DataType(DataType.Date)]
        // [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]  
        public DateTime date { get; set; }
        public string description { get; set; }
        public int status { get; set; }
        public string beginhour { get; set; }
        public string endhour { get; set; }

        public User user { get; set; }

        public User doctor { get; set; }
    }
}