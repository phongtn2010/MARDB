using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.WebServices.XmlType.Request
{
    public class ThuHoiGiayXNCL
    {
        [XmlElement] public string NSWFileCode { get; set; }

        public string CancelDateString
        {
            get => CancelDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => CancelDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        [XmlElement] public DateTime CancelDate { get; set; }

        [XmlElement] public string Reason { get; set; }

        public string SignConfirmDateString
        {
            get => SignConfirmDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => SignConfirmDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        [XmlElement] public DateTime SignConfirmDate { get; set; }

        [XmlElement] public string SignConfirmName { get; set; }

        [XmlElement] public string AniFeedResultNo { get; set; }

        [XmlElement] public string AttachmentId { get; set; }

        [XmlElement] public string FileName { get; set; }

        [XmlElement] public string FileLink { get; set; }
    }
}
