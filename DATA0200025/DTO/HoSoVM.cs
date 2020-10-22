using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATA0200025.DTO
{
    [XmlRoot("RegistrationProfile")]
    public class HoSoVM
    {
        [XmlIgnore]
        public DateTime createdAt { get; set; } = DateTime.Now;

        [XmlIgnore]
        public DateTime updatedAt { get; set; } = DateTime.Now;
        [XmlIgnore]
        public int id { get; set; }

        [XmlIgnore]
        public bool fiActive { set; get; }

        //public int fiIdHSParent { set; get; }

        [XmlIgnore]
        public int fiHSStatus { set; get; }

        [XmlIgnore] public int fiHuyStatus { get; set; } = 0;

        [XmlIgnore] public int fiSuaStatus { get; set; } = 0;
        [XmlIgnore] public int fiSuaGPStatus { get; set; } = 0;

        [XmlElement("TaxCode")]
        public string fiTaxCode { get; set; }
        [XmlElement("NSWFileCode")]
        public string fiNSWFileCode { set; get; }
        [XmlElement("LicenseNo")]
        public string fiLicenseNo { set; get; }

        public DateTime fiRegistrationDate { set; get; }
        [XmlElement("RegistrationDate"), NotMapped]

        public string fiRegistrationDateString
        {
            get => fiRegistrationDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => fiRegistrationDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        [XmlElement("RegistrationNo")]
        public string fiRegistrationNo { get; set; }

        [XmlElement("NameOfRegistration")]
        public string fiNameOfRegistration { get; set; }

        [XmlElement("AddressOfRegistration")]
        public string fiAddressOfRegistration { get; set; }
        public DateTime fiQuarantineTimeFrom { get; set; }

        [XmlElement("QuarantineTimeFrom"), NotMapped]
        public string fiQuarantineTimeFromString
        {
            get => fiQuarantineTimeFrom.ToString("yyyy-MM-dd HH:mm:ss");
            set => fiQuarantineTimeFrom = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }

        public DateTime fiQuarantineTimeTo { get; set; }

        [XmlElement("QuarantineTimeTo"), NotMapped]
        public string fiQuarantineTimeToString
        {
            get => fiQuarantineTimeTo.ToString("yyyy-MM-dd HH:mm:ss");
            set => fiQuarantineTimeTo = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        [XmlElement("Phone")]
        public string fiPhone { get; set; }
        [XmlElement("Fax")]
        public string fiFax { get; set; }
        [XmlElement("Email")]
        public string fiEmail { get; set; }
        [XmlElement("RegistrationConfirmNo")]
        public string fiRegistrationConfirmNo { get; set; }
        [XmlElement("SignAddress")]
        public string fiSignAddress { get; set; }
        public DateTime fiSignDate { get; set; }

        [XmlElement("SignDate"), NotMapped]
        public string fiSignDateString
        {
            get => fiSignDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => fiSignDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }

        [XmlElement("SignName")]
        public string fiSignName { get; set; }
        [XmlElement("SignPosition")]
        public string fiSignPosition { get; set; }
        [XmlElement("SowingScale")]
        public string fiSowingScale { get; set; }
        [XmlElement("Purpose")]
        public string fiPurpose { get; set; }
        [XmlElement("TotalCultivar")]
        public string fiTotalCultivar { get; set; }
        [XmlElement("EntryPointName")]
        public string fiEntryPointName { get; set; }
        [XmlElement("DocumentsConcerned")]
        public string fiDocumentsConcerned { get; set; }
        [XmlElement("ListOfImports")]
        public string fiListOfImports { get; set; }

        [XmlIgnore] public int idDepartment { set; get; } = 1;

        [XmlElement("ReasonForModification")]
        public string fiReasonForModification { get; set; }
        public DateTime fiDateOfModification { get; set; }


        [XmlElement("DateOfModification"), NotMapped]
        public string fiDateOfModificationString
        {
            get => fiDateOfModification.ToString("yyyy-MM-dd HH:mm:ss");
            set => fiDateOfModification = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        [XmlElement("PurposeOther")]
        public string fiPurposeOther { get; set; }

        //[XmlArray("TechnicalDeclarationList")]
        //[XmlArrayItem("TechnicalDeclaration")]
        //public virtual List<ToKhaiKyThuatVM> listToKhaiKyThuat { get; set; }

        //[XmlArray("TypeOfCultivarList")]
        //[XmlArrayItem("TypeOfCultivar")]
        //public virtual List<GiongCayVM> listGiongCay { set; get; }

        //[XmlArray("AttachmentList")]
        //[XmlArrayItem("Attachment")]
        //public virtual List<AttachmentVM> ListAttachment { set; get; }
    }
}
