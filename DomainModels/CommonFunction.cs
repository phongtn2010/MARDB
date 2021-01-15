using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Web.Mvc;
using System.IO;
using System.Web.UI;
using System.Collections.Specialized;
using System.Net.NetworkInformation;
using System.Net;
using System.Web;
using DomainModel.Controls;
using System.Net.Mail;
using Microsoft.SqlServer.Server;
using System.Linq;

namespace DomainModel
{
    public class CommonFunction
    {
        public static int VisitorOnline = 0;
        public static string folderPath = @"c:\temp";
        public static string sSoSauDauPhay = "5";
        public static void writeLog(string value)
        {
            FileStream fs = new FileStream(folderPath + "\\FileName.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter m_streamWriter = new StreamWriter(fs);
            m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
            m_streamWriter.WriteLine(" ===> " + DateTime.Now + ": " + value + "\n");
            m_streamWriter.Flush();
            m_streamWriter.Close();
        }
        public static bool IsNumeric(object ValueToCheck)
        {
            double Dummy = 0;
            return double.TryParse(Convert.ToString(ValueToCheck), System.Globalization.NumberStyles.Any, null, out Dummy);
        }

        public static bool IsNumber(string pValue)
        {
            foreach (Char c in pValue)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// iC0 = 12:00 thì giá trị trả lại là Giờ lệch +7 so với giờ Việt Nam
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime ConvertFromUnixTimestamp(long timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }
      
        /// <summary>
        /// iC0 = 12:00 thì giá trị trả lại là Giờ Việt Nam
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime ConvertFromUnixTimestamp_VietNam(long timestamp)
        {
            return ConvertFromUnixTimestamp(timestamp).AddHours(7);
        }

        public static long ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Convert.ToInt64(Math.Floor(diff.TotalMilliseconds));
        }

        /// <summary>
        /// Date = 12:00 thì giá trị trả lại là iC0 của 05:00 (trừ -07 GMT)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static long ConvertToUnixTimestamp_Second(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Convert.ToInt64(Math.Floor(diff.TotalSeconds));
        }

        /// <summary>
        /// Date = 12:00 thì giá trị trả lại là iC0 của 12:00
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static long ConvertToUnixTimestamp_Second_VietNam(DateTime date)
        {
            return ConvertToUnixTimestamp_Second(date.AddHours(-7));
        }

        public static bool SoSanh2GiaTri(char Loai, object Value1, object Value2)
        {
            Boolean vR = false;
            if (Value1 == null && Value2 == null)
            {
                vR = true;
            }
            else if (Value1 != null && Value2 != null)
            {
                switch (Loai)
                {
                    case 'r':
                        if (Convert.ToDouble(Value1) == Convert.ToDouble(Value2))
                        {
                            vR = true;
                        }
                        break;

                    case 'd':
                        if (Convert.ToDateTime(Value1) == Convert.ToDateTime(Value2))
                        {
                            vR = true;
                        }
                        break;

                    default:
                        if (Convert.ToString(Value1) == Convert.ToString(Value2))
                        {
                            vR = true;
                        }
                        break;
                }
            }
            return vR;
        }

        public static string RenderPartialViewToString(string viewName, Controller controller)
        {
            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        public static String LayTenDanhMuc(String MaDanhMuc, int KetNoi = 0)
        {
            String vR = "";
            if (String.IsNullOrEmpty(MaDanhMuc) == false)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT sTen FROM DC_DanhMuc WHERE iID_MaDanhMuc=@iID_MaDanhMuc";
                cmd.Parameters.AddWithValue("@iID_MaDanhMuc", MaDanhMuc);
                vR = (string)(Connection.GetValue(cmd, "", KetNoi));
            }
            return vR;
        }

        public static Object LayTruong(String TenBang, String TenTruongKhoa, String GiaTriKhoa, String TenTruongLayGiaTri, int KetNoi = 0)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = String.Format("SELECT {0} FROM {1} WHERE {2}=@{2}", TenTruongLayGiaTri, TenBang, TenTruongKhoa);
            cmd.Parameters.AddWithValue("@" + TenTruongKhoa, GiaTriKhoa);
            return Connection.GetValue(cmd, null, KetNoi);
        }

        public static DataTable Lay_dtDanhMuc(String TenLoaiDanhMuc, int KetNoi = 0)
        {
            String SQL = String.Format("SELECT iID_MaDanhMuc, DC_DanhMuc.sTen FROM DC_LoaiDanhMuc INNER JOIN DC_DanhMuc ON DC_DanhMuc.iID_MaLoaiDanhMuc = DC_LoaiDanhMuc.iID_MaLoaiDanhMuc WHERE DC_DanhMuc.bHoatDong=1 AND DC_LoaiDanhMuc.sTenBang=N'{0}' ORDER BY iSTT", TenLoaiDanhMuc);
            return Connection.GetDataTable(SQL, KetNoi);
        }

