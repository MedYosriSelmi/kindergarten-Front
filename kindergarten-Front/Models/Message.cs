using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kiindergarten.Models
{
    public class Message
    {
        [Key]
        public int id { get; set; }
        public string description { get; set; }
        [DataType(DataType.Date)]
        // [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]  
        public DateTime DateDelivered { get; set; }

        public User Sender { get; set; }
        public User Reciever { get; set; }

    }
}