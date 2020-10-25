using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.DTO
{
    public class HangHoaVM
    {
        [XmlIgnore]
        public int GoodsId { set; get; }

        //public bool fiActive { set; get; }
        //public string fiNSWFileCode { set; get; }
        [XmlElement("NameOfGoods")]
        public string fiName { set; get; }

        [XmlElement("NoticeOfExemptionFromInspectionNo")]
        public string fiScienceName { set; get; }

        [XmlElement("NoticeDate")]
        public string fiNoticeDate { set; get; }

        [XmlElement("GroupFoodOfGoods")]
        public int fiQuantityUnitCode { set; get; }
        [XmlElement("GroupGoodId")]
        public string fiGroupGoodId { set; get; }
        [XmlElement("GroupGoodName")]
        public string fiGroupGoodName { set; get; }
        [XmlElement("GoodTypeId")]
        public string fiGoodTypeId { set; get; }
        [XmlElement("GoodTypeName")]
        public string fiGoodTypeName { set; get; }
        [XmlElement("GroupTypeId")]
        public string fiGroupTypeId { set; get; }
        [XmlElement("GroupTypeName")]
        public string fiGroupTypeName { set; get; }

        [XmlElement("Nature")]
        public string fiNature { set; get; }

        [XmlElement("RegistrationNumber")]
        public string fiRegistrationNumber { set; get; }

        [XmlElement("Manufacture")]
        public string fiManufacture { set; get; }

        [XmlElement("ManufactureStateCode")]
        public string fiManufactureStateCode { set; get; }
        [XmlElement("ManufactureState")]
        public string fiManufactureState { set; get; }

        [XmlElement("Material")]
        public string fiMaterial { set; get; }

        [XmlElement("FormColorOfProducts")]
        public string fiFormColorOfProducts { set; get; }

        [XmlElement("StandardBase")]
        public string fiStandardBase { set; get; }

        [XmlElement("TechnicalRegulations")]
        public string fiTechnicalRegulations { set; get; }

        [XmlElement("GoodsValue")]
        public decimal fiGoodsValue { get; set; }

        [XmlElement("GoodsValueUSD")]
        public decimal fiGoodsValueUSD { get; set; }

        [XmlElement("GoodsValueUnitCode")]
        public string fiGoodsValueUnitCode { set; get; }

        [XmlElement("GoodsValueUnitName")]
        public int fiGoodsValueUnitName { set; get; }

        [XmlArray("QuantityVolumeList")]
        [XmlArrayItem("QualityCriteria")]
        public virtual List<ChatLuongVM> ListChatLuong { set; get; }

        [XmlArray("SafetyCriteriaList")]
        [XmlArrayItem("SafetyCriteria")]
        public virtual List<AnToanVM> ListAnToan { set; get; }

        [XmlArray("QuantityVolumeList")]
        [XmlArrayItem("QuantityVolume")]
        public virtual List<SoLuongVM> ListSoLuong { set; get; }

        [XmlIgnore]
        public int idHoSo { get; set; }
    }
}
