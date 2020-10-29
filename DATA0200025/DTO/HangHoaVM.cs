using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.DTO
{
    public class HangHoaVM
    {
        [XmlElement("GoodsId")]
        public long GoodsId { set; get; }

        [XmlElement("NameOfGoods")]
        public string fiNameOfGoods { set; get; }

        [XmlElement("NoticeOfExemptionFromInspectionNo")]
        public string fiScienceName { set; get; }

        
        public DateTime fiNoticeDate { set; get; }
        [XmlElement("NoticeDate"), NotMapped]
        public string fiNoticeDateString
        {
            get => fiNoticeDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => fiNoticeDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }

        [XmlElement("GroupFoodOfGoods")]
        public int fiGroupFoodOfGoods { set; get; }
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
        public string fiGoodsValueUnitName { set; get; }

        [XmlArray("QuantityVolumeList")]
        [XmlArrayItem("QuantityVolume")]
        public virtual List<SoLuongVM> ListSoLuong { set; get; }

        [XmlArray("QualityCriteriaList")]
        [XmlArrayItem("QualityCriteria")]
        public virtual List<ChatLuongVM> ListChatLuong { set; get; }

        [XmlArray("SafetyCriteriaList")]
        [XmlArrayItem("SafetyCriteria")]
        public virtual List<AnToanVM> ListAnToan { set; get; }



        [XmlIgnore]
        public long idHoSo { get; set; }
    }
}
