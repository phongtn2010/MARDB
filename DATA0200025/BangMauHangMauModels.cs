using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DomainModel;
using System.Data.SqlClient;
using DomainModel.Abstract;

namespace DATA0200025
{
    public class BangMauHangMauModels
    {
        private static void CapNhapSTTTheoCay_BangMau_HangMau_DonVi(DataTable dtHangMau, DataTable dtBangMau_HangMau, Boolean XemTheoDonVi, String MaBangMau, int MaHangMauCha, int MaHangMauCha_ChungHangMauCon, ref int STT)
        {
            int i, j, MaHangMau, MaHangMau_ChungHangMauCon;
            DataRow Row;
            

            for (i = 0; i < dtHangMau.Rows.Count; i++)
            {
                Row = dtHangMau.Rows[i];
                if (MaHangMauCha_ChungHangMauCon == Convert.ToInt32(Row["iID_MaHangMau_Cha"]))
                {
                    MaHangMau = Convert.ToInt32(Row["iID_MaHangMau"]);
                    MaHangMau_ChungHangMauCon = MaHangMau;
                    if (Convert.ToInt32(Row["iID_MaHangMau_ChungHangMauCon"]) > 0)
                    {
                        MaHangMau_ChungHangMauCon = Convert.ToInt32(Row["iID_MaHangMau_ChungHangMauCon"]);
                    }
                    for (j = 0; j < dtBangMau_HangMau.Rows.Count; j++)
                    {
                        if (MaHangMau == Convert.ToInt32(dtBangMau_HangMau.Rows[j]["iID_MaHangMau"]) &&
                            MaHangMauCha == Convert.ToInt32(dtBangMau_HangMau.Rows[j]["iID_MaHangMau_Cha"]))
                        {
                            STT++;

                            SqlCommand cmd = new SqlCommand("UPDATE BC_BangMau_HangMau_DonVi SET iSTT=@iSTT WHERE iID_MaBangMau_HangMau_DonVi=@iID_MaBangMau_HangMau_DonVi");
                            cmd.Parameters.AddWithValue("@iSTT", STT);
                            cmd.Parameters.AddWithValue("@iID_MaBangMau_HangMau_DonVi", dtBangMau_HangMau.Rows[j]["iID_MaBangMau_HangMau_DonVi"]);
                            Connection.UpdateDatabase(cmd);
                            cmd.Dispose();
                        }
                    }
                    CapNhapSTTTheoCay_BangMau_HangMau_DonVi(dtHangMau, dtBangMau_HangMau, XemTheoDonVi, MaBangMau, MaHangMau, MaHangMau_ChungHangMauCon, ref STT);
                }
            }
        }

        public static void CapNhapSTT_BangMau_HangMau_DonVi(String MaBangMau, String MaDonVi)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM BC_HangMau ORDER BY iSTT";
            DataTable dtHangMau = Connection.GetDataTable(cmd);
            cmd.Dispose();

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT bXemTheoDonVi FROM BC_BangMau WHERE iID_MaBangMau=@iID_MaBangMau";
            Boolean XemTheoDonVi = Convert.ToBoolean(Connection.GetValue(cmd,false));
            cmd.Dispose();

            
            cmd = new SqlCommand();
            String SQL1, SQL2;
            SQL1 = "SELECT * " +
                    "FROM BC_BangMau_HangMau " +
                    "WHERE iID_MaBangMau=@iID_MaBangMau AND iMaTrangThai = 1";
            if (XemTheoDonVi)
            {
                SQL2 = "SELECT * " +
                        "FROM BC_BangMau_HangMau_DonVi " +
                        "WHERE iID_MaBangMau=@iID_MaBangMau AND iID_MaDonVi=@iID_MaDonVi ";
                cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
            }
            else
            {
                SQL2 = "SELECT * " +
                        "FROM BC_BangMau_HangMau_DonVi " +
                        "WHERE iID_MaBangMau=@iID_MaBangMau";
                cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            }
            cmd.CommandText = String.Format("SELECT tb1.*, tb2.iID_MaBangMau_HangMau_DonVi FROM ({0}) tb1 INNER JOIN ({1}) tb2 ON tb1.iID_MaBangMau_HangMau=tb2.iID_MaBangMau_HangMau", SQL1, SQL2);
            DataTable dtBangMau_HangMau = Connection.GetDataTable(cmd);
            cmd.Dispose();

