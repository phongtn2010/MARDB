﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DATA0200025.DTO;
using DATA0200025.Models.Enums;
using DATA0200025.WebServices.XmlType;

namespace DATA0200025.WebServices
{
    public class WsProcessingService
    {
        //private readonly IMapper _mapper;

        //public WsProcessingService()
        //{
        //    _mapper = AutoMapperConfiguration.MapperConfig().CreateMapper();
        //}

        public HoSoVM AniFeed(Envelope envelope)
        {
            var body = envelope.Body;

            var hoso = body.Content.AniFeed;
            hoso.fiActive = true;
            hoso.fiHSStatus = (int)HSStatus.CHO_TIEP_NHAN;

            List<HangHoaVM> lstHangHoa = hoso.ListHangHoa;
            List<HopDongVM> lstHopDong = hoso.ListHopDong;
            List<HoaDonVM> lstHoaDon = hoso.ListHoaDon;
            List<PhieuDongGoiVM> lstPhieuDongGoi = hoso.ListPhieuDongGoi;
            List<AttachmentVM> lstDinhKep = hoso.ListAttachment;

            string sError = "";
            long iID_MaHoSo = 0;
            string sMaHoSo = hoso.fiNSWFileCode;

            try
            {
                //iID_MaHoSo_Sua = CHoSo.ThemHoSo(0, hoso.fiHSStatus, hoso.)

                int count = 0;
                foreach (var hh in lstHangHoa)
                {
                    count++;

                    long iID_MaHangHoa = 0;

                    List<ChatLuongVM> lstChatLuong = hh.ListChatLuong;
                    List<AnToanVM> lstAnToan = hh.ListAnToan;
                    List<SoLuongVM> lstSoLuong = hh.ListSoLuong;

                    iID_MaHangHoa = CHangHoa.ThemHangHoa(iID_MaHoSo, hh.GoodsId, hh.fiGroupFoodOfGoods, Convert.ToInt32(hh.fiGroupGoodId), Convert.ToInt32(hh.fiGoodTypeId), Convert.ToInt32(hh.fiGroupTypeId), Convert.ToInt32(hh.fiGoodsValueUnitCode), 0,
                        hh.fiGroupGoodName, hh.fiGoodTypeName, hh.fiGroupTypeName, sMaHoSo, hh.fiNameOfGoods, hh.fiRegistrationNumber, hh.fiManufacture, hh.fiManufactureStateCode, hh.fiManufactureState,
                        hh.fiNature, hh.fiGoodsValueUnitName, hh.fiMaterial, hh.fiFormColorOfProducts, hh.fiStandardBase, hh.fiTechnicalRegulations, hh.fiGoodsValue, hh.fiGoodsValueUSD, "",
                        "", "");

                    if (iID_MaHangHoa > 0)
                    {
                        foreach (var cl in lstChatLuong)
                        {
                            long iChatLuong = CHangHoa.ThemhangHoaChatLuong(iID_MaHangHoa, cl.fiQualityCriteriaName, cl.fiQualityFormOfPublication.ToString(), cl.fiQualityRequire.ToString(), cl.fiQualityRequireUnitID, cl.fiQualityRequireUnitName, "", false, "", "");
                        }

                        foreach (var at in lstAnToan)
                        {
                            long iAnToan = CHangHoa.ThemhangHoaAnToan(iID_MaHangHoa, 0, at.fiSafetyCriteriaName, at.fiSafetyFormOfPublication.ToString(), at.fiSafetyRequire.ToString(), at.fiSafetyRequireUnitID, at.fiSafetyRequireUnitName, "", false, "", "");
                        }

                        foreach (var sl in lstSoLuong)
                        {
                            long iChatLuong = CHangHoa.ThemhangHoaSoLuong(iID_MaHangHoa, sl.fiVolume, sl.fiVolumeUnitCode.ToString(), sl.fiVolumeUnitName.ToString(), sl.fiVolumeTAN, sl.fiQuantity, sl.fiQuantityUnitCode.ToString(), sl.fiQuantityUnitName, "", false, "", "");
                        }
                    }
                }
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

        public UploadBaoCaoVM UploadBaoCao(Envelope envelope)
        {

            return null;
        }

        public YeuCauHuyHoSoVM YeuCauHuyHoSo(Envelope envelope)
        {

            return null;
        }

        public GuiTCCDVM GuiTCCD(Envelope envelope)
        {

            return null;
        }

        public NopKetQuaVM NopKetQua(Envelope envelope)
        {

            return null;
        }
    }
}
