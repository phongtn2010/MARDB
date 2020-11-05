using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATA0200026.DTO
{
    [XmlRoot("Application")]
    public class HoSoVM
    {
        [XmlIgnore]
        public DateTime createdAt { get; set; } = DateTime.Now;

        [XmlIgnore]
        public DateTime updatedAt { get; set; } = DateTime.Now;

        [XmlIgnore]
        public long id { get; set; }

        [XmlIgnore]
        public bool fiActive { set; get; }

        [XmlIgnore]
        public int fiHSStatus { set; get; }

        [XmlIgnore] public int fiHuyStatus { get; set; } = 0;

        [XmlIgnore] public int fiSuaStatus { get; set; } = 0;
        [XmlIgnore] public int fiSuaGPStatus { get; set; } = 0;


        [XmlElement("NSWFileCode")]
        public string NSWFileCode { set; get; }

        [XmlElement("Organization")]
        public string Organization { set; get; }

        public DateTime SignDate { set; get; }
        [XmlElement("SignDate"), NotMapped]
        public string SignDateString
        {
            get => SignDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => SignDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }

        [XmlElement("SignPlace")]
        public string SignPlace { set; get; }
        [XmlElement("Address")]
        public string Address { set; get; }
        [XmlElement("Phone")]
        public string Phone { set; get; }
        [XmlElement("Fax")]
        public string Fax { set; get; }

        [XmlElement("DepartmentCode")]
        public string DepartmentCode { set; get; }
        [XmlElement("DepartmentName")]
        public string DepartmentName { set; get; }

        [XmlArray("GoodsList")]
        [XmlArrayItem("Goods")]
        public virtual List<HangHoaVM> ListHangHoa { set; get; }

        [XmlElement("SignPlaceCode")]
        public string SignPlaceCode { get; set; }

        [XmlElement("SignPlaceName")]
        public string SignPlaceName { get; set; }

        [XmlElement("SignName")]
        public string SignName { set; get; }

        [XmlElement("SignPosition")]
        public string SignPosition { get; set; }
    }
}
