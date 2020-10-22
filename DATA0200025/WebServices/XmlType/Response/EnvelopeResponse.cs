using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.WebServices.Wrapper
{
    [XmlRoot("Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class EnvelopeResponse
    {
        [XmlElement("Header", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public Header Header { get; set; }
        [XmlElement("Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public BodyResponse Body { get; set; }
    }

    [XmlRoot("Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class BodyResponse
    {
        [XmlElement("receiveResponse", Namespace = "http://mard.gov.vn/nsw/services")]
        public ReceiveResponse ReceiveResponse { get; set; }

    }

    [XmlRoot(ElementName = "receiveResult", Namespace = "http://mard.gov.vn/nsw/services")]
    public class ReceiveResult
    {
        [XmlElement(ElementName = "responsePayload", Namespace = "http://mard.gov.vn/nsw/data/contract")]
        public string ResponsePayload { get; set; }
    }

    [XmlRoot(ElementName = "receiveResponse", Namespace = "http://mard.gov.vn/nsw/services")]
    public class ReceiveResponse
    {
        [XmlElement(ElementName = "receiveResult", Namespace = "http://mard.gov.vn/nsw/services")]
        public ReceiveResult ReceiveResult { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "ActivityId", Namespace = "http://schemas.microsoft.com/2004/09/ServiceModel/Diagnostics")]
    public class ActivityId
    {
        [XmlAttribute(AttributeName = "CorrelationId")]
        public string CorrelationId { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "Header", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class Header
    {
        [XmlElement(ElementName = "ActivityId", Namespace = "http://schemas.microsoft.com/2004/09/ServiceModel/Diagnostics")]
        public ActivityId ActivityId { get; set; }
    }
}
