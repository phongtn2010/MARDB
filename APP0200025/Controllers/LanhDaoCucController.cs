using APP0200025.Models;
using DATA0200025;
using DATA0200025.Models;
using DATA0200025.SearchModels;
using DomainModel;
using DomainModel.Abstract;
using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Web;
using System.Web.Mvc;

namespace APP0200025.Controllers
{
    public class LanhDaoCucController : Controller
    {
        Bang bang = new Bang("CNN25_HoSo");
        Bang bangHH = new Bang("CNN25_HangHoa");
        private string ViewPath = "~/Views/LanhDaoCuc/";
        // GET: 
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
                    Page = 1,
                    PageSize = Globals.PageSize,
                    LoaiDanhSach = 12
                };
            }
            return View(models);
        }
        /// <summary>
        /// view màn hình TiepNhanHoSo
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Detail(string iID_MaHoSo)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, bang.TenBang, "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            HoSoModels hoSo = clHoSo.GetHoSoById(iID_MaHoSo);
            ViewData["DuLieuMoi"] = "0";

            ViewData["menu"] = 214;
            return View(hoSo);
        }

        /// <summary>
        /// update data màn hình TiepNhanHoSo
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="MaHoSo"></param>
        /// <returns></returns>
        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult Duyet(String ParentID)
        {
            string iID_MaHoSo = CString.SafeString(Request.Form[ParentID + "_iID_MaHoSo"]);
            string sKySo = Convert.ToString(Request.Form[ParentID + "_iID_Sign"]);
            if (String.IsNullOrEmpty(iID_MaHoSo) == false)
            {
                HoSoModels hoSo = clHoSo.GetHoSoById(Convert.ToInt64(iID_MaHoSo));
                string sNoiDung = "";
                int HanhDong = 0;
                switch (hoSo.iID_MaTrangThai)
                {
                    case 17:
                        HanhDong = (int)clHanhDong.HanhDong.DongYPheDuyetTuChoiCapGDK;
                        sNoiDung = "Đồng ý phê duyệt từ chối cấp GĐK";
                        break;
                    case 22:
                        HanhDong = (int)clHanhDong.HanhDong.DongYXacNhanGDK;
                        break;
                }
                TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.LanhDaoCuc, HanhDong, hoSo.iID_MaTrangThai, hoSo.iID_MaTrangThaiTruoc);

                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                bang.TruyenGiaTri(ParentID, Request.Form);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy);
                bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hoSo.iID_MaTrangThai);
                bang.CmdParams.Parameters.AddWithValue("@sHashCode", sKySo);
                if (hoSo.iID_MaTrangThai == 22)
                {
                    clTaoSoGDK taoSoGDK = clTaoSoGDK.GetSoGDK();
                    bang.CmdParams.Parameters.AddWithValue("@sSoGDK", taoSoGDK.SoGDK);
                    bang.CmdParams.Parameters.AddWithValue("@dNgayKyGDK", DateTime.Now);
                    bang.CmdParams.Parameters.AddWithValue("@dNgayXacNhan", DateTime.Now);
                    bang.CmdParams.Parameters.AddWithValue("@sNguoiKyGDK", eCoQuanXuLy.sNguoiKy_Ten);
                    sNoiDung = "Đồng ý xác nhận GĐK số:" + taoSoGDK.SoGDK;
                }
                bang.Save();

                clLichSuHoSo.InsertLichSu(hoSo.iID_MaHoSo, User.Identity.Name, (int)clDoiTuong.DoiTuong.LanhDaoCuc, HanhDong, sNoiDung, "", hoSo.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);

                TempData["msg"] = "success";

                clHoSo.CleanNguoiXem(iID_MaHoSo);
            }
            else
            {
                TempData["msg"] = "error";
            }   

            return RedirectToAction("HoSoChoXacNhanGDK");
        }

        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult DuyetSubmit(string ParentID)
        {
            string sNoiDung = "";
            string s_HoSo = Request.Form[ParentID + "_HO_SO"];
            if (!string.IsNullOrEmpty(s_HoSo))
            {
                string[] arr = s_HoSo.Split(',');
                for (int i = 0; i < arr.Length; i++)
                {
                    string iID_MaHoSo = arr[i];
                    HoSoModels hoSo = clHoSo.GetHoSoById(Convert.ToInt64(iID_MaHoSo));

                    if(hoSo != null)
                    {
                        Bang bangAll = new Bang("CNN25_HoSo");

                        int HanhDong = 0;
                        switch (hoSo.iID_MaTrangThai)
                        {
                            case 17:
                                HanhDong = (int)clHanhDong.HanhDong.DongYPheDuyetTuChoiCapGDK;
                                sNoiDung = "Đồng ý phê duyệt từ chối cấp GĐK";
                                break;
                            case 22:
                                HanhDong = (int)clHanhDong.HanhDong.DongYXacNhanGDK;

                                break;
                        }
                        TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.LanhDaoCuc, HanhDong, hoSo.iID_MaTrangThai, hoSo.iID_MaTrangThaiTruoc);

                        if (hoSo.iID_MaTrangThai == 22)
                        {
                            clTaoSoGDK taoSoGDK = clTaoSoGDK.GetSoGDK();
                            if (bangAll.CmdParams.Parameters.IndexOf("@sSoGDK") >= 0)
                                bangAll.CmdParams.Parameters["@sSoGDK"].Value = taoSoGDK.SoGDK;
                            else
                                bangAll.CmdParams.Parameters.AddWithValue("@sSoGDK", taoSoGDK.SoGDK);

                            if (bangAll.CmdParams.Parameters.IndexOf("@dNgayKyGDK") >= 0)
                            {
                                bangAll.CmdParams.Parameters["@dNgayKyGDK"].Value = DateTime.Now;
                                bangAll.CmdParams.Parameters["@dNgayXacNhan"].Value = DateTime.Now;
                            }
                            else
                            {
                                bangAll.CmdParams.Parameters.AddWithValue("@dNgayKyGDK", DateTime.Now);
                                bangAll.CmdParams.Parameters.AddWithValue("@dNgayXacNhan", DateTime.Now);
                            }
                            if (bangAll.CmdParams.Parameters.IndexOf("@sNguoiKyGDK") >= 0)
                                bangAll.CmdParams.Parameters["@sNguoiKyGDK"].Value = eCoQuanXuLy.sNguoiKy_Ten;
                            else
                                bangAll.CmdParams.Parameters.AddWithValue("@sNguoiKyGDK", eCoQuanXuLy.sNguoiKy_Ten);
                            sNoiDung = "Đồng ý xác nhận GĐK số:" + taoSoGDK.SoGDK;
                        }


                        bangAll.MaNguoiDungSua = User.Identity.Name;
                        bangAll.IPSua = Request.UserHostAddress;
                        bangAll.GiaTriKhoa = iID_MaHoSo;
                        bangAll.DuLieuMoi = false;

                        bangAll.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                        bangAll.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy);
                        bangAll.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                        bangAll.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hoSo.iID_MaTrangThai);

                        bangAll.Save();

                        clLichSuHoSo.InsertLichSu(hoSo.iID_MaHoSo, User.Identity.Name, (int)clDoiTuong.DoiTuong.LanhDaoCuc, HanhDong, sNoiDung, "", hoSo.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);

                        clHoSo.CleanNguoiXem(iID_MaHoSo);
                    }
                }

                TempData["msg"] = "success";
            }
            else
            {
                TempData["msg"] = "error";
            }    

            return RedirectToAction("HoSoChoXacNhanGDK");
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

            String sUserName = User.Identity.Name;
            String sIP = Request.UserHostAddress;

            String ParentID = "TC";
            NameValueCollection values = new NameValueCollection();
            HttpFileCollectionBase files = Request.Files;
            string _sNoiDung = CString.SafeString(Request.Form[ParentID + "_sNoiDung"]);
            string iID_MaHoSo = CString.SafeString(Request.Form[ParentID + "_iID_MaHoSo"]);
            string sKySo = Convert.ToString(Request.Form["txtSignTuChoi"]);
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
                result.value = Url.Action("HoSoChoXacNhanGDK");
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            try
            {
                HoSoModels hoSo = clHoSo.GetHoSoById(Convert.ToInt32(iID_MaHoSo));

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
                                iID_MaDinhKem = CDinhKem.ThemDinhKem(hoSo.iID_MaHoSo, 0, 80, "0", hoSo.sMaHoSo, "File từ chối hồ sơ Lãnh Đạo Cục.", sFileName, "", null, 1, sFileTemp, sUserName, sIP, resDinhKemFile.ItemId);
                            }
                        }
                    }
                }

                int HanhDong = 0;
                switch (hoSo.iID_MaTrangThai)
                {
                    case 17:
                        HanhDong = (int)clHanhDong.HanhDong.TuChoiPheDuyetTuChoiCapGDK;
                        break;
                    case 22:
                        HanhDong = (int)clHanhDong.HanhDong.TuChoiXacNhanGDK;
                        break;
                }
                TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.LanhDaoCuc, HanhDong, hoSo.iID_MaTrangThai, hoSo.iID_MaTrangThaiTruoc);
                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                bang.TruyenGiaTri(ParentID, Request.Form);
                bang.DuLieuMoi = false;
                bang.GiaTriKhoa = iID_MaHoSo;
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy);
                bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hoSo.iID_MaTrangThai);
                bang.CmdParams.Parameters.AddWithValue("@sHashCode", sKySo);
                bang.Save();

                clLichSuHoSo.InsertLichSu(hoSo.iID_MaHoSo, User.Identity.Name, (int)clDoiTuong.DoiTuong.LanhDaoCuc, (int)clHanhDong.HanhDong.TuChoiXacNhanGDK, _sNoiDung, sFileTemp, hoSo.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);

                result.success = true;

                clHoSo.CleanNguoiXem(iID_MaHoSo);
            }
            catch(Exception ex)
            {
                result.success = false;
            }
            
            result.value = Url.Action("HoSoChoXacNhanGDK");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Search(string ParentID)
        {
            string _sMaHoSo = CString.SafeString(Request.Form[ParentID + "_sMaHoSo"]).Trim();
            string _sTenDoanhNghiep = CString.SafeString(Request.Form[ParentID + "_sTenDoanhNghiep"]).Trim();
            string _sTenTACN = CString.SafeString(Request.Form[ParentID + "_sTenTACN"]).Trim();
            string _TuNgayDen = CString.SafeString(Request.Form[ParentID + "_viTuNgayDen"]);
            string _DenNgayDen = CString.SafeString(Request.Form[ParentID + "_viDenNgayDen"]);
            string _sSoTiepNhan = CString.SafeString(Request.Form[ParentID + "_sSoTiepNhan"]).Trim();
            string _TuNgayTiepNhan = CString.SafeString(Request.Form[ParentID + "_viTuNgayTiepNhan"]);
            string _DenNgayTiepNhan = CString.SafeString(Request.Form[ParentID + "_viDenNgayTiepNhan"]);
            string iID_MaTrangThai = CString.SafeString(Request.Form[ParentID + "_iID_MaTrangThai"]);
            string iID_KetQuaXuLy = CString.SafeString(Request.Form[ParentID + "_iID_KetQuaXuLy"]);

            sHoSoModels models = new sHoSoModels
            {
                LoaiDanhSach = 12,
                sMaHoSo = _sMaHoSo,
                sTenDoanhNghiep = _sTenDoanhNghiep,
                sTenTACN = _sTenTACN,
                TuNgayDen = _TuNgayDen,
                DenNgayDen = _DenNgayDen,
                sSoTiepNhan = _sSoTiepNhan,
                TuNgayTiepNhan = _TuNgayTiepNhan,
                DenNgayTiepNhan = _DenNgayTiepNhan,
                iID_MaTrangThai = Convert.ToInt32(iID_MaTrangThai),
                iID_KetQuaXuLy = Convert.ToInt32(iID_KetQuaXuLy)
            };

            TempData["menu"] = 214;
            TempData["msg"] = "success";
            return RedirectToAction("HoSoChoXacNhanGDK", models);
        }

        public ActionResult HoSoChoXacNhanGDK(sHoSoModels models)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, bang.TenBang, "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }


            if (models == null || models.LoaiDanhSach == 0)
            {
                models = new sHoSoModels
                {
                    Page = 1,
                    PageSize = Globals.PageSize,
                    LoaiDanhSach = 12
                };
            }

            ViewData["menu"] = 214;
            return View(models);
        }

        public ActionResult HoSoChatLuongChoDuyet(sHoSoModels models)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, bang.TenBang, "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            if (models == null || models.LoaiDanhSach == 0)
            {
                models = new sHoSoModels
                {
                    iID_MaLoaiHoSo = 3,//Chuyen viên lãnh đạo phòng, lãnh đạo cục chỉ xử lý hồ sơ 2c
                    Page = 1,
                    PageSize = Globals.PageSize,
                    LoaiDanhSach = (int)clLoaiDanhSach.From.HoSoChatLuongChoDuyet,
                };
            }

            ViewData["menu"] = 215;
            return View(models);
        }


        /// <summary>
        /// view màn hình TiepNhanHoSo
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult DetailHH(string iID_MaHangHoa)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, bang.TenBang, "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            HangHoaModels hangHoa = clHangHoa.GetHangHoaById(iID_MaHangHoa);
            ViewData["DuLieuMoi"] = "0";

            ViewData["menu"] = 215;
            return View(hangHoa);
        }

        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SearchHH(string ParentID)
        {
            string _sMaHoSo = CString.SafeString(Request.Form[ParentID + "_sMaHoSo"]).Trim();
            string _sTenDoanhNghiep = CString.SafeString(Request.Form[ParentID + "_sTenDoanhNghiep"]).Trim();
            string _sTenTACN = CString.SafeString(Request.Form[ParentID + "_sTenTACN"]).Trim();
            string _TuNgayDen = CString.SafeString(Request.Form[ParentID + "_viTuNgayDen"]);
            string _DenNgayDen = CString.SafeString(Request.Form[ParentID + "_viDenNgayDen"]);
            string _sSoGDK = CString.SafeString(Request.Form[ParentID + "_sSoGDK"]).Trim();
            string _TuNgayXacNhan = CString.SafeString(Request.Form[ParentID + "_viTuNgayXacNhan"]);
            string _DenNgayXacNhan = CString.SafeString(Request.Form[ParentID + "_viDenNgayXacNhan"]);
            sHoSoModels models = new sHoSoModels
            {
                LoaiDanhSach = (int)clLoaiDanhSach.From.HoSoChatLuongChoDuyet,
                sMaHoSo = _sMaHoSo,
                sTenDoanhNghiep = _sTenDoanhNghiep,
                sTenTACN = _sTenTACN,
                TuNgayDen = _TuNgayDen,
                DenNgayDen = _DenNgayDen,
                sSoGDK = _sSoGDK,
                TuNgayXacNhan = _TuNgayXacNhan,
                DenNgayXacNhan = _DenNgayXacNhan
            };

            TempData["menu"] = 215;
            TempData["msg"] = "success";
            return RedirectToAction("HoSoChatLuongChoDuyet", models);
        }
        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult TuChoiHoSoHHSubmit()
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
                base.ViewData["DuLieuMoi"] = "0";
                ViewData["iID_MaHangHoa"] = CString.SafeString(iID_MaHangHoa);

                //TempData["msg"] = "error";

                result.success = false;
                result.value = Url.Action("HoSoChatLuongChoDuyet");
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
                                iID_MaDinhKem = CDinhKem.ThemDinhKem(hangHoa.iID_MaHoSo, hangHoa.iID_MaHangHoa, 81, "0", hangHoa.sMaHoSo, "File từ chối XNCL Lãnh Đạo Cục.", sFileName, "", null, 1, sFileTemp, sUserName, sIP, resDinhKemFile.ItemId);
                            }
                        } 
                    }
                }
               
                TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.LanhDaoCuc, (int)clHanhDong.HanhDong.TuChoiPheDuyetThongBaoKetQua, hangHoa.iID_MaTrangThai, hangHoa.iID_MaTrangThaiTruoc);

                bangHH.MaNguoiDungSua = User.Identity.Name;
                bangHH.IPSua = Request.UserHostAddress;
                bangHH.TruyenGiaTri(ParentID, Request.Form);
                bangHH.DuLieuMoi = false;
                bangHH.GiaTriKhoa = hangHoa.iID_MaHangHoa;
                bangHH.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                bangHH.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy);
                bangHH.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                bangHH.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hangHoa.iID_MaTrangThai);
                bangHH.Save();
                
                clLichSuHoSo.InsertLichSu(hangHoa.iID_MaHangHoa, User.Identity.Name, (int)clDoiTuong.DoiTuong.LanhDaoCuc, (int)clHanhDong.HanhDong.TuChoiPheDuyetThongBaoKetQua, _sNoiDung, sFileTemp, hangHoa.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);

                clHoSo.CleanNguoiXem(iID_MaHangHoa);

                result.success = true;
            }
            catch(Exception ex)
            {
                result.success = false;
            }

            result.value = Url.Action("HoSoChatLuongChoDuyet");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult DuyetHH(String ParentID)
        {
            string iID_MaHangHoa = CString.SafeString(Request.Form[ParentID + "_iID_MaHangHoa"]);
            if(String.IsNullOrEmpty(iID_MaHangHoa) == false)
            {
                HangHoaModels hangHoa = clHangHoa.GetHangHoaById(Convert.ToInt64(iID_MaHangHoa));
                TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.LanhDaoCuc, (int)clHanhDong.HanhDong.KySoPheDuyetThongBaoKetQua, hangHoa.iID_MaTrangThai, hangHoa.iID_MaTrangThaiTruoc);

                clTaoSoThongBao taoSoTB = clTaoSoThongBao.GetSoTB();

                bangHH.MaNguoiDungSua = User.Identity.Name;
                bangHH.IPSua = Request.UserHostAddress;
                bangHH.TruyenGiaTri(ParentID, Request.Form);

                bangHH.CmdParams.Parameters.AddWithValue("@sSoThongBaoKetQua", taoSoTB.SoTB);
                bangHH.CmdParams.Parameters.AddWithValue("@sSoThongBaoKetQua_NoiKy", eCoQuanXuLy.sNguoiKy_NoiKy);
                bangHH.CmdParams.Parameters.AddWithValue("@dSoThongBaoKetQua_NgayKy", DateTime.Now);
                bangHH.CmdParams.Parameters.AddWithValue("@sSoThongBaoKetQua_NguoiKy", eCoQuanXuLy.sNguoiKy_Ten);

                bangHH.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                bangHH.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy);
                bangHH.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                bangHH.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hangHoa.iID_MaTrangThai);

                bangHH.Save();

                clLichSuHangHoa.InsertLichSu(hangHoa.iID_MaHangHoa, User.Identity.Name, (int)clDoiTuong.DoiTuong.LanhDaoCuc, (int)clHanhDong.HanhDong.KySoPheDuyetThongBaoKetQua, "Ký số phê duyệt thông báo kết quả kiểm tra", "", hangHoa.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);

                clHangHoa.CleanNguoiXem(Convert.ToString(iID_MaHangHoa));

                TempData["msg"] = "success";
            }
            else
            {
                TempData["msg"] = "error";
            }    

            return RedirectToAction("HoSoChatLuongChoDuyet");
        }

        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult DuyetHHSubmit(string ParentID)
        {
            string s_HangHoa = Request.Form[ParentID + "_HangHoa"];
            if (!string.IsNullOrEmpty(s_HangHoa))
            {
                string[] arr = s_HangHoa.Split(',');
                for (int i = 0; i < arr.Length; i++)
                {
                    long iID_MaHangHoa = Convert.ToInt64(arr[i]);
                    HangHoaModels hanghoa = clHangHoa.GetHangHoaById(Convert.ToInt64(iID_MaHangHoa));
                    if(hanghoa != null)
                    {
                        TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.LanhDaoCuc, (int)clHanhDong.HanhDong.KySoPheDuyetThongBaoKetQua, hanghoa.iID_MaTrangThai, hanghoa.iID_MaTrangThaiTruoc);

                        clTaoSoThongBao taoSoTB = clTaoSoThongBao.GetSoTB();

                        String sSoThongBaoKetQua = taoSoTB.SoTB;

                        Bang bangHHALL = new Bang("CNN25_HangHoa");
                        bangHHALL.MaNguoiDungSua = User.Identity.Name;
                        bangHHALL.IPSua = Request.UserHostAddress;
                        bangHHALL.DuLieuMoi = false;
                        bangHHALL.GiaTriKhoa = iID_MaHangHoa;

                        bangHHALL.CmdParams.Parameters.AddWithValue("@sSoThongBaoKetQua", sSoThongBaoKetQua);
                        bangHHALL.CmdParams.Parameters.AddWithValue("@sSoThongBaoKetQua_NoiKy", eCoQuanXuLy.sNguoiKy_NoiKy);
                        bangHHALL.CmdParams.Parameters.AddWithValue("@dSoThongBaoKetQua_NgayKy", DateTime.Now);
                        bangHHALL.CmdParams.Parameters.AddWithValue("@sSoThongBaoKetQua_NguoiKy", eCoQuanXuLy.sNguoiKy_Ten);

                        bangHHALL.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy);
                        bangHHALL.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                        bangHHALL.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                        bangHHALL.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hanghoa.iID_MaTrangThai);

                        bangHHALL.Save();

                        clLichSuHangHoa.InsertLichSu(hanghoa.iID_MaHangHoa, User.Identity.Name, (int)clDoiTuong.DoiTuong.LanhDaoCuc, (int)clHanhDong.HanhDong.KySoPheDuyetThongBaoKetQua, "Ký số phê duyệt thông báo kết quả kiểm tra", "", hanghoa.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);

                        clHangHoa.CleanNguoiXem(Convert.ToString(iID_MaHangHoa));
                    }
                }

                TempData["msg"] = "success";
            }
            else
            {
                TempData["msg"] = "error";
            }    

            return RedirectToAction("HoSoChatLuongChoDuyet");
        }


        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult DongY(string iID_MaHangHoa)
        {
            ResultModels result = new ResultModels();

            String sUserName = User.Identity.Name;
            String sIP = Request.UserHostAddress;
            
            if(String.IsNullOrEmpty(iID_MaHangHoa) == false)
            {
                HangHoaModels hanghoa = clHangHoa.GetHangHoaById(Convert.ToInt64(iID_MaHangHoa));
                TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.LanhDaoCuc, (int)clHanhDong.HanhDong.KySoPheDuyetThongBaoKetQua, hanghoa.iID_MaTrangThai, hanghoa.iID_MaTrangThaiTruoc);

                clTaoSoThongBao taoSoTB = clTaoSoThongBao.GetSoTB();

                bangHH.MaNguoiDungSua = sUserName;
                bangHH.IPSua = sIP;
                bangHH.DuLieuMoi = false;
                bangHH.GiaTriKhoa = iID_MaHangHoa;

                bangHH.CmdParams.Parameters.AddWithValue("@sSoThongBaoKetQua", taoSoTB.SoTB);
                bangHH.CmdParams.Parameters.AddWithValue("@sSoThongBaoKetQua_NoiKy", eCoQuanXuLy.sNguoiKy_NoiKy);
                bangHH.CmdParams.Parameters.AddWithValue("@sSoThongBaoKetQua_NgayKy", DateTime.Now);
                bangHH.CmdParams.Parameters.AddWithValue("@sSoThongBaoKetQua_NguoiKy", eCoQuanXuLy.sNguoiKy_Ten);

                bangHH.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy);
                bangHH.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                bangHH.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                bangHH.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hanghoa.iID_MaTrangThai);
                bangHH.Save();

                clLichSuHangHoa.InsertLichSu(hanghoa.iID_MaHangHoa, User.Identity.Name, (int)clDoiTuong.DoiTuong.LanhDaoCuc, (int)clHanhDong.HanhDong.KySoPheDuyetThongBaoKetQua, "Ký số phê duyệt thông báo kết quả kiểm tra", "", hanghoa.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);

                clHangHoa.CleanNguoiXem(iID_MaHangHoa);

                result.success = true;
            }   
            else
            {
                result.success = false;
            }    

            result.value = Url.Action("HoSoChatLuongChoDuyet");
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}