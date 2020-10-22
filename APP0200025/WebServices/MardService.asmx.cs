using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Serialization;
using Autofac;
using DATA0200025.WebServices;
using DATA0200025.WebServices.XmlType;

namespace APP0200025.WebServices
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class MardService : WebService
    {
        private readonly WsProcessingService _processingService;

        public MardService()
        {
            _processingService = WebApiConfig.Container.Resolve<WsProcessingService>();
        }

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
                                var result = _processingService.ReceiveRegistrationProfile(envelop);
                                envelopReturn = CreateEnvelopReturn(nswFileCode, envelop.GetMessageType(),
                                    WsConstants.MessageFunction.FUNC_SUCCESS,
                                    true, null);
                            }
                            else if (envelop.GetFunction().Equals(WsConstants.MessageFunction.FUNCTION_02))
                            {
                                //var result = _processingService.GuiSuaHoSo(envelop);
                                envelopReturn = CreateEnvelopReturn(nswFileCode, envelop.GetMessageType(),
                                    WsConstants.MessageFunction.FUNC_SUCCESS,
                                    true, null);
                            }
                            else
                            {
                                //var result = _processingService.GuiSuaTheoYeuCauBNN(envelop);
                                envelopReturn = CreateEnvelopReturn(nswFileCode, envelop.GetMessageType(),
                                    WsConstants.MessageFunction.FUNC_SUCCESS,
                                    true, null);
                            }
                            //TODO: add lich su

                            break;
                        case WsConstants.MessageType.TYPE_11:
                            if (envelop.GetFunction().Equals(WsConstants.MessageFunction.FUNCTION_04))
                            {
                                //_processingService.DoanhNghiepHuyHoSo(envelop);
                                envelopReturn = CreateEnvelopReturn(nswFileCode, envelop.GetMessageType(),
                                    WsConstants.MessageFunction.FUNC_SUCCESS, true, null);
                            }
                            else
                            {
                                envelopReturn = CreateEnvelopReturn(nswFileCode, envelop.GetMessageType(),
                                    WsConstants.MessageFunction.FUNC_ERROR, false, null);
                            }
                            break;
                        case WsConstants.MessageType.TYPE_14:
                            if (envelop.GetFunction().Equals(WsConstants.MessageFunction.FUNCTION_08))
                            {
                                //_processingService.DoanhNghiepXinSuaHoSo(envelop);
                                envelopReturn = CreateEnvelopReturn(nswFileCode, envelop.GetMessageType(),
                                    WsConstants.MessageFunction.FUNC_SUCCESS, true, null);
                            }
                            else
                            {
                                envelopReturn = CreateEnvelopReturn(nswFileCode, envelop.GetMessageType(),
                                    WsConstants.MessageFunction.FUNC_ERROR, false, null);
                            }
                            break;
                        case WsConstants.MessageType.TYPE_16:
                            if (envelop.GetFunction().Equals(WsConstants.MessageFunction.FUNCTION_11))
                            {
                                //_processingService.DoanhNghiepXinHuyHoSo(envelop);
                                envelopReturn = CreateEnvelopReturn(nswFileCode, envelop.GetMessageType(),
                                    WsConstants.MessageFunction.FUNC_SUCCESS, true, null);
                            }
                            else
                            {
                                envelopReturn = CreateEnvelopReturn(nswFileCode, envelop.GetMessageType(),
                                    WsConstants.MessageFunction.FUNC_ERROR, false, null);
                            }
                            break;
                        case WsConstants.MessageType.TYPE_19:
                            if (envelop.GetFunction().Equals(WsConstants.MessageFunction.FUNCTION_15))
                            {
                                //_processingService.DoanhNghiepXinSuaGiayPhep(envelop);
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
                }
                else
                {
                    envelopReturn = Envelope.CreateEnvelopeError(nswFileCode,
                        WsConstants.PROCEDURE_CODE,
                        WsConstants.MessageType.TYPE_UNKNOWN,
                        new Error { ErrorCode = WsConstants.Errors.ERR02_CODE, ErrorName = errorMessage });
                }
            }
            catch (Exception e)
            {
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
            var content = new Content(DateTime.Now);
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
