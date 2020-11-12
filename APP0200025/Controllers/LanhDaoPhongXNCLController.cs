using DATA0200025;
using DATA0200025.Models;
using DATA0200025.SearchModels;
using DomainModel;
using DomainModel.Abstract;
using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Web;
using System.Web.Mvc;

namespace APP0200025.Controllers
{
    public class LanhDaoPhongXNCLController : Controller
    {
        Bang bang = new Bang("CNN25_HoSo");

        // GET: LanhDaoPhongXNCL
        public ActionResult Index(sHoSoModels models)
        {
            if (models == null || models.LoaiDanhSach == 0)
            {
                models = new sHoSoModels
                {
                    Page = 1,
                    PageSize = Globals.PageSize,
                    LoaiDanhSach = 11
                };
            }

            ViewData["menu"] = 207;
            return View(models);
        }

        public ActionResult Detail(string iID_MaHoSo)
        {
            HoSoModels hoSo = clHoSo.GetHoSoById(iID_MaHoSo);

            ViewData["menu"] = 207;
            return View(hoSo);
        }
    }
}