﻿using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using DomainModel;
using DomainModel.Abstract;
using DATA0200025;
using DATA0200025.Models;
using DATA0200025.SearchModels;
using DATA0200025.WebServices.XmlType.Request;
using DATA0200025.WebServices;

namespace APP0200025.Controllers
{
    public class DaCapThongBaoKetQuaController : Controller
    {
        // GET: DaCapThongBaoKetQua
        Bang bang = new Bang("CNN25_HangHoa");

        private string ViewPath = "~/Views/DaCapThongBaoKetQua/";
        private SendService _sendService = new SendService();
        // GET: ChoTiepNhanHoSo
        public ActionResult Index(sHoSoModels models)
        {
            if (models == null || models.LoaiDanhSach == 0)
            {
                models = new sHoSoModels
                {
                    LoaiDanhSach = (int)clLoaiDanhSach.From.DaCapThongBaoKetQua,
                    Page = 1,
                    PageSize = Globals.PageSize
                };
            }
            return View(models);
        }
        public ActionResult Detail(string iID_MaHangHoa)
        {
            //if (BaoMat.ChoPhepLamViec(User.Identity.Name, bang.TenBang, "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            //{
            //    return RedirectToAction("Index", "PermitionMessage");
            //}
            clHangHoa.UpdateNguoiXem(iID_MaHangHoa, User.Identity.Name);
            ViewData["DuLieuMoi"] = "0";
            ViewData["smenu"] = 187;
            HoSoModels models = clHoSo.GetHoSoById(Convert.ToInt32(iID_MaHangHoa));

            return View(models);
        }



        /// <summary>
        /// update data màn hình YeuCauBoXung
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="MaHoSo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ThuHoiSubmit()
        {
            ////  HttpPostedFile fileư= Request.Files;
            String ParentID = "TH";
            NameValueCollection values = new NameValueCollection();
            HttpFileCollectionBase files = Request.Files;
            string _sNoiDung = CString.SafeString(Request.Form[ParentID + "_sNoiDung"]);
            string iID_MaHangHoa = CString.SafeString(Request.Form[ParentID + "_iID_MaHangHoa"]);
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
                ViewData["iID_MaHangHoa"] = CString.SafeString(iID_MaHangHoa);
                return View();
            }
            string sFileTemp = "", sFileName = "";
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
                    sFileName = string.Format("{0}_{1}", subName, postedFile.FileName);
                    sFileTemp = string.Format(newPath + "/{0}_{1}", subName, postedFile.FileName);
                    string filePath = Server.MapPath("~" + sFileTemp);
                    postedFile.SaveAs(filePath);
                }
            }
            HangHoaModels hangHoa = clHangHoa.GetHangHoaById(Convert.ToInt32(iID_MaHangHoa));
            TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.ThuHoiCongVan, hangHoa.iID_MaTrangThai, hangHoa.iID_MaTrangThaiTruoc);
            //Gui sang NSW
            ThuHoiGiayXNCL resultConfirm = new ThuHoiGiayXNCL();
            resultConfirm.NSWFileCode = hangHoa.sMaHoSo;
            resultConfirm.Reason = _sNoiDung;
            resultConfirm.AttachmentId = "01";
            resultConfirm.FileName = sFileName;
            resultConfirm.FileLink = string.Format("{0}{1}", clCommon.BNN_Url, sFileTemp);
            resultConfirm.CancelDate = DateTime.Now;
            resultConfirm.CancelDateString = "";
            resultConfirm.SignConfirmDate = DateTime.Now;
            resultConfirm.SignConfirmDateString = "";
            resultConfirm.SignConfirmName = "";
            resultConfirm.AniFeedResultNo = "";//Số giấy xác nhận chất lượng (của BNN)

            string error = _sendService.ThuHoiGiayXNCL(hangHoa.sMaHoSo, resultConfirm);
            if (error.Equals("99"))
            {


                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                bang.TruyenGiaTri(ParentID, Request.Form);
                bang.DuLieuMoi = false;
                bang.GiaTriKhoa = iID_MaHangHoa;
                bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy ?? "");
                bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hangHoa.iID_MaTrangThai);
                bang.Save();
                clLichSuHoSo.InsertLichSu(hangHoa.iID_MaHangHoa, User.Identity.Name, (int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.ThuHoiCongVan, _sNoiDung, sFileTemp, hangHoa.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);
            }
            clHoSo.CleanNguoiXem(iID_MaHangHoa);
            ResultModels result = new ResultModels { success = true };
            result.value = Url.Action("Index");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Thoat(string iID_MaHangHoa)
        {
            ResultModels result = clHoSo.CleanNguoiXem(iID_MaHangHoa);
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
                LoaiDanhSach = (int)clLoaiDanhSach.From.DaCapThongBaoKetQua,
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