using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel;
using DomainModel.Controls;
using System.Data;
using System.Data.SqlClient;

namespace DATA0200025
{
    public class CLanguage
    {
        public static DataTable Get_DC_NgonNgu()
        {
            string SQL = "SELECT * FROM DC_NgonNgu WHERE bHoatDong = 1 ORDER BY iID_MaNgonNgu";
            SqlCommand cmd = new SqlCommand(SQL);
            DataTable dataTable = Connection.GetDataTable(cmd, 0);
            cmd.Dispose();
            return dataTable;
        }

        public static SelectOptionList Get_Dropdown_NgonNgu(string sChuoiBatDau)
        {
            DataTable dt = Get_DC_NgonNgu();
            dt.Rows.InsertAt(dt.NewRow(), 0);
            dt.Rows[0]["iID_MaNgonNgu"] = -1;
            dt.Rows[0]["sTen"] = sChuoiBatDau;
            SelectOptionList list = new SelectOptionList(dt, "iID_MaNgonNgu", "sTen");
            dt.Dispose();
            return list;
        }

        public static string Get_Name_NgonNgu(int iID_MaNgonNgu)
        {
            string str = "";
            DataTable table = Get_Table_NgonNgu(iID_MaNgonNgu);
            if (table.Rows.Count > 0)
            {
                str = Convert.ToString(table.Rows[0]["sTen"]);
            }
            table.Dispose();
            return str;
        }

        public static DataTable Get_Table_NgonNgu(int iID_MaNgonNgu)
        {
            string SQL = "SELECT * FROM DC_NgonNgu WHERE iID_MaNgonNgu=@iID_MaNgonNgu AND bHoatDong = 1";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaNgonNgu", (int)iID_MaNgonNgu);
            DataTable dataTable = Connection.GetDataTable(cmd, 0);
            cmd.Dispose();
            return dataTable;
        }

        public static DataTable Get_Table_NgonNgu_Code(string CodeNgonNgu)
        {
            string SQL = "SELECT * FROM DC_NgonNgu WHERE sCode=@sCode";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@sCode", CodeNgonNgu);
            DataTable dataTable = Connection.GetDataTable(cmd, 0);
            cmd.Dispose();
            return dataTable;
        }
    }
}
