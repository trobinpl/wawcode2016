using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace wawcode2016.Models
{
    public class WawcodeDbContext : DbContext
    {
        public WawcodeDbContext()
        {

        }

        public WawcodeDbContext(string connString)
        {
            this.Database.Connection.ConnectionString = connString;
        }

        public IDbSet<Flat> Flats { get; set; }
    }
}