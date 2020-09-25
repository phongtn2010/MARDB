using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel;
using DATA0200026;

namespace APP0200026.Controllers
{
    public class ChucNangCamController : Controller
    {
        // GET: ChucNangCam
        [Authorize]
        public ActionResult Index(string MaLuat)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "PQ_Bang_ChucNangCam", "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["MaLuat"] = CString.SafeString(MaLuat);
            ViewData["smenu"] = 0;
            return View();
        }
        [Authorize]
        public ActionResult Edit(string MaLuat)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "PQ_Bang_ChucNangCam", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["MaLuat"] = MaLuat;
            ViewData["smenu"] = 0;
            return View();
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(String ParentID, String MaLuat)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "PQ_Bang_ChucNangCam", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            String tg = Convert.ToString(Request.Form[ParentID + "_txt"]);
            String[] arr = tg.Split(',');

            int vR = CPQ_BANG.Delete_Table_ChucNang_Cam(Convert.ToString(MaLuat));

            int i, j;
            for (i = 0; i < arr.Length; i++)
            {
                String[] arr1 = arr[i].Split(';');

                tg = "";
                for (j = 0; j < arr1.Length - 1; j++)
                {
                    switch (arr1[j])
                    {
                        case "0":
                            tg += "Detail" + ",";
                            break;
                        case "1":
                            tg += "Create" + ",";
                            break;
                        case "2":
                            tg += "Delete" + ",";
                            break;
                        case "3":
                            tg += "Edit" + ",";
                            break;
                        case "4":
                            tg += "Share" + ",";
                            break;
                        case "5":
                            tg += "Responsibility" + ",";
                            break;
                    }
                }
                if (tg != "")
                {
                    CPQ_BANG.Insert_Table_ChucNang_Cam(Convert.ToString(MaLuat), arr1[0], tg);
                }
            }
            return RedirectToAction("Index", "ChucNangCam", new { MaLuat = MaLuat });
        }
    }
}