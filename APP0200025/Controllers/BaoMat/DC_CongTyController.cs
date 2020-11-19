using DATA0200025;
using DomainModel;
using DomainModel.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APP0200025.Controllers
{
    public class DC_CongTyController : Controller
    {
        // GET: DC_CongTy
        [Authorize]
        public ActionResult Index()
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "DC_CongTy", "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["smenu"] = 0;

            return View();
        }
        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(String iID_MaCongTy)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "DC_CongTy", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["DuLieuMoi"] = "0";
            if (string.IsNullOrEmpty(iID_MaCongTy))
            {
                ViewData["DuLieuMoi"] = "1";
            }
            ViewData["iID_MaCongTy"] = CString.SafeString(iID_MaCongTy);
            ViewData["smenu"] = 0;

            return View();
        }

        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult Edit()
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "DC_CongTy", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            NameValueCollection values = new NameValueCollection();
            string ParentID = "Edit";
            string iID_MaCongTy = CString.SafeString(Request.Form[ParentID + "_iID_MaCongTy"]);
            string sTenCongTy = CString.SafeString(Request.Form[ParentID + "_sTenCongTy"]);
            string DuLieuMoi = CString.SafeString(Request.Form[ParentID + "_DuLieuMoi"]);
            String ctlFNName = ParentID + "_File";
            String ctlFNDau = ParentID + "_FileDau";

            if (string.IsNullOrEmpty(sTenCongTy))
            {
                values.Add("err_sTen", "Bạn chưa nhập tên công ty!");
            }
            if (values.Count > 0)
            {
                for (int i = 0; i <= (values.Count - 1); i++)
                {
                    ModelState.AddModelError(ParentID + "_" + values.GetKey(i), values[i]);
                }
                base.ViewData["DuLieuMoi"] = "0";
                if (string.IsNullOrEmpty(iID_MaCongTy))
                {
                    ViewData["DuLieuMoi"] = "1";
                }
                ViewData["iID_MaCongTy"] = CString.SafeString(iID_MaCongTy);
                ViewData["smenu"] = 0;

                return View();
            }
            else
            {
                Bang bang = new Bang("DC_CongTy");
                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                bang.TruyenGiaTri(ParentID, Request.Form);

                HttpPostedFileBase hpf = Request.Files[ctlFNName] as HttpPostedFileBase;
                if (hpf != null && hpf.ContentLength > 0)
                {
                    String FileName = hpf.FileName;
                    var filename = Path.GetFileName(hpf.FileName);
                    var path = Path.Combine(Server.MapPath("~/Uploads/Profile"), filename);
                    hpf.SaveAs(path);

                    byte[] sLogo = CommonFunction.ConvertToBytes(hpf);

                    bang.CmdParams.Parameters.AddWithValue("@sLogo", sLogo);
                }

                HttpPostedFileBase hpfDau = Request.Files[ctlFNDau] as HttpPostedFileBase;
                if (hpfDau != null && hpfDau.ContentLength > 0)
                {
                    var filename = Path.GetFileName(hpfDau.FileName);
                    var path = Path.Combine(Server.MapPath("~/Uploads/Profile"), filename);
                    hpfDau.SaveAs(path);
                    byte[] sDau = CommonFunction.ConvertToBytes(hpfDau);

                    bang.CmdParams.Parameters.AddWithValue("@sDau", sDau);
                }

                if (DuLieuMoi == "0")
                {
                    //Sua
                    bang.DuLieuMoi = false;
                    bang.GiaTriKhoa = iID_MaCongTy;
                }
                else
                {
                    //Them moi
                    bang.DuLieuMoi = true;
                }
                bang.Save();
            }
            return base.RedirectToAction("Index");
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Delete(string iID_MaCongTy)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "DC_CongTy", "Delete") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            SqlCommand cmd;

            //Xoa danh muc san pham
            cmd = new SqlCommand("DELETE FROM DC_CongTy WHERE iID_MaCongTy=@iID_MaCongTy");
            cmd.Parameters.AddWithValue("@iID_MaCongTy", CString.SafeString(iID_MaCongTy));
            Connection.UpdateDatabase(cmd);
            cmd.Dispose();

            return RedirectToAction("Index");
        }
    }
}