using System;
using System.Text;
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
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;


namespace  DATA0200025
{
    public class TieuDeBaoCaoMprModel
    {
        public TieuDeBaoCaoMprModel(String MaNguoiDung, String MaDonVi, String strThoiGian, int LoaiBaoCao, int MaPhongBan, int MaBangKieuNhapLieu)
        {
            this.MaNguoiDung = MaNguoiDung;
            this.MaDonVi = MaDonVi;
            this.strThoiGian = strThoiGian;
            this.LoaiBaoCao = LoaiBaoCao;
            this.MaPhongBan = MaPhongBan;
            this.MaBangKieuNhapLieu = MaBangKieuNhapLieu;
        }
        public String MaNguoiDung { get; set; }
        public String MaDonVi { get; set; }
        public String strThoiGian { get; set; }
        public int LoaiBaoCao { get; set; }
        public int MaPhongBan { get; set; }
        public int MaBangKieuNhapLieu { get; set; }
    }

    public class ThongSoO
    {
        public String CongThuc = "";
        public List<int> DanhSachCongThuc_h = new List<int>();
        public List<int> DanhSachCongThuc_c = new List<int>();
        public List<int> DanhSachO_h = new List<int>();
        public List<int> DanhSachO_c = new List<int>();
    }

    public class BangModels
    {
        public static String DauCachHang = "#*";
        public static String DauCachO = "##";

        //LoaiBaoCao = 1: Ngay;  = 2: Thang;  3: Quy (Chua lam);   4: Nam
        //LoaiLuyKe = 1: Lũy kế tháng, 2: Lũy kế năm
        public static String LayMaBang(String MaBangMau, Boolean XemTheoDonVi, String MaDonVi, int LoaiBaoCao, DateTime NgayBaoCao, int LoaiLuyKe)
        {
            String DK = "";
            DK = "iID_MaBangMau=@iID_MaBangMau AND iLoaiBaoCao = @iLoaiBaoCao";
            SqlCommand cmd;
            object tg;

            if (LoaiLuyKe == 1)
            {
                //Lấy ngày gần nhất trong tháng có báo cáo
                cmd = new SqlCommand();
                cmd.CommandText = "SELECT Max(dNgayBaoCao) FROM BC_Bang WHERE iID_MaBangMau=@iID_MaBangMau";
                cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                if (XemTheoDonVi)
                {
                    cmd.CommandText += " AND (bXemTheoDonVi=1 AND iID_MaDonVi=@iID_MaDonVi)";
                    cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                }
                else
                {
                    cmd.CommandText += " AND (bXemTheoDonVi=0)";
                }
                cmd.CommandText += " AND (CONVERT(varchar(7),dNgayBaoCao,111)=@sThangBaoCao) AND (CONVERT(varchar(10),dNgayBaoCao,111)<@sNgayBaoCao)";
                cmd.Parameters.AddWithValue("@sThangBaoCao", NgayBaoCao.ToString("yyyy/MM"));
                cmd.Parameters.AddWithValue("@sNgayBaoCao", NgayBaoCao.ToString("yyyy/MM/dd"));
                tg = Connection.GetValue(cmd, null);
                if (tg == null)
                {
                    return "";
                }
                NgayBaoCao = Convert.ToDateTime(tg);
                cmd.Dispose();
            }
            else if (LoaiLuyKe == 2)
            {
                //Lấy ngày gần nhất trong năm có báo cáo
                cmd = new SqlCommand();
                cmd.CommandText = "SELECT Max(dNgayBaoCao) FROM BC_Bang WHERE iID_MaBangMau=@iID_MaBangMau";
                cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                if (XemTheoDonVi)
                {
                    cmd.CommandText += " AND (bXemTheoDonVi=1 AND iID_MaDonVi=@iID_MaDonVi)";
                    cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                }
                else
                {
                    cmd.CommandText += " AND (bXemTheoDonVi=0)";
                }
                cmd.CommandText += " AND (CONVERT(varchar(4),dNgayBaoCao,111)=@sNamBaoCao) AND (CONVERT(varchar(7),dNgayBaoCao,111)<@sThangBaoCao)";
                cmd.Parameters.AddWithValue("@sThangBaoCao", NgayBaoCao.ToString("yyyy/MM"));
                cmd.Parameters.AddWithValue("@sNamBaoCao", NgayBaoCao.ToString("yyyy"));
                tg = Connection.GetValue(cmd, null);
                if (tg == null)
                {
                    return "";
                }
                NgayBaoCao = Convert.ToDateTime(tg);
                cmd.Dispose();
            }



            cmd = new SqlCommand();

            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            cmd.Parameters.AddWithValue("@iLoaiBaoCao", LoaiBaoCao);
            switch (LoaiBaoCao)
            {
                case 1:
                    DK += " AND (CONVERT(varchar(10),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", NgayBaoCao.ToString("yyyy/MM/dd"));
                    break;

                case 2:
                case 3:
                case 5:
                    DK += " AND (CONVERT(varchar(7),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", NgayBaoCao.ToString("yyyy/MM"));
                    break;

