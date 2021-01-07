using DATA0200025;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace APP0200025.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Check_Language(string iID_MaNgonNgu)
        {
            object vR = null;
            String sCode = "";
            StringBuilder strBuild = new StringBuilder();
            DataTable dt = CLanguage.Get_Table_NgonNgu(Convert.ToInt32(iID_MaNgonNgu));
            if (dt.Rows.Count > 0)
            {
                Session["LANGUAGECODE"] = Convert.ToString(dt.Rows[0]["sCode"]).Trim();
                Session["LANGUAGEID"] = Convert.ToString(dt.Rows[0]["iID_MaNgonNgu"]).Trim();

                sCode = Convert.ToString(dt.Rows[0]["sCode"]).Trim();
                strBuild.Append(dt.Rows[0]["sTen"]);
            }
            dt.Dispose();
            vR = new
            {
                _sCode = sCode,
                _sLanguage = strBuild.ToString()
            };
            return base.Json(vR, JsonRequestBehavior.AllowGet);
        }
    }
}