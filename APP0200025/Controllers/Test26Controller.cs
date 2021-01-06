using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Abstract;
using DATA0200026;
using DATA0200026.WebServices;
using DomainModel;
using DATA0200026.WebServices.XmlType.Request;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Drawing;

namespace APP0200025.Controllers
{
    public class Test26Controller : Controller
    {
        private SendService _sendService = new SendService();

        // GET: Test26
        public ActionResult Index()
        {
            ////XML13(01)
            //PhanHoiDonXM resultConfirm = new PhanHoiDonXM();
            //resultConfirm.NSWFileCode = "MK26200010210";
            //resultConfirm.NameOfStaff = "admin";
            //resultConfirm.ResponseDateString = DateTime.Now;

            //string error = _sendService.PhanHoiDonXM("MK26200010210", resultConfirm);

            string sLink = ShowLinkFile();



            return View();
        }

        private string ShowLinkFile()
        {
            string vR = "";

            string sLink = "https://mcqgcn.mard.gov.vn/Uploads/Files/2020/12/25/6abc18f8-4cc9-4678-9adb-05ce84ef7250.pdf";

            string[] stringParts = sLink.Split(new char[] { '/' });
            string sYYYY = stringParts[5];
            string sMM = stringParts[6];
            string sDD = stringParts[7];
            string sFileName = stringParts[8];

            string sPath = "/Uploads/Files";
            string subPath = sYYYY + "/" + sMM + "/" + sDD;    //yyyy/MM/dd
            string newPath = string.Format("{0}/{1}", sPath, subPath);
            string rootPath = Server.MapPath("~" + newPath);
            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);

            string pdfFilePath = rootPath + "/" + sFileName;

            string url = @"" + sLink + "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            byte[] byteData;
            using (Stream stream = response.GetResponseStream())
            {
                byteData = ReadStream(stream, 1000);
            }
            using (MemoryStream mStream = new MemoryStream(byteData))
            {
                //Image image = Image.FromStream(mStream);
                //this.pictureBoxTest.Image = image;

                System.IO.File.WriteAllBytes(pdfFilePath, byteData);

                //ShowDocument("PHONG.PDF", byteData);
            }

            //Document fileBy = FileToByteArray(url);
            //ShowDocument(fileBy.DocName, fileBy.DocContent);

            vR = sLink.Replace("mcqgcn.mard.gov.vn", "localhost:44360");

            return vR;
        }

        public Document FileToByteArray(string fileName)
        {
            byte[] fileContent = null;
            System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(fs);
            long byteLength = new System.IO.FileInfo(fileName).Length;
            fileContent = binaryReader.ReadBytes((Int32)byteLength);
            fs.Close();
            fs.Dispose();
            binaryReader.Close();
            Document fileDoc = new Document();
            fileDoc.DocName = fileName;
            fileDoc.DocContent = fileContent;
            return fileDoc;
        }

        private void ShowDocument(string fileName, byte[] fileContent)
        {
            //Split the string by character . to get file extension type  
            string[] stringParts = fileName.Split(new char[] { '.' });
            string strType = stringParts[1];
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            //Set the content type as file extension type  
            Response.ContentType = strType;
            //Write the file content  
            this.Response.BinaryWrite(fileContent);
            this.Response.End();
        }

        private byte[] ReadStream(Stream stream, int initialLength)
        {
            if (initialLength < 1)
            {
                initialLength = 32768;
            }
            byte[] buffer = new byte[initialLength];
            int read = 0;
            int chunk;
            while ((chunk = stream.Read(buffer, read, buffer.Length - read)) > 0)
            {
                read += chunk;
                if (read == buffer.Length)
                {
                    int nextByte = stream.ReadByte();
                    if (nextByte == -1)
                    {
                        return buffer;
                    }
                    byte[] newBuffer = new byte[buffer.Length * 2];
                    Array.Copy(buffer, newBuffer, buffer.Length);
                    newBuffer[read] = (byte)nextByte;
                    buffer = newBuffer;
                    read++;
                }
            }
            byte[] bytes = new byte[read];
            Array.Copy(buffer, bytes, read);
            return bytes;
        }

    }

    public class Document
    {
        public int DocId { get; set; }
        public string DocName { get; set; }
        public byte[] DocContent { get; set; }
    }
}