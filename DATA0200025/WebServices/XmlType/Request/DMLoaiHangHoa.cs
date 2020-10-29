using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.WebServices.XmlType.Request
{
    public class DMLoaiHangHoa
    {
        [XmlArray("GoodTypeList")]
        [XmlArrayItem("GoodType")]
        public virtual List<GoodType> ListGoodTypes { set; get; }

        [XmlIgnore] public DateTime UpdateDate { get; set; }
        [XmlElement("UpdateDate")]
        public string UpdateDateString
        {
            get => UpdateDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => UpdateDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }

        [XmlElement] public string UpdateName { get; set; }
    }

    public class GoodType
    {
        public string GoodTypeId { get; set; }
        public string GoodTypeName { get; set; }
        public int GroupGoodId { get; set; }
        public string GroupGoodName { get; set; }
        public int Action { get; set; }
    }
}
