using DomainModel;
using System;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace DATA0200026
{
    public class CTaoSoGDK
    {
        public static object objlockgdk = new object();
        private static int STT { get; set; }
        private static DateTime Ngay { get; set; }
        public string SoGDK { get; set; }
        private static CTaoSoGDK TaoSoGDK = null;
        public CTaoSoGDK()
        {
        }

        private void KhoiTao()
        {
            DateTime date = DateTime.Now;
            int inam = date.Year - Ngay.Year;
            if (inam > 0)
            {
                Ngay = date;
                string SQL = "SELECT iSTT FROM CNN26_TaoSoGDK WHERE iNam=@iNam";
                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@iNam", date.Year);
                STT = Convert.ToInt32(Connection.GetValue(cmd, 0));
                if (STT == 0)
                {
                    cmd.CommandText = "INSERT INTO CNN26_TaoSoGDK(iNam,iSTT) VALUES(@iNam,1)";
                    int vR = Connection.UpdateDatabase(cmd);
                }
            }
        }
        public static CTaoSoGDK GetSoGDK()
        {
            //STT/YY/CN-TACN
            DateTime date = DateTime.Now;
            lock (objlockgdk)
            {
                if (TaoSoGDK == null)
                {
                    TaoSoGDK = new CTaoSoGDK();
                }
                int inam = date.Year - Ngay.Year;
                if (STT == 0 || inam > 0)
                {
                    TaoSoGDK.KhoiTao();
                }

                STT++;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@iNam", date.Year);
                cmd.Parameters.AddWithValue("@iSTT", STT);
                Connection.UpdateRecord("CNN26_TaoSoGDK", "iNam", cmd);
                cmd.Dispose();
                string soGDK = string.Format("{0}/{1}/CN-TACN", STT, date.ToString("yy"));
                TaoSoGDK.SoGDK = soGDK;
                return TaoSoGDK;
            }

        }
    }
}
