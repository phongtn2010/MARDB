using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.DTO
{
    public class PhieuDongGoiVM
    {
        [XmlIgnore]
        public int PackingId { set; get; }

        [XmlElement("PackingNo")]
        public string fiPackingNo { set; get; }

        [XmlElement("PackingDate")]
        public DateTime fiPackingDate { set; get; }

        [XmlElement("PackingAttachmentId")]
        public string fiPackingAttachmentId { set; get; }

        [XmlElement("PackingName")]
        public string fiPackingName { set; get; }

        [XmlElement("PackingFileLink")]
        public string fiPackingFileLink { set; get; }

        [XmlIgnore]
        public int idHoSo { get; set; }
    }
}
