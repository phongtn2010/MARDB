using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DomainModel;
using DomainModel.Controls;
using System.Runtime.InteropServices;

namespace DATA0200025
{
   public class clDanhMuc
    {

        public static DataTable GetDataTable(string sTenBang)
        {
            DataTable vR;
            string SQL = "SELECT * FROM CNN25_DanhMuc WHERE iID_MaLoaiDanhMuc IN (SELECT iID_MaLoaiDanhMuc FROM DC_LoaiDanhMuc WHERE sTenBang=@sTenBang) ORDER BY sTen";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@sTenBang", sTenBang);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();
            return vR;
        }
        public static SelectOptionList GetDDL(string sTenBang)
        {
            DataTable dt = GetDataTable(sTenBang);
            SelectOptionList  DDL = new SelectOptionList(dt, "iID_MaDanhMuc", "sTen");
            return DDL;
        }

        public static DataTable GetDataTable_Detail(string sTenBang, string sMa)
        {
            DataTable vR;
            string SQL = "SELECT * FROM CNN25_DanhMuc WHERE sTenKhoa=@sTenKhoa AND sMa=@sMa";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@sTenKhoa", sTenBang);
            cmd.Parameters.AddWithValue("@sMa", sMa);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();
            return vR;
        }

        public static string Get_Name_DanhMuc(string sTenBang, string sMa)
        {
            string vR = "";
            DataTable dt = GetDataTable_Detail(sTenBang, sMa);
            if(dt.Rows.Count > 0)
            {
                vR = Convert.ToString(dt.Rows[0]["sTen"]);
            }
            dt.Dispose();

            return vR;
        }

        public static DataTable GetDataTable_TenKhoa(string sTenBang, string sOrder = "sTen")
        {
            DataTable vR;
            string SQL = "SELECT * FROM CNN25_DanhMuc WHERE sTenKhoa=@sTenKhoa ORDER BY " + sOrder + "";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@sTenKhoa", sTenBang);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();
            return vR;
        }

        public static SelectOptionList GetDDL_TenKhoa(string sTenBang, bool TatCa = true)
        {
            DataTable dt = GetDataTable_TenKhoa(sTenBang);
            DataRow r;
            if (TatCa)
            {
                dt.Rows.InsertAt(dt.NewRow(), 0);
                dt.Rows[0]["sMa"] = "";
                dt.Rows[0]["sTen"] = "--- Tất cả ---";
            }
            SelectOptionList DDL = new SelectOptionList(dt, "sMa", "sTen");
            return DDL;
        }
    } 
}
