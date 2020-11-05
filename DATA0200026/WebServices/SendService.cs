using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATA0200026.DTO;
using DATA0200026.DTO.Extensions;
using DATA0200026.DTO.Request;
using DATA0200026.WebServices.XmlType;
using DATA0200026.WebServices.XmlType.Request;

namespace DATA0200026.WebServices
{
    public class SendService
    {
        /// <summary>
        /// XML(11,03)
        /// </summary>
        /// <param name="nswFileCode"></param>
        /// <param name="objData"></param>
        /// <param name="sMessageFunction"></param>
        /// <returns></returns>
        public string PhanHoiDonXM(string nswFileCode, PhanHoiDonXM objData)
        {
            var header = Header.DefaultHeader(nswFileCode, WsConstants.PROCEDURE_CODE, WsConstants.MessageType.TYPE_11, 
                WsConstants.MessageFunction.FUNCTION_03);
            var content = new Content();
            content.ApplicationResponse = objData;

            var request = new Envelope { Header = header, Body = Body.CreateBody(content) };
            var response = WsHelper.SendMessage(request);

            return response.GetFunction();
        }
        /// <summary>
        /// XML(12,04)
        /// </summary>
        /// <param name="nswFileCode"></param>
        /// <param name="objData"></param>
        /// <returns></returns>
        public string CVMienKiem(string nswFileCode, CVMienKiem objData)
        {
            var header = Header.DefaultHeader(nswFileCode, WsConstants.PROCEDURE_CODE, WsConstants.MessageType.TYPE_12,
                WsConstants.MessageFunction.FUNCTION_04);
            var content = new Content();
            content.ApplicationReplies = objData;

            var request = new Envelope { Header = header, Body = Body.CreateBody(content) };
            var response = WsHelper.SendMessage(request);

            return response.GetFunction();

        }
        /// <summary>
        /// XML(13,05)
        /// </summary>
        /// <param name="nswFileCode"></param>
        /// <param name="objData"></param>
        /// <returns></returns>
        public string ThongBaoThuHoiCVMienKiem(string nswFileCode, ThongBaoThuHoiCVMienKiem objData)
        {
            var header = Header.DefaultHeader(nswFileCode, WsConstants.PROCEDURE_CODE, WsConstants.MessageType.TYPE_13,
                WsConstants.MessageFunction.FUNCTION_05);
            var content = new Content();
            content.ApplicationCancel = objData;

            var request = new Envelope { Header = header, Body = Body.CreateBody(content) };
            var response = WsHelper.SendMessage(request);

            return response.GetFunction();

        }
    }
}
