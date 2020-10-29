using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.DTO
{
    [XmlRoot("Report")]
    public class UploadBaoCaoVM
    {
        [XmlElement("NSWFileCode")]
        public string fiNSWFileCode { set; get; }

        [XmlArray("AttachmentReportList")]
        [XmlArrayItem("AttachmentReport")]
        public virtual List<AttachmentVM> ListAttachmentReports { set; get; }
    }
}
