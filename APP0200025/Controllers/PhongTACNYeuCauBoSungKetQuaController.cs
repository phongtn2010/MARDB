using System;
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
using System.Data;

namespace APP0200025.Controllers
{
    public class PhongTACNYeuCauBoSungKetQuaController : Controller
    {
        // GET: PhongTACNYeuCauBoSunghangHoa
        Bang bang = new Bang("CNN25_HangHoa");

        private SendService _sendService = new SendService();

        // GET: ChoTiepNhanhangHoa
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
                    LoaiDanhSach = (int)clLoaiDanhSach.From.PhongTACNYeuCauBoSungKetQua,
                    Page = 1,
                    PageSize = Globals.PageSize
                };
            }

            ViewData["menu"] = 237;
            return View(models);
        }
        public ActionResult Detail(string iID_MaHangHoa)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, bang.TenBang, "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            clHangHoa.UpdateNguoiXem(iID_MaHangHoa, User.Identity.Name);
            
            HangHoaModels models = clHangHoa.GetHangHoaById(Convert.ToInt32(iID_MaHangHoa));

            ViewData["menu"] = 237;
            return View(models);
        }

        public ActionResult GuiDoanhNghiep(String iID_MaHangHoa)
        {
            if (String.IsNullOrEmpty(iID_MaHangHoa) == false)
            {
                HangHoaModels hanghoa = clHangHoa.GetHangHoaById(Convert.ToInt64(iID_MaHangHoa));
                TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.GuiDNThongBaoYeuCauBoSungKetQua, hanghoa.iID_MaTrangThai, hanghoa.iID_MaTrangThaiTruoc);

                long iID_MaLichSu = 0;
                String sNoiDungLichSu = "", sFileLichSu = "";
                DataTable dtLS = clLichSuHangHoa.GetDataTableBoSungTuChoi(hanghoa.iID_MaHangHoa, hanghoa.iID_MaTrangThai);
                if (dtLS.Rows.Count > 0)
                {
                    iID_MaLichSu = Convert.ToInt64(dtLS.Rows[0]["id"]);
                    sNoiDungLichSu = Convert.ToString(dtLS.Rows[0]["sNoiDung"]);
                    sFileLichSu = Convert.ToString(dtLS.Rows[0]["sFile"]);
                }
                dtLS.Dispose();

                //XML(18, 21)
                XuLyKetQua resultConfirm = new XuLyKetQua();
                resultConfirm.NSWFileCode = hanghoa.sMaHoSo;
                resultConfirm.Reason = sNoiDungLichSu;
                resultConfirm.GoodsId = hanghoa.iID_MaHangHoa;
                resultConfirm.NameOfGoods = hanghoa.sTenHangHoa;
                resultConfirm.AttachmentId = "";
                resultConfirm.FileName = "File Yêu Cầu";
                resultConfirm.FileLink = sFileLichSu;
                resultConfirm.NameOfStaff = CPQ_NGUOIDUNG.Get_TenNguoiDung(User.Identity.Name);
                resultConfirm.ResponseDateString = DateTime.Now;
                string error = _sendService.XuLyKetQua(hanghoa.sMaHoSo, resultConfirm, "21");

                if (error == "99")
                {
                    bang.MaNguoiDungSua = User.Identity.Name;
                    bang.IPSua = Request.UserHostAddress;
                    bang.DuLieuMoi = false;
                    bang.GiaTriKhoa = iID_MaHangHoa;
                    //bang.TruyenGiaTri(ParentID, Request.Form);
                    bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy ?? "");
                    bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hanghoa.iID_MaTrangThai);
                    bang.Save();

                    clLichSuHangHoa.InsertLichSu(hanghoa.iID_MaHangHoa, User.Identity.Name, (int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.GuiDNThongBaoYeuCauBoSungKetQua, "Gửi doanh nghiệp thông báo yêu cầu bổ sung kết quả", "", hanghoa.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);

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

            clHangHoa.CleanNguoiXem(iID_MaHangHoa);

            return RedirectToAction("Index");
        }
        /// <summary>
        /// update data màn hình TiepNhanhangHoa
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="MahangHoa"></param>
        /// <returns></returns>
        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult GuiDoanhNghiepSubmit(String ParentID)
        {
            string iID_MaHangHoa = CString.SafeString(Request.Form[ParentID + "_iID_MaHangHoa"]);
            if (String.IsNullOrEmpty(iID_MaHangHoa) == false)
            {
                HangHoaModels hanghoa = clHangHoa.GetHangHoaById(Convert.ToInt64(iID_MaHangHoa));
                TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.GuiDNThongBaoYeuCauBoSungKetQua, hanghoa.iID_MaTrangThai, hanghoa.iID_MaTrangThaiTruoc);

                long iID_MaLichSu = 0;
                String sNoiDungLichSu = "", sFileLichSu = "";
                DataTable dtLS = clLichSuHangHoa.GetDataTableBoSungTuChoi(hanghoa.iID_MaHangHoa, hanghoa.iID_MaTrangThai);
                if (dtLS.Rows.Count > 0)
                {
                    iID_MaLichSu = Convert.ToInt64(dtLS.Rows[0]["id"]);
                    sNoiDungLichSu = Convert.ToString(dtLS.Rows[0]["sNoiDung"]);
                    sFileLichSu = Convert.ToString(dtLS.Rows[0]["sFile"]);
                }
                dtLS.Dispose();

                //XML(18, 21)
                XuLyKetQua resultConfirm = new XuLyKetQua();
                resultConfirm.NSWFileCode = hanghoa.sMaHoSo;
                resultConfirm.Reason = sNoiDungLichSu;
                resultConfirm.GoodsId = hanghoa.iID_MaHangHoa;
                resultConfirm.NameOfGoods = hanghoa.sTenHangHoa;
                resultConfirm.AttachmentId = "";
                resultConfirm.FileName = "File Yêu Cầu";
                resultConfirm.FileLink = sFileLichSu;
                resultConfirm.NameOfStaff = CPQ_NGUOIDUNG.Get_TenNguoiDung(User.Identity.Name);
                resultConfirm.ResponseDateString = DateTime.Now;
                string error = _sendService.XuLyKetQua(hanghoa.sMaHoSo, resultConfirm, "21");

                if (error == "99")
                {
                    bang.MaNguoiDungSua = User.Identity.Name;
                    bang.IPSua = Request.UserHostAddress;
                    bang.DuLieuMoi = false;
                    bang.GiaTriKhoa = iID_MaHangHoa;
                    //bang.TruyenGiaTri(ParentID, Request.Form);
                    bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy ?? "");
                    bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hanghoa.iID_MaTrangThai);
                    bang.Save();

                    clLichSuHangHoa.InsertLichSu(hanghoa.iID_MaHangHoa, User.Identity.Name, (int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.GuiDNThongBaoYeuCauBoSungKetQua, "Gửi doanh nghiệp thông báo yêu cầu bổ sung kết quả", "", hanghoa.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);

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

            clHangHoa.CleanNguoiXem(iID_MaHangHoa);

            return RedirectToAction("Index");
        }
        /// <summary>
        /// update data màn hình TuChoihangHoa
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="MahangHoa"></param>
        /// <returns></returns>
        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult TuChoiSubmit()
        {
            ResultModels result = new ResultModels();

            String sUserName = User.Identity.Name;
            String sIP = Request.UserHostAddress;

            String ParentID = "TC";

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
                    if (CommonFunction.FileUploadCheck(postedFile))
                    {
                        sFileName = postedFile.FileName;

                        ResDinhKemFiles resDinhKemFile = new ResDinhKemFiles();
                        resDinhKemFile = CDinhKemFiles.UploadFile(postedFile);
                        if (resDinhKemFile != null && resDinhKemFile.ItemId > 0)
                        {
                            sFileTemp = resDinhKemFile.UrlFile;

                            //Luu vao bang Dinh Kem
                            iID_MaDinhKem = CDinhKem.ThemDinhKem(hangHoa.iID_MaHoSo, hangHoa.iID_MaHangHoa, 27, "0", hangHoa.sMaHoSo, "File từ chối XCNL bổ sung BPMC Phòng TACN", sFileName, "", null, 1, sFileTemp, sUserName, sIP, resDinhKemFile.ItemId);
                        }
                    }
                }
            }

            TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.TraChuyenVienXuLyLai, hangHoa.iID_MaTrangThai, hangHoa.iID_MaTrangThaiTruoc);
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
            
            clLichSuHangHoa.InsertLichSu(hangHoa.iID_MaHangHoa, User.Identity.Name, (int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.TraChuyenVienXuLyLai, _sNoiDung, sFileTemp, hangHoa.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);
           
            clHangHoa.CleanNguoiXem(iID_MaHangHoa);

            result.success = true;
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
            string _sSoTiepNhan = CString.SafeString(Request.Form[ParentID + "_sSoTiepNhan"]);
            string _sSoGDK = CString.SafeString(Request.Form[ParentID + "_sSoGDK"]);
            string _TuNgayTiepNhan = CString.SafeString(Request.Form[ParentID + "_viTuNgayTiepNhan"]);
            string _DenNgayTiepNhan = CString.SafeString(Request.Form[ParentID + "_viDenNgayTiepNhan"]);
            string _TuNgayXacNhan = CString.SafeString(Request.Form[ParentID + "_viTuNgayXacNhan"]);
            string _DenNgayXacNhan = CString.SafeString(Request.Form[ParentID + "_viDenNgayXacNhan"]);
            string _iTrangThai = CString.SafeString(Request.Form[ParentID + "_iID_MaTrangThai"]);
            sHoSoModels models = new sHoSoModels
            {
                LoaiDanhSach = (int)clLoaiDanhSach.From.PhongTACNYeuCauBoSungKetQua,
                iID_MaTrangThai = Convert.ToInt32(_iTrangThai),
                sMaHoSo = _sMaHoSo,
                sTenDoanhNghiep = _sTenDoanhNghiep,
                sTenTACN = _sTenTACN,
                sSoTiepNhan = _sSoTiepNhan,
                sSoGDK = _sSoGDK,
                TuNgayTiepNhan = _TuNgayTiepNhan,
                DenNgayTiepNhan = _DenNgayTiepNhan,
                TuNgayXacNhan = _TuNgayXacNhan,
                DenNgayXacNhan = _DenNgayXacNhan
            };

            TempData["menu"] = 235;
            TempData["msg"] = "success";
            return RedirectToAction("Index", models);
        }
    }
}