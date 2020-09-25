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
    public class BangMauDinhDangController : Controller
    {
        // GET: BangMauDinhDang
        public ActionResult Index(String MaBangMau, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_DinhDang", "Index") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["MaBangMau"] = MaBangMau;
            ViewData["MaPhongBan"] = MaPhongBan;
            return View();
        }

        //
        // GET: /BangMauDinhDang/Create
        public ActionResult Create(String MaPhongBan, String MaBangMau, String MaHangMau, String MaCotMau)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_DinhDang", "Index") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["MaPhongBan"] = MaPhongBan;
            ViewData["MaBangMau"] = MaBangMau;
            ViewData["MaHangMau"] = MaHangMau;
            ViewData["MaCotMau"] = MaCotMau;
            return View();
        }

        //
        // POST: /BangMauDinhDang/CreateSubmit
        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateSubmit(String ParentID, String MaPhongBan, String MaBangMau, String MaHangMau, String MaCotMau)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_DinhDang", "Index") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            String iKieuDuLieu = Convert.ToString(Request.Form[ParentID + "_iKieuDuLieu"]);
            String iSoSauDauPhay = Convert.ToString(Request.Form[ParentID + "_iSoSauDauPhay"]);
            String sBackGround = Convert.ToString(Request.Form[ParentID + "_sBackGround"]);
            String sColor = Convert.ToString(Request.Form[ParentID + "_sColor"]);
            String iFontSize = Convert.ToString(Request.Form[ParentID + "_iFontSize"]);
            String bBold = Convert.ToString(Request.Form[ParentID + "_bBold"]);
            String bItalic = Convert.ToString(Request.Form[ParentID + "_bItalic"]);
            String bUnderline = Convert.ToString(Request.Form[ParentID + "_bUnderline"]);
            int iAlignment = Convert.ToInt32(Request.Form[ParentID + "_iAlignment"]);


            SqlCommand cmd;

            cmd = new SqlCommand();
            cmd.CommandText = "DELETE FROM BC_BangMau_DinhDang WHERE iID_MaBangMau=@iID_MaBangMau AND iID_MaHangMau=@iID_MaHangMau AND iID_MaCotMau=@iID_MaCotMau";
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
            cmd.Parameters.AddWithValue("@iID_MaCotMau", MaCotMau);
            Connection.UpdateDatabase(cmd);
            cmd.Dispose();


            cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO BC_BangMau_DinhDang(iID_MaBangMau ,iID_MaHangMau ,iID_MaCotMau ,iKieuDuLieu, iSoSauDauPhay, sBackGround, sColor, iFontSize, bBold, bItalic, bUnderline, iAlignment) " +
                              "VALUES(@iID_MaBangMau,@iID_MaHangMau,@iID_MaCotMau,@iKieuDuLieu,@iSoSauDauPhay,@sBackGround,@sColor,@iFontSize,@bBold,@bItalic,@bUnderline,@iAlignment)";
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
            cmd.Parameters.AddWithValue("@iID_MaCotMau", MaCotMau);
            if (iKieuDuLieu == "-1")
            {
                cmd.Parameters.AddWithValue("@iKieuDuLieu", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@iKieuDuLieu", iKieuDuLieu);
            }
            if (iSoSauDauPhay == "" || iSoSauDauPhay == null)
            {
                cmd.Parameters.AddWithValue("@iSoSauDauPhay", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@iSoSauDauPhay", iSoSauDauPhay);
            }
            if (sBackGround == "")
            {
                cmd.Parameters.AddWithValue("@sBackGround", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@sBackGround", sBackGround);
            }
            if (sColor == "")
            {
                cmd.Parameters.AddWithValue("@sColor", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@sColor", sColor);
            }
            if (iFontSize == "")
            {
                cmd.Parameters.AddWithValue("@iFontSize", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@iFontSize", iFontSize);
            }
            if (bBold == "-1")
            {
                cmd.Parameters.AddWithValue("@bBold", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@bBold", bBold);
            }
            if (bItalic == "-1")
            {
                cmd.Parameters.AddWithValue("@bItalic", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@bItalic", bItalic);
            }
            if (bUnderline == "-1")
            {
                cmd.Parameters.AddWithValue("@bUnderline", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@bUnderline", bUnderline);
            }
            if (iAlignment == -1)
            {
                cmd.Parameters.AddWithValue("@iAlignment", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@iAlignment", iAlignment);
            }

            Connection.UpdateDatabase(cmd);
            cmd.Dispose();

            return RedirectToAction("Index", new { MaBangMau = MaBangMau, MaPhongBan = MaPhongBan });
        }
    }
}