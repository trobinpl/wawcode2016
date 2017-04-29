using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wawcode2016.ViewModels
{
    public class FlatViewModel
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public bool HasDefects { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public List<DefectInFlatViewModel> Defects { get; set; }
    }
}