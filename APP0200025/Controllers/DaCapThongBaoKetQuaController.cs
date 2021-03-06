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
using APP0200025.Models;

namespace APP0200025.Controllers
{
    public class DaCapThongBaoKetQuaController : Controller
    {
        // GET: DaCapThongBaoKetQua
        Bang bang = new Bang("CNN25_HangHoa");

        private SendService _sendService = new SendService();

        // GET: ChoTiepNhanHoSo
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
                    LoaiDanhSach = (int)clLoaiDanhSach.From.DaCapThongBaoKetQua,
                    Page = 1,
                    PageSize = Globals.PageSize
                };
            }

            ViewData["menu"] = 239;
            return View(models);
        }
        public ActionResult Detail(string iID_MaHangHoa)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, bang.TenBang, "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            clHangHoa.UpdateNguoiXem(iID_MaHangHoa, User.Identity.Name);

            HangHoaModels models = clHangHoa.GetHangHoaById(Convert.ToInt64(iID_MaHangHoa));
            
            ViewData["menu"] = 239;
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
            ResultModels result = new ResultModels();

            String sUserName = User.Identity.Name;
            String sIP = Request.UserHostAddress;

            String ParentID = "TH";
            NameValueCollection values = new NameValueCollection();
            HttpFileCollectionBase files = Request.Files;
            string _sNoiDung = CString.SafeString(Request.Form[ParentID + "_sNoiDung"]);
            string iID_MaHangHoa = CString.SafeString(Request.Form[ParentID + "_iID_MaHangHoa"]);
            if (string.IsNullOrEmpty(iID_MaHangHoa))
            {
                values.Add("err_sNoiDung", "Bạn nhập thông tin yêu cầu cần bổ xung");
            }
            if (string.IsNullOrEmpty(_sNoiDung))
            {
                values.Add("err_sNoiDung", "Bạn nhập thông tin yêu cầu cần bổ xung");
            }
            if (values.Count > 0)
            {
                //for (int i = 0; i <= (values.Count - 1); i++)
                //{
                //    ModelState.AddModelError(ParentID + "_" + values.GetKey(i), values[i]);
                //}

                ViewData["iID_MaHangHoa"] = CString.SafeString(iID_MaHangHoa);

                result.success = false;
                result.value = Url.Action("Index");
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            HangHoaModels hangHoa = clHangHoa.GetHangHoaById(Convert.ToInt64(iID_MaHangHoa));

            long iID_MaDinhKem = -1;
            string sFileTemp = "", sFileName = "";
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase postedFile = files[i];
                if (postedFile != null && postedFile.ContentLength > 0)
                {
                    sFileName = postedFile.FileName;

                    ResDinhKemFiles resDinhKemFile = new ResDinhKemFiles();
                    resDinhKemFile = CDinhKemFiles.UploadFile(postedFile);
                    if (resDinhKemFile != null && resDinhKemFile.ItemId > 0)
                    {
                        sFileTemp = resDinhKemFile.UrlFile;

                        //Luu vao bang Dinh Kem
                        iID_MaDinhKem = CDinhKem.ThemDinhKem(hangHoa.iID_MaHoSo, hangHoa.iID_MaHangHoa, 91, "0", hangHoa.sMaHoSo, "File Thu hồi giấy XNCL.", sFileName, "", null, 1, sFileTemp, sUserName, sIP, resDinhKemFile.ItemId);
                    }
                }
            }
            
            TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.ThuHoiCongVan, hangHoa.iID_MaTrangThai, hangHoa.iID_MaTrangThaiTruoc);

            DateTime dNgayThuHoi = DateTime.Now;

            string error = "";
            if(CHamRieng.iNSW == 1)
            {
                //XML 20(23)
                ThuHoiGiayXNCL resultConfirm = new ThuHoiGiayXNCL();
                resultConfirm.NSWFileCode = hangHoa.sMaHoSo;
                resultConfirm.Reason = _sNoiDung;
                resultConfirm.AttachmentId = iID_MaDinhKem.ToString();
                resultConfirm.FileName = sFileName;
                resultConfirm.FileLink = sFileTemp;
                resultConfirm.CancelDateString = dNgayThuHoi;
                resultConfirm.SignConfirmDateString = dNgayThuHoi;
                resultConfirm.SignConfirmName = eCoQuanXuLy.sNguoiKy_Ten;
                resultConfirm.AniFeedResultNo = hangHoa.sSoThongBaoKetQua;//Số giấy xác nhận chất lượng (của BNN)

                error = _sendService.ThuHoiGiayXNCL(hangHoa.sMaHoSo, resultConfirm);
            }
            else
            {
                error = "99";
            }
            if (error.Equals("99"))
            {
                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                bang.TruyenGiaTri(ParentID, Request.Form);
                bang.DuLieuMoi = false;
                bang.GiaTriKhoa = iID_MaHangHoa;
                bang.CmdParams.Parameters.AddWithValue("@dNgayThuHoi", dNgayThuHoi);
                bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy ?? "");
                bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hangHoa.iID_MaTrangThai);
                bang.Save();

                clLichSuHangHoa.InsertLichSu(hangHoa.iID_MaHangHoa, User.Identity.Name, (int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.ThuHoiCongVan, _sNoiDung, sFileTemp, hangHoa.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);

                result.success = true;
            }
            else
            {
                result.success = false;
            }    

            clHangHoa.CleanNguoiXem(iID_MaHangHoa);
            
            result.value = Url.Action("Index");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Thoat(string iID_MaHangHoa)
        {
            ResultModels result = clHangHoa.CleanNguoiXem(iID_MaHangHoa);
            result.value = Url.Action("Index");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Search(string ParentID)
        {
            string _sMaHoSo = CString.SafeString(Request.Form[ParentID + "_sMaHoSo"]);
            string _sTenDoanhNghiep = CString.SafeString(Request.Form[ParentID + "_sTenDoanhNghiep"]);
            string _sTenTACN = CString.SafeString(Request.Form[ParentID + "_sTenTACN"]);
            string _sSoThongBaoKetQua = CString.SafeString(Request.Form[ParentID + "_sSoThongBaoKetQua"]);
            string _TuNgayThongBaoKetQua = CString.SafeString(Request.Form[ParentID + "_viTuNgayThongBaoKetQua"]);
            string _DenNgayThongBaoKetQua = CString.SafeString(Request.Form[ParentID + "_viDenNgayThongBaoKetQua"]);
            string _iTrangThai = CString.SafeString(Request.Form[ParentID + "_iID_MaTrangThai"]);
            sHoSoModels models = new sHoSoModels
            {
                LoaiDanhSach = (int)clLoaiDanhSach.From.DaCapThongBaoKetQua,
                iID_MaTrangThai = Convert.ToInt32(_iTrangThai),
                sMaHoSo = _sMaHoSo,
                sTenDoanhNghiep = _sTenDoanhNghiep,
                sTenTACN = _sTenTACN,
                sSoThongBaoKetQua = _sSoThongBaoKetQua,
                TuNgayThongBaoKetQua = _TuNgayThongBaoKetQua,
                DenNgayThongBaoKetQua = _DenNgayThongBaoKetQua
            };

            TempData["menu"] = 235;
            TempData["msg"] = "success";
            return RedirectToAction("Index", models);
        }
    }
}