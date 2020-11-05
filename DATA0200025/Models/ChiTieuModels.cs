using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA0200025.Models
{
    public class ChiTieuModels
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
        public bool bChon { get; set; }
    }
}
