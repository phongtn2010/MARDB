using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.DTO
{
    [XmlRoot("SendResultTest")]
    public class NopKetQuaVM
    {
        [XmlElement("NSWFileCode")]
        public string fiNSWFileCode { set; get; }

        [XmlElement("AssignCode")]
        public string fiAssignCode { set; get; }

        [XmlElement("AssignName")]
        public string fiAssignName { set; get; }

        [XmlElement("GoodsId")]
        public long fiGoodsId { set; get; }

        [XmlElement("NameOfGoods")]
        public string fiNameOfGoods { set; get; }

        [XmlElement("ResultTest")]
        public int fiResultTest { set; get; }

        [XmlElement("TestConfirmNumber")]
        public string fiTestConfirmNumber { set; get; }

        public DateTime fiTestConfirmDate { set; get; }
        [XmlElement("TestConfirmDate"), NotMapped]
        public string fiTestConfirmDateString
        {
            get => fiTestConfirmDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => fiTestConfirmDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }

        [XmlElement("TestConfirmAttachmentId")]
        public string fiTestConfirmAttachmentId { set; get; }

        [XmlElement("TestConfirmFileName")]
        public string fiTestConfirmFileName { set; get; }

        [XmlElement("TestConfirmFileLink")]
        public string fiTestConfirmFileLink { set; get; }

        [XmlArray("AttachmentResultList")]
        [XmlArrayItem("AttachmentResult")]
        public virtual List<AttachmentVM> ListAttachmentReports { set; get; }
    }
}
