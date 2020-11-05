using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200026.WebServices.XmlType
{
    [XmlRoot("Error")]
    public class Error
    {
        [XmlElement("ErrorCode")] public string ErrorCode { get; set; }
        [XmlElement("ErrorName")] public string ErrorName { get; set; }
    }
}
