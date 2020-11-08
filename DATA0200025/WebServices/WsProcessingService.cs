using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DATA0200025.DTO;
using DATA0200025.Models;
using DATA0200025.Models.Enums;
using DATA0200025.WebServices.XmlType;
using DomainModel.Abstract;
using Microsoft.Office.Interop.Excel;

namespace DATA0200025.WebServices
{
    public class WsProcessingService
    {
        private string sIP = "103.248.160.33";
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
            string sTenDoanhnghiep = hoso.sMua_Name;
            int iID_MaLoaiHoSo = hoso.fiTypeAniFeed;

            string sUserName = "doanhnghiep";

            try
            {
                String sTenLoaiHoSo = HamRiengModels.Get_Name_LoaiHinhThucKiemTra(hoso.fiTypeAniFeed);
                String sTenTACN = "";
                foreach (var hh in lstHangHoa)
                {
                    sTenTACN += hh.fiNameOfGoods + ";";
                }

                iID_MaHoSo = CHoSo.ThemHoSo(0, 1, hoso.fiTypeAniFeed, iID_MaHoSo_ThayThe, hoso.fiNSWFileCode, hoso.fiNSWFileCodeOld, hoso.fiCreateDate, false, "", null, null, null, null,
                hoso.fiTaxCode, sTenDoanhnghiep, sTenLoaiHoSo, "", sTenTACN, hoso.sBan_Name, hoso.sBan_DiaChi, hoso.sBan_Tel, hoso.sBan_Fax, "", hoso.sBan_MaQuocGia, hoso.sBan_QuocGia, hoso.sBan_NoiXuat,
                hoso.sMua_Name, hoso.sMua_DiaChi, hoso.sMua_Tel, hoso.sMua_Fax, "", hoso.sMua_NoiNhan, hoso.sMua_FromDate, hoso.sMua_ToDate,
                hoso.fiLocationOfStorage, hoso.fiLocationOfSampling, hoso.fiDateOfSamplingFrom, hoso.fiDateOfSamplingTo,
                hoso.fiContactPerson, hoso.fiContactAddress, hoso.fiContactTel, hoso.fiContactEmail,
                hoso.fiSignPlace, hoso.fiSignPlace, hoso.fiSignName, "", sUserName, sIP);

                if(iID_MaHoSo > 0)
                {
                    int iHH = 0;
                    foreach (var hh in lstHangHoa)
                    {
                        iHH++;

                        long iID_MaHangHoa = 0;

                        List<ChatLuongVM> lstChatLuong = hh.ListChatLuong;
                        List<AnToanVM> lstAnToan = hh.ListAnToan;
                        List<SoLuongVM> lstSoLuong = hh.ListSoLuong;

                        iID_MaHangHoa = CHangHoa.ThemHangHoa(iID_MaHoSo, hh.GoodsId, hh.fiGroupFoodOfGoods, Convert.ToString(hh.fiGroupGoodId), Convert.ToString(hh.fiGoodTypeId), Convert.ToString(hh.fiGroupTypeId), Convert.ToString(hh.fiGoodsValueUnitCode), 0,
                            hh.fiGroupGoodName, hh.fiGoodTypeName, hh.fiGroupTypeName, sMaHoSo, hh.fiNameOfGoods, hh.fiRegistrationNumber, hh.fiManufacture, hh.fiManufactureStateCode, hh.fiManufactureState,
                            hh.fiNature, hh.fiGoodsValueUnitName, hh.fiMaterial, hh.fiFormColorOfProducts, hh.fiStandardBase, hh.fiTechnicalRegulations, hh.fiGoodsValue, hh.fiGoodsValueUSD, "", sUserName, sIP);

                        if (iID_MaHangHoa > 0)
                        {
                            foreach (var cl in lstChatLuong)
                            {
                                string sHinhThuc = HamRiengModels.Get_Name_HinhThucCongBo(cl.fiQualityFormOfPublication);
                                long iChatLuong = CHangHoa.ThemhangHoaChatLuong(iID_MaHangHoa, cl.fiQualityFormOfPublication, cl.fiQualityCriteriaName, sHinhThuc, cl.fiQualityRequire.ToString(), cl.fiQualityRequireUnitID, cl.fiQualityRequireUnitName, "", false, sUserName, sIP);
                            }

                            foreach (var at in lstAnToan)
                            {
                                string sHinhThuc = HamRiengModels.Get_Name_HinhThucCongBo(at.fiSafetyFormOfPublication);
                                long iAnToan = CHangHoa.ThemhangHoaAnToan(iID_MaHangHoa, 0, at.fiSafetyFormOfPublication, at.fiSafetyCriteriaName, sHinhThuc, at.fiSafetyRequire.ToString(), at.fiSafetyRequireUnitID, at.fiSafetyRequireUnitName, "", false, sUserName, sIP);
                            }

                            foreach (var sl in lstSoLuong)
                            {
                                long iChatLuong = CHangHoa.ThemhangHoaSoLuong(iID_MaHangHoa, sl.fiVolume, sl.fiVolumeUnitCode.ToString(), sl.fiVolumeUnitName.ToString(), sl.fiVolumeTAN, sl.fiQuantity, sl.fiQuantityUnitCode.ToString(), sl.fiQuantityUnitName, "", false, sUserName, sIP);
                            }
                        }
                        else
                        {

                        }
                    }

                    int iHD = 0;
                    foreach (var hd in lstHopDong)
                    {
                        iHD++;
                        long iHopDong = CDinhKem.ThemDinhKem(iID_MaHoSo, 0, 100, hd.fiContractAttachmentId, sMaHoSo, "File Hợp Đồng Hồ Sơ", hd.fiContractName, hd.fiContractNo, hd.fiContractDate, 1, hd.fiContractFileLink, sUserName, sIP, Convert.ToInt64(hd.fiContractAttachmentId));
                    }

                    int iHDon = 0;
                    foreach (var hd in lstHoaDon)
                    {
                        iHDon++;
                        long iHoaDon = CDinhKem.ThemDinhKem(iID_MaHoSo, 0, 101, hd.fiInvoiceAttachmentId, sMaHoSo, "File Hóa Đơn  Hồ Sơ", hd.fiInvoiceName, hd.fiInvoiceNo, hd.fiInvoiceDate, 1, hd.fiInvoiceFileLink, sUserName, sIP, Convert.ToInt64(hd.fiInvoiceAttachmentId));
                    }

                    int iPDG = 0;
                    foreach (var p in lstPhieuDongGoi)
                    {
                        iPDG++;
                        long iHopDong = CDinhKem.ThemDinhKem(iID_MaHoSo, 0, 102, p.fiPackingAttachmentId, sMaHoSo, "File Phiếu Đóng Gói  Hồ Sơ", p.fiPackingName, p.fiPackingNo, p.fiPackingDate, 1, p.fiPackingFileLink, sUserName, sIP, Convert.ToInt64(p.fiPackingAttachmentId));
                    }

                    int iF = 0;
                    foreach (var f in lstDinhKem)
                    {
                        iF++;
                        long iFile = CDinhKem.ThemDinhKem(iID_MaHoSo, 0, f.fiFileCode, f.fiAttachmentId, sMaHoSo, "File Khác  Hồ Sơ", f.fiFileName, null, null, 1, f.fiFileLink, sUserName, sIP, Convert.ToInt64(f.fiAttachmentId));
                    }

                    clLichSuHoSo.InsertLichSuNsw(iID_MaHoSo, sUserName, sTenDoanhnghiep, 1, 1, "", "", 0, "Doanh nghiệp đăng ký từ NSW chuyển sang", 1);


                    //Doi voi ho so là 2d --> Chuyen trang thai sang 4 và gui ban tin 22_25
                    if (iID_MaLoaiHoSo == 4)
                    {
                        int hs2D = CHoSo.XuLy_TuDong_HoSo_2D(iID_MaHoSo, sMaHoSo, "Bộ phận một cửa", sUserName, sIP);
                    }
                }
                else
                {

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
            long iID_MaHoSo = 0, iID_MaHoSo_ThayThe = 0, iID_MaHoSo_Sua = 0;
            string sMaHoSo = hoso.fiNSWFileCode;
            string sTenDoanhnghiep = hoso.sMua_Name;
            int iID_MaLoaiHoSo = hoso.fiTypeAniFeed;

            string sUserName = "doanhnghiep";

            HoSoModels hs = clHoSo.GetHoSo_ChiTiet_Theo_Ma(sMaHoSo);
            if (hs != null)
            {
                iID_MaHoSo_Sua = hs.iID_MaHoSo;
            }

            try
            {
                String sTenLoaiHoSo = HamRiengModels.Get_Name_LoaiHinhThucKiemTra(hoso.fiTypeAniFeed);
                String sTenTACN = "";
                foreach (var hh in lstHangHoa)
                {
                    sTenTACN += hh.fiNameOfGoods + ";";
                }

                iID_MaHoSo = CHoSo.ThemHoSo(iID_MaHoSo_Sua, 1, hoso.fiTypeAniFeed, iID_MaHoSo_ThayThe, hoso.fiNSWFileCode, hoso.fiNSWFileCodeOld, hoso.fiCreateDate, false, "", null, null, null, null,
                hoso.fiTaxCode, sTenDoanhnghiep, sTenLoaiHoSo, "", sTenTACN, hoso.sBan_Name, hoso.sBan_DiaChi, hoso.sBan_Tel, hoso.sBan_Fax, "", hoso.sBan_MaQuocGia, hoso.sBan_QuocGia, hoso.sBan_NoiXuat,
                hoso.sMua_Name, hoso.sMua_DiaChi, hoso.sMua_Tel, hoso.sMua_Fax, "", hoso.sMua_NoiNhan, hoso.sMua_FromDate, hoso.sMua_ToDate,
                hoso.fiLocationOfStorage, hoso.fiLocationOfSampling, hoso.fiDateOfSamplingFrom, hoso.fiDateOfSamplingTo,
                hoso.fiContactPerson, hoso.fiContactAddress, hoso.fiContactTel, hoso.fiContactEmail,
                hoso.fiSignPlace, hoso.fiSignPlace, hoso.fiSignName, "", sUserName, sIP);

                int iHH = 0;
                foreach (var hh in lstHangHoa)
                {
                    iHH++;

                    long iID_MaHangHoa = 0, iID_MaHangHoa_Sua = 0;

                    List<ChatLuongVM> lstChatLuong = hh.ListChatLuong;
                    List<AnToanVM> lstAnToan = hh.ListAnToan;
                    List<SoLuongVM> lstSoLuong = hh.ListSoLuong;

                    System.Data.DataTable dtHH = CHangHoa.Get_HangHoa_Detail_Nsw(iID_MaHoSo_Sua, hh.GoodsId);
                    if(dtHH.Rows.Count > 0)
                    {
                        iID_MaHangHoa_Sua = Convert.ToInt64(dtHH.Rows[0]["iID_MaHangHoa"]);

                        iID_MaHangHoa = CHangHoa.ThemHangHoa(iID_MaHoSo, hh.GoodsId, hh.fiGroupFoodOfGoods, Convert.ToString(hh.fiGroupGoodId), Convert.ToString(hh.fiGoodTypeId), Convert.ToString(hh.fiGroupTypeId), Convert.ToString(hh.fiGoodsValueUnitCode), 0,
                            hh.fiGroupGoodName, hh.fiGoodTypeName, hh.fiGroupTypeName, sMaHoSo, hh.fiNameOfGoods, hh.fiRegistrationNumber, hh.fiManufacture, hh.fiManufactureStateCode, hh.fiManufactureState,
                            hh.fiNature, hh.fiGoodsValueUnitName, hh.fiMaterial, hh.fiFormColorOfProducts, hh.fiStandardBase, hh.fiTechnicalRegulations, hh.fiGoodsValue, hh.fiGoodsValueUSD, "", sUserName, sIP, iID_MaHangHoa_Sua);

                        if (iID_MaHangHoa > 0)
                        {
                            //Xoa thong tin lien quan hang hoa cu
                            CHangHoa.Delete_HangHoa_ThongTin(iID_MaHangHoa_Sua);

                            //Them lai thong tin hang hoa
                            foreach (var cl in lstChatLuong)
                            {
                                string sHinhThuc = HamRiengModels.Get_Name_HinhThucCongBo(cl.fiQualityFormOfPublication);
                                long iChatLuong = CHangHoa.ThemhangHoaChatLuong(iID_MaHangHoa, cl.fiQualityFormOfPublication, cl.fiQualityCriteriaName, sHinhThuc, cl.fiQualityRequire.ToString(), cl.fiQualityRequireUnitID, cl.fiQualityRequireUnitName, "", false, sUserName, sIP);
                            }

                            foreach (var at in lstAnToan)
                            {
                                string sHinhThuc = HamRiengModels.Get_Name_HinhThucCongBo(at.fiSafetyFormOfPublication);
                                long iAnToan = CHangHoa.ThemhangHoaAnToan(iID_MaHangHoa, 0, at.fiSafetyFormOfPublication, at.fiSafetyCriteriaName, sHinhThuc, at.fiSafetyRequire.ToString(), at.fiSafetyRequireUnitID, at.fiSafetyRequireUnitName, "", false, sUserName, sIP);
                            }

                            foreach (var sl in lstSoLuong)
                            {
                                long iChatLuong = CHangHoa.ThemhangHoaSoLuong(iID_MaHangHoa, sl.fiVolume, sl.fiVolumeUnitCode.ToString(), sl.fiVolumeUnitName.ToString(), sl.fiVolumeTAN, sl.fiQuantity, sl.fiQuantityUnitCode.ToString(), sl.fiQuantityUnitName, "", false, sUserName, sIP);
                            }
                        }
                    }
                }

                int iHD = 0;
                foreach (var hd in lstHopDong)
                {
                    iHD++;
                    long iHopDong = CDinhKem.ThemDinhKem(iID_MaHoSo, 0, 100, hd.fiContractAttachmentId, sMaHoSo, "File Hợp Đồng Hồ Sơ", hd.fiContractName, hd.fiContractNo, hd.fiContractDate, 1, hd.fiContractFileLink, sUserName, sIP, Convert.ToInt64(hd.fiContractAttachmentId));
                }

                int iHDon = 0;
                foreach (var hd in lstHoaDon)
                {
                    iHDon++;
                    long iHoaDon = CDinhKem.ThemDinhKem(iID_MaHoSo, 0, 101, hd.fiInvoiceAttachmentId, sMaHoSo, "File Hóa Đơn  Hồ Sơ", hd.fiInvoiceName, hd.fiInvoiceNo, hd.fiInvoiceDate, 1, hd.fiInvoiceFileLink, sUserName, sIP, Convert.ToInt64(hd.fiInvoiceAttachmentId));
                }

                int iPDG = 0;
                foreach (var p in lstPhieuDongGoi)
                {
                    iPDG++;
                    long iHopDong = CDinhKem.ThemDinhKem(iID_MaHoSo, 0, 102, p.fiPackingAttachmentId, sMaHoSo, "File Phiếu Đóng Gói  Hồ Sơ", p.fiPackingName, p.fiPackingNo, p.fiPackingDate, 1, p.fiPackingFileLink, sUserName, sIP, Convert.ToInt64(p.fiPackingAttachmentId));
                }

                int iF = 0;
                foreach (var f in lstDinhKem)
                {
                    iF++;
                    long iFile = CDinhKem.ThemDinhKem(iID_MaHoSo, 0, f.fiFileCode, f.fiAttachmentId, sMaHoSo, "File Khác  Hồ Sơ", f.fiFileName, null, null, 1, f.fiFileLink, sUserName, sIP, Convert.ToInt64(f.fiAttachmentId));
                }

                clLichSuHoSo.InsertLichSuNsw(iID_MaHoSo, sUserName, sTenDoanhnghiep, 1, 1, "", "", 0, "Sửa hồ sơ trước khi BNN tiếp nhận hồ sơ", 1);

                //Doi voi ho so là 2d --> Chuyen trang thai sang 4 và gui ban tin 22_25
                if (iID_MaLoaiHoSo == 4)
                {
                    int hs2D = CHoSo.XuLy_TuDong_HoSo_2D(iID_MaHoSo, sMaHoSo, "Bộ phận một cửa", sUserName, sIP);
                }
            }
            catch (Exception ex)
            {
                sError = "Error Add Product: " + ex.ToString();
            }

            return null;
        }

        public HoSoVM GuiSuaHoSoBPMC(Envelope envelope)
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
            long iID_MaHoSo = 0, iID_MaHoSo_ThayThe = 0, iID_MaHoSo_Sua = 0;
            string sMaHoSo = hoso.fiNSWFileCode;
            string sTenDoanhnghiep = hoso.sMua_Name;
            int iID_MaLoaiHoSo = hoso.fiTypeAniFeed;

            string sUserName = "doanhnghiep";

            HoSoModels hs = clHoSo.GetHoSo_ChiTiet_Theo_Ma(sMaHoSo);
            if (hs != null)
            {
                iID_MaHoSo_Sua = hs.iID_MaHoSo;
            }

            try
            {
                String sTenLoaiHoSo = HamRiengModels.Get_Name_LoaiHinhThucKiemTra(hoso.fiTypeAniFeed);
                String sTenTACN = "";
                foreach (var hh in lstHangHoa)
                {
                    sTenTACN += hh.fiNameOfGoods + ";";
                }

                iID_MaHoSo = CHoSo.ThemHoSo(iID_MaHoSo_Sua, 3, hoso.fiTypeAniFeed, iID_MaHoSo_ThayThe, hoso.fiNSWFileCode, hoso.fiNSWFileCodeOld, hoso.fiCreateDate, false, "", null, null, null, null,
                hoso.fiTaxCode, sTenDoanhnghiep, sTenLoaiHoSo, "", sTenTACN, hoso.sBan_Name, hoso.sBan_DiaChi, hoso.sBan_Tel, hoso.sBan_Fax, "", hoso.sBan_MaQuocGia, hoso.sBan_QuocGia, hoso.sBan_NoiXuat,
                hoso.sMua_Name, hoso.sMua_DiaChi, hoso.sMua_Tel, hoso.sMua_Fax, "", hoso.sMua_NoiNhan, hoso.sMua_FromDate, hoso.sMua_ToDate,
                hoso.fiLocationOfStorage, hoso.fiLocationOfSampling, hoso.fiDateOfSamplingFrom, hoso.fiDateOfSamplingTo,
                hoso.fiContactPerson, hoso.fiContactAddress, hoso.fiContactTel, hoso.fiContactEmail,
                hoso.fiSignPlace, hoso.fiSignPlace, hoso.fiSignName, "", sUserName, sIP);

                int iHH = 0;
                foreach (var hh in lstHangHoa)
                {
                    iHH++;

                    long iID_MaHangHoa = 0, iID_MaHangHoa_Sua = 0;

                    List<ChatLuongVM> lstChatLuong = hh.ListChatLuong;
                    List<AnToanVM> lstAnToan = hh.ListAnToan;
                    List<SoLuongVM> lstSoLuong = hh.ListSoLuong;

                    System.Data.DataTable dtHH = CHangHoa.Get_HangHoa_Detail_Nsw(iID_MaHoSo_Sua, hh.GoodsId);
                    if (dtHH.Rows.Count > 0)
                    {
                        iID_MaHangHoa_Sua = Convert.ToInt64(dtHH.Rows[0]["iID_MaHangHoa"]);

                        iID_MaHangHoa = CHangHoa.ThemHangHoa(iID_MaHoSo, hh.GoodsId, hh.fiGroupFoodOfGoods, Convert.ToString(hh.fiGroupGoodId), Convert.ToString(hh.fiGoodTypeId), Convert.ToString(hh.fiGroupTypeId), Convert.ToString(hh.fiGoodsValueUnitCode), 0,
                            hh.fiGroupGoodName, hh.fiGoodTypeName, hh.fiGroupTypeName, sMaHoSo, hh.fiNameOfGoods, hh.fiRegistrationNumber, hh.fiManufacture, hh.fiManufactureStateCode, hh.fiManufactureState,
                            hh.fiNature, hh.fiGoodsValueUnitName, hh.fiMaterial, hh.fiFormColorOfProducts, hh.fiStandardBase, hh.fiTechnicalRegulations, hh.fiGoodsValue, hh.fiGoodsValueUSD, "", sUserName, sIP, iID_MaHangHoa_Sua);

                        if (iID_MaHangHoa > 0)
                        {
                            //Xoa thong tin lien quan hang hoa cu
                            CHangHoa.Delete_HangHoa_ThongTin(iID_MaHangHoa_Sua);

                            //Them lai thong tin hang hoa
                            foreach (var cl in lstChatLuong)
                            {
                                string sHinhThuc = HamRiengModels.Get_Name_HinhThucCongBo(cl.fiQualityFormOfPublication);
                                long iChatLuong = CHangHoa.ThemhangHoaChatLuong(iID_MaHangHoa, cl.fiQualityFormOfPublication, cl.fiQualityCriteriaName, sHinhThuc, cl.fiQualityRequire.ToString(), cl.fiQualityRequireUnitID, cl.fiQualityRequireUnitName, "", false, sUserName, sIP);
                            }

                            foreach (var at in lstAnToan)
                            {
                                string sHinhThuc = HamRiengModels.Get_Name_HinhThucCongBo(at.fiSafetyFormOfPublication);
                                long iAnToan = CHangHoa.ThemhangHoaAnToan(iID_MaHangHoa, 0, at.fiSafetyFormOfPublication, at.fiSafetyCriteriaName, sHinhThuc, at.fiSafetyRequire.ToString(), at.fiSafetyRequireUnitID, at.fiSafetyRequireUnitName, "", false, sUserName, sIP);
                            }

                            foreach (var sl in lstSoLuong)
                            {
                                long iChatLuong = CHangHoa.ThemhangHoaSoLuong(iID_MaHangHoa, sl.fiVolume, sl.fiVolumeUnitCode.ToString(), sl.fiVolumeUnitName.ToString(), sl.fiVolumeTAN, sl.fiQuantity, sl.fiQuantityUnitCode.ToString(), sl.fiQuantityUnitName, "", false, sUserName, sIP);
                            }
                        }
                    }
                }

                int iHD = 0;
                foreach (var hd in lstHopDong)
                {
                    iHD++;
                    long iHopDong = CDinhKem.ThemDinhKem(iID_MaHoSo, 0, 100, hd.fiContractAttachmentId, sMaHoSo, "File Hợp Đồng Hồ Sơ", hd.fiContractName, hd.fiContractNo, hd.fiContractDate, 1, hd.fiContractFileLink, sUserName, sIP, Convert.ToInt64(hd.fiContractAttachmentId));
                }

                int iHDon = 0;
                foreach (var hd in lstHoaDon)
                {
                    iHDon++;
                    long iHoaDon = CDinhKem.ThemDinhKem(iID_MaHoSo, 0, 101, hd.fiInvoiceAttachmentId, sMaHoSo, "File Hóa Đơn  Hồ Sơ", hd.fiInvoiceName, hd.fiInvoiceNo, hd.fiInvoiceDate, 1, hd.fiInvoiceFileLink, sUserName, sIP, Convert.ToInt64(hd.fiInvoiceAttachmentId));
                }

                int iPDG = 0;
                foreach (var p in lstPhieuDongGoi)
                {
                    iPDG++;
                    long iHopDong = CDinhKem.ThemDinhKem(iID_MaHoSo, 0, 102, p.fiPackingAttachmentId, sMaHoSo, "File Phiếu Đóng Gói  Hồ Sơ", p.fiPackingName, p.fiPackingNo, p.fiPackingDate, 1, p.fiPackingFileLink, sUserName, sIP, Convert.ToInt64(p.fiPackingAttachmentId));
                }

                int iF = 0;
                foreach (var f in lstDinhKem)
                {
                    iF++;
                    long iFile = CDinhKem.ThemDinhKem(iID_MaHoSo, 0, f.fiFileCode, f.fiAttachmentId, sMaHoSo, "File Khác  Hồ Sơ", f.fiFileName, null, null, 1, f.fiFileLink, sUserName, sIP, Convert.ToInt64(f.fiAttachmentId));
                }

                clLichSuHoSo.InsertLichSuNsw(iID_MaHoSo, sUserName, sTenDoanhnghiep, 1, 2, "", "", 0, "Chờ tiếp nhận hồ sơ gửi bổ sung theo BPMC", 3);

                //Doi voi ho so là 2d --> Chuyen trang thai sang 4 và gui ban tin 22_25
                if (iID_MaLoaiHoSo == 4)
                {
                    int hs2D = CHoSo.XuLy_TuDong_HoSo_2D(iID_MaHoSo, sMaHoSo, "Bộ phận một cửa", sUserName, sIP);
                }
            }
            catch (Exception ex)
            {
                sError = "Error Add Product: " + ex.ToString();
            }

            return null;
        }

