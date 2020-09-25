using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APP0200025.Controllers
{
    public class ErrorsController : Controller
    {
        // GET: Errors
        public ActionResult Index(String sMess)
        {
            ViewData["sMess"] = sMess;
            return View();
        }
    }
}