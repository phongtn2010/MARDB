using DATA0200025;
using DATA0200025.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APP0200025.Controllers
{
    public class HoSoController : Controller
    {
        // GET: HoSo
        public ActionResult Index(string sMaHoSo)
        {
            HoSoModels models = clHoSo.GetHoSo_ChiTiet_Theo_Ma(sMaHoSo);

            return View(models);
        }
    }
}