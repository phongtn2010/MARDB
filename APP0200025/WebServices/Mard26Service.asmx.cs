using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Serialization;
using DATA0200026.WebServices;
using DATA0200026.WebServices.XmlType;

namespace APP0200025.WebServices
{
    /// <summary>
    /// Summary description for Mard26Service
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Mard26Service : System.Web.Services.WebService
    {
        private WsProcessingService _processingService = new WsProcessingService();

        [WebMethod]
        public string Request(string payload)
        {
            Envelope envelopReturn;
            var nswFileCode = "";
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Envelope));


                if (string.IsNullOrEmpty(payload))
                {
                    envelopReturn = Envelope.CreateEnvelopeError(
                        "",
                        WsConstants.PROCEDURE_CODE,
                        WsConstants.MessageType.TYPE_UNKNOWN,
                        new Error { ErrorCode = WsConstants.Errors.ERR00_CODE, ErrorName = WsConstants.Errors.ERR00 });
                    return WsHelper.GetXmlFromObject(envelopReturn);
                }

                TextReader textReader = new StringReader(payload);
                var envelop = (Envelope)serializer.Deserialize(textReader);
                nswFileCode = envelop.Header.Subject.Reference;
                var errorMessage = Validate(payload);
                if (string.IsNullOrEmpty(errorMessage))
                {
                    switch (envelop.GetMessageType())
                    {
                        case WsConstants.MessageType.TYPE_10:
                            if (envelop.GetFunction().Equals(WsConstants.MessageFunction.FUNCTION_01))
                            {
                                var result = _processingService.Application(envelop);
                                envelopReturn = CreateEnvelopReturn(nswFileCode, envelop.GetMessageType(),
                                    WsConstants.MessageFunction.FUNC_SUCCESS,
                                    true, null);
                            }
                            else
                            {
                                envelopReturn = CreateEnvelopReturn(nswFileCode, envelop.GetMessageType(),
                                    WsConstants.MessageFunction.FUNC_ERROR, false, null);
                            }
                            break;
                        default:
                            envelopReturn = Envelope.CreateEnvelopeError(nswFileCode,
                                WsConstants.PROCEDURE_CODE,
                                envelop.GetMessageType() == null
                                    ? envelop.GetMessageType()
                                    : WsConstants.MessageType.TYPE_UNKNOWN,
                                new Error
                                {
                                    ErrorCode = WsConstants.Errors.ERR04_CODE,
                                    ErrorName = WsConstants.Errors.ERR04
                                });
                            break;
                    }

                    //CLogNSW.Add(envelop.GetMessageType() + "_" + envelop.GetFunction(), "doanhnghiep", "Thành công", payload, "", "");
                }
                else
                {
                    //CLogNSW.Add(envelop.GetMessageType() + "_" + envelop.GetFunction(), "doanhnghiep", "Lỗi validate payload", payload, "", "");

                    envelopReturn = Envelope.CreateEnvelopeError(nswFileCode,
                        WsConstants.PROCEDURE_CODE,
                        WsConstants.MessageType.TYPE_UNKNOWN,
                        new Error { ErrorCode = WsConstants.Errors.ERR02_CODE, ErrorName = errorMessage });
                }


            }
            catch (Exception e)
            {
                //CLogNSW.Add(nswFileCode, "doanhnghiep", e.Message, payload, "", "");

                envelopReturn = Envelope.CreateEnvelopeError(nswFileCode,
                    WsConstants.PROCEDURE_CODE,
                    WsConstants.MessageType.TYPE_UNKNOWN,
                    new Error
                    {
                        ErrorCode = WsConstants.Errors.ERR02_CODE,
                        ErrorName = WsConstants.Errors.ERR02 + "|" + e.Message
                    });
            }

            return WsHelper.GetXmlFromObject(envelopReturn);
        }

        private static Envelope CreateEnvelopReturn(string nswFileCode, string msgType, string msgFunc, bool isSuccess,
            string error)
        {
            if (!isSuccess)
                return Envelope.CreateEnvelopeError(nswFileCode,
                    WsConstants.PROCEDURE_CODE,
                    msgType,
                    new Error { ErrorCode = WsConstants.Errors.BNN10, ErrorName = error });
            var header = Header.DefaultHeader(nswFileCode, WsConstants.PROCEDURE_CODE, msgType, msgFunc);
            var content = new Content(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            var body = Body.CreateBody(content);
            return new Envelope { Header = header, Body = body };
        }

        private static string Validate(string payload)
        {
            //TODO: need implement
            return null;
        }
    }
}
