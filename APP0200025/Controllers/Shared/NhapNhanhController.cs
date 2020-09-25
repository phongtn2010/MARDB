using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel;
using System.Web.Script.Serialization;

namespace APP0200025.Controllers
{
    public class NhapNhanhController : Controller
    {
        // GET: NhapNhanh
        [AcceptVerbs(HttpVerbs.Post)]
        public JavaScriptResult Index(string id, string OnSuccess, string OnLoad)
        {
            // Điền dữ liệu vào bảng
            ViewData["ControlID"] = id;
            ViewData["OnSuccess"] = OnSuccess;

            switch (id.ToUpper())
            {
                case "COTMAU":
                    ViewData["Partial_View"] = "~/Views/CotMau/Dialog.cshtml";
                    break;
                case "HANGMAU":
                    ViewData["Partial_View"] = "~/Views/HangMau/Dialog.cshtml";
                    break;
                case "BANG_CHITIEU":
                    ViewData["Partial_View"] = "~/Views/Bang/Dialog.cshtml";
                    break;
                case "BANGMAU_COTMAU_SUATEN":
                    ViewData["Partial_View"] = "~/Views/BangMauCotMauDonViTenMoi/Dialog.cshtml";
                    break;
                case "TONKHOVATTU":
                    ViewData["Partial_View"] = "~/Views/TonKhoVatTu/Dialog.cshtml";
                    break;
                case "CHITIETVATTU":
                    ViewData["Partial_View"] = "~/Views/MaVatTu/Dialog.cshtml";
                    break;
            }
            //return CommonFunction.RenderPartialViewToString("~/Views/shared/HoTro/NhapNhanh.ascx", this);
            String tg = CommonFunction.RenderPartialViewToString("~/Views/Shared/NhapNhanh.cshtml", this);
            String strJ = "";
            BocTachDuLieu(ref tg, ref strJ);
            tg = tg.Trim();
            strJ = strJ.Trim();
            if (String.IsNullOrEmpty(OnLoad) == false)
            {
                strJ = JavaScriptEncode(strJ);
                strJ = String.Format("{0}({1});ImportJavascript({2});", OnLoad, JavaScriptEncode(tg), strJ);
                //strJ = "alert(\"1\");";
            }
            if (strJ == "")
            {
                return null;
            }
            return JavaScript(strJ);
        }

        private static string JavaScriptEncode(string str)
        {
            // Encode certain characters, or the JavaScript expression could be invalid

            return new JavaScriptSerializer().Serialize(str);
            //return "";
        }

        public void BocTachDuLieu(ref string str1, ref string str2)
        {
            Boolean ok = true;
            str1 = str1.Replace("\r", "");
            str1 = str1.Replace("\n", "");

            while (ok)
            {
                int cs1 = str1.IndexOf("<script");

                if (cs1 >= 0)
                {
                    int cs2 = str1.IndexOf("</script>");
                    string tg = str1.Substring(cs1, cs2 - cs1 + 9);
                    str1 = str1.Remove(cs1, cs2 - cs1 + 9);
                    cs1 = tg.IndexOf(">");
                    tg = tg.Substring(cs1 + 1, tg.Length - cs1 - 10);
                    //tg = tg.Replace("\r", "");
                    //tg = tg.Replace("\n", "");
                    str2 += tg;
                }
                else
                {
                    ok = false;
                }
            }
        }
    }
}