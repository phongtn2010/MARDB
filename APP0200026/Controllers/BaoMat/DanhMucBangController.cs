using DATA0200026;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel;
using DomainModel.Abstract;
using System.Data.SqlClient;

namespace APP0200026.Controllers
{
    public class DanhMucBangController : Controller
    {
        // GET: DanhMucBang
        [Authorize]
        public ActionResult Index()
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "PQ_DanhMucBang", "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["smenu"] = 0;
            return View();
        }
        [Authorize]
        public ActionResult Edit(string MaBang)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "PQ_DanhMucBang", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["DuLieuMoi"] = 0;
            if (string.IsNullOrEmpty(MaBang))
            {
                ViewData["DuLieuMoi"] = 1;
            }
            ViewData["MaBang"] = MaBang;
            ViewData["smenu"] = 0;
            return View();
        }
        [Authorize]
        [ValidateInput(false), HttpPost]
        public ActionResult Edit(string ParentID, string MaBang)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "PQ_DanhMucBang", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            String sTenBang = CString.SafeString(Request.Form[ParentID + "_sTenBang"]).Trim();
            String sTenBangHT = CString.SafeString(Request.Form[ParentID + "_sTenBangHT"]).Trim();
            string DuLieuMoi = CString.SafeString(Request.Form[ParentID + "_DuLieuMoi"]);

            NameValueCollection arrLoi = new NameValueCollection();
            if (string.IsNullOrEmpty(sTenBang))
            {
                arrLoi.Add("err_sTenBang", "Bạn chưa nhập tên bảng!");
            }
            if (string.IsNullOrEmpty(sTenBangHT))
            {
                arrLoi.Add("err_sTenBangHT", "Bạn chưa nhập tên bảng hiển thị!");
            }
            if (string.IsNullOrEmpty(MaBang) || MaBang == "0")
            {
                if (CPQ_BANG.Check_Table(sTenBang) == true)
                {
                    arrLoi.Add("err_sTenBang", "Bạn đã thêm bảng này rồi!");
                }
            }

            if (arrLoi.Count > 0)
            {
                for (int i = 0; i <= arrLoi.Count - 1; i++)
                {
                    ModelState.AddModelError(ParentID + "_" + arrLoi.GetKey(i), arrLoi[i]);
                }

                ViewData["MaBang"] = MaBang;
                ViewData["smenu"] = 0;
                ViewData["DuLieuMoi"] = 0;
                if (string.IsNullOrEmpty(MaBang))
                {
                    ViewData["DuLieuMoi"] = 1;
                }
                return View();
            }

            try
            {
                Bang bang = new Bang("PQ_DanhMucBang");
                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                bang.TruyenGiaTri(ParentID, Request.Form);

                if (DuLieuMoi == "0")
                {
                    //Sua
                    bang.DuLieuMoi = false;
                    bang.GiaTriKhoa = MaBang;
                }
                else
                {
                    //Them moi
                    bang.DuLieuMoi = true;
                }
                bang.Save();

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index", "Errors", new { sMess = ex.ToString() });
            }
        }
        [Authorize]
        public ActionResult Delete(string MaBang)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "PQ_DanhMucBang", "Delete") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            try
            {
                if (string.IsNullOrEmpty(MaBang) == false)
                {
                    SqlCommand cmd;

                    //Delete
                    cmd = new SqlCommand("DELETE FROM PQ_DanhMucBang WHERE iID_MaDanhMucBang=@iID_MaDanhMucBang");
                    cmd.Parameters.AddWithValue("@iID_MaDanhMucBang", CString.SafeString(MaBang));
                    Connection.UpdateDatabase(cmd);
                    cmd.Dispose();
                }

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index", "Errors", new { sMess = ex.ToString() });
            }
        }
    }
}