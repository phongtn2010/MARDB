using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace DATA0200026
{
    public class CLichSuHoSo
    {
        public static void Add(long iID_MaHoSo, string sMaHoSo, string MaND, string sNguoiXuLy, int iID_MaDoiTuong, string sTenDoiTuong, 
            int iID_MaHanhDong, string sTenHanhDong, string sNoiDung, string sFile, int iID_MaTrangThaiTruoc, string sTenTrangThaiTruoc, 
            int iID_MaTrangThai, string sTenTrangThai)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
                cmd.Parameters.AddWithValue("@sMaHoSo", sMaHoSo);
                cmd.Parameters.AddWithValue("@sMaND", MaND);
                cmd.Parameters.AddWithValue("@sNguoiXuLy", sNguoiXuLy);
                cmd.Parameters.AddWithValue("@iID_MaDoiTuong", iID_MaDoiTuong);
                cmd.Parameters.AddWithValue("@sTenDoiTuong", sTenDoiTuong);
                cmd.Parameters.AddWithValue("@iID_MaHanhDong", iID_MaHanhDong);
                cmd.Parameters.AddWithValue("@sTenHanhDong", sTenHanhDong);
                cmd.Parameters.AddWithValue("@sNoiDung", sNoiDung ?? "");
                cmd.Parameters.AddWithValue("@sFile", sFile);
                cmd.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", iID_MaTrangThaiTruoc);
                cmd.Parameters.AddWithValue("@sTenTrangThaiTruoc", sTenTrangThaiTruoc);
                cmd.Parameters.AddWithValue("@iID_MaTrangThai", iID_MaTrangThai);
                cmd.Parameters.AddWithValue("@sTenTrangThai", sTenTrangThai);
                Connection.InsertRecord("CNN26_LichSuHoSo", cmd, CThamSo.iKetNoi);
                cmd.Dispose();
            }
            catch (Exception ex)
            { }
        }

        public static DataTable GetDataTable(long iID_MaHoSo)
        {
            string SQL = "SELECT * FROM CNN26_LichSuHoSo WHERE iID_MaHoSo=@iID_MaHoSo ORDER BY dThoiGian DESC";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
            DataTable dt = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();
            return dt;
        }
    }
}
