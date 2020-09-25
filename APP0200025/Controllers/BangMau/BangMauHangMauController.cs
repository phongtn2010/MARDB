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
    public class BangMauHangMauController : Controller
    {
        // GET: BangMauHangMau
        [Authorize]
        public ActionResult Index(String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_HangMau", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["MaPhongBan"] = MaPhongBan;
            return View();
        }

        [Authorize]
        public ActionResult Detail(String MaBangMau, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_HangMau", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["MaBangMau"] = MaBangMau;
            ViewData["MaPhongBan"] = MaPhongBan;
            return View();
        }

        [Authorize]
        public ActionResult AddNews(String MaBangMau, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_HangMau", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["MaBangMau"] = MaBangMau;
            ViewData["MaPhongBan"] = MaPhongBan;
            return View();
        }

        [Authorize]
        public ActionResult Create(String MaBangMau, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_HangMau", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["MaBangMau"] = MaBangMau;
            ViewData["MaPhongBan"] = MaPhongBan;
            return View();
        }

        [Authorize]
        public ActionResult Edit(String MaBangMau_HangMau, String MaBangMau, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_HangMau", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["DuLieuMoi"] = "0";
            ViewData["MaBangMau_HangMau"] = MaBangMau_HangMau;
            ViewData["MaBangMau"] = MaBangMau;
            return View();
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddNewsSubmit(String ParentID, String MaPhongBan, String MaBangMau, int MaHangMauCha)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_HangMau", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            int i, j;
            NameValueCollection data = Request.Form;
            String MaHangMau = "";
            for (i = 0; i <= data.Count - 1; i++)
            {
                String req = Convert.ToString(data.AllKeys[i]);
                j = req.IndexOf("_Kem");
                int h = req.IndexOf("AddNews_iID_MaHangMau_");
                if (j < 0 && h >= 0)
                {
                    MaHangMau += Request.Form[req] + ",";
                }
            }
            DataTable dt = null;
            SqlCommand cmd;
            String MaHangMau_List = "," + MaHangMau;
            String SQL;

            String[] arr = MaHangMau_List.Split(',');

            for (i = 0; i < arr.Length; i++)
            {
                if (arr[i] != "")
                {
                    int MaHangMauCha_Lay = HangMauModels.Get_MahangMau_Cha(Convert.ToInt32(arr[i].ToString()));

                    Boolean DaCo = false;
                    cmd = new SqlCommand();
                    cmd.CommandText = "SELECT iID_MaBangMau_HangMau, iID_MaHangMau FROM BC_BangMau_HangMau WHERE iID_MaBangMau = @iID_MaBangMau AND iID_MaHangMau_Cha=@iID_MaHangMau_Cha";
                    cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                    cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMauCha_Lay);
                    DataTable dtBangMau = Connection.GetDataTable(cmd);
                    cmd.Dispose();
                    for (j = 0; j <= dtBangMau.Rows.Count - 1; j++)
                    {
                        if (arr[i] == Convert.ToString(dtBangMau.Rows[j]["iID_MaHangMau"]))
                        {
                            DaCo = true;
                            break;
                        }
                    }
                    if (DaCo == false)
                    {
                        Bang bang = new Bang("BC_BangMau_HangMau");
                        bang.MaNguoiDungSua = User.Identity.Name;
                        bang.IPSua = Request.UserHostAddress;
                        bang.CmdParams.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                        bang.CmdParams.Parameters.AddWithValue("@iID_MaHangMau", arr[i].ToString());
                        bang.CmdParams.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMauCha_Lay);
                        bang.Save();
                    }
                }
            }

            //Cập nhật mã hàng mẫu gốc cho bảng mẫu
            BangMauModels.CapNhapMaHangMauChaGoc(MaBangMau);

            return RedirectToAction("Create", new { MaBangMau = MaBangMau, MaHangMauCha = MaHangMauCha, MaPhongBan = MaPhongBan });
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateSubmit(String ParentID, String MaPhongBan, String MaBangMau, int MaHangMauCha)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_HangMau", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            int i, j;
            NameValueCollection data = Request.Form;
            String MaHangMau = "";
            for (i = 0; i <= data.Count - 1; i++)
            {
                String req = Convert.ToString(data.AllKeys[i]);
                j = req.IndexOf("_Kem");
                int h = req.IndexOf("Create_iID_MaHangMau_");
                if (j < 0 && h >= 0)
                {
                    MaHangMau += Request.Form[req] + ",";
                }
            }
            DataTable dt = null;
            SqlCommand cmd;
            String strNhungMaDangCo = Request.Form["txtNhungMaDangCo"];
            if (String.IsNullOrEmpty(strNhungMaDangCo))
            {
                strNhungMaDangCo = "";
            }
            String[] arrMaDaCo = strNhungMaDangCo.Split(',');
            String MaHangMau_List = "," + MaHangMau;
            String SQL;

            String[] arr = MaHangMau_List.Split(',');

            for (i = 0; i < arrMaDaCo.Length; i++)
            {
                Boolean DaBiXoa = true;

                for (j = 0; j < arr.Length; j++)
                {
                    if (arrMaDaCo[i] == arr[j])
                    {
                        DaBiXoa = false;
                        break;
                    }
                }
                if (DaBiXoa)
                {
                    MaHangMau = arrMaDaCo[i];
                    SQL = String.Format("SELECT Count(*) FROM BC_BangMau_HangMau WHERE iID_MaHangMau_Cha={0} AND iID_MaBangMau = '{1}'", MaHangMau, MaBangMau);
                    if (Convert.ToInt32(Connection.GetValue(SQL, 0)) > 0)
                    {
                        //Vẫn còn mục con
                        return RedirectToAction("Create", new { MaBangMau = MaBangMau, MaHangMauCha = MaHangMauCha });
                    }
                    else
                    {
                        cmd = new SqlCommand("DELETE FROM BC_BangMau_HangMau WHERE iID_MaHangMau=@iID_MaHangMau AND iID_MaBangMau = @iID_MaBangMau AND iID_MaHangMau_Cha=@iID_MaHangMau_Cha");
                        cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
                        cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                        cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMauCha);
                        Connection.UpdateDatabase(cmd);
                        cmd.Dispose();
                    }
                }
            }

            for (i = 0; i < arr.Length; i++)
            {
                if (arr[i] != "")
                {
                    Boolean DaCo = false;
                    cmd = new SqlCommand();
                    cmd.CommandText = "SELECT iID_MaBangMau_HangMau, iID_MaHangMau FROM BC_BangMau_HangMau WHERE iID_MaBangMau = @iID_MaBangMau AND iID_MaHangMau_Cha=@iID_MaHangMau_Cha";
                    cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                    cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMauCha);
                    DataTable dtBangMau = Connection.GetDataTable(cmd);
                    cmd.Dispose();
                    for (j = 0; j <= dtBangMau.Rows.Count - 1; j++)
                    {
                        if (arr[i] == Convert.ToString(dtBangMau.Rows[j]["iID_MaHangMau"]))
                        {
                            DaCo = true;
                            break;
                        }
                    }
                    if (DaCo == false)
                    {
                        Bang bang = new Bang("BC_BangMau_HangMau");
                        bang.MaNguoiDungSua = User.Identity.Name;
                        bang.IPSua = Request.UserHostAddress;
                        bang.CmdParams.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                        bang.CmdParams.Parameters.AddWithValue("@iID_MaHangMau", arr[i].ToString());
                        bang.CmdParams.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMauCha);
                        bang.Save();
                    }
                }
            }

            //Cập nhật mã hàng mẫu gốc cho bảng mẫu
            BangMauModels.CapNhapMaHangMauChaGoc(MaBangMau);

            return RedirectToAction("Create", new { MaBangMau = MaBangMau, MaHangMauCha = MaHangMauCha, MaPhongBan = MaPhongBan });
        }


        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditSubmit(String ParentID, String MaBangMau_HangMau, String MaBangMau)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_HangMau", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            Bang bang = new Bang("BC_BangMau_HangMau");
            bang.MaNguoiDungSua = User.Identity.Name;
            bang.IPSua = Request.UserHostAddress;
            bang.TruyenGiaTri(ParentID, Request.Form);
            bang.Save();
            return RedirectToAction("Detail", new { MaBangMau = MaBangMau });
        }

        [Authorize]
        public ActionResult Delete(String MaBangMau_HangMau, String MaBangMau, String MaHangMauCha, String MaHangMau)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_HangMau", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            Int32 MaHangMauXoa = Convert.ToInt32(MaHangMau);
            Int32 MaBangMauXoa = Convert.ToInt32(MaBangMau);
            BangMauHangMauModels.XoaBangMauBangMau(MaHangMauXoa, MaBangMauXoa);

            SqlCommand cmd;
            cmd = new SqlCommand("DELETE FROM BC_BangMau_HangMau_DonVi WHERE iID_MaBangMau=@iID_MaBangMau AND iID_MaBangMau_HangMau=@iID_MaBangMau_HangMau");
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            cmd.Parameters.AddWithValue("@iID_MaBangMau_HangMau", MaBangMau_HangMau);
            Connection.UpdateDatabase(cmd);
            cmd.Dispose();

            Bang bang = new Bang("BC_BangMau_HangMau");
            bang.MaNguoiDungSua = User.Identity.Name;
            bang.IPSua = Request.UserHostAddress;
            bang.GiaTriKhoa = MaBangMau_HangMau;
            bang.Delete();
            return RedirectToAction("Detail", new { MaBangMau = MaBangMau, MaHangMauCha = MaHangMauCha });
        }

        [Authorize]
        public ActionResult CreateNhanhHangMau(String MaBangMau, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_HangMau", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["MaBangMau"] = MaBangMau;
            ViewData["MaPhongBan"] = MaPhongBan;
            return View();
        }

        [Authorize]
        public ActionResult CreateNhanhHangMauSubmit(String ParentID, String MaBangMau)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_HangMau", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            String MaBangMauLayDuLieu;
            MaBangMauLayDuLieu = Convert.ToString(Request.Form[ParentID + "_iID_MaBangMau"]);

            int i;
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM BC_BangMau_HangMau WHERE iID_MaBangMau=@iID_MaBangMau";
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMauLayDuLieu);
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();
            if (dt.Rows.Count > 0)
            {
                DataRow R;
                cmd = new SqlCommand("DELETE FROM BC_BangMau_HangMau WHERE iID_MaBangMau=@iID_MaBangMau");
                cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                Connection.UpdateDatabase(cmd);
                cmd.Dispose();
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    R = dt.Rows[i];
                    Bang bang = new Bang("BC_BangMau_HangMau");
                    bang.MaNguoiDungSua = User.Identity.Name;
                    bang.IPSua = Request.UserHostAddress;
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaHangMau", R["iID_MaHangMau"]);
                    if (R["iID_MaHangMau_Cha"].ToString() != "0")
                    {
                        bang.CmdParams.Parameters.AddWithValue("@iID_MaHangMau_Cha", R["iID_MaHangMau_Cha"]);
                    }
                    bang.Save();
                }


                //Cập nhật mã hàng mẫu gốc cho bảng mẫu
                BangMauModels.CapNhapMaHangMauChaGoc(MaBangMau);
            }

            return RedirectToAction("Detail", new { MaBangMau = MaBangMau });
        }

        [Authorize]
        public ActionResult CreateNhanhHangMauNoDelete(String MaBangMau, String MaPhongBan)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_HangMau", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["MaBangMau"] = MaBangMau;
            ViewData["MaPhongBan"] = MaPhongBan;
            return View();
        }

        [Authorize]
        public ActionResult CreateNhanhHangMauNoDeleteSubmit(String ParentID, String MaBangMau)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "BC_BangMau_HangMau", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            String MaBangMauLayDuLieu;
            MaBangMauLayDuLieu = Request.Form[ParentID + "_iID_MaBangMau"];

            int i, j;
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM BC_BangMau_HangMau WHERE iID_MaBangMau=@iID_MaBangMau";
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMauLayDuLieu);
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM BC_BangMau_HangMau WHERE iID_MaBangMau=@iID_MaBangMau";
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            DataTable dtHangMauDaCo = Connection.GetDataTable(cmd);
            cmd.Dispose();

            if (dt.Rows.Count > 0)
            {
                DataRow R, R1;

                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    Boolean bChiTieuMau = false;
                    R = dt.Rows[i];

                    for (j = 0; j < dtHangMauDaCo.Rows.Count; j++)
                    {
                        R1 = dtHangMauDaCo.Rows[j];

                        if ((Convert.ToInt32(R["iID_MaHangMau"]) == Convert.ToInt32(R1["iID_MaHangMau"])) && (Convert.ToInt32(R["iID_MaHangMau_Cha"]) == Convert.ToInt32(R1["iID_MaHangMau_Cha"])))
                        {
                            bChiTieuMau = true;
                            break;
                        }
                    }

                    if (bChiTieuMau == false)
                    {
                        Bang bang = new Bang("BC_BangMau_HangMau");
                        bang.MaNguoiDungSua = User.Identity.Name;
                        bang.IPSua = Request.UserHostAddress;
                        bang.CmdParams.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                        bang.CmdParams.Parameters.AddWithValue("@iID_MaHangMau", R["iID_MaHangMau"]);
                        if (R["iID_MaHangMau_Cha"].ToString() != "0")
                        {
                            bang.CmdParams.Parameters.AddWithValue("@iID_MaHangMau_Cha", R["iID_MaHangMau_Cha"]);
                        }
                        bang.Save();
                    }
                }

                //Cập nhật mã hàng mẫu gốc cho bảng mẫu
                BangMauModels.CapNhapMaHangMauChaGoc(MaBangMau);
            }

            return RedirectToAction("Detail", new { MaBangMau = MaBangMau });
        }

        public JsonResult get_dtChiTieu_DeQuy(String ParentID, String MaHangMau)
        {
            return Json(get_objChiTieu_DeQuy(ParentID, MaHangMau), JsonRequestBehavior.AllowGet);
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