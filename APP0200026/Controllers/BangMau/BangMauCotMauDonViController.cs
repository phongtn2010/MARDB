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
    public class BangMauCotMauDonViController : Controller
    {
        // GET: BangMauCotMauDonVi
        [Authorize]
        public ActionResult List(String MaPhongBan, String MaBangMau)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_CotMau_DonVi", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["MaPhongBan"] = MaPhongBan;
            ViewData["MaBangMau"] = MaBangMau;
            return View();
        }


        // GET: /BangMauCotMauDonVi/
        [Authorize]
        public ActionResult Index(String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_CotMau_DonVi", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["MaPhongBan"] = MaPhongBan;
            return View();
        }

        [Authorize]
        public ActionResult Create(String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_CotMau_DonVi", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["MaPhongBan"] = MaPhongBan;
            return View();
        }

        [Authorize]
        public ActionResult Edit()
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_CotMau_DonVi", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            return View();
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditSubmit(String ParentID, String MaDonVi, String MaBangMau)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_CotMau_DonVi", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            //Xóa cột ẩn 
            SqlCommand cmd;
            if (String.IsNullOrEmpty(MaDonVi) == false)
            {
                cmd = new SqlCommand("DELETE FROM BC_BangMau_CotMau_DonVi WHERE iID_MaBangMau=@iID_MaBangMau AND iID_MaDonVi = @iID_MaDonVi");
                cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
            }
            else
            {
                cmd = new SqlCommand("DELETE FROM BC_BangMau_CotMau_DonVi WHERE iID_MaBangMau=@iID_MaBangMau");
                cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            }
            Connection.UpdateDatabase(cmd);
            cmd.Dispose();

            NameValueCollection data = Request.Form;
            String MaBangMauCotMau = "";
            for (int i = 0; i <= data.Count - 1; i++)
            {
                String req = Convert.ToString(data.AllKeys[i]);
                int j = req.IndexOf("_Kem");
                int h = req.IndexOf("Edit_iID_MaCotMau");
                if (j < 0 && h >= 0)
                {
                    MaBangMauCotMau += Request.Form[req] + ",";
                }
            }

            //String MaBangMauHangMau = Request.Form[ParentID + "_iID_MaBangMau_HangMau"];
            String MaBangMauHangMau_List = "," + MaBangMauCotMau;
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
                            SQL = String.Format("SELECT iID_MaCotMau FROM BC_BangMau_CotMau_DonVi WHERE iID_MaDonVi = {0}", "'" + MaDonVi + "'");
                        }
                        else
                        {
                            SQL = String.Format("SELECT iID_MaCotMau FROM BC_BangMau_CotMau_DonVi WHERE iID_MaDonVi is null");
                        }

                        DataTable dtBangMau = Connection.GetDataTable(SQL);
                        for (int j = 0; j <= dtBangMau.Rows.Count - 1; j++)
                        {
                            if (arr[i] == Convert.ToString(dtBangMau.Rows[j]["iID_MaCotMau"]))
                            {
                                tg = 0;
                                break;
                            }
                        }
                        if (tg == 1)
                        {
                            Bang bang = new Bang("BC_BangMau_CotMau_DonVi");
                            bang.MaNguoiDungSua = User.Identity.Name;
                            bang.IPSua = Request.UserHostAddress;
                            bang.CmdParams.Parameters.AddWithValue("@iID_MaCotMau", arr[i].ToString());
                            bang.CmdParams.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                            bang.CmdParams.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                            bang.Save();
                        }
                    }
                }
            }
            return RedirectToAction("Edit", new { MaDonVi = MaDonVi, MaBangMau = MaBangMau });
        }

        [Authorize]
        public ActionResult Delete(String MaBangMau_CotMau_DonVi, String MaDonVi, String MaBangMau, String MaPhongBan, String MaDonViLoc)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_CotMau_DonVi", "Delete") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            Bang bang = new Bang("BC_BangMau_CotMau_DonVi");
            bang.MaNguoiDungSua = User.Identity.Name;
            bang.IPSua = Request.UserHostAddress;
            bang.GiaTriKhoa = MaBangMau_CotMau_DonVi;
            bang.Delete();

            return RedirectToAction("List", new { MaBangMau = MaBangMau, MaPhongBan = MaPhongBan, MaDonVi = MaDonViLoc });
        }

        [Authorize]
        public ActionResult AddCotMauDonVi(String MaDonVi, String MaBangMau, String MaCotMau, String MaBangMauCotMauDonVi, String MaPhongBan, String MaDonViLoc)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_CotMau_DonVi", "Delete") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            Bang bang = new Bang("BC_BangMau_CotMau_DonVi");
            bang.MaNguoiDungSua = User.Identity.Name;
            bang.IPSua = Request.UserHostAddress;
            if (MaBangMauCotMauDonVi != "" && MaBangMauCotMauDonVi != null)
            {
                bang.DuLieuMoi = false;
                bang.GiaTriKhoa = MaBangMauCotMauDonVi;
            }
            else
            {
                bang.DuLieuMoi = true;
            }
            bang.CmdParams.Parameters.AddWithValue("@iID_MaCotMau", MaCotMau);
            bang.CmdParams.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            bang.CmdParams.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
            try
            {
                bang.Save();
                return RedirectToAction("List", new { MaBangMau = MaBangMau, MaPhongBan = MaPhongBan, MaDonVi = MaDonViLoc });
            }
            catch
            {
                return RedirectToAction("List", new { MaBangMau = MaBangMau, MaPhongBan = MaPhongBan, MaDonVi = MaDonViLoc });
            }
        }
    }
}