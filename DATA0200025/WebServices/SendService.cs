using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATA0200025.DTO;
using DATA0200025.DTO.Extensions;
using DATA0200025.DTO.Request;
using DATA0200025.WebServices.XmlType;
using DATA0200025.WebServices.XmlType.Request;

namespace DATA0200025.WebServices
{
    public class SendService
    {
        public string GuiMoiGPNhapKhau(string nswFileCode, GPNhapKhauVM gpNK)
        {
            var header = Header.DefaultHeader(nswFileCode, WsConstants.PROCEDURE_CODE, WsConstants.MessageType.TYPE_18,
                WsConstants.MessageFunction.FUNCTION_14);
            var content = new Content();
            content.GPNhapKhau = gpNK;

            var request = new Envelope { Header = header, Body = Body.CreateBody(content) };
            var response = WsHelper.SendMessage(request);

            return response.GetErrors();

        }
        public string ThamDinhDatDDK(string nswFileCode, string registrationConfirmNo, string userName)
        {
            KetQuaThamDinh resultConfirm = new KetQuaThamDinh();
            resultConfirm.NSWFileCode = nswFileCode;
            resultConfirm.RegistrationConfirmDate = DateTime.Now;
            resultConfirm.RegistrationConfirmNo = registrationConfirmNo;

            //TODO: Fetch Creator name from logged in user
            resultConfirm.CreatorName = userName;

            var header = Header.DefaultHeader(nswFileCode, WsConstants.PROCEDURE_CODE, WsConstants.MessageType.TYPE_13,
                WsConstants.MessageFunction.FUNCTION_07);
            var content = new Content
            {
                ResultConfirm = resultConfirm
            };


            var request = new Envelope { Header = header, Body = Body.CreateBody(content) };
            var response = WsHelper.SendMessage(request);

            return response.GetErrors();
        }
        public string YeuCauBoSung(string nswFileCode, string reason, string userName)
        {
            YeuCauBoSungHoSo resultConfirm = new YeuCauBoSungHoSo();
            resultConfirm.NSWFileCode = nswFileCode;
            resultConfirm.RegistrationConfirmDate = DateTime.Now;
            resultConfirm.Reason = reason;
            resultConfirm.CreatorName = userName;

            var header = Header.DefaultHeader(nswFileCode, WsConstants.PROCEDURE_CODE, WsConstants.MessageType.TYPE_12,
                WsConstants.MessageFunction.FUNCTION_06);
            var content = new Content
            {
                Result = resultConfirm
            };


            var request = new Envelope { Header = header, Body = Body.CreateBody(content) };
            var response = WsHelper.SendMessage(request);

            return response.GetErrors();
        }

