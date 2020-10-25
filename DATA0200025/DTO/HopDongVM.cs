using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.DTO
{
    public class HopDongVM
    {
        [XmlIgnore]
        public int ContractId { set; get; }

        [XmlElement("ContractNo")]
        public string fiContractNo { set; get; }

        [XmlElement("ContractDate")]
        public DateTime fiContractDate { set; get; }

        [XmlElement("ContractAttachmentId")]
        public string fiContractAttachmentId { set; get; }

        [XmlElement("ContractName")]
        public string fiContractName { set; get; }

        [XmlElement("ContractFileLink")]
        public string fiContractFileLink { set; get; }

        [XmlIgnore]
        public int idHoSo { get; set; }
    }
}
