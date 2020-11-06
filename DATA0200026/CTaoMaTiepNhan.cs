using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA0200026
{
    public class CTaoMaTiepNhan
    {
        public static object objlock = new object();
        public static int STT { get; set; }
        public static DateTime Ngay { get; set; }
        private static CTaoMaTiepNhan TaoMaTiepNhan = null;
        private CTaoMaTiepNhan()
        {

            DateTime date = DateTime.Now;
            TimeSpan time = date - Ngay;
            if (time.Days > 0)
            {
                Ngay = date;
                string SQL = "SELECT iSTT FROM CNN25_TaoSoTiepNhan WHERE dNgay=@dNgay";
                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@dNgay", date.ToShortDateString());
                STT = Convert.ToInt32(Connection.GetValue(cmd, 0));
                if (STT == 0)
                {
                    cmd.CommandText = "INSERT INTO CNN25_TaoSoTiepNhan(dNgay,iSTT) VALUES(@dNgay,1)";
                    Connection.UpdateDatabase(cmd);

                }
                cmd.Dispose();
            }



        }


        public static string GetSoTiepNhan()
        {
            //STT.DD.MM.YY.15.G10
            DateTime date = DateTime.Now;
            lock (objlock)
            {

                TimeSpan time = date - Ngay;
                if (STT == 0 || time.Days > 0)
                {
                    new CTaoMaTiepNhan();
                }
                STT++;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@dNgay", date.ToShortDateString());
                cmd.Parameters.AddWithValue("@iSTT", STT);
                Connection.UpdateRecord("CNN25_TaoSoTiepNhan", "dNgay", cmd);
                cmd.Dispose();
                string soTiepNhan = string.Format("{0}.{1}.15.G10", STT, date.ToString("dd.MM.yy"));
                return soTiepNhan;
            }

        }
    }
}
