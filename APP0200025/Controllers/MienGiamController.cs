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

namespace APP0200025.Controllers
{
    public class MienGiamController : Controller
    {
        private SendService _sendService = new SendService();

        Bang bang = new Bang("CNN26_HoSo");

        // GET: MienGiam
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LanhDaoCuc_ChoPheDuyet(CHoSoSearch hoSoSearch)
        {
            if (hoSoSearch == null || hoSoSearch.iID_MaTrangThai == 0)
            {
                hoSoSearch = new CHoSoSearch
                {
                    iID_MaTrangThai = 2,
                    Page = 1,
                    PageSize = Globals.PageSize
                };
            }

            ViewData["menu"] = 212;
            return View(hoSoSearch);
        }

        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LanhDaoCuc_ChoPheDuyet_Search(string ParentID)
        {
            string _sMaHoSo = CString.SafeString(Request.Form[ParentID + "_sMaHoSo"]);
            string _sTenDoanhNghiep = CString.SafeString(Request.Form[ParentID + "_sTenDoanhNghiep"]);
            string _sTenTACN = CString.SafeString(Request.Form[ParentID + "_sTenTACN"]);
            string _FromDate = CString.SafeString(Request.Form[ParentID + "_viTuNgayDen"]);
            string _ToDate = CString.SafeString(Request.Form[ParentID + "_viDenNgayDen"]);
            string _sSoTiepNhan = CString.SafeString(Request.Form[ParentID + "_sSoTiepNhan"]);
            string _TuNgayTiepNhan = CString.SafeString(Request.Form[ParentID + "_viTuNgayTiepNhan"]);
            string _DenNgayTiepNhan = CString.SafeString(Request.Form[ParentID + "_viDenNgayTiepNhan"]);
            CHoSoSearch models = new CHoSoSearch
            {
                iID_MaTrangThai = 2,
                sMaHoSo = _sMaHoSo,
                sTenDoanhNghiep = _sTenDoanhNghiep,
                sTenTACN = _sTenTACN,
                TuNgayDen = _FromDate,
                DenNgayDen = _ToDate,
                sSoTiepNhan = _sSoTiepNhan,
                TuNgayTiepNhan = _TuNgayTiepNhan,
                DenNgayTiepNhan = _DenNgayTiepNhan
            };
            return RedirectToAction("LanhDaoCuc_ChoPheDuyet", "MienGiam", models);
        }

        public ActionResult LanhDaoCuc_ChoPheDuyet_Detail(string iID_MaHoSo)
        {
            CHoSo26.UpdateNguoiXem(iID_MaHoSo, User.Identity.Name);

            HoSo26Models models = CHoSo26.Get_Detail(Convert.ToInt64(iID_MaHoSo));

            ViewData["menu"] = 212;
            return View(models);
        }

        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult LanhDaoCuc_ChoPheDuyet_KySo(string ParentID)
        {
            String sUserName = User.Identity.Name;
            String sTenUser = CPQ_NGUOIDUNG.Get_TenNguoiDung(sUserName);

            string sID_MaHoSo = Request.Form[ParentID + "_HO_SO"];
            if (!string.IsNullOrEmpty(sID_MaHoSo))
            {
                string[] arr = sID_MaHoSo.Split(',');
                for (int i = 0; i < arr.Length; i++)
                {
                    if (String.IsNullOrEmpty(arr[i]) == false)
                    {
                        long iID_MaHoSo = Convert.ToInt64(arr[i]);

                        bang.MaNguoiDungSua = sUserName;
                        bang.IPSua = Request.UserHostAddress;
                        bang.DuLieuMoi = false;
                        bang.GiaTriKhoa = iID_MaHoSo;
                        bang.CmdParams.Parameters.AddWithValue("@sSoGDK", CTaoSoGDK.GetSoGDK());
                        bang.CmdParams.Parameters.AddWithValue("@dNgayXacNhan", DateTime.Now);
                        bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", 3);
                        bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", 2);
                        bang.Save();

                        CLichSuHoSo.Add(Convert.ToInt64(iID_MaHoSo), "", sUserName, sTenUser, eDoiTuong.TYPE_3, "Lãnh đạo cục", eHanhDong.TYPE_2_3, "Ký số", "", "", eTrangThai.TYPE_2, "Đã tiếp nhận", eTrangThai.TYPE_3, "Lãnh đạo cục đã phê duyệt ");
                    }   
                }

                TempData["msg"] = "success";
            }
            else
            {
                TempData["msg"] = "error";
            }    

            return RedirectToAction("LanhDaoCuc_ChoPheDuyet");
        }

        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult LanhDaoCuc_ChoPheDuyet_World(string ParentID)
        {
            String sUserName = User.Identity.Name;
            String sTenUser = CPQ_NGUOIDUNG.Get_TenNguoiDung(sUserName);

            string iID_MaHoSo = Request.Form[ParentID + "_iID_MaHoSo"];
            string sMaHoSo = Request.Form[ParentID + "_sMaHoSo"];

            
            CHoSo26.CleanNguoiXem(iID_MaHoSo);
            return RedirectToAction("LanhDaoCuc_ChoPheDuyet");
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public PartialViewResult HangHoaChiTiet(string iID_MaHangHoa)
        {
            HangHoa26Models hanghoa = CHangHoa26.Get_Detail(Convert.ToInt64(iID_MaHangHoa));

            return PartialView(hanghoa);
        }
    }
}