using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATA0200025.WebServices.XmlType;
using DomainModel;

namespace DATA0200025.WebServices
{
    public class WsConstants
    {
        public static string PROCEDURE_CODE = "BNNPTNT0200025";
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
            public const string TYPE_10 = "10";  //Hồ sơ đăng ký kiểm tra xác nhận thức ăn chăn nuôi nhập khẩu    //Gửi hồ sơ mới, sửa sang BNN
            public const string TYPE_11 = "11";  //Yêu cầu huỷ hồ sơ của doanh nghiệp
            public const string TYPE_12 = "12";  // Thông tin xử lý hồ sơ của cán bộ
            public const string TYPE_13 = "13";  //Thông tin xác nhận đơn đăng ký kiểm tra xác nhận 
            public const string TYPE_14 = "14";  //BNN gửi thông báo thu hồi Giấy đăng ký xác nhận chất lượng
            public const string TYPE_15 = "15";  //Hồ sơ tổ chức chỉ định
            public const string TYPE_16 = "16";  //Kết quả đánh giá sự phù hợp
            public const string TYPE_17 = "17";  //Nộp kết quả đánh giá sự phù hợp quy chuẩn kỹ thuật
            public const string TYPE_18 = "18";  //Thông báo kết quả tiếp nhận kết quả
            public const string TYPE_19 = "19";  //Giấy xác nhận chất lượng
            public const string TYPE_20 = "20";  //BNN gửi thông báo thu hồi Giấy phép xác nhận chất lượng
            public const string TYPE_21 = "21";  //NSW gửi kết quả báo cáo
            public const string TYPE_22 = "22";  //BNN gửi thông báo đã tiếp nhận hồ sơ miễn giảm
            public const string TYPE_23 = "23";  //BNN gửi thông tin phân nhóm hàng hóa
            public const string TYPE_24 = "24";  //BNN gửi thông tin loại hàng hóa
            public const string TYPE_25 = "25";  //BNN gửi thông tin phân loại

            public const string TYPE_UNKNOWN = "00";
        }

        public static class MessageFunction
        {
            public static string FUNCTION_01 = "01";  //Gửi mới hồ sơ 
            public static string FUNCTION_02 = "02";  //Gửi hồ sơ sửa đổi trước khi BNN tiếp nhận hồ sơ
            public static string FUNCTION_03 = "03";  //Gửi hồ sơ bổ sung theo BPMC
            public static string FUNCTION_04 = "04";  //Gửi hồ sơ bổ sung theo phòng TACN
            public static string FUNCTION_05 = "05";  //Gửi yêu cầu huỷ hồ sơ
            public static string FUNCTION_06 = "06";  //Tiếp nhận hồ sơ
            public static string FUNCTION_07 = "07";  //Yêu cầu bổ sung hồ sơ theo BPMC
            public static string FUNCTION_08 = "08";  //Từ chối hồ sơ
            public static string FUNCTION_09 = "09";  //Yêu cầu bổ sung hồ sơ theo phòng TACN
            public static string FUNCTION_10 = "10";  //Từ chối xác nhận đơn đăng ký
            public static string FUNCTION_11 = "11";  //Xác nhận đơn đăng ký kiểm tra xác nhận chất lượng
            public static string FUNCTION_12 = "12";  //Gửi thông báo thu hồi giấy đăng ký kiểm tra xác nhận chất lượng
            public static string FUNCTION_13 = "13";  //Gửi hồ sơ yêu cầu đánh giá sự phù hợp đến tổ chức chỉ định
            public static string FUNCTION_14 = "14";  //Trả kết quả đánh giá sự phù hợp quy chuẩn kỹ thuật
            public static string FUNCTION_15 = "15";  //Gửi hồ sơ xin cấp xác nhận chất lượng
            public static string FUNCTION_16 = "16";
            public static string FUNCTION_17 = "17";  //Gửi bổ sung kết quả đánh giá sự phù hợp quy chuẩn kỹ thuật theo BPMC
            public static string FUNCTION_18 = "18";  //Gửi bổ sung kết quả theo phòng TACN
            public static string FUNCTION_19 = "19";  //Yêu cầu bổ sung kết quả theo BPMC
            public static string FUNCTION_20 = "20";  //Đồng ý nhận kết quả
            public static string FUNCTION_21 = "21";  //Gửi thông báo yêu cầu bổ sung kết quả kiểm tra theo phòng TACN
            public static string FUNCTION_22 = "22";  //Giấy xác nhận chất lượng
            public static string FUNCTION_23 = "23";  //Thông báo thu hồi Giấy kết quả kiểm tra chất lượng
            public static string FUNCTION_24 = "24";  //Gửi file kết quả báo cáo
            public static string FUNCTION_25 = "25";  //Gửi thông báo đã tiếp nhận hồ sơ miễn giảm 2d
            public static string FUNCTION_26 = "26";  //Đồng bộ danh mục phân nhóm hàng hóa
            public static string FUNCTION_27 = "27";  //Đồng bộ danh mục loại hàng hóa
            public static string FUNCTION_28 = "28";  //Đồng bộ danh mục phân loại hàng hóa

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
        public static string GatewayUrl = Globals.api_bantinxml25;  // "http://103.248.160.33:8080/VNSWReceiveGateway/ws/gateway.wsdl";
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
