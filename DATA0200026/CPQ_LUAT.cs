using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DomainModel;
using DomainModel.Controls;

namespace DATA0200026
{
    public class CPQ_LUAT
    {
        public static DataTable Get_Table()
        {
            DataTable vR;
            string SQL = "SELECT * FROM PQ_Luat ORDER BY sTen";
            vR = Connection.GetDataTable(SQL, CThamSo.iKetNoi);
            return vR;
        }

        public static DataTable Get_One_Table(string iID_MaLuat)
        {
            DataTable vR = null;

            string SQL = "SELECT * FROM PQ_Luat WHERE iID_MaLuat = @iID_MaLuat";

            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaLuat", iID_MaLuat);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();

            return vR;
        }

        public static String Get_Name(string iID_MaLuat)
        {
            String vR = "";

            DataTable dt = Get_One_Table(iID_MaLuat);
            if(dt.Rows.Count > 0)
            {
                vR = Convert.ToString(dt.Rows[0]["sTen"]);
            }
            dt.Dispose();

            return vR;
        }
    }
}
