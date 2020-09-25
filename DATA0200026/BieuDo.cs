using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using DomainModel;
using DomainModel.Controls;
using DomainModel.Abstract;
using System.Collections.Specialized;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;

namespace DATA0200026
{
    public class BieuDoChungModel
    {
        public BieuDoChungModel(int MaBang, int MaDonVi, int MaCotMau_DonVi, int MaCotMau_Chuan, int Width, int Height)
        {
            this.MaBang = MaBang;
            this.MaDonVi = MaDonVi;
            this.MaCotMau_DonVi = MaCotMau_DonVi;
            this.MaCotMau_Chuan = MaCotMau_Chuan;
            this.Width = Width;
            this.Height = Height;
        }
        public int MaBang { get; set; }
        public int MaDonVi { get; set; }
        public int MaCotMau_DonVi { get; set; }
        public int MaCotMau_Chuan { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class BieuDoHangChungModel
    {
        public BieuDoHangChungModel(int MaBang, int MaDonVi, int MaCotMau_DonVi, int MaCotMau_Chuan, int MaHang, int ViTri, DateTime ThoiGian, int Width, int Height, int iSoThangTruoc, int iSoThangSau)
        {
            this.MaBang = MaBang;
            this.MaDonVi = MaDonVi;
            this.MaCotMau_DonVi = MaCotMau_DonVi;
            this.MaCotMau_Chuan = MaCotMau_Chuan;
            this.MaHang = MaHang;
            this.ViTri = ViTri;
            this.ThoiGian = ThoiGian;
            this.Width = Width;
            this.Height = Height;
            this.iSoThangTruoc = iSoThangTruoc;
            this.iSoThangSau = iSoThangSau;
        }
        public int MaBang { get; set; }
        public int MaDonVi { get; set; }
        public int MaCotMau_DonVi { get; set; }
        public int MaCotMau_Chuan { get; set; }
        public int MaHang { get; set; }
        public int ViTri { get; set; }
        public DateTime ThoiGian { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int iSoThangTruoc {get; set; }
        public int iSoThangSau {get; set; }
    }

    public class BieuDoHangChungLineModel
    {
        public BieuDoHangChungLineModel(int MaBang, int MaDonVi, int MaCotMau_DonVi, int MaCotMau_Chuan, String strMaHang, DateTime ThoiGian, int Width, int Height)
        {
            this.MaBang = MaBang;
            this.MaDonVi = MaDonVi;
            this.MaCotMau_DonVi = MaCotMau_DonVi;
            this.MaCotMau_Chuan = MaCotMau_Chuan;
            this.strMaHang = strMaHang;
            this.ThoiGian = ThoiGian;
            this.Width = Width;
            this.Height = Height;
        }
        public int MaBang { get; set; }
        public int MaDonVi { get; set; }
        public int MaCotMau_DonVi { get; set; }
        public int MaCotMau_Chuan { get; set; }
        public String strMaHang { get; set; }
        public DateTime ThoiGian { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class BieuDoHangSoSanhDonViModel
    {
        public BieuDoHangSoSanhDonViModel(int MaBang, int MaCotMau_DonVi, int MaCotMau_Chuan, int MaHang, int Width, int Height)
        {
            this.MaBang = MaBang;
            this.MaCotMau_DonVi = MaCotMau_DonVi;
            this.MaCotMau_Chuan = MaCotMau_Chuan;
            this.MaHang = MaHang;
            this.Width = Width;
            this.Height = Height;
        }
        public int MaBang { get; set; }
        public int MaCotMau_DonVi { get; set; }
        public int MaCotMau_Chuan { get; set; }
        public int MaHang { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
    
    public class BieuDo
    {
        public static DataTable LayBangVeBieuDo(int MaBang) {
            DataTable dt = new DataTable();
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * " +
                              "FROM BC_Bang_Cot " +
                              "WHERE iID_MaBang=@iID_MaBang ORDER BY iSTT;";
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            DataTable dtCot = Connection.GetDataTable(cmd);
            cmd.Dispose();

            DataTable dtHang;
            cmd = new SqlCommand("SELECT * FROM BC_Bang_Hang WHERE iID_MaBang=@iID_MaBang AND iMaTrangThai=1 ORDER BY iSTT");
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            dtHang = Connection.GetDataTable(cmd);
            cmd.Dispose();

            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            cmd.CommandText = "SELECT * " +
                              " FROM BC_Bang_ChiTiet " +
                              " WHERE (iID_MaBang=@iID_MaBang)";
            DataTable dtChiTiet = Connection.GetDataTable(cmd);
            cmd.Dispose();

            int nH = dtHang.Rows.Count;
            int nC = dtCot.Rows.Count;
            int i, j, h, c;

            dt.Columns.Add("CotMau", typeof(String));
            dt.Columns.Add("CotMa", typeof(String));            
            dt.Columns.Add("CotTenChiTieu", typeof(String));
            for (j = 0; j < nC; j++)
            {
                //if (Convert.ToBoolean( dtCot.Rows[j]["bVisible"]) == true)
                //{
                    dt.Columns.Add(String.Format("C{0}", dtCot.Rows[j]["iID_MaCot"]), typeof(String));
                //}
            }
            DataRow Row;
            //Dien gia tri da luu
            for (h = 0; h < nH; h++)
            {
                Row = dt.NewRow();
                dt.Rows.Add(Row);
                Row[0] = Convert.ToString(dtHang.Rows[h]["iID_MaHangMau"]);
                Row[1] = Convert.ToString(dtHang.Rows[h]["iID_MaHang"]);
                Row[2] = Convert.ToString(dtHang.Rows[h]["sTenHang"]);
                for (c = 0; c < nC; c++)
                {
                    for (i = 0; i < dtChiTiet.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dtHang.Rows[h]["iID_MaHang"]) == Convert.ToInt32(dtChiTiet.Rows[i]["iID_MaHang"]) &&
                           Convert.ToInt32(dtCot.Rows[c]["iID_MaCot"]) == Convert.ToInt32(dtChiTiet.Rows[i]["iID_MaCot"]))
                        {
                            Row[c+3] = dtChiTiet.Rows[i]["oGiaTri"];
                            break;
                        }
                    }
                }
            }
            return dt;
        }

        public static DataTable LayBangVeSoSanhCacDonVi(int MaBang, int MaCotMau_DonVi, int MaHangMau)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * " +
                              "FROM BC_Bang_Cot " +
                              "WHERE iID_MaBang=@iID_MaBang ORDER BY iSTT;";
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            DataTable dtCot = Connection.GetDataTable(cmd);
            cmd.Dispose();

            DataTable dtHang;
            cmd = new SqlCommand("SELECT * FROM BC_Bang_Hang WHERE iID_MaBang=@iID_MaBang AND iMaTrangThai=1 AND iID_MaHangMau=@iID_MaHangMau ORDER BY iSTT");
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
            dtHang = Connection.GetDataTable(cmd);
            int MaHang = Convert.ToInt32(dtHang.Rows[0]["iID_MaHang"]);
            cmd.Dispose();

            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            cmd.Parameters.AddWithValue("@iID_MaCotMau_DonVi", MaCotMau_DonVi);
            cmd.Parameters.AddWithValue("@iID_MaHang", MaHang);
            cmd.CommandText = "SELECT * " +
                              " FROM BC_Bang_ChiTiet " +
                              " WHERE (iID_MaBang=@iID_MaBang) AND iID_MaCotMau=@iID_MaCotMau_DonVi AND iID_MaHang=@iID_MaHang";
            DataTable dtChiTiet = Connection.GetDataTable(cmd);
            cmd.Dispose();

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * " +
                              " FROM BC_DonVi"+
                              " ORDER BY iSTT";
            DataTable dtDonVi = Connection.GetDataTable(cmd);
            cmd.Dispose();

            int nH = dtHang.Rows.Count;
            int nC = dtCot.Rows.Count;
            int i, j, h, c;

            dt.Columns.Add("TenDV", typeof(String));
            dt.Columns.Add("GiaTri", typeof(String));
            DataRow Row;
            //Dien gia tri da luu
            for (j = 0; j < dtDonVi.Rows.Count; j++)
            {
                Row = dt.NewRow();
                dt.Rows.Add(Row);
                Row["TenDV"] = Convert.ToString(dtDonVi.Rows[j]["sTenVietTat"]);
                for (i = 0; i < dtChiTiet.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dtDonVi.Rows[j]["iID_MaDonVi"]) == Convert.ToInt32(dtChiTiet.Rows[i]["iID_MaCot_MaDonVi"]))
                    {
                        Row["GiaTri"] = dtChiTiet.Rows[i]["oGiaTri"];
                        break;
                    }
                }
            }
            dtChiTiet.Dispose();
            dtHang.Dispose();
            dtCot.Dispose();
            dtDonVi.Dispose();
            return dt;
        }

        public static DataTable LayBangVeSoSanhDayChuyenCuaDonVi(int MaBang, int MaHangMau)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * " +
                              "FROM BC_Bang_Cot " +
                              "WHERE iID_MaBang=@iID_MaBang ORDER BY iSTT;";
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            DataTable dtCot = Connection.GetDataTable(cmd);
            cmd.Dispose();

            DataTable dtHang;
            cmd = new SqlCommand("SELECT * FROM BC_Bang_Hang WHERE iID_MaBang=@iID_MaBang AND iMaTrangThai=1 AND iID_MaHangMau=@iID_MaHangMau ORDER BY iSTT");
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
            dtHang = Connection.GetDataTable(cmd);
            int MaHang = Convert.ToInt32(dtHang.Rows[0]["iID_MaHang"]);
            cmd.Dispose();

            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            cmd.Parameters.AddWithValue("@iID_MaHang", MaHang);
            cmd.CommandText = "SELECT * " +
                              " FROM BC_Bang_ChiTiet " +
                              " WHERE (iID_MaBang=@iID_MaBang) AND iID_MaHang=@iID_MaHang";
            DataTable dtChiTiet = Connection.GetDataTable(cmd);
            cmd.Dispose();

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * " +
                              " FROM BC_DonVi" +
                              " ORDER BY iSTT";
            DataTable dtDonVi = Connection.GetDataTable(cmd);
            cmd.Dispose();

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT iID_MaBangMau FROM BC_Bang WHERE iID_MaBang=@iID_MaBang";
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            String MaBangMau = Convert.ToString(Connection.GetValue(cmd, ""));
            cmd.Dispose();

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * " +
                              "FROM BC_BangMau_CotMau_DonVi_TenMoi " +
                              "WHERE iID_MaBangMau=@iID_MaBangMau;";
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            DataTable dtCotMauDonViTenMoi = Connection.GetDataTable(cmd);
            cmd.Dispose();

            int nH = dtHang.Rows.Count;
            int nC = dtCot.Rows.Count;
            int i, j, h, c;

            dt.Columns.Add("MaCotMau", typeof(String));
            dt.Columns.Add("MaCot", typeof(String));
            dt.Columns.Add("iID_MaDonVi", typeof(String));
            dt.Columns.Add("iID_MaBangMau_DuLieu", typeof(String));
            dt.Columns.Add("iID_MaCotMau_DuLieu", typeof(String));
            dt.Columns.Add("TenCot", typeof(String));
            dt.Columns.Add("GiaTri", typeof(String));
            DataRow Row;
            for (h = 0; h < dtCot.Rows.Count; h++)
            {
                if (Convert.ToBoolean(dtCot.Rows[h]["bThuocDonVi"]))
                {
                    for (j = 0; j < dtCotMauDonViTenMoi.Rows.Count; j++)
                    {
                        if (Convert.ToInt32(dtCot.Rows[h]["iID_MaCotMau"]) == Convert.ToInt32(dtCotMauDonViTenMoi.Rows[j]["iID_MaCotMau"]) &&
                            Convert.ToInt32(dtCot.Rows[h]["iID_MaDonVi"]) == Convert.ToInt32(dtCotMauDonViTenMoi.Rows[j]["iID_MaDonVi"]))
                        {
                            dtCot.Rows[h]["sTenCot"] = dtCotMauDonViTenMoi.Rows[j]["sTenCot"];
                            break;
                        }
                    }
                }

                Row = dt.NewRow();
                dt.Rows.Add(Row);
                Row["MaCotMau"] = Convert.ToString(dtCot.Rows[h]["iID_MaCotMau"]);
                Row["MaCot"] = Convert.ToString(dtCot.Rows[h]["iID_MaCot"]);
                Row["iID_MaDonVi"] = Convert.ToString(dtCot.Rows[h]["iID_MaDonVi"]);
                Row["iID_MaBangMau_DuLieu"] = Convert.ToString(dtCot.Rows[h]["iID_MaBangMau_DuLieu"]);
                Row["iID_MaCotMau_DuLieu"] = Convert.ToString(dtCot.Rows[h]["iID_MaCotMau_DuLieu"]);
                Row["TenCot"] = Convert.ToString(dtCot.Rows[h]["sTenCot"]);
                for (i = 0; i < dtChiTiet.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dtCot.Rows[h]["iID_MaCot"]) == Convert.ToInt32(dtChiTiet.Rows[i]["iID_MaCot"]))
                    {
                        Row["GiaTri"] = dtChiTiet.Rows[i]["oGiaTri"];
                        break;
                    }
                }
            }
            dtChiTiet.Dispose();
            dtHang.Dispose();
            dtCot.Dispose();
            dtDonVi.Dispose();
            return dt;
        }

