using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using DomainModel;
using DomainModel.Abstract;
using DATA0200025;
using DATA0200025.Models;
using DATA0200025.SearchModels;
using DATA0200025.WebServices.XmlType.Request;
using DATA0200025.WebServices;
using APP0200025.Models;
using System.Data.SqlClient;

namespace APP0200025.Controllers
{
    public class CMS_HoSoController : Controller
    {
        Bang bang = new Bang("CNN25_HoSo");
        // GET: CMS_HoSo
        public ActionResult Index(sHoSoModels models)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, bang.TenBang, "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            if (models == null || models.LoaiDanhSach == 0)
            {
                models = new sHoSoModels
                {
                    LoaiDanhSach = 0,
                    Page = 1,
                    PageSize = 30
                };
            }

            ViewData["menu"] = 229;
            return View(models);
        }

        public ActionResult Detail(string iID_MaHoSo)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, bang.TenBang, "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            clHoSo.UpdateNguoiXem(iID_MaHoSo, User.Identity.Name);
            ViewData["menu"] = 229;
            HoSoModels models = clHoSo.GetHoSoById(Convert.ToInt32(iID_MaHoSo));

            return View(models);
        }

        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Search(string ParentID)
        {
            string _sMaHoSo = CString.SafeString(Request.Form[ParentID + "_sMaHoSo"]);
            string _sTenDoanhNghiep = CString.SafeString(Request.Form[ParentID + "_sTenDoanhNghiep"]);
            string _sTenTACN = CString.SafeString(Request.Form[ParentID + "_sTenTACN"]);
            string _TuNgayDen = CString.SafeString(Request.Form[ParentID + "_viTuNgayDen"]);
            string _DenNgayDen = CString.SafeString(Request.Form[ParentID + "_viDenNgayDen"]);

            sHoSoModels models = new sHoSoModels
            {
                LoaiDanhSach = 0,
                sMaHoSo = _sMaHoSo,
                sTenDoanhNghiep = _sTenDoanhNghiep,
                sTenTACN = _sTenTACN,
                TuNgayDen = _TuNgayDen,
                DenNgayDen = _DenNgayDen
            };

            TempData["menu"] = 229;
            TempData["msg"] = "success";
            return RedirectToAction("Index", models);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Delete(string iID_MaHoSo)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, bang.TenBang, "Delete") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            try
            {
                if (string.IsNullOrEmpty(iID_MaHoSo) == false)
                {
                    SqlCommand cmd;

                    //Xoa hang hoa
                    cmd = new SqlCommand("DELETE FROM CNN25_HangHoa WHERE iID_MaHoSo=@iID_MaHoSo");
                    cmd.Parameters.AddWithValue("@iID_MaHoSo", CString.SafeString(iID_MaHoSo));
                    Connection.UpdateDatabase(cmd);
                    cmd.Dispose();

                    //Xoa ho so
                    cmd = new SqlCommand("DELETE FROM CNN25_HoSo WHERE iID_MaHoSo=@iID_MaHoSo");
                    cmd.Parameters.AddWithValue("@iID_MaHoSo", CString.SafeString(iID_MaHoSo));
                    Connection.UpdateDatabase(cmd);
                    cmd.Dispose();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Errors", new { sMess = ex.ToString() });
            }
        }
    }
}