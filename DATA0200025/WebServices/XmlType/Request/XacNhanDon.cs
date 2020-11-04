using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.WebServices.XmlType.Request
{
    public class XacNhanDon
    {
        [XmlElement] public string NSWFileCode { get; set; }

        [XmlElement] public string AniFeedConfirmNo { get; set; }

        [XmlElement] public string DepartmentCode { get; set; }

        [XmlElement] public string DepartmentName { get; set; }

        [XmlIgnore] public DateTime ImportingFromDate { get; set; }

        [XmlElement("ImportingFromDate")]
        public string ImportingFromDateString
        {
            get => ImportingFromDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => ImportingFromDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }

        [XmlIgnore] public DateTime ImportingToDate { get; set; }

        [XmlElement("ImportingToDate")]
        public string ImportingToDateString
        {
            get => ImportingToDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => ImportingToDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }

        [XmlElement] public string AssignID { get; set; }

        [XmlElement] public string AssignName { get; set; }

        [XmlElement] public string AssignNameOther { get; set; }

        [XmlIgnore] public DateTime SignConfirmDate { get; set; }

        [XmlElement("SignConfirmDate")]
        public string SignConfirmDateString
        {
            get => SignConfirmDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => SignConfirmDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }

        [XmlElement] public string SignConfirmPlace { get; set; }

        [XmlElement] public string SignConfirmName { get; set; }

        [XmlElement] public string NSWFileCodeOld { get; set; }

        [XmlElement] public string AniFeedConfirmOldNo { get; set; }

        [XmlArray("GoodsList")]
        [XmlArrayItem("Goods")]
        public virtual List<HangHoaXND> ListHangHoa { set; get; }

        [XmlElement] public string NoteGoods { get; set; }
    }

    public class HangHoaXND
    {
        public long GoodsId { set; get; }
        public string NameOfGoods { get; set; }
        public string GroupTypeId { get; set; }
        public string GroupTypeName { get; set; }
        public string Nature { get; set; }
        [XmlArray("AnanyticalRequiredList")]
        [XmlArrayItem("Ananytical")]
        public virtual List<AnanyticalRequiredList> ListAnanyticals { set; get; }
    }

    public class AnanyticalRequiredList
    {
        public string AnanyticalName { get; set; }
        public int FormOfPublication { get; set; }
        public string Required { get; set; }
        public string RequireUnitID { get; set; }
        public string RequireUnitName { get; set; }
        public string Note { get; set; }
    }
}
