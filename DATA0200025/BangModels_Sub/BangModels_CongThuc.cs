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


namespace DATA0200025
{
    public class BangModels_CongThuc
    {
        public static String DienHangSoVaoCongThuc(String CongThuc, DataTable dtBang)
        {
            DateTime NgayCuaBang = Convert.ToDateTime(dtBang.Rows[0]["dNgayBaoCao"]);
            DateTime d1 = new DateTime(NgayCuaBang.Year, NgayCuaBang.Month, 1);
            DateTime d2 = NgayCuaBang.AddDays(31);
            d2 = new DateTime(d2.Year, d2.Month, 1);
            int SoNgayTrongThangBaoCao = Convert.ToInt16((d1 - d1).TotalDays);
            int Ngay = NgayCuaBang.Day;
            int Thang = NgayCuaBang.Month;
            int Nam = NgayCuaBang.Year;
            //date
            CongThuc = CongThuc.Replace("[SoNgayTrongThangBaoCao]", SoNgayTrongThangBaoCao.ToString());
            CongThuc = CongThuc.Replace("[NgayCuaBaoCao]", Ngay.ToString());
            CongThuc = CongThuc.Replace("[ThangCuaBaoCao]", Thang.ToString());
            CongThuc = CongThuc.Replace("[NamCuaBaoCao]", Nam.ToString());
            return CongThuc;
        }

