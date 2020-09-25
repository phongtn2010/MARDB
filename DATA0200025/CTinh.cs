using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using DomainModel.Controls;

namespace DATA0200025
{
    public class CTinh
    {
        public static DataTable Get_Table()
        {
            DataTable vR = null;

            SqlCommand cmd = new SqlCommand();
            string cmdText = "SELECT * FROM BC_Tinh ORDER BY iSTT";
            cmd = new SqlCommand(cmdText);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();

            return vR;
        }
        public static DataTable Get_Table(string iID_MaMien, string iID_MaVung, string sTieuDe, string _FromDate, string _ToDate, int Trang, int SoBanGhi)
        {
            DataTable vR = null;

            string sDk = "1=1 ";
            SqlCommand cmd = new SqlCommand();
            if (!string.IsNullOrEmpty(iID_MaMien))
            {
                sDk = sDk + " AND iID_MaMien = @iID_MaMien";
                cmd.Parameters.AddWithValue("@iID_MaMien", iID_MaMien);
            }
            if (!string.IsNullOrEmpty(iID_MaVung))
            {
                if(Convert.ToInt32(iID_MaVung) > 0)
                {
                    sDk = sDk + " AND iID_MaVung = @iID_MaVung";
                    cmd.Parameters.AddWithValue("@iID_MaVung", iID_MaVung);
                }    
            }
            if (!string.IsNullOrEmpty(sTieuDe))
            {
                sDk = sDk + " AND (sTenTinh LIKE N'%' + @sTieuDe + '%') ";
                cmd.Parameters.AddWithValue("@sTieuDe", sTieuDe);
            }
            if (!(string.IsNullOrEmpty(_FromDate) || (_FromDate == "")))
            {
                sDk = sDk + " AND dNgayTao >= @_FromDate";
                cmd.Parameters.AddWithValue("@_FromDate", CommonFunction.LayNgayTuXau(_FromDate));
            }
            if (!(string.IsNullOrEmpty(_ToDate) || (_ToDate == "")))
            {
                sDk = sDk + " AND dNgayTao <= @_ToDate";
                cmd.Parameters.AddWithValue("@_ToDate", CommonFunction.LayNgayTuXau(_ToDate));
            }
            string SQL = string.Format("SELECT * FROM BC_Tinh WHERE {0}", sDk);
            cmd.CommandText = SQL;
            vR = CommonFunction.dtData(cmd, "iSTT ASC", Trang, SoBanGhi, CThamSo.iKetNoi);
            cmd.Dispose();

            return vR;
        }
        public static int Get_Count(string iID_MaMien, string iID_MaVung, string sTieuDe, string _FromDate, string _ToDate)
        {
            int vR = 0;

            string sDk = "1=1 ";
            SqlCommand cmd = new SqlCommand();
            if (!string.IsNullOrEmpty(iID_MaMien))
            {
                sDk = sDk + " AND iID_MaMien = @iID_MaMien";
                cmd.Parameters.AddWithValue("@iID_MaMien", iID_MaMien);
            }
            if (!string.IsNullOrEmpty(iID_MaVung))
            {
                if (Convert.ToInt32(iID_MaVung) > 0)
                {
                    sDk = sDk + " AND iID_MaVung = @iID_MaVung";
                    cmd.Parameters.AddWithValue("@iID_MaVung", iID_MaVung);
                }
            }
            if (!string.IsNullOrEmpty(sTieuDe))
            {
                sDk = sDk + " AND (sTenTinh LIKE N'%' + @sTieuDe + '%') ";
                cmd.Parameters.AddWithValue("@sTieuDe", sTieuDe);
            }
            if (!(string.IsNullOrEmpty(_FromDate) || (_FromDate == "")))
            {
                sDk = sDk + " AND dNgayTao >= @_FromDate";
                cmd.Parameters.AddWithValue("@_FromDate", CommonFunction.LayNgayTuXau(_FromDate));
            }
            if (!(string.IsNullOrEmpty(_ToDate) || (_ToDate == "")))
            {
                sDk = sDk + " AND dNgayTao <= @_ToDate";
                cmd.Parameters.AddWithValue("@_ToDate", CommonFunction.LayNgayTuXau(_ToDate));
            }
            string SQL = string.Format("SELECT Count(*) FROM BC_Tinh WHERE {0}", sDk);
            cmd.CommandText = SQL;
            vR = Convert.ToInt32(Connection.GetValue(cmd, 0, CThamSo.iKetNoi));
            cmd.Dispose();

            return vR;
        }
        public static DataTable Get_Table_Detail(string iID_MaTinh)
        {
            DataTable vR = null;
            SqlCommand cmd = new SqlCommand();
            string cmdText = "SELECT * FROM BC_Tinh WHERE iID_MaTinh=@iID_MaTinh";
            cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@iID_MaTinh", iID_MaTinh);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();
            return vR;
        }
        public static String Get_Name(string iID_MaTinh)
        {
            String vR = "";

            DataTable dt = Get_Table_Detail(iID_MaTinh);
            if (dt.Rows.Count > 0)
            {
                vR = Convert.ToString(dt.Rows[0]["sTenTinh"]);
            }
            dt.Dispose();

            return vR;
        }
        public static SelectOptionList Get_Dropdown(string sTitle = "")
        {
            SelectOptionList vR;

            DataTable dt = Get_Table();
            dt.Rows.InsertAt(dt.NewRow(), 0);
            dt.Rows[0]["iID_MaTinh"] = "";
            dt.Rows[0]["sTenTinh"] = sTitle;
            vR = new SelectOptionList(dt, "iID_MaTinh", "sTenTinh");
            dt.Dispose();

            return vR;
        }
    }
}
