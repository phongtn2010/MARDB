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
    public class TraCuuHoSoController : Controller
    {
        // GET: TraCuuHoSo
        public ActionResult Index(sHoSoModels models)
        {
            if (models == null)
            {
                models = new sHoSoModels
                {
                    Page = 1,
                    PageSize = Globals.PageSize,
                    LoaiDanhSach = 0
                };
            }

            return View(models);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Detail(string iID_MaHoSo)
        {
            HoSoModels hoSo = clHoSo.GetHoSoById(iID_MaHoSo);
            ViewData["DuLieuMoi"] = "0";
            return View(hoSo);
        }
        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Search(string ParentID, string smenu)
        {
            string _sMaHoSo = CString.SafeString(Request.Form[ParentID + "_sMaHoSo"]);
            string TuNgayDen = CString.SafeString(Request.Form[ParentID + "_viTuNgayDen"]);
            string DenNgayDen = CString.SafeString(Request.Form[ParentID + "_viDenNgayDen"]);
            string _sTenDoanhNghiep = CString.SafeString(Request.Form[ParentID + "_sTenDoanhNghiep"]);
            string _sTenTACN = CString.SafeString(Request.Form[ParentID + "_sTenTACN"]);
           
            string _sSoTiepNhan = CString.SafeString(Request.Form[ParentID + "_sSoTiepNhan"]);
            string TuNgayTiepNhan = CString.SafeString(Request.Form[ParentID + "_viTuNgayTiepNhan"]);
            string DenNgayTiepNhan = CString.SafeString(Request.Form[ParentID + "_viDenNgayTiepNhan"]);


            string sSoGDK = CString.SafeString(Request.Form[ParentID + "_sSoGDK"]);
            string TuNgayXacNhan = CString.SafeString(Request.Form[ParentID + "_viTuNgayXacNhan"]);
            string DenNgayXacNhan = CString.SafeString(Request.Form[ParentID + "_viDenNgayXacNhan"]);

            string iID_MaTrangThai = CString.SafeString(Request.Form[ParentID + "_iID_MaTrangThai"]);

            string iID_KetQuaXuLy = CString.SafeString(Request.Form[ParentID + "_iID_KetQuaXuLy"]);
            sHoSoModels models = new sHoSoModels
            {
                LoaiDanhSach = 0,
                sMaHoSo = _sMaHoSo,
                sTenDoanhNghiep = _sTenDoanhNghiep,
                sTenTACN = _sTenTACN,
                TuNgayDen = TuNgayDen,
                DenNgayDen = DenNgayDen,
                sSoTiepNhan = _sSoTiepNhan,
                TuNgayTiepNhan = TuNgayTiepNhan,
                DenNgayTiepNhan = DenNgayTiepNhan,
                sSoGDK=sSoGDK,
                TuNgayXacNhan= TuNgayXacNhan,
                DenNgayXacNhan= DenNgayXacNhan,
                iID_MaTrangThai=Convert.ToInt32(iID_MaTrangThai),
                iID_KetQuaXuLy = Convert.ToInt32(iID_KetQuaXuLy)
            };

            TempData["menu"] = smenu;
            TempData["msg"] = "success";
            return RedirectToAction("Index", models);
        }
    }
}