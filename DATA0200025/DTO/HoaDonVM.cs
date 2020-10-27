using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.DTO
{
    public class HoaDonVM
    {
        [XmlIgnore]
        public long InvoiceId { set; get; }

        [XmlElement("InvoiceNo")]
        public string fiInvoiceNo { set; get; }

        public DateTime fiInvoiceDate { set; get; }
        [XmlElement("InvoiceDate"), NotMapped]
        public string fiInvoiceDateString
        {
            get => fiInvoiceDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => fiInvoiceDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }

        [XmlElement("InvoiceAttachmentId")]
        public string fiInvoiceAttachmentId { set; get; }

        [XmlElement("InvoiceName")]
        public string fiInvoiceName { set; get; }

        [XmlElement("InvoiceFileLink")]
        public string fiInvoiceFileLink { set; get; }

        [XmlIgnore]
        public long idHoSo { get; set; }
    }
}
