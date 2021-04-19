using DATA0200025;
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

namespace APP0200025.Controllers
{
    public class HoSoDaPheDuyetGDKController : Controller
    {
        // Đã phê duyệt GĐK
        Bang bang = new Bang("CNN25_HoSo");

        private string ViewPath = "~/Views/HoSoDaPheDuyetGDK/";

        private SendService _sendService = new SendService();
        // GET: HoSoDaPheDuyetGDK
        public ActionResult Index(sHoSoModels models)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, bang.TenBang, "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            if (models == null || models.LoaiDanhSach == 0)
            {
                models = new sHoSoModels
                {
                    LoaiDanhSach = (int)clLoaiDanhSach.From.BPMC_DaPheDuyetGDK,
                    Page = 1,
                    PageSize = Globals.PageSize
                };
            }

            ViewData["menu"] = 233;
            return View(models);
        }
        public ActionResult Detail(string iID_MaHoSo)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, bang.TenBang, "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            clHoSo.UpdateNguoiXem(iID_MaHoSo, User.Identity.Name);
            
            HoSoModels models = clHoSo.GetHoSoById(Convert.ToInt32(iID_MaHoSo));

            ViewData["menu"] = 233;
            return View(models);
        }
    
        /// <summary>
        /// update data màn hình TiepNhanHoSo
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="MaHoSo"></param>
        /// <returns></returns>
        public ActionResult GuiDoanhNghiep(String iID_MaHoSo)
        {
            //string iID_MaHoSo = Request.Form[ParentID + "_iID_MaHoSo"];
            if (String.IsNullOrEmpty(iID_MaHoSo) == false)
            {
                HoSoModels hoSo = clHoSo.GetHoSoById(Convert.ToInt64(iID_MaHoSo));
                TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.ChuyenDNPhuLucGDK, hoSo.iID_MaTrangThai, hoSo.iID_MaTrangThaiTruoc);

                string error = "";
                if(CHamRieng.iNSW ==1)
                {

                    //XML 13(11)
                    XacNhanDon resultConfirm = new XacNhanDon();
                    resultConfirm.NSWFileCode = hoSo.sMaHoSo;
                    resultConfirm.AniFeedConfirmNo = hoSo.sSoGDK;
                    resultConfirm.DepartmentCode = hoSo.sCoQuanXuLy_Ma;
                    resultConfirm.DepartmentName = hoSo.sCoQuanXuLy_Ten;
                    resultConfirm.ImportingFromDateString = hoSo.sMua_FromDate;
                    resultConfirm.ImportingToDate = hoSo.sMua_ToDate;
                    resultConfirm.ImportingFromDateString = hoSo.sMua_FromDate;
                    resultConfirm.ImportingToDate = hoSo.sMua_ToDate;
                    resultConfirm.AssignID = "";
                    resultConfirm.AssignName = "";
                    resultConfirm.AssignNameOther = "";
                    resultConfirm.SignConfirmDateString = hoSo.dNgayXacNhan;
                    resultConfirm.SignConfirmPlace = "Hà nội";
                    resultConfirm.SignConfirmName = "Lãnh đạo cục";
                    resultConfirm.NSWFileCodeOld = hoSo.sMaHoSo_ThayThe;
                    resultConfirm.AniFeedConfirmOldNo = hoSo.sSoXacNhan_ThayThe;
                    resultConfirm.ListHangHoa = clHangHoa.GetHoaXND(hoSo.iID_MaHoSo);
                    if (hoSo.iID_MaLoaiHoSo == 1)
                        resultConfirm.NoteGoods = @"Lưu ý: Trong thời hạn 15 ngày làm việc kể từ ngày thông quan hàng hóa,
                                            người nhập khẩu phải nộp kết quả tự đánh giá sự phù hợp theo quy định 
                                            về Cục Chăn nuôi thông qua hệ thống Một cửa Quốc gia.";
                    if (hoSo.iID_MaLoaiHoSo == 2)
                        resultConfirm.NoteGoods = @"Lưu ý: Trong thời hạn 15 ngày làm việc kể từ ngày thông quan hàng hóa,
                                        người nhập khẩu phải nộp bản sao ý bản chính (có ký tên và đóng dấu của người nhập khẩu) 
                                        Giấy chứng nhận hợp quy lô hàng thức ăn chăn nuôi nhập khẩu theo quy định về Cục Chăn nuôi thông qua hệ thống Một cửa Quốc gia.";
                    error = _sendService.XacNhanDon(hoSo.sMaHoSo, resultConfirm, hoSo.sHashCode);
                }
                else
                {
                    error = "99";
                }

                if (error.Equals("99"))
                {
                    bang.MaNguoiDungSua = User.Identity.Name;
                    bang.IPSua = Request.UserHostAddress;
                    bang.DuLieuMoi = false;
                    bang.GiaTriKhoa = iID_MaHoSo;
                    bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy);
                    bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hoSo.iID_MaTrangThai);
                    bang.Save();
                    clLichSuHoSo.InsertLichSu(hoSo.iID_MaHoSo, User.Identity.Name, (int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.ChuyenDNPhuLucGDK, "Chuyển phụ lục GĐK", "", hoSo.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);

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

        public ActionResult Thoat(String iID_MaHoSo)
        {
            ResultModels result = clHoSo.CleanNguoiXem(iID_MaHoSo);

            TempData["msg"] = "success";
            return RedirectToAction("Index");
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
                LoaiDanhSach = 5,
                sMaHoSo = _sMaHoSo,
                sTenDoanhNghiep = _sTenDoanhNghiep,
                sTenTACN = _sTenTACN,
                TuNgayDen = _TuNgayDen,
                DenNgayDen = _DenNgayDen,
                TuNgayTiepNhan= TuNgayTiepNhan,
                DenNgayTiepNhan = DenNgayTiepNhan,
                sSoTiepNhan= sSoTiepNhan
            };

            TempData["menu"] = 233;
            TempData["msg"] = "success";

            return RedirectToAction("Index", models);
        }
    }
}