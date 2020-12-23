using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using DomainModel;
using DomainModel.Controls;
using DomainModel.Abstract;
using System.Collections.Specialized;
using DATA0200025;
using APP0200025.Models;
using System.Drawing;
using System.Drawing.Imaging;

namespace APP0200025.Controllers
{
    public class PublicController : Controller
    {
        // GET: Public
        public JsonResult get_dtCotMau(String ParentID, String MaBangMau)
        {
            return Json(new { data = get_objCotMau(ParentID, MaBangMau, "") }, JsonRequestBehavior.AllowGet);
        }

        public static string get_objCotMau(String ParentID, String MaBangMau, String MaCotMau)
        {
            DataTable dt;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT *, sTenNhomCon+' '+sTenCot AS sTen " +
                        "FROM BC_BangMau_CotMau " +
                        "WHERE iID_MaBangMau=@iID_MaBangMau ORDER BY iSTT;";
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            dt = Connection.GetDataTable(cmd);
            dt.Rows.InsertAt(dt.NewRow(), 0);
            dt.Rows[0]["iID_MaCotMau"] = -1;
            dt.Rows[0]["sTen"] = "-- Chọn cột lấy dữ liệu --";

            SelectOptionList slCotMau = new SelectOptionList(dt, "iID_MaCotMau", "sTen");

            return MyHtmlHelper.DropDownList(ParentID, slCotMau, MaCotMau, "iID_MaCotMau_DuLieu", null, " class=\"form-control\"");
        }

        public ActionResult generateCaptcha()
        {
            String ImageServerPath = Server.MapPath("~/");
            FontFamily fontFamily = new FontFamily("Arial");
            CaptchaImage image = new CaptchaImage(180, 50, fontFamily);
            //string text = image.CreateRandomText(4) + " " + image.CreateRandomText(3);
            string text = image.CreateRandomText(4);
            image.SetText(text);
            image.GenerateImage();
            image.Image.Save(ImageServerPath + "Uploads/CapCha/" + base.Session.SessionID.ToString() + ".png", ImageFormat.Png);
            Session["captchaText"] = text;
            return Json(string.Concat(new object[] { "Uploads/CapCha/", base.Session.SessionID.ToString(), ".png?t=", DateTime.Now.Ticks }), JsonRequestBehavior.AllowGet);
        }
    }
}