using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.DTO
{
    public class HoaDonVM
    {
        [XmlIgnore]
        public int InvoiceId { set; get; }

        [XmlElement("InvoiceNo")]
        public string fiInvoiceNo { set; get; }

        [XmlElement("InvoiceDate")]
        public DateTime fiInvoiceDate { set; get; }

        [XmlElement("InvoiceAttachmentId")]
        public string fiInvoiceAttachmentId { set; get; }

        [XmlElement("InvoiceName")]
        public string fiInvoiceName { set; get; }

        [XmlElement("InvoiceFileLink")]
        public string fiInvoiceFileLink { set; get; }

        [XmlIgnore]
        public int idHoSo { get; set; }
    }
}
