using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;

namespace APP0200025.Models
{
    public class CDinhKemFiles
    {
        public static ResDinhKemFiles UploadFile(HttpPostedFileBase hpf)
        {
            ResDinhKemFiles vR = new ResDinhKemFiles();

            if (hpf != null && hpf.ContentLength > 0)
            {
                String FileName = hpf.FileName;
                var filename = Path.GetFileName(hpf.FileName);

                using (var client = new HttpClient())
                {
                    using (var content = new MultipartFormDataContent())
                    {
                        byte[] Bytes = new byte[hpf.InputStream.Length + 1];
                        hpf.InputStream.Read(Bytes, 0, Bytes.Length);
                        var fileContent = new ByteArrayContent(Bytes);
                        fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { FileName = hpf.FileName };
                        content.Add(fileContent);
                        var requestUri = WebConfigurationManager.AppSettings["api_url_upload_25"];   // "http://mardapi.adp-p.com/api/fileuploadapi";
                        try
                        {
                            var result = client.PostAsync(requestUri, content).Result;
                            if (result.StatusCode == System.Net.HttpStatusCode.Created)
                            {
                                string jsonReturn = result.Content.ReadAsStringAsync().Result;
                                dynamic obj = JsonConvert.DeserializeObject(jsonReturn);
                                long iMa = (long)obj[0].ItemId;
                                string sURL = (string)obj[0].UrlFile;

                                vR.ItemId = iMa;
                                vR.UrlFile = sURL;
                            }
                            else
                            {
                                vR.ItemId = -1;
                                vR.UrlFile = "";
                            }
                        }
                        catch (Exception ex)
                        {
                            vR.ItemId = -1;
                            vR.UrlFile = "";
                        }
                    }
                }
            }

            return vR;
        }
    }

    public class ResDinhKemFiles
    {
        public long ItemId { get; set; }
        public string UrlFile { get; set; }
    }
}