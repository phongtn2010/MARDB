using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.DTO
{
    public class PhieuDongGoiVM
    {
        [XmlIgnore]
        public long PackingId { set; get; }

        [XmlElement("PackingNo")]
        public string fiPackingNo { set; get; }

        public DateTime fiPackingDate { set; get; }
        [XmlElement("PackingDate"), NotMapped]
        public string fiPackingDateString
        {
            get => fiPackingDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => fiPackingDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }

        [XmlElement("PackingAttachmentId")]
        public string fiPackingAttachmentId { set; get; }

        [XmlElement("PackingName")]
        public string fiPackingName { set; get; }

        [XmlElement("PackingFileLink")]
        public string fiPackingFileLink { set; get; }

        [XmlIgnore]
        public long idHoSo { get; set; }
    }
}
