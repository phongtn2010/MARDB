using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DomainModel;
using DomainModel.Abstract;
using DATA0200025;

namespace APP0200025.Controllers.BangMau
{
    public class CotMauController : Controller
    {
        // GET: CotMau
        [Authorize]
        public ActionResult Edit(String MaBangMau, String MaPhong)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["MaBangMau"] = MaBangMau;
            ViewData["MaPhong"] = MaPhong;
            return View();
        }

        [Authorize]
        public ActionResult Create(String MaBangMau, int ThuTu, String MaPhong)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau", "Create") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            SqlCommand cmd = new SqlCommand("UPDATE BC_BangMau_CotMau SET iSTT=iSTT+1 WHERE iID_MaBangMau =@iID_MaBangMau AND iSTT>=@iSTT");
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            cmd.Parameters.AddWithValue("@iSTT", ThuTu);
            Connection.UpdateDatabase(cmd);
            Bang bang = new Bang("BC_BangMau_CotMau");
            bang.MaNguoiDungSua = User.Identity.Name;
            bang.IPSua = Request.UserHostAddress;
            bang.DuLieuMoi = true;
            bang.CmdParams.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            bang.CmdParams.Parameters.AddWithValue("@iSTT", ThuTu);
            bang.CmdParams.Parameters.AddWithValue("@sTenCot", " ");
            bang.Save();
            return RedirectToAction("Edit", new { MaBangMau = MaBangMau, MaPhong = MaPhong });
        }

        [Authorize]
        public ActionResult Delete(String MaBangMau, String MaCotMau, String MaPhong)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau", "Delete") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            Bang bang = new Bang("BC_BangMau_CotMau");
            bang.MaNguoiDungSua = User.Identity.Name;
            bang.IPSua = Request.UserHostAddress;
            bang.GiaTriKhoa = MaCotMau;
            bang.Delete();
            return RedirectToAction("Edit", new { MaBangMau = MaBangMau, MaPhong = MaPhong });
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditSubmit(String MaBangMau, String MaPhong)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            SqlCommand cmd;

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * " +
                              "FROM BC_BangMau_CotMau " +
                              "WHERE iID_MaBangMau=@iID_MaBangMau ORDER BY iSTT;";
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            DataTable dtCot = Connection.GetDataTable(cmd);
            cmd.Dispose();

            int j;
            string MaCotMau;
            for (j = 0; j <= dtCot.Rows.Count - 1; j++)
            {
                MaCotMau = String.Format("{0}", dtCot.Rows[j]["iID_MaCotMau"]);
                Bang bang = new Bang("BC_BangMau_CotMau");
                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                bang.TruyenGiaTri(MaCotMau, Request.Form);
                bang.DuLieuMoi = false;
                bang.GiaTriKhoa = MaCotMau;
                bang.CmdParams.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                bang.CmdParams.Parameters.AddWithValue("@iSTT", j + 1);
                if (bang.CmdParams.Parameters.IndexOf("@sLoaiNguoiDuocSua") >= 0)
                {
                    bang.CmdParams.Parameters["@sLoaiNguoiDuocSua"].Value = String.Format(",{0},", bang.CmdParams.Parameters["@sLoaiNguoiDuocSua"].Value);
                }
                bang.Save();
            }
            return RedirectToAction("Edit", "CotMau", new { MaBangMau = MaBangMau, MaPhong = MaPhong });
        }

        public ActionResult Edit_Fast_Submit(String ParentID, String MaBangMau, String MaPhong)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau", "Create") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            String sThuTu = Convert.ToString(Request.Form["txtThuTuDaCo"]);
            String strSoCot = CString.SafeString(Convert.ToString(Request.Form[ParentID + "_sSoCot"]));

            int i;
            int SoCot = 0, ThuTu = 0;

            if (String.IsNullOrEmpty(sThuTu) == false)
            {
                ThuTu = Convert.ToInt32(sThuTu);
            }
            if (String.IsNullOrEmpty(strSoCot) == false)
            {
                SoCot = Convert.ToInt32(strSoCot);
            }
            if (SoCot > 0)
            {
                for (i = 1; i <= SoCot; i++)
                {
                    Bang bang = new Bang("BC_BangMau_CotMau");
                    bang.MaNguoiDungSua = User.Identity.Name;
                    bang.IPSua = Request.UserHostAddress;
                    bang.DuLieuMoi = true;
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                    bang.CmdParams.Parameters.AddWithValue("@iSTT", ThuTu + i);
                    bang.CmdParams.Parameters.AddWithValue("@sTenCot", " ");
                    bang.Save();
                }
            }

            String strJ = "";
            strJ = String.Format("HideDialog('{0}');CallSuccess_CT();", ParentID);
            return JavaScript(strJ);
        }
    }
}