using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200026.DTO
{
    public class AnToanVM
    {
        [XmlIgnore]
        public long SafetyCriteriaId { set; get; }

        [XmlElement("SafetyCriteriaName")]
        public string fiSafetyCriteriaName { set; get; }

        [XmlElement("SafetyFormOfPublication")]
        public int fiSafetyFormOfPublication { set; get; }

        [XmlElement("SafetyRequire")]
        public decimal fiSafetyRequire { set; get; }

        [XmlElement("SafetyRequireUnitID")]
        public string fiSafetyRequireUnitID { set; get; }

        [XmlElement("SafetyRequireUnitName")]
        public string fiSafetyRequireUnitName { set; get; }

        [XmlIgnore]
        public long idHangHoa { get; set; }
    }
}
