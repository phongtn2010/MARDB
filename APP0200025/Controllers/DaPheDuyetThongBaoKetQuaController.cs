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
using System.Collections.Generic;
using System.Data;

namespace APP0200025.Controllers
{
    public class DaPheDuyetThongBaoKetQuaController : Controller
    {
        // GET: DaPheDuyetThongBaoKetQua
        Bang bang = new Bang("CNN25_HoSo");

        private string ViewPath = "~/Views/DaPheDuyetThongBaoKetQua/";
        private SendService _sendService = new SendService();
        // GET: ChoTiepNhanHoSo
        public ActionResult Index(sHoSoModels models)
        {
            if (models == null || models.LoaiDanhSach == 0)
            {
                models = new sHoSoModels
                {
                    LoaiDanhSach = (int)clLoaiDanhSach.From.DaPheDuyetThongBaoKetQua,
                    Page = 1,
                    PageSize = Globals.PageSize
                };
            }
            return View(models);
        }
        public ActionResult Detail(string iID_MaHangHoa)
        {
            //if (BaoMat.ChoPhepLamViec(User.Identity.Name, bang.TenBang, "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            //{
            //    return RedirectToAction("Index", "PermitionMessage");
            //}
            clHoSo.UpdateNguoiXem(iID_MaHangHoa, User.Identity.Name);

            HangHoaModels models = clHangHoa.GetHangHoaById(Convert.ToInt64(iID_MaHangHoa));

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
        //[Authorize, ValidateInput(false), HttpPost]
        public ActionResult GuiDoanhNghiep(String iID_MaHangHoa)
        {
            //string iID_MaHangHoa = Request.Form[ParentID + "_iID_MaHangHoa"];
            HangHoaModels hangHoa = clHangHoa.GetHangHoaById(Convert.ToInt64(iID_MaHangHoa));
            TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.ChuyenDNThongBaoKetQuaKiemTra, hangHoa.iID_MaTrangThai, hangHoa.iID_MaTrangThaiTruoc);
            HoSoModels hoSo = clHoSo.GetHoSoById(hangHoa.iID_MaHoSo);
            string error = "99";
            if (hoSo.iID_MaLoaiHoSo==3)//2c
            {
                String sMaCoQuanDanhGiaXNCL = "", sTenCoQuanDanhGiaXNCL = "", sGiayChungNhan_HopQuy = "";
                DateTime dNgayXacNhanHopQuy = DateTime.Now;
                DataTable dtHoSoXNCL = CHoSo.Get_HoSo_XNCL(hangHoa.iID_MaHoSo, hangHoa.iID_MaHangHoa);
                if(dtHoSoXNCL.Rows.Count > 0)
                {
                    sMaCoQuanDanhGiaXNCL = Convert.ToString(dtHoSoXNCL.Rows[0]["iID_MaToChuc"]);
                    sTenCoQuanDanhGiaXNCL = Convert.ToString(dtHoSoXNCL.Rows[0]["sTenToChuc"]);
                    sGiayChungNhan_HopQuy = Convert.ToString(dtHoSoXNCL.Rows[0]["sGiayChungNhan"]);
                    if(String.IsNullOrEmpty(Convert.ToString(dtHoSoXNCL.Rows[0]["dNgayCap"])) == false)
                    {
                        dNgayXacNhanHopQuy = Convert.ToDateTime(dtHoSoXNCL.Rows[0]["dNgayCap"]);
                    }
                    sTenCoQuanDanhGiaXNCL = Convert.ToString(dtHoSoXNCL.Rows[0]["sTenToChuc"]);
                }
                dtHoSoXNCL.Dispose();

                List<ContractList> lstHopDong = new List<ContractList>();
                ContractList itemHdong;
                DataTable dtHopDong = clHoSo.Get_ThongTin_HopDong(hoSo.iID_MaHoSo);
                if (dtHopDong.Rows.Count > 0)
                {
                    for (int i = 0; i < dtHopDong.Rows.Count; i++)
                    {
                        DateTime dNgayHD;
                        if(String.IsNullOrEmpty(Convert.ToString(dtHopDong.Rows[i]["dNgayHopDong"])) == false)
                        {
                            dNgayHD = Convert.ToDateTime(dtHopDong.Rows[i]["dNgayHopDong"]);
                        }
                        else
                        {
                            dNgayHD = DateTime.Now;
                        }

                        itemHdong = new ContractList
                        {
                            ContractNo = Convert.ToString(dtHopDong.Rows[i]["sHopDong"]),
                            ContractDate = dNgayHD
                        };
                        lstHopDong.Add(itemHdong);
                    }
                }

                List<InvoiceList> lstHoaDon = new List<InvoiceList>();
                InvoiceList itemHdon;
                DataTable dtHoaDon= clHoSo.Get_ThongTin_HoaDon(hoSo.iID_MaHoSo);
                if (dtHoaDon.Rows.Count > 0)
                {
                    for (int i = 0; i < dtHoaDon.Rows.Count; i++)
                    {
                        DateTime dNgayHD;
                        if (String.IsNullOrEmpty(Convert.ToString(dtHoaDon.Rows[i]["dNgayHopDong"])) == false)
                        {
                            dNgayHD = Convert.ToDateTime(dtHoaDon.Rows[i]["dNgayHopDong"]);
                        }
                        else
                        {
                            dNgayHD = DateTime.Now;
                        }

                        itemHdon = new InvoiceList
                        {
                            InvoiceNo = Convert.ToString(dtHoaDon.Rows[i]["sHopDong"]),
                            InvoiceDate = dNgayHD
                        };
                        lstHoaDon.Add(itemHdon);
                    }
                }

                //Gui sang NSW
                GiayXNCL resultConfirm = new GiayXNCL();
                resultConfirm.NSWFileCode = hoSo.sMaHoSo;
                resultConfirm.DepartmentCode = "10";
                resultConfirm.DepartmentName = "Cục chăn nuôi";
                resultConfirm.CerNumber = hangHoa.sSoThongBaoKetQua;
                resultConfirm.SignCerPlace = hangHoa.sSoThongBaoKetQua_NoiKy;
                resultConfirm.SignCerDate = DateTime.Now;
                resultConfirm.SignCerDate = hangHoa.dSoThongBaoKetQua_NgayKy;
                resultConfirm.ListHangHoa = clHangHoa.GetHoaGXNCL(hoSo.iID_MaHoSo);
                resultConfirm.PortOfDestinationName = hoSo.sMua_NoiNhan;
                resultConfirm.ImportingFromDate = hoSo.sMua_FromDate;
                resultConfirm.ImportingToDate = hoSo.sMua_ToDate;
                resultConfirm.ListContract = lstHopDong;
                resultConfirm.ListInvoice = lstHoaDon;
                resultConfirm.AniFeedConfirmNo = hoSo.sSoGDK;
                resultConfirm.SignConfirmDate = hoSo.dNgayXacNhan;
                resultConfirm.Buyer = hoSo.sMua_Name;
                resultConfirm.BuyerAddress = hoSo.sMua_DiaChi;
                resultConfirm.StandardBase = hangHoa.sSoHieu;
                resultConfirm.ResultTechnicalRegulations = hangHoa.sQuyChuan;
                resultConfirm.ImportCerNo = sGiayChungNhan_HopQuy;
                resultConfirm.AssignCode = sMaCoQuanDanhGiaXNCL;
                resultConfirm.AssignName = sTenCoQuanDanhGiaXNCL;
                resultConfirm.ImportCerDate = dNgayXacNhanHopQuy;
                resultConfirm.SignCerName = hangHoa.sSoThongBaoKetQua_NguoiKy;

                error = _sendService.GiayXNCL(hangHoa.sMaHoSo, resultConfirm);
            }
            if (error == "99")
            {

                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                //bang.TruyenGiaTri(ParentID, Request.Form);
                bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy ?? "");
                bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hangHoa.iID_MaTrangThai);
                bang.Save();
                clHangHoa.CleanNguoiXem(iID_MaHangHoa);
                clLichSuHangHoa.InsertLichSu(hangHoa.iID_MaHangHoa, User.Identity.Name, (int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.ChuyenDNThongBaoKetQuaKiemTra, "Chuyển doanh nghiệp thông báo kế quả kiểm tra", "", hangHoa.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);
            }

