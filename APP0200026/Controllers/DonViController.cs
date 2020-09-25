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
    public class DonViController : Controller
    {
        // GET: DonVi
        public ActionResult Index()
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_DonVi", "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["smenu"] = 187;
            return View();
        }
        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(String iID_MaDonVi)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_DonVi", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["DuLieuMoi"] = "0";
            if (string.IsNullOrEmpty(iID_MaDonVi))
            {
                ViewData["DuLieuMoi"] = "1";
            }
            ViewData["iID_MaDonVi"] = CString.SafeString(iID_MaDonVi);
            ViewData["smenu"] = 187;
            return View();
        }

        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult Edit(String ParentID, String iID_MaDonVi)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_DonVi", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            NameValueCollection values = new NameValueCollection();
            string sTen = CString.SafeString(Request.Form[ParentID + "_sTen"]);
            string DuLieuMoi = CString.SafeString(Request.Form[ParentID + "_DuLieuMoi"]);

            if (string.IsNullOrEmpty(sTen))
            {
                values.Add("err_sTen", "Bạn chưa nhập tiêu đề!");
            }
            if (values.Count > 0)
            {
                for (int i = 0; i <= (values.Count - 1); i++)
                {
                    ModelState.AddModelError(ParentID + "_" + values.GetKey(i), values[i]);
                }
                base.ViewData["DuLieuMoi"] = "0";
                if (string.IsNullOrEmpty(iID_MaDonVi))
                {
                    ViewData["DuLieuMoi"] = "1";
                }
                ViewData["iID_MaDonVi"] = CString.SafeString(iID_MaDonVi);
                ViewData["smenu"] = 187;
                return View();
            }

            try
            {
                Bang bang = new Bang("BC_DonVi");
                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                bang.TruyenGiaTri(ParentID, Request.Form);

                if (DuLieuMoi == "0")
                {
                    //Sua
                    bang.DuLieuMoi = false;
                    bang.GiaTriKhoa = iID_MaDonVi;
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
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Delete(string iID_MaDonVi)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_DonVi", "Delete") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            try
            {
                if (string.IsNullOrEmpty(iID_MaDonVi) == false)
                {
                    SqlCommand cmd;

                    //Xoa danh muc san pham
                    cmd = new SqlCommand("DELETE FROM BC_DonVi WHERE iID_MaDonVi=@iID_MaDonVi");
                    cmd.Parameters.AddWithValue("@iID_MaDonVi", CString.SafeString(iID_MaDonVi));
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

        [Authorize]
        public ActionResult Sort()
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_DonVi", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["smenu"] = 187;
            return View();
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Sort(String ParentID)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_DonVi", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            string strOrder = Request.Form["hiddenOrder"].ToString();
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
                        cmd.CommandText = "Update BC_DonVi SET iSTT=@iSTT WHERE iID_MaDonVi=@iID_MaDonVi";
                        cmd.Parameters.AddWithValue("@iSTT", i);
                        cmd.Parameters.AddWithValue("@iID_MaDonVi", arrTG[i]);
                        Connection.UpdateDatabase(cmd);
                        cmd.Dispose();
                    }
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