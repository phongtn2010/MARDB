using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200026.WebServices.XmlType.Request
{
    public class PhanHoiDonXM
    {
        [XmlElement] public string NSWFileCode { get; set; }

        [XmlElement] public string NameOfStaff { get; set; }

        public string ResponseDate
        {
            get => ResponseDateString.ToString("yyyy-MM-dd HH:mm:ss");
            set => ResponseDateString = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        [XmlIgnore] public DateTime ResponseDateString { get; set; }
    }
}
