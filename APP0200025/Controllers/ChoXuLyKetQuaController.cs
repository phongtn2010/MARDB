using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using DomainModel;
using DomainModel.Abstract;
using DATA0200025;
using DATA0200025.Models;
using DATA0200025.SearchModels;
using APP0200025.Models;

namespace APP0200025.Controllers
{
    public class ChoXuLyKetQuaController : Controller
    {
        // GET: ChoXuLyKetQua
        Bang bang = new Bang("CNN25_HangHoa");
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
                    LoaiDanhSach = (int)clLoaiDanhSach.From.ChoXuLyKetQua,
                    iID_MaLoaiHoSo = 3,//Chuyen viên chỉ xử lý hồ sơ 2c
                    Page = 1,
                    PageSize = Globals.PageSize
                };
            }

            ViewData["menu"] = 246;
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

            ViewData["menu"] = 246;
            return View(models);
        }
        public ActionResult SoanThongBaoKetQua(string iID_MaHangHoa)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, bang.TenBang, "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["DuLieuMoi"] = "0";
            ViewData["smenu"] = 187;
            HangHoaModels models = clHangHoa.GetHangHoaById(Convert.ToInt32(iID_MaHangHoa));

            return View(models);
        }
        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult EditSubmit(String ParentID)
        {
            string iID_MaHangHoa = Request.Form[ParentID + "_iID_MaHangHoa"];
            string _sGhiChu = Request.Form[ParentID + "_sGhiChu"];
            if(String.IsNullOrEmpty(iID_MaHangHoa) == false)
            {
                HangHoaModels hanghoa = clHangHoa.GetHangHoaById(Convert.ToInt64(iID_MaHangHoa));

                TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.TiepNhanKetQuaKiemTra, hanghoa.iID_MaTrangThai, hanghoa.iID_MaTrangThaiTruoc);

                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                //bang.TruyenGiaTri(ParentID, Request.Form);
                bang.DuLieuMoi = false;
                bang.GiaTriKhoa = iID_MaHangHoa;
                bang.CmdParams.Parameters.AddWithValue("@sGhiChu", _sGhiChu);
                bang.Save();

                TempData["msg"] = "success";

                clHangHoa.CleanNguoiXem(iID_MaHangHoa);
            }    
            else
            {
                TempData["msg"] = "error";
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// update data màn hình TiepNhanHoSo
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="MaHoSo"></param>
        /// <returns></returns>
        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult ThuHoi(String iID_MaHangHoa)
        {
            ResultModels result = new ResultModels();

            String sUserName = User.Identity.Name;
            String sIP = Request.UserHostAddress;

            if (String.IsNullOrEmpty(iID_MaHangHoa) == false)
            {
                HangHoaModels hanghoa = clHangHoa.GetHangHoaById(Convert.ToInt64(iID_MaHangHoa));

                TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.ChuyenVien, (int)clHanhDong.HanhDong.ThuHoiVaXuLyLai, hanghoa.iID_MaTrangThai, hanghoa.iID_MaTrangThaiTruoc);
                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                bang.DuLieuMoi = false;
                bang.GiaTriKhoa = iID_MaHangHoa;
                bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy);
                bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hanghoa.iID_MaTrangThai);
                bang.Save();
                
                clLichSuHangHoa.InsertLichSu(hanghoa.iID_MaHangHoa, User.Identity.Name, (int)clDoiTuong.DoiTuong.ChuyenVien, (int)clHanhDong.HanhDong.ThuHoiVaXuLyLai, "Thu hồi và xử lý lại", "", hanghoa.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);
                
                result.success = true;
            }
            else
            {
                result.success = false;
            }

            clHoSo.CleanNguoiXem(iID_MaHangHoa);

            result.value = Url.Action("Index");
            return Json(result, JsonRequestBehavior.AllowGet);
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
            ResultModels result = new ResultModels();

            String sUserName = User.Identity.Name;
            String sIP = Request.UserHostAddress;

            String ParentID = "BS";
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

                base.ViewData["DuLieuMoi"] = "0";
                ViewData["iID_MaHangHoa"] = CString.SafeString(iID_MaHangHoa);

                result.success = false;
                result.value = Url.Action("Index");
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            try
            {
                HangHoaModels hangHoa = clHangHoa.GetHangHoaById(Convert.ToInt32(iID_MaHangHoa));

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
                                iID_MaDinhKem = CDinhKem.ThemDinhKem(hangHoa.iID_MaHoSo, hangHoa.iID_MaHangHoa, 51, "0", hangHoa.sMaHoSo, "File bổ sung XNCL Chuyên Viên.", sFileName, "", null, 1, sFileTemp, sUserName, sIP, resDinhKemFile.ItemId);
                            }
                        }
                    }
                }

                TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.ChuyenVien, (int)clHanhDong.HanhDong.YeuCauBoSungHoSo, hangHoa.iID_MaTrangThai, hangHoa.iID_MaTrangThaiTruoc);

                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                //bang.TruyenGiaTri(ParentID, Request.Form);
                bang.DuLieuMoi = false;
                bang.GiaTriKhoa = iID_MaHangHoa;
                bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy);
                bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hangHoa.iID_MaTrangThai);
                bang.Save();

                clLichSuHangHoa.InsertLichSu(hangHoa.iID_MaHangHoa, User.Identity.Name, (int)clDoiTuong.DoiTuong.ChuyenVien, (int)clHanhDong.HanhDong.YeuCauBoSungHoSo, _sNoiDung, sFileTemp, hangHoa.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);

                result.success = true;
            }
            catch(Exception ex)
            {
                result.success = false;
            }

            clHangHoa.CleanNguoiXem(iID_MaHangHoa);

            result.value = Url.Action("Index");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult TrinhLanhDao(String iID_MaHangHoa)
        {
            ResultModels result = new ResultModels();

            if (String.IsNullOrEmpty(iID_MaHangHoa) == false)
            {
                HangHoaModels hangHoa = clHangHoa.GetHangHoaById(Convert.ToInt64(iID_MaHangHoa));
                TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.ChuyenVien, (int)clHanhDong.HanhDong.SoanThongBaoKetQuaKiemTra, hangHoa.iID_MaTrangThai, hangHoa.iID_MaTrangThaiTruoc);

                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                bang.DuLieuMoi = false;
                bang.GiaTriKhoa = iID_MaHangHoa;
                bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy);
                bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hangHoa.iID_MaTrangThai);
                bang.Save();
                
                clLichSuHangHoa.InsertLichSu(hangHoa.iID_MaHangHoa, User.Identity.Name, (int)clDoiTuong.DoiTuong.ChuyenVien, (int)clHanhDong.HanhDong.SoanThongBaoKetQuaKiemTra, "Soạn thông báo kết quả kiểm tra", "", hangHoa.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);

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
            string _sSoTiepNhan = CString.SafeString(Request.Form[ParentID + "_sSoTiepNhan"]);
            string _sSoGDK = CString.SafeString(Request.Form[ParentID + "_sSoGDK"]);
            string _TuNgayDen = CString.SafeString(Request.Form[ParentID + "_viTuNgayDen"]);
            string _DenNgayDen = CString.SafeString(Request.Form[ParentID + "_viDenNgayDen"]);
            string _TuNgayTiepNhan = CString.SafeString(Request.Form[ParentID + "_viTuNgayTiepNhan"]);
            string _DenNgayTiepNhan = CString.SafeString(Request.Form[ParentID + "_viDenNgayTiepNhan"]);

            sHoSoModels models = new sHoSoModels
            {
                LoaiDanhSach = (int)clLoaiDanhSach.From.ChoXuLyKetQua,
                iID_MaLoaiHoSo = 3,//Chuyen viên chỉ xử lý hồ sơ 2c
                sMaHoSo = _sMaHoSo,
                sTenDoanhNghiep = _sTenDoanhNghiep,
                sTenTACN = _sTenTACN,
                sSoTiepNhan = _sSoTiepNhan,
                sSoGDK = _sSoGDK,
                TuNgayDen = _TuNgayDen,
                DenNgayDen = _DenNgayDen,
                TuNgayTiepNhan = _TuNgayTiepNhan,
                DenNgayTiepNhan = _DenNgayTiepNhan
            };

            TempData["menu"] = 246;
            TempData["msg"] = "success";
            return RedirectToAction("Index", models);
        }
    }
}