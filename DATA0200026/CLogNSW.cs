using DomainModel;
using DomainModel.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA0200026
{
    public class CLogNSW
    {
        public static void Add(string sChucNang, string sID_MaNguoiDung, string sMaHoSo, string sNoiDung, string sTrangThai, string sXML, String sUserName, String sIP)
        {
            try
            {
                Bang bang = new Bang("CNN26_Log_NSW");
                bang.MaNguoiDungSua = sUserName;
                bang.IPSua = sIP;
                bang.DuLieuMoi = true;
                bang.CmdParams.Parameters.AddWithValue("@sChucNang", sChucNang);
                bang.CmdParams.Parameters.AddWithValue("@sID_MaNguoiDung", sID_MaNguoiDung);
                bang.CmdParams.Parameters.AddWithValue("@sMaHoSo", sMaHoSo);
                bang.CmdParams.Parameters.AddWithValue("@sNoiDung", sNoiDung);
                bang.CmdParams.Parameters.AddWithValue("@sTrangThai", sTrangThai);
                bang.CmdParams.Parameters.AddWithValue("@sXML", sXML);

                bang.Save();
            }
            catch (Exception ex)
            {

            }
        }

        public static DataTable Get_All(string sTieuDe, string _FromDate, string _ToDate, int Trang, int SoBanGhi)
        {
            DataTable vR;

            string sDk = "1=1 ";
            SqlCommand cmd = new SqlCommand();
            if (!string.IsNullOrEmpty(sTieuDe))
            {
                sDk = sDk + " AND ((sMaHoSo LIKE N'%' + @sTieuDe + '%') OR (sChucNang LIKE N'%' + @sTieuDe + '%')) ";
                cmd.Parameters.AddWithValue("@sTieuDe", sTieuDe);
            }
            if (!(string.IsNullOrEmpty(_FromDate) || (_FromDate == "")))
            {
                sDk = sDk + " AND dNgayTao >= @_FromDate";
                cmd.Parameters.AddWithValue("@_FromDate", CommonFunction.LayNgayTuXau(_FromDate));
            }
            if (!(string.IsNullOrEmpty(_ToDate) || (_ToDate == "")))
            {
                sDk = sDk + " AND dNgayTao <= @_ToDate";
                cmd.Parameters.AddWithValue("@_ToDate", CommonFunction.LayNgayTuXau(_ToDate));
            }
            string SQL = string.Format("SELECT * FROM CNN26_Log_NSW WHERE {0}", sDk);
            cmd.CommandText = SQL;
            vR = CommonFunction.dtData(cmd, "dNgayTao DESC", Trang, SoBanGhi, 0);
            cmd.Dispose();

            return vR;
        }

        public static int Get_Count(string sTieuDe, string _FromDate, string _ToDate)
        {
            int vR = 0;

            string sDk = "1=1 ";
            SqlCommand cmd = new SqlCommand();
            if (!string.IsNullOrEmpty(sTieuDe))
            {
                sDk = sDk + " AND ((sMaHoSo LIKE N'%' + @sTieuDe + '%') OR (sChucNang LIKE N'%' + @sTieuDe + '%')) ";
                cmd.Parameters.AddWithValue("@sTieuDe", sTieuDe);
            }
            if (!(string.IsNullOrEmpty(_FromDate) || (_FromDate == "")))
            {
                sDk = sDk + " AND dNgayTao >= @_FromDate";
                cmd.Parameters.AddWithValue("@_FromDate", CommonFunction.LayNgayTuXau(_FromDate));
            }
            if (!(string.IsNullOrEmpty(_ToDate) || (_ToDate == "")))
            {
                sDk = sDk + " AND dNgayTao <= @_ToDate";
                cmd.Parameters.AddWithValue("@_ToDate", CommonFunction.LayNgayTuXau(_ToDate));
            }
            string SQL = string.Format("SELECT Count(*) FROM CNN26_Log_NSW WHERE {0}", sDk);
            cmd.CommandText = SQL;
            vR = Convert.ToInt32(Connection.GetValue(cmd, 0, 0));
            cmd.Dispose();

            return vR;
        }
    }
}
