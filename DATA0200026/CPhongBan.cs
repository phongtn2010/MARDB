using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using DomainModel;
using DomainModel.Controls;

namespace DATA0200026
{
    public class CPhongBan
    {
        public static DataTable Get_Table_All()
        {
            DataTable vR = new DataTable();

            SqlCommand cmd;
            cmd = new SqlCommand("SELECT * FROM BC_PhongBan ORDER BY iSTT");
            vR = Connection.GetDataTable(cmd);
            cmd.Dispose();

            return vR;
        }

        public static DataTable Get_Table_Detail(string iID_MaPhongBan)
        {
            DataTable vR = new DataTable();

            SqlCommand cmd;
            cmd = new SqlCommand("SELECT * FROM BC_PhongBan WHERE iID_MaPhongBan=@iID_MaPhongBan");
            cmd.Parameters.AddWithValue("@iID_MaPhongBan", iID_MaPhongBan);
            vR = Connection.GetDataTable(cmd);
            cmd.Dispose();

            return vR;
        }

        public static string Get_Name(string iID_MaPhongBan)
        {
            string vR = "";

            DataTable dt = Get_Table_Detail(iID_MaPhongBan);
            if (dt.Rows.Count > 0)
            {
                vR = Convert.ToString(dt.Rows[0]["sTen"]);
            }
            dt.Dispose();

            return vR;
        }
        public static SelectOptionList Get_Dropdown(string sTitle = "")
        {
            SelectOptionList vR;

            DataTable dt = Get_Table_All();
            dt.Rows.InsertAt(dt.NewRow(), 0);
            dt.Rows[0]["iID_MaPhongBan"] = 0;
            dt.Rows[0]["sTen"] = sTitle;
            vR = new SelectOptionList(dt, "iID_MaPhongBan", "sTen");
            dt.Dispose();

            return vR;
        }
    }
}