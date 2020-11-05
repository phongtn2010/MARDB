using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DATA0200026.DTO;
using DATA0200026.WebServices.XmlType;
using Microsoft.Office.Interop.Excel;

namespace DATA0200026.WebServices
{
    public class WsProcessingService
    {
        private string sIP = "103.248.160.33";
        //private readonly IMapper _mapper;

        //public WsProcessingService()
        //{
        //    _mapper = AutoMapperConfiguration.MapperConfig().CreateMapper();
        //}

        public HoSoVM Application(Envelope envelope)
        {
            var body = envelope.Body;

            var hoso = body.Content.Application;
            hoso.fiActive = true;
            hoso.fiHSStatus = 1;

            List<HangHoaVM> lstHangHoa = hoso.ListHangHoa;

            string sError = "";
            long iID_MaHoSo = 0;
            string sMaHoSo = hoso.NSWFileCode;

            string sUserName = "doanhnghiep";

            try
            {
                String sTenLoaiHoSo = "Hồ sơ miễn kiểm";
                String sTenDoanhnghiep = hoso.Organization;
                String sTenTACN = "";
                foreach (var hh in lstHangHoa)
                {
                    sTenTACN += hh.NameOfGoods + ";";
                }

                iID_MaHoSo = CHoSo26.ThemHoSo(0, 1, 1, hoso.NSWFileCode, hoso.fiSignDate, false, "", null, null, null, null,
                    "", sTenDoanhnghiep, sTenLoaiHoSo, sTenTACN, hoso.SignPlace, hoso.Organization, hoso.Address, hoso.Phone, hoso.Fax, "", hoso.DepartmentCode, hoso.DepartmentName,
                    hoso.SignPlaceCode, hoso.SignPlaceName, hoso.SignName, hoso.SignPosition, sUserName, sIP);

                int iHH = 0;
                foreach (var hh in lstHangHoa)
                {
                    iHH++;

                    long iID_MaHangHoa = 0;

                    List<ChatLuongVM> lstChatLuong = hh.ListChatLuong;
                    List<AnToanVM> lstAnToan = hh.ListAnToan;

                    iID_MaHangHoa = CHangHoa26.ThemHangHoa(iID_MaHoSo, hh.GoodsCode, hh.GoodsId, hh.GroupFoodOfGoods, "", Convert.ToString(hh.GoodTypeId), "", 0,
                        "", hh.GoodTypeName, "", sMaHoSo, hh.NSWRegisterFileCode, hh.GoodsCode, hh.NameOfGoods, hh.RegistrationNumber, hh.Manufacture, hh.ManufactureStateCode, hh.ManufactureState,
                        "", hh.StandardBase, hh.Material, "", hh.FormColorOfProducts, "", sUserName, sIP);

                    if (iID_MaHangHoa > 0)
                    {
                        foreach (var cl in lstChatLuong)
                        {
                            string sHinhThuc = HamRiengModels.Get_Name_HinhThucCongBo(cl.fiQualityFormOfPublication);
                            long iChatLuong = CHangHoa26.ThemhangHoaChatLuong(iID_MaHangHoa, cl.fiQualityFormOfPublication, cl.fiQualityCriteriaName, sHinhThuc, cl.fiQualityRequire.ToString(), cl.fiQualityRequireUnitID, cl.fiQualityRequireUnitName, "", false, sUserName, sIP);
                        }

                        foreach (var at in lstAnToan)
                        {
                            string sHinhThuc = HamRiengModels.Get_Name_HinhThucCongBo(at.fiSafetyFormOfPublication);
                            long iAnToan = CHangHoa26.ThemhangHoaAnToan(iID_MaHangHoa, 0, at.fiSafetyFormOfPublication, at.fiSafetyCriteriaName, sHinhThuc, at.fiSafetyRequire.ToString(), at.fiSafetyRequireUnitID, at.fiSafetyRequireUnitName, "", false, sUserName, sIP);
                        }
                    }
                    else
                    {

                    }
                }

                CLichSuHoSo.Add(iID_MaHoSo, sMaHoSo, sUserName, sTenDoanhnghiep, 1, "Doanh nghiệp", 1, "Gửi hồ sơ", "", "", 0, "Doanh nghiệp đăng ký mới", 1, "Chờ tiếp nhận");
            }
            catch (Exception ex)
            {
                sError = "Error Add Product: " + ex.ToString();
            }

            //Them vao bang Ho So
            //var hosoModel = new HoSo();
            //hosoModel = _mapper.Map(hoso, hosoModel);
            //hosoModel.IdDepartment = 1;
            //result = _hoSoService.Add(hosoModel);

            ////Them vao bang lich su
            //_lichSuHoSoService.Create(hoso.fiNSWFileCode, envelope.Header.From, "Gửi hồ sơ mới", "", (int)HSStatus.CHO_TIEP_NHAN);

            //_lichSuHoSoService.Save();
            //_hoSoService.Save();

            //return _mapper.Map(result, hoso);

            return null;
        }
    }
}