        public static String LayNgayTuXau_YYYY_MM_DD(String sValue)
        {
            String vR = "";
            try
            {
                if (String.IsNullOrEmpty(sValue) == false)
                {
                    string[] strArray = sValue.Split(new char[] { '/' });
                    string sNam = Convert.ToString(strArray[2]);
                    string sThang = Convert.ToString(strArray[1]);
                    string sNgay = Convert.ToString(strArray[0]);
                    vR = sNam + "-" + sThang + "-" + sNgay;
                }
                else
                {
                    vR = "";
                }
            }
            catch
            {
                vR = "";
            }

            return vR;
        }
        public static String LayXauNgay(DateTime d)
        {
            return d.ToString("dd/MM/yyyy");
        }
                
        public static Object LayNgayTuXau(String strD)
        {
            try
            {
                String strTG1 = (string)strD;
                String[] tgA = new String[3];
                int year = 1900, month = 1, day = 1;
                if (strTG1.IndexOf("/") > 0)
                {
                    tgA = strTG1.Split('/');
                    year = Convert.ToInt32(tgA[2]);
                    month = Convert.ToInt32(tgA[1]);
                    day = Convert.ToInt32(tgA[0]);
                }
                else if (strTG1.IndexOf(":") > 0)
                {
                    tgA = strTG1.Split(':');
                    year = Convert.ToInt32(tgA[0]);
                    month = Convert.ToInt32(tgA[1]);
                    day = Convert.ToInt32(tgA[2]);
                }

                return new DateTime(year, month, day);
            }
            catch
            {
            }
            return null;
        }

        public static String LayNgayTuXau_YYYYMMDD(String strD)
        {
            try
            {
                String vR = "";
                String strTG1 = (string)strD;
                String[] tgA = new String[3];
                int year = 1900, month = 1, day = 1;
                if (strTG1.IndexOf("/") > 0)
                {
                    tgA = strTG1.Split('/');
                    year = Convert.ToInt32(tgA[2]);
                    month = Convert.ToInt32(tgA[1]);
                    day = Convert.ToInt32(tgA[0]);
                }
                else if (strTG1.IndexOf(":") > 0)
                {
                    tgA = strTG1.Split(':');
                    year = Convert.ToInt32(tgA[0]);
                    month = Convert.ToInt32(tgA[1]);
                    day = Convert.ToInt32(tgA[2]);
                }

                DateTime dDay = new DateTime(year, month, day);

                vR = dDay.ToString("yyyy-MM-dd");
                return vR;
            }
            catch
            {
            }
            return null;
        }

        public static Object LayNgayTuXau_MMDDYYYY(String strD)
        {
            try
            {
                String strTG1 = (string)strD;
                String[] tgA = new String[3];
                int year = 1900, month = 1, day = 1;
                if (strTG1.IndexOf("/") > 0)
                {
                    tgA = strTG1.Split('/');
                    year = Convert.ToInt32(tgA[2]);
                    day = Convert.ToInt32(tgA[1]);
                    month = Convert.ToInt32(tgA[0]);
                }
                else if (strTG1.IndexOf(":") > 0)
                {
                    tgA = strTG1.Split(':');
                    year = Convert.ToInt32(tgA[0]);
                    day = Convert.ToInt32(tgA[1]);
                    month = Convert.ToInt32(tgA[2]);
                }

                return new DateTime(year, month, day);
            }
            catch
            {
            }
            return null;
        }

        public static Object LayNgayTuXau_Gio(String strD, String strH)
        {
            try
            {
                String strTG1 = (string)strD;
                String[] tgA;
                if (strTG1.IndexOf('/') > -1)
                {
                    tgA = strTG1.Split('/');
                }
                else if(strTG1.IndexOf('.') > -1)
                {
                    tgA = strTG1.Split('.');
                }
                else
                {
                    tgA = strTG1.Split('-');
                }

                int year = Convert.ToInt32(tgA[2]);
                int month = Convert.ToInt32(tgA[1]);
                int day = Convert.ToInt32(tgA[0]);

                String strTGH = Convert.ToString(strH);
                String[] tgH = strTGH.Split(':');
                int _hour = Convert.ToInt32(tgH[0]);
                int _minute = Convert.ToInt32(tgH[1]);

                return new DateTime(year, month, day, _hour, _minute, 00);
            }
            catch
            {
            }
            return null;
        }

