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
    public class BangMauHamMauController : Controller
    {
        // GET: BangMauHamMau
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(String MaBangMau, String MaHamMau)
        {
            ViewData["DuLieuMoi"] = "0";
            if (String.IsNullOrEmpty(MaHamMau))
            {
                //MaDonVi = Globals.getNewGuid().ToString();
                ViewData["DuLieuMoi"] = "1";
            }
            ViewData["iID_MaHamMau"] = MaHamMau;
            ViewData["iID_MaBangMau"] = MaBangMau;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditSubmit(String ParentID, String MaBangMau, String MaHamMau)
        {
            Bang bang = new Bang("BC_BangMau_HamMau");
            bang.MaNguoiDungSua = User.Identity.Name;
            bang.IPSua = Request.UserHostAddress;
            String NoiDung = Request.Form["ctl00$MainContent$fcknoidung"];
            bang.TruyenGiaTri(ParentID, Request.Form);
            bang.CmdParams.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            bang.CmdParams.Parameters.AddWithValue("@sNoiDung", Server.HtmlDecode(NoiDung));
            bang.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(String MaHamMau)
        {
            Bang bang = new Bang("BC_BangMau_HamMau");
            bang.MaNguoiDungSua = User.Identity.Name;
            bang.IPSua = Request.UserHostAddress;
            bang.GiaTriKhoa = MaHamMau;
            bang.Delete();
            return RedirectToAction("Index");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Sort(String MaBangMau)
        {
            ViewData["iID_MaBangMau"] = MaBangMau;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SortSubmit(String MaBangMau)
        {
            string strOrder = Request.Form["hiddenOrder"].ToString();
            String[] arrTG = strOrder.Split('$');
            int i;
            for (i = 0; i < arrTG.Length - 1; i++)
            {
                Bang bang = new Bang("BC_BangMau_HamMau");
                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                bang.GiaTriKhoa = arrTG[i];
                bang.DuLieuMoi = false;
                bang.CmdParams.Parameters.AddWithValue("@iSTT", i);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                bang.Save();
            }
            return RedirectToAction("Index");
        }
    }
}