        public HoSoVM GuiSuaHoSoPhongTACN(Envelope envelope)
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
            long iID_MaHoSo = 0, iID_MaHoSo_ThayThe = 0, iID_MaHoSo_Sua = 0;
            string sMaHoSo = hoso.fiNSWFileCode;
            string sTenDoanhnghiep = hoso.sMua_Name;
            int iID_MaLoaiHoSo = hoso.fiTypeAniFeed;

            string sUserName = "doanhnghiep";

            HoSoModels hs = clHoSo.GetHoSo_ChiTiet_Theo_Ma(sMaHoSo);
            if (hs != null)
            {
                iID_MaHoSo_Sua = hs.iID_MaHoSo;
            }

            try
            {
                String sTenLoaiHoSo = HamRiengModels.Get_Name_LoaiHinhThucKiemTra(hoso.fiTypeAniFeed);
                String sTenTACN = "";
                foreach (var hh in lstHangHoa)
                {
                    sTenTACN += hh.fiNameOfGoods + ";";
                }

                iID_MaHoSo = CHoSo.ThemHoSo(iID_MaHoSo_Sua, 4, hoso.fiTypeAniFeed, iID_MaHoSo_ThayThe, hoso.fiNSWFileCode, hoso.fiNSWFileCodeOld, hoso.fiCreateDate, false, "", null, null, null, null,
                hoso.fiTaxCode, sTenDoanhnghiep, sTenLoaiHoSo, "", sTenTACN, hoso.sBan_Name, hoso.sBan_DiaChi, hoso.sBan_Tel, hoso.sBan_Fax, "", hoso.sBan_MaQuocGia, hoso.sBan_QuocGia, hoso.sBan_NoiXuat,
                hoso.sMua_Name, hoso.sMua_DiaChi, hoso.sMua_Tel, hoso.sMua_Fax, "", hoso.sMua_NoiNhan, hoso.sMua_FromDate, hoso.sMua_ToDate,
                hoso.fiLocationOfStorage, hoso.fiLocationOfSampling, hoso.fiDateOfSamplingFrom, hoso.fiDateOfSamplingTo,
                hoso.fiContactPerson, hoso.fiContactAddress, hoso.fiContactTel, hoso.fiContactEmail,
                hoso.fiSignPlace, hoso.fiSignPlace, hoso.fiSignName, "", sUserName, sIP);

                int iHH = 0;
                foreach (var hh in lstHangHoa)
                {
                    iHH++;

                    long iID_MaHangHoa = 0, iID_MaHangHoa_Sua = 0;

                    List<ChatLuongVM> lstChatLuong = hh.ListChatLuong;
                    List<AnToanVM> lstAnToan = hh.ListAnToan;
                    List<SoLuongVM> lstSoLuong = hh.ListSoLuong;

                    System.Data.DataTable dtHH = CHangHoa.Get_HangHoa_Detail_Nsw(iID_MaHoSo_Sua, hh.GoodsId);
                    if (dtHH.Rows.Count > 0)
                    {
                        iID_MaHangHoa_Sua = Convert.ToInt64(dtHH.Rows[0]["iID_MaHangHoa"]);

                        iID_MaHangHoa = CHangHoa.ThemHangHoa(iID_MaHoSo, hh.GoodsId, hh.fiGroupFoodOfGoods, Convert.ToString(hh.fiGroupGoodId), Convert.ToString(hh.fiGoodTypeId), Convert.ToString(hh.fiGroupTypeId), Convert.ToString(hh.fiGoodsValueUnitCode), 0,
                            hh.fiGroupGoodName, hh.fiGoodTypeName, hh.fiGroupTypeName, sMaHoSo, hh.fiNameOfGoods, hh.fiRegistrationNumber, hh.fiManufacture, hh.fiManufactureStateCode, hh.fiManufactureState,
                            hh.fiNature, hh.fiGoodsValueUnitName, hh.fiMaterial, hh.fiFormColorOfProducts, hh.fiStandardBase, hh.fiTechnicalRegulations, hh.fiGoodsValue, hh.fiGoodsValueUSD, "", sUserName, sIP, iID_MaHangHoa_Sua);

                        if (iID_MaHangHoa > 0)
                        {
                            //Xoa thong tin lien quan hang hoa cu
                            CHangHoa.Delete_HangHoa_ThongTin(iID_MaHangHoa_Sua);

                            //Them lai thong tin hang hoa
                            foreach (var cl in lstChatLuong)
                            {
                                string sHinhThuc = HamRiengModels.Get_Name_HinhThucCongBo(cl.fiQualityFormOfPublication);
                                long iChatLuong = CHangHoa.ThemhangHoaChatLuong(iID_MaHangHoa, cl.fiQualityFormOfPublication, cl.fiQualityCriteriaName, sHinhThuc, cl.fiQualityRequire.ToString(), cl.fiQualityRequireUnitID, cl.fiQualityRequireUnitName, "", false, sUserName, sIP);
                            }

                            foreach (var at in lstAnToan)
                            {
                                string sHinhThuc = HamRiengModels.Get_Name_HinhThucCongBo(at.fiSafetyFormOfPublication);
                                long iAnToan = CHangHoa.ThemhangHoaAnToan(iID_MaHangHoa, 0, at.fiSafetyFormOfPublication, at.fiSafetyCriteriaName, sHinhThuc, at.fiSafetyRequire.ToString(), at.fiSafetyRequireUnitID, at.fiSafetyRequireUnitName, "", false, sUserName, sIP);
                            }

                            foreach (var sl in lstSoLuong)
                            {
                                long iChatLuong = CHangHoa.ThemhangHoaSoLuong(iID_MaHangHoa, sl.fiVolume, sl.fiVolumeUnitCode.ToString(), sl.fiVolumeUnitName.ToString(), sl.fiVolumeTAN, sl.fiQuantity, sl.fiQuantityUnitCode.ToString(), sl.fiQuantityUnitName, "", false, sUserName, sIP);
                            }
                        }
                    }
                }

                int iHD = 0;
                foreach (var hd in lstHopDong)
                {
                    iHD++;
                    long iHopDong = CDinhKem.ThemDinhKem(iID_MaHoSo, 0, 100, hd.fiContractAttachmentId, sMaHoSo, "File Hợp Đồng Hồ Sơ", hd.fiContractName, hd.fiContractNo, hd.fiContractDate, 1, hd.fiContractFileLink, sUserName, sIP, Convert.ToInt64(hd.fiContractAttachmentId));
                }

                int iHDon = 0;
                foreach (var hd in lstHoaDon)
                {
                    iHDon++;
                    long iHoaDon = CDinhKem.ThemDinhKem(iID_MaHoSo, 0, 101, hd.fiInvoiceAttachmentId, sMaHoSo, "File Hóa Đơn  Hồ Sơ", hd.fiInvoiceName, hd.fiInvoiceNo, hd.fiInvoiceDate, 1, hd.fiInvoiceFileLink, sUserName, sIP, Convert.ToInt64(hd.fiInvoiceAttachmentId));
                }

                int iPDG = 0;
                foreach (var p in lstPhieuDongGoi)
                {
                    iPDG++;
                    long iHopDong = CDinhKem.ThemDinhKem(iID_MaHoSo, 0, 102, p.fiPackingAttachmentId, sMaHoSo, "File Phiếu Đóng Gói  Hồ Sơ", p.fiPackingName, p.fiPackingNo, p.fiPackingDate, 1, p.fiPackingFileLink, sUserName, sIP, Convert.ToInt64(p.fiPackingAttachmentId));
                }

                int iF = 0;
                foreach (var f in lstDinhKem)
                {
                    iF++;
                    long iFile = CDinhKem.ThemDinhKem(iID_MaHoSo, 0, f.fiFileCode, f.fiAttachmentId, sMaHoSo, "File Khác  Hồ Sơ", f.fiFileName, null, null, 1, f.fiFileLink, sUserName, sIP, Convert.ToInt64(f.fiAttachmentId));
                }

                clLichSuHoSo.InsertLichSuNsw(iID_MaHoSo, sUserName, sTenDoanhnghiep, 1, 2, "", "", 0, "Chờ tiếp nhận hồ sơ gửi bổ sung theo phòng TACN", 4);

                //Doi voi ho so là 2d --> Chuyen trang thai sang 4 và gui ban tin 22_25
                if (iID_MaLoaiHoSo == 4)
                {
                    int hs2D = CHoSo.XuLy_TuDong_HoSo_2D(iID_MaHoSo, sMaHoSo, "Bộ phận một cửa", sUserName, sIP);
                }
            }
            catch (Exception ex)
            {
                sError = "Error Add Product: " + ex.ToString();
            }