        public static string LayXauCongThuc(String MaBang)
        {
            StringBuilder builder = new StringBuilder();
            String MaO, MaHang, MaCot, strCongThuc, vR = "", MaHang1, MaCot1;
            int i, j, csH, csC, h, c;
            DataTable dtHang, dtCot, dtCongThuc;
            List<Boolean> arrCotHienThi;
            SqlCommand cmd;

            cmd = new SqlCommand("SELECT * FROM BC_Bang WHERE iID_MaBang=@iID_MaBang");
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            DataTable dtBang = Connection.GetDataTable(cmd);
            cmd.Dispose();
            String MaBangMau = Convert.ToString(dtBang.Rows[0]["iID_MaBangMau"]);
            cmd = new SqlCommand("SELECT * FROM BC_Bang_CongThuc WHERE iID_MaBang=@iID_MaBang");
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            dtCongThuc = Connection.GetDataTable(cmd);
            cmd.Dispose();

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * " +
                              "FROM BC_Bang_Cot " +
                              "WHERE iID_MaBang=@iID_MaBang ORDER BY iSTT;";
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            dtCot = Connection.GetDataTable(cmd);
            cmd.Dispose();

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * " +
                              "FROM BC_BangMau_CotMau_DonVi " +
                              "WHERE iID_MaBangMau=@iID_MaBangMau;";
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            DataTable dtCotMauDonVi = Connection.GetDataTable(cmd);
            cmd.Dispose();

            cmd = new SqlCommand("SELECT * FROM BC_Bang_Hang WHERE iID_MaBang=@iID_MaBang AND iMaTrangThai=1 ORDER BY iSTT");
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            dtHang = Connection.GetDataTable(cmd);
            cmd.Dispose();


            //Xác định các cột hiển thị hay không?
            arrCotHienThi = new List<Boolean>();
            for (i = 0; i <= dtCot.Rows.Count - 1; i++)
            {
                arrCotHienThi.Add(Convert.ToBoolean(dtCot.Rows[i]["bVisible"]));
                if (arrCotHienThi[i] && Convert.ToBoolean(dtCot.Rows[i]["bThuocDonVi"]))
                {
                    for (j = 0; j < dtCotMauDonVi.Rows.Count; j++)
                    {
                        if (Convert.ToString(dtCot.Rows[i]["iID_MaCotMau"]) == Convert.ToString(dtCotMauDonVi.Rows[j]["iID_MaCotMau"]) &&
                            Convert.ToString(dtCot.Rows[i]["iID_MaDonVi"]) == Convert.ToString(dtCotMauDonVi.Rows[j]["iID_MaDonVi"]))
                        {
                            arrCotHienThi[i] = false;
                            break;
                        }
                    }
                }
            }

            List<List<String>> arrGoiHam = new List<List<String>>();
            List<List<String>> arrCT = new List<List<String>>();
            for (i = 0; i < dtHang.Rows.Count; i++)
            {
                arrGoiHam.Add(new List<string>());
                arrCT.Add(new List<string>());
                for (j = 0; j < dtCot.Rows.Count; j++)
                {
                    arrGoiHam[i].Add("");
                    arrCT[i].Add("");
                }
            }

            String strThem = "";

            //Sap xep cong thuc
            List<String> arrCongThuc = new List<String>();
            List<String> arrMaHang = new List<String>();
            List<String> arrMaCot = new List<String>();
            List<int> arrLoaiCongThuc = new List<int>();
            int d;

            for (i = 0; i < dtCongThuc.Rows.Count; i++)
            {
                arrLoaiCongThuc.Add(0);
                arrCongThuc.Add(Convert.ToString(dtCongThuc.Rows[i]["sCongThuc"]));
                arrMaHang.Add(Convert.ToString(dtCongThuc.Rows[i]["iID_MaHang"]));
                arrMaCot.Add(Convert.ToString(dtCongThuc.Rows[i]["iID_MaCot"]));
            }
            for (c = 0; c < dtCot.Rows.Count; c++)
            {
                strCongThuc = Convert.ToString(dtCot.Rows[c]["sCongThuc"]);
                if (String.IsNullOrEmpty(strCongThuc) == false)
                {
                    arrCongThuc.Add(Convert.ToString(dtCot.Rows[c]["sCongThuc"]));
                    arrLoaiCongThuc.Add(1);
                    arrMaHang.Add("");
                    arrMaCot.Add(Convert.ToString(dtCot.Rows[c]["iID_MaCot"]));
                }
            }
            for (h = 0; h < dtHang.Rows.Count; h++)
            {
                strCongThuc = Convert.ToString(dtHang.Rows[h]["sCongThuc"]);
                if (String.IsNullOrEmpty(strCongThuc) == false)
                {
                    arrCongThuc.Add(Convert.ToString(dtHang.Rows[h]["sCongThuc"]));
                    arrLoaiCongThuc.Add(2);
                    arrMaHang.Add(Convert.ToString(dtHang.Rows[h]["iID_MaHang"]));
                    arrMaCot.Add("");
                }
            }
            List<int> arrCS = SapXepCongThuc(arrCongThuc, arrLoaiCongThuc, arrMaHang, arrMaCot);

            //Tao ham
            String strKhaiBao;
            for (h = 0; h < dtHang.Rows.Count; h++)
            {
                MaHang = Convert.ToString(dtHang.Rows[h]["iID_MaHang"]);
                for (c = 0; c < dtCot.Rows.Count; c++)
                {
                    MaCot = Convert.ToString(dtCot.Rows[c]["iID_MaCot"]);
                    if (arrCotHienThi[c])
                    {
                        strCongThuc = "";
                        for (i = 0; i < dtCongThuc.Rows.Count; i++)
                        {
                            if (MaHang == Convert.ToString(dtCongThuc.Rows[i]["iID_MaHang"]) &&
                                MaCot == Convert.ToString(dtCongThuc.Rows[i]["iID_MaCot"]))
                            {
                                strCongThuc = Convert.ToString(dtCongThuc.Rows[i]["sCongThuc"]);
                                if (strCongThuc.StartsWith("=MAX(") || strCongThuc.StartsWith("=MIN(") ||
                                    strCongThuc.StartsWith("=LIETKE(") || strCongThuc.StartsWith("=VITRIHANG"))
                                {
                                    strCongThuc = "  ";
                                }
                                break;
                            }
                        }
                        if (String.IsNullOrEmpty(strCongThuc) && Convert.ToBoolean(dtCot.Rows[c]["bKhongApDungCongThucHang"]) == false)
                        {
                            strCongThuc = Convert.ToString(dtHang.Rows[h]["sCongThuc"]);
                            if (String.IsNullOrEmpty(strCongThuc) == false)
                            {
                                strCongThuc = strCongThuc.Replace("#", MaCot);
                            }
                        }
                        if (String.IsNullOrEmpty(strCongThuc))
                        {
                            strCongThuc = Convert.ToString(dtCot.Rows[c]["sCongThuc"]);
                            if (String.IsNullOrEmpty(strCongThuc) == false)
                            {
                                strCongThuc = strCongThuc.Replace("#", MaHang);
                            }
                        }
                        strCongThuc = strCongThuc.Trim();
                        if (String.IsNullOrEmpty(strCongThuc) == false)
                        {
                            if (strCongThuc.StartsWith("="))
                            {
                                strCongThuc = strCongThuc.Substring(1);
                            }
                            strKhaiBao = "";
                            d = 0;
                            while (strCongThuc.IndexOf("[") >= 0)
                            {
                                d = d + 1;
                                int cs1 = strCongThuc.IndexOf("[");
                                int cs2 = strCongThuc.IndexOf("]");
                                MaO = strCongThuc.Substring(cs1 + 1, cs2 - cs1 - 1);
                                String[] arrTg = MaO.Split('_');
                                MaHang1 = arrTg[0];
                                MaCot1 = arrTg[1];
                                csH = BangModels_HamChung.LayCSH(dtHang, MaHang1);
                                csC = BangModels_HamChung.LayCSC(dtCot, MaCot1);
                                if (csH >= 0 && csC >= 0)
                                {
                                    strKhaiBao += String.Format("var v{0}= LayGiaTriO('{1}','{2}');{3}", d, MaHang1, MaCot1, strThem);
                                    strCongThuc = strCongThuc.Substring(0, cs1) +
                                                  String.Format("v{0}", d) +
                                                  strCongThuc.Substring(cs2 + 1);
                                    if (csH >= 0 && csC >= 0)
                                    {
                                        arrGoiHam[csH][csC] += String.Format("GH{0}_{1}();{2}", MaHang, MaCot, strThem);
                                    }
                                }
                                else
                                {
                                    String strMaOBangKhac = MaO;
                                    if (Convert.ToBoolean(dtCot.Rows[c]["bThuocDonVi"]))
                                    {
                                        strMaOBangKhac += String.Format("_{0}", dtCot.Rows[c]["iID_MaDonVi"]);
                                    }
                                    strKhaiBao += String.Format("var v{0}= Bang_GTBangKhac('{1}');", d, strMaOBangKhac);
                                    strCongThuc = strCongThuc.Substring(0, cs1) +
                                                  String.Format("v{0}", d) +
                                                  strCongThuc.Substring(cs2 + 1);
                                }

                            }

                            strCongThuc = String.Format("GanGiaTriThatChoO('{0}','{1}',{2});{3}", MaHang, MaCot, strCongThuc, strThem);

                            String strCTTG = "", strCacODaLam = "", MaO1, strCacHamGoiThem = "";
                            MaO = String.Format("[{0}_{1}]", MaHang, MaCot);
                            for (j = arrCongThuc.Count - 1; j >= 0; j--)
                            {
                                strCTTG = arrCongThuc[j];
                                MaHang1 = arrMaHang[j];
                                MaCot1 = arrMaCot[j];
                                if (arrLoaiCongThuc[j] == 1)
                                {
                                    strCTTG = strCTTG.Replace("#", MaHang);
                                    MaHang1 = MaHang;
                                }
                                if (arrLoaiCongThuc[j] == 2)
                                {
                                    strCTTG = strCTTG.Replace("#", MaCot);
                                    MaCot1 = MaCot;
                                }
                                MaO1 = String.Format("[{0}_{1}]", MaHang1, MaCot1);
                                if (strCTTG.IndexOf(MaO) >= 0 && strCacODaLam.IndexOf(MaO1) < 0)
                                {
                                    strCacHamGoiThem = String.Format("GH{0}_{1}();{2}", MaHang1, MaCot1, strThem) + strCacHamGoiThem;
                                    strCacODaLam += MaO1;
                                }
                            }
                            //vR += String.Format("function CT{0}_{1}(){2}", MaHang, MaCot, strThem);
                            //vR += "{";
                            //vR += strKhaiBao;
                            //vR += strCongThuc;
                            //vR += strCacHamGoiThem;
                            //vR += "}";

                            builder.Append(String.Format("function CT{0}_{1}(){2}", MaHang, MaCot, strThem));
                            builder.Append("{");
                            builder.Append(strKhaiBao);
                            builder.Append(strCongThuc);
                            builder.Append(strCacHamGoiThem);
                            builder.Append("}");


                            arrCT[h][c] += String.Format("CT{0}_{1}();{2}", MaHang, MaCot, strThem);
                        }
                    }
                }
            }

            for (i = 0; i < dtHang.Rows.Count; i++)
            {
                for (j = 0; j < dtCot.Rows.Count; j++)
                {
                    if (arrGoiHam[i][j] != "" || arrCT[i][j] != "")
                    {
                        //vR += String.Format("function GH{0}_{1}()", dtHang.Rows[i]["iID_MaHang"], dtCot.Rows[j]["iID_MaCot"]);
                        //vR += "{";
                        //if (arrCT[i][j] != "") vR += arrCT[i][j];
                        //if (arrGoiHam[i][j] != "") vR += arrGoiHam[i][j];
                        //vR += "}";

                        builder.Append(String.Format("function GH{0}_{1}()", dtHang.Rows[i]["iID_MaHang"], dtCot.Rows[j]["iID_MaCot"]));
                        builder.Append("{");
                        if (arrCT[i][j] != "") builder.Append(arrCT[i][j]);
                        if (arrGoiHam[i][j] != "") builder.Append(arrGoiHam[i][j]);
                        builder.Append("}");
                    }
                }
            }

            //return vR;
            return builder.ToString();
        }

