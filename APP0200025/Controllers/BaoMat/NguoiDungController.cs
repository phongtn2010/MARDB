using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DATA0200025;
using APP0200025.Models;
using DomainModel;
using DomainModel.Abstract;

namespace APP0200025.Controllers
{
    public class NguoiDungController : Controller
    {
        // GET: NguoiDung
        [Authorize]
        public ActionResult Index()
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "QT_NguoiDung", "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["smenu"] = 0;
            return View();
        }
        public ActionResult Detail(string sUserName)
        {
            ViewData["MaNguoiDung"] = sUserName;
            ViewData["smenu"] = 0;
            return View();
        }
        [Authorize]
        public ActionResult KhachHang()
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "QT_NguoiDung", "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["smenu"] = 0;
            return View();
        }
        [HttpPost, ValidateInput(false), Authorize]
        public ActionResult KhachHang_search(string ParentID)
        {
            string _TieuDe = CString.SafeString(Request.Form[ParentID + "_TieuDe"]);
            string _FromDate = CString.SafeString(Request.Form[ParentID + "_FromDate"]);
            string _ToDate = CString.SafeString(Request.Form[ParentID + "_ToDate"]);
            return RedirectToAction("KhachHang", "NguoiDung", new { _TieuDe = _TieuDe, _FromDate = _FromDate, _ToDate = _ToDate });
        }
        [Authorize]
        public ActionResult Edit(String MaNguoiDung, String MaNhomNguoiDung)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "QT_NguoiDung", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["smenu"] = 0;
            ViewData["MaNguoiDung"] = CString.SafeString(MaNguoiDung);
            ViewData["MaNhomNguoiDung"] = CString.SafeString(MaNhomNguoiDung);
            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(String ParentID, String MaNguoiDung, String MaNhomNguoiDung)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "QT_NguoiDung", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            NameValueCollection arrLoi = new NameValueCollection();
            String iID_MaNhomNguoiDung = CString.SafeString(Request.Form[ParentID + "_iID_MaNhomNguoiDung"]);
            String _sHoTen = CString.SafeString(Request.Form[ParentID + "_sHoTen"]);
            String _sEmail = CString.SafeString(Request.Form[ParentID + "_sEmail"]);
            string sMaToChucChiDinh = CString.SafeString(Request.Form[ParentID + "_sID_MaNhomNguoiDung_DuocQuanTri"]).Trim();
            String bTrangThai = CString.SafeString(Request.Form[ParentID + "_bTrangThai"]);

            if (iID_MaNhomNguoiDung == string.Empty || iID_MaNhomNguoiDung == "")
            {
                arrLoi.Add("err_iID_MaNhomNguoiDung", "Bạn chưa chọn nhóm người đùng!");
            }
            if (_sHoTen == string.Empty || _sHoTen == "")
            {
                arrLoi.Add("err_sHoTen", "Bạn chưa nhập tên tài khoản!");
            }
            if (_sEmail == string.Empty || _sEmail == "")
            {
                arrLoi.Add("err_sEmail", "Bạn chưa nhập email!");
            }
            if (arrLoi.Count > 0)
            {
                for (int i = 0; i <= arrLoi.Count - 1; i++)
                {
                    ModelState.AddModelError(ParentID + "_" + arrLoi.GetKey(i), arrLoi[i]);
                }
                return View();
            }
            else
            {
                Bang bang = new Bang("QT_NguoiDung");
                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                bang.TruyenGiaTri(ParentID, Request.Form);

                //Sua
                bang.DuLieuMoi = false;
                bang.GiaTriKhoa = MaNguoiDung;

                bang.Save();

                //int vR = CPQ_NGUOIDUNG.Update(iID_MaNhomNguoiDung, sMaToChucChiDinh, MaNguoiDung, _sHoTen, _sEmail);

                if(String.IsNullOrEmpty(MaNhomNguoiDung) == false)
                {
                    return RedirectToAction("Edit_NguoiDung", "NhomNguoiDung", new { MaNhomNguoiDung = MaNhomNguoiDung });
                }
                else
                {
                    return RedirectToAction("Index");
                }                
            }
        }
        [Authorize]
        public ActionResult Delete(String MaNguoiDung, String MaNhomNguoiDung)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "QT_NguoiDung", "Delete") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            if (String.IsNullOrEmpty(MaNguoiDung) == false)
            {
                int vR = CPQ_NGUOIDUNG.Delete(MaNguoiDung);
            }
            return RedirectToAction("Index");            
        }
    }
}