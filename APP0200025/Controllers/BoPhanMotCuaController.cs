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
using System.Collections.Specialized;
using APP0200025.Models;
using FlexCel.Core;
using System.IO;
using System.Data;
using FlexCel.Report;
using FlexCel.XlsAdapter;
using FlexCel.Render;

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

        public ActionResult Detail(string iID_MaHoSo)
        {
            CHoSo26.UpdateNguoiXem(iID_MaHoSo, User.Identity.Name);

            HoSo26Models models = CHoSo26.Get_Detail(Convert.ToInt64(iID_MaHoSo));

            ViewData["menu"] = 224;
            return View(models);
        }

        public ActionResult List(CHoSoSearch hoSoSearch)
        {
            if (hoSoSearch == null || hoSoSearch.iID_MaTrangThai == 0)
            {
                hoSoSearch = new CHoSoSearch
                {
                    iID_MaTrangThai = -1,
                    Page = 1,
                    PageSize = Globals.PageSize
                };
            }

            ViewData["menu"] = 224;
            return View(hoSoSearch);
        }

        public ActionResult List_Detail(string iID_MaHoSo)
        {
            CHoSo26.UpdateNguoiXem(iID_MaHoSo, User.Identity.Name);

            HoSo26Models models = CHoSo26.Get_Detail(Convert.ToInt64(iID_MaHoSo));

            ViewData["menu"] = 224;
            return View(models);
        }

        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Search(string ParentID)
        {
            string _sMaHoSo = CString.SafeString(Request.Form[ParentID + "_sMaHoSo"]).Trim();
            string _sTenDoanhNghiep = CString.SafeString(Request.Form[ParentID + "_sTenDoanhNghiep"]).Trim();
            string _sTenTACN = CString.SafeString(Request.Form[ParentID + "_sTenTACN"]).Trim();
            string _FromDate = CString.SafeString(Request.Form[ParentID + "_viTuNgayDen"]);
            string _ToDate = CString.SafeString(Request.Form[ParentID + "_viDenNgayDen"]);
            string _sSoTiepNhan = CString.SafeString(Request.Form[ParentID + "_sSoTiepNhan"]).Trim();
            string _TuNgayTiepNhan = CString.SafeString(Request.Form[ParentID + "_viTuNgayTiepNhan"]);
            string _DenNgayTiepNhan = CString.SafeString(Request.Form[ParentID + "_viDenNgayTiepNhan"]);
            string _sSoGDK = CString.SafeString(Request.Form[ParentID + "_sSoGDK"]).Trim();
            string _TuNgayXacNhan = CString.SafeString(Request.Form[ParentID + "_viTuNgayXacNhan"]);
            string _DenNgayXacNhan = CString.SafeString(Request.Form[ParentID + "_viDenNgayXacNhan"]);
            string _TrangThai = CString.SafeString(Request.Form[ParentID + "_iID_MaTrangThai"]);
            CHoSoSearch models = new CHoSoSearch
            {
                iID_MaTrangThai = Convert.ToInt32(_TrangThai),
                sMaHoSo = _sMaHoSo,
                sTenDoanhNghiep = _sTenDoanhNghiep,
                sTenTACN = _sTenTACN,
                TuNgayDen = _FromDate,
                DenNgayDen = _ToDate,
                sSoTiepNhan = _sSoTiepNhan,
                TuNgayTiepNhan = _TuNgayTiepNhan,
                DenNgayTiepNhan = _DenNgayTiepNhan,
                sSoGDK = _sSoGDK,
                TuNgayXacNhan = _TuNgayXacNhan,
                DenNgayXacNhan = _DenNgayXacNhan
            };

            TempData["menu"] = 224;
            TempData["msg"] = "success";
            return RedirectToAction("List", "BoPhanMotCua", models);
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

            ViewData["menu"] = 224;
            return View(hoSoSearch);
        }

        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MienGiam_ChoTiepNhan_Search(string ParentID)
        {
            string _sMaHoSo = CString.SafeString(Request.Form[ParentID + "_sMaHoSo"]).Trim();
            string _sTenDoanhNghiep = CString.SafeString(Request.Form[ParentID + "_sTenDoanhNghiep"]).Trim();
            string _sTenTACN = CString.SafeString(Request.Form[ParentID + "_sTenTACN"]).Trim();
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

            TempData["menu"] = 224;
            TempData["msg"] = "success";
            return RedirectToAction("MienGiam_ChoTiepNhan", "BoPhanMotCua", models);
        }

        public ActionResult MienGiam_ChoTiepNhan_Detail(string iID_MaHoSo)
        {
            CHoSo26.UpdateNguoiXem(iID_MaHoSo, User.Identity.Name);
            
            HoSo26Models models = CHoSo26.Get_Detail(Convert.ToInt64(iID_MaHoSo));

            ViewData["menu"] = 224;
            return View(models);
        }

        public ActionResult MienGiam_ChoTiepNhan_TiepNhan(string iID_MaHoSo, string sMaHoSo)
        {
            String sUserName = User.Identity.Name;
            String sTenUser = CPQ_NGUOIDUNG.Get_TenNguoiDung(sUserName);

            if (String.IsNullOrEmpty(iID_MaHoSo) == false)
            {
                CHoSo26.UpdateNguoiXem(iID_MaHoSo, sUserName);

                string error = "";
                if(CHamRieng.iNSW == 1)
                {
                    //XML13(01)
                    PhanHoiDonXM resultConfirm = new PhanHoiDonXM();
                    resultConfirm.NSWFileCode = sMaHoSo;
                    resultConfirm.NameOfStaff = sTenUser;
                    resultConfirm.ResponseDateString = DateTime.Now;

                    error = _sendService.PhanHoiDonXM(sMaHoSo, resultConfirm);
                }
                else
                {
                    error = "99";
                }
                
                if (error == "99")
                {
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

            ViewData["menu"] = 224;
            return View(hoSoSearch);
        }

        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MienGiam_DaDuyet_Search(string ParentID)
        {
            string _sMaHoSo = CString.SafeString(Request.Form[ParentID + "_sMaHoSo"]).Trim();
            string _sTenDoanhNghiep = CString.SafeString(Request.Form[ParentID + "_sTenDoanhNghiep"]).Trim();
            string _sTenTACN = CString.SafeString(Request.Form[ParentID + "_sTenTACN"]).Trim();
            string _TuNgayDen = CString.SafeString(Request.Form[ParentID + "_viTuNgayDen"]);
            string _DenNgayDen = CString.SafeString(Request.Form[ParentID + "_viDenNgayDen"]);
            string _sSoTiepNhan = CString.SafeString(Request.Form[ParentID + "_sTenDoanhNghiep"]).Trim();
            string _TuNgayTiepNhan = CString.SafeString(Request.Form[ParentID + "_viTuNgayTiepNhan"]);
            string _DenNgayTiepNhan = CString.SafeString(Request.Form[ParentID + "_viDenNgayTiepNhan"]);
            string _sSoGDK = CString.SafeString(Request.Form[ParentID + "_sSoGDK"]).Trim();
            string _TuNgayXacNhan = CString.SafeString(Request.Form[ParentID + "_viTuNgayXacNhan"]);
            string _DenNgayXacNhan = CString.SafeString(Request.Form[ParentID + "_viDenNgayXacNhan"]);
            CHoSoSearch models = new CHoSoSearch
            {
                iID_MaTrangThai = 3,
                sMaHoSo = _sMaHoSo,
                sTenDoanhNghiep = _sTenDoanhNghiep,
                sTenTACN = _sTenTACN,
                TuNgayDen = _TuNgayDen,
                DenNgayDen = _DenNgayDen,
                sSoTiepNhan = _sSoTiepNhan,
                TuNgayTiepNhan = _TuNgayTiepNhan,
                DenNgayTiepNhan = _DenNgayTiepNhan,
                sSoGDK = _sSoGDK,
                TuNgayXacNhan = _TuNgayXacNhan,
                DenNgayXacNhan = _DenNgayXacNhan
            };

            TempData["menu"] = 224;
            TempData["msg"] = "success";
            return RedirectToAction("MienGiam_DaDuyet", "BoPhanMotCua", models);
        }

        public ActionResult MienGiam_DaDuyet_Detail(string iID_MaHoSo)
        {
            CHoSo26.UpdateNguoiXem(iID_MaHoSo, User.Identity.Name);

            HoSo26Models models = CHoSo26.Get_Detail(Convert.ToInt64(iID_MaHoSo));

            ViewData["menu"] = 224;
            return View(models);
        }

        public ActionResult MienGiam_DaDuyet_Send(string iID_MaHoSo, string sMaHoSo)
        {
            String sUserName = User.Identity.Name;
            String sTenUser = CPQ_NGUOIDUNG.Get_TenNguoiDung(sUserName);
            if (String.IsNullOrEmpty(iID_MaHoSo) == false)
            {
                CHoSo26.UpdateNguoiXem(iID_MaHoSo, sUserName);

                HoSo26Models hs = CHoSo26.Get_Detail(Convert.ToInt64(iID_MaHoSo));
                if(hs != null)
                {
                    string error = "";
                    if (CHamRieng.iNSW == 1)
                    {
                        //XML 12(04)
                        CVMienKiem resultConfirm = new CVMienKiem();
                        resultConfirm.NSWFileCode = hs.sMaHoSo;
                        resultConfirm.ConfirmApplicationNo = hs.sSoGDK;
                        resultConfirm.Organization = hs.sTenDoanhNghiep;
                        resultConfirm.SignConfirmDateString = hs.dNgayXacNhan;
                        resultConfirm.SignConfirmPlace = hs.sSoGDK_NoiKy;
                        resultConfirm.SignDateString = hs.dNgayTaoHoSo;
                        resultConfirm.DepartmentCode = hs.sMaCoQuanXuLy;
                        resultConfirm.DepartmentName = hs.sTenCoQuanXuLy;

                        resultConfirm.ListHangHoa = CHangHoa26.GetHoaXND(Convert.ToInt64(iID_MaHoSo));

                        resultConfirm.PeriodFromString = hs.dNgayXacNhan;
                        resultConfirm.PeriodToString = hs.dNgayHetHieuLuc;
                        resultConfirm.SignName = hs.sSoGDK_NguoiKy;

                        error = _sendService.CVMienKiem(hs.sMaHoSo, resultConfirm);
                    }
                    else
                    {
                        error = "99";
                    }
                    
                    if (error == "99")
                    {
                        bang.MaNguoiDungSua = sUserName;
                        bang.IPSua = Request.UserHostAddress;
                        bang.DuLieuMoi = false;
                        bang.GiaTriKhoa = iID_MaHoSo;
                        bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", 4);
                        bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", 3);
                        bang.Save();

                        CLichSuHoSo.Add(Convert.ToInt64(iID_MaHoSo), sMaHoSo, sUserName, sTenUser, eDoiTuong.TYPE_2, "Bộ phận 1 cửa", eHanhDong.TYPE_3_4, "Chuyển Doanh Nghiệp", "", "", eTrangThai.TYPE_3, "Lãnh đạo cục đã phê duyệt ", eTrangThai.TYPE_4, "Đã cấp công văn miễn giảm");

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
            }
            else
            {
                TempData["msg"] = "error";
            }    

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

            ViewData["menu"] = 224;
            return View(hoSoSearch);
        }

        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MienGiam_DaTraKQ_Search(string ParentID)
        {
            string _sMaHoSo = CString.SafeString(Request.Form[ParentID + "_sMaHoSo"]).Trim();
            string _sTenDoanhNghiep = CString.SafeString(Request.Form[ParentID + "_sTenDoanhNghiep"]).Trim();
            string _sTenTACN = CString.SafeString(Request.Form[ParentID + "_sTenTACN"]).Trim();
            string _TuNgayDen = CString.SafeString(Request.Form[ParentID + "_viTuNgayDen"]);
            string _DenNgayDen = CString.SafeString(Request.Form[ParentID + "_viDenNgayDen"]);
            string _sSoTiepNhan = CString.SafeString(Request.Form[ParentID + "_sTenDoanhNghiep"]).Trim();
            string _TuNgayTiepNhan = CString.SafeString(Request.Form[ParentID + "_viTuNgayTiepNhan"]);
            string _DenNgayTiepNhan = CString.SafeString(Request.Form[ParentID + "_viDenNgayTiepNhan"]);
            string _sSoGDK = CString.SafeString(Request.Form[ParentID + "_sSoGDK"]).Trim();
            string _TuNgayXacNhan = CString.SafeString(Request.Form[ParentID + "_viTuNgayXacNhan"]);
            string _DenNgayXacNhan = CString.SafeString(Request.Form[ParentID + "_viDenNgayXacNhan"]);
            CHoSoSearch models = new CHoSoSearch
            {
                iID_MaTrangThai = 4,
                sMaHoSo = _sMaHoSo,
                sTenDoanhNghiep = _sTenDoanhNghiep,
                sTenTACN = _sTenTACN,
                TuNgayDen = _TuNgayDen,
                DenNgayDen = _DenNgayDen,
                sSoTiepNhan = _sSoTiepNhan,
                TuNgayTiepNhan = _TuNgayTiepNhan,
                DenNgayTiepNhan = _DenNgayTiepNhan,
                sSoGDK = _sSoGDK,
                TuNgayXacNhan = _TuNgayXacNhan,
                DenNgayXacNhan = _DenNgayXacNhan
            };

            TempData["menu"] = 224;
            TempData["msg"] = "success";
            return RedirectToAction("MienGiam_DaTraKQ", "BoPhanMotCua", models);
        }

        public ActionResult MienGiam_DaTraKQ_Detail(string iID_MaHoSo)
        {
            CHoSo26.UpdateNguoiXem(iID_MaHoSo, User.Identity.Name);

            HoSo26Models models = CHoSo26.Get_Detail(Convert.ToInt64(iID_MaHoSo));

            ViewData["menu"] = 224;
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
            bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", 6);
            bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", 5);
            bang.Save();

            CLichSuHoSo.Add(Convert.ToInt64(iID_MaHoSo), sMaHoSo, sUserName, sTenUser, eDoiTuong.TYPE_2, "Bộ phận 1 cửa", eHanhDong.TYPE_5_6, "Thu hồi công văn miễn giảm", "", "", eTrangThai.TYPE_5, "Xử lý thu hồi CV miễn/giảm kiểm tra", eTrangThai.TYPE_6, "Đã thu hồi công văn miễn giảm");

            ////Gui sang NSW
            //PhanHoiDonXM resultConfirm = new PhanHoiDonXM();
            //resultConfirm.NSWFileCode = sMaHoSo;
            //resultConfirm.NameOfStaff = sTenUser;
            //resultConfirm.ResponseDate = DateTime.Now;

            //string error = _sendService.PhanHoiDonXM(sMaHoSo, resultConfirm);
            //if (error == "99")
            //{

            //bang.MaNguoiDungSua = sUserName;
            //bang.IPSua = Request.UserHostAddress;
            //bang.DuLieuMoi = false;
            //bang.GiaTriKhoa = iID_MaHoSo;
            //bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", 6);
            //bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", 5);
            //bang.Save();

            //CLichSuHoSo.Add(Convert.ToInt64(iID_MaHoSo), sMaHoSo, sUserName, sTenUser, eDoiTuong.TYPE_2, "Bộ phận 1 cửa", eHanhDong.TYPE_5_6, "Thu hồi công văn miễn giảm", _sNoiDung, sFileTemp, eTrangThai.TYPE_5, "Xử lý thu hồi CV miễn/giảm kiểm tra", eTrangThai.TYPE_6, "Đã thu hồi công văn miễn giảm");

            //}

            CHoSo26.CleanNguoiXem(iID_MaHoSo);

            return RedirectToAction("MienGiam_DaTraKQ", "BoPhanMotCua");
        }

        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult MienGiam_DaTraKQ_ThuHoiSubmit()
        {
            String sUserName = User.Identity.Name;
            String sIP = Request.UserHostAddress;
            String sTenUser = CPQ_NGUOIDUNG.Get_TenNguoiDung(sUserName);

            ResultModels result = new ResultModels();

            String ParentID = "TH";
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

            CHoSo26.UpdateNguoiXem(iID_MaHoSo, sUserName);

            HoSo26Models models = CHoSo26.Get_Detail(Convert.ToInt64(iID_MaHoSo));
            string sMaHoSo = models.sMaHoSo;

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
                        //iID_MaDinhKem = CDinhKem.ThemDinhKem(hoSo.iID_MaHoSo, 0, 22, "0", hoSo.sMaHoSo, "File từ chối hồ sơ bổ sung Phòng TACN.", sFileName, "", null, 1, sFileTemp, sUserName, sIP);
                    }
                }
            }

            string error = "";
            if (CHamRieng.iNSW == 1)
            {
                //XML 13(05)
                ThongBaoThuHoiCVMienKiem resultConfirm = new ThongBaoThuHoiCVMienKiem();
                resultConfirm.NSWFileCode = sMaHoSo;
                resultConfirm.CancelDateString = DateTime.Now;
                resultConfirm.Reason = _sNoiDung;
                resultConfirm.SignConfirmDateString = DateTime.Now;
                resultConfirm.SignConfirmName = "Người ký";
                resultConfirm.ConfirmApplicationNo = "SO CV MG";
                resultConfirm.AttachmentId = "100";
                resultConfirm.FileName = sFileName;
                resultConfirm.FileLink = sFileTemp;

                error = _sendService.ThongBaoThuHoiCVMienKiem(sMaHoSo, resultConfirm);
            }
            else
            {
                error = "99";
            }
            
            if (error == "99")
            {

                bang.MaNguoiDungSua = sUserName;
                bang.IPSua = Request.UserHostAddress;
                bang.DuLieuMoi = false;
                bang.GiaTriKhoa = iID_MaHoSo;
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", 6);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", 5);
                bang.Save();

                CLichSuHoSo.Add(Convert.ToInt64(iID_MaHoSo), sMaHoSo, sUserName, sTenUser, eDoiTuong.TYPE_2, "Bộ phận 1 cửa", eHanhDong.TYPE_5_6, "Thu hồi công văn miễn giảm", _sNoiDung, sFileTemp, eTrangThai.TYPE_5, "Xử lý thu hồi CV miễn/giảm kiểm tra", eTrangThai.TYPE_6, "Đã thu hồi công văn miễn giảm");

                result.success = true;
            }
            else
            {
                result.success = false;
            }    

            CHoSo26.CleanNguoiXem(iID_MaHoSo);

            result.value = Url.Action("MienGiam_DaTraKQ", "BoPhanMotCua");
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        [AcceptVerbs(HttpVerbs.Get)]
        public PartialViewResult Partial_HangHoaChiTiet(string iID_MaHangHoa)
        {
            HangHoa26Models hanghoa = CHangHoa26.Get_Detail(Convert.ToInt64(iID_MaHangHoa));

            return PartialView(hanghoa);
        }

        public ActionResult BaoCao(CBaoCaoSearch repSearch)
        {
            if (repSearch == null)
            {
                repSearch = new CBaoCaoSearch
                {
                    sMaHoSo = null,
                    sMaSoThue = null,
                    sTenDoanhNghiep = null
                };
            }

            ViewData["menu"] = 224;
            return View(repSearch);
        }

        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Rep_Search(string ParentID)
        {
            string _DoanhNghiep = CString.SafeString(Request.Form[ParentID + "_DoanhNghiep"]).Trim();
            string _TuNgayDen = CString.SafeString(Request.Form[ParentID + "_viTuNgay"]);
            string _DenNgayDen = CString.SafeString(Request.Form[ParentID + "_viDenNgay"]);

            CBaoCaoSearch models = new CBaoCaoSearch
            {
                sMaSoThue = _DoanhNghiep,
                TuNgay = _TuNgayDen,
                DenNgay = _DenNgayDen
            };

            TempData["menu"] = 224;
            TempData["msg"] = "success";
            return RedirectToAction("BaoCao", models);
        }

        public ActionResult BaoCaoTinhHinhXuLyHoSo(CBaoCaoSearch repSearch)
        {
            if (repSearch == null)
            {
                repSearch = new CBaoCaoSearch
                {
                    sMaHoSo = null,
                    sMaSoThue = null,
                    sTenDoanhNghiep = null
                };
            }

            ViewData["menu"] = 224;
            return View(repSearch);
        }


        [Authorize]
        public ActionResult Print(String sMaSoThue, String TuNgay, String DenNgay)
        {
            //string _sMaSoThue = CString.SafeString(Request.Form[ParentID + "_DoanhNghiep"]);
            //string _TuNgay = CString.SafeString(Request.Form[ParentID + "_viTuNgay"]);
            //string _DenNgay = CString.SafeString(Request.Form[ParentID + "_viDenNgay"]);
            ViewBag.sMaSoThue = sMaSoThue;
            ViewBag.TuNgay = TuNgay;
            ViewBag.DenNgay = DenNgay;
            ViewData["menu"] = 224;
            return View();
        }
        [Authorize]
        public ActionResult ViewPDF(String MaND, String sMaSoThue, String TuNgay, String DenNgay)
        {
            CHamRieng.Language();

            String sFilePathPrint = "/ExcelFrom/rptCNN_CVBaoCaoMienGiam.xls";
            ExcelFile xls = CreateReport(Server.MapPath(sFilePathPrint), MaND, sMaSoThue, TuNgay, DenNgay);
            using (FlexCelPdfExport pdf = new FlexCelPdfExport())
            {
                pdf.Workbook = xls;
                using (MemoryStream ms = new MemoryStream())
                {
                    pdf.BeginExport(ms);
                    pdf.ExportAllVisibleSheets(false, "BaoCao");
                    pdf.EndExport();
                    ms.Position = 0;
                    return File(ms.ToArray(), "application/pdf");
                }
            }
            return null;
        }
        public ExcelFile CreateReport(String path, String MaND, String sMaSoThue, String TuNgay, String DenNgay)
        {
            XlsFile Result = new XlsFile(true);
            Result.Open(path);

            CBaoCaoSearch sModel = new CBaoCaoSearch
            {
                TuNgay = TuNgay,
                DenNgay = DenNgay,
                sMaSoThue = sMaSoThue
            };

            FlexCelReport fr = new FlexCelReport();
            LoadData(fr, MaND, sModel);

            fr.Run(Result);
            return Result;

        }

        private void LoadData(FlexCelReport fr, String MaND, CBaoCaoSearch sModel)
        {
            DateTime dNow = DateTime.Now;

            DataTable dtCT = CCongTy.Get_Table_Top();
            String sTenCongTy = Convert.ToString(dtCT.Rows[0]["sTenCongTy"]);
            String sDienThoaiCongTy = Convert.ToString(dtCT.Rows[0]["sDienThoai"]);
            String sDiChi = Convert.ToString(dtCT.Rows[0]["sDiaChi_In"]);
            Byte[] sLogo = (byte[])dtCT.Rows[0]["sLogo"];

            long iTongHoSoTiepNhan = 0;
            long iTongHoSoChuaTraLoi = CHoSo26.Get_Report_Count(sModel, "1,2,3");
            long iTongHoSoTraLoi = CHoSo26.Get_Report_Count(sModel, "4,5,6");
            long iTongHoSoThuHoi = CHoSo26.Get_Report_Count(sModel, "5,6");

            iTongHoSoTiepNhan = iTongHoSoChuaTraLoi + iTongHoSoTraLoi;

            fr.SetValue("TenCongTy", sTenCongTy);
            //fr.SetValue("DienThoaiCongTy", sDienThoaiCongTy);
            //fr.SetValue("DiaChiCongTy", sDiChi);

            fr.SetValue("Ngay", dNow.Day);
            fr.SetValue("Thang", dNow.Month);
            fr.SetValue("Nam", dNow.Year);

            fr.SetValue("sTuNgay", sModel.TuNgay);
            fr.SetValue("sDenNgay", sModel.DenNgay);
            fr.SetValue("sDoanhNghiep", sModel.sMaSoThue);

            fr.SetValue("So1_TraLoi", CommonFunction.DinhDangSo(iTongHoSoTraLoi));
            fr.SetValue("So2_TraLoi", CommonFunction.DinhDangSo(iTongHoSoChuaTraLoi));
            fr.SetValue("ThuHoi", CommonFunction.DinhDangSo(iTongHoSoThuHoi));
        }

        public clsExcelResult ExpExcel(String sMaSoThue, String TuNgay, String DenNgay)
        {
            string sMaND = User.Identity.Name;
            String sFilePath = "/ExcelFrom/rptCNN_CVBaoCaoMienGiam.xls";
            clsExcelResult clsResult = new clsExcelResult();
            ExcelFile xls = CreateReport(Server.MapPath(sFilePath), sMaND, sMaSoThue, TuNgay, DenNgay);

            using (MemoryStream ms = new MemoryStream())
            {
                xls.Save(ms);
                ms.Position = 0;
                clsResult.ms = ms;
                clsResult.FileName = "BaoCaoMienGiam.xls";
                clsResult.type = "xls";
                return clsResult;
            }
        }
    }
}