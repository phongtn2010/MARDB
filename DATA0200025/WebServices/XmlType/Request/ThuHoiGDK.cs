using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.WebServices.XmlType.Request
{
    //[XmlRoot("ResultConfirmCancel")]
    public class ThuHoiGDK
    {
        [XmlElement] public string NSWFileCode { get; set; }

        [XmlIgnore] public DateTime CancelDate { get; set; }

        public string CancelDateString
        {
            get => CancelDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => CancelDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }

        [XmlElement] public string Reason { get; set; }
                
        public string SignConfirmDateSignConfirmDateString
        {
            get => SignConfirmDateString.ToString("yyyy-MM-dd HH:mm:ss");
            set => SignConfirmDateString = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        [XmlElement] public DateTime SignConfirmDateString { get; set; }

        [XmlElement] public string SignConfirmName { get; set; }

        [XmlElement] public string AniFeedConfirmNo { get; set; }

        [XmlElement] public string AttachmentId { get; set; }

        [XmlElement] public string FileName { get; set; }

        [XmlElement] public string FileLink { get; set; }
    }
}
