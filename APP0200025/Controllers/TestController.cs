using APP0200025.Controllers.Api;
using APP0200025.Models;
using DATA0200025;
using DATA0200025.Models;
using DATA0200025.WebServices;
using DATA0200025.WebServices.XmlType.Request;
using DomainModel;
using DomainModel.Abstract;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
            //String sTen = "http://mardapi.adp-p.com/Uploads/Files/2020/12/16/63b22811-5af5-4c35-96a8-443e70cd96bc.pdf";

            //byte[] arrFile = ReadFile(sTen);
 
            //HttpPostedFileBase file = Request.Files[0];

            //string result = string.Empty;

            //using (BinaryReader b = new BinaryReader(file.InputStream))
            //{
            //    byte[] binData = b.ReadBytes(file.ContentLength);
            //    result = System.Text.Encoding.UTF8.GetString(binData);
            //}

            //long iID_MaFile = CDinhKem.ThemDinhKem(0, 0, 0, "", "", "", "PHONG", "", null, 1, "http://mard.adp-p.com/Files/16c8858e-0f58-4c2c-939f-f7ac40d8cf2f.png", "doanhnghiep", "");

            //KetQuaXuLy resultConfirm = new KetQuaXuLy();
            //resultConfirm.NSWFileCode = "BNNPTNT25200010180";
            //resultConfirm.Reason = "Đã tiêp nhận hồ sơ";
            //resultConfirm.AttachmentId = "01";
            //resultConfirm.FileName = "File Test";
            //resultConfirm.FileLink = "LinkFile";
            //resultConfirm.NameOfStaff = "PHONG PHẠM";
            //resultConfirm.ResponseDateString = DateTime.Now;

            //string error = _sendService.KetQuaXuLy("BNNPTNT25200010180", resultConfirm, "06");

            //string sYeuCBS = _sendService.YeuCauBoSung("BNNPTNT25200010060", "Đã tiêp nhận hồ sơ", "PHONG PHẠM");


            ////string iID_MaHangHoa = Request.Form[ParentID + "_iID_MaHangHoa"];
            //HangHoaModels hangHoa = clHangHoa.GetHangHoaById(Convert.ToInt32(10181));
            //TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.ChuyenDNThongBaoKetQuaKiemTra, hangHoa.iID_MaTrangThai, hangHoa.iID_MaTrangThaiTruoc);
            //HoSoModels hoSo = clHoSo.GetHoSoById(hangHoa.iID_MaHoSo);
            //string error = "99";
            //if (hoSo.iID_MaLoaiHoSo == 3)//2c
            //{
            //    List<ContractList> lstHopDong = new List<ContractList>();
            //    ContractList itemHdong;
            //    DataTable dtHopDong = clHoSo.Get_ThongTin_HopDong(hoSo.iID_MaHoSo);
            //    if (dtHopDong.Rows.Count > 0)
            //    {
            //        for (int i = 0; i < dtHopDong.Rows.Count; i++)
            //        {
            //            DateTime dNgayHD;
            //            if (String.IsNullOrEmpty(Convert.ToString(dtHopDong.Rows[i]["dNgayHopDong"])) == false)
            //            {
            //                dNgayHD = Convert.ToDateTime(dtHopDong.Rows[i]["dNgayHopDong"]);
            //            }
            //            else
            //            {
            //                dNgayHD = DateTime.Now;
            //            }

            //            itemHdong = new ContractList
            //            {
            //                ContractNo = Convert.ToString(dtHopDong.Rows[i]["sHopDong"]),
            //                ContractDateString = dNgayHD
            //            };
            //            lstHopDong.Add(itemHdong);
            //        }
            //    }

            //    List<InvoiceList> lstHoaDon = new List<InvoiceList>();
            //    InvoiceList itemHdon;
            //    DataTable dtHoaDon = clHoSo.Get_ThongTin_HoaDon(hoSo.iID_MaHoSo);
            //    if (dtHoaDon.Rows.Count > 0)
            //    {
            //        for (int i = 0; i < dtHoaDon.Rows.Count; i++)
            //        {
            //            DateTime dNgayHD;
            //            if (String.IsNullOrEmpty(Convert.ToString(dtHoaDon.Rows[i]["dNgayHopDong"])) == false)
            //            {
            //                dNgayHD = Convert.ToDateTime(dtHoaDon.Rows[i]["dNgayHopDong"]);
            //            }
            //            else
            //            {
            //                dNgayHD = DateTime.Now;
            //            }

            //            itemHdon = new InvoiceList
            //            {
            //                InvoiceNo = Convert.ToString(dtHoaDon.Rows[i]["sHopDong"]),
            //                InvoiceDateString = dNgayHD
            //            };
            //            lstHoaDon.Add(itemHdon);
            //        }
            //    }

            //    //Gui sang NSW
            //    GiayXNCL resultConfirm = new GiayXNCL();
            //    resultConfirm.NSWFileCode = hoSo.sMaHoSo;
            //    resultConfirm.DepartmentCode = "10";
            //    resultConfirm.DepartmentName = "Cục chăn nuôi";
            //    resultConfirm.CerNumber = hangHoa.sSoThongBaoKetQua;
            //    resultConfirm.SignCerPlace = hangHoa.sSoThongBaoKetQua_NoiKy;
            //    resultConfirm.SignCerDateString = hangHoa.dSoThongBaoKetQua_NgayKy;
            //    resultConfirm.ListHangHoa = clHangHoa.GetHoaGXNCL(hangHoa);
            //    resultConfirm.PortOfDestinationName = hoSo.sMua_NoiNhan;
            //    resultConfirm.ImportingFromDateString = hoSo.sMua_FromDate;
            //    resultConfirm.ImportingToDateString = hoSo.sMua_ToDate;
            //    resultConfirm.ListContract = lstHopDong;
            //    resultConfirm.ListInvoice = lstHoaDon;
            //    resultConfirm.AniFeedConfirmNo = "ABC";
            //    resultConfirm.SignConfirmDateString = DateTime.Now;
            //    resultConfirm.Buyer = hoSo.sMua_Name;
            //    resultConfirm.BuyerAddress = hoSo.sMua_DiaChi;
            //    resultConfirm.StandardBase = hangHoa.sSoHieu;
            //    resultConfirm.ResultTechnicalRegulations = hangHoa.sQuyChuan;
            //    resultConfirm.ImportCerNo = "GCN";
            //    resultConfirm.AssignCode = "10";
            //    resultConfirm.AssignName = "CNN";
            //    resultConfirm.ImportCerDateString = DateTime.Now;
            //    resultConfirm.SignCerName = "PHONG PHAM";

            //    error = _sendService.GiayXNCL(hangHoa.sMaHoSo, resultConfirm);
            //}
            //if (error == "99")
            //{

            //    //bang.MaNguoiDungSua = User.Identity.Name;
            //    //bang.IPSua = Request.UserHostAddress;
            //    ////bang.TruyenGiaTri(ParentID, Request.Form);
            //    //bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy ?? "");
            //    //bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
            //    //bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
            //    //bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hangHoa.iID_MaTrangThai);
            //    //bang.Save();
            //    //clHangHoa.CleanNguoiXem(iID_MaHangHoa);
            //    //clLichSuHangHoa.InsertLichSu(hangHoa.iID_MaHangHoa, User.Identity.Name, (int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.ChuyenDNThongBaoKetQuaKiemTra, "Chuyển doanh nghiệp thông báo kế quả kiểm tra", "", hangHoa.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);
            //}

            //if (string.IsNullOrEmpty(error))
            //{
            //    //_hoSoService.Save();
            //    //_lichSuHoSoService.Save();
            //    //return request.CreateResponse(HttpStatusCode.OK, new SimpleResponse()
            //    //{
            //    //    message = "Đã gửi yêu cầu bổ sung thành công",
            //    //    data = null
            //    //});
            //}
            //else
            //{
            //    //return request.CreateResponse(HttpStatusCode.InternalServerError, new SimpleResponse()
            //    //{
            //    //    message = error,
            //    //    data = null
            //    //});
            //}

            return View();
        }

        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult Edit()
        {
            NameValueCollection values = new NameValueCollection();
            string ParentID = "Edit";
            String ctlFNName = ParentID + "_File";

            if (values.Count > 0)
            {

                return View();
            }
            else
            {
                HttpPostedFileBase hpf = Request.Files[ctlFNName] as HttpPostedFileBase;

                ResDinhKemFiles vR = new ResDinhKemFiles();

                vR = CDinhKemFiles.UploadFile(hpf);
            }
            return base.RedirectToAction("Index");
        }

        public static byte[] ReadFile(string filePath)
        {
            byte[] buffer;
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;  // get file length
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }


        //Step 1 - Use NameSpace "using Microsoft.SharePoint.Client;" after adding reference in your application.
        //PM> Install-Package Microsoft.SharePoint.Client.dll
        //Step 2 - Write code to authenticate from SharePoint in asp.net.
        //string userName = Your username;  
        //string password = Your password;  
        //string baseurl = "Sharepoint base URL";
        //Uri uri = new Uri(baseurl);
        //var securePassword = new SecureString();  
        //foreach (var c in password) { securePassword.AppendChar(c); }
        //var credentials = new SharePointOnlineCredentials(userName, securePassword);
        //var authCookie = credentials.GetAuthenticationCookie(uri);
        //var fedAuthString = authCookie.TrimStart("SPOIDCRL=".ToCharArray());  
        //if (fedAuthString != null)  
        //{  
        //    result.UserToken = fedAuthString;  
        //    result.Message = "User has been logged in successfully";  
        //}
        //else
        //{
        //    result.Message = "Invalid User!!";

        //}  


        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult SendXML1513()
        {
            NameValueCollection values = new NameValueCollection();
            string ParentID = "Edit";

            String iID_MaHoSo = CString.SafeString(Request.Form[ParentID + "_iID_MaHoSo"]);

            //string iID_MaHoSo = Request.Form[ParentID + "_iID_MaHoSo"];
            if (String.IsNullOrEmpty(iID_MaHoSo) == false)
            {
                HoSoModels hoSo = clHoSo.GetHoSoById(Convert.ToInt64(iID_MaHoSo));
                TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.ChuyenDNPhuLucGDK, hoSo.iID_MaTrangThai, hoSo.iID_MaTrangThaiTruoc);

                string error = "";
                if (CHamRieng.iNSW == 1)
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
                    resultConfirm.SignConfirmPlace = eCoQuanXuLy.sNguoiKy_NoiKy;
                    resultConfirm.SignConfirmName = eCoQuanXuLy.sNguoiKy_Ten;
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

            return base.RedirectToAction("Index");
        }

        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult DeleteFileCapcha()
        {
            NameValueCollection values = new NameValueCollection();
            string ParentID = "Delete";

            try
            {
                DirectoryInfo directory = new DirectoryInfo(Server.MapPath("~/Uploads/CapCha/"));
                EmptyFolder(directory);

                TempData["msg"] = "success";
            }
            catch(Exception ex)
            {
                TempData["msg"] = "error";
            }

            return base.RedirectToAction("Index");
        }

        private void EmptyFolder(DirectoryInfo directory)
        {
            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();
            }

            foreach (DirectoryInfo subdirectory in directory.GetDirectories())
            {
                EmptyFolder(subdirectory);
                subdirectory.Delete();
            }
        }
    }
}