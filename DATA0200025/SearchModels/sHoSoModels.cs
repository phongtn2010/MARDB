using DATA0200025.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA0200025.SearchModels
{
    public class sHoSoModels
    {

        public int LoaiDanhSach { get; set; }// dùng dể phân biệt loại danh sách chơ tiêp nhận, đã bổ dung hồ sơ....
        public int iID_MaTrangThai { get; set; }
        public string sMaHoSo { get; set; }
        public string sSoTiepNhan { get; set; }
        public string sSoGDK { get; set; }
        public string sTenDoanhNghiep { get; set; }
        public string sTenTACN { get; set; }
        public string sMaSoThue { get; set; }
        public string TuNgayDen { get; set; }
        public string DenNgayDen { get; set; }
        public string TuNgayTiepNhan { get; set; }
        public string DenNgayTiepNhan { get; set; }
        
        public string TuNgayXacNhan { get; set; }
        public string DenNgayXacNhan { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string dNgayTiepNhan { get; set; }
        public string FromDateTiepNhan { get; set; }
        public string ToDateTiepNhan { get; set; }
        public int iID_KetQuaXuLy { get; set; }

    }
}
