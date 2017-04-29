using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
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
using System.Web;
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
            //DefectViewModel defectViewModel = new DefectViewModel();

            ////defectViewModel.Description = httpRequest.Form["description"];
            defectViewModel.FlatId = Guid.Parse("8981b67b-b630-4e25-be56-7d2a98e8ffcd");
            string path = "https://flateo.blob.core.windows.net/defectimages/{0}.png";
            string imageId = defectViewModel.ImageId.ToString();
            var imagePath = string.Format(path, /*defectViewModel.ImageId*/imageId);

            if (defectViewModel.FlatId == null)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            Defect defect = new Defect()
            {
                FlatId = defectViewModel.FlatId,
                Description = defectViewModel.Description,
                ImageUrl = imagePath
            };

            Flat flat = this.flatRepository.Load(defect.FlatId);

            Guid defectId = this.defectRepository.Save(defect);

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("defectimages");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(defectViewModel.ImageId.ToString() + ".png");

            string defectImagePath = HostingEnvironment.MapPath(string.Format("~/App_Data/TempImages/{0}.png", defectViewModel.ImageId));

            using (var fileStream = System.IO.File.OpenWrite(defectImagePath))
            {
                blockBlob.DownloadToStream(fileStream);
            }

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
            request.AddParameter("subject", $"Nowa usterka - {flat.Address} ({flat.Name})");
            string template = "<html><body><div style=\"margin: auto;\"><img src=\"Flateo2.png\"></div><h1 style=\"font-family:'Open Sans'\">Zgłoszono nową usterkę w mieszkaniu {0} ({1})</h1><p style=\"font-family:'Open Sans'\">{2}</p><p><img src=\"{3}\"></p></body></html>";
            request.AddParameter("html", string.Format(template, flat.Address, flat.Name, defect.Description, defectViewModel.ImageId.ToString()+".png"));
            var logoPath = HostingEnvironment.MapPath("~/Content/Images/Flateo2.png");
            request.AddFile("attachment", logoPath);
            request.AddFile("attachment", defectImagePath);

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