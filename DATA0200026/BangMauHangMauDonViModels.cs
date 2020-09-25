using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using DomainModel;
using DomainModel.Abstract;

namespace DATA0200026
{
    public class BangMauHangMauDonViModels
    {
        public static void ThemNhanhChiTieuChoDonViTheoBangTruoc(String MaDonVi, String MaBangMau, String MaBangMauLayDuLieu, String sUserName, String sIP)
        {
            int i, j;
            SqlCommand cmd;
            //Lây dữ liệu từ bảng mẫu chọn để imports
            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM BC_BangMau_HangMau AS BMHM inner join BC_BangMau_HangMau_DonVi as BMHMDV on BMHM.iID_MaBangMau_HangMau = BMHMDV.iID_MaBangMau_HangMau " +
                                "WHERE BMHMDV.iID_MaDonVi = @iID_MaDonVi AND BMHMDV.iID_MaBangMau = @iID_MaBangMau";
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMauLayDuLieu);
            cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();

            cmd = new SqlCommand("DELETE FROM BC_BangMau_HangMau_DonVi WHERE iID_MaDonVi = @iID_MaDonVi AND iID_MaBangMau = @iID_MaBangMau");
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
            Connection.UpdateDatabase(cmd);
            cmd.Dispose();

            DataRow R;
            if (dt.Rows.Count > 0)
            {
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    R = dt.Rows[i];
                    cmd = new SqlCommand("SELECT iID_MaBangMau_HangMau FROM BC_BangMau_HangMau WHERE iID_MaBangMau=@iID_MaBangMau AND iID_MaHangMau = @iID_MaHangMau AND iID_MaHangMau_Cha=@iID_MaHangMau_Cha");
                    cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                    cmd.Parameters.AddWithValue("@iID_MaHangMau", R["iID_MaHangMau"]);
                    cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", R["iID_MaHangMau_Cha"]);
                    int MaBangMau_HangMauLay = Convert.ToInt32(Connection.GetValue(cmd, 0));
                    cmd.Dispose();

                    Bang bang = new Bang("BC_BangMau_HangMau_DonVi");
                    bang.MaNguoiDungSua = sUserName;
                    bang.IPSua = sIP;
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaBangMau_HangMau", MaBangMau_HangMauLay);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                    bang.Save();
                }
            }


        }
    }
}