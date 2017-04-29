using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using wawcode2016.Models;
using wawcode2016.ViewModels;

namespace wawcode2016.Controllers
{
    public class DefectController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]DefectViewModel defectViewModel)
        {
            Defect defect = new Defect()
            {
                Description = defectViewModel.Description,
                ImageUrl = defectViewModel.ImageUrl
            };


        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}