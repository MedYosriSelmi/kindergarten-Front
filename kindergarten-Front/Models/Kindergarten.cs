using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kiindergarten.Models
{
    public class Kindergarten
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public string description { get; set; }
        public string phone { get; set; }
        public double pricePerChild { get; set; }
        public string photo { get; set; }
        public string email { get; set; }

        public ICollection<User> users { get; set; }
        public ICollection<Event> list_events { get; set; }
        public ICollection<Child> list_child { get; set; }
        public ICollection<Bill> list_fact { get; set; }
        public ICollection<Activity> list_act { get; set; }
        public ICollection<Reclamation> list_reclams { get; set; }
        public ICollection<Publication> list_pub { get; set; }
    }
}