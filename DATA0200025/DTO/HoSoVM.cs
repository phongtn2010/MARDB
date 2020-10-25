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

        [XmlElement("TypeAniFeed")]
        public int sLoaiHinhThucKiemTra { get; set; }


        [XmlElement("NSWFileCode")]
        public string fiNSWFileCode { set; get; }

        [XmlElement("NSWFileCodeOld")]
        public string fiNSWFileCodeOld { set; get; }

        [XmlElement("AniFeedConfirmOldNo")]
        public string AniFeedConfirmOldNo { set; get; }
        [XmlElement("AniFeedConfirmOldId")]
        public string AniFeedConfirmOldId { set; get; }
        [XmlElement("AniFeedConfirmOldName")]
        public string AniFeedConfirmOldName { set; get; }
        [XmlElement("AniFeedConfirmOldFileLink")]
        public string AniFeedConfirmOldFileLink { set; get; }

        [XmlElement("DepartmentCode")]
        public string DepartmentCode { set; get; }
        [XmlElement("DepartmentName")]
        public string DepartmentName { set; get; }

        [XmlElement("Seller")]
        public string sBan_Name { set; get; }
        [XmlElement("SellerState")]
        public string iID_MaQuocGia_Ban { set; get; }
        [XmlElement("NameSellerState")]
        public string sBan_QuocGia { set; get; }
        [XmlElement("SellerAddress")]
        public string sBan_DiaChi { set; get; }
        [XmlElement("SellerPhone")]
        public string sBan_Tel { set; get; }
        [XmlElement("SellerFax")]
        public string sBan_Fax { set; get; }
        [XmlElement("PortOfDepartureName")]
        public string sBan_NoiXuat { set; get; }

        [XmlElement("Buyer")]
        public string sMua_Name { set; get; }
        [XmlElement("BuyerAddress")]
        public string sMua_DiaChi { set; get; }
        [XmlElement("BuyerPhone")]
        public string sMua_Tel { set; get; }
        [XmlElement("BuyerFax")]
        public string sMua_Fax { set; get; }
        [XmlElement("PortOfDestinationName")]
        public string sMua_NoiNhan { set; get; }
        [XmlElement("ImportingFromDate")]
        public DateTime sMua_FromDate { set; get; }
        [XmlElement("ImportingToDate")]
        public DateTime sMua_ToDate { set; get; }

        [XmlElement("LocationOfStorage")]
        public string fiLocationOfStorage { set; get; }
        [XmlElement("DateOfSamplingFrom")]
        public DateTime fiDateOfSamplingFrom { set; get; }
        [XmlElement("DateOfSamplingTo")]
        public DateTime fiDateOfSamplingTo { set; get; }
        [XmlElement("LocationOfSampling")]
        public string fiLocationOfSampling { set; get; }
        [XmlElement("ContactPerson")]
        public string fiContactPerson { set; get; }
        [XmlElement("ContactAddress")]
        public string fiContactAddress { set; get; }
        [XmlElement("ContactTel")]
        public string fiContactTel { set; get; }
        [XmlElement("ContactEmail")]
        public string fiContactEmail { set; get; }

        [XmlElement("CreateDate"), NotMapped]
        public DateTime fiCreateDate { set; get; }
        public string fiCreateDateString
        {
            get => fiCreateDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => fiCreateDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        
        [XmlElement("SignPlace")]
        public string fiSignPlace { get; set; }
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



        [XmlArray("GoodsList")]
        [XmlArrayItem("Goods")]
        public virtual List<HangHoaVM> ListHangHoa { set; get; }

        [XmlArray("ContractList")]
        [XmlArrayItem("Contract")]
        public virtual List<HopDongVM> ListHopDong { set; get; }

        [XmlArray("InvoiceList")]
        [XmlArrayItem("Invoice")]
        public virtual List<HoaDonVM> ListHoaDon { set; get; }

        [XmlArray("PackingList")]
        [XmlArrayItem("Packing")]
        public virtual List<PhieuDongGoiVM> ListPhieuDongGoi { set; get; }

        [XmlArray("AttachmentList")]
        [XmlArrayItem("Attachment")]
        public virtual List<AttachmentVM> ListAttachment { set; get; }
    }
}
