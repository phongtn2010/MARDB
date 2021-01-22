using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DATA0200025.Models;
using DomainModel;
using APP0200025.Models;
using FlexCel.Core;
using FlexCel.Render;
using System.IO;
using FlexCel.XlsAdapter;
using FlexCel.Report;
using System.Data;
using DATA0200025;

namespace APP0200025.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        [Authorize]
        public ActionResult TinhHinhXuLyHoSo(ReportSearchModels model)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "CNN25_HoSo", "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            if (model==null)
            {
                model = new ReportSearchModels { };
            }
            ViewData["menu"] = 240;
            return View(model);
        }
        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult sTinhHinhXuLyHoSo(string ParentID)
        {
            string _sMaSoThue = CString.SafeString(Request.Form[ParentID + "_DoanhNghiep"]);
            string _TuNgay = CString.SafeString(Request.Form[ParentID + "_viTuNgay"]);
            string _DenNgay= CString.SafeString(Request.Form[ParentID + "_viDenNgay"]);
            ReportSearchModels model = new ReportSearchModels
            {
                TuNgay= _TuNgay,
                DenNgay=_DenNgay,
                sMaSoThue=_sMaSoThue
            };
            return RedirectToAction("TinhHinhXuLyHoSo", model);
        }

        [Authorize]
        public ActionResult Print(String sMaSoThue, String TuNgay, String DenNgay)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "CNN25_HoSo", "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            //string _sMaSoThue = CString.SafeString(Request.Form[ParentID + "_DoanhNghiep"]);
            //string _TuNgay = CString.SafeString(Request.Form[ParentID + "_viTuNgay"]);
            //string _DenNgay = CString.SafeString(Request.Form[ParentID + "_viDenNgay"]);
            ViewBag.sMaSoThue = sMaSoThue;
            ViewBag.TuNgay = TuNgay;
            ViewBag.DenNgay = DenNgay;
            ViewData["menu"] = 240;
            return View();
        }
        [Authorize]
        public ActionResult ViewPDF(String MaND, String sMaSoThue, String TuNgay, String DenNgay)
        {
            CHamRieng.Language();

            String sFilePathPrint = "/ExcelFrom/rptCNN_MC_BaoCao01.xls";
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

            ReportSearchModels sModel = new ReportSearchModels
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

        private void LoadData(FlexCelReport fr, String MaND, ReportSearchModels sModel)
        {
            DateTime dNow = DateTime.Now;

            DataTable dtCT = CCongTy.Get_Table_Top();
            String sTenCongTy = Convert.ToString(dtCT.Rows[0]["sTenCongTy"]);
            String sDienThoaiCongTy = Convert.ToString(dtCT.Rows[0]["sDienThoai"]);
            String sDiChi = Convert.ToString(dtCT.Rows[0]["sDiaChi_In"]);
            Byte[] sLogo = (byte[])dtCT.Rows[0]["sLogo"];

            DataTable dt = clBaoCao.BaoCaoTinhHinhXuLyHoSo(sModel);

            int c1 = 0, c2 = 0, c3 = 0, c4 = 0, c5 = 0, c6 = 0, c7 = 0, c8 = 0, c9 = 0, c10 = 0, c11 = 0, c12 = 0, c13 = 0, c14 = 0, c15 = 0, c16 = 0, c17 = 0, c18 = 0, c19 = 0;
            if (dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];

                c3 = Convert.ToInt32(r["c3"]); c4 = Convert.ToInt32(r["c4"]); c5 = Convert.ToInt32(r["c5"]);
                c7 = Convert.ToInt32(r["c7"]); c8 = Convert.ToInt32(r["c8"]); c9 = Convert.ToInt32(r["c9"]);
                c11 = Convert.ToInt32(r["c11"]); c12 = Convert.ToInt32(r["c12"]); c15 = Convert.ToInt32(r["c15"]);
                c16 = Convert.ToInt32(r["c16"]); c18 = Convert.ToInt32(r["c18"]); c19 = Convert.ToInt32(r["c19"]);
                c14 = c15 + c16;
                c17 = c18 + c19;
                c13 = c14 + c17;
                c10 = c11 + c12;
                c6 = c7 + c8;
                c2 = c3 + c4;
                c1 = c2 + c10 + c13;
            }
            dt.Dispose();

                fr.SetValue("TenCongTy", sTenCongTy);
            //fr.SetValue("DienThoaiCongTy", sDienThoaiCongTy);
            //fr.SetValue("DiaChiCongTy", sDiChi);

            fr.SetValue("Ngay", dNow.Day);
            fr.SetValue("Thang", dNow.Month);
            fr.SetValue("Nam", dNow.Year);

            fr.SetValue("sTuNgay", sModel.TuNgay);
            fr.SetValue("sDenNgay", sModel.DenNgay);
            fr.SetValue("sDoanhNghiep", sModel.sMaSoThue);

            fr.SetValue("c1", CommonFunction.DinhDangSo(c1));
            fr.SetValue("c2", CommonFunction.DinhDangSo(c2));
            fr.SetValue("c3", CommonFunction.DinhDangSo(c3));
            fr.SetValue("c4", CommonFunction.DinhDangSo(c4));
            fr.SetValue("c5", CommonFunction.DinhDangSo(c5));
            fr.SetValue("c6", CommonFunction.DinhDangSo(c6));
            fr.SetValue("c7", CommonFunction.DinhDangSo(c7));
            fr.SetValue("c8", CommonFunction.DinhDangSo(c8));
            fr.SetValue("c9", CommonFunction.DinhDangSo(c9));
            fr.SetValue("c10", CommonFunction.DinhDangSo(c10));
            fr.SetValue("c11", CommonFunction.DinhDangSo(c11));
            fr.SetValue("c12", CommonFunction.DinhDangSo(c12));
            fr.SetValue("c13", CommonFunction.DinhDangSo(c13));
            fr.SetValue("c14", CommonFunction.DinhDangSo(c14));
            fr.SetValue("c15", CommonFunction.DinhDangSo(c15));
            fr.SetValue("c16", CommonFunction.DinhDangSo(c16));
            fr.SetValue("c17", CommonFunction.DinhDangSo(c17));
            fr.SetValue("c18", CommonFunction.DinhDangSo(c18));
            fr.SetValue("c19", CommonFunction.DinhDangSo(c19));

        }
    }
}