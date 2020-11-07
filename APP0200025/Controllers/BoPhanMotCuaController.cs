using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Abstract;
using DATA0200026;
using DATA0200026.WebServices;
using DomainModel;
using DATA0200026.WebServices.XmlType.Request;

namespace APP0200025.Controllers
{
    public class BoPhanMotCuaController : Controller
    {
        private SendService _sendService = new SendService();

        Bang bang = new Bang("CNN26_HoSo");

        // GET: BoPhanMotCua
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MienGiam_ChoTiepNhan(CHoSoSearch hoSoSearch)
        {
            if (hoSoSearch == null || hoSoSearch.iID_MaTrangThai == 0)
            {
                hoSoSearch = new CHoSoSearch
                {
                    iID_MaTrangThai = 1,
                    Page = 1,
                    PageSize = Globals.PageSize
                };
            }

            return View(hoSoSearch);
        }

        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MienGiam_ChoTiepNhan_Search(string ParentID)
        {
            string _sMaHoSo = CString.SafeString(Request.Form[ParentID + "_sMaHoSo"]);
            string _sTenDoanhNghiep = CString.SafeString(Request.Form[ParentID + "_sTenDoanhNghiep"]);
            string _sTenTACN = CString.SafeString(Request.Form[ParentID + "_sTenTACN"]);
            string _FromDate = CString.SafeString(Request.Form[ParentID + "_viTuNgayDen"]);
            string _ToDate = CString.SafeString(Request.Form[ParentID + "_viDenNgayDen"]);
            CHoSoSearch models = new CHoSoSearch
            {
                iID_MaTrangThai = 1,
                sMaHoSo = _sMaHoSo,
                sTenDoanhNghiep = _sTenDoanhNghiep,
                sTenTACN = _sTenTACN,
                TuNgayDen = _FromDate,
                DenNgayDen = _ToDate
            };
            return RedirectToAction("MienGiam_ChoTiepNhan", "BoPhanMotCua", models);
        }

        public ActionResult MienGiam_ChoTiepNhan_Detail(string iID_MaHoSo)
        {
            CHoSo26.UpdateNguoiXem(iID_MaHoSo, User.Identity.Name);
            
            HoSo26Models models = CHoSo26.Get_Detail(Convert.ToInt64(iID_MaHoSo));

            return View(models);
        }

