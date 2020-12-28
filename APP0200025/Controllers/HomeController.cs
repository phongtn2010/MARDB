using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DATA0200025;
using DATA0200025.Models;

namespace APP0200025.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Viewer(string filePath)
        {
            //build the file path here
            filePath = "/MyPDFs/" + filePath;
            //pass the file path to the View using a viewbag variable
            ViewBag.filePath = filePath;
            //We could also just return a view along with a query string with a file param pointing to the
            //location of the file on our server, for example: "Viewer?file=/MyPDFs/Pdf1.pdf"
            //but here I've just chosen to change the default URL of the viewer object, which is essentially
            //the same thing
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}