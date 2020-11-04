using DomainModel;
using System;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace DATA0200025
{
    public class clTaoSoThongBao
    {
        public static object objlocktb = new object();
        private static int STT { get; set; }
        private static DateTime Ngay { get; set; }
        public string SoTB { get; set; }
        private static clTaoSoThongBao TaoSoTB = null;
        public clTaoSoThongBao()
        {



        }

        private void KhoiTao()
        {
            DateTime date = DateTime.Now;
            int inam = date.Year - Ngay.Year;
            if (inam > 0)
            {
                Ngay = date;
                string SQL = "SELECT iSTT FROM CNN25_TaoSoThongBao WHERE iNam=@iNam";
                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@iNam", date.Year);
                STT = Convert.ToInt32(Connection.GetValue(cmd, 0));
                if (STT == 0)
                {
                    cmd.CommandText = "INSERT INTO CNN25_TaoSoThongBao(iNam,iSTT) VALUES(@iNam,1)";
                    int vR = Connection.UpdateDatabase(cmd);

                }

            }


        }
        public static clTaoSoThongBao GetSoTB()
        {
            //STT.DD.MM.YY.15.G10
            DateTime date = DateTime.Now;
            lock (objlocktb)
            {
                if (TaoSoTB == null)
                {
                    TaoSoTB = new clTaoSoThongBao();
                }
                int inam = date.Year - Ngay.Year;
                if (STT == 0 || inam > 0)
                {

                    TaoSoTB.KhoiTao();
                }
                STT++;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@iNam", date.Year);
                cmd.Parameters.AddWithValue("@iSTT", STT);
                Connection.UpdateRecord("CNN25_TaoSoThongBao", "iNam", cmd);
                cmd.Dispose();
                string soTB = string.Format("{0}/{1}/TB-CN", STT, date.ToString("yy"));
                TaoSoTB.SoTB = soTB;
                return TaoSoTB;
            }

        }
    }
}
