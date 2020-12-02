using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.WebServices.XmlType.Request
{
    public class DMPhanLoaiHangHoa
    {
        [XmlArray("GroupTypeList")]
        [XmlArrayItem("GroupType")]
        public virtual List<GroupType> ListGroupTypes { set; get; }

        public string UpdateDate
        {
            get => UpdateDateString.ToString("yyyy-MM-dd HH:mm:ss");
            set => UpdateDateString = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        [XmlIgnore] public DateTime UpdateDateString { get; set; }

        [XmlElement] public string UpdateName { get; set; }
    }

    public class GroupType
    {
        public string GroupTypeId { get; set; }
        public string GroupTypeName { get; set; }
        public string Nature { get; set; }
        public int GoodTypeId { get; set; }
        public string GoodTypeName { get; set; }
        public int Action { get; set; }
    }
}
