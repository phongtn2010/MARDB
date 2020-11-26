using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA0200025.Models
{
    public class HoSoModels
    {
        public long iID_MaHoSo { get; set; }
        public int iID_MaTrangThai { get; set; }
        public int iID_MaTrangThaiTruoc { get; set; }
        public int iID_MaLoaiHoSo { get; set; }
        public long iID_MaHoSo_ThayThe { get; set; }
        public string sMaHoSo { get; set; }
        public string sMaHoSo_ThayThe { get; set; }
        public string sSoXacNhan_ThayThe { get; set; }
        public string sMaDinhKem_ThayThe { get; set; }
        public string sTenDinhKem_ThayThe { get; set; }
        public string sLinkDinhKem_ThayThe { get; set; }
        public string sCoQuanXuLy_Ma { get; set; }
        public string sCoQuanXuLy_Ten { get; set; }
        public string sSoGDK { get; set; }
        public string sSoGDK_ThayThe { get; set; }
        public DateTime dNgayKyGDK { get; set; }
        public string sNguoiKyGDK { get; set; }
        public string sUserTiepNhan { get; set; }
        public string sTenNguoiTiepNhan { get; set; }
        public string sTenDoanhNghiep { get; set; }
        public string sMaSoThue { get; set; }
        public DateTime dNgayTaoHoSo { get; set; }
        public DateTime dNgayTao { get; set; }
        public string sLoaiHinhThucKiemTra { get; set; }
        public string sDonViThucHienDanhGia { get; set; }
        public string sThoiGianDanhGia { get; set; }
        public DateTime dNgayXacNhan { get; set; }
        public string sBan_Name { get; set; }
        public string sBan_DiaChi { get; set; }
        public string sBan_Tel { get; set; }
        public string sBan_Fax { get; set; }
        public string sBan_QuocGia { get; set; }
        public string sBan_NoiXuat { get; set; }
        public string sMua_Name { get; set; }
        public string sMua_DiaChi { get; set; }
        public string sMua_Tel { get; set; }
        public string sMua_Fax { get; set; }
        public string sMua_Email { get; set; }
        public string sMua_NoiNhan { get; set; }
        public DateTime sMua_FromDate { get; set; }
        public DateTime sMua_ToDate { get; set; }
        public DateTime dDangKy_FromDate { get; set; }
        public DateTime dDangKy_ToDate { get; set; }
        public string sSoTiepNhan { get; set; }
        public string sKyHoSo_Tinh { get; set; }
        public string sKyHoSo_NguoiKy { get; set; }
        public string sKyHoSo_NguoiKy_ChucDanh { get; set; }
        public DateTime dKyHoSo_NgayKy { get; set; }
        public string sLienHe_Name { get; set; }
        public string sLieHe_DiaChi { get; set; }
        public string sLienHe_Tel { get; set; }
        public string sLienHe_Email { get; set; }
        public string sDiaDiemTapKet { get; set; }
        public string sDiaDiemDangKy { get; set; }
    }
}
