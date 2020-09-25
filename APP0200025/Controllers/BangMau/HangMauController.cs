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
using DomainModel.Controls;
using DATA0200025;

namespace APP0200025.Controllers
{
    public class HangMauController : Controller
    {
        // GET: HangMau
        [Authorize]
        public ActionResult Index(int? HangMau_page, String ParentID, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_HangMau", "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["HangMau_page"] = HangMau_page;
            ViewData["MaPhongBan"] = MaPhongBan;
            return View();
        }
        [Authorize]
        public ActionResult List()
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_HangMau", "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            return View();
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Create(String MaHangMauCha, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_HangMau", "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            return RedirectToAction("Edit", new { MaHangMauCha = MaHangMauCha, MaPhongBan = MaPhongBan });
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult dEdit(String MaHangMau, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_HangMau", "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandText = "SELECT iID_MaHangMau_Cha FROM BC_HangMau WHERE iID_MaHangMau=@iID_MaHangMau";
            cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
            String MaHangMauCha = Connection.GetValueString(cmd, "");
            cmd.Dispose();

            return RedirectToAction("Edit", new { MaHangMau = MaHangMau, MaHangMauCha = MaHangMauCha, MaPhongBan = MaPhongBan });
        }

        [Authorize]
        public ActionResult Delete(String MaHangMau, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_HangMau", "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandText = "SELECT iID_MaHangMau FROM BC_HangMau WHERE iID_MaHangMau_Cha=@iID_MaHangMau";
            cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();
            if (dt.Rows.Count > 0)
            {
                cmd = new SqlCommand();
                cmd.CommandText = "DELETE BC_HangMau WHERE iID_MaHangMau_Cha=@iID_MaHangMau_Cha";
                cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMau);
                Connection.UpdateDatabase(cmd);
                cmd.Dispose();
            }

            int MaHangMauCha = 0;
            cmd = new SqlCommand();
            cmd.CommandText = "SELECT iID_MaHangMau_Cha FROM BC_HangMau WHERE iID_MaHangMau=@MaHangMau";
            cmd.Parameters.AddWithValue("@MaHangMau", MaHangMau);
            MaHangMauCha = Convert.ToInt32(Connection.GetValue(cmd, 0));
            cmd.Dispose();

            Bang bang = new Bang("BC_HangMau");
            bang.MaNguoiDungSua = User.Identity.Name;
            bang.IPSua = Request.UserHostAddress;
            bang.GiaTriKhoa = MaHangMau;
            bang.Delete();

            DataTable dtBMHM;
            cmd = new SqlCommand();
            cmd = new SqlCommand("SELECT iID_MaBangMau_HangMau FROM BC_BangMau_HangMau WHERE iID_MaHangMau=@iID_MaHangMau");
            cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
            dtBMHM = Connection.GetDataTable(cmd);
            cmd.Dispose();
            if (dtBMHM.Rows.Count > 0)
            {
                for (int k = 0; k <= dtBMHM.Rows.Count - 1; k++)
                {
                    // Xóa tại bảng: BC_BangMau_HangMau_DonVi
                    cmd = new SqlCommand();
                    cmd = new SqlCommand("SELECT count(*) FROM BC_BangMau_HangMau_DonVi WHERE iID_MaBangMau_HangMau=@iID_MaBangMau_HangMau");
                    cmd.Parameters.AddWithValue("@iID_MaBangMau_HangMau", dtBMHM.Rows[k]["iID_MaBangMau_HangMau"]);
                    int SoMaBangMauHangMau = Convert.ToInt32(Connection.GetValue(cmd, 0));
                    cmd.Dispose();
                    if (SoMaBangMauHangMau > 0)
                    {
                        cmd = new SqlCommand();
                        cmd = new SqlCommand("DELETE BC_BangMau_HangMau_DonVi WHERE iID_MaBangMau_HangMau=@iID_MaBangMau_HangMau");
                        cmd.Parameters.AddWithValue("@iID_MaBangMau_HangMau", dtBMHM.Rows[k]["iID_MaBangMau_HangMau"]);
                        Connection.UpdateDatabase(cmd);
                        cmd.Dispose();
                    }
                }

                // Xóa tại bảng: BC_BangMau_HangMau
                cmd = new SqlCommand();
                cmd = new SqlCommand("DELETE BC_BangMau_HangMau WHERE iID_MaHangMau=@iID_MaHangMau");
                cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
                Connection.UpdateDatabase(cmd);
                cmd.Dispose();

                //Xóa tại bảng: BC_Bang_Hang
                cmd = new SqlCommand();
                cmd = new SqlCommand("DELETE BC_Bang_Hang WHERE iID_MaHangMau=@iID_MaHangMau");
                cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
                Connection.UpdateDatabase(cmd);
                cmd.Dispose();
            }

            return RedirectToAction("Index", new { MaHangMauCha = MaHangMauCha, MaPhongBan = MaPhongBan });
        }

