using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kiindergarten.Models
{
    public class Role
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
    }
}