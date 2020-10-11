using DATA0200025.SearchModels;
using DomainModel;
using DomainModel.Abstract;
using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;

namespace APP0200025.Controllers
{
    public class ChoTiepNhanHoSoController : Controller
    {
        // GET: ChoTiepNhanHoSo
        public ActionResult Index()
        {
            var models = new sHoSoModels
            {
                Page = 1,
                PageSize = Globals.PageSize
            };
            return View(models);
        }
        /// <summary>
        /// view màn hình TiepNhanHoSo
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult TiepNhanHoSo()
        {
            ViewData["DuLieuMoi"] = "0";
            return View();
        }
        /// <summary>
        /// view màn hình YeuCauBoXung
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult YeuCauBoXung()
        {
            ViewData["DuLieuMoi"] = "0";
            return View();
        }
        /// <summary>
        /// view màn hình TuChoiHoSo
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult TuChoiHoSo()
        {
            ViewData["DuLieuMoi"] = "0";
            return View();
        }
        /// <summary>
        /// update data màn hình TiepNhanHoSo
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="MaHoSo"></param>
        /// <returns></returns>
        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult TiepNhanHoSo(String ParentID, String MaHoSo)
        {
            Bang bang = new Bang("CNN25_HoSo");
            bang.MaNguoiDungSua = User.Identity.Name;
            bang.IPSua = Request.UserHostAddress;
            bang.TruyenGiaTri(ParentID, Request.Form);
            bang.Save();
            return RedirectToAction("Index");
        }
        /// <summary>
        /// update data màn hình YeuCauBoXung
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="MaHoSo"></param>
        /// <returns></returns>
        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult YeuCauBoXung(String ParentID)
        {
            NameValueCollection values = new NameValueCollection();
            string sTenNguoiTiepNhan = CString.SafeString(Request.Form[ParentID + "_sTenNguoiTiepNhan"]);
            string iID_MaHoSo = CString.SafeString(Request.Form[ParentID + "iID_MaHoSo"]);
            if (string.IsNullOrEmpty(sTenNguoiTiepNhan))
            {
                values.Add("err_sTenNguoiTiepNhan", "Bạn nhập thông tin yêu cầu cần bổ xung");
            }
            if (values.Count > 0)
            {
                for (int i = 0; i <= (values.Count - 1); i++)
                {
                    ModelState.AddModelError(ParentID + "_" + values.GetKey(i), values[i]);
                }
                base.ViewData["DuLieuMoi"] = "0";
                ViewData["iID_MaHoSo"] = CString.SafeString(iID_MaHoSo);
                return View();
            }
            Bang bang = new Bang("CNN25_HoSo");
            bang.MaNguoiDungSua = User.Identity.Name;
            bang.IPSua = Request.UserHostAddress;
            bang.TruyenGiaTri(ParentID, Request.Form);
            bang.Save();
            return RedirectToAction("Index");
        }
        /// <summary>
        /// update data màn hình TuChoiHoSo
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="MaHoSo"></param>
        /// <returns></returns>
        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult TuChoiHoSo(String ParentID)
        {
            NameValueCollection values = new NameValueCollection();
            string sTenNguoiTiepNhan = CString.SafeString(Request.Form[ParentID + "_sTenNguoiTiepNhan"]);
            string iID_MaHoSo = CString.SafeString(Request.Form[ParentID + "iID_MaHoSo"]);
            if (string.IsNullOrEmpty(sTenNguoiTiepNhan))
            {
                values.Add("err_sTenNguoiTiepNhan", "Bạn nhập thông tin yêu cầu cần bổ xung");
            }
            if (values.Count > 0)
            {
                for (int i = 0; i <= (values.Count - 1); i++)
                {
                    ModelState.AddModelError(ParentID + "_" + values.GetKey(i), values[i]);
                }
                base.ViewData["DuLieuMoi"] = "0";
                ViewData["iID_MaHoSo"] = CString.SafeString(iID_MaHoSo);
                return View();
            }
            Bang bang = new Bang("CNN25_HoSo");
            bang.MaNguoiDungSua = User.Identity.Name;
            bang.IPSua = Request.UserHostAddress;
            bang.TruyenGiaTri(ParentID, Request.Form);
            bang.Save();
            return RedirectToAction("Index");
        }
        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Search(string ParentID)
        {
            string _sMaHoSo = CString.SafeString(Request.Form[ParentID + "_sMaHoSo"]);
            string _sTenDoanhNgiep = CString.SafeString(Request.Form[ParentID + "_sTenDoanhNgiep"]);
            string _sTenTACN = CString.SafeString(Request.Form[ParentID + "_sTenTACN"]);
            string _FromDate = CString.SafeString(Request.Form[ParentID + "_viFromDate"]);
            string _ToDate = CString.SafeString(Request.Form[ParentID + "_viToDate"]);
            sHoSoModels models = new sHoSoModels
            {
                sMaHoSo = _sMaHoSo,
                sTenDoanhNghiep = _sTenDoanhNgiep,
                sTenTACN = _sTenTACN,
                FromDate = _FromDate,
                ToDate = _ToDate
            };
            return RedirectToAction("Index", models);
        }
    }
}