using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATA0200026.WebServices.XmlType;
using DomainModel;

namespace DATA0200026.WebServices
{
    public class WsConstants
    {
        public static string PROCEDURE_CODE = "BNNPTNT0200026";
        public static string RECEIVE_VERSION = "01";

        #region Default Unit Reference

        public static readonly UnitReference DefaultFromUnit = new UnitReference
        {
            Identity = "BNN",
            Name = "BNN",
            CountryCode = "VN",
            MinistryCode = "MARD",
            OrganizationCode = "BNN",
            UnitCode = "BNN"
        };

        public static readonly UnitReference DefaultToUnit = new UnitReference
        {
            Name = "NSW",
            Identity = "NSW",
            CountryCode = "VN",
            OrganizationCode = "TCHQ",
            UnitCode = "NSW",
            MinistryCode = "BTC"
        };

        #endregion

        #region Message Action Definition

        public static class MessageType
        {
            public const string TYPE_10 = "10";  //Đơn xin miễn kiểm
            public const string TYPE_11 = "11";  //Thông báo đã tiếp nhận đơn xin miễn kiểm
            public const string TYPE_12 = "12";  //Công văn miễn kiểm hàng hóa
            public const string TYPE_13 = "13";  //Thông báo công văn miễn kiểm bị thu hồi

            public const string TYPE_UNKNOWN = "00";
        }

        public static class MessageFunction
        {
            public static string FUNCTION_01 = "01";  //Gửi đơn xin miễn kiểm
            public static string FUNCTION_03 = "03";  //Gửi thông báo đơn xin miễn kiểm đã được tiếp nhận
            public static string FUNCTION_04 = "04";  //Gửi công văn miễn kiểm hàng hóa
            public static string FUNCTION_05 = "05";  //Gửi thông báo thu hồi đơn xin miễn kiểm hàng hóa

            public static string FUNC_ERROR = "00";   //Lỗi Validation
            public static string FUNC_SUCCESS = "99";  //Thông báo đã nhận được
        }

        #endregion


        public static class Errors
        {
            public static string ERR00_CODE = "ERR00_CODE";
            public static string ERR01_CODE = "ERR01_CODE";
            public static string ERR02_CODE = "ERR02_CODE";
            public static string ERR03_CODE = "ERR03_CODE";
            public static string ERR04_CODE = "ERR04_CODE";
            public static string ERR05_CODE = "ERR05_CODE";
            public static string ERR06_CODE = "ERR05_CODE";

            public static string ERR00 = "Bản tin không hợp lệ";
            public static string ERR01 = "Thủ tục chưa được định nghĩa";
            public static string ERR02 = "Phân tích xml bị lỗi";
            public static string ERR03 = "Lỗi phân tích xml: không lấy được thủ tục từ xml";
            public static string ERR04 = "Message Type: Không tồn tại ";
            public static string ERR05 = "Nội dung bản tin không có dữ liệu, hoặc không hợp lệ.";

            public static string BNN06_CODE = "BNN06_CODE";
            public static string BNN06 = "Không lưu được dữ liệu!";

            public static string BNN08_CODE = "BNN08_CODE";
            public static string BNN08 = "Không lưu được dữ liệu!";

            public static string BNN10_CODE = "BNN10_CODE";
            public static string BNN10 = "Không lưu được dữ liệu!";

            public static string BNN11_CODE = "BNN11_CODE";
            public static string BNN11 = "Không lưu được dữ liệu!";

            public static string BNN04_CODE = "BNN04_CODE";
            public static string BNN04 = "Không lưu được dữ liệu!";
            public static string BNN_03 = "Không lưu được dữ liệu!";
            public static string BNN_04 = "Không lưu được dữ liệu!";
        }
    }