        public string TuChoiHoSo(string nswFileCode, string reason, string userName)
        {
            YeuCauBoSungHoSo resultConfirm = new YeuCauBoSungHoSo();
            resultConfirm.NSWFileCode = nswFileCode;
            resultConfirm.RegistrationConfirmDate = DateTime.Now;
            resultConfirm.Reason = reason;
            resultConfirm.CreatorName = userName;

            var header = Header.DefaultHeader(nswFileCode, WsConstants.PROCEDURE_CODE, WsConstants.MessageType.TYPE_12,
                WsConstants.MessageFunction.FUNCTION_05);
            var content = new Content
            {
                Result = resultConfirm
            };


            var request = new Envelope { Header = header, Body = Body.CreateBody(content) };
            var response = WsHelper.SendMessage(request);

            return response.GetErrors();
        }
        public string TuChoiSuaHoSo(string nswFileCode, string reason, string userName)
        {
            ProResponseEdit resultConfirm = new ProResponseEdit();
            resultConfirm.NSWFileCode = nswFileCode;
            resultConfirm.SignConfirmDate = DateTime.Now;
            resultConfirm.Reason = reason;
            resultConfirm.CreaterName = userName;

            var header = Header.DefaultHeader(nswFileCode, WsConstants.PROCEDURE_CODE, WsConstants.MessageType.TYPE_15,
                WsConstants.MessageFunction.FUNCTION_10);
            var content = new Content
            {
                ProResponseEdit = resultConfirm
            };


            var request = new Envelope { Header = header, Body = Body.CreateBody(content) };
            var response = WsHelper.SendMessage(request);

            return response.GetErrors();
        }
        public string DongYSuaHoSo(string nswFileCode, string userName)
        {
            ProResponseEdit resultConfirm = new ProResponseEdit();
            resultConfirm.NSWFileCode = nswFileCode;
            // resultConfirm.RegistrationConfirmDate = DateTime.Now;
            // resultConfirm.RegistrationConfirmNo = registrationConfirmNo;

            //TODO: Fetch Creator name from logged in user
            resultConfirm.CreaterName = userName;

            var header = Header.DefaultHeader(nswFileCode, WsConstants.PROCEDURE_CODE, WsConstants.MessageType.TYPE_15,
                WsConstants.MessageFunction.FUNCTION_09);
            var content = new Content
            {
                ProResponseEdit = resultConfirm
            };


            var request = new Envelope { Header = header, Body = Body.CreateBody(content) };
            var response = WsHelper.SendMessage(request);

            return response.GetErrors();
        }

        // public string DongYHuyHoSo(string hoSoFiNswFileCode, UsrAccount user)
        // {
        //     throw new NotImplementedException();
        // }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nswFileCode"></param>
        /// <param name="reason"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string TuChoiHuyHoSo(string nswFileCode, string reason, string userName)
        {
            ResponseCancel resultConfirm = new ResponseCancel();
            resultConfirm.NSWFileCode = nswFileCode;
            resultConfirm.SignConfirmDate = DateTime.Now;
            resultConfirm.Reason = reason;
            resultConfirm.CreaterName = userName;

            var header = Header.DefaultHeader(nswFileCode, WsConstants.PROCEDURE_CODE, WsConstants.MessageType.TYPE_17,
                WsConstants.MessageFunction.FUNCTION_13);
            var content = new Content
            {
                ResponseCancel = resultConfirm
            };


            var request = new Envelope { Header = header, Body = Body.CreateBody(content) };
            var response = WsHelper.SendMessage(request);

            return response.GetErrors();
        }

        public string DongYHuyHoSo(string nswFileCode, string reason, string userName)
        {
            ResponseCancel resultConfirm = new ResponseCancel();
            resultConfirm.NSWFileCode = nswFileCode;
            resultConfirm.SignConfirmDate = DateTime.Now;
            resultConfirm.Reason = reason;
            resultConfirm.CreaterName = userName;
            var header = Header.DefaultHeader(nswFileCode, WsConstants.PROCEDURE_CODE, WsConstants.MessageType.TYPE_17,
                WsConstants.MessageFunction.FUNCTION_12);
            var content = new Content
            {
                ResponseCancel = resultConfirm
            };


            var request = new Envelope { Header = header, Body = Body.CreateBody(content) };
            var response = WsHelper.SendMessage(request);

            return response.GetErrors();
        }

        public string TuChoiSuaGiayPhep(string nswFileCode, string reason, string userName)
        {
            ResponseCancel resultConfirm = new ResponseCancel();
            resultConfirm.NSWFileCode = nswFileCode;
            resultConfirm.SignConfirmDate = DateTime.Now;
            resultConfirm.Reason = reason;
            resultConfirm.CreaterName = userName;
            var header = Header.DefaultHeader(nswFileCode, WsConstants.PROCEDURE_CODE, WsConstants.MessageType.TYPE_17,
                WsConstants.MessageFunction.FUNCTION_16);
            var content = new Content
            {
                ResponseCancel = resultConfirm
            };


            var request = new Envelope { Header = header, Body = Body.CreateBody(content) };
            var response = WsHelper.SendMessage(request);

            return response.GetErrors();
        }
    }
}
