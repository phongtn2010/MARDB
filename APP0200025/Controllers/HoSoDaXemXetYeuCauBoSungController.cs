﻿using DATA0200025;
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
using System.Data;

namespace APP0200025.Controllers
{
    public class HoSoDaXemXetYeuCauBoSungController : Controller
    {
        // GET: HoSoDaGuiBoSung
        Bang bang = new Bang("CNN25_HoSo");
        //Phòng TACN yêu cầu bổ sung hồ sơ
        private string ViewPath = "~/Views/HoSoDaXemXetYeuCauBoSung/";
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
                    LoaiDanhSach = 4,
                    Page = 1,
                    PageSize = Globals.PageSize
                };
            }
            ViewData["menu"] = 231;
            return View(models);
        }
        public ActionResult Detail(string iID_MaHoSo)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, bang.TenBang, "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            clHoSo.UpdateNguoiXem(iID_MaHoSo, User.Identity.Name);

            ViewData["menu"] = 231;
            HoSoModels models = clHoSo.GetHoSoById(Convert.ToInt64(iID_MaHoSo));
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
                HoSoModels hoSo = clHoSo.GetHoSoById(Convert.ToInt64(iID_MaHoSo));
                TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.GuiDoanhNghiepThongBaoYeuCauBoSungHoSo, hoSo.iID_MaTrangThai, hoSo.iID_MaTrangThaiTruoc);

                long iID_MaLichSu = 0;
                String sNoiDung = "", sFileUrl = "";
                DataTable dtLS = clLichSuHoSo.GetDataTableBoSungTuChoi(hoSo.iID_MaHoSo, hoSo.iID_MaTrangThai);
                if (dtLS.Rows.Count > 0)
                {
                    iID_MaLichSu = Convert.ToInt64(dtLS.Rows[0]["id"]);
                    sNoiDung = Convert.ToString(dtLS.Rows[0]["sNoiDung"]);
                    sFileUrl = Convert.ToString(dtLS.Rows[0]["sFile"]);
                }
                dtLS.Dispose();

                string error = "";
                if(CHamRieng.iNSW == 1)
                {
                    //Gui XML (12,09)
                    KetQuaXuLy resultConfirm = new KetQuaXuLy();
                    resultConfirm.NSWFileCode = hoSo.sMaHoSo;
                    resultConfirm.Reason = sNoiDung;
                    resultConfirm.AttachmentId = "";
                    resultConfirm.FileName = "";
                    resultConfirm.FileLink = sFileUrl;
                    resultConfirm.NameOfStaff = CPQ_NGUOIDUNG.Get_TenNguoiDung(User.Identity.Name);
                    resultConfirm.ResponseDateString = DateTime.Now;
                    error = _sendService.KetQuaXuLy(hoSo.sMaHoSo, resultConfirm, "09");
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
                    bang.GiaTriKhoa = iID_MaHoSo;
                    bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy);
                    bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hoSo.iID_MaTrangThai);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                    bang.Save();

                    clLichSuHoSo.InsertLichSu(hoSo.iID_MaHoSo, User.Identity.Name, (int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.GuiDoanhNghiepThongBaoYeuCauBoSungHoSo, "Gửi doanh nghiệp thông báo yêu cầu bổ sung hồ sơ", "", hoSo.iID_MaTrangThaiTruoc, trangThaiTiepTheo.iID_MaTrangThai);

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

                TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.GuiDoanhNghiepThongBaoYeuCauBoSungHoSo, hoSo.iID_MaTrangThai, hoSo.iID_MaTrangThaiTruoc);

                long iID_MaLichSu = 0;
                String sNoiDung = "", sFileUrl = "";
                DataTable dtLS = clLichSuHoSo.GetDataTableBoSungTuChoi(hoSo.iID_MaHoSo, hoSo.iID_MaTrangThai);
                if (dtLS.Rows.Count > 0)
                {
                    iID_MaLichSu = Convert.ToInt64(dtLS.Rows[0]["id"]);
                    sNoiDung = Convert.ToString(dtLS.Rows[0]["sNoiDung"]);
                    sFileUrl = Convert.ToString(dtLS.Rows[0]["sFile"]);
                }
                dtLS.Dispose();

                string error = "";
                if (CHamRieng.iNSW == 1)
                {
                    //Gui XML (12,09)
                    KetQuaXuLy resultConfirm = new KetQuaXuLy();
                    resultConfirm.NSWFileCode = hoSo.sMaHoSo;
                    resultConfirm.Reason = sNoiDung;
                    resultConfirm.AttachmentId = "";
                    resultConfirm.FileName = "";
                    resultConfirm.FileLink = sFileUrl;
                    resultConfirm.NameOfStaff = CPQ_NGUOIDUNG.Get_TenNguoiDung(User.Identity.Name);
                    resultConfirm.ResponseDateString = DateTime.Now;
                    error = _sendService.KetQuaXuLy(hoSo.sMaHoSo, resultConfirm, "09");
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
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hoSo.iID_MaTrangThai);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                    bang.Save();

                    clLichSuHoSo.InsertLichSu(hoSo.iID_MaHoSo, User.Identity.Name, (int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.GuiDoanhNghiepThongBaoYeuCauBoSungHoSo, "Gửi doanh nghiệp thông báo yêu cầu bổ sung hồ sơ", "", hoSo.iID_MaTrangThaiTruoc, trangThaiTiepTheo.iID_MaTrangThai);

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
        /// <summary>
        /// update data màn hình TuChoiHoSo
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="MaHoSo"></param>
        /// <returns></returns>
        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult TuChoiHoSoSubmit()
        {
            String sUserName = User.Identity.Name;
            String sIP = Request.UserHostAddress;

            ResultModels result = new ResultModels();

            String ParentID = "TC";
            NameValueCollection values = new NameValueCollection();
            HttpFileCollectionBase files = Request.Files;
            string _sNoiDung = CString.SafeString(Request.Form[ParentID + "_sNoiDung"]);
            string iID_MaHoSo = CString.SafeString(Request.Form[ParentID + "_iID_MaHoSo"]);
            if (string.IsNullOrEmpty(iID_MaHoSo))
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
                base.ViewData["DuLieuMoi"] = "0";
                ViewData["iID_MaHoSo"] = CString.SafeString(iID_MaHoSo);

                //TempData["msg"] = "error";

                result.success = false;
                result.value = Url.Action("Index");
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            HoSoModels hoSo = clHoSo.GetHoSoById(Convert.ToInt64(iID_MaHoSo));

            long iID_MaDinhKem = -1;
            string sFileTemp = "", sFileName = "";
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase postedFile = files[i];
                if (postedFile != null && postedFile.ContentLength>0)
                {
                    if (CommonFunction.FileUploadCheck(postedFile))
                    {
                        sFileName = postedFile.FileName;

                        ResDinhKemFiles resDinhKemFile = new ResDinhKemFiles();
                        resDinhKemFile = CDinhKemFiles.UploadFile(postedFile);
                        if (resDinhKemFile != null && resDinhKemFile.ItemId > 0)
                        {
                            sFileTemp = resDinhKemFile.UrlFile;

                            //Luu vao bang Dinh Kem
                            iID_MaDinhKem = CDinhKem.ThemDinhKem(hoSo.iID_MaHoSo, 0, 22, "0", hoSo.sMaHoSo, "File từ chối hồ sơ bổ sung Phòng TACN.", sFileName, "", null, 1, sFileTemp, sUserName, sIP, resDinhKemFile.ItemId);
                        }
                    }
                }
            }
            
            TrangThaiModels trangThaiTiepTheo= clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.TraChuyenVienXuLyLai, hoSo.iID_MaTrangThai, hoSo.iID_MaTrangThaiTruoc);

            string error = "";
            if(CHamRieng.iNSW == 1)
            {
                KetQuaXuLy resultConfirm = new KetQuaXuLy();
                resultConfirm.NSWFileCode = hoSo.sMaHoSo;
                resultConfirm.Reason = _sNoiDung;
                resultConfirm.AttachmentId = iID_MaDinhKem.ToString(); ;
                resultConfirm.FileName = sFileName;
                resultConfirm.FileLink = sFileTemp;
                resultConfirm.NameOfStaff = CPQ_NGUOIDUNG.Get_TenNguoiDung(User.Identity.Name);
                resultConfirm.ResponseDateString = DateTime.Now;
                error = _sendService.KetQuaXuLy(hoSo.sMaHoSo, resultConfirm, "08");
            }
            else
            {
                error = "99";
            }

            if (error.Equals("99"))
            {
                bang.MaNguoiDungSua = sUserName;
                bang.IPSua = sIP;
                bang.TruyenGiaTri(ParentID, Request.Form);
                bang.DuLieuMoi = false;
                bang.GiaTriKhoa = iID_MaHoSo;
                bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy);
                bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hoSo.iID_MaTrangThaiTruoc);
                bang.Save();

                clLichSuHoSo.InsertLichSu(hoSo.iID_MaHoSo, User.Identity.Name, (int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.TraChuyenVienXuLyLai, _sNoiDung, sFileTemp, hoSo.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);

                result.success = true;
            }
            else
            {
                result.success = false;
            }   
            
            clHoSo.CleanNguoiXem(iID_MaHoSo);
            
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
                LoaiDanhSach = 4,
                sMaHoSo = _sMaHoSo,
                sTenDoanhNghiep = _sTenDoanhNghiep,
                sTenTACN = _sTenTACN,
                TuNgayDen = _TuNgayDen,
                DenNgayDen = _DenNgayDen,
                TuNgayTiepNhan= TuNgayTiepNhan,
                DenNgayTiepNhan = DenNgayTiepNhan,
                sSoTiepNhan= sSoTiepNhan
            };

            TempData["menu"] = 231;
            TempData["msg"] = "success";
            return RedirectToAction("Index", models);
        }
    }
}