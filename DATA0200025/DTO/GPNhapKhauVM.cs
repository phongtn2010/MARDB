using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.DTO
{
    public class GPNhapKhauVM
    {
        [XmlIgnore]
        public int id { get; set; }

        [XmlElement("CertificateNo")]
        public string fiCertificateNo { get; set; }
        [XmlElement("DecisionBasis")]
        public string fiDecisionBasis { set; get; }
        [XmlElement("Active")]
        [XmlIgnore]
        public bool fiActive { set; get; }
        [XmlElement("NameOfRegistration")]
        public string fiNameOfRegistration { get; set; }
        [XmlElement("DateOfRegistration"), NotMapped]
        public string fiDateOfRegistrationString
        {
            get => fiDateOfRegistration.ToString("yyyy-MM-dd HH:mm:ss");
            set => fiDateOfRegistration = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        public DateTime fiDateOfRegistration { get; set; }
        [XmlElement("RegistrationNo")]

        public string fiRegistrationNo { get; set; }
        [XmlElement("TotalCultivar")]

        public string fiTotalCultivar { get; set; }
        [XmlElement("Purpose")]

        public string fiPurpose { get; set; }
        [XmlElement("NoteLicense")]

        public string fiNoteLicense { get; set; }
        [XmlElement("EntryPointName")]

        public string fiEntryPointName { get; set; }

        [XmlElement("CreaterName")]
        public string fiCreatorName { get; set; }

        [XmlElement("SignConfirmDate"), NotMapped]
        public string fiSignConfirmDateString
        {
            get => fiSignConfirmDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => fiSignConfirmDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        public DateTime fiSignConfirmDate { get; set; }

        [XmlElement("SignTitle")]
        public string fiSignTitle { get; set; }

        [XmlElement("SignConfirmName")]
        public string fiSignConfirmName { get; set; }

        [XmlElement("EffectiveDate"), NotMapped]
        public string fiEffectiveDateString
        {
            get => fiEffectiveDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => fiEffectiveDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        public DateTime fiEffectiveDate { get; set; }
        [XmlElement("SignConfirmAddress")]

        public string fiSignConfirmAddress { get; set; }
        [XmlElement("NSWFileCode")]

        public string fiNSWFileCode { set; get; }
        [XmlIgnore]
        public string fiReasonForModification { get; set; }
        // [XmlElement("DateOfModification"), NotMapped]
        // public string fiDateOfModificationString
        // {
        //     get => fiDateOfModification.ToString("yyyy-MM-dd HH:mm:ss");
        //     set => fiDateOfModification = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        // }
        [XmlIgnore]
        public DateTime? fiDateOfModification { get; set; }

        [XmlIgnore]
        public bool isDraft { get; set; }

        //[XmlArray("TypeOfCultivarList")]
        //[XmlArrayItem("TypeOfCultivar")]
        //public virtual List<GPGiongCayVM> listGiongCay { set; get; }
    }
}
