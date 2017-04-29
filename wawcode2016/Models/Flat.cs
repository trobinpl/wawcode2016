using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wawcode2016.Models
{
    public class Flat
    {
        [Key]
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}