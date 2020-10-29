using DomainModel.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA0200025
{
    public class CHoSo
    {
        public static long ThemHoSo(long iID_MaHoSo_Sua, int iID_MaTrangThai, int iID_MaLoaiHoSo, long iID_MaHoSo_ThayThe, 
            string sMaHoSo, string sMaHoSo_ThayThe, DateTime dNgayTaoHoSo, bool bDaTiepNhan, string sSoTiepNhan, DateTime? dNgayTiepNhan,
            string sSoGDK, string sSoGDK_ThayThe, DateTime? dNgayXacNhan,
            string sMaSoThue, string sTenDoanhNghiep, string sLoaiHinhThucKiemTra, string sDonViThucHienDanhGia, string sTenHangHoa,
            string sBan_Name, string sBan_DiaChi, string sBan_Tel, string sBan_Fax, string sBan_Email, string sBan_MaQuocGia, string sBan_QuocGia, string sBan_NoiXuat,
            string sMua_Name , string sMua_DiaChi, string sMua_Tel, string sMua_Fax, string sMua_Email, string sMua_NoiNhan, DateTime? sMua_FromDate, DateTime? sMua_ToDate,
            string sDiaDiemTapKet, string sDiaDiemDangKy, DateTime dDangKy_FromDate, DateTime dDangKy_ToDate,
            string sLienHe_Name, string sLieHe_DiaChi, string sLienHe_Tel, string sLienHe_Email,
            string sKyHoSo_MaTinh, string sKyHoSo_Tinh, string sKyHoSo_NguoiKy, string sKyHoSo_NguoiKy_ChucDanh,
            String sUserName, String sIP)
        {
            long vR = 0;

            try
            {
                String sHashCode = "";

                long iID_MaHoSo = 0;
                Bang bang = new Bang("CNN25_HoSo");
                bang.MaNguoiDungSua = sUserName;
                bang.IPSua = sIP;
                bang.DuLieuMoi = true;
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", iID_MaTrangThai);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaLoaiHoSo", iID_MaLoaiHoSo);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaHoSo_ThayThe", iID_MaHoSo_ThayThe);
                bang.CmdParams.Parameters.AddWithValue("@sMaHoSo", sMaHoSo);
                bang.CmdParams.Parameters.AddWithValue("@sMaHoSo_ThayThe", sMaHoSo_ThayThe);
                if(dNgayTaoHoSo != null)
                {
                    bang.CmdParams.Parameters.AddWithValue("@dNgayTaoHoSo", dNgayTaoHoSo);
                }
                bang.CmdParams.Parameters.AddWithValue("@bDaTiepNhan", bDaTiepNhan);
                bang.CmdParams.Parameters.AddWithValue("@sSoTiepNhan", sSoTiepNhan);
                if (dNgayTiepNhan != null)
                {
                    bang.CmdParams.Parameters.AddWithValue("@dNgayTiepNhan", dNgayTiepNhan);
                }
                bang.CmdParams.Parameters.AddWithValue("@sSoGDK", sSoGDK);
                bang.CmdParams.Parameters.AddWithValue("@sSoGDK_ThayThe", sSoGDK_ThayThe);
                if (dNgayXacNhan != null)
                {
                    bang.CmdParams.Parameters.AddWithValue("@dNgayXacNhan", dNgayXacNhan);
                }
                bang.CmdParams.Parameters.AddWithValue("@sMaSoThue", sMaSoThue);
                bang.CmdParams.Parameters.AddWithValue("@sTenDoanhNghiep", sTenDoanhNghiep);
                bang.CmdParams.Parameters.AddWithValue("@sLoaiHinhThucKiemTra", sLoaiHinhThucKiemTra);
                bang.CmdParams.Parameters.AddWithValue("@sDonViThucHienDanhGia", sDonViThucHienDanhGia);
                bang.CmdParams.Parameters.AddWithValue("@sTenTACN", sTenHangHoa);
                bang.CmdParams.Parameters.AddWithValue("@sBan_Name", sBan_Name);
                bang.CmdParams.Parameters.AddWithValue("@sBan_DiaChi", sBan_DiaChi);
                bang.CmdParams.Parameters.AddWithValue("@sBan_Tel", sBan_Tel);
                bang.CmdParams.Parameters.AddWithValue("@sBan_Fax", sBan_Fax);
                bang.CmdParams.Parameters.AddWithValue("@sBan_Email", sBan_Email);
                bang.CmdParams.Parameters.AddWithValue("@sBan_MaQuocGia", sBan_MaQuocGia);
                bang.CmdParams.Parameters.AddWithValue("@sBan_QuocGia", sBan_QuocGia);
                bang.CmdParams.Parameters.AddWithValue("@sBan_NoiXuat", sBan_NoiXuat);
                bang.CmdParams.Parameters.AddWithValue("@sMua_Name", sMua_Name);
                bang.CmdParams.Parameters.AddWithValue("@sMua_DiaChi", sMua_DiaChi);
                bang.CmdParams.Parameters.AddWithValue("@sMua_Tel", sMua_Tel);
                bang.CmdParams.Parameters.AddWithValue("@sMua_Fax", sMua_Fax);
                bang.CmdParams.Parameters.AddWithValue("@sMua_Email", sMua_Email);
                bang.CmdParams.Parameters.AddWithValue("@sMua_NoiNhan", sMua_NoiNhan);
                if (sMua_FromDate != null)
                {
                    bang.CmdParams.Parameters.AddWithValue("@sMua_FromDate", sMua_FromDate);
                }
                if (sMua_ToDate != null)
                {
                    bang.CmdParams.Parameters.AddWithValue("@sMua_ToDate", sMua_ToDate);
                }
                bang.CmdParams.Parameters.AddWithValue("@sDiaDiemTapKet", sDiaDiemTapKet);
                bang.CmdParams.Parameters.AddWithValue("@sDiaDiemDangKy", sDiaDiemDangKy);
                if (dDangKy_FromDate != null)
                {
                    if(dDangKy_FromDate.Year > 2000)
                    {
                        bang.CmdParams.Parameters.AddWithValue("@dDangKy_FromDate", dDangKy_FromDate);
                    }
                }
                if (dDangKy_ToDate != null)
                {
                    if (dDangKy_ToDate.Year > 2000)
                    {
                        bang.CmdParams.Parameters.AddWithValue("@dDangKy_ToDate", dDangKy_ToDate);
                    }
                }
                bang.CmdParams.Parameters.AddWithValue("@sLienHe_Name", sLienHe_Name);
                bang.CmdParams.Parameters.AddWithValue("@sLieHe_DiaChi", sLieHe_DiaChi);
                bang.CmdParams.Parameters.AddWithValue("@sLienHe_Tel", sLienHe_Tel);
                bang.CmdParams.Parameters.AddWithValue("@sLienHe_Email", sLienHe_Email);
                bang.CmdParams.Parameters.AddWithValue("@sKyHoSo_MaTinh", sKyHoSo_MaTinh);
                bang.CmdParams.Parameters.AddWithValue("@sKyHoSo_Tinh", sKyHoSo_Tinh);
                bang.CmdParams.Parameters.AddWithValue("@sKyHoSo_NguoiKy", sKyHoSo_NguoiKy);
                bang.CmdParams.Parameters.AddWithValue("@sKyHoSo_NguoiKy_ChucDanh", sKyHoSo_NguoiKy_ChucDanh);
                bang.CmdParams.Parameters.AddWithValue("@sHashCode", sHashCode);

                if(iID_MaHoSo_Sua > 0)
                {
                    bang.GiaTriKhoa = iID_MaHoSo_Sua;
                    bang.Save();
                }
                else
                {
                    iID_MaHoSo = Convert.ToInt64(bang.Save());
                }
                
                vR = iID_MaHoSo;
            }
            catch(Exception ex)
            {
                vR = -1;
            }

            return vR;
        }
    }
}
