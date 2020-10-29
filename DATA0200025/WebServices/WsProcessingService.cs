using System;
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
            List<AttachmentVM> lstDinhKem = hoso.ListAttachment;

            string sError = "";
            long iID_MaHoSo = 0, iID_MaHoSo_ThayThe = 0;
            string sMaHoSo = hoso.fiNSWFileCode;

            try
            {
                String sTenLoaiHoSo = "";
                String sTenTACN = "";

                iID_MaHoSo = CHoSo.ThemHoSo(0, 1, hoso.fiTypeAniFeed, iID_MaHoSo_ThayThe, hoso.fiNSWFileCode, hoso.fiNSWFileCodeOld, hoso.fiCreateDate, false, "", null, null, null, null,
                    hoso.fiTaxCode, hoso.sMua_Name, sTenLoaiHoSo, "", sTenTACN, hoso.sBan_Name, hoso.sBan_DiaChi, hoso.sBan_Tel, hoso.sBan_Fax, "", hoso.sBan_MaQuocGia, hoso.sBan_QuocGia, hoso.sBan_NoiXuat,
                    hoso.sMua_Name, hoso.sMua_DiaChi, hoso.sMua_Tel, hoso.sMua_Fax, "", hoso.sMua_NoiNhan, hoso.sMua_FromDate, hoso.sMua_ToDate,
                    hoso.fiLocationOfStorage, hoso.fiLocationOfSampling, hoso.fiDateOfSamplingFrom, hoso.fiDateOfSamplingTo,
                    hoso.fiContactPerson, hoso.fiContactAddress, hoso.fiContactTel, hoso.fiContactEmail,
                    hoso.fiSignPlace, hoso.fiSignPlace, hoso.fiSignName, "", "Doanh Nghiep", "");

                int iHH = 0;
                foreach (var hh in lstHangHoa)
                {
                    iHH++;

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

                int iHD = 0;
                foreach(var hd in lstHopDong)
                {
                    iHD++;
                    int iHopDong = CDinhKem.ThemDinhKem(iID_MaHoSo, 0, 100, hd.fiContractAttachmentId, sMaHoSo, "File Hợp Đồng", hd.fiContractName, hd.fiContractNo, hd.fiContractDate, 1, hd.fiContractFileLink, "", "");
                }

                int iHDon = 0;
                foreach (var hd in lstHoaDon)
                {
                    iHDon++;
                    int iHoaDon = CDinhKem.ThemDinhKem(iID_MaHoSo, 0, 101, hd.fiInvoiceAttachmentId, sMaHoSo, "File Hóa Đơn", hd.fiInvoiceName, hd.fiInvoiceNo, hd.fiInvoiceDate, 1, hd.fiInvoiceFileLink, "", "");
                }

                int iPDG = 0;
                foreach (var p in lstPhieuDongGoi)
                {
                    iPDG++;
                    int iHopDong = CDinhKem.ThemDinhKem(iID_MaHoSo, 0, 102, p.fiPackingAttachmentId, sMaHoSo, "File Hợp Đồng", p.fiPackingName, p.fiPackingNo, p.fiPackingDate, 1, p.fiPackingFileLink, "", "");
                }

                int iF = 0;
                foreach (var f in lstDinhKem)
                {
                    iF++;
                    int iFile = CDinhKem.ThemDinhKem(iID_MaHoSo, 0, f.fiFileCode, f.fiAttachmentId, sMaHoSo, "File Khác", f.fiFileName, null, null, 1, f.fiFileLink, "", "");
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

        public HoSoVM GuiSuaHoSo(Envelope envelope)
        {
            //var body = envelope.Body;

            //var hoso = body.Content.AniFeed;

            //var oldHoso = _hoSoService.GetByNSWFileCode(hoso.fiNSWFileCode);

            //if (oldHoso == null)
            //{
            //    throw new Exception("Hồ sơ cũ không tồn tại");
            //}

            //if (oldHoso.fiHSStatus > (int)HSStatus.CHO_TIEP_NHAN)
            //{
            //    throw new Exception("Hồ sơ đang được xử lý");
            //}

            //oldHoso.fiActive = false;

            //hoso.fiActive = true;
            //hoso.fiHSStatus = (int)HSStatus.CHO_TIEP_NHAN;

            //var hosoModel = new HoSo();
            //hosoModel = _mapper.Map(hoso, hosoModel);
            //var result = _hoSoService.Add(hosoModel);

            //_lichSuHoSoService.Create(hoso.fiNSWFileCode, envelope.Header.From, "Gửi sửa hồ sơ khi BPMC chưa tiếp nhận", "",
            //    (int)HSStatus.CHO_TIEP_NHAN);

            //_lichSuHoSoService.Save();
            //_hoSoService.Save();
            //return _mapper.Map(result, hoso);

            return null;
        }

        public UploadBaoCaoVM Report(Envelope envelope)
        {

            return null;
        }

        public YeuCauHuyHoSoVM RequestCancel(Envelope envelope)
        {

            return null;
        }

        public GuiTCCDVM TestInformation(Envelope envelope)
        {

            return null;
        }

        public NopKetQuaVM SendResultTest(Envelope envelope)
        {

            return null;
        }
    }
}
