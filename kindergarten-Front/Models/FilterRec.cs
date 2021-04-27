using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kindergarten_Front.Models
{
    public class FilterReclamation
    {
        public Status status { get; set; }
        [DataType(DataType.Date)]
        public DateTime date1 { get; set; }
        [DataType(DataType.Date)]
        public DateTime date2 { get; set; }
    }
}