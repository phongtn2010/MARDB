using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200026.DTO
{
    public class SoLuongVM
    {
        [XmlIgnore]
        public long QuantityId { set; get; }

        [XmlElement("Quantity")]
        public decimal fiQuantity { set; get; }

        [XmlElement("QuantityUnitCode")]
        public string fiQuantityUnitCode { set; get; }

        [XmlElement("QuantityUnitName")]
        public string fiQuantityUnitName { set; get; }

        [XmlElement("Volume")]
        public decimal fiVolume { set; get; }

        [XmlElement("VolumeUnitCode")]
        public string fiVolumeUnitCode { set; get; }

        [XmlElement("VolumeUnitName")]
        public string fiVolumeUnitName { set; get; }

        [XmlElement("VolumeTAN")]
        public decimal fiVolumeTAN { set; get; }

        [XmlIgnore]
        public long idHangHoa { get; set; }
    }
}
