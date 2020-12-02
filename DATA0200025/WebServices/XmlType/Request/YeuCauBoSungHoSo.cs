using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.WebServices.XmlType.Request
{
    public class YeuCauBoSungHoSo
    {
        [XmlElement] public string NSWFileCode { get; set; }
        //[XmlElement] public string RegistrationConfirmNo { get; set; } đã bỏ

        [XmlElement] public string CreatorName { get; set; }

        [XmlElement] public string Reason { get; set; }

        public string RegistrationConfirmDate
        {
            get => RegistrationConfirmDateString.ToString("yyyy-MM-dd HH:mm:ss");
            set => RegistrationConfirmDateString = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        [XmlIgnore] public DateTime RegistrationConfirmDateString { get; set; }
    }
}
