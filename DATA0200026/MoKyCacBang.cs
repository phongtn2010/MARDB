using System;
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
using System.Text.RegularExpressions;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using System.Threading;
using System.Text;

namespace DATA0200026
{
    public class MoKyCacBang
    {
        private string _strDSBagMau, _sNgayMoKy, _strPath, _MaDonVi, _UserName, _IP;
        private int _LoaiBaoCao;
        public MoKyCacBang(string strDSBangMau, string sNgayMoKy, int LoaiBaoCao, String strPath, String MaDonVi, String sUserName, String sIP)
        {
            _strDSBagMau = strDSBangMau;
            _sNgayMoKy = sNgayMoKy;
            _LoaiBaoCao = LoaiBaoCao;
            _strPath = strPath;
            _MaDonVi = MaDonVi;
            _UserName = sUserName;
            _IP = sIP;
        }

        public void funcMoKyCacBang()
        {
            String MaBangMau;
            if (_strDSBagMau != "")
            {
                String[] arrNgay = _sNgayMoKy.Split('/');
                DateTime Ngay;
                if (arrNgay.Length == 1)
                {
                    Ngay = new DateTime(Convert.ToInt16(arrNgay[0]), 1, 1);
                }
                else if (arrNgay.Length == 2)
                {
                    Ngay = new DateTime(Convert.ToInt16(arrNgay[1]), Convert.ToInt16(arrNgay[0]), 1);
                }
                else
                {
                    Ngay = new DateTime(Convert.ToInt16(arrNgay[2]), Convert.ToInt16(arrNgay[1]), Convert.ToInt16(arrNgay[0]));
                }
                SqlCommand cmd;
                String MaDonVi, MaBang;
                int i, j;
                String[] arr = _strDSBagMau.Split(',');
                if (_MaDonVi == "" || _MaDonVi == null)
                {
                    cmd = new SqlCommand("SELECT * FROM BC_DonVi");
                }
                else {
                    cmd = new SqlCommand("SELECT * FROM BC_DonVi WHERE iID_MaDonVi=@iID_MaDonVi");
                    cmd.Parameters.AddWithValue("@iID_MaDonVi", _MaDonVi);
                }
                DataTable dtDonVi = Connection.GetDataTable(cmd);
                cmd.Dispose();
                for (i = 0; i < arr.Length - 1; i++)
                {
                    MaBangMau = arr[i];
                    MaDonVi = "-1";
                    cmd = new SqlCommand("SELECT bXemTheoDonVi FROM BC_BangMau WHERE iID_MaBangMau=@iID_MaBangMau");
                    cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                    if (Convert.ToBoolean(Connection.GetValue(cmd, false)))
                    {
                        for (j = 0; j < dtDonVi.Rows.Count; j++)
                        {
                            MaDonVi = Convert.ToString(dtDonVi.Rows[j]["iID_MaDonVi"]);
                            MaBang = BangModels.TaoBangMoi(MaBangMau, MaDonVi, _LoaiBaoCao, Ngay, _strPath, _UserName, _IP);

                            DongMoKyBaoCaoModels.GhiNhatKyDongMoKy(MaBang, 0, "", _UserName, _IP);
                        }
                    }
                    else
                    {
                        MaBang = BangModels.TaoBangMoi(MaBangMau, MaDonVi, _LoaiBaoCao, Ngay, _strPath, _UserName, _IP);
                        DongMoKyBaoCaoModels.GhiNhatKyDongMoKy(MaBang, 0, "", _UserName, _IP);
                    }
                    cmd.Dispose();

                }
                dtDonVi.Dispose();
            }
        }

        public static DataTable LaySoBangTrongKy(String NgayMoKy, String LoaiBaoCao, String MaPhongBan, String MaDonVi)
        {
            SqlCommand cmd;
            String[] arrNgay = NgayMoKy.Split('/');
            DateTime Ngay;
            if (arrNgay.Length == 1)
            {
                Ngay = new DateTime(Convert.ToInt16(arrNgay[0]), 1, 1);
            }
            else if (arrNgay.Length == 2)
            {
                Ngay = new DateTime(Convert.ToInt16(arrNgay[1]), Convert.ToInt16(arrNgay[0]), 1);
            }
            else
            {
                Ngay = new DateTime(Convert.ToInt16(arrNgay[2]), Convert.ToInt16(arrNgay[1]), Convert.ToInt16(arrNgay[0]));
            }

            String DK;
            cmd = new SqlCommand();
            DK = "iLoaiBaoCao=@iLoaiBaoCao AND iID_MaPhongBan=@iID_MaPhongBan ";
            cmd.Parameters.AddWithValue("@iLoaiBaoCao", LoaiBaoCao);
            cmd.Parameters.AddWithValue("@iID_MaPhongBan", MaPhongBan);
            if (MaDonVi != "" && MaDonVi != null)
            {
                DK += "AND iID_MaDonVi=@iID_MaDonVi";
                cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
            }
            switch (Convert.ToInt32(LoaiBaoCao))
            {
                case 1:
                    DK += " AND (CONVERT(varchar(10),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", Ngay.ToString("yyyy/MM/dd"));
                    break;

                case 2:
                    DK += " AND (CONVERT(varchar(7),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", Ngay.ToString("yyyy/MM"));
                    break;

