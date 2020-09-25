using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel;
using DATA0200025;
using APP0200025.Models;
using System.Collections.Specialized;
using DomainModel.Abstract;
using System.Data.SqlClient;

namespace APP0200025.Controllers
{
    public class DanhMucTruongController : Controller
    {
        // GET: DanhMucTruong
        [Authorize]
        public ActionResult Index(string TenBang)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "PQ_DanhMucTruong", "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["TenBang"] = CString.SafeString(TenBang);
            ViewData["smenu"] = 0;
            return View();
        }
        [Authorize]
        public ActionResult Edit(String TenBang, String MaDanhMucTruong)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "PQ_DanhMucTruong", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["DuLieuMoi"] = 0;
            if (string.IsNullOrEmpty(MaDanhMucTruong))
            {
                ViewData["DuLieuMoi"] = 1;
            }
            ViewData["MaDanhMucTruong"] = CString.SafeString(MaDanhMucTruong);
            ViewData["TenBang"] = CString.SafeString(TenBang);
            return View();
        }
        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(String ParentID, string TenBang, String MaDanhMucTruong)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "PQ_DanhMucTruong", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            String sTenTruong = CString.SafeString(Request.Form[ParentID + "_sTenTruong"]).Trim();
            String sTenTruongHT = CString.SafeString(Request.Form[ParentID + "_sTenTruongHT"]).Trim();
            String bLuonDuocXem = CString.SafeString(Request.Form[ParentID + "_bLuonDuocXem"]);
            string DuLieuMoi = CString.SafeString(Request.Form[ParentID + "_DuLieuMoi"]);

            NameValueCollection arrLoi = new NameValueCollection();
            if (string.IsNullOrEmpty(sTenTruong))
            {
                arrLoi.Add("err_sTenTruong", "Bạn chưa nhập tên trường!");
            }
            if (string.IsNullOrEmpty(sTenTruongHT))
            {
                arrLoi.Add("err_sTenTruongHT", "Bạn chưa nhập tên trường hiển thị!");
            }
            if (string.IsNullOrEmpty(MaDanhMucTruong) || MaDanhMucTruong == "0")
            {
                if (CPQ_BANG.Check_Table_Truong(TenBang, sTenTruong) == true)
                {
                    arrLoi.Add("err_sTenTruong", "Bạn đã thêm trường này rồi!");
                }
            }
            if (arrLoi.Count > 0)
            {
                for (int i = 0; i <= arrLoi.Count - 1; i++)
                {
                    ModelState.AddModelError(ParentID + "_" + arrLoi.GetKey(i), arrLoi[i]);
                }

                ViewData["MaDanhMucTruong"] = CString.SafeString(MaDanhMucTruong);
                ViewData["TenBang"] = CString.SafeString(TenBang);
                ViewData["smenu"] = 0;
                ViewData["DuLieuMoi"] = 0;
                if (string.IsNullOrEmpty(MaDanhMucTruong))
                {
                    ViewData["DuLieuMoi"] = 1;
                }
                return View();
            }

            try
            {
                Bang bang = new Bang("PQ_DanhMucTruong");
                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                bang.TruyenGiaTri(ParentID, Request.Form);

                bang.CmdParams.Parameters.AddWithValue("@sTenBang", TenBang);

                if (DuLieuMoi == "0")
                {
                    //Sua
                    bang.DuLieuMoi = false;
                    bang.GiaTriKhoa = MaDanhMucTruong;
                }
                else
                {
                    //Them moi
                    bang.DuLieuMoi = true;
                }
                bang.Save();

                return RedirectToAction("Index", "DanhMucTruong", new { TenBang = TenBang });
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index", "Errors", new { sMess = ex.ToString() });
            }
        }
        [Authorize]
        public ActionResult Delete(string MaDanhMucTruong, string TenBang)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "PQ_DanhMucTruong", "Delete") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            try
            {
                if (string.IsNullOrEmpty(MaDanhMucTruong) == false)
                {
                    SqlCommand cmd;

                    //Delete
                    cmd = new SqlCommand("DELETE FROM PQ_DanhMucTruong WHERE iID_MaDanhMucTruong=@iID_MaDanhMucTruong");
                    cmd.Parameters.AddWithValue("@iID_MaDanhMucTruong", CString.SafeString(MaDanhMucTruong));
                    Connection.UpdateDatabase(cmd);
                    cmd.Dispose();
                }

                return RedirectToAction("Index", "DanhMucTruong", new { TenBang = TenBang });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Errors", new { sMess = ex.ToString() });
            }
        }
        [Authorize]
        public ActionResult Sort(string TenBang)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "PQ_DanhMucTruong", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["TenBang"] = CString.SafeString(TenBang);
            return View();
        }
        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Sort(String ParentID, String TenBang)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "PQ_DanhMucTruong", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            string strOrder = CString.SafeString(Request.Form["hiddenOrder"].ToString());
            try
            {
                if (strOrder.IndexOf('$') > 0)
                {
                    String[] arrTG = strOrder.Split('$');
                    int i;
                    SqlCommand cmd;
                    for (i = 0; i < arrTG.Length - 1; i++)
                    {
                        cmd = new SqlCommand();
                        cmd.CommandText = "UPDATE PQ_DanhMucTruong SET iSTT=@iSTT WHERE iID_MaDanhMucTruong=@iID_MaDanhMucTruong";
                        cmd.Parameters.AddWithValue("@iSTT", i);
                        cmd.Parameters.AddWithValue("@iID_MaDanhMucTruong", arrTG[i]);
                        Connection.UpdateDatabase(cmd);
                        cmd.Dispose();
                    }
                }
                return RedirectToAction("Index", "DanhMucTruong", new { TenBang = TenBang });
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index", "Errors", new { sMess = ex.ToString() });
            }
        }
    }
}