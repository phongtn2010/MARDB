using DATA0200025;
using DATA0200025.Models;
using DATA0200025.WebServices;
using DATA0200025.WebServices.XmlType.Request;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APP0200025.Controllers
{
    public class TestController : Controller
    {
        private SendService _sendService = new SendService();
        // GET: Test
        public ActionResult Index()
        {
            String sTen = "tfsdfdsfsd";

            //long iID_MaFile = CDinhKem.ThemDinhKem(0, 0, 0, "", "", "", "PHONG", "", null, 1, "http://mard.adp-p.com/Files/16c8858e-0f58-4c2c-939f-f7ac40d8cf2f.png", "doanhnghiep", "");

            //KetQuaXuLy resultConfirm = new KetQuaXuLy();
            //resultConfirm.NSWFileCode = "BNNPTNT25200010180";
            //resultConfirm.Reason = "Đã tiêp nhận hồ sơ";
            //resultConfirm.AttachmentId = "01";
            //resultConfirm.FileName = "File Test";
            //resultConfirm.FileLink = "LinkFile";
            //resultConfirm.NameOfStaff = "PHONG PHẠM";
            //resultConfirm.ResponseDate = DateTime.Now;

            //string error = _sendService.KetQuaXuLy("BNNPTNT25200010180", resultConfirm, "06");

            //string sYeuCBS = _sendService.YeuCauBoSung("BNNPTNT25200010060", "Đã tiêp nhận hồ sơ", "PHONG PHẠM");

            //string iID_MaHangHoa = Request.Form[ParentID + "_iID_MaHangHoa"];
            HangHoaModels hangHoa = clHangHoa.GetHangHoaById(Convert.ToInt32(10181));
            TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.ChuyenDNThongBaoKetQuaKiemTra, hangHoa.iID_MaTrangThai, hangHoa.iID_MaTrangThaiTruoc);
            HoSoModels hoSo = clHoSo.GetHoSoById(hangHoa.iID_MaHoSo);
            string error = "99";
            if (hoSo.iID_MaLoaiHoSo == 3)//2c
            {
                List<ContractList> lstHopDong = new List<ContractList>();
                ContractList itemHdong;
                DataTable dtHopDong = clHoSo.Get_ThongTin_HopDong(hoSo.iID_MaHoSo);
                if (dtHopDong.Rows.Count > 0)
                {
                    for (int i = 0; i < dtHopDong.Rows.Count; i++)
                    {
                        DateTime dNgayHD;
                        if (String.IsNullOrEmpty(Convert.ToString(dtHopDong.Rows[i]["dNgayHopDong"])) == false)
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
                DataTable dtHoaDon = clHoSo.Get_ThongTin_HoaDon(hoSo.iID_MaHoSo);
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
                resultConfirm.AniFeedConfirmNo = "ABC";
                resultConfirm.SignConfirmDate = DateTime.Now;
                resultConfirm.Buyer = hoSo.sMua_Name;
                resultConfirm.BuyerAddress = hoSo.sMua_DiaChi;
                resultConfirm.StandardBase = hangHoa.sSoHieu;
                resultConfirm.ResultTechnicalRegulations = hangHoa.sQuyChuan;
                resultConfirm.ImportCerNo = "GCN";
                resultConfirm.AssignCode = "10";
                resultConfirm.AssignName = "CNN";
                resultConfirm.ImportCerDate = DateTime.Now;
                resultConfirm.SignCerName = "PHONG PHAM";

                error = _sendService.GiayXNCL(hangHoa.sMaHoSo, resultConfirm);
            }
            if (error == "99")
            {

                //bang.MaNguoiDungSua = User.Identity.Name;
                //bang.IPSua = Request.UserHostAddress;
                ////bang.TruyenGiaTri(ParentID, Request.Form);
                //bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy ?? "");
                //bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                //bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                //bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hangHoa.iID_MaTrangThai);
                //bang.Save();
                //clHangHoa.CleanNguoiXem(iID_MaHangHoa);
                //clLichSuHangHoa.InsertLichSu(hangHoa.iID_MaHangHoa, User.Identity.Name, (int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.ChuyenDNThongBaoKetQuaKiemTra, "Chuyển doanh nghiệp thông báo kế quả kiểm tra", "", hangHoa.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);
            }

            if (string.IsNullOrEmpty(error))
            {
                //_hoSoService.Save();
                //_lichSuHoSoService.Save();
                //return request.CreateResponse(HttpStatusCode.OK, new SimpleResponse()
                //{
                //    message = "Đã gửi yêu cầu bổ sung thành công",
                //    data = null
                //});
            }
            else
            {
                //return request.CreateResponse(HttpStatusCode.InternalServerError, new SimpleResponse()
                //{
                //    message = error,
                //    data = null
                //});
            }

            return View();
        }
    }
}