                case 3:
                    DK += " AND (CONVERT(varchar(7),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", Ngay.ToString("yyyy/MM"));
                    break;
                case 4:
                    DK += " AND (CONVERT(varchar(4),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", Ngay.ToString("yyyy"));
                    break;
                case 5:
                    DK += " AND (CONVERT(varchar(7),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", Ngay.ToString("yyyy/MM"));
                    break;
            }
            cmd.CommandText = String.Format("SELECT * FROM BC_Bang WHERE {0} ORDER BY iSTT, sTenBang", DK);
            DataTable dtBang = Connection.GetDataTable(cmd);
            cmd.Dispose();

            return dtBang;
        }

        public static int CheckSoBangDaChot(String NgayMoKy, String LoaiBaoCao, String MaPhongBan, String MaDonVi)
        {
            int SoLuongBang = 0;
            SqlCommand cmd;
            String[] arrNgay = NgayMoKy.Split('/');
            DateTime Ngay;
            if (arrNgay.Length == 1)
            {
                Ngay = new DateTime(Convert.ToInt16(arrNgay[0]), 1, 1);
            }
            else if (arrNgay.Length == 2)
            {
                Ngay = new DateTime(Convert.ToInt16(arrNgay[1]), Convert.ToInt16(arrNgay[0]), 1);
            }
            else
            {
                Ngay = new DateTime(Convert.ToInt16(arrNgay[2]), Convert.ToInt16(arrNgay[1]), Convert.ToInt16(arrNgay[0]));
            }

            String DK;
            cmd = new SqlCommand();
            DK = "bDaChot = 1 AND iLoaiBaoCao=@iLoaiBaoCao AND iID_MaPhongBan=@iID_MaPhongBan ";
            cmd.Parameters.AddWithValue("@iLoaiBaoCao", LoaiBaoCao);
            cmd.Parameters.AddWithValue("@iID_MaPhongBan", MaPhongBan);
            if (MaDonVi != "" && MaDonVi != null)
            {
                DK += "AND iID_MaDonVi=@iID_MaDonVi";
                cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
            }
            switch (Convert.ToInt32(LoaiBaoCao))
            {
                case 1:
                    DK += " AND (CONVERT(varchar(10),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", Ngay.ToString("yyyy/MM/dd"));
                    break;

                case 2:
                    DK += " AND (CONVERT(varchar(7),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", Ngay.ToString("yyyy/MM"));
                    break;

                case 3:
                    DK += " AND (CONVERT(varchar(7),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", Ngay.ToString("yyyy/MM"));
                    break;
                case 4:
                    DK += " AND (CONVERT(varchar(4),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", Ngay.ToString("yyyy"));
                    break;
                case 5:
                    DK += " AND (CONVERT(varchar(7),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", Ngay.ToString("yyyy/MM"));
                    break;
            }
            cmd.CommandText = String.Format("SELECT * FROM BC_Bang WHERE {0} ORDER BY iSTT, sTenBang", DK);
            DataTable dtBang = Connection.GetDataTable(cmd);
            cmd.Dispose();

            SoLuongBang = dtBang.Rows.Count;
            dtBang.Dispose();

            return SoLuongBang;
        }

        public static int CheckMoKy(String NgayMoKy, String LoaiBaoCao, String MaPhongBan, String MaDonVi)
        {
            int SoLuongBang = 0;
            SqlCommand cmd;
            String[] arrNgay = NgayMoKy.Split('/');
            DateTime Ngay;
            if (arrNgay.Length == 1)
            {
                Ngay = new DateTime(Convert.ToInt16(arrNgay[0]), 1, 1);
            }
            else if (arrNgay.Length == 2)
            {
                Ngay = new DateTime(Convert.ToInt16(arrNgay[1]), Convert.ToInt16(arrNgay[0]), 1);
            }
            else
            {
                Ngay = new DateTime(Convert.ToInt16(arrNgay[2]), Convert.ToInt16(arrNgay[1]), Convert.ToInt16(arrNgay[0]));
            }

            String DK;
            cmd = new SqlCommand();
            DK = "bDaChot = 0 AND iLoaiBaoCao=@iLoaiBaoCao AND iID_MaPhongBan=@iID_MaPhongBan ";
            cmd.Parameters.AddWithValue("@iLoaiBaoCao", LoaiBaoCao);
            cmd.Parameters.AddWithValue("@iID_MaPhongBan", MaPhongBan);
            if (MaDonVi != "" && MaDonVi != null)
            {
                DK += "AND iID_MaDonVi=@iID_MaDonVi";
                cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
            }
            switch (Convert.ToInt32(LoaiBaoCao))
            {
                case 1:
                    DK += " AND (CONVERT(varchar(10),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", Ngay.ToString("yyyy/MM/dd"));
                    break;

                case 2:
                    DK += " AND (CONVERT(varchar(7),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", Ngay.ToString("yyyy/MM"));
                    break;

                case 3:
                    DK += " AND (CONVERT(varchar(7),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", Ngay.ToString("yyyy/MM"));
                    break;
                case 4:
                    DK += " AND (CONVERT(varchar(4),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", Ngay.ToString("yyyy"));
                    break;
                case 5:
                    DK += " AND (CONVERT(varchar(7),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", Ngay.ToString("yyyy/MM"));
                    break;
            }
            cmd.CommandText = String.Format("SELECT * FROM BC_Bang WHERE {0} ORDER BY iSTT, sTenBang", DK);
            DataTable dtBang = Connection.GetDataTable(cmd);
            cmd.Dispose();

            SoLuongBang = dtBang.Rows.Count;
            dtBang.Dispose();

            return SoLuongBang;
        }

        public static int CheckDongKy(String NgayMoKy, String LoaiBaoCao, String MaPhongBan, String MaDonVi)
        {
            int SoLuongBang = 0;
            SqlCommand cmd;
            String[] arrNgay = NgayMoKy.Split('/');
            DateTime Ngay;
            if (arrNgay.Length == 1)
            {
                Ngay = new DateTime(Convert.ToInt16(arrNgay[0]), 1, 1);
            }
            else if (arrNgay.Length == 2)
            {
                Ngay = new DateTime(Convert.ToInt16(arrNgay[1]), Convert.ToInt16(arrNgay[0]), 1);
            }
            else
            {
                Ngay = new DateTime(Convert.ToInt16(arrNgay[2]), Convert.ToInt16(arrNgay[1]), Convert.ToInt16(arrNgay[0]));
            }

            String DK;
            cmd = new SqlCommand();
            DK = "bDaChot = 1 AND iID_LoaiDongMoKy = 1 AND iLoaiBaoCao=@iLoaiBaoCao AND iID_MaPhongBan=@iID_MaPhongBan ";
            cmd.Parameters.AddWithValue("@iLoaiBaoCao", LoaiBaoCao);
            cmd.Parameters.AddWithValue("@iID_MaPhongBan", MaPhongBan);
            if (MaDonVi != "" && MaDonVi != null)
            {
                DK += "AND iID_MaDonVi=@iID_MaDonVi";
                cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
            }
            switch (Convert.ToInt32(LoaiBaoCao))
            {
                case 1:
                    DK += " AND (CONVERT(varchar(10),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", Ngay.ToString("yyyy/MM/dd"));
                    break;

                case 2:
                    DK += " AND (CONVERT(varchar(7),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", Ngay.ToString("yyyy/MM"));
                    break;

                case 3:
                    DK += " AND (CONVERT(varchar(7),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", Ngay.ToString("yyyy/MM"));
                    break;
                case 4:
                    DK += " AND (CONVERT(varchar(4),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", Ngay.ToString("yyyy"));
                    break;
                case 5:
                    DK += " AND (CONVERT(varchar(7),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", Ngay.ToString("yyyy/MM"));
                    break;
            }
            cmd.CommandText = String.Format("SELECT * FROM BC_Bang WHERE {0} ORDER BY iSTT, sTenBang", DK);
            DataTable dtBang = Connection.GetDataTable(cmd);
            cmd.Dispose();

            SoLuongBang = dtBang.Rows.Count;
            dtBang.Dispose();

            return SoLuongBang;
        }

        public static int CheckDongKyVinhVien(String NgayMoKy, String LoaiBaoCao, String MaPhongBan, String MaDonVi)
        {
            int SoLuongBang = 0;
            SqlCommand cmd;
            String[] arrNgay = NgayMoKy.Split('/');
            DateTime Ngay;
            if (arrNgay.Length == 1)
            {
                Ngay = new DateTime(Convert.ToInt16(arrNgay[0]), 1, 1);
            }
            else if (arrNgay.Length == 2)
            {
                Ngay = new DateTime(Convert.ToInt16(arrNgay[1]), Convert.ToInt16(arrNgay[0]), 1);
            }
            else
            {
                Ngay = new DateTime(Convert.ToInt16(arrNgay[2]), Convert.ToInt16(arrNgay[1]), Convert.ToInt16(arrNgay[0]));
            }

            String DK;
            cmd = new SqlCommand();
            DK = "bDaChot = 1 AND iID_LoaiDongMoKy = 2 AND iLoaiBaoCao=@iLoaiBaoCao AND iID_MaPhongBan=@iID_MaPhongBan ";
            cmd.Parameters.AddWithValue("@iLoaiBaoCao", LoaiBaoCao);
            cmd.Parameters.AddWithValue("@iID_MaPhongBan", MaPhongBan);
            if (MaDonVi != "" && MaDonVi != null)
            {
                DK += "AND iID_MaDonVi=@iID_MaDonVi";
                cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
            }
            switch (Convert.ToInt32(LoaiBaoCao))
            {
                case 1:
                    DK += " AND (CONVERT(varchar(10),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", Ngay.ToString("yyyy/MM/dd"));
                    break;

                case 2:
                    DK += " AND (CONVERT(varchar(7),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", Ngay.ToString("yyyy/MM"));
                    break;

                case 3:
                    DK += " AND (CONVERT(varchar(7),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", Ngay.ToString("yyyy/MM"));
                    break;
                case 4:
                    DK += " AND (CONVERT(varchar(4),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", Ngay.ToString("yyyy"));
                    break;
                case 5:
                    DK += " AND (CONVERT(varchar(7),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", Ngay.ToString("yyyy/MM"));
                    break;
            }
            cmd.CommandText = String.Format("SELECT * FROM BC_Bang WHERE {0} ORDER BY iSTT, sTenBang", DK);
            DataTable dtBang = Connection.GetDataTable(cmd);
            cmd.Dispose();

            SoLuongBang = dtBang.Rows.Count;
            dtBang.Dispose();

            return SoLuongBang;
        }

        public static int CheckSoBangTrongKy(String NgayMoKy, String LoaiBaoCao, String MaPhongBan, String MaDonVi)
        {
            int SoLuongBang = 0;
            SqlCommand cmd;
            String[] arrNgay = NgayMoKy.Split('/');
            DateTime Ngay;
            if (arrNgay.Length == 1)
            {
                Ngay = new DateTime(Convert.ToInt16(arrNgay[0]), 1, 1);
            }
            else if (arrNgay.Length == 2)
            {
                Ngay = new DateTime(Convert.ToInt16(arrNgay[1]), Convert.ToInt16(arrNgay[0]), 1);
            }
            else
            {
                Ngay = new DateTime(Convert.ToInt16(arrNgay[2]), Convert.ToInt16(arrNgay[1]), Convert.ToInt16(arrNgay[0]));
            }

            String DK;
            cmd = new SqlCommand();
            DK = "iLoaiBaoCao=@iLoaiBaoCao AND iID_MaPhongBan=@iID_MaPhongBan ";
            cmd.Parameters.AddWithValue("@iLoaiBaoCao", LoaiBaoCao);
            cmd.Parameters.AddWithValue("@iID_MaPhongBan", MaPhongBan);
            if (MaDonVi != "" && MaDonVi != null)
            {
                DK += "AND iID_MaDonVi=@iID_MaDonVi";
                cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
            }
            switch (Convert.ToInt32(LoaiBaoCao))
            {
                case 1:
                    DK += " AND (CONVERT(varchar(10),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", Ngay.ToString("yyyy/MM/dd"));
                    break;

                case 2:
                    DK += " AND (CONVERT(varchar(7),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", Ngay.ToString("yyyy/MM"));
                    break;

                case 3:
                    DK += " AND (CONVERT(varchar(7),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", Ngay.ToString("yyyy/MM"));
                    break;
                case 4:
                    DK += " AND (CONVERT(varchar(4),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", Ngay.ToString("yyyy"));
                    break;
                case 5:
                    DK += " AND (CONVERT(varchar(7),dNgayBaoCao,111)=@sNgayBaoCao)";
                    cmd.Parameters.AddWithValue("@sNgayBaoCao", Ngay.ToString("yyyy/MM"));
                    break;
            }
            cmd.CommandText = String.Format("SELECT * FROM BC_Bang WHERE {0} ORDER BY iSTT, sTenBang", DK);
            DataTable dtBang = Connection.GetDataTable(cmd);
            cmd.Dispose();

            SoLuongBang = dtBang.Rows.Count;
            dtBang.Dispose();

            return SoLuongBang;
        }
    }
}