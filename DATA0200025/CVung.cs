using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace DATA0200025
{
    public class CVung
    {
        public static DataTable Get_Table(string iID_MaMien, string sTieuDe, string _FromDate = "", string _ToDate = "")
        {
            DataTable vR = null;

            string sDk = "1=1 ";
            SqlCommand cmd = new SqlCommand();
            if (!string.IsNullOrEmpty(iID_MaMien))
            {
                sDk = sDk + " AND iID_MaMien = @iID_MaMien";
                cmd.Parameters.AddWithValue("@iID_MaMien", iID_MaMien);
            }
            if (!string.IsNullOrEmpty(sTieuDe))
            {
                sDk = sDk + " AND (sTenVung LIKE N'%" + sTieuDe + "%') ";
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
            string SQL = string.Format("SELECT * FROM BC_Vung WHERE {0} ORDER BY iSTT", sDk);
            cmd.CommandText = SQL;
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();

            return vR;
        }
        public static DataTable Get_Table_Detail(string iID_MaVung)
        {
            DataTable vR = null;
            SqlCommand cmd = new SqlCommand();
            string cmdText = "SELECT * FROM BC_Vung WHERE iID_MaVung=@iID_MaVung";
            cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@iID_MaVung", iID_MaVung);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();
            return vR;
        }
        public static String Get_Name(string iID_MaVung)
        {
            String vR = "";

            DataTable dt = Get_Table_Detail(iID_MaVung);
            if (dt.Rows.Count > 0)
            {
                vR = Convert.ToString(dt.Rows[0]["sTenVung"]);
            }
            dt.Dispose();

            return vR;
        }
    }
}