            return RedirectToAction("Index");
        }
        
      
        public JsonResult Thoat(string iID_MaHangHoa)
        {
            ResultModels result = clHoSo.CleanNguoiXem(iID_MaHangHoa);
            result.value = Url.Action("Index");
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Search(string ParentID)
        {
            string _sMaHoSo = CString.SafeString(Request.Form[ParentID + "_sMaHoSo"]);
            string _sTenDoanhNghiep = CString.SafeString(Request.Form[ParentID + "_sTenDoanhNghiep"]);
            string _sTenTACN = CString.SafeString(Request.Form[ParentID + "_sTenTACN"]);
            string _FromDate = CString.SafeString(Request.Form[ParentID + "_viFromDate"]);
            string _ToDate = CString.SafeString(Request.Form[ParentID + "_viToDate"]);
            sHoSoModels models = new sHoSoModels
            {
                LoaiDanhSach = (int)clLoaiDanhSach.From.DaPheDuyetThongBaoKetQua,
                sMaHoSo = _sMaHoSo,
                sTenDoanhNghiep = _sTenDoanhNghiep,
                sTenTACN = _sTenTACN,
                TuNgayDen = _FromDate,
                DenNgayDen = _ToDate
            };
            return RedirectToAction("Index", models);
        }
    }
}