        public static string ChinhLaiCongThuc(String CongThuc, int SoTruong)
        {
            int cs = CongThuc.IndexOf("[Last");
            int i, d, kc;
            String strCotCu, strCotMoi, tg;
            while (cs >= 0)
            {
                i = cs + 5;
                tg = "";
                kc = 0;
                if (CongThuc[cs + 5] == '-')
                {
                    d = 0;
                    tg = "-";
                    for (i = cs + 6; i < CongThuc.Length; i++)
                    {
                        if (CongThuc[i] == ']')
                        {
                            break;
                        }
                        d = d + 1;
                        tg += String.Format("{0}", CongThuc[i]);
                    }
                    kc = Convert.ToInt16(tg);
                }
                strCotCu = String.Format("[Last{0}]", tg);
                strCotMoi = String.Format("[{0}]", SoTruong + kc);
                CongThuc = CongThuc.Replace(strCotCu, strCotMoi);
                cs = CongThuc.IndexOf("[Last");
            }
            return CongThuc;
        }

        public static String TaoCongThucChoBangMoi(String MaBangMau, String MaBang, String sUserName, String sIP)
        {
            SqlCommand cmd;

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM BC_BangMau_CongThuc WHERE iID_MaBangMau = @iID_MaBangMau";
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            DataTable dtCongThuc = Connection.GetDataTable(cmd);
            cmd.Dispose();

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT BMHM.*,HM.* FROM BC_BangMau_HangMau AS BMHM INNER JOIN BC_HangMau AS HM ON BMHM.iID_MaHangMau=HM.iID_MaHangMau  WHERE iID_MaBangMau = @iID_MaBangMau";
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            DataTable dtHangMau = Connection.GetDataTable(cmd);
            cmd.Dispose();

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM BC_BangMau_CotMau WHERE iID_MaBangMau = @iID_MaBangMau";
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            DataTable dtCotMau = Connection.GetDataTable(cmd);
            cmd.Dispose();

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM BC_Bang WHERE iID_MaBang = @iID_MaBang";
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            DataTable dtBang = Connection.GetDataTable(cmd);
            cmd.Dispose();

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM BC_Bang_Hang WHERE iID_MaBang = @iID_MaBang";
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            DataTable dtHang = Connection.GetDataTable(cmd);
            cmd.Dispose();

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM BC_Bang_Cot WHERE iID_MaBang = @iID_MaBang";
            cmd.Parameters.AddWithValue("@iID_MaBang", MaBang);
            DataTable dtCot = Connection.GetDataTable(cmd);
            cmd.Dispose();

            int i, j, k, vt, cs;
            String csOCu, csOMoi, CongThuc, MaHang, MaHangMau, MaCotMau, MaCotMau_T, MaCotMau_N, Dau;

            //Sửa lại công thức của các hàng

            Boolean ok, ok1;
            for (i = 0; i < dtHang.Rows.Count; i++)
            {
                CongThuc = "";
                String sTenHangHT = Convert.ToString(dtHang.Rows[i]["sTenHang"]);
                if (Convert.ToBoolean(dtHang.Rows[i]["bTongHangCon"]))
                {
                    Dau = "+";
                    for (j = 0; j < dtHang.Rows.Count; j++)
                    {
                        if (Convert.ToString(dtHang.Rows[i]["iID_MaHangMau"]) == Convert.ToString(dtHang.Rows[j]["iID_MaHangMau_Cha"]))
                        {
                            if (CongThuc != "") CongThuc += Dau;
                            CongThuc += String.Format("[{0}_#]", dtHang.Rows[j]["iID_MaHang"]);
                        }
                    }
                }
                else
                {
                    CongThuc = Convert.ToString(dtHang.Rows[i]["sCongThuc"]);
                    if (String.IsNullOrEmpty(CongThuc) == false)
                    {
                        for (j = 0; j < dtHangMau.Rows.Count; j++)
                        {
                            if (Convert.ToString(dtHang.Rows[i]["iID_MaHangMau"]) == Convert.ToString(dtHangMau.Rows[j]["iID_MaHangMau"]))
                            {
                                CongThuc = Convert.ToString(dtHangMau.Rows[j]["sCongThuc"]);
                                break;
                            }
                        }
                        cs = 0;
                        for (j = CongThuc.Length - 1; j >= 0; j--)
                        {
                            if (CongThuc[j] == ']')
                            {
                                cs = j;
                            }
                            if (CongThuc[j] == '[')
                            {
                                MaHangMau = CongThuc.Substring(j + 1, cs - j - 1);
                                ok = true;
                                for (k = 0; k < dtHang.Rows.Count; k++)
                                {
                                    if (MaHangMau == Convert.ToString(dtHang.Rows[k]["iID_MaHangMau"]))
                                    {
                                        MaHang = String.Format("[{0}_#]", dtHang.Rows[k]["iID_MaHang"]);
                                        CongThuc = CongThuc.Substring(0, j) +
                                                   MaHang +
                                                   CongThuc.Substring(cs + 1);
                                        ok = false;
                                        break;
                                    }
                                }
                                if (ok)
                                {
                                    CongThuc = CongThuc.Substring(0, j) +
                                                   "0" +
                                                   CongThuc.Substring(cs + 1);
                                }
                            }
                        }

                    }
                }
                if (CongThuc != "")
                {
                    CongThuc = DienHangSoVaoCongThuc(CongThuc, dtBang);
                    if (CongThuc.StartsWith("=") == false)
                    {
                        CongThuc = "=" + CongThuc;
                    }
                    cmd = new SqlCommand("UPDATE BC_Bang_Hang SET sCongThuc=@sCongThuc WHERE iID_MaHang=@iID_MaHang");
                    cmd.Parameters.AddWithValue("@iID_MaHang", dtHang.Rows[i]["iID_MaHang"]);
                    cmd.Parameters.AddWithValue("@sCongThuc", CongThuc);
                    Connection.UpdateDatabase(cmd);
                }
            }

            //Sửa lại công thức của các cột
            for (i = 0; i < dtCot.Rows.Count; i++)
            {
                CongThuc = Convert.ToString(dtCot.Rows[i]["sCongThuc"]);
                if (String.IsNullOrEmpty(CongThuc) == false)
                {
                    for (j = 0; j < dtCotMau.Rows.Count; j++)
                    {
                        if (Convert.ToString(dtCot.Rows[i]["iID_MaCotMau"]) == Convert.ToString(dtCotMau.Rows[j]["iID_MaCotMau"]))
                        {
                            CongThuc = Convert.ToString(dtCotMau.Rows[j]["sCongThuc"]);
                            break;
                        }
                    }
                    ok1 = true;
                    for (j = 0; ok1 && j < dtCot.Rows.Count; j++)
                    {
                        MaCotMau = String.Format("[{0}]", dtCot.Rows[j]["iID_MaCotMau"]);
                        MaCotMau_T = String.Format("[{0}_T]", dtCot.Rows[j]["iID_MaCotMau"]);
                        MaCotMau_N = String.Format("[{0}_N]", dtCot.Rows[j]["iID_MaCotMau"]);
                        ok = true;
                        if (Convert.ToBoolean(dtCot.Rows[i]["bThuocDonVi"]) && Convert.ToBoolean(dtCot.Rows[j]["bThuocDonVi"]))
                        {
                            ok = Convert.ToString(dtCot.Rows[i]["iID_MaDonVi"]) == Convert.ToString(dtCot.Rows[j]["iID_MaDonVi"]);
                        }
                        if (ok && (CongThuc.IndexOf(MaCotMau) >= 0 || CongThuc.IndexOf(MaCotMau_N) >= 0 || CongThuc.IndexOf(MaCotMau_T) >= 0))
                        {
                            vt = CongThuc.IndexOf(MaCotMau);
                            if (vt == -1)
                            {
                                vt = CongThuc.IndexOf(MaCotMau_N);
                            }
                            if (vt == -1)
                            {
                                vt = CongThuc.IndexOf(MaCotMau_T);
                            }
                            if (vt >= 0)
                            {
                                csOMoi = "";
                                if (Convert.ToBoolean(dtCot.Rows[i]["bThuocDonVi"]))
                                {
                                    csOMoi = string.Format("[#_{0}]", dtCot.Rows[j]["iID_MaCot"]);
                                    if (CongThuc.IndexOf(MaCotMau) >= 0)
                                        csOMoi = string.Format("[#_{0}]", dtCot.Rows[j]["iID_MaCot"]);
                                    else if (CongThuc.IndexOf(MaCotMau_N) >= 0)
                                        csOMoi = string.Format("[#_{0}_N]", dtCot.Rows[j]["iID_MaCot"]);
                                    else if (CongThuc.IndexOf(MaCotMau_T) >= 0)
                                        csOMoi = string.Format("[#_{0}_T]", dtCot.Rows[j]["iID_MaCot"]);

                                    Boolean CoChonThemCot = false;
                                    Dau = "+";
                                    while (CongThuc.Length > vt && CongThuc[vt] != ']')
                                    {
                                        vt++;
                                    }
                                    if (CongThuc.Length > vt + 1 && CongThuc[vt + 1] != ')')
                                    {
                                        Dau = CongThuc[vt + 1].ToString();
                                    }
                                    for (k = 0; k < dtCot.Rows.Count; k++)
                                    {
                                        if (k != j && Convert.ToString(dtCot.Rows[j]["iID_MaCotMau"]) == Convert.ToString(dtCot.Rows[k]["iID_MaCotMau"]) &&
                                            Convert.ToString(dtCot.Rows[j]["iID_MaDonVi"]) == Convert.ToString(dtCot.Rows[k]["iID_MaDonVi"]))
                                        {
                                            if (csOMoi != "") csOMoi += Dau;
                                            if (CongThuc.IndexOf(MaCotMau) >= 0)
                                                csOMoi += string.Format("[#_{0}]", dtCot.Rows[k]["iID_MaCot"]);
                                            else if (CongThuc.IndexOf(MaCotMau_N) >= 0)
                                                csOMoi += string.Format("[#_{0}_N]", dtCot.Rows[k]["iID_MaCot"]);
                                            else if (CongThuc.IndexOf(MaCotMau_T) >= 0)
                                                csOMoi += string.Format("[#_{0}_T]", dtCot.Rows[k]["iID_MaCot"]);
                                            CoChonThemCot = true;
                                        }
                                    }
                                    if (CoChonThemCot)
                                    {
                                        csOMoi = "(" + csOMoi + ")";
                                    }
                                }
                                else
                                {
                                    Dau = "+";
                                    while (CongThuc.Length > vt && CongThuc[vt] != ']')
                                    {
                                        vt++;
                                    }
                                    if (CongThuc.Length > vt + 1 && CongThuc[vt + 1] != ')')
                                    {
                                        Dau = CongThuc[vt + 1].ToString();
                                    }
                                    for (k = 0; k < dtCot.Rows.Count; k++)
                                    {
                                        if (Convert.ToString(dtCot.Rows[j]["iID_MaCotMau"]) == Convert.ToString(dtCot.Rows[k]["iID_MaCotMau"]))
                                        {
                                            if (csOMoi != "") csOMoi += Dau;
                                            if (CongThuc.IndexOf(MaCotMau) >= 0)
                                                csOMoi += string.Format("[#_{0}]", dtCot.Rows[k]["iID_MaCot"]);
                                            else if (CongThuc.IndexOf(MaCotMau_N) >= 0)
                                                csOMoi += string.Format("[#_{0}_N]", dtCot.Rows[k]["iID_MaCot"]);
                                            else if (CongThuc.IndexOf(MaCotMau_T) >= 0)
                                                csOMoi += string.Format("[#_{0}_T]", dtCot.Rows[k]["iID_MaCot"]);
                                        }
                                    }
                                    csOMoi = "(" + csOMoi + ")";
                                }
                                if (CongThuc.IndexOf(MaCotMau) >= 0)
                                    CongThuc = CongThuc.Replace(MaCotMau, csOMoi);
                                else if (CongThuc.IndexOf(MaCotMau_N) >= 0)
                                    CongThuc = CongThuc.Replace(MaCotMau_N, csOMoi);
                                else if (CongThuc.IndexOf(MaCotMau_T) >= 0)
                                    CongThuc = CongThuc.Replace(MaCotMau_T, csOMoi);
                                if (CongThuc.IndexOf("[") < 0)
                                {
                                    ok1 = false;
                                }
                            }
                        }
                    }
                    if (CongThuc != "")
                    {
                        CongThuc = DienHangSoVaoCongThuc(CongThuc, dtBang);
                        if (CongThuc.StartsWith("=") == false)
                        {
                            CongThuc = "=" + CongThuc;
                        }
                        cmd = new SqlCommand("UPDATE BC_Bang_Cot SET sCongThuc=@sCongThuc WHERE iID_MaCot=@iID_MaCot");
                        cmd.Parameters.AddWithValue("@iID_MaCot", dtCot.Rows[i]["iID_MaCot"]);
                        cmd.Parameters.AddWithValue("@sCongThuc", CongThuc);
                        Connection.UpdateDatabase(cmd);
                    }
                }
            }

            //Sửa lại công thức của ô
            int i1, i2, i3, i4, i5, i6;
            for (i1 = 0; i1 < dtCongThuc.Rows.Count; i1++)
            {
                CongThuc = Convert.ToString(dtCongThuc.Rows[i1]["sCongThuc"]).Trim();
                if (String.IsNullOrEmpty(CongThuc) == false)
                {
                    for (i2 = 0; i2 < dtHang.Rows.Count; i2++)
                    {
                        if (Convert.ToString(dtCongThuc.Rows[i1]["iID_MaHangMau"]) == Convert.ToString(dtHang.Rows[i2]["iID_MaHangMau"]))
                        {
                            for (i3 = 0; i3 < dtCot.Rows.Count; i3++)
                            {
                                if (Convert.ToString(dtCongThuc.Rows[i1]["iID_MaCotMau"]) == Convert.ToString(dtCot.Rows[i3]["iID_MaCotMau"]))
                                {
                                    CongThuc = Convert.ToString(dtCongThuc.Rows[i1]["sCongThuc"]).Trim();
                                    ok1 = true;
                                    for (i5 = 0; ok1 && i5 < dtHang.Rows.Count; i5++)
                                    {
                                        for (i6 = 0; ok1 && i6 < dtCot.Rows.Count; i6++)
                                        {
                                            ok = true;
                                            if (Convert.ToBoolean(dtCot.Rows[i3]["bThuocDonVi"]))
                                            {
                                                ok = Convert.ToString(dtCot.Rows[i3]["iID_MaDonVi"]) == Convert.ToString(dtCot.Rows[i6]["iID_MaDonVi"]);
                                            }
                                            if (ok)
                                            {
                                                csOCu = string.Format("[H{0}_C{1}]", dtHang.Rows[i5]["iID_MaHangMau"], dtCot.Rows[i6]["iID_MaCotMau"]);

                                                if (CongThuc.IndexOf(csOCu) >= 0)
                                                {
                                                    if (Convert.ToBoolean(dtCot.Rows[i3]["bThuocDonVi"]))
                                                    {
                                                        csOMoi = string.Format("[{0}_{1}]", dtHang.Rows[i5]["iID_MaHang"], dtCot.Rows[i6]["iID_MaCot"]);
                                                    }
                                                    else
                                                    {
                                                        Boolean okCoDauSauO = false;
                                                        csOMoi = "";
                                                        Dau = "+";
                                                        vt = CongThuc.IndexOf(csOCu);
                                                        while (CongThuc.Length > vt && CongThuc[vt] != ']')
                                                        {
                                                            vt++;
                                                        }
                                                        if (CongThuc.Length > vt + 1 && CongThuc[vt + 1] != ')')
                                                        {
                                                            Dau = CongThuc[vt + 1].ToString();
                                                            okCoDauSauO = true;
                                                        }
                                                        int dSoCotMau = 0;
                                                        for (i4 = 0; i4 < dtCot.Rows.Count; i4++)
                                                        {
                                                            if (Convert.ToString(dtCot.Rows[i6]["iID_MaCotMau"]) == Convert.ToString(dtCot.Rows[i4]["iID_MaCotMau"]))
                                                            {
                                                                dSoCotMau++;
                                                                if (csOMoi != "") csOMoi += Dau;
                                                                csOMoi += string.Format("[{0}_{1}]", dtHang.Rows[i5]["iID_MaHang"], dtCot.Rows[i4]["iID_MaCot"]);
                                                            }
                                                        }
                                                        if (dSoCotMau > 1 && okCoDauSauO)
                                                        {
                                                            csOCu += Dau;
                                                        }
                                                        //csOMoi = "(" + csOMoi + ")";
                                                    }

                                                    CongThuc = CongThuc.Replace(csOCu, csOMoi);
                                                    if (CongThuc.IndexOf("[") < 0)
                                                    {
                                                        ok1 = false;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    while (CongThuc.IndexOf("[H") >= 0)
                                    {
                                        int cs1 = CongThuc.IndexOf("[H");
                                        int cs2 = cs1 + 1;
                                        while (cs2 < CongThuc.Length && CongThuc[cs2] != ']')
                                        {
                                            cs2++;
                                        }
                                        if (cs2 < CongThuc.Length)
                                        {
                                            CongThuc = CongThuc.Substring(0, cs1) + "0" + CongThuc.Substring(cs2 + 1);
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    CongThuc = CongThuc.Trim();
                                    if (CongThuc != "")
                                    {
                                        CongThuc = DienHangSoVaoCongThuc(CongThuc, dtBang);
                                        if (CongThuc.StartsWith("=") == false)
                                        {
                                            CongThuc = "=" + CongThuc;
                                        }
                                        Bang bang = new Bang("BC_Bang_CongThuc");
                                        bang.MaNguoiDungSua = sUserName;
                                        bang.IPSua = sIP;
                                        bang.DuLieuMoi = true;
                                        bang.CmdParams.Parameters.AddWithValue("@iID_MaBang", MaBang);
                                        bang.CmdParams.Parameters.AddWithValue("@iID_MaHang", dtHang.Rows[i2]["iID_MaHang"]);
                                        bang.CmdParams.Parameters.AddWithValue("@iID_MaCot", dtCot.Rows[i3]["iID_MaCot"]);
                                        bang.CmdParams.Parameters.AddWithValue("@sCongThuc", CongThuc);
                                        bang.Save();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            dtCongThuc.Dispose();
            dtHang.Dispose();
            dtCot.Dispose();
            return MaBang;
        }

        public static List<int> SapXepCongThuc(List<String> arrCongThuc, List<int> arrLoaiCongThuc, List<String> arrMaHang, List<String> arrMaCot)
        {
            String MaO;
            int i, j, k;

            //Sap xep lai cong thuc theo thu tu goi
            List<Boolean> arrTT = new List<Boolean>();
            List<int> arrCS = new List<int>();
            for (i = 0; i < arrCongThuc.Count; i++)
            {
                arrTT.Add(true);
                arrCS.Add(i);
            }

            Boolean ok;

            for (i = 0; i < arrCongThuc.Count; i++)
            {
                for (j = 0; j < arrCongThuc.Count; j++)
                {
                    if (arrTT[j])
                    {
                        ok = true;
                        for (k = 0; k < arrCongThuc.Count; k++)
                        {
                            if (arrTT[k] && k != j)
                            {
                                MaO = String.Format("[{0}_{1}]", arrMaHang[k], arrMaCot[k]);
                                if (arrLoaiCongThuc[j] == 1 || arrLoaiCongThuc[k] == 1)
                                {
                                    MaO = String.Format("[{0}_", arrMaHang[k]);
                                }
                                if (arrLoaiCongThuc[j] == 2 || arrLoaiCongThuc[k] == 2)
                                {
                                    MaO = String.Format("_{0}]", arrMaCot[k]);
                                }
                                if (arrCongThuc[j].IndexOf(MaO) >= 0)
                                {
                                    ok = false;
                                }
                                if (ok == false)
                                {
                                    break;
                                }
                            }
                        }
                        if (ok)
                        {
                            arrTT[j] = false;
                            arrCS[i] = j;
                            break;
                        }
                    }
                }
            }
            return arrCS;
        }

        public static String ChuyenCongThucSangExcel(String CongThuc)
        {
            while (CongThuc.IndexOf("?") >= 0)
            {
                int i = CongThuc.IndexOf("?");
                int j1 = i - 1;
                int d = 0;
                while (j1 >= 0 && CongThuc[j1] != ')') j1--;
                while (j1 >= 0)
                {
                    if (CongThuc[j1] == ')') d = d - 1;
                    if (CongThuc[j1] == '(') d = d + 1;
                    if (d == 0) break;
                    j1 = j1 - 1;
                }
                int j2 = i + 1;
                d = 0;
                while (j2 >= 0 && CongThuc[j2] != '(') j2++;
                while (j2 <= CongThuc.Length-1)
                {
                    if (CongThuc[j2] == ')') d = d - 1;
                    if (CongThuc[j2] == '(') d = d + 1;
                    if (d == 0) break;
                    j2 = j2 + 1;
                }
                while (j2 >= 0 && CongThuc[j2] != '(') j2++;
                while (j2 <= CongThuc.Length - 1)
                {
                    if (CongThuc[j2] == ')') d = d - 1;
                    if (CongThuc[j2] == '(') d = d + 1;
                    if (d == 0) break;
                    j2 = j2 + 1;
                }
                String subCongThuc = CongThuc.Substring(j1, j2 - j1 + 1);
                String DK = subCongThuc.Split('?')[0];
                String GT1 = subCongThuc.Split('?')[1].Split(':')[0];
                String GT2 = subCongThuc.Split('?')[1].Split(':')[1];
                String subCongThucMoi = String.Format("if({0},{1},{2})", DK, GT1, GT2);
                CongThuc = CongThuc.Replace(subCongThuc, subCongThucMoi);
            }
            //Loai bo "=="
            CongThuc = CongThuc.Replace("==","=");
            return CongThuc;
        }
    }
}