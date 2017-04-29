using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wawcode2016.Models
{
    public class Defect
    {
        [Key]
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public Guid FlatId { get; set; }

        [ForeignKey("FlatId")]
        public virtual Flat Flat { get; set; }
    }
}