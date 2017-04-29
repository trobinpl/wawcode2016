using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wawcode2016.Models;

namespace wawcode2016.Repositories
{
    public class DefectRepository
    {
        private WawcodeDbContext context;

        public DefectRepository()
        {
            this.context = new WawcodeDbContext();
        }

        public Guid Save(Defect defect)
        {
            defect.Id = Guid.NewGuid();
            this.context.Defects.Add(defect);
            this.context.SaveChanges();

            return defect.Id;
        }
    }
}