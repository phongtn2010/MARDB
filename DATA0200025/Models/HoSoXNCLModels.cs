using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA0200025.Models
{
    public class HoSoXNCLModels
    {
        public int iID_MaHoSoXNCL { get; set; }
        public int iID_MaHangHoa { get; set; }
        public int iID_MaHoSo { get; set; }    
        public int iID_MaToChuc { get; set; }
        public string sMaHoSo { get; set; }
        public string sTenHangHoa { get; set; }
        public string sTenToChuc { get; set; }
        public string sGiayChungNhan { get; set; }
        public DateTime dNgayCap { get; set; }
        public int iKetQua { get; set; }
        public string sMaFileChungNhan { get; set; }
       
        public string sTenFileChungNhan { get; set; }
        public string sLinkFileChungNhan { get; set; }
        
    }
}
