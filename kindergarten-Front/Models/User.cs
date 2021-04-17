using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kiindergarten.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public bool active { get; set; }
        public string password { get; set; }
       
        public ICollection<Role> roles { get; set; }

        public ICollection<ListParticipants> list_participants { get; set; }
        public ICollection<LikesSub> likes_subs { get; set; }
        public ICollection<LikesPub> likes_pubs { get; set; }
        public ICollection<Subject> list_subject { get; set; }
        public ICollection<Reclamation> list_reclams { get; set; }
        public ICollection<Publication> list_pub { get; set; }
        public ICollection<Kindergarten> kindergarten { get; set; }
        public ICollection<Event> list_events { get; set; }
        public ICollection<Comment> comments { get; set; }
        public ICollection<Child> list_child { get; set; }
        public ICollection<Bill> list_fact { get; set; }
        public ICollection<Appointment> list_appoint { get; set; }
        public ICollection<Activity> list_act { get; set; }
        public ICollection<Planning> list_plan { get; set; }


        public string firstName { get; set; }
        public string lastName { get; set; }
        public string photo { get; set; }
        [DataType(DataType.Date)]
        // [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]  
        public DateTime dateOfBirth { get; set; }
    }
}