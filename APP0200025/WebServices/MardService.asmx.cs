using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Serialization;
using Autofac;
using DATA0200025;
using DATA0200025.WebServices;
using DATA0200025.WebServices.XmlType;

namespace APP0200025.WebServices
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class MardService : WebService
    {
        private WsProcessingService _processingService = new WsProcessingService();

        //public MardService()
        //{
        //    _processingService = WebApiConfig.Container.Resolve<WsProcessingService>();
        //}

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
                                var result = _processingService.AniFeed(envelop);
                                envelopReturn = CreateEnvelopReturn(nswFileCode, envelop.GetMessageType(),
                                    WsConstants.MessageFunction.FUNC_SUCCESS,
                                    true, null);
                            }
                            else if (envelop.GetFunction().Equals(WsConstants.MessageFunction.FUNCTION_02))
                            {
                                var result = _processingService.GuiSuaHoSo(envelop);
                                envelopReturn = CreateEnvelopReturn(nswFileCode, envelop.GetMessageType(),
                                    WsConstants.MessageFunction.FUNC_SUCCESS,
                                    true, null);
                            }
                            else if (envelop.GetFunction().Equals(WsConstants.MessageFunction.FUNCTION_03))
                            {
                                var result = _processingService.GuiSuaHoSoBPMC(envelop);
                                envelopReturn = CreateEnvelopReturn(nswFileCode, envelop.GetMessageType(),
                                    WsConstants.MessageFunction.FUNC_SUCCESS,
                                    true, null);
                            }
                            else if (envelop.GetFunction().Equals(WsConstants.MessageFunction.FUNCTION_04))
                            {
                                var result = _processingService.GuiSuaHoSoPhongTACN(envelop);
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
                        case WsConstants.MessageType.TYPE_11:
                            if (envelop.GetFunction().Equals(WsConstants.MessageFunction.FUNCTION_05))
                            {
                                var result = _processingService.RequestCancel(envelop);
                                envelopReturn = CreateEnvelopReturn(nswFileCode, envelop.GetMessageType(),
                                    WsConstants.MessageFunction.FUNC_SUCCESS, true, null);
                            }
                            else
                            {
                                envelopReturn = CreateEnvelopReturn(nswFileCode, envelop.GetMessageType(),
                                    WsConstants.MessageFunction.FUNC_ERROR, false, null);
                            }
                            break;
                        case WsConstants.MessageType.TYPE_15:
                            if (envelop.GetFunction().Equals(WsConstants.MessageFunction.FUNCTION_13))
                            {
                                var result = _processingService.TestInformation(envelop);
                                envelopReturn = CreateEnvelopReturn(nswFileCode, envelop.GetMessageType(),
                                    WsConstants.MessageFunction.FUNC_SUCCESS, true, null);
                            }
                            else
                            {
                                envelopReturn = CreateEnvelopReturn(nswFileCode, envelop.GetMessageType(),
                                    WsConstants.MessageFunction.FUNC_ERROR, false, null);
                            }
                            break;
                        case WsConstants.MessageType.TYPE_17:
                            if (envelop.GetFunction().Equals(WsConstants.MessageFunction.FUNCTION_15))
                            {
                                var result = _processingService.SendResultTest(envelop);
                                envelopReturn = CreateEnvelopReturn(nswFileCode, envelop.GetMessageType(),
                                    WsConstants.MessageFunction.FUNC_SUCCESS, true, null);
                            }
                            else if (envelop.GetFunction().Equals(WsConstants.MessageFunction.FUNCTION_17))
                            {
                                var result = _processingService.SendResultTestBPMC(envelop);
                                envelopReturn = CreateEnvelopReturn(nswFileCode, envelop.GetMessageType(),
                                    WsConstants.MessageFunction.FUNC_SUCCESS,
                                    true, null);
                            }
                            else if (envelop.GetFunction().Equals(WsConstants.MessageFunction.FUNCTION_18))
                            {
                                var result = _processingService.SendResultTestTACN(envelop);
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
                        case WsConstants.MessageType.TYPE_21:
                            if (envelop.GetFunction().Equals(WsConstants.MessageFunction.FUNCTION_24))
                            {
                                var result = _processingService.Report(envelop);
                                envelopReturn = CreateEnvelopReturn(nswFileCode, envelop.GetMessageType(),
                                    WsConstants.MessageFunction.FUNC_SUCCESS, true, null);
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

                    CLogNSW.Add(envelop.GetMessageType() + "_" + envelop.GetFunction(), "NSW->BNN", nswFileCode, "Thành công", "99", payload, "", "");
                }
                else
                {
                    CLogNSW.Add(envelop.GetMessageType() + "_" + envelop.GetFunction(), "NSW->BNN", nswFileCode, "Lỗi không đúng định dạng bản tin", "00", payload, "", "");

                    envelopReturn = Envelope.CreateEnvelopeError(nswFileCode,
                        WsConstants.PROCEDURE_CODE,
                        WsConstants.MessageType.TYPE_UNKNOWN,
                        new Error { ErrorCode = WsConstants.Errors.ERR02_CODE, ErrorName = errorMessage });
                }

                
            }
            catch (Exception e)
            {
                CLogNSW.Add(nswFileCode, "NSW->BNN", nswFileCode, e.Message, "00", payload, "", "");

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
