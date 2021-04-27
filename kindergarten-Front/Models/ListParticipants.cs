using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kindergarten_Front.Models
{
    public class ListParticipants 
    {

       
        public string etat { get; set; }
        public virtual ListParticipantsPK listParticipantsPK { get; set; }
    }
}