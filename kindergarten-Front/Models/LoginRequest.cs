using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kindergarten_Front.Models
{
    public class LoginRequest
    {
        public string username { get; set; }
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}