            return null;
        }

        public UploadBaoCaoVM Report(Envelope envelope)
        {
            var body = envelope.Body;

            var hoso = body.Content.Report;
            string sMaHoSo = hoso.fiNSWFileCode;

            List<AttachmentVM> lstDinhKem = hoso.ListAttachmentReports;

            string sUserName = "doanhnghiep";
            string sError = "";

            long iID_MaHoSo = 0, iID_MaHoSo_TCCD = 0;
            string sTenDoanhNghiep = "", iID_MaHangHoa = "";
            HoSoModels hs = clHoSo.GetHoSo_ChiTiet_Theo_Ma(sMaHoSo);
            if (hs != null)
            {
                iID_MaHoSo = hs.iID_MaHoSo;
                sTenDoanhNghiep = hs.sTenDoanhNghiep;

                try
                {
                    //Them vao bang Dinh Kem
                    int iF = 0;
                    foreach (var f in lstDinhKem)
                    {
                        iF++;
                        long iFile = CDinhKem.ThemDinhKem(iID_MaHoSo, 0, f.fiFileCode, f.fiAttachmentId, sMaHoSo, "Upload Báo Cáo", f.fiFileName, null, null, 1, f.fiFileLink, sUserName, sIP, Convert.ToInt64(f.fiAttachmentId));
                    }

                    //Update Trang Thai Hồ sơ
                    Bang bang = new Bang("CNN25_HoSo");
                    bang.MaNguoiDungSua = sUserName;
                    bang.IPSua = sIP;
                    bang.DuLieuMoi = false;
                    bang.GiaTriKhoa = iID_MaHoSo;
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", 5);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", 4);
                    bang.Save();

                    //CHoSo.UpDate_TrangThai(iID_MaHoSo, 5);

                    //Ghi Lai Lich Su
                    clLichSuHoSo.InsertLichSu(iID_MaHoSo, sUserName, (int)clDoiTuong.DoiTuong.DoanhNghiep, (int)clHanhDong.HanhDong.UpLoadBaoCao, "", "", 4, 5);
                }
                catch (Exception ex)
                {
                    sError = "Error Add Ho So TCCD: " + ex.ToString();
                }
            }
            return null;
        }

        public YeuCauHuyHoSoVM RequestCancel(Envelope envelope)
        {
            var body = envelope.Body;

            var hoso = body.Content.RequestCancel;
            string sMaHoSo = hoso.fiNSWFileCode;

            long iID_MaHoSo = 0, iID_MaHoSo_Huy = 0;
            string sTenDoanhNghiep = "";
            HoSoModels hs = clHoSo.GetHoSo_ChiTiet_Theo_Ma(sMaHoSo);
            if (hs != null)
            {
                iID_MaHoSo = hs.iID_MaHoSo;
                sTenDoanhNghiep = hs.sTenDoanhNghiep;
            }

            string sUserName = "doanhnghiep";
            string sError = "";
            try
            {
                //Them vao bang Ho So Huy
                iID_MaHoSo_Huy = CHoSo.ThemHoSoHuy(iID_MaHoSo, 0, sMaHoSo, hoso.fiRequestDate, hoso.fiReason, hoso.fiAttachmentId, hoso.fiFileName, hoso.fiFileLink, sUserName, sIP);

                if(iID_MaHoSo_Huy > 0)
                {
                    //Update Trang Thai Hang Hoa
                    CHoSo.UpDate_TrangThai(iID_MaHoSo, 45);

                    //Ghi Lai Lich Su
                    clLichSuHoSo.InsertLichSuNsw(iID_MaHoSo, sUserName, sTenDoanhNghiep, 1, 39, hoso.fiReason, hoso.fiFileLink, 0, "", 45);
                }
                else
                {
                    sError = "Error hàm: CHoSo.ThemHoSoHuy";
                }
            }
            catch (Exception ex)
            {
                sError = "Error Add Ho So Huy: " + ex.ToString();
            }

            return hoso;
        }

        public GuiTCCDVM TestInformation(Envelope envelope)
        {
            var body = envelope.Body;

            var hoso = body.Content.TestInformation;
            string sMaHoSo = hoso.fiNSWFileCode;

            List<HangHoaTCCDVM> lstHangHoa = hoso.ListHangHoa;

            string sUserName = "doanhnghiep";
            string sError = "";

            long iID_MaHoSo = 0, iID_MaHoSo_TCCD = 0;
            string sTenDoanhNghiep = "", iID_MaHangHoa = "";
            HoSoModels hs = clHoSo.GetHoSo_ChiTiet_Theo_Ma(sMaHoSo);
            if (hs != null)
            {
                iID_MaHoSo = hs.iID_MaHoSo;
                sTenDoanhNghiep = hs.sTenDoanhNghiep;

                try
                {
                    int iHH = 0;
                    foreach (var hh in lstHangHoa)
                    {
                        iHH++;

                        iID_MaHangHoa += hh.fiGoodsId + ",";
                    }

                    //Them vao bang HoSo_XNCL
                    iID_MaHoSo_TCCD = CHoSo.ThemHoSoTCCD(0, iID_MaHoSo, iID_MaHangHoa, hoso.fiAssignCode, sMaHoSo, hoso.fiAssignName, sUserName, sIP);

                    //Update trang thai Ho So
                    CHoSo.UpDate_TrangThai(iID_MaHoSo, 27);

                    //Update Trang Thai Hang Hoa
                    CHangHoa.UpDate_TrangThai_TheoHoSo(iID_MaHoSo, 27);

                    //Ghi Lai Lich Su
                    clLichSuHoSo.InsertLichSuNsw(iID_MaHoSo, sUserName, sTenDoanhNghiep, 1, 25, "", "", 0, "Doanh nghiệp Chuyển chỉ tiêu kiểm tra của cả lô hàng cho tổ chức chỉ định đối với hình thức kiểm tra 2c từ NSW", 1);
                }
                catch (Exception ex)
                {
                    sError = "Error Add Ho So TCCD: " + ex.ToString();
                }
            }

            return null;
        }

        public NopKetQuaVM SendResultTest(Envelope envelope)
        {
            var body = envelope.Body;

            var hoso = body.Content.SendResultTest;
            string sMaHoSo = hoso.fiNSWFileCode;
            List<AttachmentVM> lstDinhKem = hoso.ListAttachmentReports;

            long iID_MaHoSo_XNCL = 0, iID_MaHoSo = 0, iID_MaHangHoa = 0;
            iID_MaHangHoa = hoso.fiGoodsId;
            string sTenDoanhNghiep = "";
            HoSoModels hs = clHoSo.GetHoSo_ChiTiet_Theo_Ma(sMaHoSo);
            if(hs != null)
            {
                iID_MaHoSo = hs.iID_MaHoSo;
                sTenDoanhNghiep = hs.sTenDoanhNghiep;
            }

            string sUserName = "doanhnghiep";
            string sError = "";
            try
            {
                //Them vao bang HoSo_XNCL
                iID_MaHoSo_XNCL = CHoSo.ThemHoSoXNCL(0, iID_MaHoSo, iID_MaHangHoa, hoso.fiAssignCode, sMaHoSo, hoso.fiNameOfGoods, hoso.fiAssignName, hoso.fiTestConfirmNumber, hoso.fiTestConfirmDate, hoso.fiResultTest,
                    hoso.fiTestConfirmAttachmentId, hoso.fiTestConfirmFileName, hoso.fiTestConfirmFileLink, sUserName, sIP);

                //Them vao bang Dinh Kem
                int iF = 0;
                foreach (var f in lstDinhKem)
                {
                    iF++;
                    long iFile = CDinhKem.ThemDinhKem(iID_MaHoSo_XNCL, iID_MaHangHoa, f.fiFileCode, f.fiAttachmentId, sMaHoSo, "File Hồ Sơ XNCL", f.fiFileName, null, null, 1, f.fiFileLink, sUserName, sIP, Convert.ToInt64(f.fiAttachmentId));
                }

                //Update Trang Thai Hang Hoa
                CHangHoa.UpDate_TrangThai(iID_MaHangHoa, 29);

                //Ghi Lai Lich Su
                clLichSuHangHoa.InsertLichSuNsw(iID_MaHangHoa, sUserName, sTenDoanhNghiep, 1, 27, "Hồ sơ xác nhận chất lượng", "", 26, "Hồ sơ Đã xác nhận GĐK", 29);
            }
            catch(Exception ex)
            {
                sError = "Error Add Ho So XNCL: " + ex.ToString();
            }            

            return null;
        }
    }
}
