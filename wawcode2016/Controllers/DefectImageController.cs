using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using wawcode2016.ViewModels;

namespace wawcode2016.Controllers
{
    public class DefectImageController : ApiController
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
        public DefectImageViewModel Post()
        {
            var httpRequest = HttpContext.Current.Request;
            if(httpRequest.Files.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var file = httpRequest.Files[0];
            Guid fileId = Guid.NewGuid();

            byte[] data;
            using (BinaryReader reader = new BinaryReader(file.InputStream))
            {
                data = reader.ReadBytes((int)file.InputStream.Length);
            }

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("defectimages");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileId.ToString()+".png");
            using (var ms = new MemoryStream(data, false))
            {
                blockBlob.UploadFromStream(ms);
            }

            return new DefectImageViewModel()
            {
                ImageId = fileId
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