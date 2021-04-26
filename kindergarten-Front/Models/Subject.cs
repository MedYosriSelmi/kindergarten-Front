using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kiindergarten.Models
{
    public class Subject
    {
        [Key]
        public int id { get; set; }
        [DataType(DataType.Date)]
        // [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]  
        public DateTime creationDate { get; set; }
        [DataType(DataType.Date)]
        // [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]  
        public DateTime lastUpdateDate { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public User user { get; set; }
        public Kindergarten kindergarten { get; set; }

        public ICollection<LikesSub> likes_subs { get; set; }
        public ICollection<Comment> Comments{ get; set; }

    }
}