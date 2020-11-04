using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.WebServices.XmlType.Request
{
    public class TCCDGuiKetQuaKT
    {
        [XmlElement] public string NSWFileCode { get; set; }

        [XmlElement] public string AssignID { get; set; }

        [XmlElement] public string AssignName { get; set; }

        [XmlElement] public long GoodsId { get; set; }

        [XmlElement] public string NameOfGoods { get; set; }

        [XmlElement] public int ResultTest { get; set; }

        [XmlElement] public string TestConfirmNumber { get; set; }

        [XmlIgnore] public DateTime TestConfirmDate { get; set; }

        [XmlElement("TestConfirmDate")]
        public string TestConfirmDateString
        {
            get => TestConfirmDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => TestConfirmDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }

        [XmlElement] public string TestConfirmAttachmentId { get; set; }

        [XmlElement] public string TestConfirmFileName { get; set; }

        [XmlElement] public string TestConfirmFileLink { get; set; }

        [XmlArray("AttachmentResultList")]
        [XmlArrayItem("AttachmentResult")]
        public virtual List<AttachmentResult> ListAttachmentResults { set; get; }

    }

    public class AttachmentResult
    {
        public int FileCode { set; get; }
        public string AttachmentId { get; set; }
        public string FileName { get; set; }
        public string FileLink { get; set; }
    }
}
