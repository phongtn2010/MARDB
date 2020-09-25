using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DomainModel
{
    public class BaoMat
    {
        public static Boolean ChoPhepCaNguoiTaoXem = false;
        public static Boolean Demo = true;
        public const string KyTuTach = ",";

        public static Boolean KiemTraNhomNguoiDungQuanTri(String MaNhomNguoiDung)
        {
            return MaNhomNguoiDung.Length == 1;
        }

        public static Boolean KiemTraNguoiDungQuanTri(String MaNguoiDung)
        {
            String MaNhomNguoiDung = LayMaNhomNguoiDung(MaNguoiDung);
            return KiemTraNhomNguoiDungQuanTri(MaNhomNguoiDung);
        }
        /// <summary>
        /// Nhóm người dùng !='' và bHoatDong=true
        /// </summary>
        /// <param name="MaNguoiDung"></param>
        /// <returns></returns>
        public static Boolean KiemTraNguoiDungDuocPhepHoatDong(string MaNguoiDung)
        {
            String MaNhomNguoiDung = LayMaNhomNguoiDung(MaNguoiDung);
            if (MaNhomNguoiDung.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string DanhSachChucNangCam(string MaNguoiDung, string sTenBang)
        {
            string strR = "";
            string SQL;
            SqlCommand cmd;

            SQL = String.Format("SELECT * FROM PQ_DanhMucBang WHERE sTenBang='{0}'", sTenBang);
            DataTable dtBang = Connection.GetDataTable(SQL);
            if (dtBang.Rows.Count == 0)
            {
                dtBang.Dispose();
                return "";
            }
            else
            {
                DataRow Row = dtBang.Rows[0];
                if ((Boolean)Row["bXem"] == false)
                {
                    strR += (strR == "") ? "" : KyTuTach;
                    strR += "Detail";
                }
                if ((Boolean)Row["bThem"] == false)
                {
                    strR += (strR == "") ? "" : KyTuTach;
                    strR += "Create";
                }
                if ((Boolean)Row["bXoa"] == false)
                {
                    strR += (strR == "") ? "" : KyTuTach;
                    strR += "Delete";
                }
                if ((Boolean)Row["bSua"] == false)
                {
                    strR += (strR == "") ? "" : KyTuTach;
                    strR += "Edit";
                }
                if ((Boolean)Row["bChiaSe"] == false)
                {
                    strR += (strR == "") ? "" : KyTuTach;
                    strR += "Share";
                }
                if ((Boolean)Row["bGiaoPhuTrach"] == false)
                {
                    strR += (strR == "") ? "" : KyTuTach;
                    strR += "Responsibility";
                }
            }

            String MaNhomNguoiDung = LayMaNhomNguoiDung(MaNguoiDung);
            SQL = "SELECT DISTINCT sChucNang " +
                  "FROM PQ_NhomNguoiDung_Luat INNER JOIN PQ_Bang_ChucNangCam ON PQ_NhomNguoiDung_Luat.iID_MaLuat=PQ_Bang_ChucNangCam.iID_MaLuat " +
                  "WHERE sTenBang=@sTenBang AND iID_MaNhomNguoiDung=@iID_MaNhomNguoiDung";
            cmd = new SqlCommand();
            cmd.CommandText = SQL;
            cmd.Parameters.AddWithValue("@iID_MaNhomNguoiDung", MaNhomNguoiDung);
            cmd.Parameters.AddWithValue("@sTenBang", sTenBang);
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();
            int i;

            for (i = 0; i <= dt.Rows.Count - 1; i++)
            {
                strR += (strR == "") ? dt.Rows[i][0].ToString() : KyTuTach + dt.Rows[i][0].ToString();
            }
            dt.Dispose();
            return strR.ToUpper();
        }

        public static string DanhSachTruongCam(string MaNguoiDung, string sTenBang)
        {
            string strR = "";
            string SQL;

            SQL = String.Format("SELECT Count(*) FROM PQ_DanhMucBang WHERE sTenBang='{0}'", sTenBang);
            if (Convert.ToInt16(Connection.GetValue(SQL, 0)) == 0)
            {
                return "";
            }

            SqlCommand cmd;
            String MaNhomNguoiDung = LayMaNhomNguoiDung(MaNguoiDung);

            SQL = "SELECT DISTINCT sTenTruong " +
                  "FROM PQ_NhomNguoiDung_Luat INNER JOIN PQ_Bang_TruongCam ON PQ_NhomNguoiDung_Luat.iID_MaLuat=PQ_Bang_TruongCam.iID_MaLuat " +
                  "WHERE sTenBang=@sTenBang AND iID_MaNhomNguoiDung=@iID_MaNhomNguoiDung";
            cmd = new SqlCommand();
            cmd.CommandText = SQL;
            cmd.Parameters.AddWithValue("@iID_MaNhomNguoiDung", MaNhomNguoiDung);
            cmd.Parameters.AddWithValue("@sTenBang", sTenBang);
            DataTable dt = Connection.GetDataTable(cmd);
            int i;

            for (i = 0; i <= dt.Rows.Count - 1; i++)
            {
                strR += (strR == "") ? dt.Rows[i][0].ToString() : KyTuTach + dt.Rows[i][0].ToString();
            }
            dt.Dispose();
            return strR.ToUpper();
        }

        public static string DanhSachTruongMenuItemCam(string MaNguoiDung)
        {
            string strR = "";
            string SQL;

            SQL = "SELECT DISTINCT iID_MaMenuItem " +
                  "FROM PQ_NguoiDung_Luat INNER JOIN PQ_MenuItem_Cam ON (PQ_NguoiDung_Luat.iID_MaLuat=PQ_MenuItem_Cam.iID_MaLuat) " +
                  "WHERE PQ_NguoiDung_Luat.sID_MaNguoiDung=@sID_MaNguoiDung ";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = SQL;
            cmd.Parameters.AddWithValue("@sID_MaNguoiDung", MaNguoiDung);
            DataTable dt = Connection.GetDataTable(cmd);
            int i;

            for (i = 0; i <= dt.Rows.Count - 1; i++)
            {
                strR += (strR == "") ? dt.Rows[i][0].ToString() : KyTuTach + dt.Rows[i][0].ToString();
            }
            dt.Dispose();
            return strR.ToUpper();
        }

        public static Boolean ChoPhepNhap(string sTen, string sDanhSachCam)
        {
            if (String.IsNullOrEmpty(sTen))
            {
                return true;
            }
            if (String.IsNullOrEmpty(sDanhSachCam))
            {
                return true;
            }
            string sTG1 = KyTuTach + sDanhSachCam.ToUpper() + KyTuTach;
            string sTG2 = KyTuTach + sTen.ToUpper() + KyTuTach;
            if (sTG1.IndexOf(sTG2) >= 0)
            {
                return false;
            }
            return true;
        }

        public static Boolean ChoPhepLamViec(string MaNguoiDung, string sTenBang, string sTenChucNang)
        {
            if(KiemTraNguoiDungDuocPhepHoatDong(MaNguoiDung))
            {
                String sDanhSachChucNangCam = BaoMat.DanhSachChucNangCam(MaNguoiDung, sTenBang);
                return BaoMat.ChoPhepNhap(sTenChucNang, sDanhSachChucNangCam);
            }
            else
            {
                return false;
            }
            
        }

        public static Boolean ChoPhepLamViec(string sTenChucNang, string sDanhSachChucNangCam)
        {
            return BaoMat.ChoPhepNhap(sTenChucNang, sDanhSachChucNangCam);
        }

        public static bool ChoPhepLamViec_Cms(string MaNguoiDung, string sTenBang, string sTenChucNang)
        {
            if (LayDoiTuongNhomNguoiDung(MaNguoiDung) == 1)
            {
                string sDanhSachCam = DanhSachChucNangCam(MaNguoiDung, sTenBang);
                return ChoPhepNhap(sTenChucNang, sDanhSachCam);
            }
            return false;
        }

        public static DataTable dtDanhSachLuat(String MaNhomNguoiDung)
        {
            SqlCommand cmd = new SqlCommand();
            String SQL = "SELECT iID_MaLuat,sTen FROM PQ_Luat ORDER BY sTen";
            //int csD = MaNhomNguoiDung.IndexOf("-");
            //int csC = MaNhomNguoiDung.LastIndexOf("-");
            //if (csD != csC && KiemTraNhomNguoiDungQuanTri(MaNhomNguoiDung) == false)
            //{   
            //    String MaNhomNguoiDungCha = MaNhomNguoiDung.Substring(0, csC);
            //    cmd.Parameters.AddWithValue("@MaNhomNguoiDungCha", MaNhomNguoiDungCha);
            //    SQL = "SELECT PQ_Luat.iID_MaLuat, PQ_Luat.sTen " +
            //          "FROM PQ_Luat INNER JOIN PQ_NhomNguoiDung_Luat ON PQ_Luat.iID_MaLuat=PQ_NhomNguoiDung_Luat.iID_MaLuat " +
            //          "WHERE iID_MaNhomNguoiDung=@MaNhomNguoiDungCha " +
            //          "ORDER BY PQ_Luat.sTen";
            //}
            cmd.CommandText = SQL;
            return Connection.GetDataTable(cmd);
        }

        public static int LayDoiTuongNhomNguoiDung(string MaNguoiDung)
        {
            int num = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@MaNguoiDung", MaNguoiDung);
            cmd.CommandText = "SELECT iDoiTuongNguoiDung FROM QT_NguoiDung WHERE sID_MaNguoiDung=@MaNguoiDung ";
            num = Convert.ToInt32(Connection.GetValue(cmd, 0, 0));
            cmd.Dispose();
            return num;
        }

        public static String LayMaNhomNguoiDung(String MaNguoiDung)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.Parameters.AddWithValue("@MaNguoiDung", MaNguoiDung);
            cmd.CommandText = "SELECT iID_MaNhomNguoiDung " +
                              "FROM QT_NguoiDung " +
                              "WHERE sID_MaNguoiDung=@MaNguoiDung AND bHoatDong=1";
            return Connection.GetValue(cmd, "").ToString();
        }

        public static DataTable Lay_dtNhomNguoiDung(String MaNguoiDung)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.Parameters.AddWithValue("@MaNguoiDung", MaNguoiDung);
            cmd.CommandText = "SELECT QT_NhomNguoiDung.* " +
                              "FROM QT_NguoiDung INNER JOIN QT_NhomNguoiDung ON QT_NguoiDung.iID_MaNhomNguoiDung=QT_NhomNguoiDung.iID_MaNhomNguoiDung " +
                              "WHERE sID_MaNguoiDung=@MaNguoiDung ";
            return Connection.GetDataTable(cmd);
        }

        public static Boolean KiemTraQuyenXemTin(String MaNguoiDung, String TenBang, String TenTruongKhoa, Object GiaTriKhoa)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = String.Format("SELECT * FROM {0} WHERE {1}=@{1}", TenBang, TenTruongKhoa);
            cmd.Parameters.AddWithValue("@" + TenTruongKhoa, GiaTriKhoa);
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();
            if (dt == null || dt.Rows.Count != 1)
            {
                dt.Dispose();
                return false;
            }
            //Kiểm tra xem người dùng có được phép làm việc với tin hay không?
            String MaNhomNguoiDung = BaoMat.LayMaNhomNguoiDung(MaNguoiDung);
            String MaNhomNguoiDung_ = MaNhomNguoiDung + "-";
            Boolean bPublic = Convert.ToBoolean(dt.Rows[0]["bPublic"]);
            String MaNhomNguoiDung_Public = String.Format("{0}", dt.Rows[0]["iID_MaNhomNguoiDung_Public"]);
            String MaNhomNguoiDung_Public_ = MaNhomNguoiDung_Public + "-";
            String MaNhomNguoiDung_DuocGiao = String.Format("{0}", dt.Rows[0]["iID_MaNhomNguoiDung_DuocGiao"]);
            String MaNhomNguoiDung_DuocGiao_ = MaNhomNguoiDung_DuocGiao + "-";
            String MaNguoiDung_DuocGiao = String.Format("{0}", dt.Rows[0]["sID_MaNguoiDung_DuocGiao"]);
            if ((bPublic && MaNhomNguoiDung_.StartsWith(MaNhomNguoiDung_Public_)) ||
                (MaNhomNguoiDung_DuocGiao.StartsWith(MaNhomNguoiDung_)) ||
                (MaNhomNguoiDung_DuocGiao == MaNhomNguoiDung && (MaNguoiDung_DuocGiao == MaNguoiDung || MaNguoiDung_DuocGiao == ""))
               )
            {
                dt.Dispose();
                return true;
            }
            dt.Dispose();
            return false;
        }

        public static String DieuKienXemTin(String MaNguoiDung, String TenBang)
        {
            String vR = "1=1";
            String SQL = String.Format("SELECT * FROM PQ_DanhMucBang WHERE sTenBang='{0}'", TenBang);
            DataTable dt = Connection.GetDataTable(SQL);

            if (dt.Rows.Count >= 1)
            {
                String DK = "";

                if (Convert.ToBoolean(dt.Rows[0]["bChiaSe"]))
                {
                    DK = " ({3}.bPublic=1 AND {3}.iID_MaNhomNguoiDung_Public=LEFT('{1}',Len({3}.iID_MaNhomNguoiDung_Public))) ";
                }
                if (Convert.ToBoolean(dt.Rows[0]["bGiaoPhuTrach"]))
                {
                    if (DK != "")
                    {
                        DK += " OR ";
                    }
                    DK += " (LEFT({3}.iID_MaNhomNguoiDung_DuocGiao,{0})='{1}-')";
                    DK += " OR ";
                    DK += " ({3}.iID_MaNhomNguoiDung_DuocGiao='{1}' AND ({3}.sID_MaNguoiDung_DuocGiao IS NULL OR {3}.sID_MaNguoiDung_DuocGiao='' OR {3}.sID_MaNguoiDung_DuocGiao='{2}'))";
                    if (ChoPhepCaNguoiTaoXem)
                    {
                        DK += " OR ";
                        DK += " ({3}.sID_MaNguoiDungTao='{2}') ";
                    }
                }
                if (DK != "")
                {
                    DK = "(" + DK + ")";
                    String MaNhomNguoiDung = BaoMat.LayMaNhomNguoiDung(MaNguoiDung);
                    vR = String.Format(DK, MaNhomNguoiDung.Length + 1, MaNhomNguoiDung, MaNguoiDung, TenBang);
                }
            }
            return vR;
        }

        public static String DieuKienXemTheoToChuc(String MaToChuc, String TenBang)
        {
            String vR = "1=1";
            String SQL = String.Format("SELECT * FROM PQ_DanhMucBang WHERE sTenBang='{0}'", TenBang);
            DataTable dt = Connection.GetDataTable(SQL);

            if (dt.Rows.Count >= 1)
            {
                String DK = "";

                if (Convert.ToBoolean(dt.Rows[0]["bGiaoPhuTrach"]))
                {
                    if (DK != "")
                    {
                        DK += " OR ";
                    }
                    DK += " (LEFT({2}.iID_MaNhomNguoiDung_DuocGiao+'-',{0})='{1}-')";
                }
                if (DK != "")
                {
                    DK = "(" + DK + ")";
                    vR = String.Format(DK, MaToChuc.Length + 1, MaToChuc, TenBang);
                }
            }
            return vR;
        }
    }
}
