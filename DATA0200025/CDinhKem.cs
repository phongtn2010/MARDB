using DomainModel;
using DomainModel.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA0200025
{
    public class CDinhKem
    {
        public static long ThemDinhKem(long iID_MaHoSo, long iID_MaHangHoa, int iID_MaLoaiFile, string iID_MaTapTin, string sMaHoSo, string sTenLoaiFile, string sTenFile, string sHopDong, DateTime? dNgayHopDong, int iHoatDong, string sDuongDan,
            String sUserName, String sIP, long iID_MaFile_Sua = 0)
        {
            long vR = 0;

            try
            {
                Bang bang = new Bang("CNN25_DinhKem");
                bang.MaNguoiDungSua = sUserName;
                bang.IPSua = sIP;
                bang.CmdParams.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaLoaiFile", iID_MaLoaiFile);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTapTin", iID_MaTapTin);
                bang.CmdParams.Parameters.AddWithValue("@sMaHoSo", sMaHoSo);
                bang.CmdParams.Parameters.AddWithValue("@sTenLoaiFile", sTenLoaiFile);
                bang.CmdParams.Parameters.AddWithValue("@sTenFile", sTenFile);
                bang.CmdParams.Parameters.AddWithValue("@sHopDong", sHopDong);
                if(dNgayHopDong != null)
                {
                    bang.CmdParams.Parameters.AddWithValue("@dNgayHopDong", dNgayHopDong);
                }
                bang.CmdParams.Parameters.AddWithValue("@iHoatDong", iHoatDong);
                bang.CmdParams.Parameters.AddWithValue("@sDuongDan", sDuongDan);

                if(iID_MaFile_Sua > 0)
                {
                    int iTonTai = Check_TonTai(iID_MaFile_Sua);
                    if(iTonTai == 1)
                    {
                        bang.DuLieuMoi = false;
                        bang.GiaTriKhoa = iID_MaFile_Sua;
                        bang.Save();

                        vR = iID_MaFile_Sua;
                    }
                    else
                    {
                        bang.DuLieuMoi = true;
                        vR = Convert.ToInt64(bang.Save());
                    }    
                }
                else
                {
                    bang.DuLieuMoi = true;
                    vR = Convert.ToInt64(bang.Save());
                }
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static DataTable Get_Table_Detail(long iID_MaFile)
        {
            DataTable vR;

            string SQL = "SELECT * FROM CNN25_DinhKem WHERE iID_MaFile=@iID_MaFile";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaFile", iID_MaFile);
            vR = Connection.GetDataTable(cmd);
            cmd.Dispose();

            return vR;
        }

        public static int Check_TonTai(long iID_MaFile)
        {
            int vR = 0;

            DataTable dt = Get_Table_Detail(iID_MaFile);
            if(dt.Rows.Count > 0)
            {
                vR = 1;
            }
            else
            {
                vR = -1;
            }
            dt.Dispose();

            return vR;
        }

        public static void Delete_DinhKem(long iID_MaHoSo)
        {
            try
            {
                SqlCommand cmd;

                //Xoa DinhKem
                cmd = new SqlCommand("DELETE FROM CNN25_DinhKem WHERE iID_MaHoSo=@iID_MaHoSo");
                cmd.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
                Connection.UpdateDatabase(cmd);
                cmd.Dispose();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
