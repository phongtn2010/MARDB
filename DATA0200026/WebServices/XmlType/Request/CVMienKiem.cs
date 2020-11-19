using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200026.WebServices.XmlType.Request
{
    public class CVMienKiem
    {
        [XmlElement] public string NSWFileCode { get; set; }

        [XmlElement] public string ConfirmApplicationNo { get; set; }

        [XmlElement] public string Organization { get; set; }

        public string SignConfirmDateString
        {
            get => SignConfirmDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => SignConfirmDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        [XmlIgnore] public DateTime SignConfirmDate { get; set; }

        [XmlElement] public string SignConfirmPlace { get; set; }

        public string SignDateString
        {
            get => SignDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => SignDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        [XmlElement] public DateTime SignDate { get; set; }

        [XmlElement] public string DepartmentCode { get; set; }

        [XmlElement] public string DepartmentName { get; set; }

        [XmlArray("GoodsList")]
        [XmlArrayItem("Goods")]
        public virtual List<HangHoaXND> ListHangHoa { set; get; }

        public string PeriodFromString
        {
            get => PeriodFrom.ToString("yyyy-MM-dd HH:mm:ss");
            set => PeriodFrom = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        [XmlElement] public DateTime PeriodFrom { get; set; }

        public string PeriodToString
        {
            get => PeriodTo.ToString("yyyy-MM-dd HH:mm:ss");
            set => PeriodTo = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        [XmlElement] public DateTime PeriodTo { get; set; }

        [XmlElement] public string SignName { get; set; }
    }

    public class HangHoaXND
    {
        public long GoodsId { set; get; }
        public string NSWRegisterFileCode { get; set; }
        public long GoodsCode { set; get; }
        public string NameOfGoods { get; set; }
        public int GroupFoodOfGoods { set; get; }
        public string GoodTypeId { get; set; }
        public string GoodTypeName { get; set; }
        public string RegistrationNumber { get; set; }
        public string Manufacture { get; set; }
        public string ManufactureStateCode { get; set; }
        public string ManufactureState { get; set; }
        public string StandardBase { get; set; }
        public string Material { get; set; }
        public string FormColorOfProducts { get; set; }
        [XmlArray("QualityCriteriaList")]
        [XmlArrayItem("QualityCriteria")]
        public virtual List<QualityCriteriaList> ListQualityCriteria { set; get; }
        [XmlArray("SafetyCriteriaList")]
        [XmlArrayItem("SafetyCriteria")]
        public virtual List<SafetyCriteriaList> ListSafetyCriteria { set; get; }
    }

    public class QualityCriteriaList
    {
        public string QualityCriteriaName { get; set; }
        public int QualityFormOfPublication { get; set; }
        public string QualityRequire { get; set; }
        public string QualityRequireUnitID { get; set; }
        public string QualityRequireUnitName { get; set; }
    }

    public class SafetyCriteriaList
    {
        public string SafetyCriteriaName { get; set; }
        public int SafetyFormOfPublication { get; set; }
        public string SafetyRequire { get; set; }
        public string SafetyRequireUnitID { get; set; }
        public string SafetyRequireUnitName { get; set; }
    }
}
