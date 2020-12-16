using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using DomainModel;
using DATA0200025;
using APP0200025.Models;

namespace APP0200025.Controllers.Api
{
    public class UploadFileController : ApiController
    {
        public HttpResponseMessage Post()
        {
            HttpResponseMessage result = null;

            List<ResDinhKemFiles> listDinhKemFile = new List<ResDinhKemFiles>();

            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string inputTagName in HttpContext.Current.Request.Files)
                {
                    HttpPostedFileBase filebase = new HttpPostedFileWrapper(HttpContext.Current.Request.Files[inputTagName]);
                    if (filebase.ContentLength > 0)
                    {
                        ResDinhKemFiles itemDinhKemFile = new ResDinhKemFiles();

                        itemDinhKemFile = CDinhKemFiles.UploadFile(filebase);

                        listDinhKemFile.Add(new ResDinhKemFiles { ItemId = itemDinhKemFile.ItemId, UrlFile = itemDinhKemFile.UrlFile });
                    }
                }

                result = Request.CreateResponse(HttpStatusCode.Created, listDinhKemFile);
            }
            else
            {
                listDinhKemFile.Add(new ResDinhKemFiles { ItemId = -1, UrlFile = "Upload File Error: Không có file nào!" });

                result = Request.CreateResponse(HttpStatusCode.Created, listDinhKemFile);
            }

            return result;
        }
    }
}
