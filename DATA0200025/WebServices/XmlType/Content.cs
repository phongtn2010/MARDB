using DATA0200025.DTO;
using DATA0200025.DTO.Extensions;
using DATA0200025.DTO.Request;
using DATA0200025.WebServices.XmlType.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.WebServices.XmlType
{
    [XmlRoot("Content")]
    public class Content
    {
        public Content()
        {
        }

        public Content(List<Error> errors)
        {
            Errors = errors;
        }

        public Content(Error error)
        {
            Errors = new List<Error> { error };
        }

        public Content(DateTime receiveDate)
        {
            ReceiveDate = receiveDate;
        }

        #region InboundObjects

        [XmlElement("RegistrationProfile")] public HoSoVM RegistrationProfile { get; set; }
        [XmlElement("RequestProCancel")] public RequestProCancelVM RequestProCancel { get; set; }

        [XmlElement("RequestProEdit")] public RequestProEditVM RequestProEdit { get; set; }
        [XmlElement("RequestCancel")] public RequestCancelVM RequestCancel { get; set; }
        [XmlElement("RequestEditCer")] public RequestEditCerVM RequestEditCer { get; set; }
        #endregion

        #region OutboundObjects

        [XmlElement("Result")] public YeuCauBoSungHoSo Result { get; set; }

        [XmlElement("ResultConfirm")] public KetQuaThamDinh ResultConfirm { get; set; }

        //[XmlElement("ImportLicense")] public GPNhapKhauVM GPNhapKhau { get; set; }

        [XmlElement("ReponseCancel")] public ResponseCancel ResponseCancel { get; set; }

        [XmlElement("ProResponseEdit")] public ProResponseEdit ProResponseEdit { get; set; }

        #endregion

        #region ResponseObjects

        [XmlArray("ErrorList")]
        [XmlArrayItem("Error")]
        public List<Error> Errors { get; set; }

        [XmlElement("ReceiveDate", IsNullable = true)]
        public DateTime? ReceiveDate { get; set; }

        public bool ReceiveDateSpecified => ReceiveDate.HasValue;

        #endregion
    }
}
