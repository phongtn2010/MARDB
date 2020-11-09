using DATA0200025;
using DATA0200025.Models;
using DATA0200025.SearchModels;
using DATA0200025.WebServices;
using DATA0200025.WebServices.XmlType.Request;
using DomainModel;
using DomainModel.Abstract;
using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;

namespace APP0200025.Controllers
{
    public class DaTraGDKController : Controller
    {
        private SendService _sendService = new SendService();

        // GET: DaTraGDK
        Bang bang = new Bang("CNN25_HoSo");

        private string ViewPath = "~/Views/DaTraGDK/";
        // GET: DaTraGDK
        public ActionResult Index(sHoSoModels models)
        {
            if (models == null || models.LoaiDanhSach == 0)
            {
                models = new sHoSoModels
                {
                    LoaiDanhSach = 6,
                    Page = 1,
                    PageSize = Globals.PageSize
                };
            }

            ViewData["menu"] = 234;
            return View(models);
        }
        public ActionResult Detail(string iID_MaHoSo)
        {
            //if (BaoMat.ChoPhepLamViec(User.Identity.Name, bang.TenBang, "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            //{
            //    return RedirectToAction("Index", "PermitionMessage");
            //}
            clHoSo.UpdateNguoiXem(iID_MaHoSo, User.Identity.Name);
            ViewData["DuLieuMoi"] = "0";
            ViewData["menu"] = 234;
            HoSoModels models = clHoSo.GetHoSoById(Convert.ToInt32(iID_MaHoSo));

            return View(models);
        }

        /// <summary>
        /// update data màn hình TiepNhanHoSo
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="MaHoSo"></param>
        /// <returns></returns>
        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult ThuHoiSubmit()
        {
            String sUserName = User.Identity.Name;
            String sIP = Request.UserHostAddress;

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

            HoSoModels hoSo = clHoSo.GetHoSoById(Convert.ToInt64(iID_MaHoSo));

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
                        iID_MaDinhKem = CDinhKem.ThemDinhKem(hoSo.iID_MaHoSo, 0, 90, "0", hoSo.sMaHoSo, "File Thu hồi giấy GDK.", sFileName, "", null, 1, sFileTemp, sUserName, sIP);
                    }
                }
            }

            TrangThaiModels trangThaiTiepTheo = clTrangThai.GetTrangThaiModelsTiepTheo((int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.ThuHoiGiayDangKyDaCap, hoSo.iID_MaTrangThai, hoSo.iID_MaTrangThaiTruoc);

            //XML(14, 12)
            ThuHoiGDK resultConfirm = new ThuHoiGDK();
            resultConfirm.NSWFileCode = hoSo.sMaHoSo;
            resultConfirm.CancelDate = DateTime.Now;
            resultConfirm.Reason = _sNoiDung;
            resultConfirm.SignConfirmDate = DateTime.Now;
            resultConfirm.SignConfirmName = "Cục chăn nuôi;
            resultConfirm.AniFeedConfirmNo = hoSo.sSoGDK;
            resultConfirm.AttachmentId = iID_MaDinhKem.ToString(); ;
            resultConfirm.FileName = sFileName;
            resultConfirm.FileLink = string.Format("{0}{1}", clCommon.BNN_Url, sFileTemp);
            string error = _sendService.ThuHoiGDK(hoSo.sMaHoSo, resultConfirm);
            if (error.Equals("99"))
            {
                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                bang.DuLieuMoi = false;
                bang.GiaTriKhoa = iID_MaHoSo;
                bang.CmdParams.Parameters.AddWithValue("@sKetQuaXuLy", trangThaiTiepTheo.sKetQuaXuLy ?? "");
                bang.CmdParams.Parameters.AddWithValue("@iID_KetQuaXuLy", trangThaiTiepTheo.iID_KetQuaXuLy);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", trangThaiTiepTheo.iID_MaTrangThai);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", hoSo.iID_MaTrangThai);
                bang.Save();

                clLichSuHoSo.InsertLichSu(hoSo.iID_MaHoSo, User.Identity.Name, (int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.ThuHoiGiayDangKyDaCap, "Thu hồi giấy GDK", "", hoSo.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);

                result.success = true;
            }
            else
            {
                result.success = false;
            }

            clHoSo.CleanNguoiXem(iID_MaHoSo);

            result.value = Url.Action("Index");
            return Json(result, JsonRequestBehavior.AllowGet);
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
            string sSoTiepNhan = CString.SafeString(Request.Form[ParentID + "_sSoTiepNhan"]);
            sHoSoModels models = new sHoSoModels
            {
                LoaiDanhSach = 5,
                sMaHoSo = _sMaHoSo,
                sTenDoanhNghiep = _sTenDoanhNghiep,
                sTenTACN = _sTenTACN,
                TuNgayDen = _TuNgayDen,
                DenNgayDen = _DenNgayDen,
                TuNgayTiepNhan = TuNgayTiepNhan,
                DenNgayTiepNhan = DenNgayTiepNhan,
                sSoTiepNhan = sSoTiepNhan
            };

            TempData["menu"] = 234;
            TempData["msg"] = "success";
            return RedirectToAction("Index", models);
        }
    }
}