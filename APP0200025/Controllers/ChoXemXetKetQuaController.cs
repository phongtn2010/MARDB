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
    public class ChoXemXetKetQuaController : Controller
    {
        // GET: ChoXemXetKetQua
        Bang bang = new Bang("CNN25_HangHoa");
        public ActionResult Index(sHoSoModels models)
        {
            if (models == null || models.LoaiDanhSach == 0)
            {
                models = new sHoSoModels
                {
                    LoaiDanhSach = (int)clLoaiDanhSach.From.ChoXemXetKetQua,
                    Page = 1,
                    PageSize = Globals.PageSize
                };
            }

            ViewData["menu"] = 208;
            return View(models);
        }

        public ActionResult Detail(string iID_MaHangHoa)
        {
            //if (BaoMat.ChoPhepLamViec(User.Identity.Name, bang.TenBang, "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            //{
            //    return RedirectToAction("Index", "PermitionMessage");
            //}
            clHangHoa.UpdateNguoiXem(iID_MaHangHoa, User.Identity.Name);

            HangHoaModels models = clHangHoa.GetHangHoaById(Convert.ToInt32(iID_MaHangHoa));

            ViewData["menu"] = 208;
            return View(models);
        }
        
        public ActionResult DongY(string iID_MaHangHoa)
        {
            if (String.IsNullOrEmpty(iID_MaHangHoa) == false)
            {
                HangHoaModels hanghoa = clHangHoa.GetHangHoaById(Convert.ToInt32(iID_MaHangHoa));
                if (hanghoa.iID_MaTrangThai == 34 || hanghoa.iID_MaTrangThai == 35 || hanghoa.iID_MaTrangThai == 43)
                {
                    int HanhDong = (int)clHanhDong.HanhDong.DongYXemXetThongBaoKetQua;
                    if (hanghoa.iID_MaTrangThai == 34)
                    {
                        HanhDong = (int)clHanhDong.HanhDong.DongYYeuCauBoSungKetQua;
                    }
                    TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.LanhDaoPhong, HanhDong, hanghoa.iID_MaTrangThai, hanghoa.iID_MaTrangThaiTruoc);

                    bang.MaNguoiDungSua = User.Identity.Name;
                    //bang.IPSua = Request.UserHostAddress;
                    bang.DuLieuMoi = false;
                    bang.GiaTriKhoa = iID_MaHangHoa;
                    bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy);
                    bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hanghoa.iID_MaTrangThai);
                    bang.Save();
                    
                    clLichSuHangHoa.InsertLichSu(hanghoa.iID_MaHangHoa, User.Identity.Name, (int)clDoiTuong.DoiTuong.LanhDaoPhong, HanhDong, "Đồng ý", "", hanghoa.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);
                }

                clHangHoa.CleanNguoiXem(iID_MaHangHoa);

                TempData["msg"] = "success";
            }
            else
            {
                TempData["msg"] = "error";
            }
            return RedirectToAction("Index");
        }

        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult DongYSubmit(string ParentID)
        {
            string s_HangHoa = Request.Form[ParentID + "_HangHoa"];
            if(!string.IsNullOrEmpty(s_HangHoa))
            {
                string[] arr = s_HangHoa.Split(',');
                for (int i = 0; i < arr.Length; i++)
                {
                    int iID_MaHangHoa = Convert.ToInt32(arr[i]);
                    HangHoaModels hanghoa = clHangHoa.GetHangHoaById(Convert.ToInt32(iID_MaHangHoa));
                    if (hanghoa.iID_MaTrangThai == 34|| hanghoa.iID_MaTrangThai == 35|| hanghoa.iID_MaTrangThai == 43)
                    {     
                        int HanhDong = (int)clHanhDong.HanhDong.DongYXemXetThongBaoKetQua;
                        if (hanghoa.iID_MaTrangThai == 34)
                        {
                            HanhDong = (int)clHanhDong.HanhDong.DongYYeuCauBoSungKetQua;
                        }
                        TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.LanhDaoPhong, HanhDong, hanghoa.iID_MaTrangThai, hanghoa.iID_MaTrangThaiTruoc);
                        
                        bang.MaNguoiDungSua = User.Identity.Name;
                        bang.IPSua = Request.UserHostAddress;
                        bang.DuLieuMoi = false;
                        bang.GiaTriKhoa = iID_MaHangHoa;

                        if (i == 0)
                        {
                            bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy);
                            bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                            bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                            bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hanghoa.iID_MaTrangThai);
                        }
                        else
                        {
                            bang.CmdParams.Parameters["@iID_MaHangHoa"].Value = iID_MaHangHoa;
                            bang.CmdParams.Parameters["@sKetQuaXuLy"].Value = trangThaiTiepTheo.sKetQuaXuLy;
                            bang.CmdParams.Parameters["@iID_KetQuaXuLy"].Value = trangThaiTiepTheo.iID_KetQuaXuLy;
                            bang.CmdParams.Parameters["@iID_MaTrangThai"].Value = trangThaiTiepTheo.iID_MaTrangThai;
                            bang.CmdParams.Parameters["@iID_MaTrangThaiTruoc"].Value = hanghoa.iID_MaTrangThai;
                        }
                        
                        bang.Save();
                   
                        clLichSuHangHoa.InsertLichSu(hanghoa.iID_MaHangHoa, User.Identity.Name, (int)clDoiTuong.DoiTuong.LanhDaoPhong, HanhDong, "Đồng ý", "", hanghoa.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);

                        clHangHoa.CleanNguoiXem(Convert.ToString(iID_MaHangHoa));
                    }
                }

                TempData["msg"] = "success";
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
        public ActionResult ThuHoi(String iID_MaHangHoa)
        {
            if (String.IsNullOrEmpty(iID_MaHangHoa) == false)
            {
                HangHoaModels hanghoa = clHangHoa.GetHangHoaById(Convert.ToInt32(iID_MaHangHoa));
                TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.LanhDaoPhong, (int)clHanhDong.HanhDong.ThuHoiVaXuLyLai, hanghoa.iID_MaTrangThai, hanghoa.iID_MaTrangThaiTruoc);
                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                bang.DuLieuMoi = false;
                bang.GiaTriKhoa = iID_MaHangHoa;
                bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy);
                bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hanghoa.iID_MaTrangThai);
                bang.Save();

                clLichSuHangHoa.InsertLichSu(hanghoa.iID_MaHangHoa, User.Identity.Name, (int)clDoiTuong.DoiTuong.LanhDaoPhong, (int)clHanhDong.HanhDong.ThuHoiVaXuLyLai, "Thu hồi và xử lý lại", "", hanghoa.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);

                TempData["msg"] = "success";
            }
            else
            {
                TempData["msg"] = "error";
            }

            clHoSo.CleanNguoiXem(iID_MaHangHoa);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// update data màn hình TiepNhanHoSo
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="MaHoSo"></param>
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

                base.ViewData["DuLieuMoi"] = "0";
                ViewData["iID_MaHangHoa"] = CString.SafeString(iID_MaHangHoa);

                result.success = false;
                result.value = Url.Action("Index");
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            try
            {
                HangHoaModels hanghoa = clHangHoa.GetHangHoaById(Convert.ToInt32(iID_MaHangHoa));
                

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

                            //Luu vao bang Dinh Kem
                            iID_MaDinhKem = CDinhKem.ThemDinhKem(hanghoa.iID_MaHoSo, hanghoa.iID_MaHangHoa, 61, "0", hanghoa.sMaHoSo, "File từ chối XNCL Lãnh Đạo Phòng.", sFileName, "", null, 1, sFileTemp, sUserName, sIP);
                        }
                    }
                }

                TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.LanhDaoPhong, (int)clHanhDong.HanhDong.TuChoiXemXetThongBaoKetQua, hanghoa.iID_MaTrangThai, hanghoa.iID_MaTrangThaiTruoc);

                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                bang.DuLieuMoi = false;
                bang.GiaTriKhoa = iID_MaHangHoa;
                bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy);
                bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hanghoa.iID_MaTrangThai);
                bang.Save();

                clLichSuHangHoa.InsertLichSu(hanghoa.iID_MaHangHoa, User.Identity.Name, (int)clDoiTuong.DoiTuong.ChuyenVien, (int)clHanhDong.HanhDong.TuChoiXemXetThongBaoKetQua, _sNoiDung, sFileTemp, hanghoa.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);

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
            string _TuNgayDen = CString.SafeString(Request.Form[ParentID + "_viTuNgayDen"]);
            string _DenNgayDen = CString.SafeString(Request.Form[ParentID + "_viDenNgayDen"]);
            string _sSoTiepNhan = CString.SafeString(Request.Form[ParentID + "_sSoTiepNhan"]);
            string _TuNgayTiepNhan = CString.SafeString(Request.Form[ParentID + "_viTuNgayTiepNhan"]);
            string _DenNgayTiepNhan = CString.SafeString(Request.Form[ParentID + "_viDenNgayTiepNhan"]);
            string iID_MaTrangThai = CString.SafeString(Request.Form[ParentID + "_iID_MaTrangThai"]);

            int iTrangThai = 0;
            if(String.IsNullOrEmpty(iID_MaTrangThai) == false)
            {
                iTrangThai = Convert.ToInt32(iID_MaTrangThai);
            }

            sHoSoModels models = new sHoSoModels
            {
                LoaiDanhSach = 10,
                sMaHoSo = _sMaHoSo,
                sTenDoanhNghiep = _sTenDoanhNghiep,
                sTenTACN = _sTenTACN,
                TuNgayDen = _TuNgayDen,
                DenNgayDen = _DenNgayDen,
                sSoTiepNhan = _sSoTiepNhan,
                TuNgayTiepNhan = _TuNgayTiepNhan,
                DenNgayTiepNhan = _DenNgayTiepNhan,
                iID_MaTrangThai = iTrangThai
            };

            TempData["menu"] = 208;
            TempData["msg"] = "success";
            return RedirectToAction("Index", models);
        }

    }
}