        [Authorize]
        public ActionResult Edit(String MaHangMau, String MaHangMauCha, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_HangMau", "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            if (String.IsNullOrEmpty(MaHangMauCha))
            {
                MaHangMauCha = "0";
            }
            ViewData["DuLieuMoi"] = "0";
            if (String.IsNullOrEmpty(MaHangMau))
            {
                //MaDonVi = Globals.getNewGuid().ToString();
                ViewData["DuLieuMoi"] = "1";
            }
            ViewData["iID_MaHangMau"] = MaHangMau;
            ViewData["iID_MaHangMau_Cha"] = MaHangMauCha;
            ViewData["MaPhongBan"] = MaPhongBan;
            return View();
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditSubmit(String ParentID, String MaHangMau, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_HangMau", "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            SqlCommand cmd;
            NameValueCollection arrLoi = new NameValueCollection();
            String TenHangMau = Convert.ToString(Request.Form[ParentID + "_sTenHang"]);
            int MaHangMauCha = Convert.ToInt32(Request.Form[ParentID + "_iID_MaHangMau_Cha"]);
            String DuLieuMoi = Convert.ToString(Request.Form[ParentID + "_DuLieuMoi"]);
            String NewID = Globals.getNewGuid().ToString();
            if (TenHangMau == string.Empty || TenHangMau == "")
            {
                arrLoi.Add("err_sTenHang", "Bạn chưa nhập tên chỉ tiêu mẫu");
            }
            if (arrLoi.Count > 0)
            {
                for (int i = 0; i <= arrLoi.Count - 1; i++)
                {
                    ModelState.AddModelError(ParentID + "_" + arrLoi.GetKey(i), arrLoi[i]);
                }
                ViewData["iID_MaHangMau"] = MaHangMau;
                ViewData["iID_MaHangMau_Cha"] = MaHangMauCha;
                ViewData["MaPhongBan"] = MaPhongBan;
                return View("Edit");
            }
            else
            {
                int i, j;
                Bang bang = new Bang("BC_HangMau");
                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                bang.TruyenGiaTri(ParentID, Request.Form);
                if (DuLieuMoi == "1")
                {
                    bang.GiaTriKhoa = null;
                    int cs = bang.CmdParams.Parameters.IndexOf("@iID_MaHangMau");
                    if (cs >= 0)
                    {
                        bang.CmdParams.Parameters.RemoveAt(cs);
                    }

                    cmd = new SqlCommand("SELECT COUNT(*) FROM BC_HangMau WHERE iID_MaHangMau_Cha=@iID_MaHangMau_Cha");
                    cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMauCha);
                    int SoHangMauCon = Convert.ToInt32(Connection.GetValue(cmd, 0));
                    cmd.Dispose();

                    //bang.CmdParams.Parameters.AddWithValue("@iID_MaTam", NewID);
                    bang.CmdParams.Parameters.AddWithValue("@iSTT", SoHangMauCon);
                }
                if (DuLieuMoi == "0") bang.GiaTriKhoa = MaHangMau;
                bang.Save();

                String MaBangMau;
                if (DuLieuMoi == "1")
                {
                    cmd = new SqlCommand("SELECT iID_MaBangMau FROM BC_BangMau_HangMau WHERE iID_MaHangMau = @iID_MaHangMau");
                    cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMauCha);
                    DataTable dtBC_BangMau_HangMau = Connection.GetDataTable(cmd);
                    cmd.Dispose();

                    cmd = new SqlCommand("SELECT MAX(iID_MaHangMau) FROM BC_HangMau");
                    //cmd.Parameters.AddWithValue("@iID_MaTam", NewID);
                    String MaHangMauMoi = Convert.ToString(Connection.GetValue(cmd, ""));
                    cmd.Dispose();

                    if (dtBC_BangMau_HangMau.Rows.Count > 0)
                    {
                        for (i = 0; i < dtBC_BangMau_HangMau.Rows.Count; i++)
                        {
                            MaBangMau = Convert.ToString(dtBC_BangMau_HangMau.Rows[i]["iID_MaBangMau"]);

                            cmd = new SqlCommand("SELECT bXemTheoDonVi FROM BC_BangMau WHERE iID_MaBangMau = @iID_MaBangMau");
                            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                            Boolean bXemTheoDonVi = Convert.ToBoolean(Connection.GetValue(cmd, 0));
                            cmd.Dispose();
                            if (bXemTheoDonVi == true)
                            {
                                //Thêm vào bảng mẫu - hàng mẫu
                                HangMauModels.ThemHangMauVaoBangMau(MaBangMau, MaHangMauMoi, MaHangMauCha);

                                //Thêm vào các mã hàng mẫu chung hàng con trong bảng mẫu
                                cmd = new SqlCommand();
                                cmd.CommandText = "SELECT A.iID_MaBangMau_HangMau, A.iID_MaHangMau FROM BC_BangMau_HangMau as A inner join BC_HangMau as B on A.iID_MaHangMau = B.iID_MaHangMau " +
                                                    "WHERE B.iID_MaHangMau_ChungHangMauCon = @iID_MaHangMau_ChungHangMauCon AND A.iID_MaBangMau = @iID_MaBangMau";
                                cmd.Parameters.AddWithValue("@iID_MaHangMau_ChungHangMauCon", MaHangMauCha);
                                cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                                DataTable dtChungHangMauCon = Connection.GetDataTable(cmd);
                                cmd.Dispose();
                                if (dtChungHangMauCon.Rows.Count > 0)
                                {
                                    for (j = 0; j < dtChungHangMauCon.Rows.Count; j++)
                                    {
                                        HangMauModels.ThemHangMauVaoBangMau(MaBangMau, MaHangMauMoi, Convert.ToInt32(dtChungHangMauCon.Rows[j]["iID_MaHangMau"]));
                                    }
                                }
                            }
                        }
                    }
                }

                //Sửa thông tin hàng mẫu của các bảng liên quan
                if (DuLieuMoi == "0")
                {
                    //Sửa chỉ tiêu trong bảng: BangMau_HangMau
                    cmd = new SqlCommand();
                    cmd = new SqlCommand("SELECT * FROM BC_BangMau_HangMau WHERE iID_MaHangMau=@iID_MaHangMau");
                    cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
                    DataTable dtBC_BangMau_HangMau = Connection.GetDataTable(cmd);
                    //int SoHangMau = Convert.ToInt32(Connection.GetValue(cmd, 0));
                    cmd.Dispose();

                    int iID_MaDonVi = Convert.ToInt32(Request.Form[ParentID + "_iID_MaDonVi"]);
                    int iID_MaPhongBan = Convert.ToInt32(Request.Form[ParentID + "_iID_MaPhongBan"]);
                    int iID_LoaiSanPham = Convert.ToInt32(Request.Form[ParentID + "_iID_LoaiSanPham"]);
                    String sCongThuc = Convert.ToString(Request.Form[ParentID + "_sCongThuc"]);
                    int iHeight = Convert.ToInt32(Request.Form[ParentID + "_iHeight"]);
                    String sBackGround = Convert.ToString(Request.Form[ParentID + "_sBackGround"]);
                    String sColor = Convert.ToString(Request.Form[ParentID + "_sColor"]);
                    int iFontSize = Convert.ToInt32(Request.Form[ParentID + "_iFontSize"]);
                    String bBold = Convert.ToString(Request.Form[ParentID + "_bBold"]);
                    String bItalic = Convert.ToString(Request.Form[ParentID + "_bItalic"]);
                    String bUnderline = Convert.ToString(Request.Form[ParentID + "_bUnderline"]);
                    String bVisible = Convert.ToString(Request.Form[ParentID + "_bVisible"]);
                    String bTongHangCon = Convert.ToString(Request.Form[ParentID + "_bTongHangCon"]);
                    String bTinhTongTheoDonVi = Convert.ToString(Request.Form[ParentID + "_bTinhTongTheoDonVi"]);
                    String bTruongGhiChu = Convert.ToString(Request.Form[ParentID + "_bTruongGhiChu"]);
                    String bThemChiTieuCon = Convert.ToString(Request.Form[ParentID + "_bThemChiTieuCon"]);
                    String bChuaLoaiSanPham = Convert.ToString(Request.Form[ParentID + "_bChuaLoaiSanPham"]);
                    String bChuaSanPham = Convert.ToString(Request.Form[ParentID + "_bChuaSanPham"]);
                    String bTinhTongTheoLoaiSanPham = Convert.ToString(Request.Form[ParentID + "_bTinhTongTheoLoaiSanPham"]);

                    int tgBold = 0, tgItalic = 0, tgUnderline = 0, tgVisible = 0, tgTongHangCon = 0, tgTinhTongTheoDonVi = 0, tgTruongGhiChu = 0, tgThemChiTieuCon = 0;
                    int tgChuaLoaiSanPham = 0, tgChuaSanPham = 0, tgTinhTongTheoLoaiSanPham = 0;

                    if (bBold == "on")
                    {
                        tgBold = 1;
                    }
                    if (bItalic == "on")
                    {
                        tgItalic = 1;
                    }
                    if (bUnderline == "on")
                    {
                        tgUnderline = 1;
                    }
                    if (bVisible == "on")
                    {
                        tgVisible = 1;
                    }
                    if (bTongHangCon == "on")
                    {
                        tgTongHangCon = 1;
                    }
                    if (bTinhTongTheoDonVi == "on")
                    {
                        tgTinhTongTheoDonVi = 1;
                    }

                    if (bTruongGhiChu == "on")
                    {
                        tgTruongGhiChu = 1;
                    }

                    if (bThemChiTieuCon == "on")
                    {
                        tgThemChiTieuCon = 1;
                    }

                    if (bChuaLoaiSanPham == "on")
                    {
                        tgChuaLoaiSanPham = 1;
                    }

                    if (bChuaSanPham == "on")
                    {
                        tgChuaSanPham = 1;
                    }
                    if (bTinhTongTheoLoaiSanPham == "on")
                    {
                        tgTinhTongTheoLoaiSanPham = 1;
                    }

                    if (dtBC_BangMau_HangMau.Rows.Count > 0)
                    {
                        //Sửa tại bảng: BC_Bang_Hang
                        cmd = new SqlCommand();
                        cmd.CommandText = "UPDATE BC_Bang_Hang SET sTenHang = @sTenHang, " +
                                                "iID_MaDonVi=@iID_MaDonVi, iID_LoaiSanPham=@iID_LoaiSanPham, iID_MaPhongBan=@iID_MaPhongBan, sCongThuc=@sCongThuc, iHeight=@iHeight, sBackGround=@sBackGround, " +
                                                "sColor=@sColor, iFontSize=@iFontSize, bBold=@bBold, bItalic=@bItalic, bUnderline=@bUnderline, bVisible=@bVisible, " +
                                                "bTongHangCon=@bTongHangCon, bTinhTongTheoDonVi=@bTinhTongTheoDonVi, bTruongGhiChu=@bTruongGhiChu, " +
                                                "bThemChiTieuCon = @bThemChiTieuCon, bChuaLoaiSanPham=@bChuaLoaiSanPham, bChuaSanPham=@bChuaSanPham, bTinhTongTheoLoaiSanPham=@bTinhTongTheoLoaiSanPham " +
                                                "WHERE iID_MaHangMau=@iID_MaHangMau";
                        cmd.Parameters.AddWithValue("@sTenHang", TenHangMau);
                        cmd.Parameters.AddWithValue("@iID_MaDonVi", iID_MaDonVi);
                        cmd.Parameters.AddWithValue("@iID_LoaiSanPham", iID_LoaiSanPham);
                        cmd.Parameters.AddWithValue("@iID_MaPhongBan", iID_MaPhongBan);
                        cmd.Parameters.AddWithValue("@sCongThuc", sCongThuc);
                        cmd.Parameters.AddWithValue("@iHeight", iHeight);
                        cmd.Parameters.AddWithValue("@sBackGround", sBackGround);
                        cmd.Parameters.AddWithValue("@sColor", sColor);
                        cmd.Parameters.AddWithValue("@iFontSize", iFontSize);
                        cmd.Parameters.AddWithValue("@bBold", tgBold);
                        cmd.Parameters.AddWithValue("@bItalic", tgItalic);
                        cmd.Parameters.AddWithValue("@bUnderline", tgUnderline);
                        cmd.Parameters.AddWithValue("@bVisible", tgVisible);
                        cmd.Parameters.AddWithValue("@bTongHangCon", tgTongHangCon);
                        cmd.Parameters.AddWithValue("@bTinhTongTheoDonVi", tgTinhTongTheoDonVi);
                        cmd.Parameters.AddWithValue("@bTruongGhiChu", tgTruongGhiChu);
                        cmd.Parameters.AddWithValue("@bThemChiTieuCon", tgThemChiTieuCon);
                        cmd.Parameters.AddWithValue("@bChuaLoaiSanPham", tgChuaLoaiSanPham);
                        cmd.Parameters.AddWithValue("@bChuaSanPham", tgChuaSanPham);
                        cmd.Parameters.AddWithValue("@bTinhTongTheoLoaiSanPham", tgTinhTongTheoLoaiSanPham);
                        cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
                        Connection.UpdateDatabase(cmd);
                        cmd.Dispose();

                        //for (i = 0; i < dtBC_BangMau_HangMau.Rows.Count; i++)
                        //{
                        //MaBangMau = Convert.ToString(dtBC_BangMau_HangMau.Rows[i]["iID_MaBangMau"]);
                        //String MaHangMauChaLay = Convert.ToString(dtBC_BangMau_HangMau.Rows[i]["iID_MaHangMau_Cha"]);

                        //Sửa tại bảng: BC_BangMau_HangMau
                        //cmd = new SqlCommand();
                        //cmd.CommandText = "UPDATE BC_BangMau_HangMau SET iID_MaHangMau_Cha = @iID_MaHangMau_Cha " +
                        //                  "WHERE iID_MaHangMau=@iID_MaHangMau";

                        //cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMauCha);
                        //cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
                        //Connection.UpdateDatabase(cmd);
                        //cmd.Dispose();                            
                        //}
                    }
                }
                return RedirectToAction("Index", new { MaHangMauCha = MaHangMauCha, MaPhongBan = MaPhongBan });
            }
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Sort(String MaHangMauCha, String MaHangMau, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_HangMau", "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            if (String.IsNullOrEmpty(MaHangMauCha))
            {
                MaHangMauCha = "0";
            }
            ViewData["iID_MaHangMau_Cha"] = MaHangMauCha;
            ViewData["iID_MaHangMau"] = MaHangMau;
            ViewData["MaPhongBan"] = MaPhongBan;
            return View();
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SortSubmit(String MaHangMauCha, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_HangMau", "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            string strOrder = Request.Form["hiddenOrder"].ToString();
            String[] arrTG = strOrder.Split('$');
            int i;
            for (i = 0; i < arrTG.Length - 1; i++)
            {
                Bang bang = new Bang("BC_HangMau");
                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                bang.GiaTriKhoa = arrTG[i];
                bang.DuLieuMoi = false;
                bang.CmdParams.Parameters.AddWithValue("@iSTT", i);
                bang.Save();
            }

            int MaHangMauChaCu = 0;
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandText = "SELECT iID_MaHangMau_Cha FROM BC_HangMau WHERE iID_MaHangMau=@MaHangMau";
            cmd.Parameters.AddWithValue("@MaHangMau", MaHangMauCha);
            MaHangMauChaCu = Convert.ToInt32(Connection.GetValue(cmd, 0));
            cmd.Dispose();
            return RedirectToAction("Index", new { MaHangMauCha = MaHangMauChaCu, MaPhongBan = MaPhongBan });
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult AddChildRow(String MaHangMau, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_HangMau", "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["iID_MaHangMau"] = MaHangMau;
            ViewData["MaPhongBan"] = MaPhongBan;
            return View();
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddChildRowSubmit(String MaHangMau, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_HangMau", "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            NameValueCollection data = Request.Form;
            String MaHangMauCon = "";
            for (int i = 0; i <= data.Count - 1; i++)
            {
                String req = Convert.ToString(data.AllKeys[i]);
                int j = req.IndexOf("_Kem");
                int h = req.IndexOf("Create_iID_MaHangMau_");
                if (j < 0 && h >= 0)
                {
                    MaHangMauCon += Request.Form[req] + ",";
                }
            }

            DataTable dt = null;
            SqlCommand cmd = new SqlCommand();
            //MaHangMau = Request.Form[ParentID + "_iID_MaHangMau"];
            String MaHangMau_List = "," + MaHangMauCon;
            if (MaHangMau_List != "")
            {
                String[] arr = MaHangMau_List.Split(',');
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] != "")
                    {
                        int tg = 1;
                        String SQL = String.Format("SELECT iID_MaHangMau FROM BC_HangMau WHERE iID_MaHangMau_Cha = {0}", MaHangMau);
                        DataTable dtBangMau = Connection.GetDataTable(SQL);
                        for (int j = 0; j <= dtBangMau.Rows.Count - 1; j++)
                        {
                            if (arr[i] == Convert.ToString(dtBangMau.Rows[j]["iID_MaHangMau"]))
                            {
                                tg = 0;
                            }
                        }

                        if (tg == 1)
                        {
                            cmd = new SqlCommand("SELECT * FROM BC_HangMau WHERE iID_MaHangMau=@iID_MaHangMau");
                            cmd.Parameters.AddWithValue("@iID_MaHangMau", arr[i]);
                            dt = Connection.GetDataTable(cmd);
                            cmd.Dispose();
                            DataRow R;
                            if (dt.Rows.Count > 0)
                            {
                                R = dt.Rows[0];
                                Bang bang = new Bang("BC_HangMau");
                                bang.MaNguoiDungSua = User.Identity.Name;
                                bang.IPSua = Request.UserHostAddress;
                                bang.GiaTriKhoa = null;
                                int cs = bang.CmdParams.Parameters.IndexOf("@iID_MaHangMau");
                                if (cs >= 0)
                                {
                                    bang.CmdParams.Parameters.RemoveAt(cs);
                                }
                                bang.CmdParams.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMau);
                                bang.CmdParams.Parameters.AddWithValue("@iID_MaDonVi", R["iID_MaDonVi"].ToString());
                                bang.CmdParams.Parameters.AddWithValue("@iID_LoaiSanPham", R["iID_LoaiSanPham"].ToString());
                                bang.CmdParams.Parameters.AddWithValue("@iID_MaHangMau_ChungHangMauCon", R["iID_MaHangMau_ChungHangMauCon"].ToString());
                                bang.CmdParams.Parameters.AddWithValue("@iID_MaPhongBan", R["iID_MaPhongBan"].ToString());
                                bang.CmdParams.Parameters.AddWithValue("@sTenHang", R["sTenHang"].ToString());
                                bang.CmdParams.Parameters.AddWithValue("@sCongThuc", R["sCongThuc"].ToString());
                                bang.CmdParams.Parameters.AddWithValue("@iHeight", R["iHeight"].ToString());
                                bang.CmdParams.Parameters.AddWithValue("@sBackGround", R["sBackGround"].ToString());
                                bang.CmdParams.Parameters.AddWithValue("@sColor", R["sColor"].ToString());
                                bang.CmdParams.Parameters.AddWithValue("@iFontSize", R["iFontSize"].ToString());
                                bang.CmdParams.Parameters.AddWithValue("@bBold", R["bBold"].ToString());
                                bang.CmdParams.Parameters.AddWithValue("@bItalic", R["bItalic"].ToString());
                                bang.CmdParams.Parameters.AddWithValue("@bUnderline", R["bUnderline"].ToString());
                                bang.CmdParams.Parameters.AddWithValue("@bVisible", R["bVisible"].ToString());
                                bang.CmdParams.Parameters.AddWithValue("@bTongHangCon", R["bTongHangCon"].ToString());
                                bang.CmdParams.Parameters.AddWithValue("@bTinhTongTheoDonVi", R["bTinhTongTheoDonVi"].ToString());
                                bang.CmdParams.Parameters.AddWithValue("@bTruongGhiChu", R["bTruongGhiChu"].ToString());
                                bang.CmdParams.Parameters.AddWithValue("@bThemChiTieuCon", R["bThemChiTieuCon"].ToString());
                                bang.CmdParams.Parameters.AddWithValue("@bChuaLoaiSanPham", R["bChuaLoaiSanPham"].ToString());
                                bang.CmdParams.Parameters.AddWithValue("@bChuaSanPham", R["bChuaSanPham"].ToString());
                                bang.CmdParams.Parameters.AddWithValue("@bTinhTongTheoLoaiSanPham", R["bTinhTongTheoLoaiSanPham"].ToString());
                                bang.CmdParams.Parameters.AddWithValue("@iSTT", R["iSTT"]);
                                bang.Save();
                            }
                            dt.Dispose();
                        }
                    }
                }
            }

