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
    public class BangMauGiaTriMacDinhController : Controller
    {
        // GET: BangMauGiaTriMacDinh
        [Authorize]
        public ActionResult Index(String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_GiaTriMacDinh", "Index") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["MaPhongBan"] = MaPhongBan;
            return View();
        }

        [Authorize]
        public ActionResult Create(String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_GiaTriMacDinh", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["MaPhongBan"] = MaPhongBan;
            return View();
        }

        [Authorize]
        public ActionResult Edit(String MaDonVi, String MaBangMau, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_GiaTriMacDinh", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["MaDonVi"] = MaDonVi;
            ViewData["MaBangMau"] = MaBangMau;
            ViewData["MaPhongBan"] = MaPhongBan;
            return View();
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditSubmit(String ParentID, String MaDonVi, String MaBangMau, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_GiaTriMacDinh", "Index") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            SqlCommand cmd;
            cmd = new SqlCommand("SELECT bXemTheoDonVi FROM BC_BangMau WHERE iID_MaBangMau=@iID_MaBangMau");
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            Boolean XemTheoDonVi = Convert.ToBoolean(Connection.GetValue(cmd, ""));
            cmd.Dispose();


            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * " +
                              "FROM BC_BangMau_CotMau " +
                              "WHERE iID_MaBangMau=@iID_MaBangMau ORDER BY iSTT;";
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            DataTable dtCotMau = Connection.GetDataTable(cmd);
            cmd.Dispose();

            cmd = new SqlCommand("SELECT * FROM BC_BangMau_HangMau WHERE iID_MaBangMau=@iID_MaBangMau");
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            DataTable dtHangMau = Connection.GetDataTable(cmd);
            cmd.Dispose();

            cmd = new SqlCommand();
            cmd.CommandText = "DELETE " +
                              "FROM BC_BangMau_GiaTriMacDinh " +
                              "WHERE iID_MaBangMau=@iID_MaBangMau AND iID_MaDonVi=@iID_MaDonVi";
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
            Connection.UpdateDatabase(cmd);


            int h, c;
            string tg, MaHangMau, MaCotMau, value;
            tg = Request.Form["idXauMaCacHang"];
            String[] arrMaHang = tg.Split(',');
            tg = Request.Form["idXauMaCacCot"];
            String[] arrMaCot = tg.Split(',');
            tg = Request.Form["idXauGiaTriChiTiet"];
            List<List<String>> arrGiaTri = new List<List<String>>();
            String[] arrGiaTriHang = tg.Split(new string[] { BangModels.DauCachHang }, StringSplitOptions.None);
            for (h = 0; h < arrMaHang.Length; h++)
            {
                String[] arrGiaTriO = arrGiaTriHang[h].Split(new string[] { BangModels.DauCachO }, StringSplitOptions.None);
                MaHangMau = arrMaHang[h];
                for (c = 0; c < arrMaCot.Length; c++)
                {
                    value = arrGiaTriO[c];
                    if (value == "0") value = "";
                    MaCotMau = arrMaCot[c];
                    if (String.IsNullOrEmpty(value) == false)
                    {
                        cmd = new SqlCommand();
                        cmd.CommandText = "INSERT INTO BC_BangMau_GiaTriMacDinh(iID_MaBangMau ,iID_MaHangMau ,iID_MaCotMau, iID_MaDonVi ,oGiaTri) " +
                                            "VALUES(@iID_MaBangMau,@iID_MaHangMau,@iID_MaCotMau,@iID_MaDonVi,@oGiaTri)";
                        cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                        cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
                        cmd.Parameters.AddWithValue("@iID_MaCotMau", MaCotMau);
                        cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                        cmd.Parameters.AddWithValue("@oGiaTri", value);
                        Connection.UpdateDatabase(cmd);
                        cmd.Dispose();
                    }
                }
            }

            return RedirectToAction("Edit", new { MaBangMau = MaBangMau, MaDonVi = MaDonVi, MaPhongBan = MaPhongBan });
        }
    }
}