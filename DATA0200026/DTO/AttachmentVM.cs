using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200026.DTO
{
    public class AttachmentVM
    {
        [XmlIgnore]
        public int AttachmentId { set; get; }

        [XmlElement("FileCode")]
        public int fiFileCode { set; get; }

        [XmlElement("AttachmentId")]
        public string fiAttachmentId { set; get; }

        [XmlElement("FileName")]
        public string fiFileName { set; get; }

        [XmlElement("FileLink")]
        public string fiFileLink { set; get; }

        [XmlIgnore]
        public int idHoSo { get; set; }
    }
}
