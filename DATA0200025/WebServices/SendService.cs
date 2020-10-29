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
        //public string KetQuaXuLy(string nswFileCode, KetQuaXuLy objData, string sMessageFunction)
        //{
        //    String sMessFun = "";
        //    switch (sMessageFunction)
        //    {
        //        case "06":
        //            sMessFun = WsConstants.MessageFunction.FUNCTION_06;
        //            break;
        //        case "07":
        //            sMessFun = WsConstants.MessageFunction.FUNCTION_07;
        //            break;
        //        case "08":
        //            sMessFun = WsConstants.MessageFunction.FUNCTION_08;
        //            break;
        //        case "09":
        //            sMessFun = WsConstants.MessageFunction.FUNCTION_09;
        //            break;
        //        case "10":
        //            sMessFun = WsConstants.MessageFunction.FUNCTION_10;
        //            break;
        //        default:
        //            sMessFun = WsConstants.MessageFunction.FUNCTION_06;
        //            break;
        //    }

        //    var header = Header.DefaultHeader(nswFileCode, WsConstants.PROCEDURE_CODE, WsConstants.MessageType.TYPE_12, sMessFun);
        //    var content = new Content();
        //    content.Result = objData;

        //    var request = new Envelope { Header = header, Body = Body.CreateBody(content) };
        //    var response = WsHelper.SendMessage(request);

        //    return response.GetErrors();
        //}

        //public string XacNhanDon(string nswFileCode, XacNhanDon objData)
        //{
        //    var header = Header.DefaultHeader(nswFileCode, WsConstants.PROCEDURE_CODE, WsConstants.MessageType.TYPE_13,
        //        WsConstants.MessageFunction.FUNCTION_11);
        //    var content = new Content();
        //    content.ResultConfirm = objData;

        //    var request = new Envelope { Header = header, Body = Body.CreateBody(content) };
        //    var response = WsHelper.SendMessage(request);

        //    return response.GetErrors();

        //}

        //public string ThuHoiGDK(string nswFileCode, ThuHoiGDK objData)
        //{
        //    var header = Header.DefaultHeader(nswFileCode, WsConstants.PROCEDURE_CODE, WsConstants.MessageType.TYPE_14,
        //        WsConstants.MessageFunction.FUNCTION_12);
        //    var content = new Content();
        //    content.ResultConfirmCancel = objData;

        //    var request = new Envelope { Header = header, Body = Body.CreateBody(content) };
        //    var response = WsHelper.SendMessage(request);

        //    return response.GetErrors();

        //}

        //public string TCCDGuiKetQuaKT(string nswFileCode, TCCDGuiKetQuaKT objData)
        //{
        //    var header = Header.DefaultHeader(nswFileCode, WsConstants.PROCEDURE_CODE, WsConstants.MessageType.TYPE_16,
        //        WsConstants.MessageFunction.FUNCTION_14);
        //    var content = new Content();
        //    content.ResultTestInformation = objData;

        //    var request = new Envelope { Header = header, Body = Body.CreateBody(content) };
        //    var response = WsHelper.SendMessage(request);

        //    return response.GetErrors();
        //}

        //public string XuLyKetQua(string nswFileCode, XuLyKetQua objData , string sMessageFunction)
        //{
        //    String sMessFun = "";
        //    switch (sMessageFunction)
        //    {
        //        case "19":
        //            sMessFun = WsConstants.MessageFunction.FUNCTION_19;
        //            break;
        //        case "20":
        //            sMessFun = WsConstants.MessageFunction.FUNCTION_20;
        //            break;
        //        case "21":
        //            sMessFun = WsConstants.MessageFunction.FUNCTION_21;
        //            break;
        //        default:
        //            sMessFun = WsConstants.MessageFunction.FUNCTION_19;
        //            break;
        //    }

        //    var header = Header.DefaultHeader(nswFileCode, WsConstants.PROCEDURE_CODE, WsConstants.MessageType.TYPE_18, sMessFun);
        //    var content = new Content();
        //    content.ResultResponse = objData;

        //    var request = new Envelope { Header = header, Body = Body.CreateBody(content) };
        //    var response = WsHelper.SendMessage(request);

        //    return response.GetErrors();
        //}

        //public string GiayXNCL(string nswFileCode, GiayXNCL objData)
        //{
        //    var header = Header.DefaultHeader(nswFileCode, WsConstants.PROCEDURE_CODE, WsConstants.MessageType.TYPE_19,
        //        WsConstants.MessageFunction.FUNCTION_22);
        //    var content = new Content();
        //    content.ResultCheck = objData;

        //    var request = new Envelope { Header = header, Body = Body.CreateBody(content) };
        //    var response = WsHelper.SendMessage(request);

        //    return response.GetErrors();
        //}

        //public string ThuHoiGiayXNCL(string nswFileCode, ThuHoiGiayXNCL objData)
        //{
        //    var header = Header.DefaultHeader(nswFileCode, WsConstants.PROCEDURE_CODE, WsConstants.MessageType.TYPE_20,
        //        WsConstants.MessageFunction.FUNCTION_23);
        //    var content = new Content();
        //    content.AniFeedResultCertificateCancel = objData;

        //    var request = new Envelope { Header = header, Body = Body.CreateBody(content) };
        //    var response = WsHelper.SendMessage(request);

        //    return response.GetErrors();
        //}

        //public string DaTiepNhanHoSo2d(string nswFileCode, DaTiepNhanHoSo2d objData)
        //{
        //    var header = Header.DefaultHeader(nswFileCode, WsConstants.PROCEDURE_CODE, WsConstants.MessageType.TYPE_22,
        //        WsConstants.MessageFunction.FUNCTION_25);
        //    var content = new Content();
        //    content.ResultReception2d = objData;

        //    var request = new Envelope { Header = header, Body = Body.CreateBody(content) };
        //    var response = WsHelper.SendMessage(request);

        //    return response.GetErrors();
        //}

        //public string DMPhanNhomHangHoa(string nswFileCode, DMPhanNhomHangHoa objData)
        //{
        //    var header = Header.DefaultHeader(nswFileCode, WsConstants.PROCEDURE_CODE, WsConstants.MessageType.TYPE_23,
        //        WsConstants.MessageFunction.FUNCTION_26);
        //    var content = new Content();
        //    content.InfoGroupGood = objData;

        //    var request = new Envelope { Header = header, Body = Body.CreateBody(content) };
        //    var response = WsHelper.SendMessage(request);

        //    return response.GetErrors();
        //}

        //public string DMLoaiHangHoa(string nswFileCode, DMLoaiHangHoa objData)
        //{
        //    var header = Header.DefaultHeader(nswFileCode, WsConstants.PROCEDURE_CODE, WsConstants.MessageType.TYPE_24,
        //        WsConstants.MessageFunction.FUNCTION_27);
        //    var content = new Content();
        //    content.InfoGoodType = objData;

        //    var request = new Envelope { Header = header, Body = Body.CreateBody(content) };
        //    var response = WsHelper.SendMessage(request);

        //    return response.GetErrors();
        //}

        //public string DMPhanLoaiHangHoa(string nswFileCode, DMPhanLoaiHangHoa objData)
        //{
        //    var header = Header.DefaultHeader(nswFileCode, WsConstants.PROCEDURE_CODE, WsConstants.MessageType.TYPE_25,
        //        WsConstants.MessageFunction.FUNCTION_28);
        //    var content = new Content();
        //    content.InfoGroupType = objData;

        //    var request = new Envelope { Header = header, Body = Body.CreateBody(content) };
        //    var response = WsHelper.SendMessage(request);

        //    return response.GetErrors();
        //}
    }
}
