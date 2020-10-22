using AutoMapper;
using DATA0200025.DTO;
using DATA0200025.WebServices.XmlType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA0200025.WebServices
{
    public class WsProcessingService
    {
        private readonly IMapper _mapper;

        public WsProcessingService()
        {
            //_mapper = AutoMapperConfiguration.MapperConfig().CreateMapper();
        }

        public HoSoVM ReceiveRegistrationProfile(Envelope envelope)
        {

            var body = envelope.Body;

            var hoso = body.Content.RegistrationProfile;
            hoso.fiActive = true;
            hoso.fiHSStatus = (int)HSStatus.CHO_TIEP_NHAN;
            var hosoModel = new HoSo();
            hosoModel = _mapper.Map(hoso, hosoModel);
            hosoModel.IdDepartment = 1;
            var result = _hoSoService.Add(hosoModel);

            _lichSuHoSoService.Create(hoso.fiNSWFileCode, envelope.Header.From, "Gửi hồ sơ mới", "",
                (int)HSStatus.CHO_TIEP_NHAN);

            _lichSuHoSoService.Save();
            _hoSoService.Save();
            return _mapper.Map(result, hoso);
        }
    }
}
