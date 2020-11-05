using DomainModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA0200025
{
    public class CToChucChiDinh
    {
        #region ChungNhanHopQuy
        public static DataTable Get_ChungNhanHopQuy(long iID_MaHoSo, long iID_MaHangHoa)
        {
            DataTable vR = null;

            string SQL = "SELECT * FROM CNN25_ChungNhanHopQuy WHERE iID_MaHoSo=@iID_MaHoSo AND iID_MaHangHoa=@iID_MaHangHoa";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
            cmd.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
            vR = Connection.GetDataTable(cmd);
            cmd.Dispose();

            return vR;
        }
        #endregion
        #region KetQuaPhanTich
        public static DataTable Get_KetQuaPhanTich(long iID_MaHoSo, long iID_MaHangHoa)
        {
            DataTable vR = null;

            string SQL = "SELECT * FROM CNN25_KetQuaPhanTich WHERE iID_MaHoSo=@iID_MaHoSo AND iID_MaHangHoa=@iID_MaHangHoa";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
            cmd.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
            vR = Connection.GetDataTable(cmd);
            cmd.Dispose();

            return vR;
        }
        #endregion
    }
}
