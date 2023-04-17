using DATA0200025;
using DomainModel.Abstract;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APP0200025.Controllers
{
    public class TCCDController : Controller
    {
        // GET: TCCD
        //[Authorize]
        public ActionResult Index()
        {
            //if (BaoMat.ChoPhepLamViec(User.Identity.Name, "CNN25_ToChucChiDinh", "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            //{
            //    return RedirectToAction("Index", "PermitionMessage");
            //}
            ViewData["smenu"] = 55;
            return View();
        }
        //[Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(String iID_MaDTCCD)
        {
            //if (BaoMat.ChoPhepLamViec(User.Identity.Name, "CNN25_ToChucChiDinh", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            //{
            //    return RedirectToAction("Index", "PermitionMessage");
            //}
            ViewData["DuLieuMoi"] = "0";
            if (string.IsNullOrEmpty(iID_MaDTCCD))
            {
                ViewData["DuLieuMoi"] = "1";
            }
            ViewData["iID_MaDTCCD"] = CString.SafeString(iID_MaDTCCD);
            ViewData["smenu"] = 55;
            return View();
        }

        [ValidateInput(false), HttpPost]
        public ActionResult Edit(String ParentID, String iID_MaDTCCD)
        {
            //if (BaoMat.ChoPhepLamViec(User.Identity.Name, "CNN25_ToChucChiDinh", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            //{
            //    return RedirectToAction("Index", "PermitionMessage");
            //}
            NameValueCollection values = new NameValueCollection();
            string sMaTCCD = CString.SafeString(Request.Form[ParentID + "_sMaTCCD"]);
            string sTen = CString.SafeString(Request.Form[ParentID + "_sTen"]);
            string sDiaChi = CString.SafeString(Request.Form[ParentID + "_sDiaChi"]);
            string sQuyetDinh = CString.SafeString(Request.Form[ParentID + "_sQuyetDinh"]);
            string sNgayCap = CString.SafeString(Request.Form[ParentID + "_sNgayCap"]);
            string sNgayHetHan = CString.SafeString(Request.Form[ParentID + "_viNgayHetHan"]);
            string DuLieuMoi = CString.SafeString(Request.Form[ParentID + "_DuLieuMoi"]);

            if (string.IsNullOrEmpty(sMaTCCD))
            {
                values.Add("err_sMaTCCD", "Bạn chưa nhập mã tccd!");
            }
            if (string.IsNullOrEmpty(sTen))
            {
                values.Add("err_sTen", "Bạn chưa nhập tên tccd!");
            }
            if (string.IsNullOrEmpty(sDiaChi))
            {
                values.Add("err_sDiaChi", "Bạn chưa nhập địa chỉ!");
            }
            if (string.IsNullOrEmpty(sQuyetDinh))
            {
                values.Add("err_sQuyetDinh", "Bạn chưa nhập quyết định!");
            }
            if (string.IsNullOrEmpty(sNgayCap))
            {
                values.Add("err_sNgayCap", "Bạn chưa nhập ngày cấp!");
            }
            if (string.IsNullOrEmpty(sNgayHetHan))
            {
                values.Add("err_sNgayHetHan", "Bạn chưa nhập ngày hết hạn!");
            }

            if (values.Count > 0)
            {
                for (int i = 0; i <= (values.Count - 1); i++)
                {
                    ModelState.AddModelError(ParentID + "_" + values.GetKey(i), values[i]);
                }
                base.ViewData["DuLieuMoi"] = "0";
                if (string.IsNullOrEmpty(iID_MaDTCCD))
                {
                    ViewData["DuLieuMoi"] = "1";
                }
                ViewData["iID_MaDTCCD"] = CString.SafeString(iID_MaDTCCD);
                ViewData["smenu"] = 55;
                return View();
            }

            try
            {

                DateTime dNgayHetHan = DateTime.Now;
                if (String.IsNullOrEmpty(sNgayHetHan) == false)
                {
                    DateTime _NgayTao = Convert.ToDateTime(CommonFunction.LayNgayTuXau(sNgayHetHan));
                    dNgayHetHan = _NgayTao;
                }

                Bang bang = new Bang("CNN25_ToChucChiDinh");
                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                bang.TruyenGiaTri(ParentID, Request.Form);
                bang.CmdParams.Parameters.AddWithValue("@dNgayHetHan", dNgayHetHan);

                if (DuLieuMoi == "0")
                {
                    //Sua
                    bang.DuLieuMoi = false;
                    bang.GiaTriKhoa = iID_MaDTCCD;
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
        public ActionResult Delete(string iID_MaDTCCD)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "CNN25_ToChucChiDinh", "Delete") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            try
            {
                if (string.IsNullOrEmpty(iID_MaDTCCD) == false)
                {
                    SqlCommand cmd;

                    //Xoa danh muc san pham
                    cmd = new SqlCommand("DELETE FROM CNN25_ToChucChiDinh WHERE iID_MaDTCCD=@iID_MaDTCCD");
                    cmd.Parameters.AddWithValue("@iID_MaDTCCD", CString.SafeString(iID_MaDTCCD));
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