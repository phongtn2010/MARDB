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

        public string SignCerDate
        {
            get => SignCerDateString.ToString("yyyy-MM-dd HH:mm:ss");
            set => SignCerDateString = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        [XmlElement] public DateTime SignCerDateString { get; set; }

        [XmlArray("GoodsList")]
        [XmlArrayItem("Goods")]
        public virtual List<HangHoaGXNCL> ListHangHoa { set; get; }

        [XmlElement] public string PortOfDestinationName { get; set; }

        public string ImportingFromDate
        {
            get => ImportingFromDateString.ToString("yyyy-MM-dd HH:mm:ss");
            set => ImportingFromDateString = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        [XmlElement] public DateTime ImportingFromDateString { get; set; }

        public string ImportingToDate
        {
            get => ImportingToDateString.ToString("yyyy-MM-dd HH:mm:ss");
            set => ImportingToDateString = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        [XmlElement] public DateTime ImportingToDateString { get; set; }

        [XmlArray("ContractList")]
        [XmlArrayItem("Contract")]
        public virtual List<ContractList> ListContract { set; get; }

        [XmlArray("InvoiceList")]
        [XmlArrayItem("Invoice")]
        public virtual List<InvoiceList> ListInvoice { set; get; }

        [XmlElement] public string AniFeedConfirmNo { get; set; }

        public string SignConfirmDate
        {
            get => SignConfirmDateString.ToString("yyyy-MM-dd HH:mm:ss");
            set => SignConfirmDateString = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        [XmlElement] public DateTime SignConfirmDateString { get; set; }

        [XmlElement] public string Buyer { get; set; }

        [XmlElement] public string BuyerAddress { get; set; }

        [XmlElement] public string StandardBase { get; set; }

        [XmlElement] public string ResultTechnicalRegulations { get; set; }

        [XmlElement] public string ImportCerNo { get; set; }

        [XmlElement] public string AssignCode { get; set; }

        [XmlElement] public string AssignName { get; set; }

        public string ImportCerDate
        {
            get => ImportCerDateString.ToString("yyyy-MM-dd HH:mm:ss");
            set => ImportCerDateString = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        [XmlElement] public DateTime ImportCerDateString { get; set; }

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
        public string ContractDate
        {
            get => ContractDateString.ToString("yyyy-MM-dd HH:mm:ss");
            set => ContractDateString = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        [XmlElement] public DateTime ContractDateString { get; set; }
    }

    public class InvoiceList
    {
        public string InvoiceNo { get; set; }
        public string InvoiceDate
        {
            get => InvoiceDateString.ToString("yyyy-MM-dd HH:mm:ss");
            set => InvoiceDateString = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);
        }
        [XmlElement] public DateTime InvoiceDateString { get; set; }
    }
}