            int STT = 0;
            CapNhapSTTTheoCay_BangMau_HangMau_DonVi(dtHangMau, dtBangMau_HangMau, XemTheoDonVi, MaBangMau, 0, 0, ref STT);
            dtHangMau.Dispose();
            dtBangMau_HangMau.Dispose();
        }

        private static void TaoHangTheoCay(DataTable dtHangMau, DataTable dtBangMau_HangMau, String MaBang, Boolean XemTheoDonVi, String MaBangMau, int MaHangMauCha, int MaHangMauCha_ChungHangMauCon, ref int STT, String sUserName, String sIP)
        {
            int i, j, k, MaHangMau, MaHangMau_ChungHangMauCon;
            Bang bang;
            DataRow Row;

            for (i = 0; i < dtHangMau.Rows.Count; i++)
            {
                Row = dtHangMau.Rows[i];
                if (MaHangMauCha_ChungHangMauCon == Convert.ToInt32(Row["iID_MaHangMau_Cha"]))
                {
                    MaHangMau = Convert.ToInt32(Row["iID_MaHangMau"]);
                    MaHangMau_ChungHangMauCon = MaHangMau;
                    if (Convert.ToInt32(Row["iID_MaHangMau_ChungHangMauCon"]) > 0)
                    {
                        MaHangMau_ChungHangMauCon = Convert.ToInt32(Row["iID_MaHangMau_ChungHangMauCon"]);
                    }
                    for (j = 0; j < dtBangMau_HangMau.Rows.Count; j++)
                    {
                        if (MaHangMau == Convert.ToInt32(dtBangMau_HangMau.Rows[j]["iID_MaHangMau"]) &&
                            MaHangMauCha == Convert.ToInt32(dtBangMau_HangMau.Rows[j]["iID_MaHangMau_Cha"]))
                        {
                            STT++;

                            bang = new Bang("BC_Bang_Hang");
                            bang.MaNguoiDungSua = sUserName;
                            bang.IPSua = sIP;
                            bang.DuLieuMoi = true;
                            bang.CmdParams.Parameters.AddWithValue("@iID_MaBang", MaBang);
                            bang.CmdParams.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMauCha);
                            bang.CmdParams.Parameters.AddWithValue("@iID_MaHangMau", Row["iID_MaHangMau"]);
                            bang.CmdParams.Parameters.AddWithValue("@sCongThuc", Row["sCongThuc"]);
                            bang.CmdParams.Parameters.AddWithValue("@sTenHang", Row["sTenHang"]);
                            bang.CmdParams.Parameters.AddWithValue("@iHeight", Row["iHeight"]);
                            bang.CmdParams.Parameters.AddWithValue("@sBackGround", Row["sBackGround"]);
                            bang.CmdParams.Parameters.AddWithValue("@sColor", Row["sColor"]);
                            bang.CmdParams.Parameters.AddWithValue("@iFontSize", Row["iFontSize"]);
                            bang.CmdParams.Parameters.AddWithValue("@bBold", Row["bBold"]);
                            bang.CmdParams.Parameters.AddWithValue("@bItalic", Row["bItalic"]);
                            bang.CmdParams.Parameters.AddWithValue("@bUnderline", Row["bUnderline"]);
                            bang.CmdParams.Parameters.AddWithValue("@bVisible", Row["bVisible"]);
                            bang.CmdParams.Parameters.AddWithValue("@bTongHangCon", Row["bTongHangCon"]);
                            bang.CmdParams.Parameters.AddWithValue("@iID_MaDonVi", Row["iID_MaDonVi"]);
                            bang.CmdParams.Parameters.AddWithValue("@bTinhTongTheoDonVi", Row["bTinhTongTheoDonVi"]);
                            bang.CmdParams.Parameters.AddWithValue("@bTruongGhiChu", Row["bTruongGhiChu"]);
                            bang.CmdParams.Parameters.AddWithValue("@iID_LoaiSanPham", Row["iID_LoaiSanPham"]);
                            bang.CmdParams.Parameters.AddWithValue("@bThemChiTieuCon", Row["bThemChiTieuCon"]);
                            bang.CmdParams.Parameters.AddWithValue("@bChuaLoaiSanPham", Row["bChuaLoaiSanPham"]);
                            bang.CmdParams.Parameters.AddWithValue("@bChuaSanPham", Row["bChuaSanPham"]);
                            bang.CmdParams.Parameters.AddWithValue("@bTinhTongTheoLoaiSanPham", Row["bTinhTongTheoLoaiSanPham"]);
                            bang.CmdParams.Parameters.AddWithValue("@iSTT", STT);
                            bang.Save();
                            if (Convert.ToBoolean(Row["bChuaLoaiSanPham"]) && (XemTheoDonVi == false))
                            {
                                DataTable dtLoaiSP = Connection.GetDataTable("SELECT * FROM BC_LoaiSanPham ORDER BY sTen");
                                for (k = 0; k < dtLoaiSP.Rows.Count; k++)
                                {
                                    STT = STT + 1;
                                    bang = new Bang("BC_Bang_Hang");
                                    bang.MaNguoiDungSua = sUserName;
                                    bang.IPSua = sIP;
                                    bang.DuLieuMoi = true;
                                    bang.CmdParams.Parameters.AddWithValue("@iID_MaBang", MaBang);
                                    bang.CmdParams.Parameters.AddWithValue("@iID_MaHangMau_Cha", Row["iID_MaHangMau"]);
                                    bang.CmdParams.Parameters.AddWithValue("@sTenHang", dtLoaiSP.Rows[k]["sTen"]);
                                    bang.CmdParams.Parameters.AddWithValue("@bTinhTongTheoDonVi", 1);
                                    bang.CmdParams.Parameters.AddWithValue("@iID_LoaiSanPham", dtLoaiSP.Rows[k]["iID_LoaiSanPham"]);
                                    bang.CmdParams.Parameters.AddWithValue("@iSTT", STT);
                                    bang.Save();
                                }
                                dtLoaiSP.Dispose();
                            }
                        }
                    }
                    TaoHangTheoCay(dtHangMau, dtBangMau_HangMau, MaBang, XemTheoDonVi, MaBangMau, MaHangMau, MaHangMau_ChungHangMauCon, ref STT, sUserName, sIP);
                }
            }
        }

        public static void TaoHangMoi(String MaBangMau, String MaDonVi, String MaBang, Boolean XemTheoDonVi, String sUserName, String sIP)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM BC_HangMau ORDER BY iSTT";
            DataTable dtHangMau = Connection.GetDataTable(cmd);
            cmd.Dispose();

            cmd = new SqlCommand();
            String SQL1, SQL2;
            SQL1 = "SELECT * " +
                    "FROM BC_BangMau_HangMau " +
                    "WHERE iID_MaBangMau=@iID_MaBangMau AND iMaTrangThai = 1";
            if (XemTheoDonVi)
            {   
                SQL2 = "SELECT * " +
                        "FROM BC_BangMau_HangMau_DonVi " +
                        "WHERE iID_MaBangMau=@iID_MaBangMau AND iID_MaDonVi=@iID_MaDonVi ";
                cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
            }
            else
            {
                SQL2 = "SELECT * " +
                        "FROM BC_BangMau_HangMau_DonVi " +
                        "WHERE iID_MaBangMau=@iID_MaBangMau";
                cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            }
            cmd.CommandText = String.Format("SELECT tb1.* FROM ({0}) tb1 INNER JOIN ({1}) tb2 ON tb1.iID_MaBangMau_HangMau=tb2.iID_MaBangMau_HangMau ORDER BY tb2.iSTT", SQL1, SQL2);
            DataTable dtBangMau_HangMau = Connection.GetDataTable(cmd);
            cmd.Dispose();

            int STT = 0;
            //TaoHangTheoCay(dtHangMau, dtBangMau_HangMau, MaBang, XemTheoDonVi, MaBangMau, 0, 0, ref STT);

            //Tạo các hàng mới
            int i, j, k;
            Bang bang;
            DataRow Row;
            for (j = 0; j < dtBangMau_HangMau.Rows.Count; j++)
            {
                for (i = 0; i < dtHangMau.Rows.Count; i++)
                {
                    Row = dtHangMau.Rows[i];
                    if (Convert.ToInt32(Row["iID_MaHangMau"]) == Convert.ToInt32(dtBangMau_HangMau.Rows[j]["iID_MaHangMau"]))
                    {
                        STT++;

                        bang = new Bang("BC_Bang_Hang");
                        bang.MaNguoiDungSua = sUserName;
                        bang.IPSua = sIP;
                        bang.DuLieuMoi = true;
                        bang.CmdParams.Parameters.AddWithValue("@iID_MaBang", MaBang);
                        bang.CmdParams.Parameters.AddWithValue("@iID_MaHangMau_Cha", dtBangMau_HangMau.Rows[j]["iID_MaHangMau_Cha"]);
                        bang.CmdParams.Parameters.AddWithValue("@iID_MaHangMau", Row["iID_MaHangMau"]);
                        bang.CmdParams.Parameters.AddWithValue("@sCongThuc", Row["sCongThuc"]);
                        bang.CmdParams.Parameters.AddWithValue("@sTenHang", Row["sTenHang"]);
                        bang.CmdParams.Parameters.AddWithValue("@iHeight", Row["iHeight"]);
                        bang.CmdParams.Parameters.AddWithValue("@sBackGround", Row["sBackGround"]);
                        bang.CmdParams.Parameters.AddWithValue("@sColor", Row["sColor"]);
                        bang.CmdParams.Parameters.AddWithValue("@iFontSize", Row["iFontSize"]);
                        bang.CmdParams.Parameters.AddWithValue("@bBold", Row["bBold"]);
                        bang.CmdParams.Parameters.AddWithValue("@bItalic", Row["bItalic"]);
                        bang.CmdParams.Parameters.AddWithValue("@bUnderline", Row["bUnderline"]);
                        bang.CmdParams.Parameters.AddWithValue("@bVisible", Row["bVisible"]);
                        bang.CmdParams.Parameters.AddWithValue("@bTongHangCon", Row["bTongHangCon"]);
                        bang.CmdParams.Parameters.AddWithValue("@iID_MaDonVi", Row["iID_MaDonVi"]);
                        bang.CmdParams.Parameters.AddWithValue("@bTinhTongTheoDonVi", Row["bTinhTongTheoDonVi"]);
                        bang.CmdParams.Parameters.AddWithValue("@bTruongGhiChu", Row["bTruongGhiChu"]);
                        bang.CmdParams.Parameters.AddWithValue("@iID_LoaiSanPham", Row["iID_LoaiSanPham"]);
                        bang.CmdParams.Parameters.AddWithValue("@bThemChiTieuCon", Row["bThemChiTieuCon"]);
                        bang.CmdParams.Parameters.AddWithValue("@bChuaLoaiSanPham", Row["bChuaLoaiSanPham"]);
                        bang.CmdParams.Parameters.AddWithValue("@bChuaSanPham", Row["bChuaSanPham"]);
                        bang.CmdParams.Parameters.AddWithValue("@bTinhTongTheoLoaiSanPham", Row["bTinhTongTheoLoaiSanPham"]);
                        bang.CmdParams.Parameters.AddWithValue("@iSTT", STT);
                        bang.Save();
                        if (Convert.ToBoolean(Row["bChuaLoaiSanPham"]) && (XemTheoDonVi == false))
                        {
                            DataTable dtLoaiSP = Connection.GetDataTable("SELECT * FROM BC_LoaiSanPham ORDER BY sTen");
                            for (k = 0; k < dtLoaiSP.Rows.Count; k++)
                            {
                                STT = STT + 1;
                                bang = new Bang("BC_Bang_Hang");
                                bang.MaNguoiDungSua = sUserName;
                                bang.IPSua = sIP;
                                bang.DuLieuMoi = true;
                                bang.CmdParams.Parameters.AddWithValue("@iID_MaBang", MaBang);
                                bang.CmdParams.Parameters.AddWithValue("@iID_MaHangMau_Cha", Row["iID_MaHangMau"]);
                                bang.CmdParams.Parameters.AddWithValue("@sTenHang", dtLoaiSP.Rows[k]["sTen"]);
                                bang.CmdParams.Parameters.AddWithValue("@bTinhTongTheoDonVi", 1);
                                bang.CmdParams.Parameters.AddWithValue("@iID_LoaiSanPham", dtLoaiSP.Rows[k]["iID_LoaiSanPham"]);
                                bang.CmdParams.Parameters.AddWithValue("@iSTT", STT);
                                bang.Save();
                            }
                            dtLoaiSP.Dispose();
                        }
                    }
                }
            }

            dtHangMau.Dispose();
            dtBangMau_HangMau.Dispose();
        }

        public static void TaoMangSTT(DataTable dtHangMau, DataTable dtBangMau_HangMau, String MaBangMau, int MaHangMauCha, int MaHangMauCha_ChungHangMauCon, ref List<int> arrSTT)
        {
            int i, j, MaHangMau, MaHangMau_ChungHangMauCon;
            DataRow Row;

            for (i = 0; i < dtHangMau.Rows.Count; i++)
            {
                Row = dtHangMau.Rows[i];
                if (MaHangMauCha_ChungHangMauCon == Convert.ToInt32(Row["iID_MaHangMau_Cha"]))
                {
                    MaHangMau = Convert.ToInt32(Row["iID_MaHangMau"]);
                    MaHangMau_ChungHangMauCon = MaHangMau;
                    if (Convert.ToInt32(Row["iID_MaHangMau_ChungHangMauCon"]) > 0)
                    {
                        MaHangMau_ChungHangMauCon = Convert.ToInt32(Row["iID_MaHangMau_ChungHangMauCon"]);
                    }
                    for (j = 0; j < dtBangMau_HangMau.Rows.Count; j++)
                    {
                        if (MaHangMau == Convert.ToInt32(dtBangMau_HangMau.Rows[j]["iID_MaHangMau"]) &&
                            MaHangMauCha == Convert.ToInt32(dtBangMau_HangMau.Rows[j]["iID_MaHangMau_Cha"]))
                        {
                            arrSTT.Add(j);
                        }
                    }
                    TaoMangSTT(dtHangMau, dtBangMau_HangMau, MaBangMau, MaHangMau, MaHangMau_ChungHangMauCon, ref arrSTT);
                }
            }
        }

        public static void XoaHangMau(Int32 MaHangMau)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM BC_HangMau WHERE iID_MaHangMau_Cha=@iID_MaHangMau";
            cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
            DataTable dtHangMau = Connection.GetDataTable(cmd);
            cmd.Dispose();
            for (int i = 0; i < dtHangMau.Rows.Count; i++)
            {
                XoaHangMau(Convert.ToInt32(dtHangMau.Rows[i]["iID_MaHangMau"]));
            }
            cmd = new SqlCommand();
            cmd.CommandText = "DELETE FROM BC_HangMau WHERE iID_MaHangMau=@iID_MaHangMau";
            cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
            Connection.UpdateDatabase(cmd);
            cmd.Dispose();
        }

        public static void XoaHangMauCon(String strMaHangMau)
        {
            String[] arr = strMaHangMau.Split(',');
            for (int j = 0; j < arr.Length; j++)
            {
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM BC_HangMau WHERE iID_MaHangMau_Cha=@iID_MaHangMau";
                cmd.Parameters.AddWithValue("@iID_MaHangMau", Convert.ToInt32 (arr[j]));
                DataTable dtHangMau = Connection.GetDataTable(cmd);
                cmd.Dispose();
                for (int i = 0; i < dtHangMau.Rows.Count; i++)
                {
                    XoaHangMau(Convert.ToInt32(dtHangMau.Rows[i]["iID_MaHangMau"]));
                }
            }
        }

        public static void XoaBangMauHangMauDonVi(String strMaHangMau)
        {
            String[] arr = strMaHangMau.Split(',');
            for (int j = 0; j < arr.Length; j++)
            {
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM BC_BangMau_HangMau WHERE iID_MaHangMau_Cha=@iID_MaHangMau";
                cmd.Parameters.AddWithValue("@iID_MaHangMau", Convert.ToInt32(arr[j]));
                DataTable dtHangMau = Connection.GetDataTable(cmd);
                cmd.Dispose();
                for (int i = 0; i < dtHangMau.Rows.Count; i++)
                {
                    cmd = new SqlCommand();
                    cmd.CommandText = "DELETE FROM BC_BangMau_HangMau_DonVi WHERE iID_MaBangMau_HangMau=@iID_MaBangMau_HangMau";
                    cmd.Parameters.AddWithValue("@iID_MaBangMau_HangMau", dtHangMau.Rows[i]["iID_MaBangMau_HangMau"]);
                    Connection.UpdateDatabase(cmd);
                    cmd.Dispose();

                    cmd = new SqlCommand();
                    cmd.CommandText = "DELETE FROM BC_BangMau_HangMau WHERE iID_MaBangMau_HangMau=@iID_MaBangMau_HangMau";
                    cmd.Parameters.AddWithValue("@iID_MaBangMau_HangMau", dtHangMau.Rows[i]["iID_MaBangMau_HangMau"]);
                    Connection.UpdateDatabase(cmd);
                    cmd.Dispose();
                }
            }
        }

        public static void XoaBangMauBangMau(Int32 MaHangMau, Int32 MaBangMau) {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM BC_BangMau_HangMau WHERE iID_MaBangMau=@iID_MaBangMau AND iID_MaHangMau_Cha=@iID_MaHangMau";
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
            DataTable dtBangMauHangMau = Connection.GetDataTable(cmd);
            cmd.Dispose();
            for (int i = 0; i < dtBangMauHangMau.Rows.Count; i++)
            {
                XoaBangMauBangMau(Convert.ToInt32(dtBangMauHangMau.Rows[i]["iID_MaHangMau"]), Convert.ToInt32(dtBangMauHangMau.Rows[i]["iID_MaBangMau"]));
            }
            //cmd = new SqlCommand();
            //cmd.CommandText = "DELETE FROM BC_BangMau_HangMau WHERE iID_MaHangMau=@iID_MaHangMau";
            //cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
            //Connection.UpdateDatabase(cmd);
            //cmd.Dispose();
            XoaBangMauHangMauDonVi(dtBangMauHangMau);
        }

        public static void XoaBangMauHangMauDonVi(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = "DELETE FROM BC_BangMau_HangMau_DonVi WHERE iID_MaBangMau_HangMau=@iID_MaBangMau_HangMau";
                cmd.Parameters.AddWithValue("@iID_MaBangMau_HangMau", dt.Rows[i]["iID_MaBangMau_HangMau"]);
                Connection.UpdateDatabase(cmd);
                cmd.Dispose();

                cmd = new SqlCommand();
                cmd.CommandText = "DELETE FROM BC_BangMau_HangMau WHERE iID_MaBangMau_HangMau=@iID_MaBangMau_HangMau";
                cmd.Parameters.AddWithValue("@iID_MaBangMau_HangMau", dt.Rows[i]["iID_MaBangMau_HangMau"]);
                Connection.UpdateDatabase(cmd);
                cmd.Dispose();
            }
        }

        public static void SuaLaiSTTCua_BangMau_HangMau_DonVi_SauKhiSapXep(String MaBangMau, String MaDonVi, String MaHangMau_Cha, List<String> listMaHangMau)
        {
            int i, BangMau_HangMau_DonVi_STTNhoNhat = -1;
            SqlCommand cmd;
            String SQL;
            List<String> listMaBangMau_HangMau_DonVi = new List<String>();
            for (i = 0; i < listMaHangMau.Count; i++)
            {
                //Tìm iID_MaBangMau_HangMau của MaHang
                SQL = "SELECT iID_MaBangMau_HangMau FROM BC_BangMau_HangMau WHERE iID_MaHangMau=@iID_MaHangMau AND " +
                                                                                 "iID_MaHangMau_Cha=@iID_MaHangMau_Cha AND " +
                                                                                 "iID_MaBangMau=@iID_MaBangMau";
                cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@iID_MaHangMau", listMaHangMau[i]);
                cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMau_Cha);
                cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                String iID_MaBangMau_HangMau = Connection.GetValueString(cmd, "");
                cmd.Dispose();

                //Tìm BangMau_HangMau_DonVi của MaHang
                SQL = "SELECT * FROM BC_BangMau_HangMau_DonVi WHERE iID_MaBangMau=@iID_MaBangMau AND iID_MaBangMau_HangMau=@iID_MaBangMau_HangMau AND iID_MaDonVi=@iID_MaDonVi";

                cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@iID_MaBangMau_HangMau", iID_MaBangMau_HangMau);
                cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                DataTable dt = Connection.GetDataTable(cmd);
                cmd.Dispose();

                if (BangMau_HangMau_DonVi_STTNhoNhat == -1 || BangMau_HangMau_DonVi_STTNhoNhat > Convert.ToInt32(dt.Rows[0]["iSTT"]))
                {
                    BangMau_HangMau_DonVi_STTNhoNhat = Convert.ToInt32(dt.Rows[0]["iSTT"]);
                }
                listMaBangMau_HangMau_DonVi.Add(Convert.ToString(dt.Rows[0]["iID_MaBangMau_HangMau_DonVi"]));
                dt.Dispose();
            }

            //Sắp xếp Bảng Hàng Mẫu
            for (i = 0; i < listMaBangMau_HangMau_DonVi.Count; i++)
            {
                cmd = new SqlCommand("UPDATE BC_BangMau_HangMau_DonVi SET iSTT=@iSTT WHERE iID_MaBangMau_HangMau_DonVi=@iID_MaBangMau_HangMau_DonVi");
                cmd.Parameters.AddWithValue("@iSTT", BangMau_HangMau_DonVi_STTNhoNhat + i);
                cmd.Parameters.AddWithValue("@iID_MaBangMau_HangMau_DonVi", listMaBangMau_HangMau_DonVi[i]);
                Connection.UpdateDatabase(cmd);
                cmd.Dispose();
            }
        }
    }
}