using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.WebServices.XmlType.Request
{
    public class XuLyKetQua
    {
        [XmlElement] public string NSWFileCode { get; set; }

        [XmlElement] public long GoodsId { get; set; }

        [XmlElement] public string NameOfGoods { get; set; }

        [XmlElement] public string Reason { get; set; }

        [XmlElement] public string AttachmentId { get; set; }

        [XmlElement] public string FileName { get; set; }

        [XmlElement] public string FileLink { get; set; }
                
        public string ResponseDate
        {
            get => ResponseDateString.ToString("yyyy-MM-dd HH:mm:ss");
            set => ResponseDateString = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        [XmlElement] public DateTime ResponseDateString { get; set; }

        [XmlElement] public string NameOfStaff { get; set; }
    }
}
