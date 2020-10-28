﻿using System;
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
        public object GetHoangHoa(string iID_MaHoSo)
        {
            var DataTable = GetHoangHoaByIdHoSo(iID_MaHoSo);
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in DataTable.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in DataTable.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(new { DataTable = serializer.Serialize(rows) }, JsonRequestBehavior.AllowGet);
        }
        public static DataTable GetHoangHoaByIdHoSo(string iID_MaHoSo)
        {
            SqlCommand cmd = new SqlCommand();
            string SQL = string.Format("SELECT A.iID_MaHoSo,A.sMaHoSo ,A.sTenHangHoa as 'sTenHangHoa', B.rKhoiLuong , B.rSoLuong FROM CNN25_HangHoa A left join CNN25_HangHoa_SoLuong B on A.iID_MaHangHoa = B.iID_MaHangHoa WHERE A.iID_MaHoSo = {0} ", iID_MaHoSo);
            cmd.CommandText = SQL;
            string sOrder = "iID_MaHoSo DESC";
            DataTable dt = CommonFunction.dtData(cmd, sOrder, 1, 10000);
            cmd.Dispose();
            return dt;
        }
    }
}