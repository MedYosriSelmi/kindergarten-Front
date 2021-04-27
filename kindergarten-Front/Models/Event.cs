using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace kindergarten_Front.Models
{
    public class Event
    {
        [Key]
        public int id { get; set; }
        [JsonProperty]
        public string name { get; set; }
        [JsonProperty]
        public string description { get; set; }
        [JsonProperty]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]  
        public DateTime? DateOfEvent { get; set; }
        
        public string photo { get; set; }

        public User user { get; set; }

       
        public virtual Kindergarten kindergarten { get; set; }
    }
}