using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APP0200025.Controllers
{
    public class LogController : Controller
    {
        // GET: Log
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateInput(true), Authorize]
        public ActionResult Search(string ParentID)
        {
            string _TieuDe = CString.SafeString(Request.Form[ParentID + "_TieuDe"]);
            string _FromDate = CString.SafeString(Request.Form[ParentID + "_FromDate"]);
            string _ToDate = CString.SafeString(Request.Form[ParentID + "_ToDate"]);
            return RedirectToAction("Index", "Log", new { _TieuDe = _TieuDe, _FromDate = _FromDate, _ToDate = _ToDate });
        }

        [Authorize]
        [ValidateInput(true)]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Delete(string iID_ActionLog_Action)
        {
            SqlCommand cmd;

            //Xoa danh muc san pham
            cmd = new SqlCommand("DELETE FROM CNN25_Log_NSW WHERE iID_ActionLog_Action=@iID_ActionLog_Action");
            cmd.Parameters.AddWithValue("@iID_ActionLog_Action", CString.SafeString(iID_ActionLog_Action));
            Connection.UpdateDatabase(cmd);
            cmd.Dispose();

            return RedirectToAction("Index");
        }

        public ActionResult List()
        {
            return View();
        }

        [HttpPost, ValidateInput(true), Authorize]
        public ActionResult cms_search(string ParentID)
        {
            string _TieuDe = CString.SafeString(Request.Form[ParentID + "_TieuDe"]);
            string _FromDate = CString.SafeString(Request.Form[ParentID + "_FromDate"]);
            string _ToDate = CString.SafeString(Request.Form[ParentID + "_ToDate"]);
            return RedirectToAction("List", "Log", new { _TieuDe = _TieuDe, _FromDate = _FromDate, _ToDate = _ToDate });
        }

        [Authorize]
        [ValidateInput(true)]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult cms_delete(string iID_ActionLog_Action)
        {
            SqlCommand cmd;

            //Xoa danh muc san pham
            cmd = new SqlCommand("DELETE FROM CNN26_Log_NSW WHERE iID_ActionLog_Action=@iID_ActionLog_Action");
            cmd.Parameters.AddWithValue("@iID_ActionLog_Action", CString.SafeString(iID_ActionLog_Action));
            Connection.UpdateDatabase(cmd);
            cmd.Dispose();

            return RedirectToAction("List");
        }
    }
}