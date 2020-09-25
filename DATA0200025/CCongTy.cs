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
    public class CCongTy
    {
        public static DataTable Get_Table_All()
        {
            DataTable vR = null;

            SqlCommand cmd;
            string cmdText = "SELECT * FROM DC_CongTy ORDER BY iID_MaCongTy";
            cmd = new SqlCommand(cmdText);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();

            return vR;
        }

        public static DataTable Get_Table_Top(int iTop = 1)
        {
            DataTable vR = null;

            SqlCommand cmd;
            string cmdText = "SELECT TOP "+ iTop + " * FROM DC_CongTy ORDER BY iID_MaCongTy DESC";
            cmd = new SqlCommand(cmdText);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();

            return vR;
        }

        public static DataTable Get_Table_Detail(int iID_MaCongTy)
        {
            DataTable vR = null;
            SqlCommand cmd;
            string cmdText = "SELECT * FROM DC_CongTy WHERE iID_MaCongTy=@iID_MaCongTy";
            cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@iID_MaCongTy", iID_MaCongTy);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();
            return vR;
        }

        public static String Get_Name_CongTy(int iID_MaCongTy)
        {
            String vR = "";

            DataTable dt = Get_Table_Detail(iID_MaCongTy);
            if (dt.Rows.Count > 0)
            {
                vR = Convert.ToString(dt.Rows[0]["sTenCongTy"]);
            }
            dt.Dispose();

            return vR;
        }
    }
}