        public ActionResult MienGiam_ChoTiepNhan_TiepNhan(string iID_MaHoSo, string sMaHoSo)
        {
            String sUserName = User.Identity.Name;
            String sTenUser = CPQ_NGUOIDUNG.Get_TenNguoiDung(sUserName);
            CHoSo26.UpdateNguoiXem(iID_MaHoSo, sUserName);

            bang.MaNguoiDungSua = sUserName;
            bang.IPSua = Request.UserHostAddress;
            bang.DuLieuMoi = false;
            bang.GiaTriKhoa = iID_MaHoSo;
            bang.CmdParams.Parameters.AddWithValue("@sSoTiepNhan", CTaoMaTiepNhan.GetSoTiepNhan());
            bang.CmdParams.Parameters.AddWithValue("@sUserTiepNhan", sUserName);
            bang.CmdParams.Parameters.AddWithValue("@dNgayTiepNhan", DateTime.Now);
            bang.CmdParams.Parameters.AddWithValue("@sTenNguoiTiepNhan", CPQ_NGUOIDUNG.Get_TenNguoiDung(sUserName));
            bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", 2);
            bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", 1);
            bang.Save();

            CLichSuHoSo.Add(Convert.ToInt64(iID_MaHoSo), sMaHoSo, sUserName, sTenUser, eDoiTuong.TYPE_2, "Bộ phận 1 cửa", eHanhDong.TYPE_1_2, "Tiếp nhận hồ sơ", "", "", eTrangThai.TYPE_1, "Chờ tiếp nhận", eTrangThai.TYPE_2, "Đã tiếp nhận");

            //Gui sang NSW
            PhanHoiDonXM resultConfirm = new PhanHoiDonXM();
            resultConfirm.NSWFileCode = sMaHoSo;
            resultConfirm.NameOfStaff = sTenUser;
            resultConfirm.ResponseDate = DateTime.Now;

            string error = _sendService.PhanHoiDonXM(sMaHoSo, resultConfirm);
            if (error == "99")
            {

                //bang.MaNguoiDungSua = sUserName;
                //bang.IPSua = Request.UserHostAddress;
                //bang.DuLieuMoi = false;
                //bang.GiaTriKhoa = iID_MaHoSo;
                //bang.CmdParams.Parameters.AddWithValue("@sSoTiepNhan", CTaoMaTiepNhan.GetSoTiepNhan());
                //bang.CmdParams.Parameters.AddWithValue("@sUserTiepNhan", sUserName);
                //bang.CmdParams.Parameters.AddWithValue("@dNgayTiepNhan", DateTime.Now);
                //bang.CmdParams.Parameters.AddWithValue("@sTenNguoiTiepNhan", CPQ_NGUOIDUNG.Get_TenNguoiDung(sUserName));
                //bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", 1);
                //bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", 0);
                //bang.Save();
                //CHoSo26.UpDate_TrangThai(Convert.ToInt64(iID_MaHoSo), 2);

                //CLichSuHoSo.Add(Convert.ToInt64(iID_MaHoSo), sMaHoSo, sUserName, sTenUser, eDoiTuong.TYPE_2, "Bộ phận 1 cửa", eHanhDong.TYPE_1_2, "Tiếp nhận hồ sơ", "", "", eTrangThai.TYPE_1, "Chờ tiếp nhận", eTrangThai.TYPE_2, "Đã tiếp nhận");
            }

            CHoSo26.CleanNguoiXem(iID_MaHoSo);

            return RedirectToAction("MienGiam_ChoTiepNhan", "BoPhanMotCua");
        }

        #region DaTiepNhan
        public ActionResult MienGiam_DaDuyet(CHoSoSearch hoSoSearch)
        {
            if (hoSoSearch == null || hoSoSearch.iID_MaTrangThai == 0)
            {
                hoSoSearch = new CHoSoSearch
                {
                    iID_MaTrangThai = 3,
                    Page = 1,
                    PageSize = Globals.PageSize
                };
            }

            return View(hoSoSearch);
        }

        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MienGiam_DaDuyet_Search(string ParentID)
        {
            string _sMaHoSo = CString.SafeString(Request.Form[ParentID + "_sMaHoSo"]);
            string _sTenDoanhNghiep = CString.SafeString(Request.Form[ParentID + "_sTenDoanhNghiep"]);
            string _sTenTACN = CString.SafeString(Request.Form[ParentID + "_sTenTACN"]);
            string _FromDate = CString.SafeString(Request.Form[ParentID + "_viTuNgayDen"]);
            string _ToDate = CString.SafeString(Request.Form[ParentID + "_viDenNgayDen"]);
            CHoSoSearch models = new CHoSoSearch
            {
                iID_MaTrangThai = 1,
                sMaHoSo = _sMaHoSo,
                sTenDoanhNghiep = _sTenDoanhNghiep,
                sTenTACN = _sTenTACN,
                TuNgayDen = _FromDate,
                DenNgayDen = _ToDate
            };
            return RedirectToAction("MienGiam_DaDuyet", "BoPhanMotCua", models);
        }

        public ActionResult MienGiam_DaDuyet_Detail(string iID_MaHoSo)
        {
            CHoSo26.UpdateNguoiXem(iID_MaHoSo, User.Identity.Name);

            HoSo26Models models = CHoSo26.Get_Detail(Convert.ToInt64(iID_MaHoSo));

            return View(models);
        }

