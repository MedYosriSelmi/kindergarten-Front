using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kindergarten_Front.Models
{
    public class Comment
    {
        [Key]
        public int id { get; set; }
        public string description { get; set; }
        [DataType(DataType.Date)]
        // [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]  
        public DateTime DateLikeSub { get; set; }

        public Subject subject { get; set; }
        public User user { get; set; }

        public Comment comment { get; set; }

    }
}