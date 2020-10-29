using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.DTO
{
    [XmlRoot("TestInformation")]
    public class GuiTCCDVM
    {
        [XmlElement("NSWFileCode")]
        public string fiNSWFileCode { set; get; }

        [XmlElement("AssignCode")]
        public string fiAssignCode { set; get; }

        [XmlElement("AssignName")]
        public string fiAssignName { set; get; }
                
        [XmlArray("GoodsList")]
        [XmlArrayItem("Goods")]
        public virtual List<HangHoaTCCDVM> ListHangHoa { set; get; }
    }

    public class HangHoaTCCDVM
    {
        [XmlElement("GoodsId")]
        public long fiGoodsId { set; get; }

        [XmlElement("NameOfGoods")]
        public string fiNameOfGoods { set; get; }

        [XmlElement("GroupFoodOfGoods")]
        public int fiGroupFoodOfGoods { set; get; }

        [XmlElement("GoodTypeId")]
        public int fiGoodTypeId { set; get; }

        [XmlElement("GoodTypeName")]
        public string fiGoodTypeName { set; get; }

        [XmlArray("AnanyticalRequiredList")]
        [XmlArrayItem("Ananytical")]
        public virtual List<AnanyticalVM> ListAnanyticals { set; get; }
    }

    public class AnanyticalVM
    {
        [XmlElement("AnanyticalName")]
        public string fiAnanyticalName { set; get; }

        [XmlElement("FormOfPublication")]
        public int fiFormOfPublication { set; get; }

        [XmlElement("Required")]
        public string fiRequired { set; get; }

        [XmlElement("RequireUnitID")]
        public string fiRequireUnitID { set; get; }

        [XmlElement("RequireUnitName")]
        public string fiRequireUnitName { set; get; }

        [XmlElement("Note")]
        public string fiNote { set; get; }
    }
}
