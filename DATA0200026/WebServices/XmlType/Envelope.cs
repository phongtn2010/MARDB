using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200026.WebServices.XmlType
{
    [Serializable, XmlRoot("Envelope")]
    public class Envelope
    {
        [XmlElement("Header")] public Header Header { get; set; }

        [XmlElement("Body")] public Body Body { get; set; }

        public static Envelope CreateEnvelopeError(string nswFileCode, string documentType, string msgType, Error error)
        {
            try
            {
                return new Envelope
                {
                    Header = Header.CreateHeaderError(nswFileCode, documentType, msgType),
                    Body = Body.CreateErrorBody(error)
                };
            }
            catch (Exception)
            {
                //TODO : save log
            }

            return null;
        }

        public static Envelope CreateResponseError(string nswFileCode, string documentType, string type, Error error)
        {
            try
            {
                return new Envelope
                {
                    Header = Header.CreateHeaderError(nswFileCode, documentType, type),
                    Body = Body.CreateErrorBody(error)
                };
            }
            catch (Exception)
            {
                //TODO : save log
            }

            return null;
        }

        public static Envelope CreateResponseError(string nswFileCode, string documentType, string type, string code,
            string name, Exception ex)
        {
            try
            {
                return new Envelope
                {
                    Header = Header.CreateHeaderError(nswFileCode, documentType, type),
                    Body = Body.CreateErrorBody(new Error { ErrorCode = code, ErrorName = name })
                };
            }
            catch (Exception)
            {
                //TODO : save log
            }

            return null;
        }

        public string GetDocumentType()
        {
            return Header?.Subject?.DocumentType;
        }

        public string GetMessageType()
        {
            return Header?.Subject?.Type;
        }

        public string GetFunction()
        {
            return Header?.Subject?.Function;
        }

        public string GetProcedure()
        {
            return Header?.From?.Identity;
        }

        public bool IsSuccess()
        {
            return Header?.Subject != null && Header.Subject.Function.Equals(WsConstants.MessageFunction.FUNC_SUCCESS);
        }

        public string GetErrors()
        {
            if (IsSuccess() || Body?.Content?.Errors == null || Body.Content.Errors.Count == 0) return "";
            var sb = new StringBuilder();
            foreach (var err in Body.Content.Errors)
            {
                sb.Append(err.ErrorCode).Append(':').Append(err.ErrorName).AppendLine();
            }

            if (sb.Length > 0) sb.Length--;
            return sb.ToString();
        }
    }
}
