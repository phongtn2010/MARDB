using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA0200025.Models
{
    public class FileDinhKemModels
    {
        public long iID_MaFile { get; set; }
        public long iID_MaHoSo { get; set; }
        public long iID_MaHangHoa { get; set; }
        public int iID_MaLoaiFile { get; set; }
        public string sMaHoSo { get; set; }
        public string sTenLoaiFile { get; set; }
        public string sTenFile { get; set; }
        public string sHopDong { get; set; }
        public DateTime dNgayHopDong { get; set; }
        public string sDuongDan { get; set; }
    }
}