        public ActionResult MienGiam_DaDuyet_Send(string iID_MaHoSo, string sMaHoSo)
        {
            String sUserName = User.Identity.Name;
            String sTenUser = CPQ_NGUOIDUNG.Get_TenNguoiDung(sUserName);
            CHoSo26.UpdateNguoiXem(iID_MaHoSo, sUserName);

            bang.MaNguoiDungSua = sUserName;
            bang.IPSua = Request.UserHostAddress;
            bang.DuLieuMoi = false;
            bang.GiaTriKhoa = iID_MaHoSo;
            bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", 4);
            bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", 3);
            bang.Save();

            CLichSuHoSo.Add(Convert.ToInt64(iID_MaHoSo), sMaHoSo, sUserName, sTenUser, eDoiTuong.TYPE_2, "Bộ phận 1 cửa", eHanhDong.TYPE_3_4, "Chuyển Doanh Nghiệp", "", "", eTrangThai.TYPE_3, "Lãnh đạo cục đã phê duyệt ", eTrangThai.TYPE_4, "Đã cấp công văn miễn giảm");

            ////Gui sang NSW
            //PhanHoiDonXM resultConfirm = new PhanHoiDonXM();
            //resultConfirm.NSWFileCode = sMaHoSo;
            //resultConfirm.NameOfStaff = sTenUser;
            //resultConfirm.ResponseDate = DateTime.Now;

            //string error = _sendService.PhanHoiDonXM(sMaHoSo, resultConfirm);
            //if (error == "99")
            //{

            //    bang.MaNguoiDungSua = sUserName;
            //    bang.IPSua = Request.UserHostAddress;
            //    bang.DuLieuMoi = false;
            //    bang.GiaTriKhoa = iID_MaHoSo;
            //    bang.CmdParams.Parameters.AddWithValue("@sSoTiepNhan", CTaoMaTiepNhan.GetSoTiepNhan());
            //    bang.CmdParams.Parameters.AddWithValue("@sUserTiepNhan", sUserName);
            //    bang.CmdParams.Parameters.AddWithValue("@dNgayTiepNhan", DateTime.Now);
            //    bang.CmdParams.Parameters.AddWithValue("@sTenNguoiTiepNhan", CPQ_NGUOIDUNG.Get_TenNguoiDung(sUserName));
            //    bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", 4);
            //    bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", 3);
            //    bang.Save();
            //    CHoSo26.UpDate_TrangThai(Convert.ToInt64(iID_MaHoSo), 2);

            //    CLichSuHoSo.Add(Convert.ToInt64(iID_MaHoSo), sMaHoSo, sUserName, sTenUser, eDoiTuong.TYPE_2, "Bộ phận 1 cửa", eHanhDong.TYPE_3_4, "Chuyển Doanh Nghiệp", "", "", eTrangThai.TYPE_3, "Lãnh đạo cục đã phê duyệt ", eTrangThai.TYPE_4, "Đã cấp công văn miễn giảm");
            //}

            CHoSo26.CleanNguoiXem(iID_MaHoSo);

            return RedirectToAction("MienGiam_DaDuyet", "BoPhanMotCua");
        }
        #endregion

        #region DaTraKQ
        public ActionResult MienGiam_DaTraKQ(CHoSoSearch hoSoSearch)
        {
            if (hoSoSearch == null || hoSoSearch.iID_MaTrangThai == 0)
            {
                hoSoSearch = new CHoSoSearch
                {
                    iID_MaTrangThai = 4,
                    Page = 1,
                    PageSize = Globals.PageSize
                };
            }

            return View(hoSoSearch);
        }

        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MienGiam_DaTraKQ_Search(string ParentID)
        {
            string _sMaHoSo = CString.SafeString(Request.Form[ParentID + "_sMaHoSo"]);
            string _sTenDoanhNghiep = CString.SafeString(Request.Form[ParentID + "_sTenDoanhNghiep"]);
            string _sTenTACN = CString.SafeString(Request.Form[ParentID + "_sTenTACN"]);
            string _FromDate = CString.SafeString(Request.Form[ParentID + "_viTuNgayDen"]);
            string _ToDate = CString.SafeString(Request.Form[ParentID + "_viDenNgayDen"]);
            CHoSoSearch models = new CHoSoSearch
            {
                iID_MaTrangThai = 1,
                sMaHoSo = _sMaHoSo,
                sTenDoanhNghiep = _sTenDoanhNghiep,
                sTenTACN = _sTenTACN,
                TuNgayDen = _FromDate,
                DenNgayDen = _ToDate
            };
            return RedirectToAction("MienGiam_DaTraKQ", "BoPhanMotCua", models);
        }

