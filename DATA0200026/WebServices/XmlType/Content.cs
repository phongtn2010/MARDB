using DATA0200026.DTO;
using DATA0200026.DTO.Extensions;
using DATA0200026.DTO.Request;
using DATA0200026.WebServices.XmlType.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200026.WebServices.XmlType
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

        [XmlElement("Application")] public HoSoVM Application { get; set; }

        #endregion

        #region OutboundObjects
        [XmlElement("ApplicationResponse")] public PhanHoiDonXM ApplicationResponse { get; set; }

        [XmlElement("ApplicationReplies")] public CVMienKiem ApplicationReplies { get; set; }

        [XmlElement("ApplicationCancel")] public ThongBaoThuHoiCVMienKiem ApplicationCancel { get; set; }
        #endregion

        
    }
}
