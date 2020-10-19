﻿using DomainModel;
using System;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace DATA0200025
{
    public class clTaoSoGDK
    {
        public static object objlockgdk = new object();
        private static int STT { get; set; }
        private static DateTime Ngay { get; set; }
        private static clTaoSoGDK TaoSoGDK = null;
        public clTaoSoGDK()
        {

            if(TaoSoGDK==null)
            {
                TaoSoGDK = new clTaoSoGDK();
            }    

        }

        private void KhoiTao()
        {
            DateTime date = DateTime.Now;
            int inam =date.Year - Ngay.Year;
            if (inam > 0)
            {
                Ngay = date;
                string SQL = "SELECT iSTT FROM CNN25_TaoSoGDK WHERE inam=@inam";
                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@iNam", date.Year);
                STT = Convert.ToInt32(Connection.GetValue(cmd, 0));
                if(STT==0)
                {
                    cmd.CommandText = "INSERT INTO CNN25_TaoSoGDK(iNam,iSTT) VALUES(@iNam,1)";
                    cmd.Parameters.AddWithValue("@iNam", date.Year);
                    int vR = Connection.UpdateDatabase(cmd);
                     
                }    
                
            }
          
            
        }
        public string GetSoGDK()
        {
            //STT.DD.MM.YY.15.G10
            DateTime date = DateTime.Now;
            lock (objlockgdk)
            {
                int inam = date.Year - Ngay.Year;
                if (STT == 0 || inam > 0)
                {
                     TaoSoGDK.KhoiTao();
                }
                STT++;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@iNam", date.Year);
                cmd.Parameters.AddWithValue("@iSTT", STT);
                Connection.UpdateRecord("CNN25_TaoSoGDK", "iNam", cmd);
                cmd.Dispose();
                string soGDK = string.Format("{0}/{1}/GĐK-CN", STT, date.ToString("yy"));
                return soGDK;
            }

        }

    }
}