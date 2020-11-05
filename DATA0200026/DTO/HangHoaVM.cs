using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200026.DTO
{
    public class HangHoaVM
    {
        [XmlElement("GoodsId")]
        public long GoodsId { set; get; }

        [XmlElement("NSWRegisterFileCode")]
        public string NSWRegisterFileCode { set; get; }

        [XmlElement("GoodsCode")]
        public string GoodsCode { set; get; }

        [XmlElement("NameOfGoods")]
        public string NameOfGoods { set; get; }

        [XmlElement("GroupFoodOfGoods")]
        public int GroupFoodOfGoods { set; get; }

        [XmlElement("GoodTypeId")]
        public string GoodTypeId { set; get; }

        [XmlElement("GoodTypeName")]
        public string GoodTypeName { set; get; }

        [XmlElement("RegistrationNumber")]
        public string RegistrationNumber { set; get; }

        [XmlElement("Manufacture")]
        public string Manufacture { set; get; }

        [XmlElement("ManufactureStateCode")]
        public string ManufactureStateCode { set; get; }

        [XmlElement("ManufactureState")]
        public string ManufactureState { set; get; }

        [XmlElement("StandardBase")]
        public string StandardBase { set; get; }

        [XmlElement("Material")]
        public string Material { set; get; }

        [XmlElement("FormColorOfProducts")]
        public string FormColorOfProducts { set; get; }

        [XmlArray("QualityCriteriaList")]
        [XmlArrayItem("QualityCriteria")]
        public virtual List<ChatLuongVM> ListChatLuong { set; get; }

        [XmlArray("SafetyCriteriaList")]
        [XmlArrayItem("SafetyCriteria")]
        public virtual List<AnToanVM> ListAnToan { set; get; }
    }
}
