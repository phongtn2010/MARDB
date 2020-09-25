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
using DATA0200026;

namespace APP0200026.Controllers.BangMau
{
    public class BangMauCotMauDonViTenMoiController : Controller
    {
        // GET: BangMauCotMauDonViTenMoi
        [Authorize]
        public ActionResult Index(String MaPhongBan, String MaBangMau)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_CotMau_DonVi_TenMoi", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["MaPhongBan"] = MaPhongBan;
            ViewData["MaBangMau"] = MaBangMau;
            return View();
        }

        [Authorize]
        public ActionResult Delete(String MaBangMau_CotMau_DonVi_TenMoi, String MaDonVi, String MaBangMau, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_CotMau_DonVi_TenMoi", "Delete") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            Bang bang = new Bang("BC_BangMau_CotMau_DonVi_TenMoi");
            bang.MaNguoiDungSua = User.Identity.Name;
            bang.IPSua = Request.UserHostAddress;
            bang.GiaTriKhoa = MaBangMau_CotMau_DonVi_TenMoi;
            bang.Delete();

            return RedirectToAction("Index", new { MaBangMau = MaBangMau, MaPhongBan = MaPhongBan });
        }

        public JavaScriptResult Edit_Fast_Submit(String ParentID)
        {
            String MaDonVi = "", MaBangMau = "", MaCotMau = "", MaCotMauDonViTenMoi = "";

            MaBangMau = Convert.ToString(Request.Form["txtMaBangMau"]);
            MaCotMau = Convert.ToString(Request.Form["txtMaCotMau"]);
            MaDonVi = Convert.ToString(Request.Form["txtMaDonVi"]);
            MaCotMauDonViTenMoi = Convert.ToString(Request.Form["txtMaBangMau_CotMau_DonVi_TenMoi"]);
            String MaDiv = "divTenMoi-" + MaDonVi + "-" + MaCotMau;

            String sTenCotMoi = Convert.ToString(Request.Form["txtTenCot"]);

            if (MaCotMau != "" && String.IsNullOrEmpty(sTenCotMoi) == false)
            {
                try
                {
                    Bang bang = new Bang("BC_BangMau_CotMau_DonVi_TenMoi");
                    bang.MaNguoiDungSua = User.Identity.Name;
                    bang.IPSua = Request.UserHostAddress;
                    if (MaCotMauDonViTenMoi != "" && MaCotMauDonViTenMoi != null)
                    {
                        bang.DuLieuMoi = false;
                        bang.GiaTriKhoa = MaCotMauDonViTenMoi;
                    }
                    else
                    {
                        bang.DuLieuMoi = true;
                    }
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaCotMau", MaCotMau);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                    bang.CmdParams.Parameters.AddWithValue("@sTenCot", sTenCotMoi);
                    bang.Save();

                    String strJ = "";
                    strJ = String.Format("HideDialog('{0}');CallSuccess_CT('{1}','{2}');", ParentID, sTenCotMoi, MaDiv);
                    return JavaScript(strJ);
                }
                catch(Exception ex)
                {
                    return null;
                }
            }
            else
            {
                String strJ = "";
                strJ = "CallValide_CT();";
                return JavaScript(strJ);
            }    
            return null;
        }

        public ActionResult Get_GiaTri_TenMoi_DonVi(string MaBangMau, string MaCotMau, string MaDonVi)
        {
            String sTenCot = "";
            SqlCommand cmd;
            DataTable dt = null;
            if (String.IsNullOrEmpty(MaBangMau) == false && String.IsNullOrEmpty(MaCotMau) == false && String.IsNullOrEmpty(MaDonVi) == false)
            {
                cmd = new SqlCommand("SELECT * FROM BC_BangMau_CotMau_DonVi_TenMoi WHERE iID_MaBangMau=@iID_MaBangMau AND iID_MaCotMau=@iID_MaCotMau AND iID_MaDonVi=@iID_MaDonVi");
                cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                cmd.Parameters.AddWithValue("@iID_MaCotMau", MaCotMau);
                cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                dt = Connection.GetDataTable(cmd);
                cmd.Dispose();
                if (dt.Rows.Count > 0)
                {
                    sTenCot = dt.Rows[0]["sTenCot"].ToString();
                }
                dt.Dispose();
            }
            return Json(string.Concat(new object[] { sTenCot }), JsonRequestBehavior.AllowGet);
        }
    }
}