using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.WebServices.XmlType.Request
{
    public class RequestProCancelVM
    {
        [XmlElement("NSWFileCode")] public string NSWFileCode { get; set; }
        [XmlElement("RequestDate")]
        public string RequestDateString
        {
            get => RequestDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => RequestDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        [XmlIgnore] public DateTime RequestDate { get; set; }

    }
}
