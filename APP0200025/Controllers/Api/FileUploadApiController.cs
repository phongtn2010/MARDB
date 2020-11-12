using DATA0200025;
using DomainModel;
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
            string sPath = "/Uploads/Files";
            string guid = Guid.NewGuid().ToString();
            DateTime TG = DateTime.Now;
            string subPath = TG.ToString("yyyy/MM/dd");
            string newPath = string.Format("{0}/{1}", sPath, subPath);
            string rootPath = HttpContext.Current.Server.MapPath("~" + newPath);
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
                            string ext = Path.GetExtension(fileName);

                            if (CommonFunction.FileUploadCheck_Api(ext))
                            {
                                string newFileName = Guid.NewGuid() + Path.GetExtension(fileName);
                                File.Move(item.LocalFileName, Path.Combine(rootPath, newFileName));
                                Uri baseuri = new Uri(Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.PathAndQuery, string.Empty));

                                string fileRelativePath = "~" + newPath + "/" + newFileName;
                                Uri fileFullPath = new Uri(baseuri, VirtualPathUtility.ToAbsolute(fileRelativePath));

                                //Them File vao Bang DinhKem
                                long iID_MaFile = CDinhKem.ThemDinhKem(0, 0, 0, "", "", "", fileName, "", null, 1, fileFullPath.ToString(), "doanhnghiep", "");

                                //Gui lai thong tin ID và duong dan cho nsw
                                savedFilePath.Add(new ResUpLoadFile { ItemId = iID_MaFile, UrlFile = fileFullPath.ToString() });
                            }
                            else
                            {
                                savedFilePath.Add(new ResUpLoadFile { ItemId = -1, UrlFile = "Upload File Error: Không đúng định dạng file!" });
                            }

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
        public long ItemId { get; set; }
        public string UrlFile { get; set; }
    }
}
