using DATA0200025.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.DTO.Extensions
{
    [XmlRoot("RequestProEdit")]
    public class RequestProEditVM
    {
        [XmlIgnore] public DateTime RequestDate { get; set; }
        [XmlElement("RequestDate"), NotMapped]

        public string RequestDateString
        {
            get => RequestDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => RequestDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        public string Reason { get; set; }
        [XmlElement("RegistrationProfile")]
        public virtual HoSoVM HoSo { set; get; }

        [XmlArray("AttachmentList")]
        [XmlArrayItem("Attachment")]
        public virtual List<Attachment> ListAttachments { set; get; }
    }
}
