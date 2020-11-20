using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA0200025.Models
{
  public  class GiayChungNhanHopQuyModels
    {
        public int iID_ChungNhanHopQuy { get; set; }
        public long iID_MaHoSo { get; set; }
        public long iID_MaHangHoa { get; set; }
        public string sSoChungNhan { get; set; }
        public DateTime dNgayCap { get; set; }
        public string sTenFile { get; set; }
        public string sDuongDan { get; set; }
        public bool bKetQuaDanhGia { get; set; }
    }
}
