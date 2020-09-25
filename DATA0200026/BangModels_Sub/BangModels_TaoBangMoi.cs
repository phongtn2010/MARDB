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

namespace DATA0200026
{
    public class BangModels_TaoBangMoi
    {
        public static int TaoCacCotTheoDonVi(String MaBang, DataTable dtCotMau, String MaDonVi, int ChiSo, ref int ThuTu, DataTable dtBang, String sUserName, String sIP)
        {
            int d = 1;
            int LoaiBaoCao = Convert.ToInt32(dtBang.Rows[0]["iLoaiBaoCao"]);
            DateTime ThoiGian = Convert.ToDateTime(dtBang.Rows[0]["dNgayBaoCao"]);

            int i;
            while (ChiSo < dtCotMau.Rows.Count && Convert.ToBoolean(dtCotMau.Rows[ChiSo]["bThuocDonVi"]))
            {
                Bang bang;
                d = 1;
                if (Convert.ToBoolean(dtCotMau.Rows[ChiSo]["bChiTietThoiGian"]))
                {
                    switch (LoaiBaoCao)
                    {
                        case 2:
                            d = 0;
                            DateTime a = new DateTime(ThoiGian.Year, ThoiGian.Month, 1);
                            while (a.Month == ThoiGian.Month)
                            {
                                a = a.AddDays(1);
                                d = d + 1;
                            }
                            break;

                        case 3:
                            d = 3;
                            break;

                        case 4:
                            d = 12;
                            break;

                        case 5:
                            d = 6;
                            break;

                    }
                }
                for (i = 1; i <= d; i++)
                {
                    bang = new Bang("BC_Bang_Cot");
                    bang.MaNguoiDungSua = sUserName;
                    bang.IPSua = sIP;
                    bang.DuLieuMoi = true;
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaBang", MaBang);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaCotMau", dtCotMau.Rows[ChiSo]["iID_MaCotMau"]);
                    bang.CmdParams.Parameters.AddWithValue("@bThuocDonVi", true);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                    bang.CmdParams.Parameters.AddWithValue("@iKieuDuLieu", dtCotMau.Rows[ChiSo]["iKieuDuLieu"]);
                    bang.CmdParams.Parameters.AddWithValue("@iSoSauDauPhay", dtCotMau.Rows[ChiSo]["iSoSauDauPhay"]);
                    bang.CmdParams.Parameters.AddWithValue("@sLoaiNguoiDuocSua", dtCotMau.Rows[ChiSo]["sLoaiNguoiDuocSua"]);
                    bang.CmdParams.Parameters.AddWithValue("@sWidth", dtCotMau.Rows[ChiSo]["sWidth"]);
                    bang.CmdParams.Parameters.AddWithValue("@bVisible", dtCotMau.Rows[ChiSo]["bVisible"]);
                    bang.CmdParams.Parameters.AddWithValue("@iSTT", ThuTu);
                    bang.CmdParams.Parameters.AddWithValue("@sCongThuc", dtCotMau.Rows[ChiSo]["sCongThuc"]);
                    bang.CmdParams.Parameters.AddWithValue("@bLayDuLieuTuBangKhac", dtCotMau.Rows[ChiSo]["bLayDuLieuTuBangKhac"]);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaBangMau_DuLieu", dtCotMau.Rows[ChiSo]["iID_MaBangMau_DuLieu"]);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaCotMau_DuLieu", dtCotMau.Rows[ChiSo]["iID_MaCotMau_DuLieu"]);
                    bang.CmdParams.Parameters.AddWithValue("@bKhongApDungCongThucHang", dtCotMau.Rows[ChiSo]["bKhongApDungCongThucHang"]);
                    bang.CmdParams.Parameters.AddWithValue("@bChiTietThoiGian", dtCotMau.Rows[ChiSo]["bChiTietThoiGian"]);
                    if (Convert.ToBoolean(dtCotMau.Rows[ChiSo]["bChiTietThoiGian"]))
                    {
                        bang.CmdParams.Parameters.AddWithValue("@iKhoangCachThoiGian", i - 1);
                        bang.CmdParams.Parameters.AddWithValue("@iLoaiLuyKe", -1);
                        bang.CmdParams.Parameters.AddWithValue("@bNhomCon", 1);
                        switch (LoaiBaoCao)
                        {
                            case 2:
                                bang.CmdParams.Parameters.AddWithValue("@sTenNhomCon", "Danh sách ngày trong tháng");
                                bang.CmdParams.Parameters.AddWithValue("@sTenCot", "Ngày " + Convert.ToString(Convert.ToInt32(ThoiGian.Month) - 1 + i));
                                break;

                            case 3:
                                bang.CmdParams.Parameters.AddWithValue("@sTenNhomCon", "Danh sách tháng trong quý");
                                bang.CmdParams.Parameters.AddWithValue("@sTenCot", "Tháng " + Convert.ToString(Convert.ToInt32(ThoiGian.Month) - 1 + i));
                                break;

                            case 4:
                                bang.CmdParams.Parameters.AddWithValue("@sTenNhomCon", "Danh sách tháng trong năm");
                                bang.CmdParams.Parameters.AddWithValue("@sTenCot", "Tháng " + Convert.ToString(Convert.ToInt32(ThoiGian.Month) - 1 + i));
                                break;

                            case 5:
                                bang.CmdParams.Parameters.AddWithValue("@sTenNhomCon", "Danh sách tháng trong 6 tháng");
                                bang.CmdParams.Parameters.AddWithValue("@sTenCot", "Tháng " + Convert.ToString(Convert.ToInt32(ThoiGian.Month) - 1 + i));
                                break;

                        }
                    }
                    else
                    {
                        bang.CmdParams.Parameters.AddWithValue("@iKhoangCachThoiGian", dtCotMau.Rows[ChiSo]["iKhoangCachThoiGian"]);
                        bang.CmdParams.Parameters.AddWithValue("@iLoaiLuyKe", dtCotMau.Rows[ChiSo]["iLoaiLuyKe"]);
                        bang.CmdParams.Parameters.AddWithValue("@bNhomCon", dtCotMau.Rows[ChiSo]["bNhomCon"]);
                        bang.CmdParams.Parameters.AddWithValue("@sTenNhomCon", dtCotMau.Rows[ChiSo]["sTenNhomCon"]);
                        bang.CmdParams.Parameters.AddWithValue("@sTenCot", dtCotMau.Rows[ChiSo]["sTenCot"]);
                    }
                    bang.Save(false);
                    ThuTu++;
                }
                ChiSo++;
            }
            return ChiSo;
        }

        public static int TaoCacCotKhongTheoDonVi(String MaBang, DataTable dtCotMau, int ChiSo, ref int ThuTu, DataTable dtDonVi, DataTable dtBang, String sUserName, String sIP)
        {
            Bang bang;
            int i, d = 1;
            int LoaiBaoCao = Convert.ToInt32(dtBang.Rows[0]["iLoaiBaoCao"]);
            DateTime ThoiGian = Convert.ToDateTime(dtBang.Rows[0]["dNgayBaoCao"]);
            if (Convert.ToBoolean(dtCotMau.Rows[ChiSo]["bChiTietThoiGian"]))
            {
                switch (LoaiBaoCao)
                {
                    case 2:
                        d = 0;
                        DateTime a = new DateTime(ThoiGian.Year, ThoiGian.Month, 1);
                        while (a.Month == ThoiGian.Month)
                        {
                            a = a.AddDays(1);
                            d = d + 1;
                        }
                        break;

                    case 3:
                        d = 3;
                        break;

                    case 4:
                        d = 12;
                        break;

                    case 5:
                        d = 6;
                        break;

                }
            }
            for (i = 1; i <= d; i++)
            {
                bang = new Bang("BC_Bang_Cot");
                bang.MaNguoiDungSua = sUserName;
                bang.IPSua = sIP;
                bang.DuLieuMoi = true;
                bang.CmdParams.Parameters.AddWithValue("@iID_MaBang", MaBang);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaCotMau", dtCotMau.Rows[ChiSo]["iID_MaCotMau"]);
                if (Convert.ToInt32(dtCotMau.Rows[ChiSo]["iID_MaDonVi"]) > 0)
                {
                    bang.CmdParams.Parameters.AddWithValue("@bThuocDonVi", true);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaDonVi", dtCotMau.Rows[ChiSo]["iID_MaDonVi"]);
                }
                else
                {
                    bang.CmdParams.Parameters.AddWithValue("@bThuocDonVi", false);
                }
                bang.CmdParams.Parameters.AddWithValue("@iKieuDuLieu", dtCotMau.Rows[ChiSo]["iKieuDuLieu"]);
                bang.CmdParams.Parameters.AddWithValue("@iSoSauDauPhay", dtCotMau.Rows[ChiSo]["iSoSauDauPhay"]);
                bang.CmdParams.Parameters.AddWithValue("@sLoaiNguoiDuocSua", dtCotMau.Rows[ChiSo]["sLoaiNguoiDuocSua"]);
                bang.CmdParams.Parameters.AddWithValue("@iSTT", ThuTu);
                bang.CmdParams.Parameters.AddWithValue("@sCongThuc", dtCotMau.Rows[ChiSo]["sCongThuc"]);
                bang.CmdParams.Parameters.AddWithValue("@sWidth", dtCotMau.Rows[ChiSo]["sWidth"]);
                bang.CmdParams.Parameters.AddWithValue("@bVisible", dtCotMau.Rows[ChiSo]["bVisible"]);
                bang.CmdParams.Parameters.AddWithValue("@bLayDuLieuTuBangKhac", dtCotMau.Rows[ChiSo]["bLayDuLieuTuBangKhac"]);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaBangMau_DuLieu", dtCotMau.Rows[ChiSo]["iID_MaBangMau_DuLieu"]);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaCotMau_DuLieu", dtCotMau.Rows[ChiSo]["iID_MaCotMau_DuLieu"]);
                bang.CmdParams.Parameters.AddWithValue("@bKhongApDungCongThucHang", dtCotMau.Rows[ChiSo]["bKhongApDungCongThucHang"]);
                bang.CmdParams.Parameters.AddWithValue("@bChiTietThoiGian", dtCotMau.Rows[ChiSo]["bChiTietThoiGian"]);
                if (Convert.ToBoolean(dtCotMau.Rows[ChiSo]["bChiTietThoiGian"]))
                {
                    bang.CmdParams.Parameters.AddWithValue("@iKhoangCachThoiGian", i - 1);
                    bang.CmdParams.Parameters.AddWithValue("@iLoaiLuyKe", -1);
                    bang.CmdParams.Parameters.AddWithValue("@bNhomCon", 1);

                    switch (LoaiBaoCao)
                    {
                        case 2:
                            bang.CmdParams.Parameters.AddWithValue("@sTenNhomCon", "Danh sách ngày trong tháng");
                            bang.CmdParams.Parameters.AddWithValue("@sTenCot", "Ngày " + Convert.ToString(Convert.ToInt32(ThoiGian.Month) - 1 + i));
                            break;

                        case 3:
                            bang.CmdParams.Parameters.AddWithValue("@sTenNhomCon", "Danh sách tháng trong quý");
                            bang.CmdParams.Parameters.AddWithValue("@sTenCot", "Tháng " + Convert.ToString(Convert.ToInt32(ThoiGian.Month) - 1 + i));
                            break;

                        case 4:
                            bang.CmdParams.Parameters.AddWithValue("@sTenNhomCon", "Danh sách tháng trong năm");
                            bang.CmdParams.Parameters.AddWithValue("@sTenCot", "Tháng " + Convert.ToString(Convert.ToInt32(ThoiGian.Month) - 1 + i));
                            break;

                        case 5:
                            bang.CmdParams.Parameters.AddWithValue("@sTenNhomCon", "Danh sách tháng trong quý");
                            bang.CmdParams.Parameters.AddWithValue("@sTenCot", "Tháng " + Convert.ToString(Convert.ToInt32(ThoiGian.Month) - 1 + i));
                            break;

                    }
                }
                else
                {
                    bang.CmdParams.Parameters.AddWithValue("@iKhoangCachThoiGian", dtCotMau.Rows[ChiSo]["iKhoangCachThoiGian"]);
                    bang.CmdParams.Parameters.AddWithValue("@iLoaiLuyKe", dtCotMau.Rows[ChiSo]["iLoaiLuyKe"]);
                    bang.CmdParams.Parameters.AddWithValue("@sTenCot", dtCotMau.Rows[ChiSo]["sTenCot"]);
                    bang.CmdParams.Parameters.AddWithValue("@bNhomCon", dtCotMau.Rows[ChiSo]["bNhomCon"]);
                    bang.CmdParams.Parameters.AddWithValue("@sTenNhomCon", dtCotMau.Rows[ChiSo]["sTenNhomCon"]);
                }
                bang.Save(false);
                ThuTu++;
            }
            ChiSo++;
            return ChiSo;
        }

        public static void ChenChiTieuConMoi(String ParentID, String MaHangMauCha, String MaBang, String MaBangMau, String MaDonVi, String MaBangMauHangMau, String strPath, DateTime NgayBaoCao, String sUserName, String sIP)
        {
            String MaBangMauHangMau_List = "," + MaBangMauHangMau;
            int i, j, k, l;
            if (MaBangMauHangMau_List != "")
            {
                String[] arr = MaBangMauHangMau_List.Split(',');
                for (i = 0; i < arr.Length - 1; i++)
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
                        for (j = 0; j <= dtBangMau.Rows.Count - 1; j++)
                        {
                            if (arr[i] == Convert.ToString(dtBangMau.Rows[j]["iID_MaBangMau_HangMau"]))
                            {
                                tg = 0;
                                break;
                            }
                        }
                        if (tg == 1)
                        {
                            int iSTT,iSTTBMDV;
                            DataTable dt;
                            SqlCommand cmd;
                            cmd = new SqlCommand("SELECT * FROM BC_BangMau_HangMau WHERE iID_MaBangMau_HangMau=@iID_MaBangMau_HangMau");
                            cmd.Parameters.AddWithValue("@iID_MaBangMau_HangMau", arr[i].ToString());
                            dt = Connection.GetDataTable(cmd);
                            cmd.Dispose();
                            if (dt.Rows.Count > 0)
                            {
                                int MaHangMau = Convert.ToInt32(dt.Rows[0]["iID_MaHangMau"]);
                                int MaBangMau_HangMau = Convert.ToInt32(dt.Rows[0]["iID_MaBangMau_HangMau"]);

                                //Xử lý số thứ tự của bảng BC_BangMau_HangMau_DonVi trước khi chèn
                                String strSQL;
                                if (String.IsNullOrEmpty(MaDonVi) == false)
                                {
                                    strSQL = "SELECT MAX(B.iSTT) FROM BC_BangMau_HangMau AS A inner join BC_BangMau_HangMau_DonVi AS B ON A.iID_MaBangMau_HangMau = B.iID_MaBangMau_HangMau " +
                                                "WHERE A.iID_MaHangMau_Cha = @iID_MaHangMau_Cha  AND B.iID_MaBangMau = @iID_MaBangMau AND B.iID_MaDonVi = @iID_MaDonVi  ";
                                    cmd = new SqlCommand(strSQL);
                                    cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMauCha);
                                    cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                                    cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);                                    
                                }
                                else
                                {
                                    strSQL = "SELECT MAX(B.iSTT) FROM BC_BangMau_HangMau AS A inner join BC_BangMau_HangMau_DonVi AS B ON A.iID_MaBangMau_HangMau = B.iID_MaBangMau_HangMau " +
                                                "WHERE A.iID_MaHangMau_Cha = @iID_MaHangMau_Cha  AND B.iID_MaBangMau = @iID_MaBangMau AND B.iID_MaDonVi  is null ";
                                    cmd = new SqlCommand(strSQL);
                                    cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMauCha);
                                    cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                                }

                                iSTTBMDV = Convert.ToInt32(Connection.GetValue(cmd, 0));
                                cmd.Dispose();

                                if (iSTTBMDV == 0) {
                                    if (String.IsNullOrEmpty(MaDonVi) == false)
                                    {
                                        strSQL = "SELECT B.iSTT FROM BC_BangMau_HangMau AS A inner join BC_BangMau_HangMau_DonVi AS B ON A.iID_MaBangMau_HangMau = B.iID_MaBangMau_HangMau " +
                                                    "WHERE A.iID_MaHangMau = @iID_MaHangMau  AND B.iID_MaBangMau = @iID_MaBangMau AND B.iID_MaDonVi = @iID_MaDonVi  ";
                                        cmd = new SqlCommand(strSQL);
                                        cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMauCha);
                                        cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                                        cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                                    }
                                    else
                                    {
                                        strSQL = "SELECT B.iSTT FROM BC_BangMau_HangMau AS A inner join BC_BangMau_HangMau_DonVi AS B ON A.iID_MaBangMau_HangMau = B.iID_MaBangMau_HangMau " +
                                                    "WHERE A.iID_MaHangMau = @iID_MaHangMau  AND B.iID_MaBangMau = @iID_MaBangMau AND B.iID_MaDonVi is null ";
                                        cmd = new SqlCommand(strSQL);
                                        cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMauCha);
                                        cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                                    }

                                    iSTTBMDV = Convert.ToInt32(Connection.GetValue(cmd, 0));
                                    cmd.Dispose();
                                }

                                //Tăng các số thứ tự trước iSTTBMDV của bảng BC_BangMau_HangMau_DonVi 
                                cmd = new SqlCommand();
                                if (String.IsNullOrEmpty(MaDonVi) == false)
                                {
                                    cmd.CommandText = "UPDATE BC_BangMau_HangMau_DonVi SET iSTT=iSTT+1 " +
                                            "WHERE iSTT>@iSTT AND iID_MaBangMau=@iID_MaBangMau AND iID_MaDonVi=@iID_MaDonVi";
                                    cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                                    cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                                }
                                else {
                                    cmd.CommandText = "UPDATE BC_BangMau_HangMau_DonVi SET iSTT=iSTT+1 " +
                                                "WHERE iSTT>@iSTT AND iID_MaBangMau=@iID_MaBangMau AND iID_MaDonVi is null";
                                    cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                                }
                                cmd.Parameters.AddWithValue("@iSTT", iSTTBMDV);
                                Connection.UpdateDatabase(cmd);
                                cmd.Dispose();

                                //Thêm hàng mẫu mới vào Bảng Mẫu Hàng Mẫu cho đơn vị
                                Bang bang = new Bang("BC_BangMau_HangMau_DonVi");
                                bang.MaNguoiDungSua = sUserName;
                                bang.IPSua = sIP;
                                bang.CmdParams.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                                bang.CmdParams.Parameters.AddWithValue("@iID_MaBangMau_HangMau", MaBangMau_HangMau);
                                bang.CmdParams.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                                bang.CmdParams.Parameters.AddWithValue("@iSTT", iSTTBMDV + 1);
                                bang.Save();

                                cmd = new SqlCommand("SELECT MAX(iSTT) FROM BC_Bang_Hang WHERE iID_MaBang=@iID_MaBang AND iID_MaHangMau_Cha=@iID_MaHangMau_Cha");
                                cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
                                cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMauCha);
                                iSTT = Convert.ToInt32(Connection.GetValue(cmd, 0));
                                cmd.Dispose();

                                if (iSTT == 0)
                                {
                                    cmd = new SqlCommand("SELECT iSTT FROM BC_Bang_Hang WHERE iID_MaBang=@iID_MaBang AND iID_MaHangMau=@iID_MaHangMau");
                                    cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
                                    cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMauCha);
                                    iSTT = Convert.ToInt32(Connection.GetValue(cmd, 0));
                                    cmd.Dispose();
                                }

                                cmd = new SqlCommand();
                                cmd.CommandText = "UPDATE BC_Bang_Hang SET iSTT=iSTT+1 " +
                                            "WHERE iSTT>@iSTT AND iID_MaBang=@iID_MaBang";
                                cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
                                cmd.Parameters.AddWithValue("@iSTT", iSTT);
                                Connection.UpdateDatabase(cmd);
                                cmd.Dispose();

                                //Lay thong tin chi tieu theo ma hang mau
                                cmd = new SqlCommand("SELECT * FROM BC_HangMau WHERE iID_MaHangMau=@iID_MaHangMau");
                                cmd.Parameters.AddWithValue("@iID_MaHangMau", dt.Rows[0]["iID_MaHangMau"]);
                                DataTable dtHangMau = Connection.GetDataTable(cmd);
                                cmd.Dispose();

                                //Thêm hàng mới vào bảng
                                cmd = new SqlCommand();
                                cmd.CommandText = "INSERT INTO BC_Bang_Hang(iID_MaBang,iID_MaHangMau_Cha,iID_MaHangMau,iID_MaDonVi,sTenHang,sCongThuc, iHeight,sBackGround, sColor, iFontSize, bBold, bItalic, bUnderline, bTongHangCon, bTinhTongTheoDonVi, bTruongGhiChu, bThemChiTieuCon, iSTT) " +
                                            "VALUES(@iID_MaBang,@iID_MaHangMau_Cha, @iID_MaHangMau, @iID_MaDonVi,@sTenHang, @sCongThuc, @iHeight,@sBackGround, @sColor, @iFontSize, @bBold, @bItalic, @bUnderline, @bTongHangCon, @bTinhTongTheoDonVi, @bTruongGhiChu, @bThemChiTieuCon, @iSTT)";
                                cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
                                cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMauCha);
                                cmd.Parameters.AddWithValue("@iID_MaHangMau", dt.Rows[0]["iID_MaHangMau"]);
                                cmd.Parameters.AddWithValue("@iID_MaDonVi", dtHangMau.Rows[0]["iID_MaDonVi"]);
                                cmd.Parameters.AddWithValue("@sTenHang", dtHangMau.Rows[0]["sTenHang"]);
                                cmd.Parameters.AddWithValue("@sCongThuc", dtHangMau.Rows[0]["sCongThuc"]);
                                cmd.Parameters.AddWithValue("@iHeight", dtHangMau.Rows[0]["iHeight"]);
                                cmd.Parameters.AddWithValue("@sBackGround", dtHangMau.Rows[0]["sBackGround"]);
                                cmd.Parameters.AddWithValue("@sColor", dtHangMau.Rows[0]["sColor"]);
                                cmd.Parameters.AddWithValue("@iFontSize", dtHangMau.Rows[0]["iFontSize"]);
                                cmd.Parameters.AddWithValue("@bBold", dtHangMau.Rows[0]["bBold"]);
                                cmd.Parameters.AddWithValue("@bItalic", dtHangMau.Rows[0]["bItalic"]);
                                cmd.Parameters.AddWithValue("@bUnderline", dtHangMau.Rows[0]["bUnderline"]);
                                cmd.Parameters.AddWithValue("@bTongHangCon", dtHangMau.Rows[0]["bTongHangCon"]);
                                cmd.Parameters.AddWithValue("@bTinhTongTheoDonVi", dtHangMau.Rows[0]["bTinhTongTheoDonVi"]);
                                cmd.Parameters.AddWithValue("@bTruongGhiChu", dtHangMau.Rows[0]["bTruongGhiChu"]);
                                cmd.Parameters.AddWithValue("@bThemChiTieuCon", dtHangMau.Rows[0]["bThemChiTieuCon"]);
                                cmd.Parameters.AddWithValue("@iSTT", iSTT + 1);
                                Connection.UpdateDatabase(cmd);
                                cmd.Dispose();


                                dt.Dispose();
                                dt.Clear();

                                //Xóa bảng công thức
                                cmd = new SqlCommand("DELETE FROM BC_Bang_CongThuc WHERE iID_MaBang=@iID_MaBang");
                                cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
                                Connection.UpdateDatabase(cmd);
                                cmd.Dispose();

                                BangModels_CongThuc.TaoCongThucChoBangMoi(MaBangMau, MaBang, sUserName, sIP);

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

                                //Thêm chỉ tiêu đó vào các bảng mẫu hàng mẫu khác
                                String tgMaBangMau = MaBangMau;
                                String MaBangMauKhac;
                                List<String> lstMaBangMauCungNhau = new List<string>();
                                cmd = new SqlCommand("SELECT iID_MaBangCungNhau FROM BC_BangMau WHERE iID_MaBangMau = @iID_MaBangMau");
                                cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                                String strMaBangMauCungNhau = Convert.ToString(Connection.GetValue(cmd, ""));
                                cmd.Dispose();
                                if (strMaBangMauCungNhau != null && strMaBangMauCungNhau != "")
                                {
                                    String[] arrChuoiBangCungNhau = strMaBangMauCungNhau.Split(';');
                                    for (i = 0; i < arrChuoiBangCungNhau.Length; i++)
                                    {
                                        if (arrChuoiBangCungNhau[i] != "" && arrChuoiBangCungNhau[i] != MaBangMau)
                                        {
                                            lstMaBangMauCungNhau.Add(arrChuoiBangCungNhau[i]);
                                        }
                                    }
                                    for (k = 0; k < lstMaBangMauCungNhau.Count; k++)
                                    {
                                        MaBangMauKhac = Convert.ToString(lstMaBangMauCungNhau[k]);
                                        cmd = new SqlCommand("SELECT bXemTheoDonVi FROM BC_BangMau WHERE iID_MaBangMau = @iID_MaBangMau");
                                        cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMauKhac);
                                        Boolean bXemTheoDonVi = Convert.ToBoolean(Connection.GetValue(cmd, 0));
                                        cmd.Dispose();
                                        if (bXemTheoDonVi == true)
                                        {
                                            cmd = new SqlCommand("SELECT iID_MaBangMau_HangMau FROM BC_BangMau_HangMau " +
                                                                "WHERE iID_MaBangMau = @iID_MaBangMau AND iID_MaHangMau = @iID_MaHangMau AND iID_MaHangMau_Cha=@iID_MaHangMau_Cha");
                                            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMauKhac);
                                            cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
                                            cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMauCha);
                                            String MaBangMauHangMauKhac = Convert.ToString(Connection.GetValue(cmd, ""));
                                            cmd.Dispose();

                                            int tgkhac = 1;
                                            if (String.IsNullOrEmpty(MaDonVi) == false)
                                            {
                                                SQL = String.Format("SELECT iID_MaBangMau_HangMau FROM BC_BangMau_HangMau_DonVi WHERE iID_MaBangMau = {0} AND iID_MaDonVi = {1}", "'" + MaBangMauKhac + "'", "'" + MaDonVi + "'");
                                            }
                                            else
                                            {
                                                SQL = String.Format("SELECT iID_MaBangMau_HangMau FROM BC_BangMau_HangMau_DonVi WHERE iID_MaBangMau = {0} AND iID_MaDonVi is null", "'" + MaBangMauKhac + "'");
                                            }

                                            DataTable dtBangMauKhac = Connection.GetDataTable(SQL);
                                            for (l = 0; l <= dtBangMauKhac.Rows.Count - 1; l++)
                                            {
                                                if (MaBangMauHangMauKhac == Convert.ToString(dtBangMauKhac.Rows[l]["iID_MaBangMau_HangMau"]))
                                                {
                                                    tgkhac = 0;
                                                    break;
                                                }
                                            }
                                            if (tgkhac == 1)
                                            {
                                                strSQL = "";
                                                iSTTBMDV = 0;
                                                if (String.IsNullOrEmpty(MaDonVi) == false)
                                                {
                                                    strSQL = "SELECT MAX(B.iSTT) FROM BC_BangMau_HangMau AS A inner join BC_BangMau_HangMau_DonVi AS B ON A.iID_MaBangMau_HangMau = B.iID_MaBangMau_HangMau " +
                                                                "WHERE A.iID_MaHangMau_Cha = @iID_MaHangMau_Cha  AND B.iID_MaBangMau = @iID_MaBangMau AND B.iID_MaDonVi = @iID_MaDonVi  ";
                                                    cmd = new SqlCommand(strSQL);
                                                    cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMauCha);
                                                    cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMauKhac);
                                                    cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                                                }
                                                else
                                                {
                                                    strSQL = "SELECT MAX(B.iSTT) FROM BC_BangMau_HangMau AS A inner join BC_BangMau_HangMau_DonVi AS B ON A.iID_MaBangMau_HangMau = B.iID_MaBangMau_HangMau " +
                                                                "WHERE A.iID_MaHangMau_Cha = @iID_MaHangMau_Cha  AND B.iID_MaBangMau = @iID_MaBangMau AND B.iID_MaDonVi  is null ";
                                                    cmd = new SqlCommand(strSQL);
                                                    cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMauCha);
                                                    cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMauKhac);
                                                }

                                                iSTTBMDV = Convert.ToInt32(Connection.GetValue(cmd, 0));
                                                cmd.Dispose();

                                                if (iSTTBMDV == 0)
                                                {
                                                    if (String.IsNullOrEmpty(MaDonVi) == false)
                                                    {
                                                        strSQL = "SELECT B.iSTT FROM BC_BangMau_HangMau AS A inner join BC_BangMau_HangMau_DonVi AS B ON A.iID_MaBangMau_HangMau = B.iID_MaBangMau_HangMau " +
                                                                    "WHERE A.iID_MaHangMau = @iID_MaHangMau  AND B.iID_MaBangMau = @iID_MaBangMau AND B.iID_MaDonVi = @iID_MaDonVi  ";
                                                        cmd = new SqlCommand(strSQL);
                                                        cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMauCha);
                                                        cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMauKhac);
                                                        cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                                                    }
                                                    else
                                                    {
                                                        strSQL = "SELECT B.iSTT FROM BC_BangMau_HangMau AS A inner join BC_BangMau_HangMau_DonVi AS B ON A.iID_MaBangMau_HangMau = B.iID_MaBangMau_HangMau " +
                                                                    "WHERE A.iID_MaHangMau = @iID_MaHangMau  AND B.iID_MaBangMau = @iID_MaBangMau AND B.iID_MaDonVi is null ";
                                                        cmd = new SqlCommand(strSQL);
                                                        cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMauCha);
                                                        cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMauKhac);
                                                    }

                                                    iSTTBMDV = Convert.ToInt32(Connection.GetValue(cmd, 0));
                                                    cmd.Dispose();
                                                }

                                                //Tăng các số thứ tự trước iSTTBMDV của bảng BC_BangMau_HangMau_DonVi 
                                                cmd = new SqlCommand();
                                                if (String.IsNullOrEmpty(MaDonVi) == false)
                                                {
                                                    cmd.CommandText = "UPDATE BC_BangMau_HangMau_DonVi SET iSTT=iSTT+1 " +
                                                            "WHERE iSTT>@iSTT AND iID_MaBangMau=@iID_MaBangMau AND iID_MaDonVi=@iID_MaDonVi";
                                                    cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMauKhac);
                                                    cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                                                }
                                                else
                                                {
                                                    cmd.CommandText = "UPDATE BC_BangMau_HangMau_DonVi SET iSTT=iSTT+1 " +
                                                                "WHERE iSTT>@iSTT AND iID_MaBangMau=@iID_MaBangMau AND iID_MaDonVi is null";
                                                    cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMauKhac);
                                                }
                                                cmd.Parameters.AddWithValue("@iSTT", iSTTBMDV);
                                                Connection.UpdateDatabase(cmd);
                                                cmd.Dispose();

                                                //Thêm hàng mẫu mới vào Bảng Mẫu Hàng Mẫu cho đơn vị
                                                Bang bang1 = new Bang("BC_BangMau_HangMau_DonVi");
                                                bang1.MaNguoiDungSua = sUserName;
                                                bang1.IPSua = sIP;
                                                bang1.CmdParams.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                                                bang1.CmdParams.Parameters.AddWithValue("@iID_MaBangMau_HangMau", MaBangMauHangMauKhac);
                                                bang1.CmdParams.Parameters.AddWithValue("@iID_MaBangMau", MaBangMauKhac);
                                                bang1.CmdParams.Parameters.AddWithValue("@iSTT", iSTTBMDV + 1);
                                                bang1.Save();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void ThemChiTieuConMoi(String ParentID, String MaHangMauCha, String MaBang, String MaBangMau, String MaDonVi, String TenHangMau, String strPath, DateTime NgayBaoCao, String sUserName, String sIP)
        {
            //String TenHangMau = Request.Form[ParentID + "_sTenHang"];
            DataTable dt;
            SqlCommand cmd;
            String NewID = Globals.getNewGuid().ToString();
            int iSTT, iSTTBMDV;
            //Thêm vào bảng hàng mẫu
            cmd = new SqlCommand("SELECT COUNT(*) FROM BC_HangMau WHERE iID_MaHangMau_Cha=@iID_MaHangMau_Cha");
            cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMauCha);
            int SoHangMauCon = Convert.ToInt32(Connection.GetValue(cmd, 0));
            cmd.Dispose();

            cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO BC_HangMau(iID_MaTam,iID_MaHangMau_Cha, iID_MaDonVi,sTenHang, sCongThuc, iHeight,sBackGround, sColor, iFontSize, bBold, bItalic, bUnderline, bVisible, bTongHangCon, bTinhTongTheoDonVi, bTruongGhiChu, bThemChiTieuCon, iSTT) " +
                            "VALUES(@iID_MaTam,@iID_MaHangMau_Cha, @iID_MaDonVi,@sTenHang, @sCongThuc, @iHeight,@sBackGround, @sColor, @iFontSize, @bBold, @bItalic, @bUnderline, @bVisible, @bTongHangCon, @bTinhTongTheoDonVi, @bTruongGhiChu, @bThemChiTieuCon, @iSTT)";
            cmd.Parameters.AddWithValue("@iID_MaTam", NewID);
            cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMauCha);
            cmd.Parameters.AddWithValue("@iID_MaDonVi", 0);
            cmd.Parameters.AddWithValue("@sTenHang", TenHangMau);
            cmd.Parameters.AddWithValue("@sCongThuc", "");
            cmd.Parameters.AddWithValue("@iHeight", 20);
            cmd.Parameters.AddWithValue("@sBackGround", "#ffffff");
            cmd.Parameters.AddWithValue("@sColor", "#000000");
            cmd.Parameters.AddWithValue("@iFontSize", 12);
            cmd.Parameters.AddWithValue("@bBold", 0);
            cmd.Parameters.AddWithValue("@bItalic", 0);
            cmd.Parameters.AddWithValue("@bUnderline", 0);
            cmd.Parameters.AddWithValue("@bVisible", 0);
            cmd.Parameters.AddWithValue("@bTongHangCon", 0);
            cmd.Parameters.AddWithValue("@bTinhTongTheoDonVi", 0);
            cmd.Parameters.AddWithValue("@bTruongGhiChu", 0);
            cmd.Parameters.AddWithValue("@bThemChiTieuCon", 0);
            cmd.Parameters.AddWithValue("@iSTT", SoHangMauCon + 1);
            Connection.UpdateDatabase(cmd);
            cmd.Dispose();

            cmd = new SqlCommand("SELECT iID_MaHangMau FROM BC_HangMau WHERE iID_MaTam=@iID_MaTam");
            cmd.Parameters.AddWithValue("@iID_MaTam", NewID);
            int MaHangMau = Convert.ToInt32(Connection.GetValue(cmd, 0));
            cmd.Dispose();

            //Thêm vào bảng mẫu - hàng mẫu
            cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO BC_BangMau_HangMau(iID_MaTam,iID_MaBangMau,iID_MaHangMau, iID_MaHangMau_Cha) " +
                        "VALUES(@iID_MaTam,@iID_MaBangMau, @iID_MaHangMau, @iID_MaHangMau_Cha)";
            cmd.Parameters.AddWithValue("@iID_MaTam", NewID);
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
            cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMauCha);
            Connection.UpdateDatabase(cmd);
            cmd.Dispose();


            cmd = new SqlCommand("SELECT * FROM BC_BangMau_HangMau WHERE iID_MaTam=@iID_MaTam");
            cmd.Parameters.AddWithValue("@iID_MaTam", NewID);
            dt = Connection.GetDataTable(cmd);
            cmd.Dispose();
            int MaBangMau_HangMau;

            if (dt.Rows.Count > 0)
            {
                MaBangMau_HangMau = Convert.ToInt32(dt.Rows[0]["iID_MaBangMau_HangMau"]);

                //Xử lý số thứ tự của bảng BC_BangMau_HangMau_DonVi trước khi thêm
                String strSQL;
                if (String.IsNullOrEmpty(MaDonVi) == false)
                {
                    strSQL = "SELECT MAX(B.iSTT) FROM BC_BangMau_HangMau AS A inner join BC_BangMau_HangMau_DonVi AS B ON A.iID_MaBangMau_HangMau = B.iID_MaBangMau_HangMau " +
                                "WHERE A.iID_MaHangMau_Cha = @iID_MaHangMau_Cha  AND B.iID_MaBangMau = @iID_MaBangMau AND B.iID_MaDonVi = @iID_MaDonVi  ";
                    cmd = new SqlCommand(strSQL);
                    cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMauCha);
                    cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                    cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                }
                else
                {
                    strSQL = "SELECT MAX(B.iSTT) FROM BC_BangMau_HangMau AS A inner join BC_BangMau_HangMau_DonVi AS B ON A.iID_MaBangMau_HangMau = B.iID_MaBangMau_HangMau " +
                                "WHERE A.iID_MaHangMau_Cha = @iID_MaHangMau_Cha  AND B.iID_MaBangMau = @iID_MaBangMau AND B.iID_MaDonVi  is null ";
                    cmd = new SqlCommand(strSQL);
                    cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMauCha);
                    cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                }

                iSTTBMDV = Convert.ToInt32(Connection.GetValue(cmd, 0));
                cmd.Dispose();

                if (iSTTBMDV == 0)
                {
                    if (String.IsNullOrEmpty(MaDonVi) == false)
                    {
                        strSQL = "SELECT B.iSTT FROM BC_BangMau_HangMau AS A inner join BC_BangMau_HangMau_DonVi AS B ON A.iID_MaBangMau_HangMau = B.iID_MaBangMau_HangMau " +
                                    "WHERE A.iID_MaHangMau = @iID_MaHangMau  AND B.iID_MaBangMau = @iID_MaBangMau AND B.iID_MaDonVi = @iID_MaDonVi  ";
                        cmd = new SqlCommand(strSQL);
                        cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMauCha);
                        cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                        cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                    }
                    else
                    {
                        strSQL = "SELECT B.iSTT FROM BC_BangMau_HangMau AS A inner join BC_BangMau_HangMau_DonVi AS B ON A.iID_MaBangMau_HangMau = B.iID_MaBangMau_HangMau " +
                                    "WHERE A.iID_MaHangMau = @iID_MaHangMau  AND B.iID_MaBangMau = @iID_MaBangMau AND B.iID_MaDonVi is null ";
                        cmd = new SqlCommand(strSQL);
                        cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMauCha);
                        cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                    }

                    iSTTBMDV = Convert.ToInt32(Connection.GetValue(cmd, 0));
                    cmd.Dispose();
                }

                //Tăng các số thứ tự trước iSTTBMDV của bảng BC_BangMau_HangMau_DonVi 
                cmd = new SqlCommand();
                if (String.IsNullOrEmpty(MaDonVi) == false)
                {
                    cmd.CommandText = "UPDATE BC_BangMau_HangMau_DonVi SET iSTT=iSTT+1 " +
                            "WHERE iSTT>@iSTT AND iID_MaBangMau=@iID_MaBangMau AND iID_MaDonVi=@iID_MaDonVi";
                    cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                    cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                }
                else
                {
                    cmd.CommandText = "UPDATE BC_BangMau_HangMau_DonVi SET iSTT=iSTT+1 " +
                                "WHERE iSTT>@iSTT AND iID_MaBangMau=@iID_MaBangMau AND iID_MaDonVi is null";
                    cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                }
                cmd.Parameters.AddWithValue("@iSTT", iSTTBMDV);
                Connection.UpdateDatabase(cmd);
                cmd.Dispose();


                //Thêm hàng mẫu mới vào Bảng Mẫu Hàng Mẫu cho đơn vị
                cmd = new SqlCommand();
                cmd.CommandText = "INSERT INTO BC_BangMau_HangMau_DonVi(iID_MaBangMau_HangMau,iID_MaDonVi,iID_MaBangMau,iSTT) " +
                            "VALUES(@iID_MaBangMau_HangMau,@iID_MaDonVi,@iID_MaBangMau,@iSTT)";
                cmd.Parameters.AddWithValue("@iID_MaBangMau_HangMau", MaBangMau_HangMau);
                cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                cmd.Parameters.AddWithValue("@iSTT", iSTTBMDV + 1);
                Connection.UpdateDatabase(cmd);
                cmd.Dispose();

                cmd = new SqlCommand("SELECT MAX(iSTT) FROM BC_Bang_Hang WHERE iID_MaBang=@iID_MaBang AND iID_MaHangMau_Cha=@iID_MaHangMau_Cha");
                cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
                cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMauCha);
                iSTT = Convert.ToInt32(Connection.GetValue(cmd, 0));
                cmd.Dispose();

                if (iSTT == 0)
                {
                    cmd = new SqlCommand("SELECT iSTT FROM BC_Bang_Hang WHERE iID_MaBang=@iID_MaBang AND iID_MaHangMau=@iID_MaHangMau");
                    cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
                    cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMauCha);
                    iSTT = Convert.ToInt32(Connection.GetValue(cmd, 0));
                    cmd.Dispose();
                }

                cmd = new SqlCommand();
                cmd.CommandText = "UPDATE BC_Bang_Hang SET iSTT=iSTT+1 " +
                            "WHERE iSTT>@iSTT AND iID_MaBang=@iID_MaBang";
                cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
                cmd.Parameters.AddWithValue("@iSTT", iSTT);
                Connection.UpdateDatabase(cmd);
                cmd.Dispose();

                //Thêm hàng mới vào bảng hàng
                cmd = new SqlCommand();
                cmd.CommandText = "INSERT INTO BC_Bang_Hang(iID_MaBang,iID_MaHangMau_Cha,iID_MaHangMau,iID_MaDonVi,sTenHang,sCongThuc, iHeight,sBackGround, sColor, iFontSize, bBold, bItalic, bUnderline, bTongHangCon, bTinhTongTheoDonVi, bTruongGhiChu, bThemChiTieuCon, iSTT) " +
                            "VALUES(@iID_MaBang,@iID_MaHangMau_Cha, @iID_MaHangMau, @iID_MaDonVi,@sTenHang, @sCongThuc, @iHeight,@sBackGround, @sColor, @iFontSize, @bBold, @bItalic, @bUnderline, @bTongHangCon, @bTinhTongTheoDonVi, @bTruongGhiChu, @bThemChiTieuCon, @iSTT)";
                cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
                cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMauCha);
                cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
                cmd.Parameters.AddWithValue("@iID_MaDonVi", 0);
                cmd.Parameters.AddWithValue("@sTenHang", TenHangMau);
                cmd.Parameters.AddWithValue("@sCongThuc", "");
                cmd.Parameters.AddWithValue("@iHeight", 20);
                cmd.Parameters.AddWithValue("@sBackGround", "#ffffff");
                cmd.Parameters.AddWithValue("@sColor", "#000000");
                cmd.Parameters.AddWithValue("@iFontSize", 12);
                cmd.Parameters.AddWithValue("@bBold", 0);
                cmd.Parameters.AddWithValue("@bItalic", 0);
                cmd.Parameters.AddWithValue("@bUnderline", 0);
                cmd.Parameters.AddWithValue("@bTongHangCon", 0);
                cmd.Parameters.AddWithValue("@bTinhTongTheoDonVi", 0);
                cmd.Parameters.AddWithValue("@bTruongGhiChu", 0);
                cmd.Parameters.AddWithValue("@bThemChiTieuCon", 0);
                cmd.Parameters.AddWithValue("@iSTT", iSTT + 1);
                Connection.UpdateDatabase(cmd);
                cmd.Dispose();

                dt.Dispose();
                dt.Clear();

                //Xóa bảng công thức
                cmd = new SqlCommand("DELETE FROM BC_Bang_CongThuc WHERE iID_MaBang=@iID_MaBang");
                cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
                Connection.UpdateDatabase(cmd);
                cmd.Dispose();

                BangModels_CongThuc.TaoCongThucChoBangMoi(MaBangMau, MaBang, sUserName, sIP);

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


                //Thêm chỉ tiêu đó vào các bảng mẫu hàng mẫu khác
                String tgMaBangMau = MaBangMau;
                int i, j;
                List<String> lstMaBangMauCungNhau = new List<string>();
                cmd = new SqlCommand("SELECT iID_MaBangCungNhau FROM BC_BangMau WHERE iID_MaBangMau = @iID_MaBangMau");
                cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                String strMaBangMauCungNhau = Convert.ToString(Connection.GetValue(cmd, ""));
                cmd.Dispose();
                if (strMaBangMauCungNhau != null && strMaBangMauCungNhau != "")
                {
                    String[] arrChuoiBangCungNhau = strMaBangMauCungNhau.Split(';');
                    for (i = 0; i < arrChuoiBangCungNhau.Length; i++)
                    {
                        if (arrChuoiBangCungNhau[i] != "" && arrChuoiBangCungNhau[i] != MaBangMau)
                        {
                            lstMaBangMauCungNhau.Add(arrChuoiBangCungNhau[i]);
                        }
                    }
                    for (i = 0; i < lstMaBangMauCungNhau.Count; i++)
                    {
                        MaBangMau = Convert.ToString(lstMaBangMauCungNhau[i]);
                        cmd = new SqlCommand("SELECT bXemTheoDonVi FROM BC_BangMau WHERE iID_MaBangMau = @iID_MaBangMau");
                        cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                        Boolean bXemTheoDonVi = Convert.ToBoolean(Connection.GetValue(cmd, 0));
                        cmd.Dispose();
                        if (bXemTheoDonVi == true)
                        {
                            //Thêm vào bảng mẫu - hàng mẫu
                            NewID = Globals.getNewGuid().ToString();
                            cmd = new SqlCommand();
                            cmd.CommandText = "INSERT INTO BC_BangMau_HangMau(iID_MaTam,iID_MaBangMau,iID_MaHangMau, iID_MaHangMau_Cha) " +
                                        "VALUES(@iID_MaTam,@iID_MaBangMau, @iID_MaHangMau, @iID_MaHangMau_Cha)";
                            cmd.Parameters.AddWithValue("@iID_MaTam", NewID);
                            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                            cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
                            cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMauCha);
                            Connection.UpdateDatabase(cmd);
                            cmd.Dispose();

                            cmd = new SqlCommand("SELECT * FROM BC_BangMau_HangMau WHERE iID_MaTam=@iID_MaTam");
                            cmd.Parameters.AddWithValue("@iID_MaTam", NewID);
                            dt = Connection.GetDataTable(cmd);
                            cmd.Dispose();
                            if (dt.Rows.Count > 0)
                            {
                                MaBangMau_HangMau = Convert.ToInt32(dt.Rows[0]["iID_MaBangMau_HangMau"]);

                                strSQL = "";
                                iSTTBMDV = 0;
                                if (String.IsNullOrEmpty(MaDonVi) == false)
                                {
                                    strSQL = "SELECT MAX(B.iSTT) FROM BC_BangMau_HangMau AS A inner join BC_BangMau_HangMau_DonVi AS B ON A.iID_MaBangMau_HangMau = B.iID_MaBangMau_HangMau " +
                                                "WHERE A.iID_MaHangMau_Cha = @iID_MaHangMau_Cha  AND B.iID_MaBangMau = @iID_MaBangMau AND B.iID_MaDonVi = @iID_MaDonVi  ";
                                    cmd = new SqlCommand(strSQL);
                                    cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMauCha);
                                    cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                                    cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                                }
                                else
                                {
                                    strSQL = "SELECT MAX(B.iSTT) FROM BC_BangMau_HangMau AS A inner join BC_BangMau_HangMau_DonVi AS B ON A.iID_MaBangMau_HangMau = B.iID_MaBangMau_HangMau " +
                                                "WHERE A.iID_MaHangMau_Cha = @iID_MaHangMau_Cha  AND B.iID_MaBangMau = @iID_MaBangMau AND B.iID_MaDonVi  is null ";
                                    cmd = new SqlCommand(strSQL);
                                    cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMauCha);
                                    cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                                }

                                iSTTBMDV = Convert.ToInt32(Connection.GetValue(cmd, 0));
                                cmd.Dispose();

                                if (iSTTBMDV == 0)
                                {
                                    if (String.IsNullOrEmpty(MaDonVi) == false)
                                    {
                                        strSQL = "SELECT B.iSTT FROM BC_BangMau_HangMau AS A inner join BC_BangMau_HangMau_DonVi AS B ON A.iID_MaBangMau_HangMau = B.iID_MaBangMau_HangMau " +
                                                    "WHERE A.iID_MaHangMau = @iID_MaHangMau  AND B.iID_MaBangMau = @iID_MaBangMau AND B.iID_MaDonVi = @iID_MaDonVi  ";
                                        cmd = new SqlCommand(strSQL);
                                        cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMauCha);
                                        cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                                        cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                                    }
                                    else
                                    {
                                        strSQL = "SELECT B.iSTT FROM BC_BangMau_HangMau AS A inner join BC_BangMau_HangMau_DonVi AS B ON A.iID_MaBangMau_HangMau = B.iID_MaBangMau_HangMau " +
                                                    "WHERE A.iID_MaHangMau = @iID_MaHangMau  AND B.iID_MaBangMau = @iID_MaBangMau AND B.iID_MaDonVi is null ";
                                        cmd = new SqlCommand(strSQL);
                                        cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMauCha);
                                        cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                                    }

                                    iSTTBMDV = Convert.ToInt32(Connection.GetValue(cmd, 0));
                                    cmd.Dispose();
                                }

                                //Tăng các số thứ tự trước iSTTBMDV của bảng BC_BangMau_HangMau_DonVi 
                                cmd = new SqlCommand();
                                if (String.IsNullOrEmpty(MaDonVi) == false)
                                {
                                    cmd.CommandText = "UPDATE BC_BangMau_HangMau_DonVi SET iSTT=iSTT+1 " +
                                            "WHERE iSTT>@iSTT AND iID_MaBangMau=@iID_MaBangMau AND iID_MaDonVi=@iID_MaDonVi";
                                    cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                                    cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                                }
                                else
                                {
                                    cmd.CommandText = "UPDATE BC_BangMau_HangMau_DonVi SET iSTT=iSTT+1 " +
                                                "WHERE iSTT>@iSTT AND iID_MaBangMau=@iID_MaBangMau AND iID_MaDonVi is null";
                                    cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                                }
                                cmd.Parameters.AddWithValue("@iSTT", iSTTBMDV);
                                Connection.UpdateDatabase(cmd);
                                cmd.Dispose();

                                //Thêm hàng mẫu mới vào Bảng Mẫu Hàng Mẫu cho đơn vị
                                cmd = new SqlCommand();
                                cmd.CommandText = "INSERT INTO BC_BangMau_HangMau_DonVi(iID_MaBangMau_HangMau,iID_MaDonVi,iID_MaBangMau,iSTT) " +
                                            "VALUES(@iID_MaBangMau_HangMau,@iID_MaDonVi,@iID_MaBangMau,@iSTT)";
                                cmd.Parameters.AddWithValue("@iID_MaBangMau_HangMau", MaBangMau_HangMau);
                                cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                                cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                                cmd.Parameters.AddWithValue("@iSTT", iSTTBMDV + 1);
                                Connection.UpdateDatabase(cmd);
                                cmd.Dispose();

                                dt.Dispose();
                                dt.Clear();

                            }
                        }
                    }
                }
            }
        }

        public static void XoaChiTieuBaoCao(String MaHangMauCha, String MaHangMau, String MaBang, String MaBangMau, String MaDonVi, String MaPhongBan, String strPath, DateTime NgayBaoCao, String LoaiBaoCao, String sUserName, String sIP)
        {
            SqlCommand cmd;
            int ThemChiTieuCon;

            cmd = new SqlCommand("SELECT bThemChiTieuCon FROM BC_HangMau WHERE iID_MaHangMau= @iID_MaHangMau");
            cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMauCha);
            ThemChiTieuCon = Convert.ToInt32(Connection.GetValue(cmd, 0));
            cmd.Dispose();

            if (ThemChiTieuCon != 0)
            {
                cmd = new SqlCommand("DELETE FROM BC_BangMau_HangMau WHERE iID_MaBangMau=@iID_MaBangMau AND iID_MaHangMau=@iID_MaHangMau AND iID_MaHangMau_Cha=@iID_MaHangMau_Cha");
                cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
                cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMauCha);
                Connection.UpdateDatabase(cmd);
                cmd.Dispose();
            }
            // Lấy mã bảng mẫu hàng mẫu
            int MaBangMauHangMau;
            cmd = new SqlCommand("SELECT iID_MaBangMau_HangMau FROM BC_BangMau_HangMau WHERE iID_MaBangMau=@iID_MaBangMau AND iID_MaHangMau=@iID_MaHangMau AND iID_MaHangMau_Cha=@iID_MaHangMau_Cha");
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
            cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMauCha);
            MaBangMauHangMau = Convert.ToInt32(Connection.GetValue(cmd, 0));
            cmd.Dispose();

            //Xóa hàng mẫu trong Bảng Mẫu Hàng Mẫu cho đơn vị
            int MaBangMauHangMauDonVi;
            cmd = new SqlCommand();
            cmd.CommandText = "SELECT iID_MaBangMau_HangMau_DonVi FROM BC_BangMau_HangMau_DonVi " +
                        "WHERE iID_MaBangMau_HangMau=@iID_MaBangMau_HangMau AND iID_MaDonVi=@iID_MaDonVi AND iID_MaBangMau=@iID_MaBangMau";
            cmd.Parameters.AddWithValue("@iID_MaBangMau_HangMau", MaBangMauHangMau);
            cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            MaBangMauHangMauDonVi = Convert.ToInt32(Connection.GetValue(cmd, 0));
            cmd.Dispose();

            cmd = new SqlCommand("DELETE FROM BC_BangMau_HangMau_DonVi WHERE iID_MaBangMau_HangMau_DonVi = @iID_MaBangMau_HangMau_DonVi");
            cmd.Parameters.AddWithValue("@iID_MaBangMau_HangMau_DonVi", MaBangMauHangMauDonVi);
            Connection.UpdateDatabase(cmd);
            cmd.Dispose();

            //Xóa hàng mẫu trong Bảng Hàng
            cmd = new SqlCommand("SELECT iID_MaHang FROM BC_Bang_Hang WHERE iID_MaBang=@iID_MaBang AND iID_MaHangMau= @iID_MaHangMau AND iID_MaHangMau_Cha=@iID_MaHangMau_Cha");
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
            cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMauCha);
            int MaBangMaHang = Convert.ToInt32(Connection.GetValue(cmd, 0));
            cmd.Dispose();

            cmd = new SqlCommand("DELETE FROM BC_Bang_Hang WHERE iID_MaHang = @iID_MaHang");
            cmd.Parameters.AddWithValue("@iID_MaHang", MaBangMaHang);
            Connection.UpdateDatabase(cmd);
            cmd.Dispose();

            //Xóa bảng công thức
            cmd = new SqlCommand("DELETE FROM BC_Bang_CongThuc WHERE iID_MaBang=@iID_MaBang");
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            Connection.UpdateDatabase(cmd);
            cmd.Dispose();

            BangModels_CongThuc.TaoCongThucChoBangMoi(MaBangMau, MaBang, sUserName, sIP);

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

            //Xóa chỉ tiêu đó vào các bảng mẫu hàng mẫu khác
            String tgMaBangMau = MaBangMau;
            int i, j;
            List<String> lstMaBangMauCungNhau = new List<string>();
            cmd = new SqlCommand("SELECT iID_MaBangCungNhau FROM BC_BangMau WHERE iID_MaBangMau = @iID_MaBangMau");
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            String strMaBangMauCungNhau = Convert.ToString(Connection.GetValue(cmd, ""));
            cmd.Dispose();
            if (strMaBangMauCungNhau != null && strMaBangMauCungNhau != "")
            {
                String[] arrChuoiBangCungNhau = strMaBangMauCungNhau.Split(';');
                for (i = 0; i < arrChuoiBangCungNhau.Length; i++)
                {
                    if (arrChuoiBangCungNhau[i] != "" && arrChuoiBangCungNhau[i] != MaBangMau)
                    {
                        lstMaBangMauCungNhau.Add(arrChuoiBangCungNhau[i]);
                    }
                }
                for (i = 0; i < lstMaBangMauCungNhau.Count; i++)
                {
                    MaBangMau = Convert.ToString(lstMaBangMauCungNhau[i]);
                    cmd = new SqlCommand("SELECT bXemTheoDonVi FROM BC_BangMau WHERE iID_MaBangMau = @iID_MaBangMau AND iID_MaBangNhapLieu = 1");
                    cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                    Boolean bXemTheoDonVi = Convert.ToBoolean(Connection.GetValue(cmd, 0));
                    cmd.Dispose();
                    if (bXemTheoDonVi == true)
                    {
                        //Lấy bảng đã tạo
                        String DK;
                        cmd = new SqlCommand();
                        DK = "iID_MaBangMau = @iID_MaBangMau AND iID_MaDonVi=@iID_MaDonVi ";
                        cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                        cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                        switch (Convert.ToInt32(LoaiBaoCao))
                        {
                            case 1:
                                DK += " AND (CONVERT(varchar(10),dNgayBaoCao,111)=@sNgayBaoCao)";
                                cmd.Parameters.AddWithValue("@sNgayBaoCao", NgayBaoCao.ToString("yyyy/MM/dd"));
                                break;

                            case 2:
                                DK += " AND (CONVERT(varchar(7),dNgayBaoCao,111)=@sNgayBaoCao)";
                                cmd.Parameters.AddWithValue("@sNgayBaoCao", NgayBaoCao.ToString("yyyy/MM"));
                                break;

                            case 3:
                                DK += " AND (CONVERT(varchar(7),dNgayBaoCao,111)=@sNgayBaoCao)";
                                cmd.Parameters.AddWithValue("@sNgayBaoCao", NgayBaoCao.ToString("yyyy/MM"));
                                break;
                            case 4:
                                DK += " AND (CONVERT(varchar(4),dNgayBaoCao,111)=@sNgayBaoCao)";
                                cmd.Parameters.AddWithValue("@sNgayBaoCao", NgayBaoCao.ToString("yyyy"));
                                break;
                            case 5:
                                DK += " AND (CONVERT(varchar(7),dNgayBaoCao,111)=@sNgayBaoCao)";
                                cmd.Parameters.AddWithValue("@sNgayBaoCao", NgayBaoCao.ToString("yyyy/MM"));
                                break;
                        }
                        cmd.CommandText = String.Format("SELECT iID_MaBang FROM BC_Bang WHERE {0} ORDER BY iSTT", DK);
                        DataTable dtBang = Connection.GetDataTable(cmd);
                        cmd.Dispose();
                        if (dtBang.Rows.Count > 0)
                        { }
                        else
                        {
                            cmd = new SqlCommand();
                            cmd.CommandText = "SELECT iID_MaBangNhapLieu FROM BC_BangMau WHERE iID_MaBangMau = @iID_MaBangMau";
                            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                            int LoaiBangNhapLieu = Convert.ToInt32(Connection.GetValue(cmd, 0));
                            cmd.Dispose();
                            if (LoaiBangNhapLieu == 1)
                            {
                                //cmd = new SqlCommand("DELETE FROM BC_BangMau_HangMau WHERE iID_MaBangMau=@iID_MaBangMau AND iID_MaHangMau=@iID_MaHangMau AND iID_MaHangMau_Cha=@iID_MaHangMau_Cha");
                                //cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                                //cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
                                //cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMauCha);
                                //Connection.UpdateDatabase(cmd);
                                //cmd.Dispose();

                                // Lấy mã bảng mẫu hàng mẫu
                                int MaBangMauHangMauCacBang;
                                cmd = new SqlCommand("SELECT iID_MaBangMau_HangMau FROM BC_BangMau_HangMau WHERE iID_MaBangMau=@iID_MaBangMau AND iID_MaHangMau=@iID_MaHangMau AND iID_MaHangMau_Cha=@iID_MaHangMau_Cha");
                                cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                                cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
                                cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMauCha);
                                MaBangMauHangMauCacBang = Convert.ToInt32(Connection.GetValue(cmd, 0));
                                cmd.Dispose();

                                //Xóa hàng mẫu trong Bảng Mẫu Hàng Mẫu cho đơn vị
                                int MaBangMauHangMauDonViCacBang;
                                cmd = new SqlCommand();
                                cmd.CommandText = "SELECT iID_MaBangMau_HangMau_DonVi FROM BC_BangMau_HangMau_DonVi " +
                                                  "WHERE iID_MaBangMau_HangMau=@iID_MaBangMau_HangMau AND iID_MaDonVi=@iID_MaDonVi AND iID_MaBangMau=@iID_MaBangMau";
                                cmd.Parameters.AddWithValue("@iID_MaBangMau_HangMau", MaBangMauHangMauCacBang);
                                cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                                cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                                MaBangMauHangMauDonViCacBang = Convert.ToInt32(Connection.GetValue(cmd, 0));
                                cmd.Dispose();

                                cmd = new SqlCommand("DELETE FROM BC_BangMau_HangMau_DonVi WHERE iID_MaBangMau_HangMau_DonVi = @iID_MaBangMau_HangMau_DonVi");
                                cmd.Parameters.AddWithValue("@iID_MaBangMau_HangMau_DonVi", MaBangMauHangMauDonViCacBang);
                                Connection.UpdateDatabase(cmd);
                                cmd.Dispose();
                            }
                            //Thêm vào bảng mẫu - hàng mẫu
                            //cmd = new SqlCommand("UPDATE BC_BangMau_HangMau SET iMaTrangThai = 0 WHERE iID_MaBangMau=@iID_MaBangMau AND iID_MaHangMau=@iID_MaHangMau");
                            //cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                            //cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
                            //Connection.UpdateDatabase(cmd);
                            //cmd.Dispose();
                        }
                    }
                }
            }
        }
    }
}