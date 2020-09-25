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
