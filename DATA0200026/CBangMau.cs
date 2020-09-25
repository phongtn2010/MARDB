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
    public class CBangMau
    {
        public static DataTable Get_Table()
        {
            DataTable vR = null;

            SqlCommand cmd = new SqlCommand();
            string cmdText = "SELECT * FROM BC_DonVi ORDER BY iSTT";
            cmd = new SqlCommand(cmdText);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();

            return vR;
        }
        public static DataTable Get_Table(string iID_MaPhongBan, string sTieuDe = "")
        {
            DataTable vR = null;

            SqlCommand cmd = new SqlCommand();
            string cmdText = "SELECT * FROM BC_BangMau WHERE iMaTrangThai=1 AND iID_MaPhongBan = @iID_MaPhongBan ORDER BY iSTT";
            cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@iID_MaPhongBan", iID_MaPhongBan);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();

            return vR;
        }
        public static DataTable Get_Table_Detail(string iID_MaBangMau)
        {
            DataTable vR = null;
            SqlCommand cmd = new SqlCommand();
            string cmdText = "SELECT * FROM BC_BangMau WHERE iID_MaBangMau=@iID_MaBangMau";
            cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@iID_MaBangMau", iID_MaBangMau);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();
            return vR;
        }
        public static String Get_Name(string iID_MaBangMau)
        {
            String vR = "";

            DataTable dt = Get_Table_Detail(iID_MaBangMau);
            if (dt.Rows.Count > 0)
            {
                vR = Convert.ToString(dt.Rows[0]["sTenBang"]);
            }
            dt.Dispose();

            return vR;
        }
        public static int Get_Max_BangMau()
        {
            int vR = 0;
            SqlCommand cmd;
            cmd = new SqlCommand("SELECT MAX(iSTT) FROM BC_BangMau");
            vR = Convert.ToInt32(Connection.GetValue(cmd, CThamSo.iKetNoi));
            cmd.Dispose();

            return vR;
        }
        public static SelectOptionList Get_Dropdown(string iID_MaPhongBan, string sTitle = "")
        {
            SelectOptionList vR;

            DataTable dt = Get_Table(iID_MaPhongBan, "");
            dt.Rows.InsertAt(dt.NewRow(), 0);
            dt.Rows[0]["iID_MaBangMau"] = 0;
            dt.Rows[0]["sTenBang"] = sTitle;
            vR = new SelectOptionList(dt, "iID_MaBangMau", "sTenBang");
            dt.Dispose();

            return vR;
        }
    }
}
