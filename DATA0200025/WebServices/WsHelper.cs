using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using DATA0200025.WebServices.Wrapper;
using DATA0200025.WebServices.XmlType;

namespace DATA0200025.WebServices
{
    public class WsHelper
    {
        private static bool IsSendXML = true;
        public static string GetXmlFromObject<T>(T value)
        {
            var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(value.GetType());
            var settings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true };

            using (var stream = new StringWriter())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, value, emptyNamespaces);
                return stream.ToString();
            }
        }

        public static Envelope ParseResponse(string payload)
        {
            var responseSerializer = new XmlSerializer(typeof(EnvelopeResponse));
            var envelopeSerializer = new XmlSerializer(typeof(Envelope));
            using (TextReader reader = new StringReader(payload))
            {
                var response = (EnvelopeResponse)responseSerializer.Deserialize(reader);

                using (TextReader envelopeReader = new StringReader(response.Body.ReceiveResponse.ResponsePayload))
                {
                    var envelop = (Envelope)envelopeSerializer.Deserialize(envelopeReader);
                    var sType = envelop.Header.Subject.Type;
                    var sFun = envelop.Header.Subject.Function;
                    var nswFileCode = envelop.Header.Subject.Function;

                    return envelop;
                    //return (Envelope)envelopeSerializer.Deserialize(envelopeReader);
                }
            }
        }

        public static Envelope SendMessage(string payload)
        {
            Console.WriteLine(payload);
            if (!IsSendXML)
            {
                return new Envelope();
            }

            //TODO: Save request / response
            var soapEnvelopeXml = CreateSoapEnvelope(payload);

            try
            {
                var webRequest = CreateWebRequest(WsConfig.GatewayUrl, WsConfig.Action);
                InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);
                string soapResult;
                using (var webResponse = webRequest.GetResponse())
                {
                    using (var rd =
                        new StreamReader(webResponse.GetResponseStream() ?? throw new NullReferenceException()))
                    {
                        soapResult = rd.ReadToEnd();
                    }
                }
                
                return ParseResponse(soapResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Envelope SendMessage(Envelope envelope)
        {
            System.Diagnostics.Debug.WriteLine(GetXmlFromObject(envelope));
            return SendMessage(GetXmlFromObject(envelope));
        }

        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", "");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            webRequest.Timeout = 100000;
            return webRequest;
        }

        private static XmlDocument CreateSoapEnvelope(string payload)
        {
            var soapEnvelopeDocument = new XmlDocument();
            soapEnvelopeDocument.LoadXml(WsConfig.GetTemplate().Replace(WsConfig.PayloadPlaceholder, payload));
            return soapEnvelopeDocument;
        }

        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, WebRequest webRequest)
        {
            soapEnvelopeXml.Save(webRequest.GetRequestStream());
        }
    }
}
