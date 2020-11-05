using DomainModel;
using DomainModel.Abstract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA0200026
{
    public class CHoSo26
    {
        public static long ThemHoSo(long iID_MaHoSo_Sua, int iID_MaTrangThai, int iID_MaLoaiHoSo, 
            string sMaHoSo, DateTime dNgayTaoHoSo, bool bDaTiepNhan, string sSoTiepNhan, DateTime? dNgayTiepNhan,
            string sSoGDK, string sSoGDK_ThayThe, DateTime? dNgayXacNhan,
            string sMaSoThue, string sTenDoanhNghiep, string sLoaiHinhThucKiemTra, string sTenHangHoa,
            string sNoiLamHoSo, string sToChuc_Name, string sToChuc_DiaChi, string sToChuc_Tel, string sToChuc_Fax, string sToChuc_Email, 
            string sMaCoQuanXuLy, string sTenCoQuanXuLy,
            string sKyHoSo_MaTinh, string sKyHoSo_Tinh, string sKyHoSo_NguoiKy, string sKyHoSo_NguoiKy_ChucDanh,
            String sUserName, String sIP)
        {
            long vR = 0;

            try
            {
                String sHashCode = "";

                long iID_MaHoSo = 0;
                Bang bang = new Bang("CNN26_HoSo");
                bang.MaNguoiDungSua = sUserName;
                bang.IPSua = sIP;

                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", iID_MaTrangThai);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaLoaiHoSo", iID_MaLoaiHoSo);
                bang.CmdParams.Parameters.AddWithValue("@sMaHoSo", sMaHoSo);
                if (dNgayTaoHoSo != null)
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
                bang.CmdParams.Parameters.AddWithValue("@sTenTACN", sTenHangHoa);
                bang.CmdParams.Parameters.AddWithValue("@sNoiLamHoSo", sNoiLamHoSo);
                bang.CmdParams.Parameters.AddWithValue("@sToChuc_Name", sToChuc_Name);
                bang.CmdParams.Parameters.AddWithValue("@sToChuc_DiaChi", sToChuc_DiaChi);
                bang.CmdParams.Parameters.AddWithValue("@sToChuc_Tel", sToChuc_Tel);
                bang.CmdParams.Parameters.AddWithValue("@sToChuc_Fax", sToChuc_Fax);
                bang.CmdParams.Parameters.AddWithValue("@sToChuc_Email", sToChuc_Email);
                bang.CmdParams.Parameters.AddWithValue("@sMaCoQuanXuLy", sMaCoQuanXuLy);
                bang.CmdParams.Parameters.AddWithValue("@sTenCoQuanXuLy", sTenCoQuanXuLy);
                bang.CmdParams.Parameters.AddWithValue("@sKyHoSo_MaTinh", sKyHoSo_MaTinh);
                bang.CmdParams.Parameters.AddWithValue("@sKyHoSo_Tinh", sKyHoSo_Tinh);
                bang.CmdParams.Parameters.AddWithValue("@sKyHoSo_NguoiKy", sKyHoSo_NguoiKy);
                bang.CmdParams.Parameters.AddWithValue("@sKyHoSo_NguoiKy_ChucDanh", sKyHoSo_NguoiKy_ChucDanh);
                bang.CmdParams.Parameters.AddWithValue("@sHashCode", sHashCode);

                if (iID_MaHoSo_Sua > 0)
                {
                    bang.DuLieuMoi = false;
                    bang.GiaTriKhoa = iID_MaHoSo_Sua;
                    bang.Save();

                    iID_MaHoSo = iID_MaHoSo_Sua;
                }
                else
                {
                    bang.DuLieuMoi = true;
                    iID_MaHoSo = Convert.ToInt64(bang.Save());
                }

                vR = iID_MaHoSo;
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static int UpDate_TrangThai(long iID_MaHoSo, int iTrangThai)
        {
            int vR = 0;
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("UPDATE CNN26_HoSo SET iID_MaTrangThai=@iID_MaTrangThai WHERE iID_MaHoSo=@iID_MaHoSo");
                cmd.Parameters.AddWithValue("@iID_MaTrangThai", iTrangThai);
                cmd.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
                Connection.UpdateDatabase(cmd);

                vR = 1;
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }
    }
}
