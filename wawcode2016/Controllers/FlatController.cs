using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using wawcode2016.Models;
using wawcode2016.Repositories;
using wawcode2016.ViewModels;

namespace wawcode2016.Controllers
{
    public class FlatController : ApiController
    {
        //private FlatRepository flatRepository;

        //public FlatController()
        //{
        //    this.flatRepository = new FlatRepository();
        //}

        // GET api/<controller>
        public IEnumerable<FlatViewModel> Get()
        {
            List<FlatViewModel> flats = new List<FlatViewModel>()
            {
                new FlatViewModel()
                {
                    Id = Guid.NewGuid(),
                    Address = "Ząbkowska 33C",
                    Name = "Moje ekstra mieszkanie"
                }
            };

            return flats;
        }

        // GET api/<controller>/5
        public FlatViewModel Get(Guid id)
        {
            FlatRepository flatRepository = new FlatRepository();
            Flat flat = flatRepository.Load(id);

            if(flat == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return new FlatViewModel()
            {
                Id = flat.Id,
                Name = flat.Address,
                Address = flat.Address
            };
        }

        // POST api/<controller>
        public void Post([FromBody]FlatViewModel flatViewModel)
        {
            FlatRepository flatRepository = new FlatRepository();

            Flat flat = new Flat()
            {
                Id = flatViewModel.Id,
                Name = flatViewModel.Name,
                Address = flatViewModel.Address
            };

            flatRepository.Save(flat);
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