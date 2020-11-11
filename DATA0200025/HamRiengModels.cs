using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using DomainModel;
using System.Configuration;

namespace DATA0200025
{
    public class HamRiengModels
    {
        public static String SSODomain = "";
        public static int SSOTimeout = 2000;

        public static string Get_Domain()
        {
            string vR = "";
            //Uri myuri = new Uri(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
            //string pathQuery = myuri.PathAndQuery;
            //string hostName = "";
            //if (pathQuery == "/")
            //{
            //    hostName = myuri.ToString();
            //}
            //else
            //{
            //    hostName = myuri.ToString().Replace(pathQuery, "");
            //}
            //if (String.IsNullOrEmpty(hostName) == false)
            //{
            //    vR = hostName;
            //}
            //else
            //{
            vR = ConfigurationManager.AppSettings["ServerURL"];
            //}

            return vR;
        }

        public static DataTable GetTable_LoaiHinhThucKiemTra()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("NAME", typeof(string));

            table.Rows.Add(1, "Dựa trên kết quả tự đánh giá sự phù hợp của tổ chức, cá nhân (2a)");
            table.Rows.Add(2, "Dựa trên kết quả chứng nhận của tổ chức chứng nhận đã đăng ký (2b)");
            table.Rows.Add(3, "Dựa trên kết quả chứng nhận của tổ chức chứng nhận được chỉ định (2c)");
            table.Rows.Add(4, "Miễn giảm kiểm tra (2d)");

            return table;
        }

        public static String Get_Name_LoaiHinhThucKiemTra(int iMa)
        {
            string vR = "";
            DataTable dt = GetTable_LoaiHinhThucKiemTra();
            var rowColl = dt.AsEnumerable();
            vR = (from r in rowColl
                  where r.Field<int>("ID") == iMa
                  select r.Field<string>("NAME")).First<string>();
            dt.Dispose();

            return vR;
        }

        public static DataTable GetTable_QuyChuanKyThuat()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("NAME", typeof(string));

            table.Rows.Add(1, "QCVN 01-190: 2020/BNNPTNT");
            table.Rows.Add(2, "QCVN 01-183: 2016/BNNPTNT");

