﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.WebServices.XmlType.Request
{
    public class XuLyKetQua
    {
        [XmlElement] public string NSWFileCode { get; set; }

        [XmlElement] public long GoodsId { get; set; }

        [XmlElement] public string NameOfGoods { get; set; }

        [XmlElement] public string Reason { get; set; }

        [XmlElement] public string AttachmentId { get; set; }

        [XmlElement] public string FileName { get; set; }

        [XmlElement] public string FileLink { get; set; }

        [XmlElement] public DateTime ResponseDate { get; set; }
        [XmlElement("ResponseDate")]
        public string ResponseDateString
        {
            get => ResponseDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => ResponseDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }

        [XmlElement] public string NameOfStaff { get; set; }
    }
}
