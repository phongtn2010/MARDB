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
    }

 
}
