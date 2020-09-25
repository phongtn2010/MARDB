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

namespace APP0200025.Controllers
{
    public class BangMauController : Controller
    {
        // GET: BangMau
        [Authorize]
        public ActionResult Index(String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau", "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["MaPhongBan"] = MaPhongBan;
            ViewData["smenu"] = 187;
            return View();
        }
        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Search(string ParentID, String MaPhongBan)
        {
            string _TieuDe = CString.SafeString(Request.Form[ParentID + "_TieuDe"]);
            return RedirectToAction("Index", "BangMau", new { MaPhongBan = MaPhongBan, _TieuDe = _TieuDe });
        }
        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(String iID_MaBangMau, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["DuLieuMoi"] = "0";
            if (string.IsNullOrEmpty(iID_MaBangMau))
            {
                ViewData["DuLieuMoi"] = "1";
            }
            ViewData["MaPhongBan"] = MaPhongBan;
            ViewData["iID_MaBangMau"] = CString.SafeString(iID_MaBangMau);
            ViewData["smenu"] = 187;
            return View();
        }

        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult Edit(String ParentID, String iID_MaBangMau, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            int i, j;
            NameValueCollection arrLoi = new NameValueCollection();
            String TenBang = Convert.ToString(Request.Form[ParentID + "_sTenBang"]);
            int LoaiBaoCao = Convert.ToInt32(Request.Form[ParentID + "_iLoaiBaoCao"]);
            int MaPhongBanLay = Convert.ToInt32(Request.Form[ParentID + "_iID_MaPhongBan"]);
            int iID_MaBangNhapLieu = Convert.ToInt32(Request.Form[ParentID + "_iID_MaBangNhapLieu"]);
            string DuLieuMoi = CString.SafeString(Request.Form[ParentID + "_DuLieuMoi"]);
            String tg = Convert.ToString(Request.Form[ParentID + "_txt"]);

            if (TenBang == string.Empty || TenBang == "")
            {
                arrLoi.Add("err_sTenBang", "Bạn chưa nhập tên báo cáo mẫu!");
            }
            if (LoaiBaoCao <= 0)
            {
                arrLoi.Add("err_iLoaiBaoCao", "Bạn chưa chọn loại báo cáo!");
            }
            if (MaPhongBanLay <= 0)
            {
                arrLoi.Add("err_iPhongBan", "Bạn phải chọn phòng ban!");
            }
            if (iID_MaBangNhapLieu < 0)
            {
                arrLoi.Add("err_iID_MaBangNhapLieu", "Bạn phải chọn bảng mẫu để nhập liệu hay báo cáo!");
            }
            if (arrLoi.Count > 0)
            {
                for (i = 0; i <= arrLoi.Count - 1; i++)
                {
                    ModelState.AddModelError(ParentID + "_" + arrLoi.GetKey(i), arrLoi[i]);
                }
                base.ViewData["DuLieuMoi"] = "0";
                if (string.IsNullOrEmpty(iID_MaBangMau))
                {
                    ViewData["DuLieuMoi"] = "1";
                }
                ViewData["iID_MaBangMau"] = CString.SafeString(iID_MaBangMau);
                ViewData["smenu"] = 187;
                return View();
            }

            try
            {
                String strChuoiBangCungNhau = "";
                if (String.IsNullOrEmpty(tg) == false)
                {
                    String[] arr = tg.Split(',');
                    for (i = 0; i < arr.Length; i++)
                    {
                        String[] arr1 = arr[i].Split(';');
                        if (arr1.Length > 2)
                        {
                            for (j = 0; j < arr1.Length; j++)
                            {
                                if (j == 0)
                                {
                                    strChuoiBangCungNhau += arr1[j] + ";";
                                }
                            }
                        }
                    }
                }

                Bang bang = new Bang("BC_BangMau");
                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                bang.TruyenGiaTri(ParentID, Request.Form);
                if (strChuoiBangCungNhau != "")
                {
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaBangCungNhau", strChuoiBangCungNhau);
                }
                else
                {
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaBangCungNhau", DBNull.Value);
                }

                if (bang.CmdParams.Parameters.IndexOf("@sLoaiNguoiDuocSua") >= 0)
                {
                    bang.CmdParams.Parameters["@sLoaiNguoiDuocSua"].Value = String.Format(",{0},", bang.CmdParams.Parameters["@sLoaiNguoiDuocSua"].Value);
                }
                if (DuLieuMoi == "0")
                {
                    //Sua
                    bang.DuLieuMoi = false;
                    bang.GiaTriKhoa = iID_MaBangMau;
                }
                else
                {
                    //Them moi
                    bang.DuLieuMoi = true;

                    //Cap nhap them STT tu dong
                    int intSTT = CBangMau.Get_Max_BangMau();
                    bang.CmdParams.Parameters.AddWithValue("@iSTT", intSTT + 1);
                }
                bang.Save();

                return RedirectToAction("Index", new { MaPhongBan = MaPhongBan });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Errors", new { sMess = ex.ToString() });
            }
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Delete(string iID_MaBangMau)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau", "Delete") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            try
            {
                if (string.IsNullOrEmpty(iID_MaBangMau) == false)
                {
                    SqlCommand cmd;

                    //Xoa danh muc san pham
                    cmd = new SqlCommand("DELETE FROM BC_BangMau WHERE iID_MaBangMau=@iID_MaBangMau");
                    cmd.Parameters.AddWithValue("@iID_MaBangMau", CString.SafeString(iID_MaBangMau));
                    Connection.UpdateDatabase(cmd);
                    cmd.Dispose();

                    cmd = new SqlCommand("DELETE FROM BC_BangMau_HangMau WHERE iID_MaBangMau=@iID_MaBangMau");
                    cmd.Parameters.AddWithValue("@iID_MaBangMau", iID_MaBangMau);
                    Connection.UpdateDatabase(cmd);
                    cmd.Dispose();

                    cmd = new SqlCommand("DELETE FROM BC_BangMau_HangMau_DonVi WHERE iID_MaBangMau=@iID_MaBangMau");
                    cmd.Parameters.AddWithValue("@iID_MaBangMau", iID_MaBangMau);
                    Connection.UpdateDatabase(cmd);
                    cmd.Dispose();

                    cmd = new SqlCommand("DELETE FROM BC_BangMau_CotMau WHERE iID_MaBangMau=@iID_MaBangMau");
                    cmd.Parameters.AddWithValue("@iID_MaBangMau", iID_MaBangMau);
                    Connection.UpdateDatabase(cmd);
                    cmd.Dispose();

                    cmd = new SqlCommand("DELETE FROM BC_BangMau_CotMau_DonVi WHERE iID_MaBangMau=@iID_MaBangMau");
                    cmd.Parameters.AddWithValue("@iID_MaBangMau", iID_MaBangMau);
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

        [Authorize]
        public ActionResult DeleteHangMau(String MaBangMau, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau", "Delete") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("DELETE FROM BC_BangMau_HangMau WHERE iID_MaBangMau=@iID_MaBangMau");
                cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                Connection.UpdateDatabase(cmd);
                cmd.Dispose();

                cmd = new SqlCommand("DELETE FROM BC_BangMau_HangMau_DonVi WHERE iID_MaBangMau=@iID_MaBangMau");
                cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                Connection.UpdateDatabase(cmd);
                cmd.Dispose();

                return RedirectToAction("Index", new { MaPhongBan = MaPhongBan });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Errors", new { sMess = ex.ToString() });
            }
        }

        [Authorize]
        public ActionResult Sort(String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["smenu"] = 187;
            ViewData["MaPhongBan"] = MaPhongBan;
            return View();
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Sort(String ParentID, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            string strOrder = Request.Form["hiddenOrder"].ToString();
            try
            {
                if (strOrder.IndexOf('$') > 0)
                {
                    String[] arrTG = strOrder.Split('$');
                    int i;
                    SqlCommand cmd;
                    for (i = 0; i < arrTG.Length - 1; i++)
                    {
                        cmd = new SqlCommand();
                        cmd.CommandText = "Update BC_BangMau SET iSTT=@iSTT WHERE iID_MaBangMau=@iID_MaBangMau";
                        cmd.Parameters.AddWithValue("@iSTT", i);
                        cmd.Parameters.AddWithValue("@iID_MaBangMau", arrTG[i]);
                        Connection.UpdateDatabase(cmd);
                        cmd.Dispose();
                    }
                }
                return RedirectToAction("Index", new { MaPhongBan = MaPhongBan });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Errors", new { sMess = ex.ToString() });
            }

        }

        [Authorize]
        public ActionResult Detail(String MaBangMau, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["smenu"] = 187;
            ViewData["MaBangMau"] = MaBangMau;
            ViewData["MaPhongBan"] = MaPhongBan;
            return View();
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Detail(String ParentID, String MaBangMau, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
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
            cmd.Parameters.AddWithValue("@iID_MaDonVi", -2);
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
                        cmd.Parameters.AddWithValue("@iID_MaDonVi", -2);
                        cmd.Parameters.AddWithValue("@oGiaTri", value);
                        Connection.UpdateDatabase(cmd);
                        cmd.Dispose();
                    }
                }
            }

            return RedirectToAction("Index", new { MaPhongBan = MaPhongBan });
        }

        public JsonResult getAjax(String ParentID, String MaBangMau, String MaPhongBan, String LoaiBaoCao, int bThuocDonVi)
        {
            return Json(getObj(ParentID, MaBangMau, MaPhongBan, LoaiBaoCao, bThuocDonVi), JsonRequestBehavior.AllowGet);
        }

        public static String getObj(String ParentID, String MaBangMau, String MaPhongBan, String LoaiBaoCao, int bThuocDonVi)
        {
            StringBuilder builder = new StringBuilder();

            SqlCommand cmd;
            DataTable dtBangMau;

            String SQL = "";
            String DK = "1=1";
            if (!String.IsNullOrEmpty(MaPhongBan) && MaPhongBan != "0")
            {
                DK += " AND iID_MaPhongBan = '" + MaPhongBan + "'";
            }
            if (!String.IsNullOrEmpty(LoaiBaoCao) && LoaiBaoCao != "0")
            {
                DK += " AND iLoaiBaoCao = '" + LoaiBaoCao + "'";
            }
            if (bThuocDonVi != -1)
            {
                DK += " AND bXemTheoDonVi = '" + bThuocDonVi + "'";
            }

            SQL = "SELECT * FROM BC_BangMau WHERE " + DK + " ORDER BY iSTT";

            cmd = new SqlCommand(SQL);
            dtBangMau = Connection.GetDataTable(cmd);
            cmd.Dispose();

            String strDanhSachMaBangCungNhau = "";
            if (!String.IsNullOrEmpty(MaBangMau) && MaBangMau != "0")
            {
                cmd = new SqlCommand();
                cmd.CommandText = "SELECT iID_MaBangCungNhau FROM BC_BangMau WHERE iID_MaBangMau=@iID_MaBangMau";
                cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                strDanhSachMaBangCungNhau = Convert.ToString(Connection.GetValue(cmd, ""));
                cmd.Dispose();
            }

            String[] arrDanhSachMaBangCungNhau = strDanhSachMaBangCungNhau.Split(';');

            int i, j;
            String strXem, txtTenBang;
            builder.Append("<table class='table table-bordered table-hover table-striped'>");
            builder.Append("<tr>");
            builder.Append("<th width='20px' align='center'><b>STT</b></th>");
            builder.Append("<th align='center'><b>Tên báo cáo</b></th>");
            builder.Append("<th width='60px' align='center'>");
            builder.Append("<input type='checkbox' id='" + ParentID + "_checkall_xem' onclick=\"setCheckboxes('xem')\" />");
            builder.Append("</th>");
            builder.Append("</tr>");
            for (i = 0; i < dtBangMau.Rows.Count; i++)
            {
                strXem = "";
                txtTenBang = Convert.ToString(dtBangMau.Rows[i]["iID_MaBangMau"]) + ";";

                for (j = 0; j < arrDanhSachMaBangCungNhau.Length - 1; j++)
                {
                    if (arrDanhSachMaBangCungNhau[j] != "")
                    {
                        if (Convert.ToString(arrDanhSachMaBangCungNhau[j]) == Convert.ToString(dtBangMau.Rows[i]["iID_MaBangMau"]))
                        {
                            strXem = " checked='checked'";
                            txtTenBang += "0;";
                        }
                    }
                }

                if (i % 2 == 0)
                {
                    builder.Append("<tr >");
                }
                else
                {
                    builder.Append("<tr style='background-color:#FFC'>");
                }

                builder.Append("<td align='center'>" + (i + 1) + "</td>");
                builder.Append("<td align='left'>");
                builder.Append("" + dtBangMau.Rows[i]["sTenBang"] + "");
                builder.Append("<input type='hidden' name='" + ParentID + "_txt' id='" + ParentID + "_txt_" + i + "' value='" + txtTenBang + "'/>");
                builder.Append("</td>");
                builder.Append("<td align='center'>");
                builder.Append("" + String.Format("<input type=\"checkbox\" name=\"{0}_xem\" id=\"{0}_xem_{1}\" onclick=\"changeCheckbox({1})\" {2}/>", ParentID, i, strXem) + "");
                builder.Append("</td>");
                builder.Append("</tr>");
            }
            builder.Append("</table>");
            dtBangMau.Dispose();

            return builder.ToString();
        }
    }
}