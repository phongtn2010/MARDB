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
    public class CVReportController : Controller
    {
        // GET: CVReport
        #region TinhHinhXuLyHoSo
        [Authorize]
        public ActionResult TinhHinhXuLyHoSo(ReportSearchModels model)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "CNN25_HoSo", "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            if (model == null)
            {
                model = new ReportSearchModels { };
            }
            ViewData["menu"] = 247;
            return View(model);
        }
        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult sTinhHinhXuLyHoSo(string ParentID)
        {
            string _sMaSoThue = CString.SafeString(Request.Form[ParentID + "_DoanhNghiep"]);
            string _TuNgay = CString.SafeString(Request.Form[ParentID + "_viTuNgay"]);
            string _DenNgay = CString.SafeString(Request.Form[ParentID + "_viDenNgay"]);
            ReportSearchModels model = new ReportSearchModels
            {
                TuNgay = _TuNgay,
                DenNgay = _DenNgay,
                sMaSoThue = _sMaSoThue
            };
            return RedirectToAction("TinhHinhXuLyHoSo", model);
        }

        [Authorize]
        public ActionResult TinhHinhXuLyHoSoPrint(String sMaSoThue, String TuNgay, String DenNgay)
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
            ViewData["menu"] = 247;
            return View();
        }
        [Authorize]
        public ActionResult TinhHinhXuLyHoSoViewPDF(String MaND, String sMaSoThue, String TuNgay, String DenNgay)
        {
            CHamRieng.Language();

            String sFilePathPrint = "/ExcelFrom/rptCNN_MC_BaoCao01.xls";
            ExcelFile xls = TinhHinhXuLyHoSoCreateReport(Server.MapPath(sFilePathPrint), MaND, sMaSoThue, TuNgay, DenNgay);
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
        public ExcelFile TinhHinhXuLyHoSoCreateReport(String path, String MaND, String sMaSoThue, String TuNgay, String DenNgay)
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
            TinhHinhXuLyHoSoLoadData(fr, MaND, sModel);

            fr.Run(Result);
            return Result;

        }

        private void TinhHinhXuLyHoSoLoadData(FlexCelReport fr, String MaND, ReportSearchModels sModel)
        {
            DateTime dNow = DateTime.Now;

            DataTable dtCT = CCongTy.Get_Table_Top();
            String sTenCongTy = Convert.ToString(dtCT.Rows[0]["sTenCongTy"]);
            String sDienThoaiCongTy = Convert.ToString(dtCT.Rows[0]["sDienThoai"]);
            String sDiChi = Convert.ToString(dtCT.Rows[0]["sDiaChi_In"]);
            Byte[] sLogo = (byte[])dtCT.Rows[0]["sLogo"];

            DataTable dt = clBaoCao.CVBaoCaoTinhHinhXuLyHoSo(sModel);

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

        public clsExcelResult TinhHinhXuLyHoSoExpExcel(String sMaSoThue, String TuNgay, String DenNgay)
        {
            string sMaND = User.Identity.Name;
            String sFilePath = "/ExcelFrom/rptCNN_MC_BaoCao01.xls";
            clsExcelResult clsResult = new clsExcelResult();
            ExcelFile xls = TinhHinhXuLyHoSoCreateReport(Server.MapPath(sFilePath), sMaND, sMaSoThue, TuNgay, DenNgay);

            using (MemoryStream ms = new MemoryStream())
            {
                xls.Save(ms);
                ms.Position = 0;
                clsResult.ms = ms;
                clsResult.FileName = "BC_TinhHinhXuLyHoSo.xls";
                clsResult.type = "xls";
                return clsResult;
            }
        }
        #endregion

        #region BaoCao01
        [Authorize]
        public ActionResult BaoCao01(ReportSearchModels model)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "CNN25_HoSo", "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            if (model == null)
            {
                model = new ReportSearchModels { };
            }
            ViewData["menu"] = 248;
            return View(model);
        }
        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult BaoCao01Search(string ParentID)
        {
            string _TuNgay = CString.SafeString(Request.Form[ParentID + "_viTuNgay"]);
            string _DenNgay = CString.SafeString(Request.Form[ParentID + "_viDenNgay"]);
            ReportSearchModels model = new ReportSearchModels
            {
                TuNgay = _TuNgay,
                DenNgay = _DenNgay
            };
            return RedirectToAction("BaoCao01", model);
        }

        [Authorize]
        public ActionResult BaoCao01Print(String TuNgay, String DenNgay)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "CNN25_HoSo", "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            //string _sMaSoThue = CString.SafeString(Request.Form[ParentID + "_DoanhNghiep"]);
            //string _TuNgay = CString.SafeString(Request.Form[ParentID + "_viTuNgay"]);
            //string _DenNgay = CString.SafeString(Request.Form[ParentID + "_viDenNgay"]);
            ViewBag.TuNgay = TuNgay;
            ViewBag.DenNgay = DenNgay;
            ViewData["menu"] = 248;
            return View();
        }

        [Authorize]
        public ActionResult BaoCao01ViewPDF(String MaND, String TuNgay, String DenNgay)
        {
            CHamRieng.Language();

            String sFilePathPrint = "/ExcelFrom/rptCNN_CVBaoCao01.xls";
            ExcelFile xls = BaoCao01CreateReport(Server.MapPath(sFilePathPrint), MaND, TuNgay, DenNgay);
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

        public ExcelFile BaoCao01CreateReport(String path, String MaND, String TuNgay, String DenNgay)
        {
            XlsFile Result = new XlsFile(true);
            Result.Open(path);

            ReportSearchModels sModel = new ReportSearchModels
            {
                TuNgay = TuNgay,
                DenNgay = DenNgay
            };

            FlexCelReport fr = new FlexCelReport();
            BaoCao01LoadData(fr, MaND, sModel);

            fr.Run(Result);
            return Result;

        }

        private void BaoCao01LoadData(FlexCelReport fr, String MaND, ReportSearchModels sModel)
        {
            DateTime dNow = DateTime.Now;

            DataTable dtCT = CCongTy.Get_Table_Top();
            String sTenCongTy = Convert.ToString(dtCT.Rows[0]["sTenCongTy"]);
            String sDienThoaiCongTy = Convert.ToString(dtCT.Rows[0]["sDienThoai"]);
            String sDiChi = Convert.ToString(dtCT.Rows[0]["sDiaChi_In"]);
            Byte[] sLogo = (byte[])dtCT.Rows[0]["sLogo"];

            DataTable dt = clBaoCao.CVBaoCao01_Print(sModel);
            dt.Columns.Add("sSoLuong", typeof(System.String));
            dt.Columns.Add("sKhoiLuong", typeof(System.String));
            dt.Columns.Add("sKhoiLuongTan", typeof(System.String));
            dt.Columns.Add("sThoiGianNhap", typeof(System.String));
            dt.Columns.Add("sTenToChucChungNhan", typeof(System.String));
            dt.Columns.Add("sGiaVN", typeof(System.String));
            dt.Columns.Add("sGiaUSD", typeof(System.String));

            if (dt.Rows.Count > 0)
            {
                DataRow r;
                for (int i=0; i< dt.Rows.Count; i++)
                {
                    r = dt.Rows[i];

                    string sMua_FromDate = "", sMua_ToDate = "";

                    String sKhoiLuong = "", sKhoiLuongTan = "", sSoLuong = "";
                    DataTable dtKhoiLuong = clHangHoa.Get_ThongTinKhoiLuong(Convert.ToInt64(r["iID_MaHangHoa"]));
                    if (dtKhoiLuong.Rows.Count > 0)
                    {
                        for (int k = 0; k < dtKhoiLuong.Rows.Count; k++)
                        {
                            sKhoiLuong += Convert.ToString(dtKhoiLuong.Rows[k]["rKhoiLuong"]) + " " + Convert.ToString(dtKhoiLuong.Rows[k]["sDonViTinhKL"]) + "; ";
                            sKhoiLuongTan += Convert.ToString(dtKhoiLuong.Rows[k]["rKhoiLuongTan"]) + "; ";
                            sSoLuong += Convert.ToString(dtKhoiLuong.Rows[k]["rSoLuong"]) + " " + Convert.ToString(dtKhoiLuong.Rows[k]["sDonViTinhSL"]) + "; ";
                        }
                    }
                    dtKhoiLuong.Dispose();

                    HoSoXNCLModels hsXNCL = clHangHoa.GetHoSoXNCL(Convert.ToInt64(r["iID_MaHoSo"]), Convert.ToInt64(r["iID_MaHangHoa"]));

                    if (String.IsNullOrEmpty(Convert.ToString(r["sMua_FromDate"])) == false)
                    {
                        sMua_FromDate = Convert.ToDateTime(r["sMua_FromDate"]).ToString("dd/MM/yyyy");
                    }
                    if (String.IsNullOrEmpty(Convert.ToString(r["sMua_ToDate"])) == false)
                    {
                        sMua_ToDate = Convert.ToDateTime(r["sMua_ToDate"]).ToString("dd/MM/yyyy");
                    }

                    r["sGiaVN"] = CommonFunction.DinhDangSo(r["rGiaVN"]);
                    r["sGiaUSD"] = CommonFunction.DinhDangSo(r["rGiaUSD"]);

                    r["sSoLuong"] = sSoLuong;
                    r["sKhoiLuong"] = sKhoiLuong;
                    r["sKhoiLuongTan"] = sKhoiLuongTan;
                    r["sThoiGianNhap"] = Convert.ToString(sMua_FromDate + " - " + sMua_ToDate);
                    r["sTenToChucChungNhan"] = Convert.ToString(hsXNCL.sTenToChuc);
                }
            }
            dt.Dispose();

            dt.TableName = "ChiTiet";
            fr.AddTable("ChiTiet", dt);

            fr.SetValue("TenCongTy", sTenCongTy);
            //fr.SetValue("DienThoaiCongTy", sDienThoaiCongTy);
            //fr.SetValue("DiaChiCongTy", sDiChi);

            fr.SetValue("Ngay", dNow.Day);
            fr.SetValue("Thang", dNow.Month);
            fr.SetValue("Nam", dNow.Year);

            fr.SetValue("TuNgay", sModel.TuNgay);
            fr.SetValue("DenNgay", sModel.DenNgay);
        }

        public clsExcelResult BaoCao01ExpExcel(String TuNgay, String DenNgay)
        {
            string sMaND = User.Identity.Name;
            String sFilePath = "/ExcelFrom/rptCNN_CVBaoCao01.xls";
            clsExcelResult clsResult = new clsExcelResult();
            ExcelFile xls = BaoCao01CreateReport(Server.MapPath(sFilePath), sMaND, TuNgay, DenNgay);

            using (MemoryStream ms = new MemoryStream())
            {
                xls.Save(ms);
                ms.Position = 0;
                clsResult.ms = ms;
                clsResult.FileName = "BC_BAOCAO01.xls";
                clsResult.type = "xls";
                return clsResult;
            }
        }
        #endregion

        #region BaoCao02
        [Authorize]
        public ActionResult BaoCao02(ReportSearchModels model)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "CNN25_HoSo", "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            if (model == null)
            {
                model = new ReportSearchModels { };
            }
            ViewData["menu"] = 262;
            return View(model);
        }
        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult BaoCao02Search(string ParentID)
        {
            string _TuNgay = CString.SafeString(Request.Form[ParentID + "_viTuNgay"]);
            string _DenNgay = CString.SafeString(Request.Form[ParentID + "_viDenNgay"]);
            string sNuocSanXuat = CString.SafeString(Request.Form[ParentID + "_NuocSanXuat"]);
            ReportSearchModels model = new ReportSearchModels
            {
                TuNgay = _TuNgay,
                DenNgay = _DenNgay,
                sNuocSanXuat = sNuocSanXuat
            };

            return RedirectToAction("BaoCao02", model);
        }
        [Authorize]
        public ActionResult BaoCao02Print(String sNuocSanXuat, String TuNgay, String DenNgay)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "CNN25_HoSo", "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            //string _sMaSoThue = CString.SafeString(Request.Form[ParentID + "_DoanhNghiep"]);
            //string _TuNgay = CString.SafeString(Request.Form[ParentID + "_viTuNgay"]);
            //string _DenNgay = CString.SafeString(Request.Form[ParentID + "_viDenNgay"]);
            ViewBag.sNuocSanXuat = sNuocSanXuat;
            ViewBag.TuNgay = TuNgay;
            ViewBag.DenNgay = DenNgay;
            ViewData["menu"] = 262;
            return View();
        }

        [Authorize]
        public ActionResult BaoCao02ViewPDF(String MaND, String sNuocSanXuat, String TuNgay, String DenNgay)
        {
            CHamRieng.Language();

            String sFilePathPrint = "/ExcelFrom/rptCNN_CVBaoCao02.xls";
            ExcelFile xls = BaoCao02CreateReport(Server.MapPath(sFilePathPrint), MaND, sNuocSanXuat, TuNgay, DenNgay);
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

        public ExcelFile BaoCao02CreateReport(String path, String MaND, String sNuocSanXuat, String TuNgay, String DenNgay)
        {
            XlsFile Result = new XlsFile(true);
            Result.Open(path);

            ReportSearchModels sModel = new ReportSearchModels
            {
                TuNgay = TuNgay,
                DenNgay = DenNgay,
                sNuocSanXuat = sNuocSanXuat
            };

            FlexCelReport fr = new FlexCelReport();
            BaoCao02LoadData(fr, MaND, sModel);

            fr.Run(Result);
            return Result;

        }

        private void BaoCao02LoadData(FlexCelReport fr, String MaND, ReportSearchModels sModel)
        {
            DateTime dNow = DateTime.Now;

            DataTable dtCT = CCongTy.Get_Table_Top();
            String sTenCongTy = Convert.ToString(dtCT.Rows[0]["sTenCongTy"]);
            String sDienThoaiCongTy = Convert.ToString(dtCT.Rows[0]["sDienThoai"]);
            String sDiChi = Convert.ToString(dtCT.Rows[0]["sDiaChi_In"]);
            Byte[] sLogo = (byte[])dtCT.Rows[0]["sLogo"];

            //DataTable dtNuocSanXuatVal = CommonFunction.SearchInAllColums(CHangHoa.Get_DanhSach_NuocSanXuat(), sModel.sNuocSanXuat);

            DataTable dt = clBaoCao.CVBaoCao02_Print(sModel);
            dt.Columns.Add("sKhoiLuongTanHT", typeof(System.String));
            dt.Columns.Add("sGiaTriUSDHT", typeof(System.String));
            dt.Columns.Add("sSoLoHT", typeof(System.String));

            if (dt.Rows.Count > 0)
            {
                DataRow r;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    r = dt.Rows[i];

                    r["sKhoiLuongTanHT"] = Convert.ToDouble(r["sKhoiLuongTan"]).ToString("#,##"); ;
                    r["sGiaTriUSDHT"] = Convert.ToDouble(r["sGiaTriUSD"]).ToString("#,##"); ;
                    r["sSoLoHT"] = Convert.ToDouble(r["sSoLo"]).ToString("#,##"); ;
                }
            }
            dt.Dispose();

            dt.TableName = "ChiTiet";
            fr.AddTable("ChiTiet", dt);

            fr.SetValue("TenCongTy", sTenCongTy);
            //fr.SetValue("DienThoaiCongTy", sDienThoaiCongTy);
            //fr.SetValue("DiaChiCongTy", sDiChi);

            fr.SetValue("Ngay", dNow.Day);
            fr.SetValue("Thang", dNow.Month);
            fr.SetValue("Nam", dNow.Year);

            DataRow[] filteredRows = CHangHoa.Get_DanhSach_NuocSanXuat().Select("sMaQuocGia = '" + sModel.sNuocSanXuat + "'");
            if (filteredRows.Length == 1)
            {
                fr.SetValue("NuocSanXuat", Convert.ToString(filteredRows[0]["sTenQuocGia"]));
            }
            else
            {
                fr.SetValue("NuocSanXuat", "");
            }
            
            fr.SetValue("TuNgay", sModel.TuNgay);
            fr.SetValue("DenNgay", sModel.DenNgay);
        }

        public clsExcelResult BaoCao02ExpExcel(String sNuocSanXuat, String TuNgay, String DenNgay)
        {
            string sMaND = User.Identity.Name;
            String sFilePath = "/ExcelFrom/rptCNN_CVBaoCao02.xls";
            clsExcelResult clsResult = new clsExcelResult();
            ExcelFile xls = BaoCao02CreateReport(Server.MapPath(sFilePath), sMaND, sNuocSanXuat, TuNgay, DenNgay);

            using (MemoryStream ms = new MemoryStream())
            {
                xls.Save(ms);
                ms.Position = 0;
                clsResult.ms = ms;
                clsResult.FileName = "BC_BAOCAO02.xls";
                clsResult.type = "xls";
                return clsResult;
            }
        }
        #endregion

        #region BaoCao03
        [Authorize]
        public ActionResult BaoCao03(ReportSearchModels model)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "CNN25_HoSo", "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            if (model == null)
            {
                model = new ReportSearchModels { };
            }
            ViewData["menu"] = 263;
            return View(model);
        }
        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult BaoCao03Search(string ParentID)
        {
            string _TuNgay = CString.SafeString(Request.Form[ParentID + "_viTuNgay"]);
            string _DenNgay = CString.SafeString(Request.Form[ParentID + "_viDenNgay"]);
            string sNuocSanXuat = CString.SafeString(Request.Form[ParentID + "_PhanLoai"]);
            ReportSearchModels model = new ReportSearchModels
            {
                TuNgay = _TuNgay,
                DenNgay = _DenNgay,
                sNuocSanXuat = sNuocSanXuat
            };
            return RedirectToAction("BaoCao03", model);
        }
        [Authorize]
        public ActionResult BaoCao03Print(String sNuocSanXuat, String TuNgay, String DenNgay)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "CNN25_HoSo", "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            //string _sMaSoThue = CString.SafeString(Request.Form[ParentID + "_DoanhNghiep"]);
            //string _TuNgay = CString.SafeString(Request.Form[ParentID + "_viTuNgay"]);
            //string _DenNgay = CString.SafeString(Request.Form[ParentID + "_viDenNgay"]);
            ViewBag.sNuocSanXuat = sNuocSanXuat;
            ViewBag.TuNgay = TuNgay;
            ViewBag.DenNgay = DenNgay;
            ViewData["menu"] = 263;
            return View();
        }

        [Authorize]
        public ActionResult BaoCao03ViewPDF(String MaND, String sNuocSanXuat, String TuNgay, String DenNgay)
        {
            CHamRieng.Language();

            String sFilePathPrint = "/ExcelFrom/rptCNN_CVBaoCao03.xls";
            ExcelFile xls = BaoCao03CreateReport(Server.MapPath(sFilePathPrint), MaND, sNuocSanXuat, TuNgay, DenNgay);
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

        public ExcelFile BaoCao03CreateReport(String path, String MaND, String sNuocSanXuat, String TuNgay, String DenNgay)
        {
            XlsFile Result = new XlsFile(true);
            Result.Open(path);

            ReportSearchModels sModel = new ReportSearchModels
            {
                TuNgay = TuNgay,
                DenNgay = DenNgay,
                sNuocSanXuat = sNuocSanXuat
            };

            FlexCelReport fr = new FlexCelReport();
            BaoCao03LoadData(fr, MaND, sModel);

            fr.Run(Result);
            return Result;

        }

        private void BaoCao03LoadData(FlexCelReport fr, String MaND, ReportSearchModels sModel)
        {
            DateTime dNow = DateTime.Now;

            DataTable dtCT = CCongTy.Get_Table_Top();
            String sTenCongTy = Convert.ToString(dtCT.Rows[0]["sTenCongTy"]);
            String sDienThoaiCongTy = Convert.ToString(dtCT.Rows[0]["sDienThoai"]);
            String sDiChi = Convert.ToString(dtCT.Rows[0]["sDiaChi_In"]);
            Byte[] sLogo = (byte[])dtCT.Rows[0]["sLogo"];

            DataTable dt = clBaoCao.CVBaoCao03(sModel);
            dt.Columns.Add("sTyLeKL", typeof(System.String));
            dt.Columns.Add("sTyLeGT", typeof(System.String));

            if (dt.Rows.Count > 0)
            {
                Double rTongKL = 0, rTongTL = 0;

                DataRow r;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    r = dt.Rows[i];

                    Double sTyLeKL = 0, sTyLeGT = 0;

                    rTongKL = Convert.ToDouble(dt.Compute("SUM(sKhoiLuongTan)", string.Empty));
                    rTongTL = Convert.ToDouble(dt.Compute("SUM(sGiaTriUSD)", string.Empty));

                    sTyLeKL = Convert.ToDouble(Math.Round(Convert.ToDouble(r["sKhoiLuongTan"]) / rTongKL * 100, 2, MidpointRounding.AwayFromZero));
                    sTyLeGT = Convert.ToDouble(Math.Round(Convert.ToDouble(r["sGiaTriUSD"]) / rTongTL * 100, 2, MidpointRounding.AwayFromZero));

                    r["sTyLeKL"] = sTyLeKL;
                    r["sTyLeGT"] = sTyLeGT;
                }
            }
            dt.Dispose();

            dt.TableName = "ChiTiet";
            fr.AddTable("ChiTiet", dt);

            fr.SetValue("TenCongTy", sTenCongTy);
            //fr.SetValue("DienThoaiCongTy", sDienThoaiCongTy);
            //fr.SetValue("DiaChiCongTy", sDiChi);

            fr.SetValue("Ngay", dNow.Day);
            fr.SetValue("Thang", dNow.Month);
            fr.SetValue("Nam", dNow.Year);

            DataRow[] filteredRows = clDanhMuc.GetDataTable_TenKhoa("PHANLOAITACN").Select("sMa = '" + sModel.sNuocSanXuat + "'");
            if (filteredRows.Length == 1)
            {
                fr.SetValue("PhanLoaiTACN", Convert.ToString(filteredRows[0]["sTen"]));
            }
            else
            {
                fr.SetValue("PhanLoaiTACN", "");
            }
            fr.SetValue("TuNgay", sModel.TuNgay);
            fr.SetValue("DenNgay", sModel.DenNgay);
        }

        public clsExcelResult BaoCao03ExpExcel(String sNuocSanXuat, String TuNgay, String DenNgay)
        {
            string sMaND = User.Identity.Name;
            String sFilePath = "/ExcelFrom/rptCNN_CVBaoCao03.xls";
            clsExcelResult clsResult = new clsExcelResult();
            ExcelFile xls = BaoCao03CreateReport(Server.MapPath(sFilePath), sMaND, sNuocSanXuat, TuNgay, DenNgay);

            using (MemoryStream ms = new MemoryStream())
            {
                xls.Save(ms);
                ms.Position = 0;
                clsResult.ms = ms;
                clsResult.FileName = "BC_BAOCAO03.xls";
                clsResult.type = "xls";
                return clsResult;
            }
        }
        #endregion

        #region BaoCao04
        [Authorize]
        public ActionResult BaoCao04(ReportSearchModels model)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "CNN25_HoSo", "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            if (model == null)
            {
                model = new ReportSearchModels { };
            }
            ViewData["menu"] = 264;
            return View(model);
        }
        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult BaoCao04Search(string ParentID)
        {
            string _TuNgay = CString.SafeString(Request.Form[ParentID + "_viTuNgay"]);
            string _DenNgay = CString.SafeString(Request.Form[ParentID + "_viDenNgay"]);
            ReportSearchModels model = new ReportSearchModels
            {
                TuNgay = _TuNgay,
                DenNgay = _DenNgay
            };
            return RedirectToAction("BaoCao04", model);
        }
        [Authorize]
        public ActionResult BaoCao04Print(String TuNgay, String DenNgay)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "CNN25_HoSo", "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            ViewBag.TuNgay = TuNgay;
            ViewBag.DenNgay = DenNgay;
            ViewData["menu"] = 264;
            return View();
        }
        public clsExcelResult BaoCao04ExpExcel(String TuNgay, String DenNgay)
        {
            string sMaND = User.Identity.Name;
            String sFilePath = "/ExcelFrom/rptCNN_CVBaoCao04.xls";
            clsExcelResult clsResult = new clsExcelResult();
            ExcelFile xls = BaoCao04CreateReport(Server.MapPath(sFilePath), sMaND, TuNgay, DenNgay);

            using (MemoryStream ms = new MemoryStream())
            {
                xls.Save(ms);
                ms.Position = 0;
                clsResult.ms = ms;
                clsResult.FileName = "BC_BAOCAO04.xls";
                clsResult.type = "xls";
                return clsResult;
            }
        }
        [Authorize]
        public ActionResult BaoCao04ViewPDF(String MaND, String TuNgay, String DenNgay)
        {
            CHamRieng.Language();

            String sFilePathPrint = "/ExcelFrom/rptCNN_CVBaoCao04.xls";
            ExcelFile xls = BaoCao04CreateReport(Server.MapPath(sFilePathPrint), MaND, TuNgay, DenNgay);
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
        public ExcelFile BaoCao04CreateReport(String path, String MaND, String TuNgay, String DenNgay)
        {
            XlsFile Result = new XlsFile(true);
            Result.Open(path);

            ReportSearchModels sModel = new ReportSearchModels
            {
                TuNgay = TuNgay,
                DenNgay = DenNgay
            };

            FlexCelReport fr = new FlexCelReport();
            BaoCao04LoadData(fr, MaND, sModel);

            fr.Run(Result);
            return Result;

        }
        private void BaoCao04LoadData(FlexCelReport fr, String MaND, ReportSearchModels sModel)
        {
            DateTime dNow = DateTime.Now;

            DataTable dtCT = CCongTy.Get_Table_Top();
            String sTenCongTy = Convert.ToString(dtCT.Rows[0]["sTenCongTy"]);
            String sDienThoaiCongTy = Convert.ToString(dtCT.Rows[0]["sDienThoai"]);
            String sDiChi = Convert.ToString(dtCT.Rows[0]["sDiaChi_In"]);
            Byte[] sLogo = (byte[])dtCT.Rows[0]["sLogo"];

            Double rTong = 0, rTongKL = 0, rTongTL = 0;

            DataTable dt = clBaoCao.CVBaoCao04(sModel);
            dt.Columns.Add("sTyLeXNCL", typeof(System.String));
            dt.Columns.Add("sTyLeKL", typeof(System.String));
            dt.Columns.Add("sTyLeGT", typeof(System.String));

            if (dt.Rows.Count > 0)
            {
                DataRow r;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    r = dt.Rows[i];

                    Double sTyLeXNCL = 0, sTyLeKL = 0, sTyLeGT = 0;

                    rTong = Convert.ToDouble(dt.Compute("SUM(sTongXNCL)", string.Empty));
                    rTongKL = Convert.ToDouble(dt.Compute("SUM(sSoLuong)", string.Empty));
                    rTongTL = Convert.ToDouble(dt.Compute("SUM(sGiaTriUSD)", string.Empty));

                    sTyLeXNCL = Math.Round(Convert.ToDouble(Convert.ToDouble(r["sTongXNCL"]) / rTong * 100), 2, MidpointRounding.AwayFromZero);
                    sTyLeKL = Math.Round(Convert.ToDouble(Convert.ToDouble(r["sSoLuong"]) / rTongKL * 100), 2, MidpointRounding.AwayFromZero);
                    sTyLeGT = Math.Round(Convert.ToDouble(Convert.ToDouble(r["sGiaTriUSD"]) / rTongTL * 100), 2, MidpointRounding.AwayFromZero);

                    r["sTyLeXNCL"] = sTyLeXNCL;
                    r["sTyLeKL"] = sTyLeKL;
                    r["sTyLeGT"] = sTyLeGT;
                }
            }
            dt.Dispose();

            dt.TableName = "ChiTiet";
            fr.AddTable("ChiTiet", dt);

            fr.SetValue("TenCongTy", sTenCongTy);
            //fr.SetValue("DienThoaiCongTy", sDienThoaiCongTy);
            //fr.SetValue("DiaChiCongTy", sDiChi);

            fr.SetValue("Ngay", dNow.Day);
            fr.SetValue("Thang", dNow.Month);
            fr.SetValue("Nam", dNow.Year);

            if(String.IsNullOrEmpty(sModel.TuNgay) == false)
            {
                fr.SetValue("TuNgay", sModel.TuNgay);
            }   
            else
            {
                fr.SetValue("TuNgay", "...");
            }
            if (String.IsNullOrEmpty(sModel.DenNgay) == false)
            {
                fr.SetValue("DenNgay", sModel.DenNgay);
            }
            else
            {
                fr.SetValue("DenNgay", "...");
            }  
        }
        #endregion

        #region BaoCao05
        [Authorize]
        public ActionResult BaoCao05(ReportSearchModels model)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "CNN25_HoSo", "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            if (model == null)
            {
                model = new ReportSearchModels { };
            }
            ViewData["menu"] = 265;
            return View(model);
        }
        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult BaoCao05Search(string ParentID)
        {
            string _TuNgay = CString.SafeString(Request.Form[ParentID + "_viTuNgay"]);
            string _DenNgay = CString.SafeString(Request.Form[ParentID + "_viDenNgay"]);
            ReportSearchModels model = new ReportSearchModels
            {
                TuNgay = _TuNgay,
                DenNgay = _DenNgay
            };
            return RedirectToAction("BaoCao05", model);
        }
        [Authorize]
        public ActionResult BaoCao05Print(String TuNgay, String DenNgay)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "CNN25_HoSo", "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            ViewBag.TuNgay = TuNgay;
            ViewBag.DenNgay = DenNgay;
            ViewData["menu"] = 265;
            return View();
        }
        public clsExcelResult BaoCao05ExpExcel(String TuNgay, String DenNgay)
        {
            string sMaND = User.Identity.Name;
            String sFilePath = "/ExcelFrom/rptCNN_CVBaoCao05.xls";
            clsExcelResult clsResult = new clsExcelResult();
            ExcelFile xls = BaoCao05CreateReport(Server.MapPath(sFilePath), sMaND, TuNgay, DenNgay);

            using (MemoryStream ms = new MemoryStream())
            {
                xls.Save(ms);
                ms.Position = 0;
                clsResult.ms = ms;
                clsResult.FileName = "BC_BAOCAO05.xls";
                clsResult.type = "xls";
                return clsResult;
            }
        }
        [Authorize]
        public ActionResult BaoCao05ViewPDF(String MaND, String TuNgay, String DenNgay)
        {
            CHamRieng.Language();

            String sFilePathPrint = "/ExcelFrom/rptCNN_CVBaoCao05.xls";
            ExcelFile xls = BaoCao05CreateReport(Server.MapPath(sFilePathPrint), MaND, TuNgay, DenNgay);
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
        public ExcelFile BaoCao05CreateReport(String path, String MaND, String TuNgay, String DenNgay)
        {
            XlsFile Result = new XlsFile(true);
            Result.Open(path);

            ReportSearchModels sModel = new ReportSearchModels
            {
                TuNgay = TuNgay,
                DenNgay = DenNgay
            };

            FlexCelReport fr = new FlexCelReport();
            BaoCao05LoadData(fr, MaND, sModel);

            fr.Run(Result);
            return Result;

        }
        private void BaoCao05LoadData(FlexCelReport fr, String MaND, ReportSearchModels sModel)
        {
            DateTime dNow = DateTime.Now;

            DataTable dtCT = CCongTy.Get_Table_Top();
            String sTenCongTy = Convert.ToString(dtCT.Rows[0]["sTenCongTy"]);
            String sDienThoaiCongTy = Convert.ToString(dtCT.Rows[0]["sDienThoai"]);
            String sDiChi = Convert.ToString(dtCT.Rows[0]["sDiaChi_In"]);
            Byte[] sLogo = (byte[])dtCT.Rows[0]["sLogo"];

            Double rTongKL = 0, rTongTL = 0;

            DataTable dtDonVi = clDanhMuc.GetDataTable_TenKhoa("NHOMTACN", "sMa");
            dtDonVi.Columns.Add("sSTT", typeof(System.String));
            if (dtDonVi.Rows.Count > 0)
            {
                DataRow r;
                for (int i = 0; i < dtDonVi.Rows.Count; i++)
                {
                    r = dtDonVi.Rows[i];

                    int iSTT = i + 1;
                    string sSTTLaMa = CHamRieng.ToRoman(iSTT);

                    r["sSTT"] = sSTTLaMa;
                }
            }

            DataTable dt = clBaoCao.CVBaoCao05(sModel);
            dt.Columns.Add("sTyLeKL", typeof(System.Double));
            dt.Columns.Add("sTyLeGT", typeof(System.Double));
            dt.Columns.Add("sSTT", typeof(System.String));
            if (dt.Rows.Count > 0)
            {
                Double rTongKL_G = 0, rTongTL_G = 0;

                DataRow r;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    r = dt.Rows[i];

                    Double sTyLeKL = 0, sTyLeGT = 0;

                    rTongKL = Convert.ToDouble(dt.Compute("SUM(sSoLuongTan)", string.Empty));
                    rTongTL = Convert.ToDouble(dt.Compute("SUM(sGiaTriUSD)", string.Empty));

                    rTongKL_G = Convert.ToDouble(dt.Compute("SUM(sSoLuongTan)", "sMa = " + r["sMa"] + ""));
                    rTongTL_G = Convert.ToDouble(dt.Compute("SUM(sGiaTriUSD)", "sMa = " + r["sMa"] + ""));

                    sTyLeKL = Convert.ToDouble(Math.Round(Convert.ToDouble(r["sSoLuongTan"]) / rTongKL_G * 100, 2, MidpointRounding.AwayFromZero));
                    sTyLeGT = Convert.ToDouble(Math.Round(Convert.ToDouble(r["sGiaTriUSD"]) / rTongTL_G * 100, 2, MidpointRounding.AwayFromZero));

                    r["sTyLeKL"] = sTyLeKL;
                    r["sTyLeGT"] = sTyLeGT;
                    r["sSTT"] = i + 1;
                }
            }
            dt.Dispose();

            dt.TableName = "ChiTiet";
            fr.AddTable("ChiTiet", dt);
            fr.AddTable("DonVi", dtDonVi);

            fr.SetValue("TenCongTy", sTenCongTy);
            //fr.SetValue("DienThoaiCongTy", sDienThoaiCongTy);
            //fr.SetValue("DiaChiCongTy", sDiChi);

            fr.SetValue("TongCongKL", rTongKL);
            fr.SetValue("TongCongGT", rTongTL);

            fr.SetValue("Ngay", dNow.Day);
            fr.SetValue("Thang", dNow.Month);
            fr.SetValue("Nam", dNow.Year);

            if (String.IsNullOrEmpty(sModel.TuNgay) == false)
            {
                fr.SetValue("TuNgay", sModel.TuNgay);
            }
            else
            {
                fr.SetValue("TuNgay", "...");
            }
            if (String.IsNullOrEmpty(sModel.DenNgay) == false)
            {
                fr.SetValue("DenNgay", sModel.DenNgay);
            }
            else
            {
                fr.SetValue("DenNgay", "...");
            }
        }
        #endregion

        #region BaoCao06
        [Authorize]
        public ActionResult BaoCao06(ReportSearchModels model)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "CNN25_HoSo", "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            if (model == null)
            {
                model = new ReportSearchModels { };
            }
            ViewData["menu"] = 266;
            return View(model);
        }
        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult BaoCao06Search(string ParentID)
        {
            string _TuNgay = CString.SafeString(Request.Form[ParentID + "_viTuNgay"]);
            string _DenNgay = CString.SafeString(Request.Form[ParentID + "_viDenNgay"]);
            ReportSearchModels model = new ReportSearchModels
            {
                TuNgay = _TuNgay,
                DenNgay = _DenNgay
            };
            return RedirectToAction("BaoCao06", model);
        }
        [Authorize]
        public ActionResult BaoCao06Print(String TuNgay, String DenNgay)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "CNN25_HoSo", "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            ViewBag.TuNgay = TuNgay;
            ViewBag.DenNgay = DenNgay;
            ViewData["menu"] = 266;
            return View();
        }
        public clsExcelResult BaoCao06ExpExcel(String TuNgay, String DenNgay)
        {
            string sMaND = User.Identity.Name;
            String sFilePath = "/ExcelFrom/rptCNN_CVBaoCao06.xls";
            clsExcelResult clsResult = new clsExcelResult();
            ExcelFile xls = BaoCao06CreateReport(Server.MapPath(sFilePath), sMaND, TuNgay, DenNgay);

            using (MemoryStream ms = new MemoryStream())
            {
                xls.Save(ms);
                ms.Position = 0;
                clsResult.ms = ms;
                clsResult.FileName = "BC_BAOCAO06.xls";
                clsResult.type = "xls";
                return clsResult;
            }
        }
        [Authorize]
        public ActionResult BaoCao06ViewPDF(String MaND, String TuNgay, String DenNgay)
        {
            CHamRieng.Language();

            String sFilePathPrint = "/ExcelFrom/rptCNN_CVBaoCao06.xls";
            ExcelFile xls = BaoCao06CreateReport(Server.MapPath(sFilePathPrint), MaND, TuNgay, DenNgay);
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
        public ExcelFile BaoCao06CreateReport(String path, String MaND, String TuNgay, String DenNgay)
        {
            XlsFile Result = new XlsFile(true);
            Result.Open(path);

            ReportSearchModels sModel = new ReportSearchModels
            {
                TuNgay = TuNgay,
                DenNgay = DenNgay
            };

            FlexCelReport fr = new FlexCelReport();
            BaoCao06LoadData(fr, MaND, sModel);

            fr.Run(Result);
            return Result;

        }
        private void BaoCao06LoadData(FlexCelReport fr, String MaND, ReportSearchModels sModel)
        {
            DateTime dNow = DateTime.Now;

            DataTable dtCT = CCongTy.Get_Table_Top();
            String sTenCongTy = Convert.ToString(dtCT.Rows[0]["sTenCongTy"]);
            String sDienThoaiCongTy = Convert.ToString(dtCT.Rows[0]["sDienThoai"]);
            String sDiChi = Convert.ToString(dtCT.Rows[0]["sDiaChi_In"]);
            Byte[] sLogo = (byte[])dtCT.Rows[0]["sLogo"];

            Double rTongKL = 0, rTongTL = 0;

            DataTable dt = clBaoCao.CVBaoCao06(sModel);
            dt.Columns.Add("sTyLeKL", typeof(System.String));
            dt.Columns.Add("sTyLeGT", typeof(System.String));

            if (dt.Rows.Count > 0)
            {
                DataRow r;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    r = dt.Rows[i];

                    Double sTyLeKL = 0, sTyLeGT = 0;

                    rTongKL = Convert.ToDouble(dt.Compute("SUM(sKhoiLuongTan)", string.Empty));
                    rTongTL = Convert.ToDouble(dt.Compute("SUM(sGiaTriUSD)", string.Empty));

                    sTyLeKL = Math.Round(Convert.ToDouble(Convert.ToDouble(r["sKhoiLuongTan"]) / rTongKL * 100), 2, MidpointRounding.AwayFromZero);
                    sTyLeGT = Math.Round(Convert.ToDouble(Convert.ToDouble(r["sGiaTriUSD"]) / rTongTL * 100), 2, MidpointRounding.AwayFromZero);

                    r["sTyLeKL"] = sTyLeKL;
                    r["sTyLeGT"] = sTyLeGT;
                }
            }
            dt.Dispose();

            dt.TableName = "ChiTiet";
            fr.AddTable("ChiTiet", dt);

            fr.SetValue("TenCongTy", sTenCongTy);
            //fr.SetValue("DienThoaiCongTy", sDienThoaiCongTy);
            //fr.SetValue("DiaChiCongTy", sDiChi);

            fr.SetValue("Ngay", dNow.Day);
            fr.SetValue("Thang", dNow.Month);
            fr.SetValue("Nam", dNow.Year);

            if (String.IsNullOrEmpty(sModel.TuNgay) == false)
            {
                fr.SetValue("TuNgay", sModel.TuNgay);
            }
            else
            {
                fr.SetValue("TuNgay", "...");
            }
            if (String.IsNullOrEmpty(sModel.DenNgay) == false)
            {
                fr.SetValue("DenNgay", sModel.DenNgay);
            }
            else
            {
                fr.SetValue("DenNgay", "...");
            }
        }
        #endregion

        #region BaoCao07
        [Authorize]
        public ActionResult BaoCao07(ReportSearchModels model)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "CNN25_HoSo", "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            if (model == null)
            {
                model = new ReportSearchModels { };
            }
            ViewData["menu"] = 265;
            return View(model);
        }
        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult BaoCao07Search(string ParentID)
        {
            string _TuNgay = CString.SafeString(Request.Form[ParentID + "_viTuNgay"]);
            string _DenNgay = CString.SafeString(Request.Form[ParentID + "_viDenNgay"]);
            ReportSearchModels model = new ReportSearchModels
            {
                TuNgay = _TuNgay,
                DenNgay = _DenNgay
            };
            return RedirectToAction("BaoCao07", model);
        }
        [Authorize]
        public ActionResult BaoCao07Print(String TuNgay, String DenNgay)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "CNN25_HoSo", "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            ViewBag.TuNgay = TuNgay;
            ViewBag.DenNgay = DenNgay;
            ViewData["menu"] = 265;
            return View();
        }
        public clsExcelResult BaoCao07ExpExcel(String TuNgay, String DenNgay)
        {
            string sMaND = User.Identity.Name;
            String sFilePath = "/ExcelFrom/rptCNN_CVBaoCao07.xls";
            clsExcelResult clsResult = new clsExcelResult();
            ExcelFile xls = BaoCao07CreateReport(Server.MapPath(sFilePath), sMaND, TuNgay, DenNgay);

            using (MemoryStream ms = new MemoryStream())
            {
                xls.Save(ms);
                ms.Position = 0;
                clsResult.ms = ms;
                clsResult.FileName = "BC_BAOCAO07.xls";
                clsResult.type = "xls";
                return clsResult;
            }
        }
        [Authorize]
        public ActionResult BaoCao07ViewPDF(String MaND, String TuNgay, String DenNgay)
        {
            CHamRieng.Language();

            String sFilePathPrint = "/ExcelFrom/rptCNN_CVBaoCao07.xls";
            ExcelFile xls = BaoCao07CreateReport(Server.MapPath(sFilePathPrint), MaND, TuNgay, DenNgay);
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
        public ExcelFile BaoCao07CreateReport(String path, String MaND, String TuNgay, String DenNgay)
        {
            XlsFile Result = new XlsFile(true);
            Result.Open(path);

            ReportSearchModels sModel = new ReportSearchModels
            {
                TuNgay = TuNgay,
                DenNgay = DenNgay
            };

            FlexCelReport fr = new FlexCelReport();
            BaoCao07LoadData(fr, MaND, sModel);

            fr.Run(Result);
            return Result;

        }
        private void BaoCao07LoadData(FlexCelReport fr, String MaND, ReportSearchModels sModel)
        {
            DateTime dNow = DateTime.Now;

            DataTable dtCT = CCongTy.Get_Table_Top();
            String sTenCongTy = Convert.ToString(dtCT.Rows[0]["sTenCongTy"]);
            String sDienThoaiCongTy = Convert.ToString(dtCT.Rows[0]["sDienThoai"]);
            String sDiChi = Convert.ToString(dtCT.Rows[0]["sDiaChi_In"]);
            Byte[] sLogo = (byte[])dtCT.Rows[0]["sLogo"];

            Double rTongKL = 0, rTongTL = 0, rTongLO = 0;

            DataTable dtDonVi = clDanhMuc.GetDataTable_BanChat("PHANLOAITACN", "sGhiChu");
            dtDonVi.Columns.Add("sSTT", typeof(System.String));
            if (dtDonVi.Rows.Count > 0)
            {
                DataRow r;
                for (int i = 0; i < dtDonVi.Rows.Count; i++)
                {
                    r = dtDonVi.Rows[i];

                    int iSTT = i + 1;
                    string sSTTLaMa = CHamRieng.ToRoman(iSTT);

                    r["sSTT"] = sSTTLaMa;
                }
            }

            DataTable dt = clBaoCao.CVBaoCao07(sModel);
            dt.Columns.Add("sSTT", typeof(System.String));
            if (dt.Rows.Count > 0)
            {
                DataRow r;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    r = dt.Rows[i];

                    String sTyLeXNCL = "", sTyLeKL = "", sTyLeGT = "";

                    rTongKL = Convert.ToDouble(dt.Compute("SUM(sKhoiLuongTan)", string.Empty));
                    rTongTL = Convert.ToDouble(dt.Compute("SUM(sGiaTriUSD)", string.Empty));
                    rTongLO = Convert.ToDouble(dt.Compute("SUM(sSoLo)", string.Empty));

                    r["sSTT"] = i + 1;
                }
            }
            
            dt.TableName = "ChiTiet";
            fr.AddTable("ChiTiet", dt);
            fr.AddTable("DonVi", dtDonVi);

            dt.Dispose();
            dtDonVi.Dispose();

            fr.SetValue("TenCongTy", sTenCongTy);
            //fr.SetValue("DienThoaiCongTy", sDienThoaiCongTy);
            //fr.SetValue("DiaChiCongTy", sDiChi);

            fr.SetValue("TongCongKL", rTongKL);
            fr.SetValue("TongCongGT", rTongTL);
            fr.SetValue("TongCongLO", rTongLO);

            fr.SetValue("Ngay", dNow.Day);
            fr.SetValue("Thang", dNow.Month);
            fr.SetValue("Nam", dNow.Year);

            if (String.IsNullOrEmpty(sModel.TuNgay) == false)
            {
                fr.SetValue("TuNgay", sModel.TuNgay);
            }
            else
            {
                fr.SetValue("TuNgay", "...");
            }
            if (String.IsNullOrEmpty(sModel.DenNgay) == false)
            {
                fr.SetValue("DenNgay", sModel.DenNgay);
            }
            else
            {
                fr.SetValue("DenNgay", "...");
            }
        }
        #endregion

        #region BaoCao08
        [Authorize]
        public ActionResult BaoCao08(ReportSearchModels model)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "CNN25_HoSo", "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            if (model == null)
            {
                model = new ReportSearchModels { };
            }
            ViewData["menu"] = 268;
            return View(model);
        }
        [Authorize, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult BaoCao08Search(string ParentID)
        {
            string _TuNgay = CString.SafeString(Request.Form[ParentID + "_viTuNgay"]);
            string _DenNgay = CString.SafeString(Request.Form[ParentID + "_viDenNgay"]);
            ReportSearchModels model = new ReportSearchModels
            {
                TuNgay = _TuNgay,
                DenNgay = _DenNgay
            };
            return RedirectToAction("BaoCao08", model);
        }
        [Authorize]
        public ActionResult BaoCao08Print(String TuNgay, String DenNgay)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "CNN25_HoSo", "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            ViewBag.TuNgay = TuNgay;
            ViewBag.DenNgay = DenNgay;
            ViewData["menu"] = 268;
            return View();
        }
        public clsExcelResult BaoCao08ExpExcel(String TuNgay, String DenNgay)
        {
            string sMaND = User.Identity.Name;
            String sFilePath = "/ExcelFrom/rptCNN_CVBaoCao08.xls";
            clsExcelResult clsResult = new clsExcelResult();
            ExcelFile xls = BaoCao08CreateReport(Server.MapPath(sFilePath), sMaND, TuNgay, DenNgay);

            using (MemoryStream ms = new MemoryStream())
            {
                xls.Save(ms);
                ms.Position = 0;
                clsResult.ms = ms;
                clsResult.FileName = "BC_BAOCAO08.xls";
                clsResult.type = "xls";
                return clsResult;
            }
        }
        [Authorize]
        public ActionResult BaoCao08ViewPDF(String MaND, String TuNgay, String DenNgay)
        {
            CHamRieng.Language();

            String sFilePathPrint = "/ExcelFrom/rptCNN_CVBaoCao08.xls";
            ExcelFile xls = BaoCao08CreateReport(Server.MapPath(sFilePathPrint), MaND, TuNgay, DenNgay);
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
        public ExcelFile BaoCao08CreateReport(String path, String MaND, String TuNgay, String DenNgay)
        {
            XlsFile Result = new XlsFile(true);
            Result.Open(path);

            ReportSearchModels sModel = new ReportSearchModels
            {
                TuNgay = TuNgay,
                DenNgay = DenNgay
            };

            FlexCelReport fr = new FlexCelReport();
            BaoCao08LoadData(fr, MaND, sModel);

            fr.Run(Result);
            return Result;

        }
        private void BaoCao08LoadData(FlexCelReport fr, String MaND, ReportSearchModels sModel)
        {
            DateTime dNow = DateTime.Now;

            DataTable dtCT = CCongTy.Get_Table_Top();
            String sTenCongTy = Convert.ToString(dtCT.Rows[0]["sTenCongTy"]);
            String sDienThoaiCongTy = Convert.ToString(dtCT.Rows[0]["sDienThoai"]);
            String sDiChi = Convert.ToString(dtCT.Rows[0]["sDiaChi_In"]);
            Byte[] sLogo = (byte[])dtCT.Rows[0]["sLogo"];

            Double rTongKL = 0, rTongTL = 0, rTongSLo = 0;

            DataTable dt = clBaoCao.CVBaoCao08(sModel);
            dt.Columns.Add("sTyLeKL", typeof(System.String));
            dt.Columns.Add("sTyLeGT", typeof(System.String));
            dt.Columns.Add("sTyLeSLo", typeof(System.String));

            if (dt.Rows.Count > 0)
            {
                DataRow r;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    r = dt.Rows[i];

                    Double sTyLeKL = 0, sTyLeGT = 0, sTyLeSLo = 0;

                    rTongKL = Convert.ToDouble(dt.Compute("SUM(sKhoiLuongTan)", string.Empty));
                    rTongTL = Convert.ToDouble(dt.Compute("SUM(sGiaTriUSD)", string.Empty));
                    rTongSLo = Convert.ToDouble(dt.Compute("SUM(sSoLo)", string.Empty));

                    sTyLeKL = Math.Round(Convert.ToDouble(Convert.ToDouble(r["sKhoiLuongTan"]) / rTongKL * 100), 2, MidpointRounding.AwayFromZero);
                    sTyLeGT = Math.Round(Convert.ToDouble(Convert.ToDouble(r["sGiaTriUSD"]) / rTongTL * 100), 2, MidpointRounding.AwayFromZero);
                    sTyLeSLo = Math.Round(Convert.ToDouble(Convert.ToDouble(r["sSoLo"]) / rTongSLo * 100), 2, MidpointRounding.AwayFromZero);

                    r["sTyLeKL"] = sTyLeKL;
                    r["sTyLeGT"] = sTyLeGT;
                    r["sTyLeSLo"] = sTyLeSLo;
                }
            }
            dt.Dispose();

            dt.TableName = "ChiTiet";
            fr.AddTable("ChiTiet", dt);

            fr.SetValue("TenCongTy", sTenCongTy);
            //fr.SetValue("DienThoaiCongTy", sDienThoaiCongTy);
            //fr.SetValue("DiaChiCongTy", sDiChi);

            fr.SetValue("Ngay", dNow.Day);
            fr.SetValue("Thang", dNow.Month);
            fr.SetValue("Nam", dNow.Year);

            if (String.IsNullOrEmpty(sModel.TuNgay) == false)
            {
                fr.SetValue("TuNgay", sModel.TuNgay);
            }
            else
            {
                fr.SetValue("TuNgay", "...");
            }
            if (String.IsNullOrEmpty(sModel.DenNgay) == false)
            {
                fr.SetValue("DenNgay", sModel.DenNgay);
            }
            else
            {
                fr.SetValue("DenNgay", "...");
            }
        }
        #endregion
    }
}