            return RedirectToAction("Index", new { MaHangMauCha = MaHangMau, MaPhongBan = MaPhongBan });
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult UpdateMaCha(String MaHangMau, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_HangMau", "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["iID_MaHangMau"] = MaHangMau;
            ViewData["MaPhongBan"] = MaPhongBan;
            return View();
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateMaChaSubmit(String MaHangMau, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_HangMau", "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            int MaGiaTri = Convert.ToInt32(Request.Form["Update_iID_MaHangMau"].ToString());

            SqlCommand cmd;
            cmd = new SqlCommand();

            cmd.CommandText = "UPDATE BC_HangMau SET iID_MaHangMau_Cha = @iID_MaHangMau_Cha WHERE iID_MaHangMau=@iID_MaHangMau";
            cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaGiaTri);
            cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
            Connection.UpdateDatabase(cmd);
            cmd.Dispose();

            cmd = new SqlCommand();
            cmd.CommandText = "UPDATE BC_BangMau_HangMau SET iID_MaHangMau_Cha = @iID_MaHangMau_Cha WHERE iID_MaHangMau=@iID_MaHangMau";
            cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaGiaTri);
            cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
            Connection.UpdateDatabase(cmd);
            cmd.Dispose();

            cmd = new SqlCommand();
            cmd.CommandText = "UPDATE BC_Bang_Hang SET iID_MaHangMau_Cha = @iID_MaHangMau_Cha WHERE iID_MaHangMau=@iID_MaHangMau";
            cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaGiaTri);
            cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
            Connection.UpdateDatabase(cmd);
            cmd.Dispose();