            return table;
        }
        public static String Get_Name_QuyChuanKyThuat(int iMa)
        {
            string vR = "";
            DataTable dt = GetTable_QuyChuanKyThuat();
            var rowColl = dt.AsEnumerable();
            vR = (from r in rowColl
                  where r.Field<int>("ID") == iMa
                  select r.Field<string>("NAME")).First<string>();
            dt.Dispose();

            return vR;
        }

        public static DataTable GetTable_HinhThucCongBo()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("NAME", typeof(string));

            table.Rows.Add(0, "Không có");
            table.Rows.Add(1, "<");
            table.Rows.Add(2, ">");
            table.Rows.Add(3, "=");
            table.Rows.Add(4, "<=");
            table.Rows.Add(5, ">=");
            table.Rows.Add(6, "min - max");
            
            return table;
        }
        public static String Get_Name_HinhThucCongBo(int iMa)
        {
            string vR = "";
            DataTable dt = GetTable_HinhThucCongBo();
            var rowColl = dt.AsEnumerable();
            vR = (from r in rowColl
                  where r.Field<int>("ID") == iMa
                  select r.Field<string>("NAME")).First<string>();
            dt.Dispose();

            return vR;
        }
        public enum LoaiTapTinDinhKem
        {
            PhieuKetQuaPhanTich=1,
            NhanSanPhamCuaCoSoSanXuat=2,
        }
        public static DataTable GetTable_LoaiTapTinDinhKem()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("NAME", typeof(string));

            table.Rows.Add(1, "Phiếu kết quả phân tích chất lượng của nước xuất khẩu cấp cho lô hàng (Certificate of Analysis)");
            table.Rows.Add(2, "Nhãn sản phẩm của cơ sở sản xuất");
            table.Rows.Add(3, "Bản tiêu chuẩn công bố áp dụng của tổ chức, cá nhân nhập khẩu");
            table.Rows.Add(4, "Bản thông tin sản phẩm in từ cổng thông tin điện tử ");
            table.Rows.Add(5, "Giấy chứng nhận lưu hành tự do hoặc văn bản có giá trị tương đương do cơ quan có thẩm quyền của nước xuất xứ cấp đối với nguyên liệu đơn, thức ăn truyền thống");
            table.Rows.Add(6, "Giấy chứng nhận Hệ thống quản lý chất lượng (ISO)");
            table.Rows.Add(7, "Giấy chứng nhận thực hành sản xuất tốt (GMP)");
            table.Rows.Add(8, "Giấy chứng nhận phân tích nguy cơ và kiểm soát điểm tới hạn (HACCP) hoặc giấy chứng nhận tương đương của cơ sở sản xuất đối với nguyên liệu đơn.");
            table.Rows.Add(9, "Tài liệu khác.");
            table.Rows.Add(20, "File từ chối hồ sơ chờ tiếp nhận BPMC.");
            table.Rows.Add(21, "File từ chối hồ sơ bổ sung BPMC.");
            table.Rows.Add(22, "File từ chối hồ sơ bổ sung Phòng TACN.");
            table.Rows.Add(26, "File từ chối XCNL bổ sung BPMC");
            table.Rows.Add(27, "File từ chối XCNL bổ sung BPMC Phòng TACN");

            table.Rows.Add(30, "File bổ sung hồ sơ chờ tiếp nhận BPMC.");
            table.Rows.Add(31, "File bổ sung hồ sơ bổ sung BPMC.");
            table.Rows.Add(32, "File bổ sung hồ sơ bổ sung Phòng TACN.");
            table.Rows.Add(35, "File bổ sung XCNL chờ tiếp nhận XNCL.");
            table.Rows.Add(36, "File bổ sung XCNL bổ sung XNCL.");
            table.Rows.Add(37, "File bổ sung XCNL bổ sung XNCL Phòng TACN.");

            table.Rows.Add(40, "File từ chối hồ sơ Chuyên Viên.");
            table.Rows.Add(41, "File từ chối XNCL Chuyên Viên.");
            table.Rows.Add(50, "File bổ sung hồ sơ Chuyên Viên.");
            table.Rows.Add(51, "File bổ sung XNCL Chuyên Viên.");

            table.Rows.Add(60, "File từ chối hồ sơ Lãnh Đạo Phòng.");
            table.Rows.Add(80, "File từ chối hồ sơ Lãnh Đạo Cục.");

            table.Rows.Add(90, "File Thu hồi giấy GDK.");
            table.Rows.Add(91, "File Thu hồi giấy XNCL.");
            table.Rows.Add(100, "File Hợp Đồng Hồ Sơ");
            table.Rows.Add(101, "File Hóa Đơn  Hồ Sơ");
            table.Rows.Add(102, "File Phiếu Đóng Gói  Hồ Sơ");

            return table;
        }
        public static String Get_Name_LoaiTapTinDinhKem(int iMa)
        {
            string vR = "";
            DataTable dt = GetTable_LoaiTapTinDinhKem();
            var rowColl = dt.AsEnumerable();
            vR = (from r in rowColl
                  where r.Field<int>("ID") == iMa
                  select r.Field<string>("NAME")).First<string>();
            dt.Dispose();

            return vR;
        }

        public static int MaDonViCuaNhomNguoiDung(String MaNguoiDung)
        {
            String SQL = String.Format("SELECT iID_MaDonVi " +
                                       "FROM QT_NguoiDung INNER JOIN " +
                                            "QT_NhomNguoiDung ON QT_NhomNguoiDung.iID_MaNhomNguoiDung=QT_NguoiDung.iID_MaNhomNguoiDung " +
                                       "WHERE sID_MaNguoiDung='{0}'", MaNguoiDung);
            SqlCommand cmd = new SqlCommand(SQL);
            int MaDonVi = Convert.ToInt16(Connection.GetValue(cmd, 0));
            cmd.Dispose();
            return MaDonVi;
        }

        public static int ChuyenThoiGianSangSo(String ThoiGian)
        {
            int vR=0;
            int Gio = 0;
            int Phut = 0;
            String[] arr = ThoiGian.Split('h');
            if (CommonFunction.IsNumeric(arr[0]))
            {
                Gio = Convert.ToInt16(arr[0]);
            }
            if (arr.Length>1 && CommonFunction.IsNumeric(arr[1]))
            {
                Phut = Convert.ToInt16(arr[1]);
            }
            vR = Gio * 60 + Phut;
            return vR;
        }

        public static String ChuyenSoSangThoiGian(int ThoiGian)
        {
            String vR="";
            if (ThoiGian > 0)
            {
                int Gio = ThoiGian / 60;
                int Phut = ThoiGian % 60;
                vR = String.Format("{0}h{1}", Gio, Phut);
            }
            return vR;
        }

        public static object LayGiaTri(DataTable dt, string TenTruongTim, object GiaTriTim, string TenTruongLayGiaTri)
        {
            int i;
            for (i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToString(dt.Rows[i][TenTruongTim]) == Convert.ToString(GiaTriTim))
                {
                    return dt.Rows[i][TenTruongLayGiaTri];
                }
            }
            return null;
        }

        public static String LayXauDinhDangSo(int SoSauDauPhay)
        {
            String vR="0";
            if (SoSauDauPhay > 0)
            {
                vR += ".";
                for (int i = 1; i <= SoSauDauPhay; i++)
                {
                    vR += "0";
                }
            }
            return vR;
        }
    }
}