        private static String _strXauChu = "dmMyhHs";
        public static object LayNgayTuXau(string strValue, string strFormat = "ddMMyyyy")
        {
            Object vR = null;
            strValue = strValue.Trim();
            if (String.IsNullOrEmpty(strValue))
            {
                return null;
            }
            strValue = strValue.Replace(" ", "");
            strValue = strValue.Replace("/", "");
            strValue = strValue.Replace("-", "");
            strValue = strValue.Replace("\\", "");
            strValue = strValue.Replace(":", "");
            strValue = strValue.Replace("_", "");
            DateTime HienTai = DateTime.Now;
            int i, Ngay = HienTai.Day, Thang = HienTai.Month, Nam = HienTai.Year, Gio = 0, Phut = 0, Giay = 0;

            String strTG1 = "", strTG2;
            int GT = 0;
            Boolean CoGiaTri = false;
            for (i = 0; i < strFormat.Length && i < strValue.Length; i++)
            {
                String KyTu = Convert.ToString(strFormat[i]);
                if (_strXauChu.IndexOf(KyTu) >= 0)
                {
                    if (strTG1.StartsWith(KyTu))
                    {
                        strTG1 += Convert.ToString(strFormat[i]);
                        strTG2 = Convert.ToString(strValue[i]);
                        if (CommonFunction.IsNumeric(strTG2))
                        {
                            GT = GT * 10 + Convert.ToInt32(strTG2);
                        }
                    }
                    else
                    {
                        strTG1 = Convert.ToString(strFormat[i]);
                        strTG2 = Convert.ToString(strValue[i]);
                        if (CommonFunction.IsNumeric(strTG2))
                        {
                            GT = Convert.ToInt32(strTG2);
                        }
                        else
                        {
                            GT = 0;
                        }
                    }
                }
                else
                {
                    strTG1 = "";
                    GT = 0;
                }
                switch (strTG1)
                {
                    case "dd":
                    case "d":
                        if (GT >= 0)
                        {
                            Ngay = GT;
                            CoGiaTri = true;
                        }
                        break;
                    case "MM":
                    case "M":
                        if (GT >= 0)
                        {
                            Thang = GT;
                            CoGiaTri = true;
                        }
                        break;
                    case "y":
                    case "yy":
                    case "yyyy":
                        if (GT >= 0)
                        {
                            Nam = GT;
                            if (Nam < 30)
                            {
                                Nam = Nam + 2000;
                            }
                            else if (Nam < 100)
                            {
                                Nam = Nam + 1900;
                            }
                            CoGiaTri = true;
                        }
                        break;
                    case "HH":
                    case "H":
                        if (GT >= 0)
                        {
                            Gio = GT;
                            CoGiaTri = true;
                        }
                        break;
                    case "mm":
                    case "m":
                        if (GT >= 0)
                        {
                            Phut = GT;
                            CoGiaTri = true;
                        }
                        break;
                    case "ss":
                    case "s":
                        if (GT >= 0)
                        {
                            Giay = GT;
                            CoGiaTri = true;
                        }
                        break;
                }
            }
            if (CoGiaTri)
            {
                try
                {
                    vR = new DateTime(Nam, Thang, Ngay, Gio, Phut, Giay);
                }
                catch
                {
                }
            }
            return vR;
        }

        public static Object ChuyenXauSangNgay(Object strNgay)
        {
            if (strNgay != null && strNgay != DBNull.Value && String.IsNullOrEmpty(Convert.ToString(strNgay)) == false)
            {
                return CDate.LayNgayTuXau(Convert.ToString(strNgay), "yyyy:MM:dd:HH:mm:ss");
            }
            return null;
        }

        public static Object ChuyenXauSangNgay(Object strNgay, String Format)
        {
            if (strNgay != null && strNgay != DBNull.Value && String.IsNullOrEmpty(Convert.ToString(strNgay)) == false)
            {
                return CDate.LayNgayTuXau(Convert.ToString(strNgay), Format);
            }
            return null;
        }

        public static String ChuyenNgaySangXau(Object Ngay)
        {
            String vR = "";
            if (Ngay != null && Ngay != DBNull.Value && String.IsNullOrEmpty(Convert.ToString(Ngay)) == false)
            {
                vR = Convert.ToDateTime(Ngay).ToString("yyyy:MM:dd:HH:mm:ss");
            }
            return vR;
        }

        public static String ChuyenNgaySangXau(Object Ngay, String Format)
        {
            String vR = "";
            if (Ngay != null && Ngay != DBNull.Value && String.IsNullOrEmpty(Convert.ToString(Ngay)) == false)
            {
                vR = Convert.ToDateTime(Ngay).ToString(Format);
            }
            return vR;
        }
        
