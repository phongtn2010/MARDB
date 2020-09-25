using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel;
using DomainModel.Abstract;
using DomainModel.Controls;
using DATA0200025;

namespace APP0200025.Controllers
{
    public class TinhController : Controller
    {
        // GET: Tinh
        public ActionResult Index()
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_Tinh", "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["smenu"] = 55;
            return View();
        }
        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Search(string ParentID)
        {
            string _MaMien = CString.SafeString(Request.Form[ParentID + "_MaMien"]);
            string _MaVung = CString.SafeString(Request.Form[ParentID + "_iID_MaVung"]);
            string _TieuDe = CString.SafeString(Request.Form[ParentID + "_TieuDe"]);
            string _FromDate = CString.SafeString(Request.Form[ParentID + "_viFromDate"]);
            string _ToDate = CString.SafeString(Request.Form[ParentID + "_viToDate"]);
            return RedirectToAction("Index", "Tinh", new { _MaMien = _MaMien, _MaVung = _MaVung, _TieuDe = _TieuDe, _FromDate = _FromDate, _ToDate = _ToDate });
        }
        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(String iID_MaTinh)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_Tinh", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["DuLieuMoi"] = "0";
            if (string.IsNullOrEmpty(iID_MaTinh))
            {
                ViewData["DuLieuMoi"] = "1";
            }
            ViewData["iID_MaTinh"] = CString.SafeString(iID_MaTinh);
            ViewData["smenu"] = 55;
            return View();
        }

        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult Edit(String ParentID, String MaTinh)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_Tinh", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            NameValueCollection values = new NameValueCollection();
            string iID_MaMien = CString.SafeString(Request.Form[ParentID + "_iID_MaMien"]);
            string iID_MaVung = CString.SafeString(Request.Form[ParentID + "_iID_MaVung"]);
            string iID_MaTinh = CString.SafeString(Request.Form[ParentID + "_iID_MaTinh"]);
            string _sTenTinh = CString.SafeString(Request.Form[ParentID + "_sTenTinh"]);
            string DuLieuMoi = CString.SafeString(Request.Form[ParentID + "_DuLieuMoi"]);

            if (string.IsNullOrEmpty(iID_MaMien))
            {
                values.Add("err_iID_MaMien", "Bạn chưa nhập mã miền!");
            }
            if (string.IsNullOrEmpty(iID_MaVung))
            {
                values.Add("err_iID_MaVung", "Bạn chưa nhập mã vùng!");
            }
            if (string.IsNullOrEmpty(iID_MaTinh))
            {
                values.Add("err_sMaTinh", "Bạn chưa nhập mã tỉnh!");
            }
            if (string.IsNullOrEmpty(_sTenTinh))
            {
                values.Add("err_sTenTinh", "Bạn chưa nhập tiêu đề!");
            }
            if (values.Count > 0)
            {
                for (int i = 0; i <= (values.Count - 1); i++)
                {
                    ModelState.AddModelError(ParentID + "_" + values.GetKey(i), values[i]);
                }
                base.ViewData["DuLieuMoi"] = "0";
                if (string.IsNullOrEmpty(iID_MaTinh))
                {
                    ViewData["DuLieuMoi"] = "1";
                }
                ViewData["iID_MaTinh"] = CString.SafeString(iID_MaTinh);
                ViewData["smenu"] = 55;
                return View();
            }

            try
            {
                Bang bang = new Bang("BC_Tinh");
                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                bang.TruyenGiaTri(ParentID, Request.Form);

                if (DuLieuMoi == "0")
                {
                    //Sua
                    bang.DuLieuMoi = false;
                    bang.GiaTriKhoa = MaTinh;
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
        public ActionResult Delete(string iID_MaTinh)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_Tinh", "Delete") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            try
            {
                if (string.IsNullOrEmpty(iID_MaTinh) == false)
                {
                    SqlCommand cmd;

                    //Xoa danh muc san pham
                    cmd = new SqlCommand("DELETE FROM BC_Tinh WHERE iID_MaTinh=@iID_MaTinh");
                    cmd.Parameters.AddWithValue("@iID_MaTinh", CString.SafeString(iID_MaTinh));
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
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_Tinh", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["smenu"] = 55;
            return View();
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Sort(String ParentID)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_Tinh", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
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
                        cmd.CommandText = "Update BC_Tinh SET iSTT=@iSTT WHERE iID_MaTinh=@iID_MaTinh";
                        cmd.Parameters.AddWithValue("@iSTT", i);
                        cmd.Parameters.AddWithValue("@iID_MaTinh", arrTG[i]);
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