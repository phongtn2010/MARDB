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
            long iID_MaHoSo = 0, iID_MaHoSo_ThayThe = 0;
            string sMaHoSo = hoso.NSWFileCode;

            string sUserName = "doanhnghiep";

            try
            {
                //String sTenLoaiHoSo = HamRiengModels.Get_Name_LoaiHinhThucKiemTra(hoso.fiTypeAniFeed);
                //String sTenTACN = "";
                //foreach (var hh in lstHangHoa)
                //{
                //    sTenTACN += hh.fiNameOfGoods + ";";
                //}

                //iID_MaHoSo = CHoSo.ThemHoSo(0, 1, hoso.fiTypeAniFeed, iID_MaHoSo_ThayThe, hoso.fiNSWFileCode, hoso.fiNSWFileCodeOld, hoso.fiCreateDate, false, "", null, null, null, null,
                //    hoso.fiTaxCode, sTenDoanhnghiep, sTenLoaiHoSo, "", sTenTACN, hoso.sBan_Name, hoso.sBan_DiaChi, hoso.sBan_Tel, hoso.sBan_Fax, "", hoso.sBan_MaQuocGia, hoso.sBan_QuocGia, hoso.sBan_NoiXuat,
                //    hoso.sMua_Name, hoso.sMua_DiaChi, hoso.sMua_Tel, hoso.sMua_Fax, "", hoso.sMua_NoiNhan, hoso.sMua_FromDate, hoso.sMua_ToDate,
                //    hoso.fiLocationOfStorage, hoso.fiLocationOfSampling, hoso.fiDateOfSamplingFrom, hoso.fiDateOfSamplingTo,
                //    hoso.fiContactPerson, hoso.fiContactAddress, hoso.fiContactTel, hoso.fiContactEmail,
                //    hoso.fiSignPlace, hoso.fiSignPlace, hoso.fiSignName, "", sUserName, sIP);

                int iHH = 0;
                foreach (var hh in lstHangHoa)
                {
                    iHH++;

                    long iID_MaHangHoa = 0;

                    List<ChatLuongVM> lstChatLuong = hh.ListChatLuong;
                    List<AnToanVM> lstAnToan = hh.ListAnToan;

                    //iID_MaHangHoa = CHangHoa.ThemHangHoa(iID_MaHoSo, hh.GoodsId, hh.fiGroupFoodOfGoods, Convert.ToString(hh.fiGroupGoodId), Convert.ToString(hh.fiGoodTypeId), Convert.ToString(hh.fiGroupTypeId), Convert.ToString(hh.fiGoodsValueUnitCode), 0,
                    //    hh.fiGroupGoodName, hh.fiGoodTypeName, hh.fiGroupTypeName, sMaHoSo, hh.fiNameOfGoods, hh.fiRegistrationNumber, hh.fiManufacture, hh.fiManufactureStateCode, hh.fiManufactureState,
                    //    hh.fiNature, hh.fiGoodsValueUnitName, hh.fiMaterial, hh.fiFormColorOfProducts, hh.fiStandardBase, hh.fiTechnicalRegulations, hh.fiGoodsValue, hh.fiGoodsValueUSD, "", sUserName, sIP);

                    if (iID_MaHangHoa > 0)
                    {
                        foreach (var cl in lstChatLuong)
                        {
                            //string sHinhThuc = HamRiengModels.Get_Name_HinhThucCongBo(cl.fiQualityFormOfPublication);
                            //long iChatLuong = CHangHoa.ThemhangHoaChatLuong(iID_MaHangHoa, cl.fiQualityFormOfPublication, cl.fiQualityCriteriaName, sHinhThuc, cl.fiQualityRequire.ToString(), cl.fiQualityRequireUnitID, cl.fiQualityRequireUnitName, "", false, sUserName, sIP);
                        }

                        foreach (var at in lstAnToan)
                        {
                            //string sHinhThuc = HamRiengModels.Get_Name_HinhThucCongBo(at.fiSafetyFormOfPublication);
                            //long iAnToan = CHangHoa.ThemhangHoaAnToan(iID_MaHangHoa, 0, at.fiSafetyFormOfPublication, at.fiSafetyCriteriaName, sHinhThuc, at.fiSafetyRequire.ToString(), at.fiSafetyRequireUnitID, at.fiSafetyRequireUnitName, "", false, sUserName, sIP);
                        }
                    }
                    else
                    {

                    }
                }

                //clLichSuHoSo.InsertLichSuNsw(iID_MaHoSo, sUserName, sTenDoanhnghiep, 1, 1, "", "", 0, "Doanh nghiệp đăng ký từ NSW chuyển sang", 1);
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
