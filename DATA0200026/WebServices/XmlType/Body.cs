using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200026.WebServices.XmlType
{
    [XmlRoot("Body")]
    public class Body
    {
        [XmlElement("Content")] public Content Content { get; set; }

        [XmlElement("Signature")] public string Signature { get; set; }

        public static Body CreateBody(Content content)
        {
            return new Body { Content = content };
        }

        public static Body CreateErrorBody(Error error)
        {
            return CreateBody(new Content(error));
        }
    }
}
