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
    public class LanhDaoPhongController : Controller
    {
        Bang bang = new Bang("CNN25_HoSo");

        private string ViewPath = "~/Views/LanhDaoPhong/";
        // GET: 
        public ActionResult Index(sHoSoModels models)
        {
            if (models == null || models.LoaiDanhSach == 0)
            {
                models = new sHoSoModels
                {
                    Page = 1,
                    PageSize = Globals.PageSize,
                    LoaiDanhSach = 10
                };
            }

            ViewData["menu"] = 207;
            return View(models);
        }


        /// <summary>
        /// view màn hình TiepNhanHoSo
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Detail(string iID_MaHoSo)
        {
            HoSoModels hoSo = clHoSo.GetHoSoById(iID_MaHoSo);

            ViewData["menu"] = 207;
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
            if(String.IsNullOrEmpty(iID_MaHoSo) == false)
            {
                HoSoModels hoSo = clHoSo.GetHoSoById(Convert.ToInt32(iID_MaHoSo));

                int HanhDong = 0;
                switch (hoSo.iID_MaTrangThai)
                {
                    case 12:
                        HanhDong = (int)clHanhDong.HanhDong.DongYYeuCauBoSung;
                        break;
                    case 11:
                    case 20:
                        HanhDong = (int)clHanhDong.HanhDong.DongYXemXetTuChoiCap;
                        break;
                    case 10:
                    case 25:
                        HanhDong = (int)clHanhDong.HanhDong.DongYXemXetXacNhan;
                        break;
                }
                TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.LanhDaoPhong, HanhDong, hoSo.iID_MaTrangThai, hoSo.iID_MaTrangThaiTruoc);

                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                bang.DuLieuMoi = false;
                //bang.TruyenGiaTri(ParentID, Request.Form);
                bang.GiaTriKhoa = hoSo.iID_MaHoSo;
                bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy);
                bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hoSo.iID_MaTrangThai);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                bang.Save();

                clLichSuHoSo.InsertLichSu(hoSo.iID_MaHoSo, User.Identity.Name, (int)clDoiTuong.DoiTuong.LanhDaoPhong, HanhDong, trangThaiTiepTheo.sTen, "", hoSo.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);

                TempData["msg"] = "success";
            }
            else
            {
                TempData["msg"] = "error";
            }    
            
            clHoSo.CleanNguoiXem(iID_MaHoSo);

            return RedirectToAction("Index");
        }

        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult DongYSubmit(String ParentID)
        {
            string s_HoSo = Request.Form[ParentID + "_HO_SO"];
            if (!string.IsNullOrEmpty(s_HoSo))
            {
                HoSoModels hoSo;
                string[] arr = s_HoSo.Split(',');
                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                bang.DuLieuMoi = false;
                for (int i = 0; i < arr.Length; i++)
                {
                    hoSo = clHoSo.GetHoSoById(Convert.ToInt32(arr[i]));
                    int HanhDong = 0;
                    switch (hoSo.iID_MaTrangThai)
                    {
                        case 12:
                            HanhDong = (int)clHanhDong.HanhDong.DongYYeuCauBoSung;
                            break;
                        case 11:
                        case 20:
                            HanhDong = (int)clHanhDong.HanhDong.DongYXemXetTuChoiCap;
                            break;
                        case 10:
                        case 25:
                            HanhDong = (int)clHanhDong.HanhDong.DongYXemXetXacNhan;
                            break;
                    }
                    TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.LanhDaoPhong, HanhDong, hoSo.iID_MaTrangThai, hoSo.iID_MaTrangThaiTruoc);

                    if (i == 0)
                    {
                        bang.GiaTriKhoa = hoSo.iID_MaHoSo;
                        bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy);
                        bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                        bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                        bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hoSo.iID_MaTrangThai);
                    }
                    else
                    {
                        bang.CmdParams.Parameters["@iID_MaHoSo"].Value = hoSo.iID_MaHoSo;
                        bang.CmdParams.Parameters["@sKetQuaXuLy"].Value = trangThaiTiepTheo.sKetQuaXuLy;
                        bang.CmdParams.Parameters["@iID_KetQuaXuLy"].Value = trangThaiTiepTheo.iID_KetQuaXuLy;
                        bang.CmdParams.Parameters["@iID_MaTrangThai"].Value = trangThaiTiepTheo.iID_MaTrangThai;
                        bang.CmdParams.Parameters["@iID_MaTrangThaiTruoc"].Value = hoSo.iID_MaTrangThai;
                    }
            
                    bang.Save();
                    
                    clLichSuHoSo.InsertLichSu(hoSo.iID_MaHoSo, User.Identity.Name, (int)clDoiTuong.DoiTuong.LanhDaoPhong, HanhDong, trangThaiTiepTheo.sTen, "", hoSo.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);

                    clHoSo.CleanNguoiXem(arr[i]);
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

            string sFileTemp = "";
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase postedFile = files[i];
                if (postedFile != null && postedFile.ContentLength > 0)
                {
                    if (CommonFunction.FileUploadCheck(postedFile))
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
            }
            HoSoModels hoSo = clHoSo.GetHoSoById(Convert.ToInt32(iID_MaHoSo));
            int HanhDong = 0;
            switch (hoSo.iID_MaTrangThai)
            {
                case 12:
                    HanhDong = (int)clHanhDong.HanhDong.TuChoiYeuCauBoSungTraLaiChuyenVien;
                    break;
                case 11:
                case 20:
                    HanhDong = (int)clHanhDong.HanhDong.TuChoiXemXetTuChoiCapGDK;
                    break;
                case 10:
                case 25:
                    HanhDong = (int)clHanhDong.HanhDong.TuChoiXemXetXacNhan;
                    break;
            }

            TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.LanhDaoPhong, HanhDong, hoSo.iID_MaTrangThai, hoSo.iID_MaTrangThaiTruoc);

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

            clLichSuHoSo.InsertLichSu(hoSo.iID_MaHoSo, User.Identity.Name, (int)clDoiTuong.DoiTuong.LanhDaoPhong, HanhDong, _sNoiDung, sFileTemp, hoSo.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);

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
        public ActionResult ThuHoiHoSo(string iID_MaHoSo)
        {
            if(String.IsNullOrEmpty(iID_MaHoSo) == false)
            {
                HoSoModels hoSo = clHoSo.GetHoSoById(Convert.ToInt32(iID_MaHoSo));

                TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.LanhDaoPhong, (int)clHanhDong.HanhDong.ThuHoiVaXuLyLai, hoSo.iID_MaTrangThai, hoSo.iID_MaTrangThaiTruoc);

                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                
                bang.DuLieuMoi = false;
                bang.GiaTriKhoa = iID_MaHoSo;
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy);
                bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hoSo.iID_MaTrangThai);
                bang.Save();

                clLichSuHoSo.InsertLichSu(hoSo.iID_MaHoSo, User.Identity.Name, (int)clDoiTuong.DoiTuong.LanhDaoPhong, (int)clHanhDong.HanhDong.ThuHoiVaXuLyLai, "", "", hoSo.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);

                TempData["msg"] = "success";
            }
            else
            {
                TempData["msg"] = "error";
            }

            clHoSo.CleanNguoiXem(iID_MaHoSo);

            return RedirectToAction("Index");
        }

        public JsonResult Thoat(string iID_MaHoSo)
        {
            ResultModels result = clHoSo.CleanNguoiXem(iID_MaHoSo); ;
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
                DenNgayTiepNhan = _DenNgayTiepNhan
            };

            TempData["menu"] = 207;
            TempData["msg"] = "success";
            return RedirectToAction("Index", models);
        }
    }
}