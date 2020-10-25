using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace APP0200025.Controllers.Api
{
    public class FileUploadApiController : ApiController
    {
        [HttpPost]
        public Task<HttpResponseMessage> Post()
        {
            List<ResUpLoadFile> savedFilePath = new List<ResUpLoadFile>();
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            string rootPath = HttpContext.Current.Server.MapPath("~/Files");
            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);
            var pathProvider = new MultipartFileStreamProvider(rootPath);
            var newTask = Request.Content.ReadAsMultipartAsync(pathProvider).
                ContinueWith(task =>
                {
                    if (task.IsCanceled || task.IsFaulted)
                    {
                        Request.CreateErrorResponse(HttpStatusCode.InternalServerError, task.Exception);
                    }

                    int i = 0;
                    foreach (MultipartFileData item in pathProvider.FileData)
                    {
                        try
                        {
                            string fileName = item.Headers.ContentDisposition.FileName.Replace("\"", "");
                            string newFileName = Guid.NewGuid() + Path.GetExtension(fileName);
                            File.Move(item.LocalFileName, Path.Combine(rootPath, newFileName));
                            Uri baseuri = new Uri(Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.PathAndQuery, string.Empty));
                            string fileRelativePath = "~/Files/" + newFileName;
                            Uri fileFullPath = new Uri(baseuri, VirtualPathUtility.ToAbsolute(fileRelativePath));

                            savedFilePath.Add(new ResUpLoadFile { ItemId=i, UrlFile = fileFullPath.ToString() });

                            i++;
                        }
                        catch (Exception ex) { }
                    }
                    return Request.CreateResponse(HttpStatusCode.Created, savedFilePath);
                });
            return newTask;
        }
    }

    public class ResUpLoadFile
    {
        public int ItemId { get; set; }
        public string UrlFile { get; set; }
    }
}
