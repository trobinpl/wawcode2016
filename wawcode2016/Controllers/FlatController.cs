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
        private FlatRepository flatRepository;

        public FlatController()
        {
            this.flatRepository = new FlatRepository();
        }

        // GET api/<controller>
        public IEnumerable<FlatViewModel> Get()
        {

            IEnumerable<Flat> flats = this.flatRepository.LoadAll();

            IEnumerable<FlatViewModel> flatsViewModels = flats.Select(flat => new FlatViewModel()
            {
                Id = flat.Id,
                Address = flat.Address,
                Name = flat.Name,
                HasDefects = flat.Defects.Any()
            }).ToList();

            return flatsViewModels;
        }

        // GET api/<controller>/5
        public FlatViewModel Get(Guid id)
        {
            Flat flat = this.flatRepository.Load(id);

            if(flat == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return new FlatViewModel()
            {
                Id = flat.Id,
                Name = flat.Name,
                Address = flat.Address,
                HasDefects = flat.Defects.Any()
            };
        }

        // POST api/<controller>
        public void Post([FromBody]FlatViewModel flatViewModel)
        {
            Flat flat = new Flat()
            {
                Id = flatViewModel.Id,
                Name = flatViewModel.Name,
                Address = flatViewModel.Address
            };

            this.flatRepository.Save(flat);
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