    public static class WsConfig
    {
        public const string Template = @"
                                        <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:gen=""" + DefaultNamespace + @""">
                                            <soapenv:Header/>
                                            <soapenv:Body>
                                                <gen:" + DefaultMethodName + @">
                                                    <gen:" + DefaultMethodTag + @"><![CDATA[" + PayloadPlaceholder + @"]]></gen:" + DefaultMethodTag + @">
                                                </gen:" + DefaultMethodName + @">
                                            </soapenv:Body>
                                        </soapenv:Envelope>";


        public const string DirectGatewayTemplate = @"
                                        <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:gen=""" + DefaultNamespace + @""">
                                            <soapenv:Header/>
                                            <soapenv:Body>
                                                <gen:" + DefaultMethodName + @">
                                                    <gen:" + DefaultOfficeTag + @">" + OfficeCodePlaceHolder + @"</gen:" + DefaultOfficeTag + @">
                                                    <gen:" + DefaultDocumentType + @">" + DocumentTypePlaceHolder + @"</gen:" + DefaultDocumentType + @">
                                                    <gen:" + DefaultPayloadTag + @"><![CDATA[" + PayloadPlaceholder + @"]]></gen:" + DefaultPayloadTag + @">
                                                </gen:" + DefaultMethodName + @">
                                            </soapenv:Body>
                                        </soapenv:Envelope>";

        public const string MardGatewayTemplate = @"
                                        <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:gen=""" + DefaultNamespace + @""" xmlns:con=""" + DefaultGatewayNamespace + @""">
                                            <soapenv:Header/>
                                            <soapenv:Body>
                                                <gen:" + DefaultMethodName + @">
                                                    <gen:" + DefaultMethodTag + @">
                                                        <con:" + DefaultOfficeTag + @">" + OfficeCodePlaceHolder + @"</con:" + DefaultOfficeTag + @">
                                                        <con:" + DefaultDocumentType + @">" + DocumentTypePlaceHolder + @"</con:" + DefaultDocumentType + @">
                                                        <con:" + DefaultPayloadTag + @"><![CDATA[" + PayloadPlaceholder + @"]]></con:" + DefaultPayloadTag + @">
                                                    </gen:" + DefaultMethodTag + @">
                                                </gen:" + DefaultMethodName + @">
                                            </soapenv:Body>
                                        </soapenv:Envelope>";


        public const string PayloadPlaceholder = "_PAYLOAD";
        private const string OfficeCodePlaceHolder = "_OFFICE_CODE";
        private const string DocumentTypePlaceHolder = "_DOCUMENT_TYPE";

        public static string GetTemplate()
        {
            // DEV ENV
            // return Template
            //     .Replace(NamespacePlaceholder, DefaultNamespace)
            //     .Replace(MethodNamePlaceHolder, DefaultMethodName)
            //     .Replace(MethodTagPlaceHolder, DefaultMethodTag);

            // DIRECT GW
            return DirectGatewayTemplate
                .Replace(OfficeCodePlaceHolder, "BNN")
                .Replace(DocumentTypePlaceHolder, WsConstants.PROCEDURE_CODE);

            //// MARD GW
            //return MardGatewayTemplate
            //    .Replace(OfficeCodePlaceHolder, "NSW")
            //    .Replace(DocumentTypePlaceHolder, WsConstants.PROCEDURE_CODE);
        }

        //public const string GatewayUrl = "http://192.168.31.7:18888/mard-gw/MardGateway.svc";
        public static string GatewayUrl = Globals.api_bantinxml26;  // "http://103.248.160.33:8080/VNSWReceiveGateway/ws/gateway.wsdl";
        public const string Action = "http://mard.gov.vn/nsw/services/IMardGateway/receive";
        public const string DefaultMethodName = "receiveRequest";
        public const string DefaultMethodTag = "receiveRequest";
        public const string DefaultPayloadTag = "requestPayload";
        public const string DefaultOfficeTag = "officeCode";
        public const string DefaultDocumentType = "documentType";
        public const string DefaultNamespace = "http://com/vnsw/receive/gateway/generated";
        public const string DefaultGatewayNamespace = "http://com/vnsw/receive/gateway/generated";
    }
}
