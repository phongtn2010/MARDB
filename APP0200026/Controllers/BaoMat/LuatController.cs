using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel;
using DomainModel.Abstract;
using DATA0200026;

namespace APP0200026.Controllers
{
    public class LuatController : Controller
    {
        // GET: Luat
        [Authorize]
        public ActionResult Index()
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "PQ_Luat", "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["smenu"] = 0;
            return View();
        }
        [Authorize]
        public ActionResult Edit(string MaLuat)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "PQ_Luat", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["DuLieuMoi"] = 0;
            if (string.IsNullOrEmpty(MaLuat))
            {
                ViewData["DuLieuMoi"] = 1;
            }
            ViewData["MaLuat"] = MaLuat;
            ViewData["smenu"] = 0;
            return View();
        }
        [Authorize]
        [ValidateInput(false), HttpPost]
        public ActionResult Edit(string ParentID, string MaLuat)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "PQ_Luat", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            String sTenLuat = CString.SafeString(Request.Form[ParentID + "_sTen"]).Trim();
            string DuLieuMoi = CString.SafeString(Request.Form[ParentID + "_DuLieuMoi"]);

            NameValueCollection arrLoi = new NameValueCollection();
            if (string.IsNullOrEmpty(sTenLuat))
            {
                arrLoi.Add("err_sTen", "Bạn chưa nhập tên luật!");
            }
            if (arrLoi.Count > 0)
            {
                for (int i = 0; i <= arrLoi.Count - 1; i++)
                {
                    ModelState.AddModelError(ParentID + "_" + arrLoi.GetKey(i), arrLoi[i]);
                }

                ViewData["MaLuat"] = MaLuat;
                ViewData["smenu"] = 0;
                ViewData["DuLieuMoi"] = 0;
                if (string.IsNullOrEmpty(MaLuat))
                {
                    ViewData["DuLieuMoi"] = 1;
                }
                return View();
            }

            try
            {
                Bang bang = new Bang("PQ_Luat");
                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                bang.TruyenGiaTri(ParentID, Request.Form);

                if (DuLieuMoi == "0")
                {
                    //Sua
                    bang.DuLieuMoi = false;
                    bang.GiaTriKhoa = MaLuat;
                }
                else
                {
                    //Them moi
                    bang.DuLieuMoi = true;
                }
                bang.Save();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Errors", new { sMess = ex.ToString() });
            }
        }
        [Authorize]
        public ActionResult Delete(string MaLuat)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "PQ_Luat", "Delete") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            try
            {
                if (string.IsNullOrEmpty(MaLuat) == false)
                {
                    SqlCommand cmd;

                    //Delete
                    cmd = new SqlCommand("DELETE FROM PQ_Luat WHERE iID_MaLuat=@iID_MaLuat");
                    cmd.Parameters.AddWithValue("@iID_MaLuat", CString.SafeString(MaLuat));
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