        public static DataTable LayBangVeBieuDoTheoChiTieu(String MaBangMau, String MaHangMau, String MaCotMau_DonVi, String MaDonVi, String MaCotMau_Chuan, DateTime ThoiGian, int nSoThangTruoc, int nSoThangSau)
        {
            DataTable dt = new DataTable();
            int i, j, h, c;
            //String MaBang;
            DateTime dtg;
            DataRow Row;

            dt.Columns.Add("ThoiGian", typeof(String));
            dt.Columns.Add("GiaTri", typeof(String));
            dt.Columns.Add("GiaTri_Chuan", typeof(String));
            
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@TuThoiGian", ThoiGian.AddMonths(-nSoThangTruoc).ToString("yyyy/MM"));
            cmd.Parameters.AddWithValue("@DenThoiGian", ThoiGian.AddMonths(nSoThangSau).ToString("yyyy/MM"));
            cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            cmd.Parameters.AddWithValue("@iID_MaCotMau_DonVi", MaCotMau_DonVi);
            cmd.Parameters.AddWithValue("@iID_MaDonVi", MaDonVi);
            cmd.Parameters.AddWithValue("@iID_MaCotMau_Chuan", MaCotMau_Chuan);
            cmd.CommandText = "SELECT * " +
                              " FROM BC_Bang_ChiTiet " +
                              " WHERE (iID_MaHangMau=@iID_MaHangMau) AND " +
                                     "(iID_MaBangMau=@iID_MaBangMau) AND " +
                                     "(CONVERT(varchar(7),dNgayBaoCao,111)>=@TuThoiGian) AND "+
                                     "(CONVERT(varchar(7),dNgayBaoCao,111)<=@DenThoiGian) AND " +
                                      "(" +
                                        "(iID_MaCotMau=@iID_MaCotMau_DonVi AND iID_MaCot_MaDonVi=@iID_MaDonVi) OR " +
                                        "(iID_MaCotMau=@iID_MaCotMau_Chuan)" +
                                       ")";
            DataTable dtChiTiet = Connection.GetDataTable(cmd);
            cmd.Dispose();
            for (j = -nSoThangTruoc; j < nSoThangSau; j++)
            {
                dtg = ThoiGian.AddMonths(j);
                Row = dt.NewRow();
                dt.Rows.Add(Row);
                Row["ThoiGian"] = String.Format("T{0}", dtg.ToString("MM_yy"));                
                dtg = ThoiGian.AddMonths(j);
                for (i = 0; i < dtChiTiet.Rows.Count; i++)
                {
                    DateTime dtg1 = Convert.ToDateTime(dtChiTiet.Rows[i]["dNgayBaoCao"]);
                    if (dtg.ToString("yyyy/MM") == dtg1.ToString("yyyy/MM"))
                    {
                        if (Convert.ToString(dtChiTiet.Rows[i]["iID_MaCotMau"]) == MaCotMau_DonVi)
                        {
                            Row["GiaTri"] = dtChiTiet.Rows[i]["oGiaTri"];
                        }
                        else
                        {
                            Row["GiaTri_Chuan"] = dtChiTiet.Rows[i]["oGiaTri"];
                        }
                    }
                }
            }
            dtChiTiet.Dispose();
            return dt;
        }

