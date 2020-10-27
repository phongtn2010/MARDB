using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.DTO
{
    public class ChatLuongVM
    {
        [XmlIgnore]
        public long QualityCriteriaId { set; get; }

        [XmlElement("QualityCriteriaName")]
        public string fiQualityCriteriaName { set; get; }

        [XmlElement("QualityFormOfPublication")]
        public string fiQualityFormOfPublication { set; get; }

        [XmlElement("QualityRequire")]
        public decimal fiQualityRequire { set; get; }

        [XmlElement("QualityRequireUnitID")]
        public string fiQualityRequireUnitID { set; get; }

        [XmlElement("QualityRequireUnitName")]
        public string fiQualityRequireUnitName { set; get; }

        [XmlIgnore]
        public long idHangHoa { get; set; }
    }
}
