using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using DomainModel;
using DomainModel.Abstract;
using DATA0200025;
using DATA0200025.Models;
using DATA0200025.SearchModels;

namespace APP0200025.Controllers
{
    public class DaPheDuyetThongBaoKetQuaController : Controller
    {
        // GET: DaPheDuyetThongBaoKetQua
        Bang bang = new Bang("CNN25_HoSo");

        private string ViewPath = "~/Views/DaPheDuyetThongBaoKetQua/";
        // GET: ChoTiepNhanHoSo
        public ActionResult Index(sHoSoModels models)
        {
            if (models == null || models.LoaiDanhSach == 0)
            {
                models = new sHoSoModels
                {
                    LoaiDanhSach = (int)clLoaiDanhSach.From.DaPheDuyetThongBaoKetQua,
                    Page = 1,
                    PageSize = Globals.PageSize
                };
            }
            return View(models);
        }
        public ActionResult Detail(string iID_MaHoSo)
        {
            //if (BaoMat.ChoPhepLamViec(User.Identity.Name, bang.TenBang, "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            //{
            //    return RedirectToAction("Index", "PermitionMessage");
            //}
            clHoSo.UpdateNguoiXem(iID_MaHoSo, User.Identity.Name);
            ViewData["DuLieuMoi"] = "0";
            ViewData["smenu"] = 187;
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
            HoSoModels hoSo = clHoSo.GetHoSoById(Convert.ToInt32(iID_MaHoSo));
            TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.ChuyenDNThongBaoKetQuaKiemTra, hoSo.iID_MaTrangThai, hoSo.iID_MaTrangThaiTruoc);
            bang.MaNguoiDungSua = User.Identity.Name;
            bang.IPSua = Request.UserHostAddress;
            bang.TruyenGiaTri(ParentID, Request.Form);
            bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy ?? "");
            bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
            bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
            bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hoSo.iID_MaTrangThai);
            bang.Save();
            clHoSo.CleanNguoiXem(iID_MaHoSo);
            clLichSuHoSo.InsertLichSu(User.Identity.Name, (int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.ChuyenDNThongBaoKetQuaKiemTra, "Chuyển doanh nghiệp thông báo kế quả kiểm tra", "", hoSo.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);
            //  XML(18, 21)

            return RedirectToAction("Index");
        }
        /// <summary>
        /// update data màn hình YeuCauBoXung
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="MaHoSo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult YeuCauBoSungSubmit()
        {
            ////  HttpPostedFile fileư= Request.Files;
            String ParentID = "BS";
            NameValueCollection values = new NameValueCollection();
            HttpFileCollectionBase files = Request.Files;
            string _sNoiDung = CString.SafeString(Request.Form[ParentID + "_sNoiDung"]);
            string iID_MaHoSo = CString.SafeString(Request.Form[ParentID + "_iID_MaHoSo"]);
            if (string.IsNullOrEmpty(_sNoiDung))
            {
                values.Add("err_sNoiDung", "Bạn nhập thông tin yêu cầu cần bổ xung");
            }
            if (values.Count > 0)
            {
                for (int i = 0; i <= (values.Count - 1); i++)
                {
                    ModelState.AddModelError(ParentID + "_" + values.GetKey(i), values[i]);
                }
                base.ViewData["DuLieuMoi"] = "0";
                ViewData["iID_MaHoSo"] = CString.SafeString(iID_MaHoSo);
                return View();
            }
            string sFileTemp = "";
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase postedFile = files[i];
                if (postedFile != null && postedFile.ContentLength > 0)
                {
                    string guid = Guid.NewGuid().ToString();
                    string sPath = "/Uploads/File";
                    DateTime TG = DateTime.Now;
                    string subPath = TG.ToString("yyyy/MM/dd");
                    string subName = TG.ToString("HHmmssfff") + "_" + guid;
                    string newPath = string.Format("{0}/{1}", sPath, subPath);
                    CImage.CreateDirectory(Server.MapPath("~" + newPath));
                    sFileTemp = string.Format(newPath + "/{0}_{1}", subName, postedFile.FileName);
                    string filePath = Server.MapPath("~" + sFileTemp);
                    postedFile.SaveAs(filePath);
                }
            }
            HoSoModels hoSo = clHoSo.GetHoSoById(Convert.ToInt32(iID_MaHoSo));
            TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.YeuCauBoSungHoSoDNCanBoSungKetQua, hoSo.iID_MaTrangThai, hoSo.iID_MaTrangThaiTruoc);
            bang.MaNguoiDungSua = User.Identity.Name;
            bang.IPSua = Request.UserHostAddress;
            bang.TruyenGiaTri(ParentID, Request.Form);
            bang.DuLieuMoi = false;
            bang.GiaTriKhoa = iID_MaHoSo;
            bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy ?? "");
            bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
            bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
            bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hoSo.iID_MaTrangThaiTruoc);
            bang.Save();
            clHoSo.CleanNguoiXem(iID_MaHoSo);
            clLichSuHoSo.InsertLichSu(User.Identity.Name, (int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.YeuCauBoSungHoSoDNCanBoSungKetQua, _sNoiDung, sFileTemp, hoSo.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);
            ResultModels result = new ResultModels { success = true };
            result.value = Url.Action("Index");
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// update data màn hình TuChoiHoSo
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="MaHoSo"></param>
        /// <returns></returns>
        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult TuChoiHoSoSubmit()
        {
            String ParentID = "TC";
            NameValueCollection values = new NameValueCollection();
            HttpFileCollectionBase files = Request.Files;
            string _sNoiDung = CString.SafeString(Request.Form[ParentID + "_sNoiDung"]);
            string iID_MaHoSo = CString.SafeString(Request.Form[ParentID + "_iID_MaHoSo"]);
            if (string.IsNullOrEmpty(_sNoiDung))
            {
                values.Add("err_sNoiDung", "Bạn nhập thông tin yêu cầu cần bổ xung");
            }
            if (values.Count > 0)
            {
                for (int i = 0; i <= (values.Count - 1); i++)
                {
                    ModelState.AddModelError(ParentID + "_" + values.GetKey(i), values[i]);
                }
                base.ViewData["DuLieuMoi"] = "0";
                ViewData["iID_MaHoSo"] = CString.SafeString(iID_MaHoSo);
                return View();
            }
            string sFileTemp = "";
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase postedFile = files[i];
                if (postedFile != null && postedFile.ContentLength > 0)
                {
                    string guid = Guid.NewGuid().ToString();
                    string sPath = "/Uploads/File";
                    DateTime TG = DateTime.Now;
                    string subPath = TG.ToString("yyyy/MM/dd");
                    string subName = TG.ToString("HHmmssfff") + "_" + guid;
                    string newPath = string.Format("{0}/{1}", sPath, subPath);
                    CImage.CreateDirectory(Server.MapPath("~" + newPath));
                    sFileTemp = string.Format(newPath + "/{0}_{1}", subName, postedFile.FileName);
                    string filePath = Server.MapPath("~" + sFileTemp);
                    postedFile.SaveAs(filePath);
                }
            }
            HoSoModels hoSo = clHoSo.GetHoSoById(Convert.ToInt32(iID_MaHoSo));
            TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.TraChuyenVienXuLyLai, hoSo.iID_MaTrangThai, hoSo.iID_MaTrangThaiTruoc);
            bang.MaNguoiDungSua = User.Identity.Name;
            bang.IPSua = Request.UserHostAddress;
            bang.TruyenGiaTri(ParentID, Request.Form);
            bang.DuLieuMoi = false;
            bang.GiaTriKhoa = iID_MaHoSo;
            bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy ?? "");
            bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
            bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
            bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hoSo.iID_MaTrangThaiTruoc);
            bang.Save();
            clHoSo.CleanNguoiXem(iID_MaHoSo);
            clLichSuHoSo.InsertLichSu(User.Identity.Name, (int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.TraChuyenVienXuLyLai, _sNoiDung, sFileTemp, hoSo.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);
            ResultModels result = new ResultModels { success = true };
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
            string _FromDate = CString.SafeString(Request.Form[ParentID + "_viFromDate"]);
            string _ToDate = CString.SafeString(Request.Form[ParentID + "_viToDate"]);
            sHoSoModels models = new sHoSoModels
            {
                LoaiDanhSach = (int)clLoaiDanhSach.From.DaPheDuyetThongBaoKetQua,
                sMaHoSo = _sMaHoSo,
                sTenDoanhNghiep = _sTenDoanhNghiep,
                sTenTACN = _sTenTACN,
                TuNgayDen = _FromDate,
                DenNgayDen = _ToDate
            };
            return RedirectToAction("Index", models);
        }
    }
}