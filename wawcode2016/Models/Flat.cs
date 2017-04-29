using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wawcode2016.Models
{
    public class Flat
    {
        public Flat()
        {
            this.Defects = new List<Defect>();
        }

        [Key]
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public virtual List<Defect> Defects { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}