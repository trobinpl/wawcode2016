using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wawcode2016.Models;

namespace wawcode2016.Repositories
{
    public class FlatRepository
    {
        private WawcodeDbContext context;

        public FlatRepository()
        {
            this.context = new WawcodeDbContext();
        }

        public Flat Load(Guid id)
        {
            return new Flat()
            {
                Id = Guid.NewGuid(),
                Address = "Ząbkowska 33C",
                Name = "Moje fajne mieszkanko"
            };

            //return this.context.Flats.Where(flat => flat.Id == id).FirstOrDefault();
        }

        public void Save(Flat flat)
        {
            //this.context.Flats.Add(flat);
            //this.context.SaveChanges();
        }
    }
}