﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA0200026
{
    public class HangHoa26Models
    {
        public long iID_MaHangHoa { get; set; }
        public long iID_MaHangHoa25 { get; set; }
        public long iID_MaHoSo { get; set; }
        public int iID_MaNhom { get; set; }
        public string iID_MaLoai { get; set; }
        public string iID_MaPhanLoai { get; set; }
        public string iID_MaDonViTinh { get; set; }
        public int iID_MaTrangThai { get; set; }
        public int iID_MaTrangThaiTruoc { get; set; }
        public string sMaHoSo { get; set; }
        public string sMaHoSo_DangKy { get; set; }
        public string sTenHangHoa { get; set; }
        public string sTenNhom { get; set; }
        public string sTenPhanNhom { get; set; }
        public string sTenPhanLoai { get; set; }
        public string sTenLoaiHangHoa { get; set; }
        public string sMaSoCongNhan { get; set; }
        public string sHangSanXuat { get; set; }
        public string sMaQuocGia { get; set; }
        public string sTenQuocGia { get; set; }
        public string sThanhPhan { get; set; }
        public string sMauSac { get; set; }
        public string sSoHieu { get; set; }
        public string sQuyChuan { get; set; }
        public string sDonViTinh { get; set; }
        public string sCVMienGiam { get; set; }
        public double rGiaVN { get; set; }
        public double rGiaUSD { get; set; }
        public string sGhiChu { get; set; }
        public string sSoThongBaoKetQua { get; set; }
    }

    public class ChatLuong26Models
    {
        public long iID_MaHangHoaCL { get; set; }
        public long iID_MaHangHoa { get; set; }
        public int iID_MaHinhThuc { get; set; }
        public string sChiTieu { get; set; }
        public string sHinhThuc { get; set; }
        public string sHamLuong { get; set; }
        public string sMaDonViTinh { get; set; }
        public string sDonViTinh { get; set; }
        public string sGhiChu { get; set; }
    }

    public class AnToan26Models
    {
        public long iID_MahangHoaAT { get; set; }
        public long iID_MaHangHoa { get; set; }
        public int iID_MaLoaiAnToan { get; set; }
        public int iID_MaHinhThuc { get; set; }
        public string sChiTieu { get; set; }
        public string sHinhThuc { get; set; }
        public string sHamLuong { get; set; }
        public string sMaDonViTinh { get; set; }
        public string sDonViTinh { get; set; }
        public string sGhiChu { get; set; }
    }
}
