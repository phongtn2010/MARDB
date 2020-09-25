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
    public class VungController : Controller
    {
        // GET: Vung
        public ActionResult Index()
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_Vung", "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
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
            string _TieuDe = CString.SafeString(Request.Form[ParentID + "_TieuDe"]);
            string _FromDate = CString.SafeString(Request.Form[ParentID + "_viFromDate"]);
            string _ToDate = CString.SafeString(Request.Form[ParentID + "_viToDate"]);
            return RedirectToAction("Index", "Vung", new { _MaMien = _MaMien, _TieuDe= _TieuDe, _FromDate = _FromDate, _ToDate = _ToDate });
        }
        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(String iID_MaVung)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_Vung", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["DuLieuMoi"] = "0";
            if (string.IsNullOrEmpty(iID_MaVung))
            {
                ViewData["DuLieuMoi"] = "1";
            }
            ViewData["iID_MaVung"] = CString.SafeString(iID_MaVung);
            ViewData["smenu"] = 55;
            return View();
        }

        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult Edit(String ParentID, String iID_MaVung)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_Vung", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            NameValueCollection values = new NameValueCollection();
            string iID_MaMien = CString.SafeString(Request.Form[ParentID + "_iID_MaMien"]);
            string _sTenVung = CString.SafeString(Request.Form[ParentID + "_sTenVung"]);
            string DuLieuMoi = CString.SafeString(Request.Form[ParentID + "_DuLieuMoi"]);

            if (string.IsNullOrEmpty(iID_MaMien))
            {
                values.Add("err_iID_MaMien", "Bạn chưa nhập mã miền!");
            }
            if (string.IsNullOrEmpty(_sTenVung))
            {
                values.Add("err_sTenVung", "Bạn chưa nhập tiêu đề!");
            }
            if (values.Count > 0)
            {
                for (int i = 0; i <= (values.Count - 1); i++)
                {
                    ModelState.AddModelError(ParentID + "_" + values.GetKey(i), values[i]);
                }
                base.ViewData["DuLieuMoi"] = "0";
                if (string.IsNullOrEmpty(iID_MaVung))
                {
                    ViewData["DuLieuMoi"] = "1";
                }
                ViewData["iID_MaVung"] = CString.SafeString(iID_MaVung);
                ViewData["smenu"] = 55;
                return View();
            }

            try
            {
                Bang bang = new Bang("BC_Vung");
                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                bang.TruyenGiaTri(ParentID, Request.Form);

                if (DuLieuMoi == "0")
                {
                    //Sua
                    bang.DuLieuMoi = false;
                    bang.GiaTriKhoa = iID_MaVung;
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
        public ActionResult Delete(string iID_MaVung)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_Vung", "Delete") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            try
            {
                if (string.IsNullOrEmpty(iID_MaVung) == false)
                {
                    SqlCommand cmd;

                    //Xoa danh muc san pham
                    cmd = new SqlCommand("DELETE FROM BC_Vung WHERE iID_MaVung=@iID_MaVung");
                    cmd.Parameters.AddWithValue("@iID_MaVung", CString.SafeString(iID_MaVung));
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
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_Vung", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
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
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_Vung", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
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
                        cmd.CommandText = "Update BC_Vung SET iSTT=@iSTT WHERE iID_MaVung=@iID_MaVung";
                        cmd.Parameters.AddWithValue("@iSTT", i);
                        cmd.Parameters.AddWithValue("@iID_MaVung", arrTG[i]);
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

        public JsonResult getAjax(String ParentID, String MaMien, String MaVung)
        {
            return Json(getObj(ParentID, MaMien, MaVung), JsonRequestBehavior.AllowGet);
        }

        public static String getObj(String ParentID, String MaMien, String MaVung)
        {
            String vR = string.Empty;
            DataTable dt = CVung.Get_Table(MaMien, "", "", "");
            dt.Rows.InsertAt(dt.NewRow(), 0);
            dt.Rows[0]["iID_MaVung"] = -1;
            dt.Rows[0]["sTenVung"] = "--- Vùng ---";
            SelectOptionList sl = new SelectOptionList(dt, "iID_MaVung", "sTenVung");
            vR = MyHtmlHelper.DropDownList(ParentID, sl, MaVung, "iID_MaVung", "", "class=\"form-control\"");
            dt.Dispose();
            return vR;
        }
    }
}