        public ActionResult MienGiam_DaTraKQ_Detail(string iID_MaHoSo)
        {
            CHoSo26.UpdateNguoiXem(iID_MaHoSo, User.Identity.Name);

            HoSo26Models models = CHoSo26.Get_Detail(Convert.ToInt64(iID_MaHoSo));

            return View(models);
        }

        public ActionResult MienGiam_DaTraKQ_ThuHoi(string iID_MaHoSo, string sMaHoSo)
        {
            String sUserName = User.Identity.Name;
            String sTenUser = CPQ_NGUOIDUNG.Get_TenNguoiDung(sUserName);
            CHoSo26.UpdateNguoiXem(iID_MaHoSo, sUserName);

            bang.MaNguoiDungSua = sUserName;
            bang.IPSua = Request.UserHostAddress;
            bang.DuLieuMoi = false;
            bang.GiaTriKhoa = iID_MaHoSo;
            bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", 4);
            bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", 3);
            bang.Save();

            CLichSuHoSo.Add(Convert.ToInt64(iID_MaHoSo), sMaHoSo, sUserName, sTenUser, eDoiTuong.TYPE_2, "Bộ phận 1 cửa", eHanhDong.TYPE_3_4, "Chuyển Doanh Nghiệp", "", "", eTrangThai.TYPE_3, "Lãnh đạo cục đã phê duyệt ", eTrangThai.TYPE_4, "Đã cấp công văn miễn giảm");

            ////Gui sang NSW
            //PhanHoiDonXM resultConfirm = new PhanHoiDonXM();
            //resultConfirm.NSWFileCode = sMaHoSo;
            //resultConfirm.NameOfStaff = sTenUser;
            //resultConfirm.ResponseDate = DateTime.Now;

            //string error = _sendService.PhanHoiDonXM(sMaHoSo, resultConfirm);
            //if (error == "99")
            //{

            //    bang.MaNguoiDungSua = sUserName;
            //    bang.IPSua = Request.UserHostAddress;
            //    bang.DuLieuMoi = false;
            //    bang.GiaTriKhoa = iID_MaHoSo;
            //    bang.CmdParams.Parameters.AddWithValue("@sSoTiepNhan", CTaoMaTiepNhan.GetSoTiepNhan());
            //    bang.CmdParams.Parameters.AddWithValue("@sUserTiepNhan", sUserName);
            //    bang.CmdParams.Parameters.AddWithValue("@dNgayTiepNhan", DateTime.Now);
            //    bang.CmdParams.Parameters.AddWithValue("@sTenNguoiTiepNhan", CPQ_NGUOIDUNG.Get_TenNguoiDung(sUserName));
            //    bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", 4);
            //    bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", 3);
            //    bang.Save();
            //    CHoSo26.UpDate_TrangThai(Convert.ToInt64(iID_MaHoSo), 2);

            //    CLichSuHoSo.Add(Convert.ToInt64(iID_MaHoSo), sMaHoSo, sUserName, sTenUser, eDoiTuong.TYPE_2, "Bộ phận 1 cửa", eHanhDong.TYPE_3_4, "Chuyển Doanh Nghiệp", "", "", eTrangThai.TYPE_3, "Lãnh đạo cục đã phê duyệt ", eTrangThai.TYPE_4, "Đã cấp công văn miễn giảm");
            //}

            CHoSo26.CleanNguoiXem(iID_MaHoSo);

            return RedirectToAction("MienGiam_DaTraKQ", "BoPhanMotCua");
        }
        #endregion
    }
}