using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kiindergarten.Models
{
    public class Child
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string photo { get; set; }
        [DataType(DataType.Date)]
        // [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]  
        public DateTime dateOfBirth { get; set; }

        public User user { get; set; }
        public Kindergarten kindergarten { get; set; }
    }
}