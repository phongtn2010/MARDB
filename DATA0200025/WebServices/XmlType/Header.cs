using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.WebServices.XmlType
{
    public class Header
    {
        [XmlElement("Reference")] public Reference Reference { get; set; }

        [XmlElement("From")] public UnitReference From { get; set; }

        [XmlElement("To")] public UnitReference To { get; set; }

        [XmlElement("Subject")] public Subject Subject { get; set; }

        public static Header CreateHeaderError(string nswFileCode, string documentType, string msgType)
        {
            var header = new Header();
            var now = DateTime.Now;
            var uuid = Guid.NewGuid();

            header.Reference = new Reference { Version = WsConstants.RECEIVE_VERSION, MessageId = uuid.ToString() };
            header.From = WsConstants.DefaultFromUnit;
            header.To = WsConstants.DefaultToUnit;
            header.Subject = new Subject
            {
                DocumentType = documentType,
                DocumentYear = now.Year.ToString(),
                PreDocumentYear = now.Year.ToString(),
                Function = WsConstants.MessageFunction.FUNC_ERROR,
                Type = msgType,
                Reference = nswFileCode,
                PreReference = nswFileCode,
                SendDate = now
            };
            return header;
        }

        public static Header DefaultHeader(string nswFileCode, string documentType, string msgType, string msgFunc)
        {
            var header = new Header();
            var now = DateTime.Now;
            var uuid = Guid.NewGuid();
            header.Reference = new Reference { Version = WsConstants.RECEIVE_VERSION, MessageId = uuid.ToString() };
            header.From = WsConstants.DefaultFromUnit;
            header.To = WsConstants.DefaultToUnit;
            header.Subject = new Subject
            {
                DocumentType = documentType,
                DocumentYear = now.Year.ToString(),
                PreDocumentYear = now.Year.ToString(),
                Function = msgFunc,
                Type = msgType,
                Reference = nswFileCode,
                PreReference = nswFileCode,
                SendDate = now
            };
            return header;
        }

        public static Header CreateSendHeader(string nswFileCode, string documentType, string msgType, string msgFunc,
            string destUnit)
        {
            var header = new Header();
            var now = DateTime.Now;
            var uuid = System.Guid.NewGuid();
            header.Reference = new Reference { Version = WsConstants.RECEIVE_VERSION, MessageId = uuid.ToString() };
            header.From = WsConstants.DefaultFromUnit;
            header.To = WsConstants.DefaultToUnit;
            header.To.UnitCode = destUnit;
            header.Subject = new Subject
            {
                DocumentType = documentType,
                DocumentYear = now.Year.ToString(),
                PreDocumentYear = now.Year.ToString(),
                Function = msgFunc,
                Type = msgType,
                Reference = nswFileCode,
                PreReference = nswFileCode,
                SendDate = now
            };

            return header;
        }
    }

    public class Reference
    {
        [XmlElement("version")] public string Version { get; set; }
        [XmlElement("messageId")] public string MessageId { get; set; }
    }

    public class UnitReference
    {
        [XmlElement("name")] public string Name { get; set; }
        [XmlElement("identity")] public string Identity { get; set; }
        [XmlElement("countryCode")] public string CountryCode { get; set; }
        [XmlElement("ministryCode")] public string MinistryCode { get; set; }
        [XmlElement("organizationCode")] public string OrganizationCode { get; set; }
        [XmlElement("unitCode")] public string UnitCode { get; set; }
    }

    public class Subject
    {
        [XmlElement("documentType")] public string DocumentType { get; set; }
        [XmlElement("type")] public string Type { get; set; }
        [XmlElement("function")] public string Function { get; set; }
        [XmlElement("reference")] public string Reference { get; set; }
        [XmlElement("documentYear")] public string DocumentYear { get; set; }
        [XmlElement("preReference")] public string PreReference { get; set; }
        [XmlElement("preDocumentYear")] public string PreDocumentYear { get; set; }
        [XmlIgnore] public DateTime SendDate { get; set; }
        [XmlElement("sendDate")]
        public string SendDateString
        {
            get => SendDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => SendDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
    }
}
