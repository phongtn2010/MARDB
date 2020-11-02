using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using DomainModel;
using DomainModel.Abstract;
using DATA0200025;
using DATA0200025.Models;
using DATA0200025.SearchModels;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace APP0200025.Controllers
{
    public class ToChucChiDinhController : Controller
    {
        Bang bang = new Bang("CNN25_HoSo");
        public ActionResult Index(sHoSoModels models)
        {
            if (models == null || models.LoaiDanhSach == 0)
            {
                models = new sHoSoModels
                {
                    Page = 1,
                    PageSize = Globals.PageSize,
                    LoaiDanhSach = 50
                };
            }
            return View(models);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ThongTinHoangHoa(string iID_MaHoSo)
        {
            DataTable hoSo = GetHoangHoaByIdHoSo(iID_MaHoSo);
            ViewData["DuLieuMoi"] = "0";
            return View(hoSo);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult KetQuaUpload()
        {
            return View();
        }
        public ActionResult XemKetQuaUpload()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult BuildForm()
        {
            return View();
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
            string _iID_MaTrangThai = CString.SafeString(Request.Form[ParentID + "_iID_MaTrangThai"]);
            int LoaiDanhSach = 50;
            if (_iID_MaTrangThai != "0")
            {
                if (_iID_MaTrangThai == "27")
                {
                    LoaiDanhSach = 51;
                }
                else if (_iID_MaTrangThai == "28")
                {
                    LoaiDanhSach = 52;
                }
            }
            sHoSoModels models = new sHoSoModels
            {
                LoaiDanhSach = LoaiDanhSach,
                iID_MaTrangThai = int.Parse(_iID_MaTrangThai),
                sMaHoSo = _sMaHoSo,
                sTenDoanhNghiep = _sTenDoanhNghiep,
                sTenTACN = _sTenTACN,
                TuNgayDen = _FromDate,
                DenNgayDen = _ToDate,
                TuNgayTiepNhan = _FromDateTiepNhan,
                DenNgayTiepNhan = _ToDateTiepNhan,
                sSoTiepNhan = _sSoTiepNhan
            };
            return RedirectToAction("Index", models);
        }
        [HttpPost]
        public object GetUpload(string iID_MaHoSo, string iID_MaHangHoa)
        {
            var ThongTin = LayThongTinChungNhanHopQuy(iID_MaHoSo, iID_MaHangHoa);
            return Json(new { response = ThongTin }, JsonRequestBehavior.AllowGet);
        }
        public static DataTable GetHoangHoaByIdHoSo(string iID_MaHoSo)
        {
            SqlCommand cmd = new SqlCommand();
            string SQL = string.Format("SELECT A.iID_MaHoSo,A.sMaHoSo ,A.iID_MaHangHoa ,A.sTenHangHoa as 'sTenHangHoa', B.rKhoiLuong , B.rSoLuong FROM CNN25_HangHoa A left join CNN25_HangHoa_SoLuong B on A.iID_MaHangHoa = B.iID_MaHangHoa WHERE A.iID_MaHoSo = {0} ", iID_MaHoSo);
            cmd.CommandText = SQL;
            string sOrder = "iID_MaHoSo DESC";
            DataTable dt = CommonFunction.dtData(cmd, sOrder, 1, 10000);
            cmd.Dispose();
            return dt;
        }
        public static ToChucChiDinh LayThongTinChungNhanHopQuy(string iID_MaHoSo, string iID_MaHangHoa)
        {
            ToChucChiDinh KetQua = new ToChucChiDinh();
            SqlCommand cmd = new SqlCommand();
            string SQL = string.Format("SELECT * FROM CNN25_ChungNhanHopQuy WHERE iID_MaHoSo = {0} and iID_MaHangHoa = {1} ", iID_MaHoSo, iID_MaHangHoa);
            cmd.CommandText = SQL;
            string sOrder = "iID_MaHoSo DESC";
            var ThongTin = CommonFunction.dtData(cmd, sOrder, 1, 10000);
            List<ChungNhanHopQuy> info = new List<ChungNhanHopQuy>();
            info = (from DataRow dr in ThongTin.Rows
                    select new ChungNhanHopQuy()
                    {
                        bKetQuaDanhGia = (bool)dr["bKetQuaDanhGia"],
                        dNgayCap = Convert.ToDateTime(dr["dNgayCap"]).ToString("dd/MM/yyyy"),
                        iID_ChungNhanHopQuy = dr["iID_ChungNhanHopQuy"] != null ? int.Parse(dr["iID_ChungNhanHopQuy"].ToString()) : 0,
                        iID_MaHangHoa = dr["iID_MaHangHoa"] != null ? int.Parse(dr["iID_MaHangHoa"].ToString()) : 0,
                        iID_MaHoSo = dr["iID_MaHoSo"] != null ? int.Parse(dr["iID_MaHoSo"].ToString()) : 0,
                        sDuongDan = dr["sDuongDan"] as string,
                        sSoChungNhan = dr["sSoChungNhan"] as string,
                        sTenFile = dr["sTenFile"] as string
                    }).ToList();
            if (info.FirstOrDefault() != null)
            {
                KetQua.ThongTin = info.First();
            }

            cmd = new SqlCommand();
            SQL = string.Format("SELECT * FROM CNN25_KetQuaPhanTich WHERE iID_MaHoSo = {0} and iID_MaHangHoa = {1} ", iID_MaHoSo, iID_MaHangHoa);
            cmd.CommandText = SQL;
            sOrder = "iID_MaHoSo DESC";
            List<ChungNhanHopQuy_dsFile> dsfile = new List<ChungNhanHopQuy_dsFile>();
            var ThongTinFiles = CommonFunction.dtData(cmd, sOrder, 1, 10000);
            dsfile = (from DataRow dr in ThongTinFiles.Rows
                      select new ChungNhanHopQuy_dsFile()
                      {
                          iID_KetQuaPhanTich = dr["iID_KetQuaPhanTich"] != null ? int.Parse(dr["iID_KetQuaPhanTich"].ToString()) : 0,
                          sTenFile = dr["sTenFile"] as string,
                          iID_MaHangHoa = dr["iID_MaHangHoa"] != null ? int.Parse(dr["iID_MaHangHoa"].ToString()) : 0,
                          iID_MaHoSo = dr["iID_MaHoSo"] != null ? int.Parse(dr["iID_MaHoSo"].ToString()) : 0,
                          sDuongDan = dr["sDuongDan"] as string,
                      }).ToList();
            if (dsfile.Count > 0)
            {
                KetQua.DanhSachFile = dsfile;
            }
            cmd.Dispose();
            return KetQua;
        }
        public class ToChucChiDinh
        {
            public ChungNhanHopQuy ThongTin { get; set; }
            public List<ChungNhanHopQuy_dsFile> DanhSachFile { get; set; }
        }
        public class ChungNhanHopQuy
        {
            public int iID_ChungNhanHopQuy { get; set; }
            public int iID_MaHangHoa { get; set; }
            public int iID_MaHoSo { get; set; }
            public string sSoChungNhan { get; set; }
            public string dNgayCap { get; set; }
            public string sTenFile { get; set; }
            public string sDuongDan { get; set; }
            public bool bKetQuaDanhGia { get; set; }
        }
        public class ChungNhanHopQuy_dsFile
        {
            public int iID_KetQuaPhanTich { get; set; }
            public int iID_MaHoSo { get; set; }
            public int iID_MaHangHoa { get; set; }
            public string sTenFile { get; set; }
            public string sDuongDan { get; set; }

        }
        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult KetQuaUpload(HttpPostedFileBase file)
        {
            Bang bang = new Bang("CNN25_ChungNhanHopQuy");
            String ParentID = "Index";
            NameValueCollection values = new NameValueCollection();
            HttpFileCollectionBase files = Request.Files;
            string _iID_MaHoSo = CString.SafeString(Request.Form[ParentID + "_iID_MaHoSo"]);
            string _iID_MaHangHoa = CString.SafeString(Request.Form[ParentID + "_iID_MaHangHoa"]);
            string _sKetQuaDanhGia = CString.SafeString(Request.Form[ParentID + "_sKetQuaDanhGia"]);
            string _sSoChungNhan = CString.SafeString(Request.Form[ParentID + "_sSoChungNhan"]);
            string _bKetQuaDanhGia = CString.SafeString(Request.Form[ParentID + "_bKetQuaDanhGia"]);
            string _dNgayCap = CString.SafeString(Request.Form[ParentID + "_vidNgayCap"]) as string;
            string _sTenFile = string.Empty;
            string _sDuongDan = string.Empty;
            var sDateTime = string.Empty;
            if (string.IsNullOrEmpty(_bKetQuaDanhGia))
            {
                values.Add("err_bKetQuaDanhGia", "Chọn kết quả đánh giá");
            }
            if (string.IsNullOrEmpty(_sSoChungNhan))
            {
                values.Add("err_sSoChungNhan", "Nhập số chứng nhận");
            }
            if (string.IsNullOrEmpty(_dNgayCap))
            {
                values.Add("err_dNgayCap", "Bạn ngày cấp");
            }
            if (values.Count == 0)
            {
                _dNgayCap = CommonFunction.LayNgayTuXau(_dNgayCap).ToString();
                sDateTime = Convert.ToDateTime(_dNgayCap).ToString("yyyy-MM-dd");
                var sFileTemp = string.Empty;
                if (file != null)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        string guid = Guid.NewGuid().ToString();
                        string sPath = "/Uploads/File";
                        DateTime TG = DateTime.Now;
                        string subPath = TG.ToString("yyyy/MM/dd");
                        string subName = TG.ToString("HHmmssfff") + "_" + guid;
                        string newPath = string.Format("{0}/{1}", sPath, subPath);
                        CImage.CreateDirectory(Server.MapPath("~" + newPath));

                        sFileTemp = string.Format(newPath + "/{0}_{1}", subName, file.FileName);
                        string filePath = Server.MapPath("~" + sFileTemp);
                        file.SaveAs(filePath);
                        _sTenFile = file.FileName;
                        _sDuongDan = sPath;
                    }
                }
                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                bang.DuLieuMoi = true;
                bang.TruongKhoa = "iID_ChungNhanHopQuy";
                bang.CmdParams.Parameters.AddWithValue("@bKetQuaDanhGia", int.Parse(_bKetQuaDanhGia));
                var DSTruong = "iID_MaHoSo,iID_MaHangHoa,sSoChungNhan,dNgayCap,bKetQuaDanhGia,sTenFile,sDuongDan";
                var DSGiaTri = string.Format("'{0}','{1}','{2}','{3}',{4},'{5}','{6}'", _iID_MaHoSo, _iID_MaHangHoa, _sSoChungNhan, sDateTime, _bKetQuaDanhGia, _sTenFile, _sDuongDan);
                var SQL = String.Format("INSERT INTO {0}({1}) VALUES({2});", "CNN25_ChungNhanHopQuy", DSTruong, DSGiaTri);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQL;
                Connection.UpdateDatabase(cmd, 0);
                //clHoSo.CleanNguoiXem(iID_MaHoSo);
                //clLichSuHoSo.InsertLichSu(hoSo.iID_MaHoSo, User.Identity.Name, (int)clDoiTuong.DoiTuong.BoPhanMotCua, (int)clHanhDong.HanhDong.TuChoiCapGDK, _sNoiDung, sFileTemp, hoSo.iID_MaTrangThai, trangThaiTiepTheo.iID_MaTrangThai);
                ResultModels result = new ResultModels { success = true };
                return RedirectToAction("ThongTinHoangHoa", "ToChucChiDinh", new { @iID_MaHoSo = _iID_MaHoSo });
            }
            else
            {
                for (int i = 0; i <= (values.Count - 1); i++)
                {
                    ModelState.AddModelError(ParentID + "_" + values.GetKey(i), values[i]);
                }
                return View();
            }
        }
        [Authorize, ValidateInput(false), HttpPost]
        public object KetQuaUpload1()
        {
            var m = Request.Form;
            var n = Request.Files;
            var sDateTime = string.Empty;
            string _sTenFile = string.Empty;
            string _sDuongDan = string.Empty;

            string _iID_MaHoSo = CString.SafeString(Request.Form["_iID_MaHoSo"]);
            string _iID_MaHangHoa = CString.SafeString(Request.Form["_iID_MaHangHoa"]);
            string _sKetQuaDanhGia = CString.SafeString(Request.Form["_sKetQuaDanhGia"]);
            string _sSoChungNhan = CString.SafeString(Request.Form["_sSoChungNhan"]);
            string _bKetQuaDanhGia = CString.SafeString(Request.Form["_bKetQuaDanhGia"]);
            string _dNgayCap = CString.SafeString(Request.Form["_dNgayCap"]) as string;

            for (var i = 0; i < Request.Files.Count; i++)
            {
                if (Request.Files.AllKeys[i] == "_file")
                {
                    string guid = Guid.NewGuid().ToString();
                    string sPath = "/Uploads/File";
                    DateTime TG = DateTime.Now;
                    string subPath = TG.ToString("yyyy/MM/dd");
                    string subName = TG.ToString("HHmmssfff") + "_" + guid;
                    string newPath = string.Format("{0}/{1}", sPath, subPath);
                    CImage.CreateDirectory(Server.MapPath("~" + newPath));
                    string sFileTemp = string.Empty;
                    sFileTemp = string.Format(newPath + "/{0}_{1}", subName, Request.Files[i].FileName);
                    string filePath = Server.MapPath("~" + sFileTemp);
                    Request.Files[i].SaveAs(filePath);
                    _sTenFile = Request.Files[i].FileName;
                    _sDuongDan = sFileTemp;
                }
                else
                {
                    string guid = Guid.NewGuid().ToString();
                    string sPath = "/Uploads/File";
                    DateTime TG = DateTime.Now;
                    string subPath = TG.ToString("yyyy/MM/dd");
                    string subName = TG.ToString("HHmmssfff") + "_" + guid;
                    string newPath = string.Format("{0}/{1}", sPath, subPath);
                    CImage.CreateDirectory(Server.MapPath("~" + newPath));
                    string sFileTemp = string.Empty;
                    sFileTemp = string.Format(newPath + "/{0}_{1}", subName, Request.Files[i].FileName);
                    string filePath = Server.MapPath("~" + sFileTemp);
                    Request.Files[i].SaveAs(filePath);
                    var _sTenFile_PhanTich = Request.Files[i].FileName;
                    var _sDuongDan_PhanTich = sFileTemp;
                    var DSTruong_PhanTich = "iID_MaHoSo,iID_MaHangHoa,sTenFile,sDuongDan";
                    var DSGiaTri_PhanTich = string.Format("'{0}','{1}','{2}','{3}'", _iID_MaHoSo, _iID_MaHangHoa, _sTenFile_PhanTich, _sDuongDan_PhanTich);
                    var SQL_PhanTich = String.Format("INSERT INTO {0}({1}) VALUES({2});", "CNN25_KetQuaPhanTich", DSTruong_PhanTich, DSGiaTri_PhanTich);
                    SqlCommand cmd_PhanTich = new SqlCommand();
                    cmd_PhanTich.CommandText = SQL_PhanTich;
                    Connection.UpdateDatabase(cmd_PhanTich, 0);
                }
            }
            SqlCommand cmd_KiemTraTonTai = new SqlCommand();
            string SQL_KiemTraTonTai = string.Format("SELECT iID_MaHangHoa FROM {0} WHERE iID_MaHoSo = {1} and iID_MaHangHoa = {2}", "CNN25_ChungNhanHopQuy", _iID_MaHoSo, _iID_MaHangHoa);
            cmd_KiemTraTonTai.CommandText = SQL_KiemTraTonTai;
            string sOrder = "iID_MaHangHoa DESC";
            DataTable dt = CommonFunction.dtData(cmd_KiemTraTonTai, sOrder, 1, 10000);
            _dNgayCap = CommonFunction.LayNgayTuXau(_dNgayCap).ToString();
            sDateTime = Convert.ToDateTime(_dNgayCap).ToString("yyyy-MM-dd");
            if (dt.Rows.Count > 0)
            {
                var DSGiaTri = string.Format("sSoChungNhan = '{0}',dNgayCap = '{1}',bKetQuaDanhGia = {2}", _sSoChungNhan, sDateTime, _bKetQuaDanhGia);
                if (!string.IsNullOrEmpty(_sTenFile))
                {
                    DSGiaTri = string.Format("sSoChungNhan = '{0}',dNgayCap = '{1}',bKetQuaDanhGia = {2},sTenFile = '{3}',sDuongDan= '{4}'", _sSoChungNhan, sDateTime, _bKetQuaDanhGia, _sTenFile, _sDuongDan);
                }
                var SQL = String.Format("UPDATE  {0} SET {1} WHERE iID_MaHoSo = {2} and iID_MaHangHoa = {3}", "CNN25_ChungNhanHopQuy", DSGiaTri, _iID_MaHoSo, _iID_MaHangHoa);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQL;
                Connection.UpdateDatabase(cmd, 0);
                if (Request.Form["deleteFile"] != null)
                {
                    var DSGiaTri_XoaFile = string.Format("({0})", Request.Form["deleteFile"] as string);
                    var SQL_XoaFile = String.Format("DELETE {0} WHERE iID_KetQuaPhanTich IN " + DSGiaTri_XoaFile, "CNN25_KetQuaPhanTich");
                    SqlCommand cmd_XoaFile = new SqlCommand();
                    cmd_XoaFile.CommandText = SQL_XoaFile;
                    Connection.UpdateDatabase(cmd_XoaFile, 0);
                }
            }
            else
            {
                var DSTruong = "iID_MaHoSo,iID_MaHangHoa,sSoChungNhan,dNgayCap,bKetQuaDanhGia,sTenFile,sDuongDan";
                var DSGiaTri = string.Format("'{0}','{1}','{2}','{3}',{4},'{5}','{6}'", _iID_MaHoSo, _iID_MaHangHoa, _sSoChungNhan, sDateTime, _bKetQuaDanhGia, _sTenFile, _sDuongDan);
                var SQL = String.Format("INSERT INTO {0}({1}) VALUES({2});", "CNN25_ChungNhanHopQuy", DSTruong, DSGiaTri);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQL;
                Connection.UpdateDatabase(cmd, 0);
            }
            return null;
        }
    }
}