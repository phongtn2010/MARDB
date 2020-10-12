using System;
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
        public int iID_MaLoaiHoSo { get; set; }
        public int iID_MaHoSo_ThayThe { get; set; }
        public string sMaHoSo { get; set; }
        public string sUserTiepNhan { get; set; }
        public string sTenNguoiTiepNhan { get; set; }
        public string sTenDoanhNghiep { get; set; }
        public string sMaSoThue { get; set; }
        public DateTime dNgayTaoHoSo { get; set; }
    }
}
