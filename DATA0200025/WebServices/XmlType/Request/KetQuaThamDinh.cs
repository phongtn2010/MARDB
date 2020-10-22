using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.WebServices.XmlType.Request
{
    public class KetQuaThamDinh
    {
        [XmlElement] public string NSWFileCode { get; set; }

        [XmlElement] public string Reason { get; set; }
        [XmlElement] public string RegistrationConfirmNo { get; set; }

        [XmlElement("CreaterName")] public string CreatorName { get; set; }

        [XmlIgnore] public DateTime RegistrationConfirmDate { get; set; }

        [XmlElement("RegistrationConfirmDate")]
        public string RegistrationConfirmDateString
        {
            get => RegistrationConfirmDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => RegistrationConfirmDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
    }
}
