using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel;
using DomainModel.Controls;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace DATA0200026
{
    public class CPQ_NGUOIDUNG
    {
        private const int PBKDF2IterCount = 1000; // default for Rfc2898DeriveBytes
        private const int PBKDF2SubkeyLength = 256 / 8; // 256 bits
        private const int SaltSize = 128 / 8; // 128 bits

        public static string Encode(string value)
        {
            var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(value ?? "");
            return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
        }

        public static string HashPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            // Produce a version 0 (see comment above) text hash.
            byte[] salt;
            byte[] subkey;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, SaltSize, PBKDF2IterCount))
            {
                salt = deriveBytes.Salt;
                subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
            }

            var outputBytes = new byte[1 + SaltSize + PBKDF2SubkeyLength];
            Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, PBKDF2SubkeyLength);
            return Convert.ToBase64String(outputBytes);
        }

        public static DataTable Get_NguoiDung_All(string sMaNhom, string sTieuDe, string _FromDate, string _ToDate, int Trang, int SoBanGhi)
        {
            DataTable vR;

            string sDk = "1=1 ";
            SqlCommand cmd = new SqlCommand();
            if (!string.IsNullOrEmpty(sMaNhom))
            {
                sDk += " AND iID_MaNhomNguoiDung = @iID_MaNhomNguoiDung";
                cmd.Parameters.AddWithValue("@iID_MaNhomNguoiDung", sMaNhom);
            }
            if (!string.IsNullOrEmpty(sTieuDe))
            {
                sDk += " AND ((sID_MaNguoiDung LIKE N'%" + sTieuDe + "%') OR (sHoTen LIKE N'%" + sTieuDe + "%') OR (sEmail LIKE N'%" + sTieuDe + "%') OR (sMobile LIKE N'%" + sTieuDe + "%') OR (sDiaChi LIKE N'%" + sTieuDe + "%')) ";
            }
            if (!(string.IsNullOrEmpty(_FromDate) || (_FromDate == "")))
            {
                sDk += " AND dNgayTao >= @_FromDate";
                cmd.Parameters.AddWithValue("@_FromDate", CommonFunction.LayNgayTuXau(_FromDate));
            }
            if (!(string.IsNullOrEmpty(_ToDate) || (_ToDate == "")))
            {
                sDk += " AND dNgayTao <= @_ToDate";
                cmd.Parameters.AddWithValue("@_ToDate", CommonFunction.LayNgayTuXau(_ToDate));
            }
            string SQL = string.Format("SELECT * FROM QT_NGUOIDUNG WHERE {0}", sDk);
            cmd.CommandText = SQL;
            vR = CommonFunction.dtData(cmd, "sID_MaNguoiDung ASC", Trang, SoBanGhi, 0);
            cmd.Dispose();

            return vR;
        }

        //SELECT * FROM QT_NGUOIDUNG WHERE iID_MaNhomNguoiDung='1-54' ORDER BY sID_MANGUOIDUNG
        public static int Get_NguoiDung_Count(string sMaNhom, string sTieuDe, string _FromDate, string _ToDate)
        {
            int vR = 0;

            string sDk = "1=1 ";
            SqlCommand cmd = new SqlCommand();
            if (!string.IsNullOrEmpty(sMaNhom))
            {
                sDk += " AND iID_MaNhomNguoiDung = @iID_MaNhomNguoiDung";
                cmd.Parameters.AddWithValue("@iID_MaNhomNguoiDung", sMaNhom);
            }
            if (!string.IsNullOrEmpty(sTieuDe))
            {
                sDk += " AND ((sID_MaNguoiDung LIKE N'%" + sTieuDe + "%') OR (sHoTen LIKE N'%" + sTieuDe + "%') OR (sEmail LIKE N'%" + sTieuDe + "%') OR (sMobile LIKE N'%" + sTieuDe + "%') OR (sDiaChi LIKE N'%" + sTieuDe + "%')) ";
            }
            if (!(string.IsNullOrEmpty(_FromDate) || (_FromDate == "")))
            {
                sDk += " AND dNgayTao >= @_FromDate";
                cmd.Parameters.AddWithValue("@_FromDate", CommonFunction.LayNgayTuXau(_FromDate));
            }
            if (!(string.IsNullOrEmpty(_ToDate) || (_ToDate == "")))
            {
                sDk += " AND dNgayTao <= @_ToDate";
                cmd.Parameters.AddWithValue("@_ToDate", CommonFunction.LayNgayTuXau(_ToDate));
            }
            string SQL = string.Format("SELECT Count(*) FROM QT_NGUOIDUNG WHERE {0}", sDk);
            cmd.CommandText = SQL;
            vR = Convert.ToInt32(Connection.GetValue(cmd, 0, 0));
            cmd.Dispose();

            return vR;
        }

        public static String LayMaNhomNguoiDung(String MaNguoiDung)
        {
            String vR = "";

            String SQL = "SELECT IID_MANHOMNGUOIDUNG FROM QT_NGUOIDUNG WHERE sID_MANGUOIDUNG=@sID_MANGUOIDUNG AND ITRANGTHAI = 1";

            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@sID_MANGUOIDUNG", MaNguoiDung);
            vR = Convert.ToString(Connection.GetValue(cmd, "", CThamSo.iKetNoi));
            cmd.Dispose();

            return vR;
        }

        public static DataTable HienThi_Autocomplete(string sTieuDe)
        {
            DataTable vR;

            SqlCommand cmd;
            string SQL = "SELECT TOP 20 * FROM QT_NGUOIDUNG WHERE UPPER(sID_MANGUOIDUNG) LIKE '%" + sTieuDe.ToUpper() + "%' AND ITRANGTHAI = 1";
            cmd = new SqlCommand(SQL);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();

            return vR;
        }

        public static DataTable Get_Like_NguoiDung(string sTerm)
        {
            SqlCommand command = new SqlCommand();
            command = new SqlCommand("SELECT TOP 20 * FROM QT_NguoiDung WHERE sHoTen LIKE '" + sTerm + "%' ORDER BY sHoTen");
            DataTable dataTable = Connection.GetDataTable(command, 0);
            command.Dispose();
            return dataTable;
        }

        public static int Check_NguoiDung(string UserName)
        {
            int vR = 0;

            DataTable dt = Get_One_Table(UserName);
            if (dt.Rows.Count > 0)
            {
                vR = 1;
            }
            dt.Dispose();
            return vR;
        }

        public static int Insert_Full(String sID_MaNguoiDung, String iID_MaNguoiDung_Cha, String iID_MaNhomNguoiDung, String sHoTen, String sEmail, string sMobile, string sDiaChi, int iTrangThai, int iLoai, string sNgaySinh, int iID_MaKhachHang = 0, int iGioiTinh = 0)
        {
            int vR = -1;

            try
            {
                String SQL = "INSERT INTO QT_NGUOIDUNG(sID_MaNguoiDung, iID_MaKhachHang, iID_MaNguoiDung_Cha, iID_MaNhomNguoiDung, sHoTen, sEmail, sMobile, sDiaChi, iLoai, iTrangThai, dNgaySinh, iGioiTinh) VALUES(@sID_MaNguoiDung, @iID_MaKhachHang, @iID_MaNguoiDung_Cha, @iID_MaNhomNguoiDung, @sHoTen, @sEmail, @sMobile, @sDiaChi, @iLoai, @iTrangThai, @dNgaySinh, @iGioiTinh)";

                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@sID_MaNguoiDung", sID_MaNguoiDung);
                cmd.Parameters.AddWithValue("@iID_MaKhachHang", iID_MaKhachHang);
                cmd.Parameters.AddWithValue("@iID_MaNguoiDung_Cha", iID_MaNguoiDung_Cha);
                cmd.Parameters.AddWithValue("@iID_MaNhomNguoiDung", iID_MaNhomNguoiDung);
                cmd.Parameters.AddWithValue("@sHoTen", sHoTen);
                cmd.Parameters.AddWithValue("@sEmail", sEmail);
                cmd.Parameters.AddWithValue("@sMobile", sMobile);
                cmd.Parameters.AddWithValue("@iGioiTinh", iGioiTinh);
                if (!string.IsNullOrEmpty(sDiaChi))
                    cmd.Parameters.AddWithValue("@sDiaChi", sDiaChi);
                else
                    cmd.Parameters.AddWithValue("@sDiaChi", "");
                if (!string.IsNullOrEmpty(sNgaySinh))
                    cmd.Parameters.AddWithValue("@dNgaySinh", CommonFunction.LayNgayTuXau(sNgaySinh));
                else
                    cmd.Parameters.AddWithValue("@dNgaySinh", DateTime.Now);
                cmd.Parameters.AddWithValue("@iTrangThai", iTrangThai);
                cmd.Parameters.AddWithValue("@iLoai", iLoai);
                vR = Connection.UpdateDatabase(cmd, CThamSo.iKetNoi);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static int Insert(String sID_MaNguoiDung, String iID_MaNhomNguoiDung, String sHoTen, String sEmail, int iTrangThai)
        {
            int vR = -1;

            try
            {
                String SQL = "INSERT INTO QT_NGUOIDUNG(sID_MaNguoiDung, iID_MaNhomNguoiDung, sHoTen, sEmail, iTrangThai) VALUES(@sID_MaNguoiDung, @iID_MaNhomNguoiDung, @sHoTen, @sEmail, @iTrangThai)";

                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@sID_MaNguoiDung", sID_MaNguoiDung);
                cmd.Parameters.AddWithValue("@iID_MaNhomNguoiDung", iID_MaNhomNguoiDung);
                cmd.Parameters.AddWithValue("@sHoTen", sHoTen);
                cmd.Parameters.AddWithValue("@sEmail", sEmail);
                cmd.Parameters.AddWithValue("@iTrangThai", iTrangThai);
                vR = Connection.UpdateDatabase(cmd, CThamSo.iKetNoi);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }
        public static int Update(String iID_MaNhomNguoiDung, String iID_MaKhachHang, String sID_MANGUOIDUNG, String sHoTen, String sEmail, int iTrangThai)
        {
            int vR = -1;

            try
            {
                String SQL = "UPDATE QT_NGUOIDUNG SET iID_MaNhomNguoiDung=@iID_MaNhomNguoiDung, iID_MaKhachHang=@iID_MaKhachHang, sHoTen=@sHoTen, sEmail=@sEmail, iTrangThai=@iTrangThai WHERE sID_MaNguoiDung = @id";

                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@iID_MaNhomNguoiDung", iID_MaNhomNguoiDung);
                cmd.Parameters.AddWithValue("@iID_MaKhachHang", iID_MaKhachHang);
                cmd.Parameters.AddWithValue("@sHoTen", sHoTen);
                cmd.Parameters.AddWithValue("@sEmail", sEmail);
                cmd.Parameters.AddWithValue("@iTrangThai", iTrangThai);
                cmd.Parameters.AddWithValue("@id", sID_MANGUOIDUNG);
                vR = Connection.UpdateDatabase(cmd, CThamSo.iKetNoi);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }
        public static int Update_iBalance(String sID_MaNguoiDung, int iBalance)
        {
            int vR = -1;

            try
            {
                String SQL = "UPDATE QT_NGUOIDUNG SET iBalance=@iBalance WHERE sID_MaNguoiDung = @id";

                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@iBalance", iBalance);
                cmd.Parameters.AddWithValue("@id", sID_MaNguoiDung);
                vR = Connection.UpdateDatabase(cmd, CThamSo.iKetNoi);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }
        public static int Delete(String id)
        {
            int vR = -1;

            try
            {
                SqlCommand cmd;

                String SQL = "DELETE FROM QT_NGUOIDUNG WHERE sID_MANGUOIDUNG = @id";
                cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@id", id);
                vR = Connection.UpdateDatabase(cmd, CThamSo.iKetNoi);
                cmd.Dispose();

                String SQL_M = "DELETE FROM AspNetUsers WHERE UserName = @id";
                cmd = new SqlCommand(SQL_M);
                cmd.Parameters.AddWithValue("@id", id);
                vR = Connection.UpdateDatabase(cmd, CThamSo.iKetNoi);
                cmd.Dispose();

                vR = 1;
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }
        public static int Update_NhomNguoiDung(String id, String IID_MANHOMNGUOIDUNG)
        {
            int vR = -1;

            try
            {
                String SQL = "UPDATE QT_NGUOIDUNG SET IID_MANHOMNGUOIDUNG=@IID_MANHOMNGUOIDUNG WHERE sID_MANGUOIDUNG = @id";

                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@IID_MANHOMNGUOIDUNG", IID_MANHOMNGUOIDUNG);
                cmd.Parameters.AddWithValue("@id", id);
                vR = Connection.UpdateDatabase(cmd, CThamSo.iKetNoi);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static DataTable Get_Table()
        {
            DataTable vR = null;

            string SQL = "SELECT * FROM QT_NGUOIDUNG ORDER BY sID_MANGUOIDUNG";
            SqlCommand cmd = new SqlCommand(SQL);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();

            return vR;
        }

        public static DataTable Get_Table_KhachHang()
        {
            DataTable vR = null;

            string SQL = "SELECT * FROM QT_NGUOIDUNG WHERE iID_MaNhomNguoiDung='1-54' ORDER BY sID_MANGUOIDUNG";
            SqlCommand cmd = new SqlCommand(SQL);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();

            return vR;
        }

        public static DataTable Get_One_Table(String sID_MaNguoiDung)
        {
            DataTable vR = null;

            string SQL = "SELECT * FROM QT_NGUOIDUNG WHERE sID_MANGUOIDUNG=@sID_MANGUOIDUNG";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@sID_MANGUOIDUNG", sID_MaNguoiDung);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();

            return vR;
        }

        public static DataTable Get_Table_NguoiDung_Nhom(String iID_MaNhomNguoiDung)
        {
            DataTable vR = null;

            string SQL = "SELECT * FROM QT_NGUOIDUNG WHERE IID_MANHOMNGUOIDUNG=@IID_MANHOMNGUOIDUNG AND ITRANGTHAI = 1 ORDER BY sID_MANGUOIDUNG";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@IID_MANHOMNGUOIDUNG", iID_MaNhomNguoiDung);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();

            return vR;
        }

        public static DataTable Get_Table_NguoiDung_Auto(String sID_MANGUOIDUNG)
        {
            DataTable vR = null;

            string SQL = "SELECT * FROM QT_NGUOIDUNG WHERE sID_MANGUOIDUNG LIKE @sID_MANGUOIDUNG ORDER BY sID_MANGUOIDUNG";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@sID_MANGUOIDUNG", "%" + sID_MANGUOIDUNG + "%");
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();

            return vR;
        }

        public static long Get_MaKhachHang(String sID_MaNguoiDung)
        {
            long vR = 0;
            DataTable dt = Get_One_Table(sID_MaNguoiDung);
            if (dt.Rows.Count > 0)
            {
                vR = Convert.ToInt64(dt.Rows[0]["iID_MaKhachHang"]);
            }
            dt.Dispose();
            return vR;
        }

        public static string Get_MaNhomNguoiDung(String sID_MaNguoiDung)
        {
            string vR = "";
            DataTable dt = Get_One_Table(sID_MaNguoiDung);
            if (dt.Rows.Count > 0)
            {
                vR = Convert.ToString(dt.Rows[0]["iID_MaNhomNguoiDung"]);
            }
            dt.Dispose();
            return vR;
        }

        public static int Get_MaKhoHang_User(string sUser)
        {
            int vR = -1;
            DataTable table = Get_One_Table(sUser);
            if (table.Rows.Count > 0)
            {
                vR = Convert.ToInt32(table.Rows[0]["iID_MaKhoHang"]);
            }
            table.Dispose();
            return vR;
        }
    }
}
