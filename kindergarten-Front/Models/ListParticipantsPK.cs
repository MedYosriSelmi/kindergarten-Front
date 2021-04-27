using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace kindergarten_Front.Models
{
    public class ListParticipantsPK
    {
        [JsonProperty]
        [Key, Column(Order = 1)]
        [ForeignKey("User")]
        public int idUser { get; set; }


        [JsonProperty]
        [Key, Column(Order = 2)]
        [ForeignKey("Event")]
        public int idEvent { get; set; }

        [JsonProperty]
        [Key, Column(Order = 0)]
        [DataType(DataType.Date)]
       [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]  
        public DateTime DateParticipation = DateTime.Now;
        //public DateTime DateParticipation = DateTime.Now;
        [JsonProperty]
        public string etat { get; set; }

        public virtual Event Event { get; set; }
        public virtual User User { get; set; }
    }


}