using DATA0200025.SearchModels;
using DomainModel;
using System.Web.Mvc;

namespace APP0200025.Controllers
{
    public class ChoTiepNhanHoSoController : Controller
    {
        // GET: ChoTiepNhanHoSo
        public ActionResult Index()
        {
            var models = new sHoSoModels
            {
                Page = 1,
                PageSize = Globals.PageSize
            };
            return View(models);
        }

        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Search(string ParentID)
        {
            string _sMaHoSo = CString.SafeString(Request.Form[ParentID + "_sMaHoSo"]);
            string _sTenDoanhNgiep = CString.SafeString(Request.Form[ParentID + "_sTenDoanhNgiep"]);
            string _sTenTACN = CString.SafeString(Request.Form[ParentID + "_sTenTACN"]);
            string _FromDate = CString.SafeString(Request.Form[ParentID + "_viFromDate"]);
            string _ToDate = CString.SafeString(Request.Form[ParentID + "_viToDate"]);
            sHoSoModels models = new sHoSoModels
            {
                sMaHoSo = _sMaHoSo,
                sTenDoanhNghiep = _sTenDoanhNgiep,
                sTenTACN = _sTenTACN,
                FromDate = _FromDate,
                ToDate = _ToDate
            };
            return RedirectToAction("Index", models);
        }
    }
}