using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Abstract;
using DATA0200026;
using DATA0200026.WebServices;
using DomainModel;
using DATA0200026.WebServices.XmlType.Request;
using System.Collections.Specialized;

namespace APP0200025.Controllers
{
    public class Test26Controller : Controller
    {
        private SendService _sendService = new SendService();

        // GET: Test26
        public ActionResult Index()
        {
            //XML13(01)
            PhanHoiDonXM resultConfirm = new PhanHoiDonXM();
            resultConfirm.NSWFileCode = "MK26200010210";
            resultConfirm.NameOfStaff = "admin";
            resultConfirm.ResponseDateString = DateTime.Now;

            string error = _sendService.PhanHoiDonXM("MK26200010210", resultConfirm);

            return View();
        }
    }
}