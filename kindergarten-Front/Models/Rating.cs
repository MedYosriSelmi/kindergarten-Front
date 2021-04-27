using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kindergarten_Front.Models
{
    public class Rating

    {
        [Key]
        public int ratId { get; set; }
        public float ratingValue { get; set; }

        public virtual User user { get; set; }
        public virtual Event  Event{ get; set; }
    }
}