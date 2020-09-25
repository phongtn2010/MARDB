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
    public class BangMauHangMauDonViController : Controller
    {
        // GET: BangMauHangMauDonVi
        [Authorize]
        public ActionResult Index(String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_HangMau_DonVi", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["MaPhongBan"] = MaPhongBan;
            return View();
        }
        [Authorize]
        public ActionResult AddNews(String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_HangMau_DonVi", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            return View();
        }

        [Authorize]
        public ActionResult Create(String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_HangMau_DonVi", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["MaPhongBan"] = MaPhongBan;
            return View();
        }

        [Authorize]
        public ActionResult Detail(String MaDonVi, String MaBangMau, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_HangMau_DonVi", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["MaDonVi"] = MaDonVi;
            ViewData["MaBangMau"] = MaBangMau;
            return View();
        }

        [Authorize]
        public ActionResult Edit(String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_HangMau_DonVi", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            return View();
        }

        [Authorize]
        public ActionResult Sort(String MaDonVi, String MaBangMau, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_HangMau_DonVi", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            BangMauHangMauModels.CapNhapSTT_BangMau_HangMau_DonVi(MaBangMau, MaDonVi);
            return RedirectToAction("Detail", new { MaDonVi = MaDonVi, MaBangMau = MaBangMau, MaPhongBan = MaPhongBan });
        }

        [Authorize]
        public ActionResult SortAllDonVi(String MaDonVi, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_HangMau_DonVi", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            int i;
            SqlCommand cmd;
            DataTable dt;
            if (String.IsNullOrEmpty(MaDonVi) == false)
            {
                cmd = new SqlCommand("SELECT * FROM BC_BangMau WHERE bXemTheoDonVi = 1 AND iID_MaPhongBan = @iID_MaPhongBan ORDER BY iSTT");
                cmd.Parameters.AddWithValue("@iID_MaPhongBan", MaPhongBan);
                dt = Connection.GetDataTable(cmd);
            }
            else
            {
                cmd = new SqlCommand("SELECT * FROM BC_BangMau WHERE bXemTheoDonVi = 0 AND iID_MaPhongBan = @iID_MaPhongBan ORDER BY iSTT");
                cmd.Parameters.AddWithValue("@iID_MaPhongBan", MaPhongBan);
                dt = Connection.GetDataTable(cmd);
            }
            cmd.Dispose();

            String MaBangMau;
            for (i = 0; i < dt.Rows.Count; i++)
            {
                MaBangMau = Convert.ToString(dt.Rows[i]["iID_MaBangMau"]);
                BangMauHangMauModels.CapNhapSTT_BangMau_HangMau_DonVi(MaBangMau, MaDonVi);
            }
            dt.Dispose();
            return RedirectToAction("Index", new { MaPhongBan = MaPhongBan });
        }

        [Authorize]
        public ActionResult SortAll(String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_HangMau_DonVi", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            int i, j;
            SqlCommand cmd;
            DataTable dt, dtDonVi;

            cmd = new SqlCommand("SELECT * FROM BC_DonVi ORDER BY iSTT");
            dtDonVi = Connection.GetDataTable(cmd);
            cmd.Dispose();
            String MaDonVi, MaBangMau;

            //Sắp xếp các bảng cho toàn bộ đơn vị
            for (i = 0; i < dtDonVi.Rows.Count; i++)
            {
                MaDonVi = Convert.ToString(dtDonVi.Rows[i]["iID_MaDonVi"]);

                cmd = new SqlCommand("SELECT * FROM BC_BangMau WHERE bXemTheoDonVi = 1 AND iID_MaPhongBan = @iID_MaPhongBan ORDER BY iSTT");
                cmd.Parameters.AddWithValue("@iID_MaPhongBan", MaPhongBan);
                dt = Connection.GetDataTable(cmd);
                cmd.Dispose();

                for (j = 0; j < dt.Rows.Count; j++)
                {
                    MaBangMau = Convert.ToString(dt.Rows[j]["iID_MaBangMau"]);
                    BangMauHangMauModels.CapNhapSTT_BangMau_HangMau_DonVi(MaBangMau, MaDonVi);
                }
                dt.Dispose();
            }
            dtDonVi.Dispose();

            //Sắp xếp các bảng cho Vicem
            cmd = new SqlCommand("SELECT * FROM BC_BangMau WHERE bXemTheoDonVi = 0 AND iID_MaPhongBan = @iID_MaPhongBan ORDER BY iSTT");
            cmd.Parameters.AddWithValue("@iID_MaPhongBan", MaPhongBan);
            DataTable dtBangMauKhongTheoDonVi = Connection.GetDataTable(cmd);
            for (i = 0; i < dtBangMauKhongTheoDonVi.Rows.Count; i++)
            {
                MaBangMau = Convert.ToString(dtBangMauKhongTheoDonVi.Rows[i]["iID_MaBangMau"]);
                BangMauHangMauModels.CapNhapSTT_BangMau_HangMau_DonVi(MaBangMau, "");
            }
            dtBangMauKhongTheoDonVi.Dispose();

            return RedirectToAction("Index", new { MaPhongBan = MaPhongBan });
        }

        [Authorize]
        public ActionResult Delete(String MaBangMauHangMauDonVi, String MaDonVi, String MaBangMau)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_HangMau_DonVi", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            Bang bang = new Bang("BC_BangMau_HangMau_DonVi");
            bang.MaNguoiDungSua = User.Identity.Name;
            bang.IPSua = Request.UserHostAddress;
            bang.GiaTriKhoa = MaBangMauHangMauDonVi;
            bang.Delete();
            return RedirectToAction("Detail", new { MaDonVi = MaDonVi, MaBangMau = MaBangMau });
        }

        [Authorize]
        public ActionResult DeleteAll(String MaDonVi, String MaBangMau, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_HangMau_DonVi", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd = new SqlCommand("DELETE FROM BC_BangMau_HangMau_DonVi WHERE iID_MaBangMau=@iID_MaBangMau AND iID_MaDonVi=@iID_MaDonVi");
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
            Connection.UpdateDatabase(cmd);
            return RedirectToAction("Create", new { MaDonVi = MaDonVi, MaPhongBan = MaPhongBan });
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddNewsSubmit(String ParentID, String MaDonVi, String MaPhongBan, String MaBangMau)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_HangMau_DonVi", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            NameValueCollection data = Request.Form;
            String MaBangMauHangMau = "";
            for (int i = 0; i <= data.Count - 1; i++)
            {
                String req = Convert.ToString(data.AllKeys[i]);
                int j = req.IndexOf("_Kem");
                int h = req.IndexOf("Edit_iID_MaBangMau_HangMau");
                if (j < 0 && h >= 0)
                {
                    MaBangMauHangMau += Request.Form[req] + ",";
                }
            }

            //String MaBangMauHangMau = Request.Form[ParentID + "_iID_MaBangMau_HangMau"];
            String MaBangMauHangMau_List = "," + MaBangMauHangMau;
            if (MaBangMauHangMau_List != "")
            {
                String[] arr = MaBangMauHangMau_List.Split(',');
                for (int i = 1; i < arr.Length - 1; i++)
                {
                    if (arr[i] != "")
                    {
                        int tg = 1;

                        String SQL;
                        if (String.IsNullOrEmpty(MaDonVi) == false)
                        {
                            SQL = String.Format("SELECT iID_MaBangMau_HangMau FROM BC_BangMau_HangMau_DonVi WHERE iID_MaBangMau = {0} AND iID_MaDonVi = {1}", "'" + MaBangMau + "'", "'" + MaDonVi + "'");
                        }
                        else
                        {
                            SQL = String.Format("SELECT iID_MaBangMau_HangMau FROM BC_BangMau_HangMau_DonVi WHERE iID_MaBangMau = {0} AND iID_MaDonVi is null", "'" + MaBangMau + "'");
                        }

                        DataTable dtBangMau = Connection.GetDataTable(SQL);
                        for (int j = 0; j <= dtBangMau.Rows.Count - 1; j++)
                        {
                            if (arr[i] == Convert.ToString(dtBangMau.Rows[j]["iID_MaBangMau_HangMau"]))
                            {
                                tg = 0;
                                break;
                            }
                        }
                        if (tg == 1)
                        {
                            Bang bang = new Bang("BC_BangMau_HangMau_DonVi");
                            bang.MaNguoiDungSua = User.Identity.Name;
                            bang.IPSua = Request.UserHostAddress;
                            bang.CmdParams.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                            bang.CmdParams.Parameters.AddWithValue("@iID_MaBangMau_HangMau", arr[i].ToString());
                            bang.CmdParams.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                            bang.Save();
                        }
                    }
                }
                BangMauHangMauModels.CapNhapSTT_BangMau_HangMau_DonVi(MaBangMau, MaDonVi);
            }

            return RedirectToAction("Detail", new { MaDonVi = MaDonVi, MaPhongBan = MaPhongBan, MaBangMau = MaBangMau });
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditSubmit(String ParentID, String MaDonVi, String MaBangMau, int MaHangMauCha)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_HangMau_DonVi", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            NameValueCollection data = Request.Form;
            String MaBangMauHangMau = "";
            for (int i = 0; i <= data.Count - 1; i++)
            {
                String req = Convert.ToString(data.AllKeys[i]);
                int j = req.IndexOf("_Kem");
                int h = req.IndexOf("Edit_iID_MaBangMau_HangMau");
                if (j < 0 && h >= 0)
                {
                    MaBangMauHangMau += Request.Form[req] + ",";
                }
            }

            //String MaBangMauHangMau = Request.Form[ParentID + "_iID_MaBangMau_HangMau"];
            String MaBangMauHangMau_List = "," + MaBangMauHangMau;
            if (MaBangMauHangMau_List != "")
            {
                String[] arr = MaBangMauHangMau_List.Split(',');
                for (int i = 1; i < arr.Length - 1; i++)
                {
                    if (arr[i] != "")
                    {
                        int tg = 1;

                        String SQL;
                        if (String.IsNullOrEmpty(MaDonVi) == false)
                        {
                            SQL = String.Format("SELECT iID_MaBangMau_HangMau FROM BC_BangMau_HangMau_DonVi WHERE iID_MaBangMau = {0} AND iID_MaDonVi = {1}", "'" + MaBangMau + "'", "'" + MaDonVi + "'");
                        }
                        else
                        {
                            SQL = String.Format("SELECT iID_MaBangMau_HangMau FROM BC_BangMau_HangMau_DonVi WHERE iID_MaBangMau = {0} AND iID_MaDonVi is null", "'" + MaBangMau + "'");
                        }

                        DataTable dtBangMau = Connection.GetDataTable(SQL);
                        for (int j = 0; j <= dtBangMau.Rows.Count - 1; j++)
                        {
                            if (arr[i] == Convert.ToString(dtBangMau.Rows[j]["iID_MaBangMau_HangMau"]))
                            {
                                tg = 0;
                                break;
                            }
                        }
                        if (tg == 1)
                        {
                            Bang bang = new Bang("BC_BangMau_HangMau_DonVi");
                            bang.MaNguoiDungSua = User.Identity.Name;
                            bang.IPSua = Request.UserHostAddress;
                            bang.CmdParams.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                            bang.CmdParams.Parameters.AddWithValue("@iID_MaBangMau_HangMau", arr[i].ToString());
                            bang.CmdParams.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                            bang.Save();
                        }
                    }
                }
            }
            BangMauHangMauModels.CapNhapSTT_BangMau_HangMau_DonVi(MaBangMau, MaDonVi);
            return RedirectToAction("Edit", new { MaDonVi = MaDonVi, MaBangMau = MaBangMau, MaHangMauCha = MaHangMauCha });
        }

        [Authorize]
        public ActionResult ThemNhanhChiTieu(String MaDonVi, String MaBangMau, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_HangMau_DonVi", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
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
        public ActionResult ThemNhanhChiTieuSubmit(String ParentID, String MaDonVi, String MaBangMau, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_HangMau_DonVi", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            String MaDonViLayDuLieu;
            MaDonViLayDuLieu = Request.Form[ParentID + "_iID_MaDonVi"];

            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM BC_BangMau_HangMau_DonVi WHERE iID_MaBangMau=@iID_MaBangMau AND iID_MaDonVi=@iID_MaDonVi";
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonViLayDuLieu);
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();
            if (dt.Rows.Count > 0)
            {
                cmd = new SqlCommand("DELETE FROM BC_BangMau_HangMau_DonVi WHERE iID_MaBangMau=@iID_MaBangMau AND iID_MaDonVi=@iID_MaDonVi");
                cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                Connection.UpdateDatabase(cmd);
                cmd.Dispose();
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    Bang bang = new Bang("BC_BangMau_HangMau_DonVi");
                    bang.MaNguoiDungSua = User.Identity.Name;
                    bang.IPSua = Request.UserHostAddress;
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaBangMau_HangMau", dt.Rows[i]["iID_MaBangMau_HangMau"]);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                    bang.Save();
                }
            }
            BangMauHangMauModels.CapNhapSTT_BangMau_HangMau_DonVi(MaBangMau, MaDonVi);
            return RedirectToAction("Detail", new { MaDonVi = MaDonVi, MaBangMau = MaBangMau, MaPhongBan = MaPhongBan });
        }

        [Authorize]
        public ActionResult ThemNhanhChiTieuTheoBangChon(String MaDonVi, String MaBangMau, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_HangMau_DonVi", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
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
        public ActionResult ThemNhanhChiTieuTheoBangChonSubmit(String ParentID, String MaDonVi, String MaBangMau, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_HangMau_DonVi", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            String MaBangMauLayDuLieu;
            MaBangMauLayDuLieu = Request.Form[ParentID + "_iID_MaBangMau"];

            int i, j;
            SqlCommand cmd;
            //Lây dữ liệu từ bảng mẫu chọn để imports
            cmd = new SqlCommand();
            if (String.IsNullOrEmpty(MaDonVi) == false)
            {
                cmd.CommandText = "SELECT * FROM BC_BangMau_HangMau AS BMHM inner join BC_BangMau_HangMau_DonVi as BMHMDV on BMHM.iID_MaBangMau_HangMau = BMHMDV.iID_MaBangMau_HangMau " +
                                    "WHERE BMHMDV.iID_MaDonVi = @iID_MaDonVi AND BMHMDV.iID_MaBangMau = @iID_MaBangMau";
                cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMauLayDuLieu);
                cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
            }
            else
            {
                cmd.CommandText = "SELECT * FROM BC_BangMau_HangMau AS BMHM inner join BC_BangMau_HangMau_DonVi as BMHMDV on BMHM.iID_MaBangMau_HangMau = BMHMDV.iID_MaBangMau_HangMau " +
                                    "WHERE BMHMDV.iID_MaDonVi is null AND BMHMDV.iID_MaBangMau = @iID_MaBangMau";
                cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMauLayDuLieu);
            }
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();

            if (String.IsNullOrEmpty(MaDonVi) == false)
            {
                cmd = new SqlCommand("DELETE FROM BC_BangMau_HangMau_DonVi WHERE iID_MaDonVi = @iID_MaDonVi AND iID_MaBangMau = @iID_MaBangMau");
                cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
            }
            else
            {
                cmd = new SqlCommand("DELETE FROM BC_BangMau_HangMau_DonVi WHERE iID_MaDonVi is null AND iID_MaBangMau = @iID_MaBangMau");
                cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            }
            Connection.UpdateDatabase(cmd);
            cmd.Dispose();

            DataRow R;
            if (dt.Rows.Count > 0)
            {
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    R = dt.Rows[i];
                    cmd = new SqlCommand("SELECT iID_MaBangMau_HangMau FROM BC_BangMau_HangMau WHERE iID_MaBangMau=@iID_MaBangMau AND iID_MaHangMau = @iID_MaHangMau AND iID_MaHangMau_Cha=@iID_MaHangMau_Cha");
                    cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                    cmd.Parameters.AddWithValue("@iID_MaHangMau", R["iID_MaHangMau"]);
                    cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", R["iID_MaHangMau_Cha"]);
                    int MaBangMau_HangMauLay = Convert.ToInt32(Connection.GetValue(cmd, 0));
                    cmd.Dispose();

                    Bang bang = new Bang("BC_BangMau_HangMau_DonVi");
                    bang.MaNguoiDungSua = User.Identity.Name;
                    bang.IPSua = Request.UserHostAddress;
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaBangMau_HangMau", MaBangMau_HangMauLay);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                    bang.Save();
                }
            }
            BangMauHangMauModels.CapNhapSTT_BangMau_HangMau_DonVi(MaBangMau, MaDonVi);
            return RedirectToAction("Detail", new { MaDonVi = MaDonVi, MaBangMau = MaBangMau, MaPhongBan = MaPhongBan });
        }
    }
}