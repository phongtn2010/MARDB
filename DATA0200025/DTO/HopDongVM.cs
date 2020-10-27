using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.DTO
{
    public class HopDongVM
    {
        [XmlIgnore]
        public long ContractId { set; get; }

        [XmlElement("ContractNo")]
        public string fiContractNo { set; get; }

        public DateTime fiContractDate { set; get; }
        [XmlElement("ContractDate"), NotMapped]
        public string fiContractDateString
        {
            get => fiContractDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => fiContractDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }

        [XmlElement("ContractAttachmentId")]
        public string fiContractAttachmentId { set; get; }

        [XmlElement("ContractName")]
        public string fiContractName { set; get; }

        [XmlElement("ContractFileLink")]
        public string fiContractFileLink { set; get; }

        [XmlIgnore]
        public long idHoSo { get; set; }
    }
}
