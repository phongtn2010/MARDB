﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA0200025.Models
{
    public class HoSoModels
    {
        public int iID_MaHoSo { get; set; }
        public int iID_MaTrangThai { get; set; }
        public int iID_MaTrangThaiTruoc { get; set; }
        public int iID_MaLoaiHoSo { get; set; }
        public int iID_MaHoSo_ThayThe { get; set; }
        public string sMaHoSo { get; set; }
        public string sMaHoSo_ThayThe { get; set; }
        public string sSoGDK { get; set; }
        public string sSoGDK_ThayThe { get; set; }
        public string sUserTiepNhan { get; set; }
        public string sTenNguoiTiepNhan { get; set; }
        public string sTenDoanhNghiep { get; set; }
        public string sMaSoThue { get; set; }
        public DateTime dNgayTaoHoSo { get; set; }
        public DateTime dNgayTao { get; set; }
        public string sLoaiHinhThucKiemTra { get; set; }
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
    }
}
