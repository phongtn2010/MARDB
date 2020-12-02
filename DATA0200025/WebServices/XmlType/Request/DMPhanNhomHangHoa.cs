using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.WebServices.XmlType.Request
{
    public class DMPhanNhomHangHoa
    {
        [XmlArray("GroupGoodList")]
        [XmlArrayItem("GroupGood")]
        public virtual List<GroupGood> ListGroupGoods { set; get; }

        public string UpdateDate
        {
            get => UpdateDateString.ToString("yyyy-MM-dd HH:mm:ss");
            set => UpdateDateString = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        [XmlIgnore] public DateTime UpdateDateString { get; set; }

        [XmlElement] public string UpdateName { get; set; } 
    }

    public class GroupGood
    {
        public string GroupGoodId { get; set; }
        public string GroupGoodName { get; set; }
        public int GoodType { get; set; }
        public int Action { get; set; }
    }
}
