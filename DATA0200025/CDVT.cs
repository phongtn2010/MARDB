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
    public class CDVT
    {
        public static DataTable Get_Table()
        {
            DataTable vR = null;

            SqlCommand cmd = new SqlCommand();
            string cmdText = "SELECT * FROM CNN25_DonViTinh ORDER BY iID_MaDonViTinh ASC";
            cmd = new SqlCommand(cmdText);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();

            return vR;
        }
        public static DataTable Get_Table_Detail(string iID_MaDonViTinh)
        {
            DataTable vR = null;
            SqlCommand cmd = new SqlCommand();
            string cmdText = "SELECT * FROM CNN25_DonViTinh WHERE iID_MaDonViTinh=@iID_MaDonViTinh";
            cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@iID_MaDonViTinh", iID_MaDonViTinh);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();
            return vR;
        }
        public static String Get_Name(string iID_MaDonViTinh)
        {
            String vR = "";

            DataTable dt = Get_Table_Detail(iID_MaDonViTinh);
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
            dt.Rows[0]["iID_MaDonViTinh"] = 0;
            dt.Rows[0]["sTen"] = sTitle;
            vR = new SelectOptionList(dt, "iID_MaDonViTinh", "sTen");
            dt.Dispose();

            return vR;
        }
    }
}
