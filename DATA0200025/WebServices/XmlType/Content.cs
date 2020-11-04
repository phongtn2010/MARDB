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

        #region ResponseObjects
        private string _date; // Private variable to store XML string

        // Property that exposes date. Specifying the type forces
        // the serializer to return the value as a string.
        [XmlElement("ReceiveDate", Type = typeof(string))]
        public object ReceiveDate
        {
            // Return a DateTime object
            get
            {
                return
                    !string.IsNullOrEmpty(_date) ?
                    (DateTime?)Convert.ToDateTime(_date) :
                    null;
            }
            set { _date = (string)value; }
        }
        //[XmlElement("ReceiveDate")] public DateTime? ReceiveDate { get; set; }

        //public bool ReceiveDateSpecified => ReceiveDate.HasValue;

        [XmlArray("ErrorList")]
        [XmlArrayItem("Error")]
        public List<Error> Errors { get; set; }

        #endregion

        #region InboundObjects

        [XmlElement("AniFeed")] public HoSoVM AniFeed { get; set; }

        [XmlElement("Report")] public UploadBaoCaoVM Report { get; set; }

        [XmlElement("RequestCancel")] public YeuCauHuyHoSoVM RequestCancel { get; set; }

        [XmlElement("TestInformation")] public GuiTCCDVM TestInformation { get; set; }

        [XmlElement("SendResultTest")] public NopKetQuaVM SendResultTest { get; set; }
        #endregion

        #region OutboundObjects
        [XmlElement("Result")] public KetQuaXuLy Result { get; set; }

        [XmlElement("ResultConfirm")] public XacNhanDon ResultConfirm { get; set; }

        [XmlElement("ResultConfirmCancel")] public ThuHoiGDK ResultConfirmCancel { get; set; }

        [XmlElement("ResultTestInformation")] public TCCDGuiKetQuaKT ResultTestInformation { get; set; }

        [XmlElement("ResultResponse")] public XuLyKetQua ResultResponse { get; set; }

        [XmlElement("ResultCheck")] public GiayXNCL ResultCheck { get; set; }

        [XmlElement("AniFeedResultCertificateCancel")] public ThuHoiGiayXNCL AniFeedResultCertificateCancel { get; set; }

        [XmlElement("ResultReception2d")] public DaTiepNhanHoSo2d ResultReception2d { get; set; }

        [XmlElement("InfoGroupGood")] public DMPhanNhomHangHoa InfoGroupGood { get; set; }

        [XmlElement("InfoGoodType")] public DMLoaiHangHoa InfoGoodType { get; set; }

        [XmlElement("InfoGroupType")] public DMPhanLoaiHangHoa InfoGroupType { get; set; }
        #endregion

        
    }
}
