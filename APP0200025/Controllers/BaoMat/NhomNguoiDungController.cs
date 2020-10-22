using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DATA0200025;
using APP0200025.Models;
using DomainModel;
using System.Data;

namespace APP0200025.Controllers
{
    public class NhomNguoiDungController : Controller
    {
        // GET: NhomNguoiDung
        [Authorize]
        public ActionResult Index()
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "QT_NhomNguoiDung", "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["smenu"] = 0;
            return View();
        }
        [Authorize]
        public ActionResult Create(string MaNhomNguoiDungCha)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "QT_NhomNguoiDung", "Create") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["smenu"] = 0;
            ViewData["DuLieuMoi"] = "1";
            ViewData["MaNhomNguoiDungCha"] = MaNhomNguoiDungCha;
            return RedirectToAction("Edit", "NhomNguoiDung",  new { MaNhomNguoiDungCha = MaNhomNguoiDungCha });
        }
        [Authorize]
        public ActionResult Edit(string MaNhomNguoiDung, string MaNhomNguoiDungCha)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "QT_NhomNguoiDung", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["smenu"] = 0;
            ViewData["DuLieuMoi"] = "0";
            if (string.IsNullOrEmpty(MaNhomNguoiDung))
            {
                ViewData["DuLieuMoi"] = "1";
            }
            ViewData["MaNhomNguoiDung"] = CString.SafeString(MaNhomNguoiDung);
            ViewData["MaNhomNguoiDungCha"] = CString.SafeString(MaNhomNguoiDungCha);
            return View();
        }
        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditSubmit(String ParentID, String MaNhomNguoiDungCha)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "QT_NhomNguoiDung", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            NameValueCollection values = new NameValueCollection();
            string _iID_MaNhomNguoiDung = CString.SafeString(Request.Form[ParentID + "_iID_MaNhomNguoiDung"]);
            string sTen = CString.SafeString(Request.Form[ParentID + "_sTen"]);
            String bTrangThai = Convert.ToString(Request.Form[ParentID + "_bTrangThai"]);
            string DuLieuMoi = CString.SafeString(Request.Form[ParentID + "_DuLieuMoi"]);

            if (string.IsNullOrEmpty(sTen))
            {
                values.Add("err_sTen", "Bạn chưa nhập tên nhóm người dùng!");
            }
            if (values.Count > 0)
            {
                for (int i = 0; i <= (values.Count - 1); i++)
                {
                    ModelState.AddModelError(ParentID + "_" + values.GetKey(i), values[i]);
                }
                base.ViewData["DuLieuMoi"] = "0";
                if (string.IsNullOrEmpty(_iID_MaNhomNguoiDung))
                {
                    ViewData["DuLieuMoi"] = "1";
                }
                ViewData["MaNhomNguoiDung"] = CString.SafeString(_iID_MaNhomNguoiDung);
                ViewData["MaNhomNguoiDungCha"] = CString.SafeString(MaNhomNguoiDungCha);
                return View();
            }
            else
            {
                int iTrangThai = 0;
                if (bTrangThai == "on")
                {
                    iTrangThai = 1;
                }
                
                if (DuLieuMoi == "0")
                {
                    //Sua
                    int vR = CPQ_NHOMNGUOIDUNG.Update(_iID_MaNhomNguoiDung, sTen, iTrangThai);
                }
                else
                {
                    //Them moi
                    if(MaNhomNguoiDungCha == "1")
                    {
                        //int vR = CPQ_NHOMNGUOIDUNG.Insert(MaNhomNguoiDungCha, sTen, iTrangThai);

                        int iSoLuongNhomCon = CPQ_NHOMNGUOIDUNG.Update_SoNhomCon(MaNhomNguoiDungCha);

                        int iSoNhomCon = CPQ_NHOMNGUOIDUNG.Get_SoNhom(MaNhomNguoiDungCha);

                        String MaNhom = String.Format("{0}-{1}", MaNhomNguoiDungCha, iSoNhomCon);

                        int iInsert = CPQ_NHOMNGUOIDUNG.Insert(MaNhom, sTen, iTrangThai);
                    }
                    else
                    {
                        int iSoLuongNhomCon = CPQ_NHOMNGUOIDUNG.Update_SoNhomCon(MaNhomNguoiDungCha);

                        int iSoNhomCon = CPQ_NHOMNGUOIDUNG.Get_SoNhom(MaNhomNguoiDungCha);

                        String MaNhom = String.Format("{0}-{1}", MaNhomNguoiDungCha, iSoNhomCon); 

                        int iInsert = CPQ_NHOMNGUOIDUNG.Insert(MaNhom, sTen, iTrangThai);
                    }
                }

                return RedirectToAction("Index");
            }
        }
        [Authorize]
        public ActionResult Delete(string MaNhomNguoiDung)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "QT_NhomNguoiDung", "Delete") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            int vR = 0;
            if (string.IsNullOrEmpty(MaNhomNguoiDung) == false)
            {
                //Xoa
                vR = CPQ_NHOMNGUOIDUNG.Delete(MaNhomNguoiDung);
            }

            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult Edit_Luat(string MaNhomNguoiDung)
        {
            ViewData["MaNhomNguoiDung"] = CString.SafeString(MaNhomNguoiDung);
            return View();
        }
        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditLuatSubmit(String ParentID, String MaNhomNguoiDung)
        {
            String MaLuat = Request.Form[ParentID + "_iID_MaLuat"];
            if (String.IsNullOrEmpty(MaLuat) == false)
            {
                int iCheck = CPQ_NHOMNGUOIDUNG.Check_NhomNguoiDung_Luat(MaNhomNguoiDung, MaLuat);
                if(iCheck == 0)
                {
                    int vR = CPQ_NHOMNGUOIDUNG.Insert_NhomNguoiDung_Luat(MaNhomNguoiDung, MaLuat, 1);
                }
            }
            return RedirectToAction("Edit_Luat", new { MaNhomNguoiDung = MaNhomNguoiDung });
        }
        [Authorize]
        public ActionResult Delete_Luat(string MaNhomNguoiDung, string MaLuat)
        {
            int vR = 0;
            if (string.IsNullOrEmpty(MaNhomNguoiDung) == false && string.IsNullOrEmpty(MaLuat) == false)
            {
                //Xoa
                vR = CPQ_NHOMNGUOIDUNG.Delete_NhonNguoiDung_Luat(MaNhomNguoiDung, MaLuat);
            }
            return RedirectToAction("Edit_Luat", new { MaNhomNguoiDung = MaNhomNguoiDung });
        }
        [Authorize]
        public ActionResult Edit_NguoiDung(string MaNhomNguoiDung)
        {
            ViewData["MaNhomNguoiDung"] = CString.SafeString(MaNhomNguoiDung);
            return View();
        }
        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditNguoiDungSubmit(String ParentID, String MaNhomNguoiDung)
        {
            String MaNguoiDung = Request.Form[ParentID + "_sID_MaNguoiDung"];

            if (MaNguoiDung != null)
            {
                int vR = CPQ_NGUOIDUNG.Update_NhomNguoiDung(MaNguoiDung, MaNhomNguoiDung);
            }

            return RedirectToAction("Edit_NguoiDung", new { MaNhomNguoiDung = MaNhomNguoiDung });
        }
        [Authorize]
        public ActionResult Delete_NguoiDung(string MaNguoiDung, string MaNhomNguoiDung)
        {

            if (String.IsNullOrEmpty(MaNguoiDung) == false)
            {
                int vR = CPQ_NGUOIDUNG.Update_NhomNguoiDung(MaNguoiDung, "1-1");
            }
            return RedirectToAction("Edit_NguoiDung", new { MaNhomNguoiDung = MaNhomNguoiDung });
        }

        public ActionResult get_Auto_NguoiDung(string term, string term1)
        {
            List<Object> list = new List<Object>();

            DataTable dt = CPQ_NGUOIDUNG.Get_Table_NguoiDung_Auto(term);
            
            int i;
            for (i = 0; i < dt.Rows.Count; i++)
            {
                Object item = new {
                    label = dt.Rows[i]["sID_MaNguoiDung"],
                    value = dt.Rows[i]["sID_MaNguoiDung"]
                };
                list.Add(item);
            }
            dt.Dispose();

            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}