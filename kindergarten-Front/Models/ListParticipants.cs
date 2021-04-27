using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace kindergarten_Front.Models
{
    public class ListParticipants
    {
        [Key, Column(Order = 1)]
        [ForeignKey("User")]
        public int idUser { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Event")]
        public int idEvent { get; set; }
        [DataType(DataType.Date)]
        // [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]  
        public DateTime DateParticipation { get; set; }
        public string etat { get; set; }

    }


}