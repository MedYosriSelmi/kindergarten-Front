using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kiindergarten.Models
{
    public class Publication
    {
        [Key]
        public int id { get; set; }
        public List<LikesPub> likes_pub { get; set; }
        public User user { get; set; }
        public Kindergarten kindergarten { get; set; }
        public string description { get; set; }
        public string photo { get; set; }

        public Kindergarten Kindergarten { get; set; }
        public User User { get; set; }
    }
}