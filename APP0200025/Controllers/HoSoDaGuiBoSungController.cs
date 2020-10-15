using DATA0200025;
using DATA0200025.Models;
using DATA0200025.SearchModels;
using DomainModel;
using DomainModel.Abstract;
using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;

namespace APP0200025.Controllers
{
    public class HoSoDaGuiBoSungController : Controller
    {
        // GET: HoSoDaGuiBoSung
        Bang bang = new Bang("CNN25_HoSo");

        private string ViewPath = "~/Views/HoSoDaGuiBoSung/";
        // GET: ChoTiepNhanHoSo
        public ActionResult Index(sHoSoModels models)
        {
            if (models == null || models.LoaiDanhSach == 0)
            {
                models = new sHoSoModels
                {
                    LoaiDanhSach=2,
                    Page = 1,
                    PageSize = Globals.PageSize
                };
            }
            return View(models);
        }
    }
}