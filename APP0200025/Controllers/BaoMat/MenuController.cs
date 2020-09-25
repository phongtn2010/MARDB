using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DATA0200025;
using APP0200025.Models;
using DomainModel;

namespace APP0200025.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        [Authorize]
        public ActionResult Index()
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "PQ_Menu", "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            return View();
        }
        [Authorize]
        public ActionResult Detail(string MaMenu)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "PQ_Menu", "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            return View();
        }
        [Authorize]
        public ActionResult Create(int MaMenuCha)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "PQ_Menu", "Create") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            return RedirectToAction("Edit", "Menu", new { MaMenuCha = MaMenuCha });
        }
        [Authorize]
        public ActionResult Edit(string MaMenu, string MaMenuCha)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "PQ_Menu", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["DuLieuMoi"] = 0;
            if (string.IsNullOrEmpty(MaMenu))
            {
                ViewData["DuLieuMoi"] = 1;
            }
            ViewData["MaMenu"] = MaMenu;
            ViewData["MaMenuCha"] = MaMenuCha;
            ViewData["smenu"] = 0;
            return View();
        }
        [Authorize]
        [ValidateInput(false), HttpPost]
        public ActionResult Edit(string ParentID, string MaMenu, string MaMenuCha)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "PQ_Menu", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            String sTen = CString.SafeString(Request.Form[ParentID + "_sTen"]).Trim();
            String sURL = CString.SafeString(Request.Form[ParentID + "_sURL"]).Trim();

            NameValueCollection arrLoi = new NameValueCollection();
            if (string.IsNullOrEmpty(sTen))
            {
                arrLoi.Add("err_sTen", "Bạn chưa nhập tên menu!");
            }
            if (arrLoi.Count > 0)
            {
                for (int i = 0; i <= arrLoi.Count - 1; i++)
                {
                    ModelState.AddModelError(ParentID + "_" + arrLoi.GetKey(i), arrLoi[i]);
                }

                ViewData["smenu"] = 0;
                ViewData["MaMenu"] = MaMenu;
                ViewData["MaMenuCha"] = MaMenuCha;
                ViewData["DuLieuMoi"] = 0;
                if (string.IsNullOrEmpty(MaMenu))
                {
                    ViewData["DuLieuMoi"] = 1;
                }
                return View();
            }
            

            int vR = 0;
            if (string.IsNullOrEmpty(MaMenu))
            {
                //Them moi
                vR = CPQ_MENU.Insert(MaMenuCha, sTen, sURL);
            }
            else
            {
                //Sua
                vR = CPQ_MENU.Update(Convert.ToInt32(MaMenu), MaMenuCha, sTen, sURL);
            }

            if (vR == 1)
            {
                return RedirectToAction("Index", "Menu");
            }
            else
            {
                ViewData["smenu"] = 0;
                ViewData["MaMenu"] = MaMenu;
                ViewData["MaMenuCha"] = MaMenuCha;
                ViewData["DuLieuMoi"] = 0;
                if (string.IsNullOrEmpty(MaMenu))
                {
                    ViewData["DuLieuMoi"] = 1;
                }
                return View();
            }
        }
        [Authorize]
        public ActionResult Delete(string MaMenu)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "PQ_Menu", "Delete") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            int vR = 0;
            if (string.IsNullOrEmpty(MaMenu) == false)
            {
                //Xoa
                vR = CPQ_MENU.Delete(Convert.ToInt32(MaMenu));
            }

            if (vR == 1)
            {
                return RedirectToAction("Index", "Menu");
            }
            else
            {
                return RedirectToAction("Index", "Menu");
            }
        }
        [Authorize]
        public ActionResult Sort(String MaMenuCha)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "PQ_Menu", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["smenu"] = 0;
            ViewData["MaMenuCha"] = CString.SafeString(MaMenuCha);
            return View();
        }
        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Sort(String ParentID, String id)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "PQ_Menu", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            string strOrder = CString.SafeString(Request.Form["hiddenOrder"].ToString());
            if (strOrder.IndexOf('$') > 0)
            {
                String[] arrTG = strOrder.Split('$');
                int i;
                for (i = 0; i < arrTG.Length - 1; i++)
                {
                    int vR = CPQ_MENU.Sort(Convert.ToInt32(arrTG[i]), i);
                }
            }
            return RedirectToAction("Index", "Menu");
        }

        [Authorize]
        public ActionResult Menu_Cam()
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "PQ_Menu_Cam", "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            return View();
        }
        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Menu_Cam(String MaLuat)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "PQ_Menu_Cam", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            String DSMaMenuItem = CString.SafeString(Convert.ToString(Request.Form["MenuItem_Cam"])).Trim();

            if (String.IsNullOrEmpty(DSMaMenuItem) == false)
            {
                CPQ_MENU.Delete_MenuCam(MaLuat);
                
                String[] arr = DSMaMenuItem.Split(',');
                int MaMenuItem;
                for (int i = 0; i < arr.Length; i++)
                {
                    MaMenuItem = Convert.ToInt32(arr[i]);

                    int vR = CPQ_MENU.Insert_MenuCam(MaMenuItem, MaLuat);
                }
            }
            else
            {
                CPQ_MENU.Delete_MenuCam(MaLuat);
            }
            return RedirectToAction("Index", "Luat", new { MaLuat = MaLuat });
        }
    }
}