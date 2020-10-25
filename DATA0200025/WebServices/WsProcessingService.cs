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

        public HoSoVM ReceiveRegistrationProfile(Envelope envelope)
        {
            var body = envelope.Body;

            var hoso = body.Content.RegistrationProfile;
            hoso.fiActive = true;
            hoso.fiHSStatus = (int)HSStatus.CHO_TIEP_NHAN;

            List<HangHoaVM> lstHangHoa = hoso.ListHangHoa;
            List<HopDongVM> lstHopDong = hoso.ListHopDong;
            List<HoaDonVM> lstHoaDon = hoso.ListHoaDon;
            List<PhieuDongGoiVM> lstPhieuDongGoi = hoso.ListPhieuDongGoi;
            List<AttachmentVM> lstDinhKep = hoso.ListAttachment;

            int count = 0;
            foreach (var hangHoa in lstHangHoa)
            {
                count++;

                List<ChatLuongVM> lstChatLuong = hangHoa.ListChatLuong;
                List<AnToanVM> lstAnToan = hangHoa.ListAnToan;
                List<SoLuongVM> lstSoLuong = hangHoa.ListSoLuong;
                
            }

            ////Them vao bang Ho So
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
