using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200026.WebServices.XmlType.Request
{
    public class RequestEditCerVM
    {
        [XmlElement("NSWFileCode")] public string NSWFileCode { get; set; }

        public string RequestDateString
        {
            get => RequestDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => RequestDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        [XmlIgnore] public DateTime RequestDate { get; set; }
        [XmlElement] public string Reason { get; set; }

        //[XmlArray("AttachmentList")]
        //[XmlArrayItem("Attachment")]
        //public virtual List<Attachment> ListAttachments { set; get; }
        [XmlElement] public string FileName { get; set; }
        [XmlElement] public string FileAttach { get; set; }
    }
}
