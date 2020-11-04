using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DATA0200025.WebServices.XmlType.Request
{
    public class GiayXNCL
    {
        [XmlElement] public string NSWFileCode { get; set; }

        [XmlElement] public string DepartmentCode { get; set; }

        [XmlElement] public string DepartmentName { get; set; }

        [XmlElement] public string CerNumber { get; set; }

        [XmlElement] public string SignCerPlace { get; set; }

        public string SignCerDateString
        {
            get => SignCerDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => SignCerDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        [XmlElement] public DateTime SignCerDate { get; set; }

        [XmlArray("GoodsList")]
        [XmlArrayItem("Goods")]
        public virtual List<HangHoaGXNCL> ListHangHoa { set; get; }

        [XmlElement] public string PortOfDestinationName { get; set; }

        public string ImportingFromDateString
        {
            get => ImportingFromDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => ImportingFromDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        [XmlElement] public DateTime ImportingFromDate { get; set; }

        public string ImportingToDateString
        {
            get => ImportingToDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => ImportingToDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        [XmlElement] public DateTime ImportingToDate { get; set; }

        [XmlArray("ContractList")]
        [XmlArrayItem("Contract")]
        public virtual List<ContractList> ListContract { set; get; }

        [XmlArray("InvoiceList")]
        [XmlArrayItem("Invoice")]
        public virtual List<InvoiceList> ListInvoice { set; get; }

        [XmlElement] public string AniFeedConfirmNo { get; set; }

        public string SignConfirmDateString
        {
            get => SignConfirmDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => SignConfirmDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        [XmlElement] public DateTime SignConfirmDate { get; set; }

        [XmlElement] public string Buyer { get; set; }

        [XmlElement] public string BuyerAddress { get; set; }

        [XmlElement] public string StandardBase { get; set; }

        [XmlElement] public string ResultTechnicalRegulations { get; set; }

        [XmlElement] public string ImportCerNo { get; set; }

        [XmlElement] public string AssignCode { get; set; }

        [XmlElement] public string AssignName { get; set; }

        public string ImportCerDateString
        {
            get => ImportCerDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => ImportCerDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        [XmlElement] public DateTime ImportCerDate { get; set; }

        [XmlElement] public string SignCerName { get; set; }
    }

    public class HangHoaGXNCL
    {
        public long GoodsId { set; get; }
        public string NameOfGoods { get; set; }
        public string RegistrationNumber { get; set; }
        public string Manufacture { get; set; }
        public string ManufactureStateCode { get; set; }
        public string ManufactureState { get; set; }

        [XmlArray("QuantityVolumeList")]
        [XmlArrayItem("QuantityVolume")]
        public virtual List<QuantityVolumeList> ListQuantityVolume { set; get; }

        public string Note { get; set; }
    }

    public class QuantityVolumeList
    {
        public double Volume { get; set; }
        public string VolumeUnitID { get; set; }
        public string VolumeUnit { get; set; }
        public double Quantity { get; set; }
        public string QuantityUnitID { get; set; }
        public string QuantityUnitName { get; set; }
    }

    public class ContractList
    {
        public string ContractNo { get; set; }
        public string ContractDateString
        {
            get => ContractDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => ContractDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        [XmlElement] public DateTime ContractDate { get; set; }
    }

    public class InvoiceList
    {
        public string InvoiceNo { get; set; }
        public string InvoiceDateString
        {
            get => InvoiceDate.ToString("yyyy-MM-dd HH:mm:ss");
            set => InvoiceDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        [XmlElement] public DateTime InvoiceDate { get; set; }
    }
}
