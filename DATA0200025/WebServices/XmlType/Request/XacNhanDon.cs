using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.WebServices.XmlType.Request
{
    public class XacNhanDon
    {
        [XmlElement] public string NSWFileCode { get; set; }

        [XmlElement] public string AniFeedConfirmNo { get; set; }

        [XmlElement] public string AttachmentId { get; set; }

        [XmlElement] public string FileName { get; set; }

        [XmlElement] public string FileLink { get; set; }

        [XmlElement] public string NameOfStaff { get; set; }

        [XmlIgnore] public DateTime ResponseDate { get; set; }

        [XmlElement("ResponseDate")]
        public string ResponseDateString
        {
            get => ResponseDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => ResponseDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
    }
}
