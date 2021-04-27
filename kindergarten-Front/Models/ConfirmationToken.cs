using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace kindergarten_Front.Models
{
    public class ConfirmationToken
    {
        [Key]
        public int id { get; set; }
        public String token { get; set; }
        [DataType(DataType.DateTime)]
        // [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]  
        public DateTime createdAt { get; set; }
        [DataType(DataType.DateTime)]
        // [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]  
        public DateTime expiresAt { get; set; }
        [DataType(DataType.DateTime)]
        // [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]  
        public DateTime confirmedAt { get; set; }
      
        public virtual int user_id { get; set; }
        //public int id { get; set; }

        //public string Token { get; set; }

        //public DateTime CreatedAt { get; set; }

        //public DateTime ExpiresAt { get; set; }

        //public DateTime ConfirmedAt { get; set; }


        //public virtual User User { get; set; }

        public ConfirmationToken()
        {
        }

    }
}