                case 4:
                    DK += " AND (CONVERT(varchar(4),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", NgayBaoCao.ToString("yyyy"));
                    break;
            }
            if (XemTheoDonVi)
            {
                DK += " AND (bXemTheoDonVi=1 AND iID_MaDonVi=@iID_MaDonVi)";
                cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
            }
            else
            {
                DK += " AND (bXemTheoDonVi=0)";
            }
            cmd.CommandText = String.Format("SELECT iID_MaBang FROM BC_Bang WHERE {0}", DK);
            return Connection.GetValueString(cmd, "");
        }

        public static String TaoBangMoi(String MaBangMau, String MaDonVi, int LoaiBaoCao, DateTime NgayBaoCao, String strPath, String sUserName, String sIP)
        {
            String MaBang = "";
            SqlCommand cmd;
            String TenBang = "", CongThuc = "";
            Boolean XemTheoDonVi = false;

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM BC_BangMau WHERE iID_MaBangMau=@iID_MaBangMau";
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            DataTable dtBangMau = Connection.GetDataTable(cmd);
            cmd.Dispose();

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM BC_BangMau_CongThuc WHERE iID_MaBangMau=@iID_MaBangMau";
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            DataTable dtCongThuc = Connection.GetDataTable(cmd);
            cmd.Dispose();

            TenBang = String.Format("{0}", dtBangMau.Rows[0]["sTenBang"]);
            CongThuc = String.Format("{0}", dtBangMau.Rows[0]["sCongThuc"]);
            XemTheoDonVi = Convert.ToBoolean(dtBangMau.Rows[0]["bXemTheoDonVi"]);


            MaBang = LayMaBang(MaBangMau, XemTheoDonVi, MaDonVi, LoaiBaoCao, NgayBaoCao, -1);
            if (String.IsNullOrEmpty(MaBang) == false)
            {
                return MaBang;
            }

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM BC_BangMau_CotMau WHERE iID_MaBangMau =@iID_MaBangMau ORDER BY iSTT";
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            DataTable dtCotMau = Connection.GetDataTable(cmd);
            cmd.Dispose();

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM BC_BangMau_HamMau WHERE (iID_MaBangMau IS NULL) OR (iID_MaBangMau=@iID_MaBangMau)";
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            DataTable dtHamMau = Connection.GetDataTable(cmd);
            cmd.Dispose();

            DataTable dtDonVi = null;
            cmd = new SqlCommand();
            cmd = new SqlCommand();

            if (XemTheoDonVi)
            {
                cmd.CommandText = "SELECT * " +
                                  " FROM BC_DonVi " +
                                  " WHERE (iID_MaDonVi = @iID_MaDonVi)" +
                                  " ORDER BY iSTT;";
                cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                dtDonVi = Connection.GetDataTable(cmd);
                //TenBang = String.Format("{0} _ {1}", dtDonVi.Rows[0]["sTen"], TenBang);
            }
            else
            {
                cmd.CommandText = "SELECT * " +
                                  " FROM BC_DonVi " +
                                  " ORDER BY iSTT;";
                dtDonVi = Connection.GetDataTable(cmd);
            }

            cmd.Dispose();

            Bang bang = new Bang("BC_Bang");
            bang.MaNguoiDungSua = sUserName;
            bang.IPSua = sIP;
            bang.DuLieuMoi = true;
            bang.CmdParams.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            bang.CmdParams.Parameters.AddWithValue("@iID_MaHangMauGoc", dtBangMau.Rows[0]["iID_MaHangMauGoc"]);
            bang.CmdParams.Parameters.AddWithValue("@iSTT", dtBangMau.Rows[0]["iSTT"]);
            bang.CmdParams.Parameters.AddWithValue("@sLoaiNguoiDuocSua", dtBangMau.Rows[0]["sLoaiNguoiDuocSua"]);
            bang.CmdParams.Parameters.AddWithValue("@iID_MaPhongBan", dtBangMau.Rows[0]["iID_MaPhongBan"]);
            bang.CmdParams.Parameters.AddWithValue("@iID_MaBangNhapLieu", dtBangMau.Rows[0]["iID_MaBangNhapLieu"]);
            bang.CmdParams.Parameters.AddWithValue("@bPhanTrang", dtBangMau.Rows[0]["bPhanTrang"]);
            bang.CmdParams.Parameters.AddWithValue("@bXemTheoDonVi", XemTheoDonVi);
            if (XemTheoDonVi)
            {
                bang.CmdParams.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
            }
            bang.CmdParams.Parameters.AddWithValue("@iLoaiBaoCao", LoaiBaoCao);
            bang.CmdParams.Parameters.AddWithValue("@dNgayBaoCao", NgayBaoCao);
            bang.CmdParams.Parameters.AddWithValue("@sTenBang", TenBang);
            bang.CmdParams.Parameters.AddWithValue("@sCongThuc", CongThuc);
            bang.CmdParams.Parameters.AddWithValue("@iID_MaBangMau_LayGhiChu", dtBangMau.Rows[0]["iID_MaBangMau_LayGhiChu"]);
            MaBang = String.Format("{0}", bang.Save());

            //Lấy thông tin của bảng tạo ra
            DataTable dtBang;
            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM BC_Bang WHERE iID_MaBang = @iID_MaBang";
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            dtBang = Connection.GetDataTable(cmd);
            cmd.Dispose();


            int i = 0, ThuTu = 1, j, csC = 0;
            DataRow Row;

            //Ghi cột
            while (i < dtCotMau.Rows.Count)
            {
                Row = dtCotMau.Rows[i];
                if (Convert.ToBoolean(Row["bThuocDonVi"]))
                {
                    csC = i;
                    if (XemTheoDonVi)
                    {
                        csC = BangModels_TaoBangMoi.TaoCacCotTheoDonVi(MaBang, dtCotMau, MaDonVi, i, ref ThuTu, dtBang, sUserName, sIP);
                    }
                    else
                    {
                        for (j = 0; j < dtDonVi.Rows.Count; j++)
                        {
                            csC = BangModels_TaoBangMoi.TaoCacCotTheoDonVi(MaBang, dtCotMau, Convert.ToString(dtDonVi.Rows[j]["iID_MaDonVi"]), i, ref ThuTu, dtBang, sUserName, sIP);
                        }
                    }
                    if (i == csC)
                    {
                        break;
                    }
                    i = csC;
                }
                else
                {
                    BangModels_TaoBangMoi.TaoCacCotKhongTheoDonVi(MaBang, dtCotMau, i, ref ThuTu, dtDonVi, dtBang, sUserName, sIP);
                    i = i + 1;
                }
            }

            //Ghi hàng
            BangMauHangMauModels.TaoHangMoi(MaBangMau, MaDonVi, MaBang, XemTheoDonVi, sUserName, sIP);


            //Ghi hàm thêm
            for (i = 0; i < dtHamMau.Rows.Count; i++)
            {
                Row = dtHamMau.Rows[i];
                bang = new Bang("BC_Bang_Ham");
                bang.MaNguoiDungSua = sUserName;
                bang.IPSua = sIP;
                bang.DuLieuMoi = true;
                bang.CmdParams.Parameters.AddWithValue("@iID_MaBang", MaBang);
                bang.CmdParams.Parameters.AddWithValue("@sNoiDung", Row["sNoiDung"]);
                bang.Save();
            }

            //Tạo công thức
            BangModels_CongThuc.TaoCongThucChoBangMoi(MaBangMau, MaBang, sUserName, sIP);

            dtBangMau.Dispose();
            dtCongThuc.Dispose();
            dtCotMau.Dispose();
            dtDonVi.Dispose();
            dtHamMau.Dispose();

            //Tạo file JS
            strPath = String.Format("{0}/CongThuc/{1}", strPath, NgayBaoCao.ToString("yyyy/MM/dd"));
            if (Directory.Exists(strPath) == false)
            {
                try
                {
                    Directory.CreateDirectory(strPath);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            String TenFileJS = String.Format("{0}{1}", MaBang, DateTime.Now.ToString("yyMMddHHmmss"));
            cmd = new SqlCommand("UPDATE BC_Bang SET sTenFileJS=@sTenFileJS WHERE iID_MaBang=@iID_MaBang");
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            cmd.Parameters.AddWithValue("@sTenFileJS", TenFileJS);
            Connection.UpdateDatabase(cmd);

            StreamWriter sw = new StreamWriter(String.Format("{0}/{1}.js", strPath, TenFileJS));

            sw.Write(BangModels_CongThuc.LayXauCongThuc(MaBang));
            sw.Encoding.ToString();
            sw.Flush();
            sw.Close();

            return MaBang;
        }

        public static void XoaBang(String MaBang)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "DELETE FROM BC_Bang WHERE iID_MaBang=@iID_MaBang;" +
                              "DELETE FROM BC_Bang_Cot WHERE iID_MaBang=@iID_MaBang;" +
                              "DELETE FROM BC_Bang_Hang WHERE iID_MaBang=@iID_MaBang;" +
                              "DELETE FROM BC_Bang_Ham WHERE iID_MaBang=@iID_MaBang;" +
                              "DELETE FROM BC_Bang_CongThuc WHERE iID_MaBang=@iID_MaBang;" +
                              "DELETE FROM BC_Bang_ChiTiet WHERE iID_MaBang=@iID_MaBang";
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            Connection.UpdateDatabase(cmd);
            cmd.Dispose();
        }


        public static DataTable LayDuLieuCot(String MaBangMau, String MaCotMau, Boolean XemTheoDonVi, String MaDonVi, int LoaiBaoCao, DateTime NgayBaoCao)
        {
            String MaBang = LayMaBang(MaBangMau, XemTheoDonVi, MaDonVi, LoaiBaoCao, NgayBaoCao, -1);
            if (String.IsNullOrEmpty(MaBang) == false)
            {
                SqlCommand cmd;
                //cmd = new SqlCommand("SELECT bDaChot FROM BC_Bang WHERE (iID_MaBang=@iID_MaBang)");
                //cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
                //Boolean DaChot = Convert.ToBoolean(Connection.GetValue(cmd,false));
                //if (DaChot)
                {
                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
                    cmd.Parameters.AddWithValue("@iID_MaCotMau", MaCotMau);
                    cmd.CommandText = "SELECT iID_MaCot " +
                                      " FROM BC_Bang_Cot " +
                                      " WHERE (iID_MaBang=@iID_MaBang) AND (iID_MaCotMau=@iID_MaCotMau)";
                    String MaCot = Convert.ToString(Connection.GetValue(cmd, 0));
                    cmd.Dispose();

                    cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
                    cmd.Parameters.AddWithValue("@iID_MaCot", MaCot);
                    cmd.CommandText = "SELECT * " +
                                      " FROM BC_Bang_ChiTiet " +
                                      " WHERE (iID_MaBang=@iID_MaBang) AND (iID_MaCot=@iID_MaCot)" +
                                      " ORDER BY iHang";
                    DataTable dt = Connection.GetDataTable(cmd);
                    cmd.Dispose();
                    return dt;
                }
            }
            return null;
        }

        public static Boolean LayThongTinCuaBang(Boolean ChiCoQuyenDoc,
                                              DataTable dtBang,
                                              DataTable dtHang,
                                              DataTable dtCot,
                                              DataTable dtDonVi,
                                              ref DataTable dtCongThuc,
                                              ref List<List<object>> arrGiaTri,
                                              ref List<List<object>> arrGiaTriCu,
                                              ref List<bool> arrCotHienThi,
                                              ref String strXauMaGiaTriKhac,
                                              ref String strXauGiaTriKhac)
        {
            String MaBang = Convert.ToString(dtBang.Rows[0]["iID_MaBang"]);
            strXauMaGiaTriKhac = "";
            strXauGiaTriKhac = "";

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            cmd.CommandText = "SELECT * " +
                              " FROM BC_Bang_ChiTiet " +
                              " WHERE (iID_MaBang=@iID_MaBang)";
            DataTable dtChiTiet = Connection.GetDataTable(cmd);
            cmd.Dispose();

            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@iID_MaBangMau", dtBang.Rows[0]["iID_MaBangMau"]);
            cmd.CommandText = "SELECT * " +
                              " FROM BC_BangMau_GiaTriMacDinh " +
                              " WHERE (iID_MaBangMau=@iID_MaBangMau)";
            DataTable dtGiaTriMacDinh = Connection.GetDataTable(cmd);
            cmd.Dispose();

            int nH = dtHang.Rows.Count;
            int nC = dtCot.Rows.Count;
            int i, j, h, c;

            List<DataTable> arrDTBang = new List<DataTable>();
            List<DataTable> arrDTHang = new List<DataTable>();
            List<DataTable> arrDTCot = new List<DataTable>();
            List<DataTable> arrDTChiTiet = new List<DataTable>();

            List<List<Boolean>> arrDaCoGiaTri = new List<List<Boolean>>();
            List<List<Boolean>> arrThayDoiGiaTri = new List<List<Boolean>>();

            String MaBangMau;
            Boolean XemTheoDonVi;
            String MaDonVi, MaHangMau, MaCotMau;
            int KhoangCachThoiGian;
            int ChiSoBang;
            DateTime NgayBaoCao = Convert.ToDateTime(dtBang.Rows[0]["dNgayBaoCao"]);
            int LoaiLuyKe;

            object value;
            //Khởi tạo giá trị
            arrGiaTri = new List<List<object>>();
            arrGiaTriCu = new List<List<object>>();
            for (i = 0; i < nH; i++)
            {
                arrGiaTri.Add(new List<object>());
                arrGiaTriCu.Add(new List<object>());
                arrDaCoGiaTri.Add(new List<bool>());
                arrThayDoiGiaTri.Add(new List<bool>());
                for (j = 0; j < nC; j++)
                {
                    arrDaCoGiaTri[i].Add(false);
                    arrThayDoiGiaTri[i].Add(false);
                    value = "";
                    if (dtCot.Rows[j]["oGiaTriMacDinh"] != DBNull.Value)
                    {
                        value = dtCot.Rows[j]["oGiaTriMacDinh"];
                    }
                    arrGiaTri[i].Add(value);
                    arrGiaTriCu[i].Add("");
                }
            }
            //Dien gia tri da luu
            for (h = 0; h < nH; h++)
            {
                for (c = 0; c < nC; c++)
                {
                    for (i = 0; i < dtChiTiet.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dtHang.Rows[h]["iID_MaHang"]) == Convert.ToInt32(dtChiTiet.Rows[i]["iID_MaHang"]) &&
                           Convert.ToInt32(dtCot.Rows[c]["iID_MaCot"]) == Convert.ToInt32(dtChiTiet.Rows[i]["iID_MaCot"]))
                        {
                            arrGiaTri[h][c] = dtChiTiet.Rows[i]["oGiaTri"];
                            arrDaCoGiaTri[h][c] = true;
                            break;
                        }
                    }
                }
            }


            //Cap nhap gia tri cu
            for (i = 0; i < nH; i++)
            {
                for (j = 0; j < nC; j++)
                {
                    arrGiaTriCu[i][j] = arrGiaTri[i][j];
                }
            }

            if (ChiCoQuyenDoc) return true;

            //Dien gia tri mac dinh chung cho bảng mẫu
            for (i = 0; i < dtGiaTriMacDinh.Rows.Count; i++)
            {
                MaHangMau = Convert.ToString(dtGiaTriMacDinh.Rows[i]["iID_MaHangMau"]);
                MaCotMau = Convert.ToString(dtGiaTriMacDinh.Rows[i]["iID_MaCotMau"]);
                MaDonVi = Convert.ToString(dtGiaTriMacDinh.Rows[i]["iID_MaDonVi"]);
                if (MaDonVi == "-2")
                {
                    h = -1;
                    c = -1;
                    for (j = 0; j < dtHang.Rows.Count; j++)
                    {
                        if (MaHangMau == Convert.ToString(dtHang.Rows[j]["iID_MaHangMau"]))
                        {
                            h = j;
                            break;
                        }
                    }
                    for (j = 0; j < dtCot.Rows.Count; j++)
                    {
                        if (MaCotMau == Convert.ToString(dtCot.Rows[j]["iID_MaCotMau"]))
                        {
                            c = j;
                            if (h >= 0 && c >= 0 && arrDaCoGiaTri[h][c] == false)
                            {
                                arrGiaTri[h][c] = dtGiaTriMacDinh.Rows[i]["oGiaTri"];
                            }
                        }
                    }
                }
            }

            //Dien gia tri mac dinh
            for (i = 0; i < dtGiaTriMacDinh.Rows.Count; i++)
            {
                MaHangMau = Convert.ToString(dtGiaTriMacDinh.Rows[i]["iID_MaHangMau"]);
                MaCotMau = Convert.ToString(dtGiaTriMacDinh.Rows[i]["iID_MaCotMau"]);
                MaDonVi = Convert.ToString(dtGiaTriMacDinh.Rows[i]["iID_MaDonVi"]);
                h = -1;
                c = -1;
                for (j = 0; j < dtHang.Rows.Count; j++)
                {
                    if (MaHangMau == Convert.ToString(dtHang.Rows[j]["iID_MaHangMau"]))
                    {
                        h = j;
                        break;
                    }
                }
                for (j = 0; j < dtCot.Rows.Count; j++)
                {
                    if (MaCotMau == Convert.ToString(dtCot.Rows[j]["iID_MaCotMau"]) &&
                        MaDonVi == Convert.ToString(dtCot.Rows[j]["iID_MaDonVi"]))
                    {
                        c = j;
                        break;
                    }
                }
                if (h >= 0 && c >= 0 && arrDaCoGiaTri[h][c] == false)
                {
                    arrGiaTri[h][c] = dtGiaTriMacDinh.Rows[i]["oGiaTri"];
                }
            }

            //Điền giá trị theo cột
            Boolean ChiTietThoiGian;
            for (c = 0; c < nC; c++)
            {
                if (Convert.ToBoolean(dtCot.Rows[c]["bLayDuLieuTuBangKhac"]))
                {
                    MaBangMau = Convert.ToString(dtCot.Rows[c]["iID_MaBangMau_DuLieu"]);
                    XemTheoDonVi = Convert.ToBoolean(dtCot.Rows[c]["bThuocDonVi"]);
                    MaDonVi = Convert.ToString(dtCot.Rows[c]["iID_MaDonVi"]);
                    KhoangCachThoiGian = Convert.ToInt16(dtCot.Rows[c]["iKhoangCachThoiGian"]);
                    LoaiLuyKe = Convert.ToInt16(dtCot.Rows[c]["iLoaiLuyKe"]);
                    ChiTietThoiGian = Convert.ToBoolean(dtCot.Rows[c]["bChiTietThoiGian"]);
                    ChiSoBang = LayChiSoBangDuLieu(MaBangMau, XemTheoDonVi, MaDonVi, KhoangCachThoiGian, NgayBaoCao, LoaiLuyKe, ChiTietThoiGian, ref arrDTBang, ref arrDTHang, ref arrDTCot, ref arrDTChiTiet);

                    if (ChiSoBang >= 0)
                    {
                        for (h = 0; h < nH; h++)
                        {
                            arrGiaTri[h][c] = LayGiaTriTuBangKhac(Convert.ToString(dtHang.Rows[h]["iID_MaHangMau"]),
                                                            Convert.ToString(dtHang.Rows[h]["iID_MaHangMau_Cha"]),
                                                            Convert.ToString(dtCot.Rows[c]["iID_MaCotMau_DuLieu"]),
                                                            Convert.ToBoolean(dtCot.Rows[c]["bThuocDonVi"]),
                                                            Convert.ToString(dtCot.Rows[c]["iID_MaDonVi"]),
                                                            arrDTBang[ChiSoBang],
                                                            arrDTHang[ChiSoBang],
                                                            arrDTCot[ChiSoBang],
                                                            arrDTChiTiet[ChiSoBang]
                                                            );
                            if (Convert.ToString(arrGiaTri[h][c]) != Convert.ToString(arrGiaTriCu[h][c]))
                            {
                                arrThayDoiGiaTri[h][c] = true;
                            }
                        }
                    }
                    if (arrCotHienThi[c])
                    {
                        cmd = new SqlCommand("SELECT Count(*) FROM BC_BangMau_CotMau_DonVi WHERE iID_MaCotMau=@iID_MaCotMau AND iID_MaDonVi=@iID_MaDonVi");
                        cmd.Parameters.AddWithValue("@iID_MaCotMau", dtCot.Rows[c]["iID_MaCotMau_DuLieu"]);
                        cmd.Parameters.AddWithValue("@iID_MaDonVi", dtCot.Rows[c]["iID_MaDonVi"]);
                        arrCotHienThi[c] = Convert.ToInt16(Connection.GetValue(cmd, 0)) == 0;
                    }
                }
            }

            //Điền giá trị theo hàng
            String MaHangMau_Cha, MaBangMauDuLieu, MaHangMauDuLieu, MaCotMauDuLieu, MaDonViDuLieu, strKetQuaHam, SQL1, SQL2;
            int LoaiSanPham, iDonVi;
            DataTable dtBangMau_HangMauDuLieu = null;
            List<DataTable> dtBangMau_HangMauDuLieu_CoTheoDonVi = null;
            Double S;
            Boolean ok, ok1;
            if (Convert.ToBoolean(dtBang.Rows[0]["bXemTheoDonVi"]) == false)
            {
                for (h = 0; h < nH; h++)
                {
                    if (Convert.ToBoolean(dtHang.Rows[h]["bTinhTongTheoDonVi"]))
                    {
                        MaHangMau_Cha = Convert.ToString(dtHang.Rows[h]["iID_MaHangMau_Cha"]);
                        LoaiSanPham = Convert.ToInt32(dtHang.Rows[h]["iID_LoaiSanPham"]);
                        for (c = 0; c < nC; c++)
                        {
                            if (Convert.ToBoolean(dtCot.Rows[c]["bKhongApDungCongThucHang"]) == false)
                            {
                                S = 0;
                                MaDonVi = Convert.ToString(dtCot.Rows[c]["iID_MaDonVi"]);
                                XemTheoDonVi = true;
                                MaBangMauDuLieu = Convert.ToString(dtCot.Rows[c]["iID_MaBangMau_DuLieu"]);
                                MaCotMauDuLieu = Convert.ToString(dtCot.Rows[c]["iID_MaCotMau_DuLieu"]);
                                if (String.IsNullOrEmpty(MaCotMauDuLieu) == false && Convert.ToInt32(MaCotMauDuLieu) > 0 && MaBangMauDuLieu != "-1")
                                {
                                    ok = true;
                                    iDonVi = -1;
                                    for (i = 0; i < dtDonVi.Rows.Count; i++)
                                    {
                                        if (MaDonVi == Convert.ToString(dtDonVi.Rows[i]["iID_MaDonVi"]))
                                        {
                                            if (Convert.ToBoolean(dtDonVi.Rows[i]["bSanXuat"]) == false)
                                            {
                                                ok = false;
                                            }
                                            iDonVi = i;
                                            break;
                                        }
                                    }
                                    if (ok)
                                    {
                                        if (dtBangMau_HangMauDuLieu_CoTheoDonVi == null)
                                        {
                                            dtBangMau_HangMauDuLieu_CoTheoDonVi = new List<DataTable>();
                                            for (i = 0; i < dtDonVi.Rows.Count; i++)
                                            {
                                                SQL1 = "SELECT * FROM BC_HangMau WHERE iID_MaDonVi=@iID_MaDonVi ";
                                                SQL2 = "SELECT * FROM BC_BangMau_HangMau WHERE iID_MaBangMau=@iID_MaBangMau";
                                                cmd = new SqlCommand(String.Format("SELECT tb2.*, tb1.iID_LoaiSanPham FROM ({0}) tb1 INNER JOIN ({1}) tb2 ON tb1.iID_MaHangMau=tb2.iID_MaHangMau", SQL1, SQL2));
                                                cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMauDuLieu);
                                                cmd.Parameters.AddWithValue("@iID_MaDonVi", dtDonVi.Rows[i]["iID_MaDonVi"]);
                                                dtBangMau_HangMauDuLieu_CoTheoDonVi.Add(Connection.GetDataTable(cmd));
                                                cmd.Dispose();
                                            }
                                        }

                                        ok1 = false;
                                        for (i = 0; i < dtBangMau_HangMauDuLieu_CoTheoDonVi[iDonVi].Rows.Count; i++)
                                        {
                                            if (Convert.ToString(dtBangMau_HangMauDuLieu_CoTheoDonVi[iDonVi].Rows[i]["iID_MaHangMau_Cha"]) == MaHangMau_Cha &&
                                                Convert.ToInt32(dtBangMau_HangMauDuLieu_CoTheoDonVi[iDonVi].Rows[i]["iID_LoaiSanPham"]) == LoaiSanPham)
                                            {
                                                ok1 = true;
                                                MaHangMauDuLieu = Convert.ToString(dtBangMau_HangMauDuLieu_CoTheoDonVi[iDonVi].Rows[i]["iID_MaHangMau"]);
                                                for (j = 0; j < dtDonVi.Rows.Count; j++)
                                                {
                                                    ChiSoBang = LayChiSoBangDuLieu(MaBangMauDuLieu, XemTheoDonVi, Convert.ToString(dtDonVi.Rows[j]["iID_MaDonVi"]), 0, NgayBaoCao, -1, false, ref arrDTBang, ref arrDTHang, ref arrDTCot, ref arrDTChiTiet);
                                                    value = 0;
                                                    if (ChiSoBang >= 0)
                                                    {
                                                        value = LayGiaTriTuBangKhac(MaHangMauDuLieu,
                                                                                    MaHangMau_Cha,
                                                                                    MaCotMauDuLieu,
                                                                                    XemTheoDonVi,
                                                                                    Convert.ToString(dtDonVi.Rows[j]["iID_MaDonVi"]),
                                                                                    arrDTBang[ChiSoBang],
                                                                                    arrDTHang[ChiSoBang],
                                                                                    arrDTCot[ChiSoBang],
                                                                                    arrDTChiTiet[ChiSoBang]
                                                                                    );
                                                    }
                                                    if (String.IsNullOrEmpty(value.ToString()))
                                                    {
                                                        value = 0;
                                                    }
                                                    S = S + Convert.ToDouble(value);
                                                }
                                            }
                                            else
                                            {
                                                if (ok1) break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        SQL1 = "SELECT * FROM BC_HangMau WHERE iID_LoaiSanPham=@iID_LoaiSanPham ";
                                        SQL2 = "SELECT * FROM BC_BangMau_HangMau WHERE iID_MaBangMau=@iID_MaBangMau AND iID_MaHangMau_Cha>0 AND iID_MaHangMau_Cha=@iID_MaHangMau_Cha ";
                                        cmd = new SqlCommand(String.Format("SELECT tb2.*, tb1.iID_LoaiSanPham FROM ({0}) tb1 INNER JOIN ({1}) tb2 ON tb1.iID_MaHangMau=tb2.iID_MaHangMau", SQL1, SQL2));
                                        cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMauDuLieu);
                                        cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMau_Cha);
                                        cmd.Parameters.AddWithValue("@iID_LoaiSanPham", LoaiSanPham);
                                        dtBangMau_HangMauDuLieu = Connection.GetDataTable(cmd);
                                        cmd.Dispose();
                                        for (i = 0; i < dtBangMau_HangMauDuLieu.Rows.Count; i++)
                                        {
                                            if (MaHangMau_Cha == Convert.ToString(dtBangMau_HangMauDuLieu.Rows[i]["iID_MaHangMau_Cha"]) &&
                                                LoaiSanPham == Convert.ToInt32(dtBangMau_HangMauDuLieu.Rows[i]["iID_LoaiSanPham"]))
                                            {
                                                MaHangMauDuLieu = Convert.ToString(dtBangMau_HangMauDuLieu.Rows[i]["iID_MaHangMau"]);
                                                LoaiLuyKe = -1;
                                                ChiSoBang = LayChiSoBangDuLieu(MaBangMauDuLieu, XemTheoDonVi, MaDonVi, 0, NgayBaoCao, LoaiLuyKe, false, ref arrDTBang, ref arrDTHang, ref arrDTCot, ref arrDTChiTiet);
                                                value = 0;
                                                if (ChiSoBang >= 0)
                                                {
                                                    value = LayGiaTriTuBangKhac(MaHangMauDuLieu,
                                                                                MaHangMau_Cha,
                                                                                MaCotMauDuLieu,
                                                                                XemTheoDonVi,
                                                                                MaDonVi,
                                                                                arrDTBang[ChiSoBang],
                                                                                arrDTHang[ChiSoBang],
                                                                                arrDTCot[ChiSoBang],
                                                                                arrDTChiTiet[ChiSoBang]
                                                                                );
                                                }
                                                if (String.IsNullOrEmpty(value.ToString()))
                                                {
                                                    value = 0;
                                                }
                                                S = S + Convert.ToDouble(value);
                                            }
                                        }
                                        dtBangMau_HangMauDuLieu.Dispose();
                                    }
                                    value = S;
                                    if (value != arrGiaTri[h][c])
                                    {
                                        if (!(value.ToString() == "0" && arrGiaTriCu[h][c].ToString() == ""))
                                        {
                                            arrGiaTri[h][c] = S;
                                            arrThayDoiGiaTri[h][c] = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (dtBangMau_HangMauDuLieu_CoTheoDonVi != null)
                {
                    for (i = 0; i < dtDonVi.Rows.Count; i++)
                    {
                        dtBangMau_HangMauDuLieu_CoTheoDonVi[i].Dispose();
                    }
                }
                for (h = 0; h < nH; h++)
                {
                    if (Convert.ToBoolean(dtHang.Rows[h]["bTinhTongTheoLoaiSanPham"]))
                    {
                        LoaiSanPham = Convert.ToInt32(dtHang.Rows[h]["iID_LoaiSanPham"]);
                        for (c = 0; c < nC; c++)
                        {
                            if (Convert.ToInt32(dtCot.Rows[c]["iKieuDuLieu"]) == 1)
                            {
                                S = 0;
                                for (i = 0; i < nH; i++)
                                {
                                    if (LoaiSanPham == Convert.ToInt32(dtHang.Rows[i]["iID_LoaiSanPham"]) && Convert.ToInt32(dtHang.Rows[i]["iID_MaHangMau"]) == 0 && Convert.ToString(arrGiaTri[i][c]) != "")
                                    {
                                        S += Convert.ToDouble(arrGiaTri[i][c]);
                                    }
                                }
                                value = S;
                                if (value.ToString() != arrGiaTriCu[h][c].ToString())
                                {
                                    arrGiaTri[h][c] = S;
                                    arrThayDoiGiaTri[h][c] = true;
                                }
                            }
                        }
                    }
                }
            }

            //Thay các giá trị của các bảng khác vào công thức
            String MaHang, MaCot, strCongThuc, strTG;
            int h1, c1;
            Boolean okBatBuocGoiHam;

            MaBangMau = Convert.ToString(dtBang.Rows[0]["iID_MaBangMau"]);
            for (i = 0; i < dtCongThuc.Rows.Count; i++)
            {
                strCongThuc = Convert.ToString(dtCongThuc.Rows[i]["sCongThuc"]);
                MaHang = Convert.ToString(dtCongThuc.Rows[i]["iID_MaHang"]);
                MaCot = Convert.ToString(dtCongThuc.Rows[i]["iID_MaCot"]);
                h = BangModels_HamChung.LayCSH(dtHang, MaHang);
                c = BangModels_HamChung.LayCSC(dtCot, MaCot);
                if (strCongThuc.StartsWith("=VITRIHANG"))
                {
                }
                else if (strCongThuc.StartsWith("=MAX(") || strCongThuc.StartsWith("=MIN(") || strCongThuc.StartsWith("=LIETKE("))
                {
                    int cs1 = strCongThuc.IndexOf("[");
                    int cs2 = strCongThuc.IndexOf("]");
                    strTG = strCongThuc.Substring(cs1 + 1, cs2 - cs1 - 1);
                    String[] arrTG = strTG.Split('_');
                    if (arrTG.Length >= 3)
                    {
                        MaBangMauDuLieu = arrTG[0].Substring(1);
                        MaHangMauDuLieu = arrTG[1].Substring(1);
                        MaCotMauDuLieu = arrTG[2].Substring(1);
                    }
                    else
                    {
                        //Trường hợp lấy dữ liệu ngay trên bảng
                        MaBangMauDuLieu = "";
                        MaHangMauDuLieu = arrTG[0].Substring(1);
                        MaCotMauDuLieu = arrTG[1].Substring(1);
                    }

                    XemTheoDonVi = true;
                    KhoangCachThoiGian = 0;
                    LoaiLuyKe = -1;
                    strKetQuaHam = "";
                    if (MaBangMauDuLieu == "" || MaBangMau == MaBangMauDuLieu)
                    {
                    }
                    else
                    {
                        //Trường hợp gọi hàm MIN,MAX... của các ô của bảng khác
                        for (j = 0; j < dtDonVi.Rows.Count; j++)
                        {
                            MaDonVi = Convert.ToString(dtDonVi.Rows[j]["iID_MaDonVi"]);
                            value = "";

                            ChiSoBang = LayChiSoBangDuLieu(MaBangMauDuLieu, XemTheoDonVi, MaDonVi, KhoangCachThoiGian, NgayBaoCao, LoaiLuyKe, false, ref arrDTBang, ref arrDTHang, ref arrDTCot, ref arrDTChiTiet);

                            if (ChiSoBang >= 0)
                            {
                                value = LayGiaTriTuBangKhac(MaHangMauDuLieu,
                                                            "",
                                                            MaCotMauDuLieu,
                                                            XemTheoDonVi,
                                                            MaDonVi,
                                                            arrDTBang[ChiSoBang],
                                                            arrDTHang[ChiSoBang],
                                                            arrDTCot[ChiSoBang],
                                                            arrDTChiTiet[ChiSoBang]
                                                            );
                            }
                            if (strCongThuc.StartsWith("=LIETKE("))
                            {
                                value = String.Format("{0}:{1}", dtDonVi.Rows[j]["sTenVietTat"], value);
                            }
                            if (strKetQuaHam == "")
                            {
                                strKetQuaHam = value.ToString();
                            }
                            else
                            {
                                if (strCongThuc.StartsWith("=MAX("))
                                {
                                    if (CommonFunction.IsNumeric(value))
                                    {
                                        strKetQuaHam = (Convert.ToDouble(strKetQuaHam) > Convert.ToDouble(value)) ? strKetQuaHam : value.ToString();
                                    }
                                }
                                else if (strCongThuc.StartsWith("=MIN("))
                                {
                                    if (CommonFunction.IsNumeric(value))
                                    {
                                        strKetQuaHam = (Convert.ToDouble(strKetQuaHam) < Convert.ToDouble(value)) ? strKetQuaHam : value.ToString();
                                    }
                                }
                                else if (strCongThuc.StartsWith("=LIETKE("))
                                {
                                    strKetQuaHam += "\n" + value.ToString();
                                }
                            }
                        }
                    }
                    arrGiaTri[h][c] = strKetQuaHam;
                }
                else
                {
                    okBatBuocGoiHam = false;
                    while (strCongThuc.IndexOf("[B") >= 0)
                    {
                        value = "";
                        int cs1 = strCongThuc.IndexOf("[B");
                        int cs2 = cs1 + 1;
                        while (cs2 < strCongThuc.Length && strCongThuc[cs2] != ']')
                        {
                            cs2++;
                        }
                        strTG = strCongThuc.Substring(cs1 + 1, cs2 - cs1 - 1);
                        String strMaOBangKhac = strTG;
                        String[] arrTG = strTG.Split('_');


                        MaBangMauDuLieu = "-1";
                        MaHangMauDuLieu = "-1";
                        MaCotMauDuLieu = "-1";
                        XemTheoDonVi = Convert.ToBoolean(dtCot.Rows[c]["bThuocDonVi"]);
                        MaDonVi = Convert.ToString(dtCot.Rows[c]["iID_MaDonVi"]);
                        KhoangCachThoiGian = 0;
                        LoaiLuyKe = -1;
                        if (XemTheoDonVi)
                        {
                            strMaOBangKhac += "_" + MaDonVi;
                        }

                        for (int tgi = 0; tgi < arrTG.Length; tgi++)
                        {
                            strTG = arrTG[tgi];
                            switch (strTG[0])
                            {
                                case 'B':
                                    MaBangMauDuLieu = strTG.Substring(1);
                                    break;
                                case 'H':
                                    MaHangMauDuLieu = strTG.Substring(1);
                                    break;
                                case 'C':
                                    MaCotMauDuLieu = strTG.Substring(1);
                                    break;
                                case 'D':
                                    XemTheoDonVi = true;
                                    MaDonVi = Convert.ToString(strTG.Substring(1));
                                    break;
                                case 'K':
                                    KhoangCachThoiGian = Convert.ToInt16(strTG.Substring(1));
                                    break;
                                case 'T':
                                    //Lũy kế tháng
                                    LoaiLuyKe = 1;
                                    break;
                                case 'N':
                                    //Lũy kế năm
                                    LoaiLuyKe = 2;
                                    break;
                                default:
                                    KhoangCachThoiGian = Convert.ToInt16(strTG);
                                    break;
                            }
                        }

                        ChiSoBang = LayChiSoBangDuLieu(MaBangMauDuLieu, XemTheoDonVi, MaDonVi, KhoangCachThoiGian, NgayBaoCao, LoaiLuyKe, false, ref arrDTBang, ref arrDTHang, ref arrDTCot, ref arrDTChiTiet);

                        if (ChiSoBang >= 0)
                        {
                            value = LayGiaTriTuBangKhac(MaHangMauDuLieu,
                                                        "",
                                                        MaCotMauDuLieu,
                                                        XemTheoDonVi,
                                                        MaDonVi,
                                                        arrDTBang[ChiSoBang],
                                                        arrDTHang[ChiSoBang],
                                                        arrDTCot[ChiSoBang],
                                                        arrDTChiTiet[ChiSoBang]
                                                        );
                            okBatBuocGoiHam = true;
                        }
                        if (CommonFunction.IsNumeric(value))
                        {
                            strCongThuc = strCongThuc.Substring(0, cs1) +
                                          value +
                                          strCongThuc.Substring(cs2 + 1);
                        }
                        else
                        {
                            strCongThuc = strCongThuc.Substring(0, cs1) +
                                          "\"" + value + "\"" +
                                          strCongThuc.Substring(cs2 + 1);
                        }
                        if (strXauGiaTriKhac != "") strXauGiaTriKhac += ",";
                        strXauGiaTriKhac += value;
                        if (strXauMaGiaTriKhac != "") strXauMaGiaTriKhac += ",";
                        strXauMaGiaTriKhac += strMaOBangKhac;
                    }
                    dtCongThuc.Rows[i]["sCongThuc"] = strCongThuc;
                    if (okBatBuocGoiHam)
                    {
                        value = ThucHienBieuThuc(dtHang, dtCot, strCongThuc, ref arrGiaTri);
                        if (value.ToString() != arrGiaTriCu[h][c].ToString())
                        {
                            arrGiaTri[h][c] = value;
                            arrThayDoiGiaTri[h][c] = true;
                        }
                    }
                }
            }

            //Thực hiện lại công thức
            List<List<ThongSoO>> arrThongSo = XacDinhCongThucAnhHuongO(dtHang, dtCot, dtCongThuc);

            for (h = 0; h < nH; h++)
            {
                for (c = 0; c < nC; c++)
                {
                    if (arrThayDoiGiaTri[h][c])
                    {
                        GoiHamKhiCoThayDoiO(dtHang, dtCot, dtCongThuc, h, c, ref arrGiaTri, ref arrThongSo, ref arrThayDoiGiaTri);
                    }
                }
            }

            //Thực hiện các hàm MIN,MAX,LIETKE của các ô cùng bảng

            MaBangMau = Convert.ToString(dtBang.Rows[0]["iID_MaBangMau"]);
            for (i = 0; i < dtCongThuc.Rows.Count; i++)
            {
                strCongThuc = Convert.ToString(dtCongThuc.Rows[i]["sCongThuc"]);
                MaHang = Convert.ToString(dtCongThuc.Rows[i]["iID_MaHang"]);
                MaCot = Convert.ToString(dtCongThuc.Rows[i]["iID_MaCot"]);
                h = BangModels_HamChung.LayCSH(dtHang, MaHang);
                c = BangModels_HamChung.LayCSC(dtCot, MaCot);
                if (strCongThuc.StartsWith("=VITRIHANG"))
                {
                    Boolean okTangDan = strCongThuc.StartsWith("=VITRIHANG_TANGDAN(");
                    double gt;
                    //Trường hợp giá trị =''
                    if (okTangDan)
                    {
                        gt = Double.MinValue;
                    }
                    else
                    {
                        gt = Double.MaxValue;
                    }
                    int ViTriTrongHang = 1;


                    string tgMaHang, tgMaCot;
                    int cs1 = strCongThuc.IndexOf("[");
                    int cs2 = strCongThuc.LastIndexOf("]");
                    strTG = strCongThuc.Substring(cs1 + 1, cs2 - cs1 - 1);
                    String[] arrTG = strTG.Split('_');
                    tgMaHang = arrTG[0];
                    tgMaCot = arrTG[1];
                    h1 = BangModels_HamChung.LayCSH(dtHang, tgMaHang);
                    c1 = BangModels_HamChung.LayCSC(dtCot, tgMaCot);
                    if (CommonFunction.IsNumeric(arrGiaTri[h][c1]))
                    {
                        //Trường hợp có giá trị của ô so sánh
                        gt = Convert.ToDouble(arrGiaTri[h][c1]);
                    }
                    for (j = 0; j < dtCot.Rows.Count; j++)
                    {
                        if (c1 != j && Convert.ToInt32(dtCot.Rows[c1]["iID_MaCotMau"]) == Convert.ToInt32(dtCot.Rows[j]["iID_MaCotMau"]))
                        {
                            if (CommonFunction.IsNumeric(arrGiaTri[h][j]))
                            {
                                if ((okTangDan == true && gt < Convert.ToDouble(arrGiaTri[h][j])) ||
                                    (okTangDan == false && gt > Convert.ToDouble(arrGiaTri[h][j])))
                                {
                                    ViTriTrongHang++;
                                }
                            }
                        }
                    }
                    arrGiaTri[h][c] = ViTriTrongHang;
                }
                else if (strCongThuc.StartsWith("=MAX(") || strCongThuc.StartsWith("=MIN(") || strCongThuc.StartsWith("=LIETKE("))
                {
                    int cs1 = strCongThuc.IndexOf("[");
                    int cs2 = strCongThuc.IndexOf("]");
                    strTG = strCongThuc.Substring(cs1 + 1, cs2 - cs1 - 1);
                    String[] arrTG = strTG.Split('_');
                    if (arrTG.Length >= 3)
                    {
                        MaBangMauDuLieu = arrTG[0].Substring(1);
                        MaHangMauDuLieu = arrTG[1].Substring(1);
                        MaCotMauDuLieu = arrTG[2].Substring(1);
                    }
                    else
                    {
                        //Trường hợp lấy dữ liệu ngay trên bảng
                        MaBangMauDuLieu = "";
                        MaHangMauDuLieu = arrTG[0].Substring(1);
                        MaCotMauDuLieu = arrTG[1].Substring(1);
                    }

                    XemTheoDonVi = true;
                    KhoangCachThoiGian = 0;
                    LoaiLuyKe = -1;
                    strKetQuaHam = "";
                    if (MaBangMauDuLieu == "" || MaBangMau == MaBangMauDuLieu)
                    {
                        //Trường hợp gọi hàm MIN,MAX... của các ô trong bảng
                        cs1 = strCongThuc.IndexOf("(");
                        cs2 = strCongThuc.LastIndexOf(")");
                        strTG = strCongThuc.Substring(cs1 + 1, cs2 - cs1 - 1);
                        arrTG = strTG.Split(',');
                        for (j = 0; j < arrTG.Length; j++)
                        {
                            cs1 = arrTG[j].IndexOf("[");
                            cs2 = arrTG[j].LastIndexOf("]");
                            strTG = arrTG[j].Substring(cs1 + 1, cs2 - cs1 - 1);
                            String[] arrTG1 = strTG.Split('_');
                            string tgMaHang = arrTG1[0];
                            string tgMaCot = arrTG1[1];
                            h1 = BangModels_HamChung.LayCSH(dtHang, tgMaHang);
                            c1 = BangModels_HamChung.LayCSC(dtCot, tgMaCot);
                            value = "";
                            if (h1 >= 0 && c1 >= 0)
                            {
                                value = arrGiaTri[h1][c1];
                            }
                            if (strCongThuc.StartsWith("=LIETKE("))
                            {
                                value = String.Format("{0}:{1}", dtDonVi.Rows[j]["sTenVietTat"], value);
                            }
                            if (strKetQuaHam == "")
                            {
                                strKetQuaHam = value.ToString();
                            }
                            else
                            {
                                if (strCongThuc.StartsWith("=MAX("))
                                {
                                    if (CommonFunction.IsNumeric(value))
                                    {
                                        strKetQuaHam = (Convert.ToDouble(strKetQuaHam) > Convert.ToDouble(value)) ? strKetQuaHam : value.ToString();
                                    }
                                }
                                else if (strCongThuc.StartsWith("=MIN("))
                                {
                                    if (CommonFunction.IsNumeric(value))
                                    {
                                        strKetQuaHam = (Convert.ToDouble(strKetQuaHam) < Convert.ToDouble(value)) ? strKetQuaHam : value.ToString();
                                    }
                                }
                                else if (strCongThuc.StartsWith("=LIETKE("))
                                {
                                    strKetQuaHam += "\n" + value.ToString();
                                }
                            }
                        }
                    }
                    arrGiaTri[h][c] = strKetQuaHam;
                }
            }


            //Ghi lại các giá trị
            String MaChiTiet = "", MaCot_MaDonVi;
            for (h = 0; h < nH; h++)
            {
                MaHang = String.Format("{0}", dtHang.Rows[h]["iID_MaHang"]);
                MaHangMau = String.Format("{0}", dtHang.Rows[h]["iID_MaHangMau"]);
                MaHangMau_Cha = String.Format("{0}", dtHang.Rows[h]["iID_MaHangMau_Cha"]);
                MaDonVi = String.Format("{0}", dtHang.Rows[h]["iID_MaDonVi"]);
                for (c = 0; c < nC; c++)
                {
                    if (arrGiaTriCu[h][c] != arrGiaTri[h][c])
                    {
                        MaCot = String.Format("{0}", dtCot.Rows[c]["iID_MaCot"]);
                        MaCotMau = String.Format("{0}", dtCot.Rows[c]["iID_MaCotMau"]);
                        MaCot_MaDonVi = String.Format("{0}", dtCot.Rows[c]["iID_MaDonVi"]);

                        MaChiTiet = "";
                        ok = false;
                        for (i = 0; i < dtChiTiet.Rows.Count; i++)
                        {
                            if (h == Convert.ToInt32(dtChiTiet.Rows[i]["iHang"]) - 1 &&
                                c == Convert.ToInt32(dtChiTiet.Rows[i]["iCot"]) - 1)
                            {
                                MaChiTiet = Convert.ToString(dtChiTiet.Rows[i]["iID_ChiTietBang"]);
                                ok = true;
                                break;
                            }
                        }
                        if (ok)
                        {
                            cmd = new SqlCommand();
                            cmd.CommandText = "UPDATE BC_Bang_ChiTiet SET oGiaTri=@oGiaTri WHERE iID_ChiTietBang=@iID_ChiTietBang";
                            cmd.Parameters.AddWithValue("@oGiaTri", arrGiaTri[h][c]);
                            cmd.Parameters.AddWithValue("@iID_ChiTietBang", MaChiTiet);
                            Connection.UpdateDatabase(cmd);
                            cmd.Dispose();
                        }
                        else
                        {
                            cmd = new SqlCommand();
                            cmd.CommandText = "INSERT INTO BC_Bang_ChiTiet(iID_MaBang ,iID_MaBangMau ,iID_MaHang ,iID_MaHangMau_Cha ,iID_MaHangMau ,iID_MaCot ,iID_MaCot_MaDonVi ,iID_MaCotMau ,iID_MaDonVi ,dNgayBaoCao ,oGiaTri ,iHang ,iCot) " +
                                                                   "VALUES(@iID_MaBang,@iID_MaBangMau,@iID_MaHang,@iID_MaHangMau_Cha,@iID_MaHangMau,@iID_MaCot,@iID_MaCot_MaDonVi,@iID_MaCotMau,@iID_MaDonVi,@dNgayBaoCao,@oGiaTri,@iHang,@iCot)";
                            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
                            cmd.Parameters.AddWithValue("@iID_MaBangMau", dtBang.Rows[0]["IID_MaBangMau"]);
                            cmd.Parameters.AddWithValue("@iID_MaHang", MaHang);
                            cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMau_Cha);
                            cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
                            cmd.Parameters.AddWithValue("@iID_MaCot", MaCot);
                            cmd.Parameters.AddWithValue("@iID_MaCot_MaDonVi", MaCot_MaDonVi);
                            cmd.Parameters.AddWithValue("@iID_MaCotMau", MaCotMau);
                            cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                            cmd.Parameters.AddWithValue("@dNgayBaoCao", dtBang.Rows[0]["dNgayBaoCao"]);
                            cmd.Parameters.AddWithValue("@oGiaTri", arrGiaTri[h][c]);
                            cmd.Parameters.AddWithValue("@iHang", h + 1);
                            cmd.Parameters.AddWithValue("@iCot", c + 1);
                            Connection.UpdateDatabase(cmd);
                            cmd.Dispose();
                        }
                    }
                }
            }

            return true;
        }

        private static Object ThucHienBieuThuc(DataTable dtHang, DataTable dtCot, String strCongThuc, ref List<List<object>> arrGiaTri)
        {
            Object value = "";
            int h1, c1;
            if (strCongThuc.StartsWith("="))
            {
                strCongThuc = strCongThuc.Substring(1);
            }
            while (strCongThuc.IndexOf("[") >= 0)
            {
                value = 0;
                int cs1 = strCongThuc.IndexOf("[");
                int cs2 = strCongThuc.IndexOf("]");
                String strTG = strCongThuc.Substring(cs1 + 1, cs2 - cs1 - 1);

                String[] arrTg = strTG.Split('_');
                String MaHang1 = arrTg[0];
                String MaCot1 = arrTg[1];
                h1 = BangModels_HamChung.LayCSH(dtHang, MaHang1);
                c1 = BangModels_HamChung.LayCSC(dtCot, MaCot1);
                value = "";
                if (h1 >= 0 && c1 >= 0)
                {
                    value = arrGiaTri[h1][c1];
                }
                //Trường hợp phép toán +-*/
                if (value == "")
                {
                    value = 0;
                }
                strCongThuc = strCongThuc.Substring(0, cs1) +
                              value +
                              strCongThuc.Substring(cs2 + 1);
            }
            value = "";
            try
            {
                value = CString.Evaluate(strCongThuc);
            }
            catch
            {
            }
            if (CommonFunction.IsNumeric(value))
            {
                value = Convert.ToDouble(value).ToString("#0.##");
            }
            return value;
        }

        private static List<List<ThongSoO>> XacDinhCongThucAnhHuongO(DataTable dtHang, DataTable dtCot, DataTable dtCongThuc)
        {
            int i, h, c, cs1, cs2;
            String strCongThuc, MaHang, MaCot;
            List<List<ThongSoO>> vR = new List<List<ThongSoO>>();
            ThongSoO tg;



            for (h = 0; h < dtHang.Rows.Count; h++)
            {
                vR.Add(new List<ThongSoO>());
                for (c = 0; c < dtCot.Rows.Count; c++)
                {
                    vR[h].Add(new ThongSoO());
                }
            }

            for (h = 0; h < dtHang.Rows.Count; h++)
            {
                MaHang = Convert.ToString(dtHang.Rows[h]["iID_MaHang"]);
                for (c = 0; c < dtCot.Rows.Count; c++)
                {
                    MaCot = Convert.ToString(dtCot.Rows[c]["iID_MaCot"]);
                    strCongThuc = "";
                    for (i = 0; i < dtCongThuc.Rows.Count; i++)
                    {
                        if (MaHang == Convert.ToString(dtCongThuc.Rows[i]["iID_MaHang"]) &&
                            MaCot == Convert.ToString(dtCongThuc.Rows[i]["iID_MaCot"]))
                        {
                            strCongThuc = Convert.ToString(dtCongThuc.Rows[i]["sCongThuc"]);
                            break;
                        }
                    }
                    if (String.IsNullOrEmpty(strCongThuc))
                    {
                        strCongThuc = Convert.ToString(dtHang.Rows[h]["sCongThuc"]);
                        if (String.IsNullOrEmpty(strCongThuc) == false)
                        {
                            strCongThuc = strCongThuc.Replace("#", MaCot);
                        }
                    }
                    if (String.IsNullOrEmpty(strCongThuc))
                    {
                        strCongThuc = Convert.ToString(dtCot.Rows[c]["sCongThuc"]);
                        if (String.IsNullOrEmpty(strCongThuc) == false)
                        {
                            strCongThuc = strCongThuc.Replace("#", MaHang);
                        }
                    }
                    if (String.IsNullOrEmpty(strCongThuc) == false)
                    {
                        tg = vR[h][c];
                        tg.CongThuc = strCongThuc;

                        cs1 = 0;
                        while (cs1 < strCongThuc.Length)
                        {
                            if (strCongThuc[cs1] == '[')
                            {
                                for (cs2 = cs1 + 1; strCongThuc[cs2] != ']'; cs2++) ;

                                String strTG = strCongThuc.Substring(cs1 + 1, cs2 - cs1 - 1);

                                String[] arrTg = strTG.Split('_');
                                String MaHang1 = arrTg[0];
                                String MaCot1 = arrTg[1];

                                int h1 = BangModels_HamChung.LayCSH(dtHang, MaHang1);
                                int c1 = BangModels_HamChung.LayCSC(dtCot, MaCot1);
                                if (h1 >= 0 && c1 >= 0)
                                {
                                    vR[h1][c1].DanhSachCongThuc_h.Add(h);
                                    vR[h1][c1].DanhSachCongThuc_c.Add(c);
                                    vR[h][c].DanhSachO_h.Add(h1);
                                    vR[h][c].DanhSachO_c.Add(c1);
                                }
                                cs1 = cs2 + 1;
                            }
                            cs1++;
                        }
                    }
                }
            }
            return vR;
        }

        private static void GoiHamKhiCoThayDoiO(DataTable dtHang, DataTable dtCot, DataTable dtCongThuc, int csH, int csC, ref List<List<object>> arrGiaTri, ref List<List<ThongSoO>> arrThongSo, ref List<List<bool>> arrThayDoiGiaTri)
        {
            object value;
            int i, j, k, h, c, h1, c1;
            String strCongThuc;
            String MaO = String.Format("[{0}_{1}]", dtHang.Rows[csH]["iID_MaHang"], dtCot.Rows[csC]["iID_MaCot"]);
            ThongSoO tg;

            for (k = 0; k < arrThongSo[csH][csC].DanhSachCongThuc_h.Count; k++)
            {
                //Lấy ô có công thức có chứa ô (csH,csC)
                h = arrThongSo[csH][csC].DanhSachCongThuc_h[k];
                c = arrThongSo[csH][csC].DanhSachCongThuc_c[k];

                tg = arrThongSo[h][c];
                strCongThuc = arrThongSo[h][c].CongThuc;
                if (String.IsNullOrEmpty(strCongThuc) == false && strCongThuc.IndexOf(MaO) >= 0 &&
                    !(strCongThuc.StartsWith("=MAX(") || strCongThuc.StartsWith("=MIN(") ||
                      strCongThuc.StartsWith("=LIETKE(") || strCongThuc.StartsWith("=VITRIHANG")))
                {
                    value = ThucHienBieuThuc(dtHang, dtCot, strCongThuc, ref arrGiaTri);
                    if (value != arrGiaTri[h][c])
                    {
                        arrGiaTri[h][c] = value;
                        GoiHamKhiCoThayDoiO(dtHang, dtCot, dtCongThuc, h, c, ref arrGiaTri, ref arrThongSo, ref arrThayDoiGiaTri);
                    }
                    arrThayDoiGiaTri[csH][csC] = false;
                    for (i = 0; i < tg.DanhSachO_h.Count; i++)
                    {
                        h1 = tg.DanhSachO_h[i];
                        c1 = tg.DanhSachO_c[i];
                        if (arrThayDoiGiaTri[h1][c1])
                        {
                            for (j = 0; j < arrThongSo[h1][c1].DanhSachCongThuc_h.Count; j++)
                            {
                                if (arrThongSo[h1][c1].DanhSachCongThuc_h[j] == h && arrThongSo[h1][c1].DanhSachCongThuc_c[j] == c)
                                {
                                    arrThongSo[h1][c1].DanhSachCongThuc_h.RemoveAt(j);
                                    arrThongSo[h1][c1].DanhSachCongThuc_c.RemoveAt(j);
                                    break;
                                }
                            }
                            if (arrThongSo[h1][c1].DanhSachCongThuc_h.Count == 0)
                            {
                                arrThayDoiGiaTri[h1][c1] = false;
                            }
                        }
                    }
                }
            }
        }

        public static object LayGiaTriTuBangKhac(String MaHangMau, String MaHangMau_Cha, String MaCotMau, Boolean ThuocDonVi, String MaDonVi,
                                                 DataTable dtBang, DataTable dtHang, DataTable dtCot, DataTable dtChiTiet)
        {
            Object vR = "";
            int i, csH, csC;

            csH = -1;
            csC = -1;
            for (i = 0; i < dtHang.Rows.Count; i++)
            {
                if (Convert.ToString(dtHang.Rows[i]["iID_MaHangMau"]) == MaHangMau && (MaHangMau_Cha == "" || Convert.ToString(dtHang.Rows[i]["iID_MaHangMau_Cha"]) == MaHangMau_Cha))
                {
                    csH = i;
                    break;
                }
            }
            for (i = 0; i < dtCot.Rows.Count; i++)
            {
                if (Convert.ToString(dtCot.Rows[i]["iID_MaCotMau"]) == MaCotMau)
                {
                    if (ThuocDonVi == false ||
                        (ThuocDonVi && MaDonVi == Convert.ToString(dtCot.Rows[i]["iID_MaDonVi"])))
                    {
                        csC = i;
                        break;
                    }
                }
            }
            if (csH >= 0 && csC >= 0)
            {
                for (i = 0; i < dtChiTiet.Rows.Count; i++)
                {
                    if (Convert.ToString(dtChiTiet.Rows[i]["iID_MaHang"]) == Convert.ToString(dtHang.Rows[csH]["iID_MaHang"]) &&
                        Convert.ToString(dtChiTiet.Rows[i]["iID_MaCot"]) == Convert.ToString(dtCot.Rows[csC]["iID_MaCot"]))
                    {
                        vR = dtChiTiet.Rows[i]["oGiaTri"];
                        break;
                    }
                }
            }
            return vR;
        }

        public static int LayChiSoBangDuLieu(String MaBangMau,
                                         Boolean XemTheoDonVi,
                                         String MaDonVi,
                                         int KhoangCachThoiGian,
                                         DateTime NgayBaoCao,
                                         int LoaiLuyKe,
                                         Boolean ChiTietThoiGian,
                                         ref List<DataTable> arrDTBang,
                                         ref List<DataTable> arrDTHang,
                                         ref List<DataTable> arrDTCot,
                                         ref List<DataTable> arrDTChiTiet)
        {
            int vR = -1, i;


            SqlCommand cmd = new SqlCommand("SELECT * FROM BC_BangMau WHERE iID_MaBangMau=@iID_MaBangMau");
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            DataTable dtBangMau = Connection.GetDataTable(cmd);
            cmd.Dispose();


            int LoaiBaoCao = Convert.ToInt16(dtBangMau.Rows[0]["iLoaiBaoCao"]);

            if (LoaiLuyKe == -1)
            {
                //Nếu theo lũy kế thì không tính theo khoảng cách ngày
                if (ChiTietThoiGian)
                {
                    switch (LoaiBaoCao)
                    {
                        case 1:
                            NgayBaoCao = NgayBaoCao.AddDays(KhoangCachThoiGian);
                            break;
                        case 2:
                            NgayBaoCao = NgayBaoCao.AddMonths(KhoangCachThoiGian);
                            break;

                        case 3:
                        case 4:
                        case 5:
                            NgayBaoCao = NgayBaoCao.AddMonths(KhoangCachThoiGian);
                            break;
                    }
                }
                else
                {
                    switch (LoaiBaoCao)
                    {
                        case 1:
                            NgayBaoCao = NgayBaoCao.AddDays(KhoangCachThoiGian);
                            break;

                        case 2:
                        case 3:
                        case 5:
                            NgayBaoCao = NgayBaoCao.AddMonths(KhoangCachThoiGian);
                            break;


                        case 4:
                            NgayBaoCao = NgayBaoCao.AddYears(KhoangCachThoiGian);
                            break;
                    }
                }
            }

            String MaBangDuLieu;

            if (Convert.ToBoolean(dtBangMau.Rows[0]["bXemTheoDonVi"]))
            {
                MaBangDuLieu = LayMaBang(MaBangMau, true, MaDonVi, LoaiBaoCao, NgayBaoCao, LoaiLuyKe);
            }
            else
            {
                MaBangDuLieu = LayMaBang(MaBangMau, false, MaDonVi, LoaiBaoCao, NgayBaoCao, LoaiLuyKe);
            }

            if (String.IsNullOrEmpty(MaBangDuLieu) == false)
            {
                for (i = 0; i < arrDTBang.Count; i++)
                {
                    if (MaBangDuLieu == Convert.ToString(arrDTBang[i].Rows[0]["iID_MaBang"]))
                    {
                        vR = i;
                        break;
                    }
                }
                if (vR == -1)
                {
                    cmd = new SqlCommand("SELECT * FROM BC_Bang WHERE iID_MaBang=@iID_MaBang");
                    cmd.Parameters.AddWithValue("@iID_MaBang", MaBangDuLieu);
                    arrDTBang.Add(Connection.GetDataTable(cmd));
                    cmd.Dispose();

                    cmd = new SqlCommand();
                    cmd.CommandText = "SELECT * " +
                                      "FROM BC_Bang_Cot " +
                                      "WHERE iID_MaBang=@iID_MaBang ORDER BY iSTT;";
                    cmd.Parameters.AddWithValue("@iID_MaBang", MaBangDuLieu);
                    arrDTCot.Add(Connection.GetDataTable(cmd));
                    cmd.Dispose();

                    cmd = new SqlCommand("SELECT * FROM BC_Bang_Hang WHERE iID_MaBang=@iID_MaBang ORDER BY iSTT");
                    cmd.Parameters.AddWithValue("@iID_MaBang", MaBangDuLieu);
                    arrDTHang.Add(Connection.GetDataTable(cmd));
                    cmd.Dispose();

                    cmd = new SqlCommand("SELECT * FROM BC_Bang_ChiTiet WHERE iID_MaBang=@iID_MaBang");
                    cmd.Parameters.AddWithValue("@iID_MaBang", MaBangDuLieu);
                    arrDTChiTiet.Add(Connection.GetDataTable(cmd));
                    cmd.Dispose();

                    vR = arrDTBang.Count - 1;
                }
            }
            return vR;
        }


        public static void XacDinhDanhSachCacMaHangSeHienThi(String MaBang, int MaHangMauCha, ref String XauMaHangHienThi, ref String XauMaHangMauThuocNhanh)
        {
            XauMaHangMauThuocNhanh += String.Format("{0},", MaHangMauCha);
            String SQL = String.Format("SELECT * FROM BC_Bang_Hang WHERE iID_MaBang='{0}' AND iID_MaHangMau_Cha={1}", MaBang, MaHangMauCha);
            DataTable dt = Connection.GetDataTable(SQL);
            int i, MaHangMauChaMoi;

            for (i = 0; i < dt.Rows.Count; i++)
            {
                XauMaHangHienThi += String.Format("{0},", dt.Rows[i]["iID_MaHang"]);
            }
            SQL = String.Format("SELECT iID_MaHangMau_Cha FROM BC_Bang_Hang WHERE iID_MaBang='{0}' AND iID_MaHangMau={1}", MaBang, MaHangMauCha);
            MaHangMauChaMoi = Convert.ToInt32(Connection.GetValue(SQL, -1));
            if (MaHangMauChaMoi >= 0)
            {
                XacDinhDanhSachCacMaHangSeHienThi(MaBang, MaHangMauChaMoi, ref XauMaHangHienThi, ref XauMaHangMauThuocNhanh);
            }
        }

        public static String ExportExcel(String MaBang, String Path)
        {
            // Get process ids before running the excel codes
            CheckExcellProcesses();
            //khoi tao cac doi tuong Com Excel de lam viec
            Excel.Application xlApp;
            Excel.Worksheet xlSheet;
            Excel.Workbook xlBook;
            Excel.Range caption;

            //doi tuong Trống để thêm  vào xlApp sau đó lưu lại sau
            object missValue = System.Reflection.Missing.Value;

            //khoi tao doi tuong Com Excel moi
            xlApp = new Excel.Application();
            xlBook = xlApp.Workbooks.Add(missValue);

            //su dung Sheet dau tien de thao tac
            xlSheet = (Excel.Worksheet)xlBook.Worksheets.get_Item(1);
            //không cho hiện ứng dụng Excel lên để tránh gây đơ máy
            xlApp.Visible = false;

            //Bỏ lock toàn bộ cell trong sheet
            xlApp.Cells.Locked = false;

            //set thuoc tinh cho tieu de
            caption = xlSheet.get_Range("A1", "A1");
            caption.Select();
            caption.Value2 = MaBang;

            SqlCommand cmd;

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * " +
                              "FROM BC_Bang " +
                              "WHERE iID_MaBang=@iID_MaBang;";
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            DataTable dtBang = Connection.GetDataTable(cmd);
            cmd.Dispose();

            String MaBangMau = Convert.ToString(dtBang.Rows[0]["iID_MaBangMau"]);
            DateTime Ngay = Convert.ToDateTime(dtBang.Rows[0]["dNgayBaoCao"]);
            int LoaiBaoCao = Convert.ToInt16(dtBang.Rows[0]["iLoaiBaoCao"]);
            String ThoiGian = "";
            switch (LoaiBaoCao)
            {
                case 1:
                    ThoiGian = Ngay.ToString("dd/MM/yyyy");
                    break;
                case 2:
                    ThoiGian = Ngay.ToString("MM/yyyy");
                    break;
                case 3:
                    ThoiGian = Ngay.ToString("MM/yyyy");
                    break;
                case 4:
                    ThoiGian = Ngay.ToString("yyyy");
                    break;
                case 5:
                    ThoiGian = Ngay.ToString("MM/yyyy");
                    break;
            }

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT *, 0 AS ChiSo " +
                              "FROM BC_Bang_Cot " +
                              "WHERE iID_MaBang=@iID_MaBang " +
                              "ORDER BY iSTT;";
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            DataTable dtCot = Connection.GetDataTable(cmd);
            cmd.Dispose();

            DataTable dtHang;
            cmd = new SqlCommand();
            cmd.CommandText = "SELECT *, -1 AS ChiSo " +
                              "FROM BC_Bang_Hang " +
                              "WHERE iID_MaBang=@iID_MaBang " +
                              "ORDER BY iSTT";
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            dtHang = Connection.GetDataTable(cmd);
            cmd.Dispose();

            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            cmd.CommandText = "SELECT * " +
                              " FROM BC_Bang_CongThuc " +
                              " WHERE (iID_MaBang=@iID_MaBang)";
            DataTable dtCongThuc = Connection.GetDataTable(cmd);
            cmd.Dispose();

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * " +
                              "FROM BC_BangMau_CotMau_DonVi " +
                              "WHERE iID_MaBangMau=@iID_MaBangMau;";
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            DataTable dtCotMauDonVi = Connection.GetDataTable(cmd);
            cmd.Dispose();

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * " +
                              "FROM BC_BangMau_CotMau_DonVi_TenMoi " +
                              "WHERE iID_MaBangMau=@iID_MaBangMau;";
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            DataTable dtCotMauDonViTenMoi = Connection.GetDataTable(cmd);
            cmd.Dispose();

            Boolean XemTheoDonVi = Convert.ToBoolean(dtBang.Rows[0]["bXemTheoDonVi"]);
            String MaDonVi = Convert.ToString(dtBang.Rows[0]["iID_MaDonVi"]);

            cmd = new SqlCommand();
            if (XemTheoDonVi)
            {
                cmd.CommandText = "SELECT * " +
                                  " FROM BC_DonVi " +
                                  " WHERE (iID_MaDonVi = @iID_MaDonVi)" +
                                  " ORDER BY iSTT;";
                cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
            }
            else
            {
                cmd.CommandText = "SELECT * " +
                                  " FROM BC_DonVi " +
                                  " ORDER BY iSTT;";
            }
            DataTable dtDonVi = Connection.GetDataTable(cmd);
            cmd.Dispose();


            String strCS1, strCS2, sColor;
            int C0 = 2, H0 = 6, SoCotHienThi = 0, d = 0;
            int i, j, ChiSoHang, ChiSoCot;

            //Lấy thông tin
            List<List<object>> arrGiaTriChiTietCu = null, arrGiaTriChiTiet = null;
            List<Boolean> arrCotHienThi = new List<Boolean>();
            for (i = 0; i <= dtCot.Rows.Count - 1; i++)
            {
                arrCotHienThi.Add(Convert.ToBoolean(dtCot.Rows[i]["bVisible"]));
                if (arrCotHienThi[i] && Convert.ToBoolean(dtCot.Rows[i]["bThuocDonVi"]))
                {
                    for (j = 0; j < dtCotMauDonVi.Rows.Count; j++)
                    {
                        if (Convert.ToString(dtCot.Rows[i]["iID_MaCotMau"]) == Convert.ToString(dtCotMauDonVi.Rows[j]["iID_MaCotMau"]) &&
                            Convert.ToString(dtCot.Rows[i]["iID_MaDonVi"]) == Convert.ToString(dtCotMauDonVi.Rows[j]["iID_MaDonVi"]))
                        {
                            arrCotHienThi[i] = false;
                            break;
                        }
                    }
                }
            }
            String strXauMaGiaTriKhac = "", strXauGiaTriKhac = "";
            LayThongTinCuaBang(false, dtBang, dtHang, dtCot, dtDonVi, ref dtCongThuc, ref arrGiaTriChiTiet, ref arrGiaTriChiTietCu, ref arrCotHienThi, ref strXauMaGiaTriKhac, ref strXauGiaTriKhac);

            //Điền thông tin các cột vào Excel
            d = 0;
            for (j = 0; j < dtCot.Rows.Count; j++)
            {
                d++;
                dtCot.Rows[j]["ChiSo"] = C0 + d - 1;

                strCS1 = String.Format("{0}1", ExportExcel_MaCot(C0 + d - 1));
                caption = xlSheet.get_Range(strCS1, strCS1);
                caption.Value2 = dtCot.Rows[j]["iID_MaCot"];
                caption.EntireColumn.ColumnWidth = Convert.ToDouble(dtCot.Rows[j]["sWidth"]) / 8;
                if (arrCotHienThi[j] == false)
                {
                    caption.EntireColumn.ColumnWidth = 0;
                }
                if (Convert.ToInt16(dtCot.Rows[j]["iKieuDuLieu"]) == 1)
                {
                    String strF = "#,###";
                    if (Convert.ToInt16(dtCot.Rows[j]["iSoSauDauPhay"]) == -1)
                    {
                        strF += ".##";
                    }
                    else
                    {
                        for (i = 1; i <= Convert.ToInt16(dtCot.Rows[j]["iSoSauDauPhay"]); i++)
                        {
                            if (i == 1) strF += ".";
                            strF += "0";
                        }
                    }
                    caption.EntireColumn.NumberFormat = strF;
                }
            }
            SoCotHienThi = d + 1;

            //Điền thông tin các hàng vào Excel
            for (i = 0; i < dtHang.Rows.Count; i++)
            {
                dtHang.Rows[i]["ChiSo"] = H0 + i;

                strCS1 = String.Format("A{0}", H0 + i);
                caption = xlSheet.get_Range(strCS1, strCS1);
                caption.Value2 = dtHang.Rows[i]["iID_MaHang"];

                strCS1 = String.Format("B{0}", H0 + i);
                caption = xlSheet.get_Range(strCS1, strCS1);
                caption.Font.Bold = false;
                caption.Font.Name = "Times New Roman";
                caption.Font.Size = 12;
                caption.Select();
                caption.Value2 = dtHang.Rows[i]["sTenHang"];


                strCS1 = String.Format("A{0}", H0 + i);
                strCS2 = String.Format("{0}{1}", ExportExcel_MaCot(C0 + SoCotHienThi - 2), H0 + i);
                caption = xlSheet.get_Range(strCS1, strCS2);

                sColor = Convert.ToString(dtHang.Rows[i]["sBackground"]).Substring(1);
                sColor = sColor.Substring(2) + sColor.Substring(0, 2);
                caption.Interior.Color = Int32.Parse(sColor, System.Globalization.NumberStyles.HexNumber);
                sColor = Convert.ToString(dtHang.Rows[i]["sColor"]).Substring(1);
                sColor = sColor.Substring(2) + sColor.Substring(0, 2);
                caption.Font.Color = Int32.Parse(sColor, System.Globalization.NumberStyles.HexNumber);
                caption.Font.Bold = Convert.ToBoolean(dtHang.Rows[i]["bBold"]);
                caption.Font.Italic = Convert.ToBoolean(dtHang.Rows[i]["bItalic"]);
                caption.Font.Underline = Convert.ToBoolean(dtHang.Rows[i]["bUnderline"]);
                caption.Font.Size = Convert.ToInt32(dtHang.Rows[i]["iFontSize"]);
                caption.RowHeight = Convert.ToInt32(dtHang.Rows[i]["iHeight"]);
                caption.Borders.ColorIndex = 1;

                caption.VerticalAlignment = Excel.Constants.xlCenter;
            }

            caption = xlSheet.get_Range("A1", "A1");
            caption.EntireColumn.Hidden = true;
            caption.EntireRow.Hidden = true;



            //Hàng tiêu đề của bảng
            int MocH1 = -1, MocH2 = -1;
            Boolean CoH1, CoH2;
            String TenDonVi = "";

            strCS1 = "B3";
            strCS2 = String.Format("{0}5", ExportExcel_MaCot(C0 + SoCotHienThi - 2));
            caption = xlSheet.get_Range(strCS1, strCS2);
            caption.Borders.ColorIndex = 1;

            strCS1 = "B3";
            strCS2 = "B5";
            caption = xlSheet.get_Range(strCS1, strCS2);
            caption.Merge(false);
            caption.HorizontalAlignment = Excel.Constants.xlCenter;
            caption.Font.Bold = true;
            caption.VerticalAlignment = Excel.Constants.xlCenter;
            caption.Font.Name = "Times New Roman";
            caption.Font.Size = 12;
            caption.Value2 = "Chỉ tiêu";
            caption.EntireColumn.ColumnWidth = 50;
            for (i = 0; i <= dtCot.Rows.Count - 1; i++)
            {
                if (arrCotHienThi[i])
                {
                    //dtCot.Rows[i]["sTenCot"]
                    if (Convert.ToBoolean(dtCot.Rows[i]["bThuocDonVi"]))
                    {
                        for (j = 0; j < dtCotMauDonViTenMoi.Rows.Count; j++)
                        {
                            if (Convert.ToInt32(dtCot.Rows[i]["iID_MaCotMau"]) == Convert.ToInt32(dtCotMauDonViTenMoi.Rows[j]["iID_MaCotMau"]) &&
                                Convert.ToInt32(dtCot.Rows[i]["iID_MaDonVi"]) == Convert.ToInt32(dtCotMauDonViTenMoi.Rows[j]["iID_MaDonVi"]))
                            {
                                dtCot.Rows[i]["sTenCot"] = dtCotMauDonViTenMoi.Rows[j]["sTenCot"];
                                break;
                            }
                        }
                    }
                    CoH1 = false;
                    CoH2 = false;
                    if (i > MocH1 && Convert.ToBoolean(dtCot.Rows[i]["bThuocDonVi"]))
                    {
                        CoH1 = true;
                        d = 1;
                        for (j = i + 1; j <= dtCot.Rows.Count - 1; j++)
                        {
                            if (Convert.ToBoolean(dtCot.Rows[j]["bThuocDonVi"]) && Convert.ToString(dtCot.Rows[i]["iID_MaDonVi"]) == Convert.ToString(dtCot.Rows[j]["iID_MaDonVi"]))
                            {
                                d = d + 1;
                                MocH1 = j;
                            }
                            else
                            {
                                break;
                            }
                        }
                        cmd = new SqlCommand();
                        cmd.CommandText = "SELECT sTenVietTat FROM BC_DonVi WHERE iID_MaDonVi=@iID_MaDonVi";
                        cmd.Parameters.AddWithValue("@iID_MaDonVi", dtCot.Rows[i]["iID_MaDonVi"]);
                        TenDonVi = Connection.GetValueString(cmd, "");
                        cmd.Dispose();
                        ChiSoCot = Convert.ToInt16(dtCot.Rows[i]["ChiSo"]);
                        strCS1 = String.Format("{0}3", ExportExcel_MaCot(ChiSoCot));
                        strCS2 = String.Format("{0}3", ExportExcel_MaCot(ChiSoCot + d - 1));
                        caption = xlSheet.get_Range(strCS1, strCS2);
                        caption.Merge(false);
                        caption.HorizontalAlignment = Excel.Constants.xlCenter;
                        caption.Font.Bold = true;
                        caption.VerticalAlignment = Excel.Constants.xlCenter;
                        caption.Font.Name = "Times New Roman";
                        caption.Font.Size = 12;
                        caption.Value2 = TenDonVi;
                        caption.Locked = true;
                        //MocH1 = i + d - 1;
                    }
                    else if (i <= MocH1)
                    {
                        CoH1 = true;
                    }
                    if (i > MocH2 && Convert.ToBoolean(dtCot.Rows[i]["bNhomCon"]))
                    {
                        CoH2 = true;
                        d = 1;
                        for (j = i + 1; j <= dtCot.Rows.Count - 1; j++)
                        {
                            if (Convert.ToBoolean(dtCot.Rows[j]["bNhomCon"]) && Convert.ToBoolean(dtCot.Rows[i]["bThuocDonVi"]) == Convert.ToBoolean(dtCot.Rows[j]["bThuocDonVi"]) && Convert.ToString(dtCot.Rows[i]["sTenNhomCon"]) == Convert.ToString(dtCot.Rows[j]["sTenNhomCon"]))
                            {
                                d = d + 1;
                                MocH2 = j;
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (CoH1)
                        {
                            ChiSoCot = Convert.ToInt16(dtCot.Rows[i]["ChiSo"]);
                            strCS1 = String.Format("{0}4", ExportExcel_MaCot(ChiSoCot));
                            strCS2 = String.Format("{0}4", ExportExcel_MaCot(ChiSoCot + d - 1));
                            caption = xlSheet.get_Range(strCS1, strCS2);
                            caption.Merge(false);
                            caption.HorizontalAlignment = Excel.Constants.xlCenter;
                            caption.Font.Bold = true;
                            caption.VerticalAlignment = Excel.Constants.xlCenter;
                            caption.Font.Name = "Times New Roman";
                            caption.Font.Size = 12;
                            caption.Value2 = dtCot.Rows[i]["sTenNhomCon"].ToString();
                            caption.Locked = true;
                        }
                        else
                        {
                            ChiSoCot = Convert.ToInt16(dtCot.Rows[i]["ChiSo"]);
                            strCS1 = String.Format("{0}3", ExportExcel_MaCot(ChiSoCot));
                            strCS2 = String.Format("{0}4", ExportExcel_MaCot(ChiSoCot + d - 1));
                            caption = xlSheet.get_Range(strCS1, strCS2);
                            caption.Merge(false);
                            caption.HorizontalAlignment = Excel.Constants.xlCenter;
                            caption.Font.Bold = true;
                            caption.VerticalAlignment = Excel.Constants.xlCenter;
                            caption.Font.Name = "Times New Roman";
                            caption.Font.Size = 12;
                            caption.Value2 = dtCot.Rows[i]["sTenNhomCon"].ToString();
                            caption.Locked = true;
                        }
                        //MocH2 = i + d - 1;
                    }
                    else if (i <= MocH2)
                    {
                        CoH2 = true;
                    }
                    String TenCotHienThi = "";
                    String[] arrThoiGianLay;
                    String strChuoiTenThayDoi = "";
                    d = 1;
                    if (CoH2 == false)
                    {
                        if (CoH1 == false)
                        {
                            if (Convert.ToString(dtCot.Rows[i]["sTenCot"]).IndexOf("{Y}") != -1)
                            {
                                TenCotHienThi = Convert.ToString(dtCot.Rows[i]["sTenCot"]).Replace("{Y}", "" + Ngay.Year + "");
                            }
                            else if (Convert.ToString(dtCot.Rows[i]["sTenCot"]).IndexOf("{Y-1}") != -1)
                            {
                                TenCotHienThi = Convert.ToString(dtCot.Rows[i]["sTenCot"]).Replace("{Y-1}", "" + (Convert.ToInt32(Ngay.Year) - 1) + "");
                            }
                            else if (Convert.ToString(dtCot.Rows[i]["sTenCot"]).IndexOf("{T}") != -1)
                            {
                                TenCotHienThi = Convert.ToString(dtCot.Rows[i]["sTenCot"]).Replace("{T}", "" + Ngay.Month + "");
                            }
                            else if (Convert.ToString(dtCot.Rows[i]["sTenCot"]).IndexOf("{T-1}") != -1)
                            {
                                TenCotHienThi = Convert.ToString(dtCot.Rows[i]["sTenCot"]).Replace("{T-1}", "" + (Ngay.Month - 1) + "");
                            }
                            else if (Convert.ToString(dtCot.Rows[i]["sTenCot"]).IndexOf("{N}") != -1)
                            {
                                TenCotHienThi = Convert.ToString(dtCot.Rows[i]["sTenCot"]).Replace("{N}", "" + Ngay.Day + "");
                            }
                            else if (Convert.ToString(dtCot.Rows[i]["sTenCot"]).IndexOf("{N-1}") != -1)
                            {
                                TenCotHienThi = Convert.ToString(dtCot.Rows[i]["sTenCot"]).Replace("{N-1}", "" + (Ngay.Day - 1) + "");
                            }
                            else if (Convert.ToString(dtCot.Rows[i]["sTenCot"]).IndexOf("{Q}") != -1)
                            {
                                arrThoiGianLay = Convert.ToString(ThoiGian).Split('/');
                                switch (arrThoiGianLay[0]) { 
                                    case "01":
                                        strChuoiTenThayDoi = "Quý I";
                                        break;
                                    case "04":
                                        strChuoiTenThayDoi = "Quý II";
                                        break;                            
                                    case "07":
                                        strChuoiTenThayDoi = "Quý III";
                                        break;
                                    case "10":
                                        strChuoiTenThayDoi = "Quý IV";
                                        break;
                                    default:
                                        strChuoiTenThayDoi = "Quý";
                                        break;
                                }
                                TenCotHienThi = Convert.ToString(dtCot.Rows[i]["sTenCot"]).Replace("{Q}", "" + strChuoiTenThayDoi + "-" + arrThoiGianLay[1] + "");
                            }
                            else if (Convert.ToString(dtCot.Rows[i]["sTenCot"]).IndexOf("{S}") != -1)
                            {
                                arrThoiGianLay = Convert.ToString(ThoiGian).Split('/');
                                switch (arrThoiGianLay[0])
                                {
                                    case "01":
                                        strChuoiTenThayDoi = "6 tháng đầu năm";
                                        break;
                                    case "07":
                                        strChuoiTenThayDoi = "6 tháng cuối năm";
                                        break;
                                    default:
                                        strChuoiTenThayDoi = "6 tháng";
                                        break;
                                }
                                TenCotHienThi = Convert.ToString(dtCot.Rows[i]["sTenCot"]).Replace("{S}", "" + strChuoiTenThayDoi + "-" + arrThoiGianLay[1] + "");
                            }
                            else
                            {
                                TenCotHienThi = dtCot.Rows[i]["sTenCot"].ToString();
                            }
                            ChiSoCot = Convert.ToInt16(dtCot.Rows[i]["ChiSo"]);
                            strCS1 = String.Format("{0}3", ExportExcel_MaCot(ChiSoCot));
                            strCS2 = String.Format("{0}5", ExportExcel_MaCot(ChiSoCot));
                            caption = xlSheet.get_Range(strCS1, strCS2);
                            caption.Merge(false);
                            caption.HorizontalAlignment = Excel.Constants.xlCenter;
                            caption.Font.Bold = true;
                            caption.VerticalAlignment = Excel.Constants.xlCenter;
                            caption.Font.Name = "Times New Roman";
                            caption.Font.Size = 12;
                            caption.Select();
                            caption.Value2 = TenCotHienThi;
                            caption.Locked = true;
                        }
                        else
                        {
                            if (Convert.ToString(dtCot.Rows[i]["sTenCot"]).IndexOf("{Y}") != -1)
                            {
                                TenCotHienThi = Convert.ToString(dtCot.Rows[i]["sTenCot"]).Replace("{Y}", "" + Ngay.Year + "");
                            }
                            else if (Convert.ToString(dtCot.Rows[i]["sTenCot"]).IndexOf("{Y-1}") != -1)
                            {
                                TenCotHienThi = Convert.ToString(dtCot.Rows[i]["sTenCot"]).Replace("{Y-1}", "" + (Convert.ToInt32(Ngay.Year) - 1) + "");
                            }
                            else if (Convert.ToString(dtCot.Rows[i]["sTenCot"]).IndexOf("{T}") != -1)
                            {
                                TenCotHienThi = Convert.ToString(dtCot.Rows[i]["sTenCot"]).Replace("{T}", "" + Ngay.Month + "");
                            }
                            else if (Convert.ToString(dtCot.Rows[i]["sTenCot"]).IndexOf("{T-1}") != -1)
                            {
                                TenCotHienThi = Convert.ToString(dtCot.Rows[i]["sTenCot"]).Replace("{T-1}", "" + (Ngay.Month - 1) + "");
                            }
                            else if (Convert.ToString(dtCot.Rows[i]["sTenCot"]).IndexOf("{N}") != -1)
                            {
                                TenCotHienThi = Convert.ToString(dtCot.Rows[i]["sTenCot"]).Replace("{N}", "" + Ngay.Day + "");
                            }
                            else if (Convert.ToString(dtCot.Rows[i]["sTenCot"]).IndexOf("{N-1}") != -1)
                            {
                                TenCotHienThi = Convert.ToString(dtCot.Rows[i]["sTenCot"]).Replace("{N-1}", "" + (Ngay.Day - 1) + "");
                            }
                            else if (Convert.ToString(dtCot.Rows[i]["sTenCot"]).IndexOf("{Q}") != -1)
                            {
                                arrThoiGianLay = Convert.ToString(ThoiGian).Split('/');
                                switch (arrThoiGianLay[0])
                                {
                                    case "01":
                                        strChuoiTenThayDoi = "Quý I";
                                        break;
                                    case "04":
                                        strChuoiTenThayDoi = "Quý II";
                                        break;
                                    case "07":
                                        strChuoiTenThayDoi = "Quý III";
                                        break;
                                    case "10":
                                        strChuoiTenThayDoi = "Quý IV";
                                        break;
                                    default:
                                        strChuoiTenThayDoi = "Quý";
                                        break;
                                }
                                TenCotHienThi = Convert.ToString(dtCot.Rows[i]["sTenCot"]).Replace("{Q}", "" + strChuoiTenThayDoi + "-" + arrThoiGianLay[1] + "");
                            }
                            else if (Convert.ToString(dtCot.Rows[i]["sTenCot"]).IndexOf("{S}") != -1)
                            {
                                arrThoiGianLay = Convert.ToString(ThoiGian).Split('/');
                                switch (arrThoiGianLay[0])
                                {
                                    case "01":
                                        strChuoiTenThayDoi = "6 tháng đầu năm";
                                        break;
                                    case "07":
                                        strChuoiTenThayDoi = "6 tháng cuối năm";
                                        break;
                                    default:
                                        strChuoiTenThayDoi = "6 tháng";
                                        break;
                                }
                                TenCotHienThi = Convert.ToString(dtCot.Rows[i]["sTenCot"]).Replace("{S}", "" + strChuoiTenThayDoi + "-" + arrThoiGianLay[1] + "");
                            }
                            else
                            {
                                TenCotHienThi = dtCot.Rows[i]["sTenCot"].ToString();
                            }
                            ChiSoCot = Convert.ToInt16(dtCot.Rows[i]["ChiSo"]);
                            strCS1 = String.Format("{0}4", ExportExcel_MaCot(ChiSoCot));
                            strCS2 = String.Format("{0}5", ExportExcel_MaCot(ChiSoCot));
                            caption = xlSheet.get_Range(strCS1, strCS2);
                            caption.Merge(false);
                            caption.HorizontalAlignment = Excel.Constants.xlCenter;
                            caption.Font.Bold = true;
                            caption.VerticalAlignment = Excel.Constants.xlCenter;
                            caption.Font.Name = "Times New Roman";
                            caption.Font.Size = 12;
                            caption.Select();
                            caption.Value2 = TenCotHienThi;
                            caption.Locked = true;
                        }
                    }
                    else
                    {
                        if (Convert.ToString(dtCot.Rows[i]["sTenCot"]).IndexOf("{Y}") != -1)
                        {
                            TenCotHienThi = Convert.ToString(dtCot.Rows[i]["sTenCot"]).Replace("{Y}", "" + Ngay.Year + "");
                        }
                        else if (Convert.ToString(dtCot.Rows[i]["sTenCot"]).IndexOf("{Y-1}") != -1)
                        {
                            TenCotHienThi = Convert.ToString(dtCot.Rows[i]["sTenCot"]).Replace("{Y-1}", "" + (Convert.ToInt32(Ngay.Year) - 1) + "");
                        }
                        else if (Convert.ToString(dtCot.Rows[i]["sTenCot"]).IndexOf("{T}") != -1)
                        {
                            TenCotHienThi = Convert.ToString(dtCot.Rows[i]["sTenCot"]).Replace("{T}", "" + Ngay.Month + "");
                        }
                        else if (Convert.ToString(dtCot.Rows[i]["sTenCot"]).IndexOf("{T-1}") != -1)
                        {
                            TenCotHienThi = Convert.ToString(dtCot.Rows[i]["sTenCot"]).Replace("{T-1}", "" + (Ngay.Month - 1) + "");
                        }
                        else if (Convert.ToString(dtCot.Rows[i]["sTenCot"]).IndexOf("{N}") != -1)
                        {
                            TenCotHienThi = Convert.ToString(dtCot.Rows[i]["sTenCot"]).Replace("{N}", "" + Ngay.Day + "");
                        }
                        else if (Convert.ToString(dtCot.Rows[i]["sTenCot"]).IndexOf("{N-1}") != -1)
                        {
                            TenCotHienThi = Convert.ToString(dtCot.Rows[i]["sTenCot"]).Replace("{N-1}", "" + (Ngay.Day - 1) + "");
                        }
                        else if (Convert.ToString(dtCot.Rows[i]["sTenCot"]).IndexOf("{Q}") != -1)
                        {
                            arrThoiGianLay = Convert.ToString(ThoiGian).Split('/');
                            switch (arrThoiGianLay[0])
                            {
                                case "01":
                                    strChuoiTenThayDoi = "Quý I";
                                    break;
                                case "04":
                                    strChuoiTenThayDoi = "Quý II";
                                    break;
                                case "07":
                                    strChuoiTenThayDoi = "Quý III";
                                    break;
                                case "10":
                                    strChuoiTenThayDoi = "Quý IV";
                                    break;
                                default:
                                    strChuoiTenThayDoi = "Quý";
                                    break;
                            }
                            TenCotHienThi = Convert.ToString(dtCot.Rows[i]["sTenCot"]).Replace("{Q}", "" + strChuoiTenThayDoi + "-" + arrThoiGianLay[1] + "");
                        }
                        else if (Convert.ToString(dtCot.Rows[i]["sTenCot"]).IndexOf("{S}") != -1)
                        {
                            arrThoiGianLay = Convert.ToString(ThoiGian).Split('/');
                            switch (arrThoiGianLay[0])
                            {
                                case "01":
                                    strChuoiTenThayDoi = "6 tháng đầu năm";
                                    break;
                                case "07":
                                    strChuoiTenThayDoi = "6 tháng cuối năm";
                                    break;
                                default:
                                    strChuoiTenThayDoi = "6 tháng";
                                    break;
                            }
                            TenCotHienThi = Convert.ToString(dtCot.Rows[i]["sTenCot"]).Replace("{S}", "" + strChuoiTenThayDoi + "-" + arrThoiGianLay[1] + "");
                        }
                        else
                        {
                            TenCotHienThi = dtCot.Rows[i]["sTenCot"].ToString();
                        }
                        ChiSoCot = Convert.ToInt16(dtCot.Rows[i]["ChiSo"]);
                        strCS1 = String.Format("{0}5", ExportExcel_MaCot(ChiSoCot));
                        strCS2 = String.Format("{0}5", ExportExcel_MaCot(ChiSoCot));
                        caption = xlSheet.get_Range(strCS1, strCS2);
                        caption.Merge(false);
                        caption.HorizontalAlignment = Excel.Constants.xlCenter;
                        caption.Font.Bold = true;
                        caption.VerticalAlignment = Excel.Constants.xlCenter;
                        caption.Font.Name = "Times New Roman";
                        caption.Font.Size = 12;
                        caption.Select();
                        caption.Value2 = TenCotHienThi;
                        caption.Locked = true;
                    }
                }
            }


            //Điền dữ liệu
            for (i = 0; i < dtHang.Rows.Count; i++)
            {
                for (j = 0; j < dtCot.Rows.Count; j++)
                {
                    ChiSoCot = Convert.ToInt16(dtCot.Rows[j]["ChiSo"]);
                    if (ChiSoCot >= 0)
                    {
                        strCS1 = String.Format("{0}{1}", ExportExcel_MaCot(ChiSoCot), H0 + i);
                        caption = xlSheet.get_Range(strCS1, strCS1);
                        caption.Font.Bold = false;
                        caption.Font.Name = "Times New Roman";
                        caption.Font.Size = 12;
                        caption.Select();
                        caption.Value2 = arrGiaTriChiTiet[i][j];
                    }
                }
            }

            //Điền công thức
            String strCongThuc, value;
            String MaHang, MaCot, MaHang1, MaCot1, MaO;
            int csH, csC;

            //Tao ham
            int h, c;
            for (h = 0; h < dtHang.Rows.Count; h++)
            {
                MaHang = Convert.ToString(dtHang.Rows[h]["iID_MaHang"]);
                for (c = 0; c < dtCot.Rows.Count; c++)
                {
                    if (arrCotHienThi[c])
                    {
                        MaCot = Convert.ToString(dtCot.Rows[c]["iID_MaCot"]);
                        strCongThuc = "";
                        for (i = 0; i < dtCongThuc.Rows.Count; i++)
                        {
                            if (MaHang == Convert.ToString(dtCongThuc.Rows[i]["iID_MaHang"]) &&
                                MaCot == Convert.ToString(dtCongThuc.Rows[i]["iID_MaCot"]))
                            {
                                strCongThuc = Convert.ToString(dtCongThuc.Rows[i]["sCongThuc"]);
                                if (strCongThuc.StartsWith("=MAX(") || strCongThuc.StartsWith("=MIN(") ||
                                    strCongThuc.StartsWith("=LIETKE(") || strCongThuc.StartsWith("=VITRIHANG"))
                                {
                                    strCongThuc = "  ";
                                }
                                break;
                            }
                        }
                        if (String.IsNullOrEmpty(strCongThuc))
                        {
                            strCongThuc = Convert.ToString(dtCot.Rows[c]["sCongThuc"]);
                            if (String.IsNullOrEmpty(strCongThuc) == false)
                            {
                                strCongThuc = strCongThuc.Replace("#", MaHang);
                            }
                        }
                        if (String.IsNullOrEmpty(strCongThuc) && Convert.ToBoolean(dtCot.Rows[c]["bKhongApDungCongThucHang"]) == false)
                        {
                            strCongThuc = Convert.ToString(dtHang.Rows[h]["sCongThuc"]);
                            if (String.IsNullOrEmpty(strCongThuc) == false)
                            {
                                strCongThuc = strCongThuc.Replace("#", MaCot);
                            }
                        }
                        strCongThuc = strCongThuc.Trim();
                        if (String.IsNullOrEmpty(strCongThuc) == false)
                        {
                            strCongThuc = BangModels_CongThuc.ChuyenCongThucSangExcel(strCongThuc);
                            if (strCongThuc.StartsWith("=") == false)
                            {
                                strCongThuc = "=" + strCongThuc;
                            }
                            d = 0;
                            while (strCongThuc.IndexOf("[") >= 0)
                            {
                                d = d + 1;
                                int cs1 = strCongThuc.IndexOf("[");
                                int cs2 = strCongThuc.IndexOf("]");
                                MaO = strCongThuc.Substring(cs1 + 1, cs2 - cs1 - 1);
                                String[] arrTg = MaO.Split('_');
                                MaHang1 = arrTg[0];
                                MaCot1 = arrTg[1];
                                csH = BangModels_HamChung.LayCSH(dtHang, MaHang1);
                                csC = BangModels_HamChung.LayCSC(dtCot, MaCot1);
                                value = "0";
                                if (csH >= 0 && csC >= 0)
                                {
                                    ChiSoCot = Convert.ToInt32(dtCot.Rows[csC]["ChiSo"]);
                                    ChiSoHang = Convert.ToInt32(dtHang.Rows[csH]["ChiSo"]);
                                    if (ChiSoCot >= 0)
                                    {
                                        value = String.Format("{0}{1}", ExportExcel_MaCot(ChiSoCot), ChiSoHang);
                                    }
                                }
                                strCongThuc = strCongThuc.Substring(0, cs1) +
                                              value +
                                              strCongThuc.Substring(cs2 + 1);

                            }
                            ChiSoCot = Convert.ToInt32(dtCot.Rows[c]["ChiSo"]);
                            ChiSoHang = Convert.ToInt32(dtHang.Rows[h]["ChiSo"]);
                            if (ChiSoCot >= 0)
                            {
                                strCS1 = String.Format("{0}{1}", ExportExcel_MaCot(ChiSoCot), ChiSoHang);
                                caption = xlSheet.get_Range(strCS1, strCS1);
                                caption.Formula = strCongThuc;
                                caption.Locked = true;
                                //caption = xlSheet.Protection
                            }
                        }
                    }
                }
            }

            //object missing = Type.Missing;
            for (j = 0; j < dtCot.Rows.Count; j++)
            {
                strCS1 = "";
                strCS2 = "";

                ChiSoCot = Convert.ToInt32(dtCot.Rows[j]["ChiSo"]);
                ChiSoHang = Convert.ToInt32(dtHang.Rows[0]["ChiSo"]);
                if (ChiSoCot >= 0)
                {
                    strCS1 = String.Format("{0}{1}", ExportExcel_MaCot(ChiSoCot), ChiSoHang);
                }

                ChiSoCot = Convert.ToInt32(dtCot.Rows[j]["ChiSo"]);
                ChiSoHang = Convert.ToInt32(dtHang.Rows[dtHang.Rows.Count - 1]["ChiSo"]);
                if (ChiSoCot >= 0)
                {
                    strCS2 = String.Format("{0}{1}", ExportExcel_MaCot(ChiSoCot), ChiSoHang);
                }

                if (strCS1 != "" && strCS2 != "")
                {
                    caption = xlSheet.get_Range(strCS1, strCS2);
                    caption.Font.Bold = false;
                    caption.Font.Name = "Times New Roman";
                    caption.Font.Size = 12;
                    caption.Select();
                    //caption.NumberFormat = "#,##0";
                }
                //caption.Validation.Add(Microsoft.Office.Interop.Excel.XlDVType.xlValidateCustom, Excel.XlDVAlertStyle.xlValidAlertStop, missing, "=\"\"", missing);
            }

            //Ghi chú của bảng
            strCS1 = String.Format("{0}{1}", ExportExcel_MaCot(1), H0 + dtHang.Rows.Count);
            strCS2 = String.Format("{0}{1}", ExportExcel_MaCot(dtCot.Rows.Count - 1), H0 + dtHang.Rows.Count);
            caption = xlSheet.get_Range(strCS1, strCS2);
            caption.Merge(false);
            caption.Font.Bold = false;
            caption.Font.Name = "Times New Roman";
            caption.Font.Size = 12;
            caption.Select();
            caption.Value2 = dtBang.Rows[0]["sGhiChu"];

            strCS1 = String.Format("{0}{1}", ExportExcel_MaCot(1), H0 + dtHang.Rows.Count + 1);
            strCS2 = String.Format("{0}{1}", ExportExcel_MaCot(dtCot.Rows.Count - 1), H0 + dtHang.Rows.Count + 1);
            caption = xlSheet.get_Range(strCS1, strCS2);
            caption.Merge(false);
            caption.Font.Bold = false;
            caption.Font.Name = "Times New Roman";
            caption.Font.Size = 12;
            caption.Select();
            caption.Value2 = dtBang.Rows[0]["sGhiChuTongHop"];

            //Hàng tiêu đề 
            strCS1 = "B2";
            strCS2 = String.Format("{0}2", ExportExcel_MaCot(C0 + SoCotHienThi - 2));
            caption = xlSheet.get_Range(strCS1, strCS2);
            caption.Merge(false);
            caption.HorizontalAlignment = Excel.Constants.xlCenter;
            caption.Font.Bold = true;
            caption.VerticalAlignment = Excel.Constants.xlCenter;
            caption.Font.Name = "Times New Roman";
            caption.Font.Size = 16;
            caption.Select();
            caption.Value2 = String.Format("{0} [{1}]", dtBang.Rows[0]["sTenBang"], ThoiGian);
            caption.Locked = true;

            //Set protect cho Sheet
            xlSheet.Protect("vicem228", missValue, missValue, missValue, missValue, missValue, missValue, missValue, false, false,
             missValue, false, false, missValue, missValue, missValue);

            //save file
            String FileName = String.Format("{0}{1}.xls", Path, Guid.NewGuid().ToString());
            xlApp.DisplayAlerts = false;
            xlBook.SaveAs(FileName, Excel.XlFileFormat.xlWorkbookNormal, missValue, missValue, missValue, missValue, Excel.XlSaveAsAccessMode.xlExclusive, missValue, missValue, missValue, missValue, missValue);
            xlBook.Close(true, missValue, missValue);
            xlApp.DisplayAlerts = true;
            xlApp.Quit();

            // release cac doi tuong COM
            releaseObject(xlSheet);
            releaseObject(xlBook);
            releaseObject(xlApp);
            xlSheet = null;
            xlBook = null;
            xlApp = null;

            // Kill the right process after export completed
            //KillExcel();
            System.Threading.Thread.Sleep(5000);

            return FileName;
        }


        private static string ExportExcel_LayXauCongThucTheoCot(DataTable dtCot, string CongThuc)
        {
            string csCot = "", csCotMoi, strChiSoCot;
            int j, ChiSoCot;


            CongThuc = BangModels_CongThuc.ChinhLaiCongThuc(CongThuc, dtCot.Rows.Count);
            for (j = 0; j <= dtCot.Rows.Count - 1; j++)
            {
                ChiSoCot = Convert.ToInt32(dtCot.Rows[j]["ChiSo"]);
                strChiSoCot = ExportExcel_MaCot(ChiSoCot);
                csCot = String.Format("[{0}]", j + 1);
                if (CongThuc.IndexOf(csCot) >= 0)
                {
                    csCotMoi = strChiSoCot + "{0}";
                    CongThuc = CongThuc.Replace(csCot, csCotMoi);
                }
            }
            if (CongThuc.StartsWith("=") == false)
            {
                CongThuc = "=" + CongThuc;
            }
            return CongThuc;
        }

        public static string ExportExcel_LayXauCongThucTheoHang(DataTable dtHang, string CongThuc)
        {
            string csHang = "", csHangMoi;
            int i, ChiSoHang;

            CongThuc = BangModels_CongThuc.ChinhLaiCongThuc(CongThuc, dtHang.Rows.Count);
            for (i = 0; i <= dtHang.Rows.Count - 1; i++)
            {
                ChiSoHang = Convert.ToInt32(dtHang.Rows[i]["ChiSo"]);
                csHang = String.Format("[{0}]", dtHang.Rows[i]["iID_MaHangMau"]);
                if (CongThuc.IndexOf(csHang) >= 0)
                {
                    csHangMoi = "{0}" + ChiSoHang;
                    CongThuc = CongThuc.Replace(csHang, csHangMoi);
                }
            }

            while (CongThuc.IndexOf('[') >= 0)
            {
                int cs1 = CongThuc.IndexOf('[');
                int cs2 = CongThuc.IndexOf(']');
                CongThuc = CongThuc.Substring(0, cs1) + "0" + CongThuc.Substring(cs2 + 1);
            }

            if (CongThuc.StartsWith("=") == false)
            {
                CongThuc = "=" + CongThuc;
            }
            return CongThuc;
        }

        static private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                throw new Exception("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        static private void CheckExcellProcesses()
        {
            Hashtable myHashtable;
            Process[] AllProcesses = Process.GetProcessesByName("EXCEL");
            myHashtable = new Hashtable();
            int iCount = 0;

            foreach (Process ExcelProcess in AllProcesses)
            {
                myHashtable.Add(ExcelProcess.Id, iCount);
                iCount = iCount + 1;
            }
        }

        static private void KillExcel(Excel.Application xlApp)
        {
            try
            {
                Hashtable myHashtable = new Hashtable();
                Process[] AllProcesses = Process.GetProcessesByName("EXCEL");

                // check to kill the right process
                foreach (Process ExcelProcess in AllProcesses)
                {
                    if (myHashtable.ContainsKey(ExcelProcess.Id) == false)
                        ExcelProcess.Kill();   //ExcelProcess.CloseMainWindow();
                }
                AllProcesses = null;
            }
            finally
            {
            }
        }

        static private String ExportExcel_MaCot(int ChiSoCot)
        {
            String vR = "";
            int Du;
            Boolean ok = false;

            do
            {
                Du = ChiSoCot % 26;
                ChiSoCot /= 26;
                if (ok)
                {
                    Du = Du - 1;
                }
                vR = Convert.ToChar(Du + 65) + vR;
                ok = true;
            } while (ChiSoCot > 0);
            return vR;
        }

        public static Boolean ImportExcel(String MaBang, String FileName)
        {
            SqlCommand cmd;

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * " +
                              "FROM BC_Bang " +
                              "WHERE iID_MaBang=@iID_MaBang;";
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            DataTable dtBang = Connection.GetDataTable(cmd);
            cmd.Dispose();

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT *, 0 AS ChiSo " +
                              "FROM BC_Bang_Cot " +
                              "WHERE iID_MaBang=@iID_MaBang " +
                              "ORDER BY iSTT;";
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            DataTable dtCot = Connection.GetDataTable(cmd);
            cmd.Dispose();

            DataTable dtHang;
            cmd = new SqlCommand();
            cmd.CommandText = "SELECT *, 0 AS ChiSo " +
                              "FROM BC_Bang_Hang " +
                              "WHERE iID_MaBang=@iID_MaBang " +
                              "ORDER BY iSTT";
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            dtHang = Connection.GetDataTable(cmd);
            cmd.Dispose();

            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            cmd.CommandText = "SELECT * " +
                              " FROM BC_Bang_ChiTiet " +
                              " WHERE (iID_MaBang=@iID_MaBang)" +
                              " ORDER BY iHang,iCot";
            DataTable dtChiTietBang = Connection.GetDataTable(cmd);
            cmd.Dispose();

            Excel.Application appOP = null;
            appOP = new Excel.Application();
            Excel.Workbook workbook = null;
            try
            {
                workbook = appOP.Workbooks.Open(FileName, 2, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, true, 0, true, 1, 0);
            }
            catch
            {
            }
            // Đọc File Excel.
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets.get_Item(1);
            Excel.Range caption;

            int nMaxCol = ((Excel.Range)worksheet.UsedRange).EntireColumn.Count;
            int nMaxRow = ((Excel.Range)worksheet.UsedRange).EntireRow.Count;
            int h, c;
            long MaHang, MaCot;
            object value;
            string strCS;

            for (h = 2; h <= nMaxRow; h++)
            {
                MaHang = -1;

                strCS = String.Format("{0}{1}", ExportExcel_MaCot(0), h);
                caption = worksheet.get_Range(strCS, strCS);
                if (Convert.ToString(caption.Value2) == "-2146826281" || Convert.ToString(caption.Value2) == "NaN" || Convert.ToString(caption.Value2) == "In.fin.ity")
                {
                    value = null;
                }
                else
                {
                    value = caption.Value2;
                }

                if (CommonFunction.IsNumeric(value))
                {
                    MaHang = Convert.ToInt32(value);
                }
                if (MaHang > 0)
                {
                    for (c = 2; c <= nMaxCol; c++)
                    {
                        MaCot = -1;
                        strCS = String.Format("{0}{1}", ExportExcel_MaCot(c - 1), 1);
                        caption = worksheet.get_Range(strCS, strCS);
                        if (Convert.ToString(caption.Value2) == "-2146826281" || Convert.ToString(caption.Value2) == "NaN" || Convert.ToString(caption.Value2) == "In.fin.ity")
                        {
                            value = null;
                        }
                        else
                        {
                            value = caption.Value2;
                        }

                        if (CommonFunction.IsNumeric(value))
                        {
                            MaCot = Convert.ToInt32(value);
                        }
                        if (MaCot > 0)
                        {
                            strCS = String.Format("{0}{1}", ExportExcel_MaCot(c - 1), h);
                            caption = worksheet.get_Range(strCS, strCS);
                            if (Convert.ToString(caption.Value2) == "NaN" || Convert.ToString(caption.Value2) == "In.fin.ity" || (CommonFunction.IsNumeric(caption.Value2) && Convert.ToDouble(caption.Value2) < -2100000000))
                            {
                                value = null;
                            }
                            else
                            {
                                value = caption.Value2;
                            }
                            if (Convert.ToString(value) == "NaN")
                            {
                                value = null;
                            }
                            if (Convert.ToString(value) == "In.fin.ity")
                            {
                                value = null;
                            }

                            ImportExcel_LuuGiaTri(dtBang, dtHang, dtCot, dtChiTietBang, MaHang, MaCot, value);

                        }
                    }
                }
            }

            if (workbook != null)
            {
                // Đóng chế độ Read
                workbook.Close(false, false, Type.Missing);
                workbook = null;
            }
            dtBang.Dispose();
            dtChiTietBang.Dispose();
            dtCot.Dispose();
            dtHang.Dispose();
            return true;
        }

        private static Boolean ImportExcel_LuuGiaTri(DataTable dtBang, DataTable dtHang, DataTable dtCot, DataTable dtChiTietBang, long MaHang, long MaCot, object value)
        {
            String MaBang = Convert.ToString(dtBang.Rows[0]["iID_MaBang"]);
            String MaBangMau = Convert.ToString(dtBang.Rows[0]["iID_MaBangMau"]);
            int i, j, k;
            string tg, MaHangMau, MaDonVi, MaHangMau_Cha, MaCotMau, MaChiTiet;
            SqlCommand cmd;
            String sValue = "";
            if (value != null)
            {
                if (CommonFunction.IsNumeric(value))
                {
                    sValue = Convert.ToDouble(value).ToString("0.##");
                }
                else
                {
                    sValue = Convert.ToString(value);
                }
            }

            for (i = 0; i <= dtHang.Rows.Count - 1; i++)
            {
                if (MaHang == Convert.ToInt32(dtHang.Rows[i]["iID_MaHang"]))
                {
                    MaHangMau = String.Format("{0}", dtHang.Rows[i]["iID_MaHangMau"]);
                    MaHangMau_Cha = String.Format("{0}", dtHang.Rows[i]["iID_MaHangMau_Cha"]);
                    MaDonVi = String.Format("{0}", dtHang.Rows[i]["iID_MaDonVi"]);
                    for (j = 0; j <= dtCot.Rows.Count - 1; j++)
                    {
                        if (MaCot == Convert.ToInt32(dtCot.Rows[j]["iID_MaCot"]))
                        {
                            MaCotMau = String.Format("{0}", dtCot.Rows[j]["iID_MaCotMau"]);
                            tg = String.Format("{0}_{1}", MaHang, MaCot);
                            MaChiTiet = "";
                            for (k = 0; k <= dtChiTietBang.Rows.Count - 1; k++)
                            {
                                if (Convert.ToInt32(dtChiTietBang.Rows[k]["iID_MaHang"]) == MaHang && Convert.ToInt32(dtChiTietBang.Rows[k]["iID_MaCot"]) == MaCot)
                                {
                                    MaChiTiet = Convert.ToString(dtChiTietBang.Rows[k]["iID_ChiTietBang"]);
                                    break;
                                }
                            }
                            if (MaChiTiet != "")
                            {
                                cmd = new SqlCommand();
                                cmd.CommandText = "UPDATE BC_Bang_ChiTiet SET oGiaTri=@oGiaTri WHERE iID_MaHang=@iID_MaHang AND iID_MaCot=@iID_MaCot";
                                cmd.Parameters.AddWithValue("@oGiaTri", sValue);
                                cmd.Parameters.AddWithValue("@iID_MaHang", MaHang);
                                cmd.Parameters.AddWithValue("@iID_MaCot", MaCot);
                                Connection.UpdateDatabase(cmd);
                                cmd.Dispose();
                            }
                            else
                            {
                                cmd = new SqlCommand();
                                cmd.CommandText = "INSERT INTO BC_Bang_ChiTiet(iID_MaBang ,iID_MaBangMau ,iID_MaHang ,iID_MaHangMau_Cha ,iID_MaHangMau ,iID_MaCot ,iID_MaCotMau ,iID_MaDonVi ,dNgayBaoCao ,oGiaTri ,iHang ,iCot) " +
                                                                        "VALUES(@iID_MaBang,@iID_MaBangMau,@iID_MaHang,@iID_MaHangMau_Cha,@iID_MaHangMau,@iID_MaCot,@iID_MaCotMau,@iID_MaDonVi,@dNgayBaoCao,@oGiaTri,@iHang,@iCot)";
                                cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
                                cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                                cmd.Parameters.AddWithValue("@iID_MaHang", MaHang);
                                cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMau_Cha);
                                cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
                                cmd.Parameters.AddWithValue("@iID_MaCot", MaCot);
                                cmd.Parameters.AddWithValue("@iID_MaCotMau", MaCotMau);
                                cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                                cmd.Parameters.AddWithValue("@dNgayBaoCao", dtBang.Rows[0]["dNgayBaoCao"]);
                                cmd.Parameters.AddWithValue("@oGiaTri", sValue);
                                cmd.Parameters.AddWithValue("@iHang", i + 1);
                                cmd.Parameters.AddWithValue("@iCot", j + 1);
                                Connection.UpdateDatabase(cmd);
                                cmd.Dispose();
                            }
                            break;
                        }
                    }
                }
            }

            return true;
        }


        public static void GhiLaiBang(String MaBang)
        {
            Boolean ChiCoQuyenDoc = false;
            SqlCommand cmd;
            cmd = new SqlCommand("SELECT * FROM BC_Bang WHERE iID_MaBang=@iID_MaBang");
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            DataTable dtBang = Connection.GetDataTable(cmd);
            cmd.Dispose();
            String TenBang = Convert.ToString(dtBang.Rows[0]["sTenBang"]);
            String MaBangMau = Convert.ToString(dtBang.Rows[0]["iID_MaBangMau"]);
            String MaBangMau_LayGhiChu = Convert.ToString(dtBang.Rows[0]["iID_MaBangMau_LayGhiChu"]);
            if (Convert.ToBoolean(dtBang.Rows[0]["bDaChot"]))
            {
                ChiCoQuyenDoc = true;
            }

            Boolean PhanTrang = false;
            if (DBNull.Value != dtBang.Rows[0]["bPhanTrang"])
            {
                PhanTrang = Convert.ToBoolean(dtBang.Rows[0]["bPhanTrang"]);
            }

            Boolean XemTheoDonVi = Convert.ToBoolean(dtBang.Rows[0]["bXemTheoDonVi"]);
            String MaDonVi = Convert.ToString(dtBang.Rows[0]["iID_MaDonVi"]);
            DateTime Ngay = Convert.ToDateTime(dtBang.Rows[0]["dNgayBaoCao"]);
            int LoaiBaoCao = Convert.ToInt16(dtBang.Rows[0]["iLoaiBaoCao"]);
            String ThoiGian = "";
            switch (LoaiBaoCao)
            {
                case 1:
                    ThoiGian = Ngay.ToString("dd/MM/yyyy");
                    break;
                case 2:
                    ThoiGian = Ngay.ToString("MM/yyyy");
                    break;
                case 3:
                    ThoiGian = Ngay.ToString("MM/yyyy");
                    break;
                case 4:
                    ThoiGian = Ngay.ToString("yyyy");
                    break;
                case 5:
                    ThoiGian = Ngay.ToString("MM/yyyy");
                    break;
            }


            cmd = new SqlCommand();
            if (XemTheoDonVi)
            {
                cmd.CommandText = "SELECT * " +
                                  " FROM BC_DonVi " +
                                  " WHERE (iID_MaDonVi = @iID_MaDonVi)" +
                                  " ORDER BY iSTT;";
                cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
            }
            else
            {
                cmd.CommandText = "SELECT * " +
                                  " FROM BC_DonVi " +
                                  " ORDER BY iSTT;";
            }
            DataTable dtDonVi = Connection.GetDataTable(cmd);
            cmd.Dispose();


            cmd = new SqlCommand("SELECT * FROM BC_Bang_CongThuc WHERE iID_MaBang=@iID_MaBang");
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            DataTable dtCongThuc = Connection.GetDataTable(cmd);
            cmd.Dispose();

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * " +
                              "FROM BC_Bang_Cot " +
                              "WHERE iID_MaBang=@iID_MaBang ORDER BY iSTT;";
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            DataTable dtCot = Connection.GetDataTable(cmd);
            cmd.Dispose();

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * " +
                              "FROM BC_BangMau_CotMau_DonVi " +
                              "WHERE iID_MaBangMau=@iID_MaBangMau;";
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            DataTable dtCotMauDonVi = Connection.GetDataTable(cmd);
            cmd.Dispose();

            DataTable dtHang;
            cmd = new SqlCommand("SELECT * FROM BC_Bang_Hang WHERE iID_MaBang=@iID_MaBang AND iMaTrangThai=1 ORDER BY iSTT");
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            dtHang = Connection.GetDataTable(cmd);
            cmd.Dispose();

            int i, j;
            //Xác định các cột hiển thị hay không?
            List<Boolean> arrCotHienThi = new List<Boolean>();
            arrCotHienThi.Add(true);
            for (i = 0; i <= dtCot.Rows.Count - 1; i++)
            {
                arrCotHienThi.Add(Convert.ToBoolean(dtCot.Rows[i]["bVisible"]));
                if (arrCotHienThi[i + 1] && Convert.ToBoolean(dtCot.Rows[i]["bThuocDonVi"]))
                {
                    for (j = 0; j < dtCotMauDonVi.Rows.Count; j++)
                    {
                        if (Convert.ToString(dtCot.Rows[i]["iID_MaCotMau"]) == Convert.ToString(dtCotMauDonVi.Rows[j]["iID_MaCotMau"]) &&
                            Convert.ToString(dtCot.Rows[i]["iID_MaDonVi"]) == Convert.ToString(dtCotMauDonVi.Rows[j]["iID_MaDonVi"]))
                        {
                            arrCotHienThi[i + 1] = false;
                            break;
                        }
                    }
                }
            }

            List<List<object>> arrGiaTriChiTietCu = null, arrGiaTriChiTiet = null;
            String strXauMaGiaTriKhac = "", strXauGiaTriKhac = "";
            LayThongTinCuaBang(ChiCoQuyenDoc, dtBang, dtHang, dtCot, dtDonVi, ref dtCongThuc, ref arrGiaTriChiTiet, ref arrGiaTriChiTietCu, ref arrCotHienThi, ref strXauMaGiaTriKhac, ref strXauGiaTriKhac);

        }

    }
}
