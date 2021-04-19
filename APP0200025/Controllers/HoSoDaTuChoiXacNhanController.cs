using DATA0200025;
using DATA0200025.Models;
using DATA0200025.SearchModels;
using DomainModel;
using DomainModel.Abstract;
using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using DATA0200025.WebServices.XmlType.Request;
using DATA0200025.WebServices;
using APP0200025.Models;

namespace APP0200025.Controllers
{
    public class HoSoDaTuChoiXacNhanController : Controller
    {
        //  Đã từ chối xác nhận GĐK
        Bang bang = new Bang("CNN25_HoSo");

        private string ViewPath = "~/Views/HoSoDaTuChoiXacNhan/";
        private SendService _sendService = new SendService();

        // GET: HoSoDaTuChoiXacNhan
        public ActionResult Index(sHoSoModels models)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, bang.TenBang, "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            if (models == null || models.LoaiDanhSach == 0)
            {
                models = new sHoSoModels
                {
                    LoaiDanhSach = 3,
                    Page = 1,
                    PageSize = Globals.PageSize
                };
            }
            ViewData["menu"] = 232;
            return View(models);
        }
        public ActionResult Detail(string iID_MaHoSo)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, bang.TenBang, "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            clHoSo.UpdateNguoiXem(iID_MaHoSo, User.Identity.Name);

            ViewData["menu"] = 232;
            HoSoModels models = clHoSo.GetHoSoById(Convert.ToInt32(iID_MaHoSo));

