using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using DomainModel;

namespace DATA0200025
{
    public class BangMauModels
    {
        private static Int32 LayHangMauGocTheoCay(DataTable dtHangMau, DataTable dtBangMau_HangMau, String MaBangMau, int MaHangMauCha, int MaHangMauCha_ChungHangMauCon)
        {
            Int32 vR = -1;
            int i, j, k, MaHangMau, MaHangMau_ChungHangMauCon;
            DataRow Row;

            for (i = 0; i < dtHangMau.Rows.Count; i++)
            {
                Row = dtHangMau.Rows[i];
                if (MaHangMauCha_ChungHangMauCon == Convert.ToInt32(Row["iID_MaHangMau_Cha"]))
                {
                    MaHangMau = Convert.ToInt32(Row["iID_MaHangMau"]);
                    MaHangMau_ChungHangMauCon = MaHangMau;
                    if (Convert.ToInt32(Row["iID_MaHangMau_ChungHangMauCon"]) > 0)
                    {
                        MaHangMau_ChungHangMauCon = Convert.ToInt32(Row["iID_MaHangMau_ChungHangMauCon"]);
                    }
                    for (j = 0; j < dtBangMau_HangMau.Rows.Count; j++)
                    {
                        if (MaHangMau == Convert.ToInt32(dtBangMau_HangMau.Rows[j]["iID_MaHangMau"]) &&
                            MaHangMauCha == Convert.ToInt32(dtBangMau_HangMau.Rows[j]["iID_MaHangMau_Cha"]))
                        {
                            return Convert.ToInt32(MaHangMauCha);
                        }
                    }
                    vR = LayHangMauGocTheoCay(dtHangMau, dtBangMau_HangMau, MaBangMau, MaHangMau, MaHangMau_ChungHangMauCon);
                    if (vR >= 0)
                    {
                        return vR;
                    }
                }
            }
            return vR;
        }

        public static void CapNhapMaHangMauChaGoc(String MaBangMau)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM BC_HangMau ORDER BY iSTT";
            DataTable dtHangMau = Connection.GetDataTable(cmd);
            cmd.Dispose();

            cmd = new SqlCommand();

            cmd.CommandText = "SELECT * " +
                                "FROM BC_BangMau_HangMau " +
                                "WHERE iID_MaBangMau=@iID_MaBangMau";
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            DataTable dtBangMau_HangMau = Connection.GetDataTable(cmd);
            cmd.Dispose();

            Int32 MaHangMauGoc = LayHangMauGocTheoCay(dtHangMau, dtBangMau_HangMau, MaBangMau, 0, 0);
            cmd = new SqlCommand("UPDATE BC_BangMau SET iID_MaHangMauGoc = @iID_MaHangMauGoc WHERE iID_MaBangMau=@iID_MaBangMau");
            cmd.Parameters.AddWithValue("@iID_MaHangMauGoc", MaHangMauGoc);
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            Connection.UpdateDatabase(cmd);
            cmd.Dispose();
            dtHangMau.Dispose();
            dtBangMau_HangMau.Dispose();
        }
    }
}