using DomainModel.Abstract;
using System;
using System.Collections.Generic;
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
                bang.DuLieuMoi = true;
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
                    bang.GiaTriKhoa = iID_MaFile_Sua;
                    bang.Save();

                    vR = iID_MaFile_Sua;
                }
                else
                {
                    vR = Convert.ToInt64(bang.Save());
                }
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }
    }
}