        public static void CreateYAxis(Chart chart, ChartArea area, Series series, float axisOffset, float labelsSize, Color intCorlor)
        {
            // Create new chart area for original series
            ChartArea areaSeries = chart.ChartAreas.Add("ChartArea_" + series.Name);
            areaSeries.BackColor = Color.Transparent;
            areaSeries.BorderColor = Color.Transparent;
            areaSeries.Position.FromRectangleF(area.Position.ToRectangleF());
            areaSeries.InnerPlotPosition.FromRectangleF(area.InnerPlotPosition.ToRectangleF());
            areaSeries.AxisX.MajorGrid.Enabled = false;
            areaSeries.AxisX.MajorTickMark.Enabled = false;
            areaSeries.AxisX.LabelStyle.Enabled = false;
            areaSeries.AxisY.MajorGrid.Enabled = false;
            areaSeries.AxisY.MajorTickMark.Enabled = false;
            areaSeries.AxisY.LabelStyle.Enabled = false;
            areaSeries.AxisY.IsStartedFromZero = area.AxisY.IsStartedFromZero;


            series.ChartArea = areaSeries.Name;

            // Create new chart area for axis
            ChartArea areaAxis = chart.ChartAreas.Add("AxisY_" + series.ChartArea);
            areaAxis.BackColor = Color.Transparent;
            areaAxis.BorderColor = Color.Transparent;
            areaAxis.AxisY.LineColor = intCorlor;
            areaAxis.AxisY.LineWidth = 3;
            areaAxis.Position.FromRectangleF(chart.ChartAreas[series.ChartArea].Position.ToRectangleF());
            areaAxis.InnerPlotPosition.FromRectangleF(chart.ChartAreas[series.ChartArea].InnerPlotPosition.ToRectangleF());

            // Create a copy of specified series
            Series seriesCopy = chart.Series.Add(series.Name + "_Copy");
            seriesCopy.ChartType = series.ChartType;
            foreach (DataPoint point in series.Points)
            {
                seriesCopy.Points.AddXY(point.XValue, point.YValues[0]);
            }

            // Hide copied series
            seriesCopy.IsVisibleInLegend = false;
            seriesCopy.Color = Color.Transparent;
            seriesCopy.BorderColor = Color.Transparent;
            seriesCopy.ChartArea = areaAxis.Name;

            // Disable drid lines & tickmarks
            areaAxis.AxisX.LineWidth = 0;
            areaAxis.AxisX.MajorGrid.Enabled = false;
            areaAxis.AxisX.MajorTickMark.Enabled = false;
            areaAxis.AxisX.LabelStyle.Enabled = false;
            areaAxis.AxisY.MajorGrid.Enabled = false;
            areaAxis.AxisY.IsStartedFromZero = area.AxisY.IsStartedFromZero;
            areaAxis.AxisY.LabelStyle.Font = area.AxisY.LabelStyle.Font;

            // Adjust area position
            areaAxis.Position.X -= axisOffset;
            areaAxis.InnerPlotPosition.X += labelsSize;
        }
    }
}