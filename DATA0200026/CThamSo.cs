using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA0200026
{
    public class CThamSo
    {
        public static int iKetNoi = 0;

        public static DateTime Get_ServerDateTime()
        {
            SqlCommand cmd = new SqlCommand("SELECT GETDATE()");
            DateTime vR = Convert.ToDateTime(Connection.GetValue(cmd, iKetNoi));
            cmd.Dispose();
            return vR;
        }
    }
}
