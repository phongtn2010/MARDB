using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.DTO
{
    [XmlRoot("RequestCancel")]
    public class YeuCauHuyHoSoVM
    {
        [XmlElement("NSWFileCode")]
        public string fiNSWFileCode { set; get; }

        public DateTime fiRequestDate { set; get; }
        [XmlElement("RequestDate"), NotMapped]
        public string fiRequestDateString
        {
            get => fiRequestDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => fiRequestDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }

        [XmlElement("Reason")]
        public string fiReason { set; get; }

        [XmlElement("AttachmentId")]
        public string fiAttachmentId { set; get; }

        [XmlElement("FileName")]
        public string fiFileName { set; get; }

        [XmlElement("FileLink")]
        public string fiFileLink { set; get; }
    }
}
