
using DATA0200025;
using DATA0200025.Models;
using DATA0200025.SearchModels;
using DATA0200025.WebServices;
using DATA0200025.WebServices.XmlType.Request;
using DomainModel;
using DomainModel.Abstract;
using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;

namespace APP0200025.Controllers
{
    public class HoSoYeuCauBoSungController : Controller
    {
        private SendService _sendService = new SendService();

        // GET: HoSoDaGuiBoSung
        Bang bang = new Bang("CNN25_HoSo");

        private string ViewPath = "~/Views/HoSoYeuCauBoSung/";
        // GET: ChoTiepNhanHoSo
        public ActionResult Index(sHoSoModels models)
        {
            if (models == null || models.LoaiDanhSach == 0)
            {
                models = new sHoSoModels
                {
                    LoaiDanhSach = 2,
                    Page = 1,
                    PageSize = Globals.PageSize
                };
            }
            ViewData["menu"] = 230;
            return View(models);
        }
        public ActionResult Detail(string iID_MaHoSo)
        {
            //if (BaoMat.ChoPhepLamViec(User.Identity.Name, bang.TenBang, "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            //{
            //    return RedirectToAction("Index", "PermitionMessage");
            //}
            clHoSo.UpdateNguoiXem(iID_MaHoSo, User.Identity.Name);
            ViewData["menu"] = 230;
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
            string iID_MaHoSo = CString.SafeString(Request.Form[ParentID + "_iID_MaHoSo"]);
            if(String.IsNullOrEmpty(iID_MaHoSo) == false)
            {
                HoSoModels hoSo = clHoSo.GetHoSoById(Convert.ToInt32(iID_MaHoSo));

                TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.TiepNhanHoSo, hoSo.iID_MaTrangThai, hoSo.iID_MaTrangThaiTruoc);

                //Gui sang NSW
                KetQuaXuLy resultConfirm = new KetQuaXuLy();
                resultConfirm.NSWFileCode = hoSo.sMaHoSo;
                resultConfirm.Reason = "Đã tiếp nhận hồ sơ";
                resultConfirm.AttachmentId = "";
                resultConfirm.FileName = "";
                resultConfirm.FileLink = "";
                resultConfirm.NameOfStaff = CPQ_NGUOIDUNG.Get_TenNguoiDung(User.Identity.Name);
                resultConfirm.ResponseDate = DateTime.Now;
                string error = _sendService.KetQuaXuLy(hoSo.sMaHoSo, resultConfirm, "06");

                if (error == "99")
                {
                    bang.MaNguoiDungSua = User.Identity.Name;
                    bang.IPSua = Request.UserHostAddress;
                    bang.DuLieuMoi = false;
                    bang.TruyenGiaTri(ParentID, Request.Form);
                    bang.CmdParams.Parameters.AddWithValue("@sSoTiepNhan", clTaoMaTiepNhan.GetSoTiepNhan());
                    bang.CmdParams.Parameters.AddWithValue("@sUserTiepNhan", User.Identity.Name);
                    bang.CmdParams.Parameters.AddWithValue("@dNgayTiepNhan", DateTime.Now);
                    bang.CmdParams.Parameters.AddWithValue("@sTenNguoiTiepNhan", CPQ_NGUOIDUNG.Get_TenNguoiDung(User.Identity.Name));
                    bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy);
                    bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hoSo.iID_MaTrangThai);
                    bang.Save();

                    clLichSuHoSo.InsertLichSu(hoSo.iID_MaHoSo, User.Identity.Name, (int)clDoiTuong.DoiTuong.LanhDaoCuc, (int)clHanhDong.HanhDong.TiepNhanHoSo, "Tiếp nhận hồ sơ", "", hoSo.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);