            return View(models);
        }
        /// <summary>
        /// view màn hình TiepNhanHoSo
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult TiepNhanHoSo()
        {
            ViewData["DuLieuMoi"] = "0";
            return View();
        }

        /// <summary>
        /// update data màn hình TiepNhanHoSo
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="MaHoSo"></param>
        /// <returns></returns>
        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult GuiDoanhNghiep(String ParentID)
        {
            string iID_MaHoSo = Request.Form[ParentID + "_iID_MaHoSo"];
            if (String.IsNullOrEmpty(iID_MaHoSo) == false)
            {
                HoSoModels hoSo = clHoSo.GetHoSoById(Convert.ToInt32(iID_MaHoSo));

                //iTrangThaiTiepTheo=21
                TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.ChuyenDNThongBaoTuChoiCapGDK, hoSo.iID_MaTrangThai, hoSo.iID_MaTrangThaiTruoc);

                string error = "";
                if(CHamRieng.iNSW == 1)
                {
                    //XML(12, 10)
                    KetQuaXuLy resultConfirm = new KetQuaXuLy();
                    resultConfirm.NSWFileCode = hoSo.sMaHoSo;
                    resultConfirm.Reason = "Chuyển thông báo từ chối cấp GĐK";
                    resultConfirm.AttachmentId = "01";
                    resultConfirm.FileName = "";
                    resultConfirm.FileLink = "";
                    resultConfirm.NameOfStaff = CPQ_NGUOIDUNG.Get_TenNguoiDung(User.Identity.Name);
                    resultConfirm.ResponseDateString = DateTime.Now;
                    error = _sendService.KetQuaXuLy(hoSo.sMaHoSo, resultConfirm, "10");
                }
                else
                {
                    error = "99";
                }
                if (error.Equals("99"))
                {
                    bang.MaNguoiDungSua = User.Identity.Name;
                    bang.IPSua = Request.UserHostAddress;
                    bang.DuLieuMoi = false;
                    bang.TruyenGiaTri(ParentID, Request.Form);
                    bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy);
                    bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hoSo.iID_MaTrangThai);
                    bang.Save();
                    clLichSuHoSo.InsertLichSu(hoSo.iID_MaHoSo, User.Identity.Name, (int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.YeuCauBoSungHoSo, "Chuyển thông báo từ chối cấp GĐK", "", hoSo.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);

                    TempData["msg"] = "success";
                }
                else
                {
                    TempData["msg"] = "error";
                }
            }
            else
            {
                TempData["msg"] = "error";
            }

            clHoSo.CleanNguoiXem(iID_MaHoSo);
            

            return RedirectToAction("Index");
        }
        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult GuiDoanhNghiepSubmit(String iID_MaHoSo)
        {
            ResultModels result = new ResultModels();
            if (String.IsNullOrEmpty(iID_MaHoSo) == false)
            {
                HoSoModels hoSo = clHoSo.GetHoSoById(Convert.ToInt64(iID_MaHoSo));

                TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.ChuyenDNThongBaoTuChoiCapGDK, hoSo.iID_MaTrangThai, hoSo.iID_MaTrangThaiTruoc);

                string error = "";
                if (CHamRieng.iNSW == 1)
                {
                    //XML(12, 10)
                    KetQuaXuLy resultConfirm = new KetQuaXuLy();
                    resultConfirm.NSWFileCode = hoSo.sMaHoSo;
                    resultConfirm.Reason = "Chuyển thông báo từ chối cấp GĐK";
                    resultConfirm.AttachmentId = "01";
                    resultConfirm.FileName = "";
                    resultConfirm.FileLink = "";
                    resultConfirm.NameOfStaff = CPQ_NGUOIDUNG.Get_TenNguoiDung(User.Identity.Name);
                    resultConfirm.ResponseDateString = DateTime.Now;
                    error = _sendService.KetQuaXuLy(hoSo.sMaHoSo, resultConfirm, "10");
                }
                else
                {
                    error = "99";
                }
                
                if (error == "99")
                {
                    bang.MaNguoiDungSua = User.Identity.Name;
                    bang.IPSua = Request.UserHostAddress;
                    bang.DuLieuMoi = false;
                    bang.GiaTriKhoa = iID_MaHoSo;
                    bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy);
                    bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hoSo.iID_MaTrangThai);
                    bang.Save();

                    clLichSuHoSo.InsertLichSu(hoSo.iID_MaHoSo, User.Identity.Name, (int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.YeuCauBoSungHoSo, "Chuyển thông báo từ chối cấp GĐK", "", hoSo.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);

                    result.success = true;
                }
                else
                {
                    result.success = false;
                }
            }
            else
            {
                result.success = false;
            }
            result.value = Url.Action("Index");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Thoat(string iID_MaHoSo)
        {
            ResultModels result = clHoSo.CleanNguoiXem(iID_MaHoSo);
            result.value = Url.Action("Index");
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Search(string ParentID)
        {
            string _sMaHoSo = CString.SafeString(Request.Form[ParentID + "_sMaHoSo"]);
            string _sTenDoanhNghiep = CString.SafeString(Request.Form[ParentID + "_sTenDoanhNghiep"]);
            string _sTenTACN = CString.SafeString(Request.Form[ParentID + "_sTenTACN"]);
            string _TuNgayDen = CString.SafeString(Request.Form[ParentID + "_viTuNgayDen"]);
            string _DenNgayDen = CString.SafeString(Request.Form[ParentID + "_viDenNgayDen"]);
            string TuNgayTiepNhan = CString.SafeString(Request.Form[ParentID + "_viTuNgayTiepNhan"]);
            string DenNgayTiepNhan = CString.SafeString(Request.Form[ParentID + "_viDenNgayTiepNhan"]);
            string sSoTiepNhan= CString.SafeString(Request.Form[ParentID + "_sSoTiepNhan"]);
            sHoSoModels models = new sHoSoModels
            {
                LoaiDanhSach = 3,
                sMaHoSo = _sMaHoSo,
                sTenDoanhNghiep = _sTenDoanhNghiep,
                sTenTACN = _sTenTACN,
                TuNgayDen = _TuNgayDen,
                DenNgayDen = _DenNgayDen,
                TuNgayTiepNhan= TuNgayTiepNhan,
                DenNgayTiepNhan = DenNgayTiepNhan,
                sSoTiepNhan= sSoTiepNhan

            };

            TempData["menu"] = 232;
            TempData["msg"] = "success";
            return RedirectToAction("Index", models);
        }
    }
}