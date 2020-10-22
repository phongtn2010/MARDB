using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.DTO.Request
{
    public class ResponseCancel
    {
        public string NSWFileCode { get; set; }
        public string Reason { get; set; }
        [XmlIgnore] public DateTime SignConfirmDate { get; set; }
        [XmlElement("SignConfirmDate"), NotMapped]

        public string SignConfirmDateString
        {
            get => SignConfirmDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => SignConfirmDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        public string CreaterName { get; set; }
    }
}
