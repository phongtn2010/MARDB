using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel;
using DATA0200025;


namespace APP0200025.Controllers
{
    public class TruongCamController : Controller
    {
        // GET: TruongCam
        [Authorize]
        public ActionResult Index()
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "PQ_Bang_TruongCam", "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["smenu"] = 0;
            return View();
        }
        [Authorize]
        public ActionResult Edit(string MaLuat, String TenBang)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "PQ_Bang_TruongCam", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            //if (CHamChung.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            //{
            //    ViewData["MaLuat"] = MaLuat;
            //    ViewData["TenBang"] = TenBang;
            //    return View();
            //}
            //else
            //{
            //    return RedirectToAction("Index", "PermitionMessage");
            //}
            ViewData["smenu"] = 0;
            ViewData["MaLuat"] = MaLuat;
            ViewData["TenBang"] = TenBang;
            return View();
        }
        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(String ParentID, String MaLuat, String TenBang)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "PQ_Bang_TruongCam", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            String tg = Convert.ToString(Request.Form[ParentID + "_txt"]);
            String[] arr = tg.Split(',');

            int vR = CPQ_BANG.Delete_Table_Truong_Cam(Convert.ToString(MaLuat), TenBang);
            
            int i;
            tg = "";
            for (i = 0; i < arr.Length; i++)
            {
                String[] arr1 = arr[i].Split(';');

                if (arr1.Length > 1)
                {
                    tg += arr1[0] + ",";
                }
            }
            if (tg != "")
            {
                CPQ_BANG.Insert_Table_Truong_Cam(Convert.ToString(MaLuat), TenBang, tg);
            }
            return RedirectToAction("Index", "ChucNangCam", new { MaLuat = MaLuat });
        }
    }
}