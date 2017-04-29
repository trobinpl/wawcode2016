using RestSharp;
using RestSharp.Authenticators;
using SMSApi.Api;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;
using wawcode2016.Models;
using wawcode2016.Repositories;
using wawcode2016.ViewModels;

namespace wawcode2016.Controllers
{
    public class DefectController : ApiController
    {
        private DefectRepository defectRepository;
        private FlatRepository flatRepository;

        public DefectController()
        {
            this.defectRepository = new DefectRepository();
            this.flatRepository = new FlatRepository();
        }

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
            if (defectViewModel.FlatId == null)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            Defect defect = new Defect()
            {
                FlatId = defectViewModel.FlatId,
                Description = defectViewModel.Description,
                ImageUrl = defectViewModel.ImageUrl
            };

            Flat flat = this.flatRepository.Load(defect.FlatId);

            this.defectRepository.Save(defect);

            //Client smsClient = new Client("pawel+smsapi@hemperek.pl");
            //smsClient.SetPasswordHash("909c655e3677bbba306c19edbdfd80a1");

            //var smsApi = new SMSFactory(smsClient);

            //var result = smsApi.ActionSend()
            //    .SetText($"Mieszkanie: Ząbkowicka 33C. Zgłoszono nową usterkę. Opis: { defect.Description }")
            //    .SetTo("664660800")
            //    .SetSender("Alert")
            //    .Execute();

            var restClient = new RestClient("https://api.mailgun.net/v3");
            restClient.Authenticator = new HttpBasicAuthenticator("api", "key-ad33d50d4b3f693639b663f64c81df4d");
            var request = new RestRequest(Method.POST);
            request.AddParameter("domain", "sandbox8eb74b7940404e57b90996353993faca.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";

            request.AddParameter("from", "Flateo <alert@flat.eo>");
            request.AddParameter("to", "pawel+flateo@hemperek.pl");
            request.AddParameter("subject", $"Nowa usterka - {flat.Address}");
            string template = "<html><body><div style=\"margin: auto;\"><img src=\"Flateo2.png\"></div><h1 style=\"font-family:'Open Sans'\">Zgłoszono nową usterkę</h1><p style=\"font-family:'Open Sans'\">{0}</p></body></html>";
            request.AddParameter("html", string.Format(template, defect.Description));
            var path = HostingEnvironment.MapPath("~/Content/Images/Flateo2.png");
            request.AddFile("attachment", path);
            request.AddHeader("Content-Type", "multipart/form-data");

            var emailResult = restClient.Execute(request);

            //Debug.WriteLine(result);
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