                    TempData["msg"] = "success";
                }
                else
                {
                    //clLichSuHoSo.InsertLichSu(hoSo.iID_MaHoSo, User.Identity.Name, (int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.TiepNhanHoSo, "Tiếp nhận hồ sơ", "", hoSo.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);

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
        public ActionResult TiepNhanHoSoSubmit(String iID_MaHoSo)
        {
            ResultModels result = new ResultModels();
            if (String.IsNullOrEmpty(iID_MaHoSo) == false)
            {
                HoSoModels hoSo = clHoSo.GetHoSoById(Convert.ToInt32(iID_MaHoSo));

                TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.TiepNhanHoSo, hoSo.iID_MaTrangThai, hoSo.iID_MaTrangThaiTruoc);

                //Gui sang NSW
                KetQuaXuLy resultConfirm = new KetQuaXuLy();
                resultConfirm.NSWFileCode = hoSo.sMaHoSo;
                resultConfirm.Reason = "Đã tiếp nhận hồ sơ";
                resultConfirm.AttachmentId = "";
                resultConfirm.FileName = "";
                resultConfirm.FileLink = "";
                resultConfirm.NameOfStaff = CPQ_NGUOIDUNG.Get_TenNguoiDung(User.Identity.Name);
                resultConfirm.ResponseDate = DateTime.Now;
                string error = _sendService.KetQuaXuLy(hoSo.sMaHoSo, resultConfirm, "06");

                if (error == "99")
                {
                    bang.MaNguoiDungSua = User.Identity.Name;
                    bang.IPSua = Request.UserHostAddress;
                    bang.DuLieuMoi = false;
                    bang.GiaTriKhoa = iID_MaHoSo;
                    bang.CmdParams.Parameters.AddWithValue("@sSoTiepNhan", clTaoMaTiepNhan.GetSoTiepNhan());
                    bang.CmdParams.Parameters.AddWithValue("@sUserTiepNhan", User.Identity.Name);
                    bang.CmdParams.Parameters.AddWithValue("@dNgayTiepNhan", DateTime.Now);
                    bang.CmdParams.Parameters.AddWithValue("@sTenNguoiTiepNhan", CPQ_NGUOIDUNG.Get_TenNguoiDung(User.Identity.Name));
                    bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy);
                    bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hoSo.iID_MaTrangThai);
                    bang.Save();

                    clLichSuHoSo.InsertLichSu(hoSo.iID_MaHoSo, User.Identity.Name, (int)clDoiTuong.DoiTuong.LanhDaoCuc, (int)clHanhDong.HanhDong.TiepNhanHoSo, "Tiếp nhận hồ sơ", "", hoSo.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);

                    TempData["msg"] = "success";
                    result.success = true;
                }
                else
                {
                    //clLichSuHoSo.InsertLichSu(hoSo.iID_MaHoSo, User.Identity.Name, (int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.TiepNhanHoSo, "Tiếp nhận hồ sơ", "", hoSo.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);

                    TempData["msg"] = "error";
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
        /// update data màn hình YeuCauBoXung
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="MaHoSo"></param>
        /// <returns></returns>
        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult YeuCauBoSungSubmit()
        {
            ResultModels result = new ResultModels();

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
            string sFileTemp = "", sFileName = "";
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase postedFile = files[i];
                if (postedFile != null)
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
            HoSoModels hoSo = clHoSo.GetHoSoById(Convert.ToInt32(iID_MaHoSo));
            TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.YeuCauBoSungHoSo, hoSo.iID_MaTrangThai, hoSo.iID_MaTrangThaiTruoc);

            KetQuaXuLy resultConfirm = new KetQuaXuLy();
            resultConfirm.NSWFileCode = hoSo.sMaHoSo;
            resultConfirm.Reason = "Yêu cầu bổ sung";
            resultConfirm.AttachmentId = "";
            resultConfirm.FileName = sFileName;
            resultConfirm.FileLink = string.Format("{0}{1}", clCommon.BNN_Url, sFileTemp);
            resultConfirm.NameOfStaff = CPQ_NGUOIDUNG.Get_TenNguoiDung(User.Identity.Name);
            resultConfirm.ResponseDate = DateTime.Now;
            string error = _sendService.KetQuaXuLy(hoSo.sMaHoSo, resultConfirm, "07");
            if (error.Equals("99"))
            {
                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                bang.TruyenGiaTri(ParentID, Request.Form);
                bang.DuLieuMoi = false;
                bang.GiaTriKhoa = iID_MaHoSo;
                bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy);
                bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hoSo.iID_MaTrangThaiTruoc);
                bang.Save();
                clLichSuHoSo.InsertLichSu(hoSo.iID_MaHoSo, User.Identity.Name, (int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.YeuCauBoSungHoSo, _sNoiDung, sFileTemp, hoSo.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);
            }

            clHoSo.CleanNguoiXem(iID_MaHoSo);

            result.success = true;
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
            ResultModels result = new ResultModels();

            String ParentID = "TC";
            NameValueCollection values = new NameValueCollection();
            HttpFileCollectionBase files = Request.Files;
            string _sNoiDung = CString.SafeString(Request.Form[ParentID + "_sNoiDung"]);
            string iID_MaHoSo = CString.SafeString(Request.Form[ParentID + "_iID_MaHoSo"]);
            if (string.IsNullOrEmpty(_sNoiDung))
            {
                values.Add("err_sNoiDung", "Bạn nhập nội dung từ chối");
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
            TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.TuChoiHoSo, hoSo.iID_MaTrangThai, hoSo.iID_MaTrangThaiTruoc);

            KetQuaXuLy resultConfirm = new KetQuaXuLy();
            resultConfirm.NSWFileCode = hoSo.sMaHoSo;
            resultConfirm.Reason = "Từ chối hồ sơ";
            resultConfirm.AttachmentId = "01";
            resultConfirm.FileName = "";
            resultConfirm.FileLink = "";
            resultConfirm.NameOfStaff = CPQ_NGUOIDUNG.Get_TenNguoiDung(User.Identity.Name);
            resultConfirm.ResponseDate = DateTime.Now;
            string error = _sendService.KetQuaXuLy(hoSo.sMaHoSo, resultConfirm, "08");
            if (error.Equals("99"))
            {
                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                bang.TruyenGiaTri(ParentID, Request.Form);
                bang.DuLieuMoi = false;
                bang.GiaTriKhoa = iID_MaHoSo;
                bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy);
                bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hoSo.iID_MaTrangThaiTruoc);
                bang.Save();
                clLichSuHoSo.InsertLichSu(hoSo.iID_MaHoSo, User.Identity.Name, (int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.TuChoiHoSo, _sNoiDung, sFileTemp, hoSo.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);
            }

            clHoSo.CleanNguoiXem(iID_MaHoSo);
                        
            result.success = true;
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
            string iID_MaTrangThai = CString.SafeString(Request.Form[ParentID + "_iID_MaTrangThai"]);
            sHoSoModels models = new sHoSoModels
            {
                LoaiDanhSach = 2,
                sMaHoSo = _sMaHoSo,
                iID_MaTrangThai= Convert.ToInt32(iID_MaTrangThai),
                sTenDoanhNghiep = _sTenDoanhNghiep,
                sTenTACN = _sTenTACN,
                TuNgayDen = _TuNgayDen,
                DenNgayDen = _DenNgayDen,
                TuNgayTiepNhan= TuNgayTiepNhan,
                DenNgayTiepNhan = DenNgayTiepNhan
            };

            TempData["menu"] = 230;
            TempData["msg"] = "success";
            return RedirectToAction("Index", models);
        }
    }
}