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

            if (models == null)
            {
                models = new sHoSoModels
                {
                    Page = 1,
                    PageSize = 30,
                    LoaiDanhSach = 0
                };
            }

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
            string _sMaHoSo = CString.SafeString(Request.Form[ParentID + "_sMaHoSo"]).Trim();
            string TuNgayDen = CString.SafeString(Request.Form[ParentID + "_viTuNgayDen"]);
            string DenNgayDen = CString.SafeString(Request.Form[ParentID + "_viDenNgayDen"]);
            string _sTenDoanhNghiep = CString.SafeString(Request.Form[ParentID + "_sTenDoanhNghiep"]).Trim();
            string _sTenTACN = CString.SafeString(Request.Form[ParentID + "_sTenTACN"]).Trim();

            string _sSoTiepNhan = CString.SafeString(Request.Form[ParentID + "_sSoTiepNhan"]).Trim();
            string TuNgayTiepNhan = CString.SafeString(Request.Form[ParentID + "_viTuNgayTiepNhan"]);
            string DenNgayTiepNhan = CString.SafeString(Request.Form[ParentID + "_viDenNgayTiepNhan"]);


            string sSoGDK = CString.SafeString(Request.Form[ParentID + "_sSoGDK"]).Trim();
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
                sSoGDK = sSoGDK,
                TuNgayXacNhan = TuNgayXacNhan,
                DenNgayXacNhan = DenNgayXacNhan,
                iID_MaTrangThai = Convert.ToInt32(iID_MaTrangThai),
                iID_KetQuaXuLy = Convert.ToInt32(iID_KetQuaXuLy)
            };

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