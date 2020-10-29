using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.WebServices.XmlType.Request
{
    public class GiayXNCL
    {
        [XmlElement] public string NSWFileCode { get; set; }

        [XmlElement] public string DepartmentCode { get; set; }

        [XmlElement] public string DepartmentName { get; set; }

        [XmlElement] public string CerNumber { get; set; }

        [XmlElement] public string SignCerPlace { get; set; }

        [XmlElement] public DateTime SignCerDate { get; set; }
        [XmlElement("SignCerDate")]
        public string SignCerDateString
        {
            get => SignCerDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => SignCerDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }

        [XmlArray("AttachmentResultList")]
        [XmlArrayItem("AttachmentResult")]
        public virtual List<AttachmentResult> ListAttachmentResults { set; get; }

        [XmlElement] public string FileName { get; set; }

        [XmlElement] public string FileLink { get; set; }

        [XmlElement] public string NameOfStaff { get; set; }
    }
}