            int MaHangMauChaCu = 0;
            cmd = new SqlCommand();
            cmd.CommandText = "SELECT iID_MaHangMau_Cha FROM BC_HangMau WHERE iID_MaHangMau=@MaHangMau";
            cmd.Parameters.AddWithValue("@MaHangMau", MaGiaTri);
            MaHangMauChaCu = Convert.ToInt32(Connection.GetValue(cmd, 0));
            cmd.Dispose();
            return RedirectToAction("Index", new { MaHangMauCha = MaHangMauChaCu, MaPhongBan = MaPhongBan });
        }

        public JavaScriptResult Edit_Fast_Submit(String ParentID)
        {
            
            String MaHangMau = Request.Form["txtMaHangMau"];
            String CongThuc = Request.Form["txtCongThuc"];
            String MaDiv = "divCongThuc" + MaHangMau;

            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandText = "UPDATE BC_HangMau SET sCongThuc = @sCongThuc  WHERE iID_MaHangMau=@iID_MaHangMau";
            cmd.Parameters.AddWithValue("@sCongThuc", CongThuc);
            cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
            if (Connection.UpdateDatabase(cmd) == 1)
            {
                cmd.Dispose();

                String strJ = "";
                strJ = String.Format("HideDialog('{0}');CallSuccess_CT('{1}','{2}');", ParentID, CongThuc, MaDiv);
                return JavaScript(strJ);
            }
            return null;
        }

        public ActionResult Get_GiaTri_CongThuc(String MaHangMau)
        {
            String CongThuc = "";
            SqlCommand cmd;
            DataTable dt = null;
            if (MaHangMau != "")
            {
                cmd = new SqlCommand("SELECT * FROM BC_HangMau WHERE iID_MaHangMau=@iID_MaHangMau");
                cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
                dt = Connection.GetDataTable(cmd);
                cmd.Dispose();
                if (dt.Rows.Count > 0)
                {
                    CongThuc = dt.Rows[0]["sCongThuc"].ToString();
                }
                dt.Dispose();
            }
            return Json(string.Concat(new object[] { CongThuc }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult get_dtChiTieuTren(String ParentID, String MaHangMau)
        {
            return Json(get_objChiTieuTren(ParentID, MaHangMau), JsonRequestBehavior.AllowGet);
        }

        public static String get_objChiTieuTren(String ParentID, String MaHangMau)
        {
            String strChiTieu = string.Empty;
            DataTable dt;
            if (string.IsNullOrEmpty(MaHangMau) == false)
            {
                SqlCommand cmd;
                cmd = new SqlCommand("SELECT iID_MaHangMau, sTenHang FROM BC_HangMau WHERE iID_MaHangMau_Cha = @iID_MaHangMau_Cha AND iID_MaHangMau <> @iID_MaHangMau ORDER BY iSTT");
                cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMau);
                cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
                dt = Connection.GetDataTable(cmd);
                dt.Rows.InsertAt(dt.NewRow(), 0);
                dt.Rows[0]["iID_MaHangMau"] = 0;
                dt.Rows[0]["sTenHang"] = "-- Chọn chỉ tiêu cần lấy --";
                SelectOptionList optChiTieu = new SelectOptionList(dt, "iID_MaHangMau", "sTenHang");
                string sChiTieu = dt.Rows[0]["iID_MaHangMau"].ToString();
                dt.Dispose();
                cmd.Dispose();

                strChiTieu += MyHtmlHelper.DropDownList(ParentID, optChiTieu, sChiTieu, "iID_MaHangMau", null, "onchange=\"ChonChiTieu(this.value)\" class=\"form-control\"");

                dt.Dispose();
                cmd.Dispose();
            }
            return strChiTieu;
        }

        public JsonResult get_dtChiTieu(String ParentID, String MaHangMau)
        {
            return Json(get_objChiTieu(ParentID, MaHangMau), JsonRequestBehavior.AllowGet);
        }
        public JsonResult get_dtChiTieu_DeQuy(String ParentID, String MaHangMau)
        {
            return Json(get_objChiTieu_DeQuy(ParentID, MaHangMau), JsonRequestBehavior.AllowGet);
        }

        public static String get_objChiTieu(String ParentID, String MaHangMau)
        {
            String strChiTieu = string.Empty;
            DataTable dt;
            if (string.IsNullOrEmpty(MaHangMau) == false)
            {
                SqlCommand cmd;
                cmd = new SqlCommand("SELECT * FROM BC_HangMau WHERE iID_MaHangMau_Cha = @iID_MaHangMau_Cha ORDER BY iSTT");
                cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMau);
                dt = Connection.GetDataTable(cmd);
                cmd.Dispose();

                if (dt != null)
                {
                    strChiTieu += "<table class='table table-bordered table-hover table-striped'>";
                    strChiTieu += "<tr>";
                    strChiTieu += "<th align='center' style='width: 5%'><b>Mã CT</b></th>";
                    strChiTieu += "<th align='center' style='width: 5%'><b>Mã chỉ tiêu</b></th>";
                    strChiTieu += "<th align='center'><b>Tên chỉ tiêu</b></th>";
                    strChiTieu += "<th align='center' style='width: 15%'><b>Công thức</b></th>";
                    strChiTieu += "<th align='center' style='width: 10%'><b>Đơn vị</b></th>";
                    strChiTieu += "<th align='center' style='width: 5%'><b>Tính tổng chỉ tiêu</b></th>";
                    strChiTieu += "<th align='center' style='width: 10px; text-align: center;'><b>" + MyHtmlHelper.CheckBox(ParentID, "", "chb_DeleteAll", "", "onclick=\"JavaScript:checkDeleteAll(this.form);\" title=\"Chọn tất cả\"") + "</b></th>";
                    strChiTieu += "</tr>";
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        DataRow R = dt.Rows[i];
                        String TenDonVi = "";
                        if (Convert.ToInt32(R["iID_MaDonVi"]) > 0)
                        {
                            cmd = new SqlCommand("SELECT sTen FROM BC_DonVi WHERE iID_MaDonVi=@iID_MaDonVi");
                            cmd.Parameters.AddWithValue("@iID_MaDonVi", R["iID_MaDonVi"]);
                            TenDonVi = Convert.ToString(Connection.GetValue(cmd, 0));
                            cmd.Dispose();
                        }

                        Boolean bTongHangCon = false;
                        bTongHangCon = Convert.ToBoolean(R["bTongHangCon"]);

                        String strXauChonGiaTri = MyHtmlHelper.CheckBox(ParentID, "", "iID_MaHangMau_" + i, "", "" + String.Format("value='{0}'", Convert.ToString(dt.Rows[i]["iID_MaHangMau"])) + "");

                        strChiTieu += "<tr>";
                        strChiTieu += "<td align='center'>" + R["iID_MaHangMau"] + "</td>";
                        strChiTieu += "<td>" + R["sMaHang"] + "</td>";
                        strChiTieu += "<td>" + R["sTenHang"] + "</td>";
                        strChiTieu += "<td>" + R["sCongThuc"] + "</td>";
                        strChiTieu += "<td>" + TenDonVi + "</td>";
                        strChiTieu += "<td align='center'>" + MyHtmlHelper.LabelCheckBox("", bTongHangCon.ToString(), "") + "</td>";
                        strChiTieu += "<td align='center' width='5%'>" + strXauChonGiaTri + "</td>";
                        strChiTieu += "</tr>";
                    }

                    strChiTieu += "</table>";
                }
                dt.Dispose();
            }
            return strChiTieu;
        }

        public static String get_objChiTieu_DeQuy(String ParentID, String MaHangMau)
        {
            int iThuTu = 0;

            String strChiTieu = string.Empty;
            strChiTieu += "<table class='table table-bordered table-hover table-striped'>";
            strChiTieu += "<tr>";
            strChiTieu += "<th align='center' style='width: 5%'><b>Mã CT</b></th>";
            strChiTieu += "<th align='center' style='width: 5%'><b>Mã chỉ tiêu</b></th>";
            strChiTieu += "<th align='center'><b>Tên chỉ tiêu</b></th>";
            strChiTieu += "<th align='center' style='width: 15%'><b>Công thức</b></th>";
            strChiTieu += "<th align='center' style='width: 10%'><b>Đơn vị</b></th>";
            strChiTieu += "<th align='center' style='width: 10%'><b>Tính tổng chỉ tiêu</b></th>";
            strChiTieu += "<th align='center' style='width: 10px; text-align: center;'><b>" + MyHtmlHelper.CheckBox(ParentID, "", "chb_DeleteAll", "", "onclick=\"JavaScript:checkDeleteAll(this.form);\" title=\"Chọn tất cả\"") + "</b></th>";
            strChiTieu += "</tr>";

            strChiTieu += LayXau_ChiTieu(ParentID, MaHangMau, 0, ref iThuTu);

            strChiTieu += "</table>";
            return strChiTieu;
        }

        public static string LayXau_ChiTieu(string ParentID, String MaHangMau, int Cap, ref int ThuTu)
        {
            string vR = "";

            SqlCommand cmd;
            String SQL = string.Format("SELECT * FROM BC_HangMau WHERE iID_MaHangMau_Cha={0} ORDER BY iSTT, iID_MaHangMau", MaHangMau);
            DataTable dt = Connection.GetDataTable(SQL);

            if (dt.Rows.Count > 0)
            {
                int i, tgThuTu;

                string strPG = "", strXauMenuCon, strDoanTrang = "";

                for (i = 1; i <= Cap; i++)
                {
                    strDoanTrang += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    ThuTu++;
                    tgThuTu = ThuTu;
                    DataRow Row = dt.Rows[i];

                    String TenDonVi = "";
                    if (Convert.ToInt32(Row["iID_MaDonVi"]) > 0)
                    {
                        cmd = new SqlCommand("SELECT sTen FROM BC_DonVi WHERE iID_MaDonVi=@iID_MaDonVi");
                        cmd.Parameters.AddWithValue("@iID_MaDonVi", Row["iID_MaDonVi"]);
                        TenDonVi = Convert.ToString(Connection.GetValue(cmd, ""));
                        cmd.Dispose();
                    }

                    String strXauChonGiaTri = MyHtmlHelper.CheckBox(ParentID, "", "iID_MaHangMau_" + i, "", "" + String.Format("value='{0}'", Convert.ToString(dt.Rows[i]["iID_MaHangMau"])) + "");
                    Boolean bTongHangCon = false;
                    bTongHangCon = Convert.ToBoolean(Row["bTongHangCon"]);

                    strXauMenuCon = LayXau_ChiTieu(ParentID, Convert.ToString(Row["iID_MaHangMau"]), Cap + 1, ref ThuTu);

                    strPG += string.Format("<tr>");
                    strPG += string.Format("<td>{0}{1}</td>", strDoanTrang, Row["iID_MaHangMau"]);
                    strPG += string.Format("<td>{0}{1}</td>", strDoanTrang, Row["sMaHang"]);
                    strPG += string.Format("<td>{0}{1}</td>", strDoanTrang, Row["sTenHang"]);
                    strPG += string.Format("<td>{0}</td>", Row["sCongThuc"]);
                    strPG += string.Format("<td>{0}</td>", TenDonVi);
                    strPG += string.Format("<td align='center'>{0}</td>", MyHtmlHelper.LabelCheckBox("", bTongHangCon.ToString(), ""));
                    strPG += string.Format("<td align='center' width='5%'>{0}</td>", strXauChonGiaTri);
                    strPG += string.Format("</tr>");
                    strPG += strXauMenuCon;
                }
                vR = String.Format("{0}", strPG);
            }
            dt.Dispose();
            return vR;
        }
    }
}