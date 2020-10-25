using DATA0200025;
using DATA0200025.Models;
using DATA0200025.SearchModels;
using DomainModel;
using DomainModel.Abstract;
using System;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace APP0200025.Controllers
{
    public class ChuyenVienController : Controller
    {
        Bang bang = new Bang("CNN25_HoSo");

        private string ViewPath = "~/Views/ChoTiepNhanHoSo/";
        // GET: ChoTiepNhanHoSo
        public ActionResult Index(sHoSoModels models)
        {
            if (models == null || models.LoaiDanhSach == 0)
            {
                models = new sHoSoModels
                {
                    Page = 1,
                    PageSize = Globals.PageSize,
                    LoaiDanhSach = 7
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
            HoSoModels hoSo = clHoSo.GetHoSoById(iID_MaHoSo);
            ViewData["DuLieuMoi"] = "0";
            return View(hoSo);
        }
        /// <summary>
        /// view màn hình soạn phụ lục
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SoanPhuLuc(string iID_MaHoSo)
        {
            HoSoModels hoSo = clHoSo.GetHoSoById(iID_MaHoSo);
            ViewData["DuLieuMoi"] = "0";
            return View(hoSo);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public PartialViewResult XuLyChiTieuKiemTra(string iID_MaHangHoa)
        {
            HangHoaModels hanghoa = clHangHoa.GetHangHoaById(Convert.ToInt32(iID_MaHangHoa));
            ViewData["DuLieuMoi"] = "0";
            return PartialView(hanghoa);
        }
        
        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult XuLyChiTieuSubmit(String ParentID)
        {
            string iID_MaHangHoa = CString.SafeString(Request.Form["EDit_iID_MaHangHoa"]);
            string CL_Chons = CString.SafeString(Request.Form["CL_bChon"]);
            string ATDN_Chons= CString.SafeString(Request.Form["ATDN_bChon"]);
            string ATKT_Chons = CString.SafeString(Request.Form["ATKT_bChon"]);
            SqlCommand cmd;
            string sGhiChu = "";
            clHangHoa.Update_ResetbChon(iID_MaHangHoa);
            if (!string.IsNullOrEmpty(CL_Chons))
            {
                string[] arr = CL_Chons.Split(',');

                cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@iID_MaHangHoaCL", 0);
                cmd.Parameters.AddWithValue("@bChon", 1);
                cmd.Parameters.AddWithValue("@sGhiChu", "");
                for (int i = 0; i < arr.Length; i++)
                {
                    sGhiChu = Request.Form[string.Format("CL{0}_sGhiChu", arr[i])];
                    cmd.Parameters["@iID_MaHangHoaCL"].Value = arr[i];
                    cmd.Parameters["@sGhiChu"].Value = sGhiChu;
                    Connection.UpdateRecord("CNN25_HangHoa_ChatLuong", "iID_MaHangHoaCL", cmd);
                }
                cmd.Dispose();
            }
            if (!string.IsNullOrEmpty(ATDN_Chons))
            {
                string[] arr = ATDN_Chons.Split(',');

                cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@iID_MahangHoaAT", 0);
                cmd.Parameters.AddWithValue("@bChon", 1);
                cmd.Parameters.AddWithValue("@sGhiChu", "");
                for (int i = 0; i < arr.Length; i++)
                {
                    sGhiChu = Request.Form[string.Format("ATDN{0}_sGhiChu", arr[i])];
                    cmd.Parameters["@iID_MahangHoaAT"].Value = arr[i];
                    cmd.Parameters["@sGhiChu"].Value = sGhiChu;
                    Connection.UpdateRecord("CNN25_HangHoa_AnToan", "iID_MahangHoaAT", cmd);
                }
                cmd.Dispose();
            }
            if (!string.IsNullOrEmpty(ATKT_Chons))
            {
                string[] arr = ATKT_Chons.Split(',');

                cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@iID_MaHangHoaATKT", 0);
                cmd.Parameters.AddWithValue("@bChon", 1);
                cmd.Parameters.AddWithValue("@sGhiChu", "");
                for (int i = 0; i < arr.Length; i++)
                {
                    sGhiChu = Request.Form[string.Format("ATKT{0}_sGhiChu", arr[i])];
                    cmd.Parameters["@iID_MaHangHoaATKT"].Value = arr[i];
                    cmd.Parameters["@sGhiChu"].Value = sGhiChu;
                    Connection.UpdateRecord("CNN25_HangHoa_AnToan_KyThuat", "iID_MaHangHoaATKT", cmd);
                }
                cmd.Dispose();
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
        public ActionResult TrinhLanhDao(String ParentID)
        {
            string iID_MaHoSo = CString.SafeString(Request.Form[ParentID + "_iID_MaHoSo"]);
            HoSoModels hoSo = clHoSo.GetHoSoById(Convert.ToInt32(iID_MaHoSo));
            TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.ChuyenVien, (int)clHanhDong.HanhDong.DongYSoanPhuLucTrinhLanhXemXet, hoSo.iID_MaTrangThai, hoSo.iID_MaTrangThaiTruoc);

           
            bang.MaNguoiDungSua = User.Identity.Name;
            bang.IPSua = Request.UserHostAddress;
            bang.TruyenGiaTri(ParentID, Request.Form);
            bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
            bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy);
            bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
            bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hoSo.iID_MaTrangThaiTruoc);
            bang.Save();
            clHoSo.CleanNguoiXem(iID_MaHoSo);
            clLichSuHoSo.InsertLichSu(hoSo.iID_MaHoSo, User.Identity.Name, (int)clDoiTuong.DoiTuong.ChuyenVien, (int)clHanhDong.HanhDong.DongYSoanPhuLucTrinhLanhXemXet, "Trình lãnh đạo", "", hoSo.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);
            return RedirectToAction("Index");
        }
        /// <s
        /// <summary>
        /// view màn hình YeuCauBoXung
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult YeuCauBoXung()
        {
            ViewData["DuLieuMoi"] = "0";
            return View();
        }
        /// <summary>
        /// view màn hình TuChoiHoSo
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult TuChoiHoSo()
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
        public ActionResult TiepNhanHoSo(String ParentID, String MaHoSo)
        {
            Bang bang = new Bang("CNN25_HoSo");
            bang.MaNguoiDungSua = User.Identity.Name;
            bang.IPSua = Request.UserHostAddress;
            bang.TruyenGiaTri(ParentID, Request.Form);
            bang.Save();
            return RedirectToAction("Index");
        }
        /// <summary>
        /// update data màn hình TuChoiHoSo
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="MaHoSo"></param>
        /// <returns></returns>
        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult TuChoiHoSo(String ParentID)
        {
            NameValueCollection values = new NameValueCollection();
            string sTenNguoiTiepNhan = CString.SafeString(Request.Form[ParentID + "_sTenNguoiTiepNhan"]);
            string iID_MaHoSo = CString.SafeString(Request.Form[ParentID + "iID_MaHoSo"]);
            if (string.IsNullOrEmpty(sTenNguoiTiepNhan))
            {
                values.Add("err_sTenNguoiTiepNhan", "Bạn nhập thông tin yêu cầu cần bổ xung");
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
            Bang bang = new Bang("CNN25_HoSo");
            bang.MaNguoiDungSua = User.Identity.Name;
            bang.IPSua = Request.UserHostAddress;
            bang.TruyenGiaTri(ParentID, Request.Form);
            bang.Save();
            return RedirectToAction("Index");
        }
        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Search(string ParentID)
        {
            string _sMaHoSo = CString.SafeString(Request.Form[ParentID + "_sMaHoSo"]);
            string _sTenDoanhNghiep = CString.SafeString(Request.Form[ParentID + "_sTenDoanhNghiep"]);
            string _sTenTACN = CString.SafeString(Request.Form[ParentID + "_sTenTACN"]);
            string _FromDate = CString.SafeString(Request.Form[ParentID + "_viFromDate"]);
            string _ToDate = CString.SafeString(Request.Form[ParentID + "_viToDate"]);
            string _sSoTiepNhan = CString.SafeString(Request.Form[ParentID + "_sSoTiepNhan"]);
            string _FromDateTiepNhan = CString.SafeString(Request.Form[ParentID + "_viFromDateTiepNhan"]);
            string _ToDateTiepNhan = CString.SafeString(Request.Form[ParentID + "_viToDateTiepNhan"]);
            sHoSoModels models = new sHoSoModels
            {
                LoaiDanhSach=7,
                sMaHoSo = _sMaHoSo,
                sTenDoanhNghiep = _sTenDoanhNghiep,
                sTenTACN = _sTenTACN,
                TuNgayDen = _FromDate,
                DenNgayDen = _ToDate,
                sSoTiepNhan = _sSoTiepNhan,
                TuNgayTiepNhan = _FromDateTiepNhan,
                DenNgayTiepNhan = _ToDateTiepNhan
            };
            return RedirectToAction("Index", models);
        }

        public PartialViewResult Partial_HangHoa(int iID_MaHoSo,string iID_MaPhanLoai)
        {
            ViewBag.dt = clHangHoa.Get_HangHoaTheoHoSo(iID_MaHoSo,iID_MaPhanLoai);
            return PartialView();
        }
    }
}