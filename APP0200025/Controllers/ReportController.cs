using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DATA0200025.Models;
using DATA0200026;
using DomainModel;

namespace APP0200025.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult TinhHinhXuLyHoSo(ReportSearchModels model)
        {
            if(model==null)
            {
                model = new ReportSearchModels { };
            }    
            return View(model);
        }
        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult sTinhHinhXuLyHoSo(string ParentID)
        {
            string _sMaSoThue = CString.SafeString(Request.Form[ParentID + "_DoanhNghiep"]);
            string _TuNgay = CString.SafeString(Request.Form[ParentID + "_viTuNgay"]);
            string _DenNgay= CString.SafeString(Request.Form[ParentID + "_viDenNgay"]);
            ReportSearchModels model = new ReportSearchModels
            {
                TuNgay= _TuNgay,
                DenNgay=_DenNgay,
                sMaSoThue=_sMaSoThue
            };
            return RedirectToAction("TinhHinhXuLyHoSo", model);
        }
     }
}