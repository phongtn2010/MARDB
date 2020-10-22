using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA0200025.Models
{
    public class Attachment
    {
        public int Id { set; get; }

        public string AttachmentTypeCode { get; set; }
        public string AttachmentTypeName { get; set; }
        public string LinkFile { get; set; }
        public int IdHoSo { get; set; }
    }
}
