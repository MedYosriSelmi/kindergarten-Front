using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kindergarten_Front.Models
{
    public class Role
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
    }
}