        /// <summary>
        /// Lấy bảng CSDL đã được phân trang
        /// </summary>
        /// <param name="SQL"></param>
        /// <param name="sOrder">Danh sách các trường sắp xếp</param>
        /// <param name="page">page>=1</param>
        /// <param name="numrecord">numrecord>=1</param>
        /// <returns></returns>
        public static DataTable dtData(String SQL, string sOrder, int page, int numrecord, int KetNoi = 0)
        {
            //SQL=SQL.ToUpper ();
            String strSQL = SQL;
            if (page > 0 && numrecord > 0)
            {
                int start = 0, end = 0;
                start = (page - 1) * numrecord + 1;
                end = page * numrecord;

                int cs = SQL.IndexOf(" FROM ");
                SQL = SQL.Substring(0, cs) + " ,ROW_NUMBER() OVER(ORDER BY {0}) AS rownum " + SQL.Substring(cs);
                SQL = String.Format(SQL, sOrder);
                strSQL = String.Format("SELECT * FROM ({0}) AS BangTam WHERE {1}<=rownum and rownum<={2}", SQL, start, end);
            }
            DataTable dt = Connection.GetDataTable(strSQL, KetNoi);
            int i, j;
            DateTime tg;
            string TenCotMoi;
            if (NgonNgu.MaNgonNgu != "")
            {
                for (j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Columns[j].ColumnName.StartsWith("d"))
                    {
                        TenCotMoi = NgonNgu.MaNgonNgu + dt.Columns[j].ColumnName;
                        dt.Columns.Add(TenCotMoi, typeof(string));
                        for (i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            try
                            {
                                tg = (DateTime)(dt.Rows[i][j]);
                                dt.Rows[i][TenCotMoi] = LayXauNgay(tg);
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
            return dt;
        }

        public static DataTable dtData(SqlCommand cmd, string sOrder, int page, int numrecord, int KetNoi = 0)
        {
            String strTG = cmd.CommandText;
            String SQL = cmd.CommandText;
            String strSQL = SQL;
            if (page > 0 && numrecord > 0)
            {
                int start = 0, end = 0;
                start = (page - 1) * numrecord + 1;
                end = page * numrecord;

                int cs = SQL.IndexOf(" FROM ");
                SQL = SQL.Substring(0, cs) + " ,ROW_NUMBER() OVER(ORDER BY {0}) AS rownum " + SQL.Substring(cs);
                SQL = String.Format(SQL, sOrder);
                strSQL = String.Format("SELECT * FROM ({0}) AS BangTam WHERE {1}<=rownum and rownum<={2}", SQL, start, end);
            }
            else
            {
                if (!String.IsNullOrEmpty(sOrder))
                {
                    strSQL = String.Format("{0} ORDER BY {1}", SQL, sOrder);
                }
            }
            cmd.CommandText = strSQL;
            DataTable dt = Connection.GetDataTable(cmd, KetNoi);
            int i, j;
            DateTime tg;
            string TenCotMoi;
            if (dt != null && NgonNgu.MaNgonNgu != "")
            {
                for (j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Columns[j].ColumnName.StartsWith("d"))
                    {
                        TenCotMoi = NgonNgu.MaDate + dt.Columns[j].ColumnName;
                        dt.Columns.Add(TenCotMoi, typeof(string));
                        for (i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            try
                            {
                                tg = (DateTime)(dt.Rows[i][j]);
                                dt.Rows[i][TenCotMoi] = LayXauNgay(tg);
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
            cmd.CommandText = strTG;
            return dt;
        }

        public static List<T> dtDataList<T>(SqlCommand cmd, string sOrder, int page, int numrecord, int KetNoi = 0) where T : class, new()
        {
            List<T> res = new List<T>();

            String strTG = cmd.CommandText;
            String SQL = cmd.CommandText;
            String strSQL = SQL;
            if (page > 0 && numrecord > 0)
            {
                int start = 0, end = 0;
                start = (page - 1) * numrecord + 1;
                end = page * numrecord;

                int cs = SQL.IndexOf(" FROM ");
                SQL = SQL.Substring(0, cs) + " ,ROW_NUMBER() OVER(ORDER BY {0}) AS rownum " + SQL.Substring(cs);
                SQL = String.Format(SQL, sOrder);
                strSQL = String.Format("SELECT * FROM ({0}) AS BangTam WHERE {1}<=rownum and rownum<={2}", SQL, start, end);
            }
            else
            {
                if (!String.IsNullOrEmpty(sOrder))
                {
                    strSQL = String.Format("{0} ORDER BY {1}", SQL, sOrder);
                }
            }
            cmd.CommandText = strSQL;

            res = Connection.GetList<T>(cmd, KetNoi);

            return res;
        }

        public static int SoBanGhi(String SQL, int KetNoi = 0)
        {
            //SQL = SQL.ToUpper();
            int cs = SQL.IndexOf(" FROM ");
            SQL = "SELECT Count(*) " + SQL.Substring(cs);
            return (int)(Connection.GetValue(SQL, 0, KetNoi));
        }

        public static int SoBanGhi(SqlCommand cmd, int KetNoi = 0)
        {
            int vR = 0;
            String strTG = cmd.CommandText;
            String SQL = cmd.CommandText;
            int cs = SQL.IndexOf(" FROM ");
            SQL = "SELECT Count(*) " + SQL.Substring(cs);
            cmd.CommandText = SQL;
            vR = (int)(Connection.GetValue(cmd, 0, KetNoi));
            cmd.CommandText = strTG;
            return vR;
        }

        public static string LayTruongKhoa(String TenBang, int KetNoi = 0)
        {
            return ThongTinBangCSDL.LayTruongKhoaCuaBang(TenBang, KetNoi);
        }

        public static NameValueCollection LayDataTheoDataRow(DataRow dr)
        {
            NameValueCollection data = new NameValueCollection();

            if (dr != null)
            {
                DataTable dt = dr.Table;
                DateTime tg;
                string TenCotMoi, GiaTriMoi;

                for (int i = 0; i <= dt.Columns.Count - 1; i++)
                {
                    data.Add(dt.Columns[i].ColumnName, dr[i].ToString());
                    if (NgonNgu.MaNgonNgu != "" && dt.Columns[i].ColumnName.StartsWith("d"))
                    {
                        try
                        {
                            tg = (DateTime)(dr[i]);
                            TenCotMoi = NgonNgu.MaDate + dt.Columns[i].ColumnName;
                            GiaTriMoi = tg.ToString("dd/MM/yyyy");
                            data.Add(TenCotMoi, GiaTriMoi);
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return data;
        }

        public static string ValueToString(Object v)
        {
            return Convert.ToString(v);
        }

        public static Boolean KiemTraTrungMa(string TenBang, string TenTruong, object GiaTri, int KetNoi = 0)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = String.Format("SELECT count(*) FROM {0} WHERE {1}=@{1}", TenBang, TenTruong);
            cmd.Parameters.AddWithValue("@" + TenTruong, GiaTri);
            return Convert.ToInt16(Connection.GetValue(cmd, 0, KetNoi)) >= 1;
        }

        public static String LayGiaTriMacDinh(string MaGiaTri, int KetNoi = 0)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = String.Format("SELECT GiaTri FROM HT_GiaTriMacDinh WHERE MaGiaTri=@MaGiaTri");
            cmd.Parameters.AddWithValue("@MaGiaTri", MaGiaTri);
            return Connection.GetValueString(cmd, "", KetNoi);
        }

        public static String DinhDangSo(Object So, int SoSauDauPhay)
        {
            String KQ = "";
            String SoTien;
            SoTien = Convert.ToString(So);
            int j, SoAm;
            if (So != null)
            {
                SoAm = SoTien.IndexOf("-");
                if (SoAm >= 0)
                {
                    SoTien = SoTien.Replace("-", "");
                }
                int i = SoTien.IndexOf(".");
                int d = 0;
                string PhanDu = "";
                if (i >= 0)
                {
                    PhanDu = SoTien.Substring(i + 1, SoTien.Length - i - 1);
                    SoTien = SoTien.Substring(0, i);
                }
                int Len = SoTien.Length;
                KQ = SoTien;
                if (SoTien.Length > 3)
                {
                    for (j = Len; j > 1; j--)
                    {
                        d++;
                        if (d % 3 == 0)
                        {
                            if (Globals.KieuSoVietNam)
                            {
                                SoTien = SoTien.Insert(j - 1, ".");
                            }
                            else
                            {
                                SoTien = SoTien.Insert(j - 1, ",");
                            }
                        }
                    }
                }
                if (SoSauDauPhay >= 0)
                {
                    String tg = "";
                    for (i = 0; i < SoSauDauPhay; i++)
                    {
                        if (i < PhanDu.Length)
                        {
                            tg = tg + PhanDu[i];
                        }
                        else
                        {
                            tg = tg + "0";
                        }
                    }
                    PhanDu = tg;
                }
                if (PhanDu != "")
                {
                    if (Globals.KieuSoVietNam)
                    {
                        SoTien = SoTien + "," + PhanDu;
                    }
                    else
                    {
                        SoTien = SoTien + "." + PhanDu;
                    }
                }
                KQ = SoTien;
                if (SoAm >= 0)
                {
                    KQ = "-" + KQ;
                }
            }
            return KQ;

        }

        public static String DinhDangSo(Object So)
        {
            return DinhDangSo(So, -2);
        }

        public static string DinhDangSo(String strSo)
        {
            String orgfnum = strSo;
            Boolean flagneg = false;

            if (strSo.StartsWith("-"))
            {
                flagneg = true;
                strSo = strSo.Substring(1, strSo.Length - 1);
            }

            String[] psplit = strSo.Split('.'); //sửa dấu "." thành dấu ","

            String cnum = psplit[0], parr = "";
            int j = cnum.Length, d = 0;

            for (int i = j - 1; i >= 0; i--)
            {
                d = d + 1;
                parr = cnum[i] + parr;
                if (d % 3 == 0 && i > 0)
                {
                    if (Globals.KieuSoVietNam)
                    {
                        parr = "." + parr;
                    }
                    else
                    {
                        parr = "," + parr;
                    }
                }
            }
            strSo = parr;

            if (orgfnum.IndexOf(".") != -1)
            {
                if (Globals.KieuSoVietNam)
                {
                    strSo += "," + psplit[1];
                }
                else
                {
                    strSo += "." + psplit[1];
                }
            }

            if (flagneg == true)
            {
                strSo = "-" + strSo;
            }

            return strSo;
        }

        /// <summary>
        /// Định dạng số
        /// </summary>
        /// <param name="So"></param>
        /// <param name="HienThiGiaTri_0">True hiển thị giá trị 0 và ngược lại </param>
        /// <returns></returns>
        public static String DinhDangSo(Object So, Boolean HienThiGiaTri_0 = true)
        {
            return DinhDangSo(So, 0, HienThiGiaTri_0);
        }

        public static String DinhDangSo(Object So, int SoSauDauPhay, Boolean HienThiGiaTri_0 = false)
        {
            String KQ = "";
            String SoTien;
            Boolean SoAm = false;
            SoTien = CommonFunction.ValueToString(So);
            if (IsNumeric(SoTien))
            {
                Double dbSoTien = Convert.ToDouble(SoTien);
                if (dbSoTien == 0)
                {
                    if (HienThiGiaTri_0)
                    {
                        KQ = "0";
                    }
                    return KQ;
                }
            }
            else if (String.IsNullOrEmpty(SoTien))
            {
                return KQ;
            }
            int j;
            if (So != null)
            {
                j = SoTien.IndexOf("-");
                if (j >= 0)
                {
                    if (j == 0)
                    {
                        SoAm = true;
                    }
                    SoTien = SoTien.Replace("-", "");
                }
                int i = SoTien.IndexOf(".");
                int d = 0;
                string PhanDu = "";
                if (i >= 0)
                {
                    PhanDu = SoTien.Substring(i + 1, SoTien.Length - i - 1);
                    SoTien = SoTien.Substring(0, i);
                }
                int Len = SoTien.Length;
                KQ = SoTien;
                if (SoTien.Length > 3)
                {
                    for (j = Len; j > 1; j--)
                    {
                        d++;
                        if (d % 3 == 0)
                        {
                            if (Globals.KieuSoVietNam)
                            {
                                SoTien = SoTien.Insert(j - 1, ".");
                            }
                            else
                            {
                                SoTien = SoTien.Insert(j - 1, ",");
                            }
                        }
                    }
                }
                if (SoSauDauPhay >= 0)
                {
                    String tg = "";
                    for (i = 0; i < SoSauDauPhay; i++)
                    {
                        if (i < PhanDu.Length)
                        {
                            tg = tg + PhanDu[i];
                        }
                        else
                        {
                            tg = tg + "0";
                        }
                    }
                    PhanDu = tg;
                }
                if (PhanDu != "")
                {
                    if (Globals.KieuSoVietNam)
                    {
                        SoTien = SoTien + "," + PhanDu;
                    }
                    else
                    {
                        SoTien = SoTien + "." + PhanDu;
                    }
                }
                KQ = SoTien;
            }
            if (SoAm && !String.IsNullOrEmpty(KQ))
            {
                KQ = "-" + KQ;
            }
            return KQ;

        }

        public static String TienRaChu(long Tien)
        {
            String vR = "";
            if (Tien < 0)
                return vR;
            String vR1 = "";
            long d = 0, So1, So2, So3;
            String ChuSo = "không,một,hai,ba,bốn,năm,sáu,bảy,tám,chín";
            String DonViTien = ",nghìn,triệu,tỉ,nghìn tỉ, triệu tỉ, tỉ tỉ";
            String[] arr1 = ChuSo.Split(',');
            String[] arr2 = DonViTien.Split(',');
            do
            {
                So1 = Tien % 10;
                Tien = (Tien - So1) / 10;
                So2 = Tien % 10;
                Tien = (Tien - So2) / 10;
                So3 = Tien % 10;
                Tien = (Tien - So3) / 10;
                if (!(So1 == 0 && So2 == 0 && So3 == 0))
                {
                    vR1 = "";
                    if (So3 != 0 || Tien != 0)
                    {
                        vR1 = arr1[So3] + " trăm";
                    }
                    if (So2 == 0)
                    {
                        if (vR1 != "" && So1 != 0)
                        {
                            vR1 += " linh";
                        }
                    }
                    else if (So2 == 1)
                    {
                        vR1 += " mười";
                    }
                    else
                    {
                        vR1 += " " + arr1[So2] + " mươi";
                    }
                    if (So1 != 0)
                    {
                        if (So1 == 1 && So2 >= 2)
                        {
                            vR1 += " mốt";
                        }
                        else if (So1 == 5 && So2 >= 1)
                        {
                            vR1 += " lăm";
                        }
                        else
                        {
                            vR1 += " " + arr1[So1];
                        }
                    }
                    vR1 = vR1.Trim();
                    if (vR1 != "")
                    {
                        vR = vR1 + " " + arr2[d] + " " + vR.Trim();
                    }
                }
                d = d + 1;
            } while (Tien != 0);
            vR = vR.Trim();
            if (vR == "")
            {
                vR = "không";
            }
            vR = vR.Substring(0, 1).ToUpper() + vR.Substring(1);
            return vR + " đồng";
        }

        public static void SapXepDanhSachNhomNguoiDung(DataTable dt, int cs0, ref List<Boolean> arrTT, ref List<int> arrCS)
        {
            arrTT[cs0] = false;
            arrCS.Add(cs0);
            int i;
            String MaNhomNguoiDung0 = String.Format("{0}-", dt.Rows[cs0]["iID_MaNhomNguoiDung"]);
            String MaNhomNguoiDung;

            for (i = 0; i < dt.Rows.Count; i++)
            {
                if (arrTT[i])
                {
                    MaNhomNguoiDung = Convert.ToString(dt.Rows[i]["iID_MaNhomNguoiDung"]);
                    if (MaNhomNguoiDung.StartsWith(MaNhomNguoiDung0) && MaNhomNguoiDung.LastIndexOf("-") < MaNhomNguoiDung0.Length)
                    {
                        SapXepDanhSachNhomNguoiDung(dt, i, ref arrTT, ref arrCS);
                    }
                }
            }
        }

        public static int LaySoNgayTrongThang(DateTime ThoiGian)
        {
            return CDate.LaySoNgayTrongThang(ThoiGian);
        }

        public static DataTable GetTable_Thang()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("sTen", typeof(string));

            table.Rows.Add(-1, "--- Tháng ---");
            table.Rows.Add(1, "Tháng 1");
            table.Rows.Add(2, "Tháng 2");
            table.Rows.Add(3, "Tháng 3");
            table.Rows.Add(4, "Tháng 4");
            table.Rows.Add(5, "Tháng 5");
            table.Rows.Add(6, "Tháng 6");
            table.Rows.Add(7, "Tháng 7");
            table.Rows.Add(8, "Tháng 8");
            table.Rows.Add(9, "Tháng 9");
            table.Rows.Add(10, "Tháng 10");
            table.Rows.Add(11, "Tháng 11");
            table.Rows.Add(12, "Tháng 12");

            return table;
        }

        public static SelectOptionList Get_Dropdown_Thang()
        {
            DataTable dt = GetTable_Thang();
            SelectOptionList list = new SelectOptionList(dt, "ID", "sTen");
            dt.Dispose();
            return list;
        }

        public static DataTable GetTable_Nam(int iBuoc = 5)
        {
            DateTime toDay = DateTime.Now;
            int yearDay = toDay.Year;
            int fromYear = yearDay - iBuoc;
            int toYear = yearDay + iBuoc;


            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("sTen", typeof(string));

            table.Rows.Add(-1, "--- Năm ---");
            for(int i = fromYear; i <= toYear; i++)
            {
                table.Rows.Add(i, "Năm " + i);
            }            

            return table;
        }

        public static SelectOptionList Get_Dropdown_Nam()
        {
            DataTable dt = GetTable_Nam();
            SelectOptionList list = new SelectOptionList(dt, "ID", "sTen");
            dt.Dispose();
            return list;
        }

        public static List<DateTime> GetDates(int year, int month)
        {
            return Enumerable.Range(1, DateTime.DaysInMonth(year, month))
                             // Days: 1, 2 ... 31 etc.
                             .Select(day => new DateTime(year, month, day))
                             // Map each day to a date
                             .ToList(); // Load dates into a list
        }

        //public static List<DateTime> GetDates(int iBuoc)
        //{
        //    DateTime dDayNow = DateTime.Now;
        //    DateTime fromDay = dDayNow.AddDays(-iBuoc);
        //    DateTime toDay = dDayNow.AddDays(+iBuoc);


        //    return Enumerable.Range(1, DateTime.DaysInMonth(year, month))
        //                     // Days: 1, 2 ... 31 etc.
        //                     .Select(day => fromDay)
        //                     // Map each day to a date
        //                     .ToList(); // Load dates into a list
        //}

        public static Object ThemGiaTriVaoThamSo(SqlParameterCollection Params, String TenTruong, Object GiaTri)
        {
            Object vR = null;
            if (TenTruong.StartsWith("@") == false)
            {
                TenTruong = "@" + TenTruong;
            }
            if (Params.IndexOf(TenTruong) >= 0)
            {
                Params[TenTruong].Value = GiaTri;
                vR = Params[TenTruong].Value;
            }
            else
            {
                Params.AddWithValue(TenTruong, GiaTri);
            }
            return vR;
        }

        public static String LamNganXau(String str, int nMax)
        {
            String vR = str;
            if (nMax < str.Length)
            {
                int l = nMax;
                while (l >= 0 && str[l] != ' ')
                {
                    l = l - 1;
                }
                if (l < 0)
                {
                    l = nMax;
                }
                vR = String.Format("{0}...", str.Substring(0, l));
            }
            return vR;
        }        

        public static String AnCuoiXau(String str, int k)
        {
            if (k > 0)
            {
                if (str.Length > k)
                {
                    str = str.Remove(str.Length - k);
                }
                else
                {
                    str = "";
                }
                for (int i = 1; i <= k; i++)
                {
                    str = str + "*";
                }
            }
            return str;
        }

        public static int LayViTriChuoi(String str, int k) {
            int vR = 0;
            if (k > 0) {
                vR = str.Length - k;
            }
            return vR;
        }

        public static String Ngay_Thang_Nam_HienTai()
        {
            String Ngay = "";
            int day = DateTime.Now.Day;
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            Ngay = "Ngày  " + day + " Tháng  " + month + "  Năm  " + year;
            return Ngay;
        }

        public static byte[] ConvertToBytes(HttpPostedFileBase Image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(Image.InputStream);
            imageBytes = reader.ReadBytes((int)Image.ContentLength);
            return imageBytes;
        }

        public static string GetIPAddress()
        {
            string strHostName = Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.GetHostEntry(strHostName);
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            return ipAddress.ToString();
        }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
        
        public static bool IsEmailValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool FileUploadCheck(HttpPostedFileBase hpf)
        {
            bool vR = false;
            if (hpf != null && hpf.ContentLength > 0)
            {
                String FileName = hpf.FileName;
                string ext = System.IO.Path.GetExtension(FileName).ToLower();
                string[] exts = { ".doc", ".docx", ".pdf", ".rtf", ".xls", ".xlsx", ".rar", ".zip", ".jpg", ".jpeg", ".bmp", ".png", ".emf", ".exif", ".gif", ".icon", ".MemoryBmp", ".tiff", ".wmf" };
                for (int i = 0; i < exts.Length; i++)
                {
                    if (ext == exts[i])
                        vR = true;
                }
            }

            return vR;
        }

        public static bool FileUploadCheck_Api(string sDinhDangFile)
        {
            bool vR = false;
            if (sDinhDangFile != null)
            {
                string ext = sDinhDangFile.ToLower();
                string[] exts = { ".doc", ".docx", ".pdf", ".rtf", ".xls", ".xlsx", ".rar", ".zip", ".jpg", ".jpeg", ".bmp", ".png", ".emf", ".exif", ".gif", ".icon", ".MemoryBmp", ".tiff", ".wmf" };
                for (int i = 0; i < exts.Length; i++)
                {
                    if (ext == exts[i])
                        vR = true;
                }
            }

            return vR;
        }

        public static bool FileUploadCheck_Video(HttpPostedFileBase hpf)
        {
            bool vR = false;
            if (hpf != null && hpf.ContentLength > 0)
            {
                String FileName = hpf.FileName;
                string ext = System.IO.Path.GetExtension(FileName).ToLower();
                string[] exts = { ".mp3", ".mp4" };
                for (int i = 0; i < exts.Length; i++)
                {
                    if (ext == exts[i])
                        vR = true;
                }
            }

            return vR;
        }

        public static bool FileUploadCheck_Images(HttpPostedFileBase hpf)
        {
            bool vR = false;
            if (hpf != null && hpf.ContentLength > 0)
            {
                String FileName = hpf.FileName;
                string ext = System.IO.Path.GetExtension(FileName).ToLower();
                string[] exts = { ".jpg", ".jpeg", ".bmp", ".png", ".emf", ".exif", ".gif", ".icon", ".MemoryBmp", ".tiff", ".wmf" };
                for (int i = 0; i < exts.Length; i++)
                {
                    if (ext == exts[i])
                        vR = true;
                }
            }

            return vR;
        }
    }
}
