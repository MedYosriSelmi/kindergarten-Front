using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace kindergarten_Front.Models
{
    public class LikesPub
    {
        [Key, Column(Order = 1)]
        [ForeignKey("User")]
        public int idUser { get; set; }
        [Key, Column(Order = 2)]
        [ForeignKey("Publication")]
        public int idPublication { get; set; }
        [DataType(DataType.Date)]
        // [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]  
        public DateTime DateLikesPub { get; set; }
       
    }
}