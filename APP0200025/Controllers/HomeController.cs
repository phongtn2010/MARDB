using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.DataVisualization.Charting;
using DATA0200025;
using DATA0200025.Models;
using DomainModel;

namespace APP0200025.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "CNN25_HoSo", "Detail") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult MyChart_GDK_Day()
        {
            int dacount1 = 20;
            int i;
            int Width = 1024;
            int Height = 500;
            SeriesChartType LoaiBieuDo = SeriesChartType.Column;
            List<String> arrChiTieu = new List<String>();
            List<Double> arrTG = new List<Double>();
            //Tạo các cột dữ liệu
            String strMaCot, TenTruc;
            //Dữ liệu SMS
            strMaCot = "Lượng đăng ký";
            TenTruc = "Tháng";

            int iNgay = 0, iThang = 0, iNam = 0;
            DateTime _dayNow = DateTime.Now;

            iNgay = _dayNow.Day;
            iThang = _dayNow.Month;
            iNam = _dayNow.Year;

            DataTable dtNgay = CommonFunction.GetDates(7);

            //Tao cột ngày
            for (i = 0; i < dtNgay.Rows.Count; i++)
            {
                arrChiTieu.Add(Convert.ToDateTime(dtNgay.Rows[i]["dNgay"]).ToString("dd/MM/yyyy"));
            }
            //Tao cột dữ liệu
            for (i = 0; i < dtNgay.Rows.Count; i++)
            {
                Double iGiaTri = 0;
                iGiaTri = clHoSo.GetThongKe_BieuDo_Ngay(Convert.ToDateTime(dtNgay.Rows[i]["dNgay"]).ToString("dd/MM/yyyy"));
                arrTG.Add(iGiaTri);
            }
            dtNgay.Dispose();

            Bitmap image = new Bitmap(500, 250);

            Graphics g = Graphics.FromImage(image);

            System.Web.UI.DataVisualization.Charting.Chart Chart2 = new System.Web.UI.DataVisualization.Charting.Chart();
            Chart2.Width = Width;
            Chart2.Height = Height;
            Chart2.RenderType = RenderType.ImageTag;
            Chart2.Palette = ChartColorPalette.BrightPastel;
            Title t = new Title("THỐNG KÊ HỒ SƠ ĐĂNG KÝ THEO NGÀY", Docking.Top, new System.Drawing.Font("Tahoma , Arial, Helvetica, sans-serif", 10, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
            Chart2.Titles.Add(t);

            //Căn chỉnh đường viền, cột
            Chart2.ChartAreas.Add(TenTruc);
            Chart2.ChartAreas[TenTruc].BackColor = Color.Transparent;
            Chart2.ChartAreas[TenTruc].AxisX.IsLabelAutoFit = true;
            Chart2.ChartAreas[TenTruc].AxisY.IsLabelAutoFit = false;
            Chart2.ChartAreas[TenTruc].AxisX.LabelStyle.Angle = 0;
            Chart2.ChartAreas[TenTruc].AxisX.LabelStyle.Font = new Font("Verdana", 8F, FontStyle.Regular);
            Chart2.ChartAreas[TenTruc].AxisY.LabelStyle.Font = new Font("Verdana", 8F, FontStyle.Regular);
            Chart2.ChartAreas[TenTruc].AxisY.MajorGrid.LineColor = Color.FromArgb(240, 240, 240);
            Chart2.ChartAreas[TenTruc].AxisX.MajorGrid.LineColor = Color.FromArgb(240, 240, 240);
            Chart2.ChartAreas[TenTruc].AxisX.Interval = 1;
            //Chart2.ChartAreas[TenTruc].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            Chart2.ChartAreas[TenTruc].AxisX.MajorGrid.Interval = 1;
            Chart2.ChartAreas[TenTruc].AxisX.MajorTickMark.Interval = 1;
            Chart2.ChartAreas[TenTruc].AxisX.MinorGrid.Interval = 0.5;
            Chart2.ChartAreas[TenTruc].AxisX.MinorTickMark.Interval = 0.5;
            Chart2.ChartAreas[TenTruc].Area3DStyle.Enable3D = false;
            Chart2.ChartAreas[TenTruc].AxisY.LabelStyle.Format = "#,###";


            // create a couple of series
            Chart2.Series.Add(TenTruc);
            Chart2.Series[TenTruc].Points.DataBindXY(arrChiTieu, arrTG);
            Chart2.Series[TenTruc].ChartType = LoaiBieuDo;
            Chart2.Series[TenTruc].IsValueShownAsLabel = true;
            Chart2.Series[TenTruc].Color = Color.CornflowerBlue;
            Chart2.Series[TenTruc].BackSecondaryColor = Color.SkyBlue;
            Chart2.Series[TenTruc].BackGradientStyle = GradientStyle.VerticalCenter;
            Chart2.Series[TenTruc].BorderColor = Color.Gray;
            Chart2.Series[TenTruc].BorderWidth = 1;
            Chart2.Series[TenTruc].BorderDashStyle = ChartDashStyle.Solid;
            Chart2.Series[TenTruc].ShadowOffset = 2;
            Chart2.Series[TenTruc]["PointWidth"] = "0.6";
            Chart2.Series[TenTruc].Font = new Font("Verdana,Arial,Helvetica,sans-serif", 8F, FontStyle.Regular);
            Chart2.Series[TenTruc]["BarLabelStyle"] = "Center";
            Chart2.Series[TenTruc].Label = "#VALY{#,###}";
            Chart2.Series[TenTruc].ToolTip = "#SMS" + ": " + "#VALX" + ": " + "#VALY" + "%";  // Hiển thị tooltip
            Chart2.Series[TenTruc].LegendUrl = "#";

            Chart2.BackColor = Color.FromArgb(211, 223, 240);
            Chart2.BorderlineDashStyle = ChartDashStyle.Solid;
            Chart2.BackSecondaryColor = Color.White;
            Chart2.BackGradientStyle = GradientStyle.TopBottom;
            Chart2.BorderlineWidth = 0;
            Chart2.BorderlineColor = Color.FromArgb(26, 59, 105);
            Chart2.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;


            MemoryStream imageStream = new MemoryStream();

            Chart2.SaveImage(imageStream, ChartImageFormat.Png);

            Chart2.TextAntiAliasingQuality = TextAntiAliasingQuality.SystemDefault;

            Response.ContentType = "image/png";

            imageStream.WriteTo(Response.OutputStream);

            g.Dispose();

            image.Dispose();

            return null;

        }

        public ActionResult MyChart_XNCL_Day()
        {
            int dacount1 = 20;
            int i;
            int Width = 1024;
            int Height = 500;
            SeriesChartType LoaiBieuDo = SeriesChartType.Column;
            List<String> arrChiTieu = new List<String>();
            List<Double> arrTG = new List<Double>();
            //Tạo các cột dữ liệu
            String strMaCot, TenTruc;
            //Dữ liệu SMS
            strMaCot = "Lượng đăng ký";
            TenTruc = "Tháng";

            int iNgay = 0, iThang = 0, iNam = 0;
            DateTime _dayNow = DateTime.Now;

            iNgay = _dayNow.Day;
            iThang = _dayNow.Month;
            iNam = _dayNow.Year;

            DataTable dtNgay = CommonFunction.GetDates(7);

            //Tao cột ngày
            for (i = 0; i < dtNgay.Rows.Count; i++)
            {
                arrChiTieu.Add(Convert.ToDateTime(dtNgay.Rows[i]["dNgay"]).ToString("dd/MM/yyyy"));
            }
            //Tao cột dữ liệu
            for (i = 0; i < dtNgay.Rows.Count; i++)
            {
                Double iGiaTri = 0;
                iGiaTri = clHoSo.GetThongKe_XNCL_BieuDo_Ngay(Convert.ToDateTime(dtNgay.Rows[i]["dNgay"]).ToString("dd/MM/yyyy"));
                arrTG.Add(iGiaTri);
            }
            dtNgay.Dispose();

            Bitmap image = new Bitmap(500, 250);

            Graphics g = Graphics.FromImage(image);

            System.Web.UI.DataVisualization.Charting.Chart Chart2 = new System.Web.UI.DataVisualization.Charting.Chart();
            Chart2.Width = Width;
            Chart2.Height = Height;
            Chart2.RenderType = RenderType.ImageTag;
            Chart2.Palette = ChartColorPalette.BrightPastel;
            Title t = new Title("THỐNG KÊ XÁC NHẬN CHẤT LƯỢNG THEO NGÀY", Docking.Top, new System.Drawing.Font("Tahoma , Arial, Helvetica, sans-serif", 10, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
            Chart2.Titles.Add(t);

            //Căn chỉnh đường viền, cột
            Chart2.ChartAreas.Add(TenTruc);
            Chart2.ChartAreas[TenTruc].BackColor = Color.Transparent;
            Chart2.ChartAreas[TenTruc].AxisX.IsLabelAutoFit = true;
            Chart2.ChartAreas[TenTruc].AxisY.IsLabelAutoFit = false;
            Chart2.ChartAreas[TenTruc].AxisX.LabelStyle.Angle = 0;
            Chart2.ChartAreas[TenTruc].AxisX.LabelStyle.Font = new Font("Verdana", 8F, FontStyle.Regular);
            Chart2.ChartAreas[TenTruc].AxisY.LabelStyle.Font = new Font("Verdana", 8F, FontStyle.Regular);
            Chart2.ChartAreas[TenTruc].AxisY.MajorGrid.LineColor = Color.FromArgb(240, 240, 240);
            Chart2.ChartAreas[TenTruc].AxisX.MajorGrid.LineColor = Color.FromArgb(240, 240, 240);
            Chart2.ChartAreas[TenTruc].AxisX.Interval = 1;
            //Chart2.ChartAreas[TenTruc].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            Chart2.ChartAreas[TenTruc].AxisX.MajorGrid.Interval = 1;
            Chart2.ChartAreas[TenTruc].AxisX.MajorTickMark.Interval = 1;
            Chart2.ChartAreas[TenTruc].AxisX.MinorGrid.Interval = 0.5;
            Chart2.ChartAreas[TenTruc].AxisX.MinorTickMark.Interval = 0.5;
            Chart2.ChartAreas[TenTruc].Area3DStyle.Enable3D = false;
            Chart2.ChartAreas[TenTruc].AxisY.LabelStyle.Format = "#,###";


            // create a couple of series
            Chart2.Series.Add(TenTruc);
            Chart2.Series[TenTruc].Points.DataBindXY(arrChiTieu, arrTG);
            Chart2.Series[TenTruc].ChartType = LoaiBieuDo;
            Chart2.Series[TenTruc].IsValueShownAsLabel = true;
            Chart2.Series[TenTruc].Color = Color.CornflowerBlue;
            Chart2.Series[TenTruc].BackSecondaryColor = Color.SkyBlue;
            Chart2.Series[TenTruc].BackGradientStyle = GradientStyle.VerticalCenter;
            Chart2.Series[TenTruc].BorderColor = Color.Gray;
            Chart2.Series[TenTruc].BorderWidth = 1;
            Chart2.Series[TenTruc].BorderDashStyle = ChartDashStyle.Solid;
            Chart2.Series[TenTruc].ShadowOffset = 2;
            Chart2.Series[TenTruc]["PointWidth"] = "0.6";
            Chart2.Series[TenTruc].Font = new Font("Verdana,Arial,Helvetica,sans-serif", 8F, FontStyle.Regular);
            Chart2.Series[TenTruc]["BarLabelStyle"] = "Center";
            Chart2.Series[TenTruc].Label = "#VALY{#,###}";
            Chart2.Series[TenTruc].ToolTip = "#SMS" + ": " + "#VALX" + ": " + "#VALY" + "%";  // Hiển thị tooltip
            Chart2.Series[TenTruc].LegendUrl = "#";

            Chart2.BackColor = Color.FromArgb(211, 223, 240);
            Chart2.BorderlineDashStyle = ChartDashStyle.Solid;
            Chart2.BackSecondaryColor = Color.White;
            Chart2.BackGradientStyle = GradientStyle.TopBottom;
            Chart2.BorderlineWidth = 0;
            Chart2.BorderlineColor = Color.FromArgb(26, 59, 105);
            Chart2.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;


            MemoryStream imageStream = new MemoryStream();

            Chart2.SaveImage(imageStream, ChartImageFormat.Png);

            Chart2.TextAntiAliasingQuality = TextAntiAliasingQuality.SystemDefault;

            Response.ContentType = "image/png";

            imageStream.WriteTo(Response.OutputStream);

            g.Dispose();

            image.Dispose();

            return null;

        }

        public ActionResult MyChart_MG_Day()
        {
            int dacount1 = 20;
            int i;
            int Width = 1024;
            int Height = 500;
            SeriesChartType LoaiBieuDo = SeriesChartType.Column;
            List<String> arrChiTieu = new List<String>();
            List<Double> arrTG = new List<Double>();
            //Tạo các cột dữ liệu
            String strMaCot, TenTruc;
            //Dữ liệu SMS
            strMaCot = "Lượng đăng ký";
            TenTruc = "Tháng";

            int iNgay = 0, iThang = 0, iNam = 0;
            DateTime _dayNow = DateTime.Now;

            iNgay = _dayNow.Day;
            iThang = _dayNow.Month;
            iNam = _dayNow.Year;

            DataTable dtNgay = CommonFunction.GetDates(7);

            //Tao cột ngày
            for (i = 0; i < dtNgay.Rows.Count; i++)
            {
                arrChiTieu.Add(Convert.ToDateTime(dtNgay.Rows[i]["dNgay"]).ToString("dd/MM/yyyy"));
            }
            //Tao cột dữ liệu
            for (i = 0; i < dtNgay.Rows.Count; i++)
            {
                Double iGiaTri = 0;
                iGiaTri = DATA0200026.CHoSo26.GetThongKe_BieuDo_Ngay(Convert.ToDateTime(dtNgay.Rows[i]["dNgay"]).ToString("dd/MM/yyyy"));
                arrTG.Add(iGiaTri);
            }
            dtNgay.Dispose();

            Bitmap image = new Bitmap(500, 250);

            Graphics g = Graphics.FromImage(image);

            System.Web.UI.DataVisualization.Charting.Chart Chart2 = new System.Web.UI.DataVisualization.Charting.Chart();
            Chart2.Width = Width;
            Chart2.Height = Height;
            Chart2.RenderType = RenderType.ImageTag;
            Chart2.Palette = ChartColorPalette.BrightPastel;
            Title t = new Title("THỐNG KÊ HỒ SƠ ĐĂNG KÝ MIỄN GIẢM THEO NGÀY", Docking.Top, new System.Drawing.Font("Tahoma , Arial, Helvetica, sans-serif", 10, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
            Chart2.Titles.Add(t);

            //Căn chỉnh đường viền, cột
            Chart2.ChartAreas.Add(TenTruc);
            Chart2.ChartAreas[TenTruc].BackColor = Color.Transparent;
            Chart2.ChartAreas[TenTruc].AxisX.IsLabelAutoFit = true;
            Chart2.ChartAreas[TenTruc].AxisY.IsLabelAutoFit = false;
            Chart2.ChartAreas[TenTruc].AxisX.LabelStyle.Angle = 0;
            Chart2.ChartAreas[TenTruc].AxisX.LabelStyle.Font = new Font("Verdana", 8F, FontStyle.Regular);
            Chart2.ChartAreas[TenTruc].AxisY.LabelStyle.Font = new Font("Verdana", 8F, FontStyle.Regular);
            Chart2.ChartAreas[TenTruc].AxisY.MajorGrid.LineColor = Color.FromArgb(240, 240, 240);
            Chart2.ChartAreas[TenTruc].AxisX.MajorGrid.LineColor = Color.FromArgb(240, 240, 240);
            Chart2.ChartAreas[TenTruc].AxisX.Interval = 1;
            //Chart2.ChartAreas[TenTruc].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            Chart2.ChartAreas[TenTruc].AxisX.MajorGrid.Interval = 1;
            Chart2.ChartAreas[TenTruc].AxisX.MajorTickMark.Interval = 1;
            Chart2.ChartAreas[TenTruc].AxisX.MinorGrid.Interval = 0.5;
            Chart2.ChartAreas[TenTruc].AxisX.MinorTickMark.Interval = 0.5;
            Chart2.ChartAreas[TenTruc].Area3DStyle.Enable3D = false;
            Chart2.ChartAreas[TenTruc].AxisY.LabelStyle.Format = "#,###";


            // create a couple of series
            Chart2.Series.Add(TenTruc);
            Chart2.Series[TenTruc].Points.DataBindXY(arrChiTieu, arrTG);
            Chart2.Series[TenTruc].ChartType = LoaiBieuDo;
            Chart2.Series[TenTruc].IsValueShownAsLabel = true;
            Chart2.Series[TenTruc].Color = Color.CornflowerBlue;
            Chart2.Series[TenTruc].BackSecondaryColor = Color.SkyBlue;
            Chart2.Series[TenTruc].BackGradientStyle = GradientStyle.VerticalCenter;
            Chart2.Series[TenTruc].BorderColor = Color.Gray;
            Chart2.Series[TenTruc].BorderWidth = 1;
            Chart2.Series[TenTruc].BorderDashStyle = ChartDashStyle.Solid;
            Chart2.Series[TenTruc].ShadowOffset = 2;
            Chart2.Series[TenTruc]["PointWidth"] = "0.6";
            Chart2.Series[TenTruc].Font = new Font("Verdana,Arial,Helvetica,sans-serif", 8F, FontStyle.Regular);
            Chart2.Series[TenTruc]["BarLabelStyle"] = "Center";
            Chart2.Series[TenTruc].Label = "#VALY{#,###}";
            Chart2.Series[TenTruc].ToolTip = "#SMS" + ": " + "#VALX" + ": " + "#VALY" + "%";  // Hiển thị tooltip
            Chart2.Series[TenTruc].LegendUrl = "#";

            Chart2.BackColor = Color.FromArgb(211, 223, 240);
            Chart2.BorderlineDashStyle = ChartDashStyle.Solid;
            Chart2.BackSecondaryColor = Color.White;
            Chart2.BackGradientStyle = GradientStyle.TopBottom;
            Chart2.BorderlineWidth = 0;
            Chart2.BorderlineColor = Color.FromArgb(26, 59, 105);
            Chart2.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;


            MemoryStream imageStream = new MemoryStream();

            Chart2.SaveImage(imageStream, ChartImageFormat.Png);

            Chart2.TextAntiAliasingQuality = TextAntiAliasingQuality.SystemDefault;

            Response.ContentType = "image/png";

            imageStream.WriteTo(Response.OutputStream);

            g.Dispose();

            image.Dispose();

            return null;

        }
    }
}