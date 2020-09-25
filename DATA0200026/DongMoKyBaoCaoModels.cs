using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using DomainModel;
using DomainModel.Controls;
using DomainModel.Abstract;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using System.Threading;
using System.Text;

namespace DATA0200026
{
    public class DongMoKyBaoCaoModels
    {
        public static void GhiNhatKyDongMoKy(String MaBang, int MaLoaiDongMoKy, String LyDo, String sUserName, String sIP)
        {
            Bang bang = new Bang("BC_BangMau_MoKy_NhatKy");
            bang.MaNguoiDungSua = sUserName;
            bang.IPSua = sIP;
            bang.CmdParams.Parameters.AddWithValue("@iID_MaBang", MaBang);
            bang.CmdParams.Parameters.AddWithValue("@iID_LoaiDongMoKy", MaLoaiDongMoKy);
            bang.CmdParams.Parameters.AddWithValue("@sLyDo", LyDo);
            bang.DuLieuMoi = true;
            bang.Save();
        }

        public static Boolean DuocPhepMoKy(String MaNguoiDung)
        {
            Boolean vR = false;
            SqlCommand cmd;
            String SQL = "SELECT iID_MaLuat " +
                            "FROM QT_NguoiDung INNER JOIN PQ_NhomNguoiDung_Luat ON QT_NguoiDung.iID_MaNhomNguoiDung = PQ_NhomNguoiDung_Luat.iID_MaNhomNguoiDung " +
                            "WHERE sID_MaNguoiDung = @sID_MaNguoiDung";
            cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@sID_MaNguoiDung", MaNguoiDung);
            String MaLuat = Connection.GetValueString(cmd, "");
            cmd.Dispose();
            String MaNhomNguoiDung = BaoMat.LayMaNhomNguoiDung(MaNguoiDung);
            if (String.IsNullOrEmpty(MaLuat) == false || BaoMat.KiemTraNhomNguoiDungQuanTri(MaNhomNguoiDung))
            {
                SQL = "SELECT iID_MaMenuItem FROM MENU_MenuItem WHERE  bHoatDong = 1 AND sTen = N'Mở kỳ báo cáo'";
                cmd = new SqlCommand(SQL);
                String MaMenuItem = Connection.GetValueString(cmd, "");
                cmd.Dispose();
                if (MaMenuItem != "")
                {
                    SQL = "SELECT count(*) FROM PQ_MenuItem_Cam WHERE  iID_MaMenuItem = @MaMenuItem AND iID_MaLuat = @iID_MaLuat";
                    cmd = new SqlCommand(SQL);
                    cmd.Parameters.AddWithValue("@MaMenuItem", MaMenuItem);
                    cmd.Parameters.AddWithValue("@iID_MaLuat", MaLuat);
                    if (Convert.ToInt32(Connection.GetValue(cmd, 0)) == 0)
                    {
                        vR = true;
                    }
                    cmd.Dispose();
                }
            }
            return vR;
        }
    }
}