using DomainModel;
using DomainModel.Controls;
using DATA0200025;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace APP0200025.Models
{
    public class CHamRieng
    {
        public static string SSODomain = "";
        public static int SSOTimeout = 0;
        public static int VisitorOnline = 0;


        public static string Get_Domain()
        {
            string vR = "";
            //Uri myuri = new Uri(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
            //string pathQuery = myuri.PathAndQuery;
            //string hostName = "";
            //if(pathQuery == "/")
            //{
            //    hostName = myuri.ToString();
            //}
            //else
            //{
            //    hostName = myuri.ToString().Replace(pathQuery, "");
            //}
            //if(String.IsNullOrEmpty(hostName) == false)
            //{
            //    vR = hostName;
            //}
            //else
            //{
            vR = ConfigurationManager.AppSettings["ServerURL"];
            //}

            return vR;
        }

        public static string Get_Domain_File()
        {
            string vR = "";
            vR = ConfigurationManager.AppSettings["FilesUrl"];

            return vR;
        }

        public static void Language()
        {
            string lang = "vi-VN";
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(lang);
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture(lang);
        }

        public static string Get_Google_Map_Key()
        {
            string vR = "";
            vR = "AIzaSyBwarlVfqlWKfonKl9T0wr9VGC0ynvbdiU";
            return vR;
        }

        public static DataTable GetTable_Ngay()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("sTen", typeof(string));


            table.Rows.Add(-1, "--- Ngày sinh ---");

            for (int i = 1; i < 32; i++)
            {
                table.Rows.Add(i, i);
            }

            return table;
        }
        public static DataTable GetTable_Thang()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("sTen", typeof(string));

            table.Rows.Add(-1, "--- Tháng sinh ---");
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
        public static DataTable GetTable_Nam()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("sTen", typeof(string));

            int iNam = DateTime.Now.Year;

            table.Rows.Add(-1, "--- Năm sinh ---");

            for (int i = 1920; i <= iNam; i++)
            {
                table.Rows.Add(i, i);
            }

            return table;
        }
        public static SelectOptionList Get_Dropdown_Ngay()
        {
            DataTable dt = GetTable_Ngay();
            SelectOptionList list = new SelectOptionList(dt, "ID", "sTen");
            dt.Dispose();
            return list;
        }
        public static SelectOptionList Get_Dropdown_Thang()
        {
            DataTable dt = GetTable_Thang();
            SelectOptionList list = new SelectOptionList(dt, "ID", "sTen");
            dt.Dispose();
            return list;
        }
        public static SelectOptionList Get_Dropdown_Nam()
        {
            DataTable dt = GetTable_Nam();
            SelectOptionList list = new SelectOptionList(dt, "ID", "sTen");
            dt.Dispose();
            return list;
        }

        public static DataTable GetTable_PageSize()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("sTen", typeof(string));

            table.Rows.Add(10, "10 bản ghi");
            table.Rows.Add(20, "20 bản ghi");
            table.Rows.Add(30, "30 bản ghi");
            table.Rows.Add(40, "40 bản ghi");
            table.Rows.Add(50, "50 bản ghi");
            table.Rows.Add(80, "80 bản ghi");
            table.Rows.Add(100, "100 bản ghi");

            return table;
        }

        public static SelectOptionList Get_Dropdown_PageSize()
        {
            DataTable dt = GetTable_PageSize();
            SelectOptionList list = new SelectOptionList(dt, "ID", "sTen");
            dt.Dispose();
            return list;
        }

        public static DataTable GetTable_GioiTinh()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("sTen", typeof(string));

            table.Rows.Add(-1, "--- Giới Tính ---");
            table.Rows.Add(1, "Nam");
            table.Rows.Add(2, "Nữ");

            return table;
        }
        public static SelectOptionList Get_Dropdown_GioiTinh()
        {
            DataTable dt = GetTable_GioiTinh();
            SelectOptionList list = new SelectOptionList(dt, "ID", "sTen");
            dt.Dispose();
            return list;
        }

        public static DataTable GetTable_TrangThai()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("sTen", typeof(string));

            table.Rows.Add(-1, "--- Trạng thái ---");
            table.Rows.Add(0, "Mới đăng ký");
            table.Rows.Add(1, "Đã tư vấn");
            table.Rows.Add(2, "Đã khám");

            table.Rows.Add(11, "Hủy khám");

            return table;
        }

        public static SelectOptionList Get_Dropdown_TrangThai()
        {
            DataTable dt = GetTable_TrangThai();
            SelectOptionList list = new SelectOptionList(dt, "ID", "sTen");
            dt.Dispose();
            return list;
        }

        public static DataTable GetTable_CauHinh()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("sTen", typeof(string));

            table.Rows.Add(-1, "--- Cấu hình API ---");
            table.Rows.Add(1, "API HNI");
            table.Rows.Add(2, "API VINAPHONE");
            table.Rows.Add(3, "ĐẦU SỐ");

            return table;
        }

        public static SelectOptionList Get_Dropdown_CauHinh()
        {
            DataTable dt = GetTable_CauHinh();
            SelectOptionList list = new SelectOptionList(dt, "ID", "sTen");
            dt.Dispose();
            return list;
        }

        public static DataTable GetTable_CoSoKham()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("sTen", typeof(string));

            table.Rows.Add(-1, "--- Bạn vui lòng chọn cơ sở khám bệnh ---");
            table.Rows.Add(1, "Cơ sở 1: Thôn Bầu, xã Kim Chung, huyện Đông Anh, thành phố Hà Nội");
            table.Rows.Add(2, "Cơ sở 2: Số 78, Đường Giải Phóng, Hà Nội");

            return table;
        }

        public static SelectOptionList Get_Dropdown_CoSoKham()
        {
            DataTable dt = GetTable_CoSoKham();
            SelectOptionList list = new SelectOptionList(dt, "ID", "sTen");
            dt.Dispose();
            return list;
        }

        public static DataTable GetTable_ViTri_QuangCao()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("sTen", typeof(string));

            table.Rows.Add(-1, "--- Vị trí đặt quảng cáo ---");
            table.Rows.Add(1, "Vị trí 1");
            table.Rows.Add(2, "Vị trí 2");
            table.Rows.Add(3, "Vị trí 3");
            table.Rows.Add(4, "Vị trí 4");
            table.Rows.Add(5, "Vị trí 5");
            table.Rows.Add(6, "Vị trí 6");
            table.Rows.Add(7, "Vị trí 7");
            table.Rows.Add(8, "Vị trí 8");
            table.Rows.Add(9, "Vị trí 9");
            table.Rows.Add(10, "Vị trí 10");
            table.Rows.Add(11, "Vị trí 11");
            table.Rows.Add(12, "Vị trí 12");
            table.Rows.Add(13, "Vị trí 13");
            table.Rows.Add(14, "Vị trí 14");
            table.Rows.Add(15, "Vị trí 15");

            return table;
        }

        public static DataTable GetTable_User_Location()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("sTen", typeof(string));

            table.Rows.Add(-1, "--- Loại người dùng ---");
            table.Rows.Add(0, "Chưa có trong người dùng");
            table.Rows.Add(1, "Có trong người dùng");

            return table;
        }

        public static DataTable GetTable_KetQuaXuLy()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("sTen", typeof(string));

            table.Rows.Add(-1, "--- Tất cả ---");
            table.Rows.Add(1, "Đạt cấp giấy đăng ký");
            table.Rows.Add(2, "Từ chối cấp GĐK");
            table.Rows.Add(3, "Yêu cầu bổ sung");

            return table;
        }

        public static SelectOptionList Get_Dropdown_User_Location()
        {
            DataTable dt = GetTable_User_Location();
            SelectOptionList list = new SelectOptionList(dt, "ID", "sTen");
            dt.Dispose();
            return list;
        }

        public static SelectOptionList Get_Dropdown_ViTri_QuangCao()
        {
            DataTable dt = GetTable_ViTri_QuangCao();
            SelectOptionList list = new SelectOptionList(dt, "ID", "sTen");
            dt.Dispose();
            return list;
        }

        public static SelectOptionList Get_Dropdown_KetQuaXuLy()
        {
            DataTable dt = GetTable_KetQuaXuLy();
            SelectOptionList list = new SelectOptionList(dt, "ID", "sTen");
            dt.Dispose();
            return list;
        }
    }
    public class SheetData
    {
        public string sData { get; set; }
    }


    public class DanhSachCuaHang
    {
        public int MaCuaHang { get; set; }
        public Double KhoangCach { get; set; }
    }

    public class CuaHangComparer : IComparer<DanhSachCuaHang>
    {
        public int Compare(DanhSachCuaHang x, DanhSachCuaHang y)
        {
            return x.KhoangCach.CompareTo(y.KhoangCach);
        }
    }
}