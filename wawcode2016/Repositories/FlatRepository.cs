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
            return this.context.Flats.Where(flat => flat.Id == id).FirstOrDefault();
        }

        public IEnumerable<Flat> LoadAll()
        {
            return this.context.Flats.ToList();
        }

        public void Save(Flat flat)
        {
            this.context.Flats.Add(flat);
            this.context.SaveChanges();
        }
    }
}