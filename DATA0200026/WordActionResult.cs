using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace DATA0200026
{
    public class WordActionResult : ActionResult
    {
        private string _content;
        public string content
        {
            get { return _content; }
        }

        private string _filename;
        public string fileName
        {
            get { return _filename; }
        }

        public WordActionResult(string pContent, string pFileName)
        {
            _content = pContent;
            _filename = pFileName;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            HttpContext curContext = HttpContext.Current;
            curContext.Response.Clear();
            curContext.Response.AddHeader("content-disposition", "attachment;filename=" + _filename);
            curContext.Response.Charset = "";
            curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
            curContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            curContext.Response.ContentType = "application/ms-word";

            //HttpWebRequest wreq = (HttpWebRequest)HttpWebRequest.Create(_url);
            //HttpWebResponse wres = (HttpWebResponse)wreq.GetResponse();
            //Stream s = wres.GetResponseStream();
            //StreamReader sr = new StreamReader(s, Encoding.UTF8);

            curContext.Response.Write(_content);
            curContext.Response.End();
        }

    }
}