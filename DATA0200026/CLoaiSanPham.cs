using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using DomainModel.Controls;

namespace DATA0200026
{
    public class CLoaiSanPham
    {
        public static DataTable Get_Table()
        {
            DataTable vR = null;

            SqlCommand cmd = new SqlCommand();
            string cmdText = "SELECT * FROM BC_LoaiSanPham ORDER BY iSTT";
            cmd = new SqlCommand(cmdText);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();

            return vR;
        }
        public static DataTable Get_Table_Detail(string iID_LoaiSanPham)
        {
            DataTable vR = null;
            SqlCommand cmd = new SqlCommand();
            string cmdText = "SELECT * FROM BC_LoaiSanPham WHERE iID_LoaiSanPham=@iID_LoaiSanPham";
            cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@iID_LoaiSanPham", iID_LoaiSanPham);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();
            return vR;
        }
        public static String Get_Name(string iID_LoaiSanPham)
        {
            String vR = "";

            DataTable dt = Get_Table_Detail(iID_LoaiSanPham);
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

            DataTable dt = Get_Table();
            dt.Rows.InsertAt(dt.NewRow(), 0);
            dt.Rows[0]["iID_LoaiSanPham"] = 0;
            dt.Rows[0]["sTen"] = sTitle;
            vR = new SelectOptionList(dt, "iID_LoaiSanPham", "sTen");
            dt.Dispose();

            return vR;
        }
    }
}
