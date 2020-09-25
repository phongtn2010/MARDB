using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract;
using System.Collections.Specialized;
using DomainModel;
using System.Data;
using DomainModel.Controls;
using System.Data.SqlClient;


namespace DATA0200025
{
    public class HangMauModels:Bang
    {
        public HangMauModels()
            : base("BC_HangMau")
        {
            GiaTriKhoa = null;
        }

        //override public Dictionary<string, object> LayGoiDuLieu(NameValueCollection dataIn, Boolean DuLieuSua)
        //{
        //    Dictionary<string, object> dicData = null;
        //    NameValueCollection data = null;

        //    if (dataIn != null)
        //    {
        //        data = new NameValueCollection();
        //        data.Add(dataIn);
        //    }
        //    else
        //    {
        //        data = this.dataTheoGiaTriKhoa();
        //    }

        //    dicData = new Dictionary<string, object>();
        //    dicData["TenBang"] = this.TenBang;
        //    dicData["TruongKhoa"] = this.TruongKhoa;
        //    if (this.GiaTriKhoa != null)
        //    {
        //        data[this.TruongKhoa] = this.GiaTriKhoa.ToString();
        //    }

        //    dicData["data"] = data;
        //    return dicData;
        //}

        override public Boolean IsValid(string TenTruong, ref object GiaTri, NameValueCollection arrLoi)
        {


            if (TenTruong.ToUpper() == "sTenHang")
            {
                arrLoi.Add("err_sTenHang", NgonNgu.LayXau("Phải nhập tên hàng mẫu ."));
            }

            return true;
        }

        public static string LayXauHangMau(string Path, string XauHanhDong, string XauSapXep, string MaHangMauCha, int Cap, ref int ThuTu)
        {
            String vR = "";
            String SQL = string.Format("SELECT * FROM BC_HangMau WHERE iID_MaHangMau_Cha = {0} ORDER BY iSTT, iID_MaHangMau", MaHangMauCha);
            DataTable dt = Connection.GetDataTable(SQL);

            if (dt.Rows.Count > 0)
            {
                int i, tgThuTu;

                string strPG = "", strXauMenuCon, strDoanTrang = "";

                for (i = 1; i <= Cap; i++)
                {
                    strDoanTrang += "&nbsp;&nbsp;";
                }
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    ThuTu++;
                    tgThuTu = ThuTu;
                    DataRow Row = dt.Rows[i];
                    String strHanhDong = XauHanhDong.Replace("%23%23", Row["iID_MaHangMau"].ToString());
                    strXauMenuCon = LayXauHangMau(Path, XauHanhDong, XauSapXep, Row["iID_MaHangMau"].ToString(), Cap + 1, ref ThuTu);
                    String strXauPopup = "<div style=\"float: right;\"><a href=\"HangMau/Dialog?MaHangMau=" + Row["iID_MaHangMau"].ToString() + "\" onclick=\"$(this).modal({width:480, height:150}).open(); return false;\" class=\"icoMr\" title=\"Thiết lập công thức\"><b>...<b></a></div>";

                    String MaHangMauHT;
                    String TenHang = "", CongThuc = "", BackGround = "", MauSac = "";
                    int Heigh = 0, FontSize = 0;
                    Boolean Bold = false;
                    Boolean Italic = false;
                    Boolean Underline = false;

                    MaHangMauHT = Convert.ToString(Row["iID_MaHangMau"]);
                    TenHang = Convert.ToString(Row["sTenHang"]);
                    CongThuc = Convert.ToString(Row["sCongThuc"]);
                    BackGround = Convert.ToString(Row["sBackGround"]);
                    MauSac = Convert.ToString(Row["sColor"]);
                    Heigh = Convert.ToInt32(Row["iHeight"]);
                    FontSize = Convert.ToInt32(Row["iFontSize"]);
                    Bold = Convert.ToBoolean(Row["bBold"]);
                    Italic = Convert.ToBoolean(Row["bItalic"]);
                    Underline = Convert.ToBoolean(Row["bUnderline"]);

                    String tgStyle = "";

                    if (Convert.ToInt16(Row["iHeight"]) > 0)
                    {
                        tgStyle += String.Format("height:{0}px;", Row["iHeight"]);
                    }
                    if (Convert.ToString(Row["sBackground"]) != "")
                    {
                        tgStyle += String.Format("background-color:{0};", Row["sBackground"]);
                    }
                    if (Convert.ToString(Row["sColor"]) != "")
                    {
                        tgStyle += String.Format("color: {0};", Row["sColor"]);
                    }
                    if (Convert.ToInt16(Row["iFontSize"]) > 0)
                    {
                        tgStyle += String.Format("font-size:{0}px;", Row["iFontSize"]);
                    }
                    if (Convert.ToBoolean(Row["bBold"]))
                    {
                        tgStyle += String.Format("font-weight:bold;");
                    }
                    if (Convert.ToBoolean(Row["bItalic"]))
                    {
                        tgStyle += String.Format("font-style:italic;");
                    }
                    if (Convert.ToBoolean(Row["bUnderline"]))
                    {
                        tgStyle += String.Format("text-decoration:underline;");
                    }

                    if (strXauMenuCon != "")
                    {
                        strHanhDong += XauSapXep.Replace("%23%23", Row["iID_MaHangMau"].ToString());
                    }
                    strPG += string.Format("<tr style=\"{0}\">", tgStyle);
                    strPG += string.Format("<td>{0}</td>", MaHangMauHT);
                    if (Cap == 0)
                    {
                        strPG += string.Format("<td>{0}{1}</td>", strDoanTrang, TenHang);
                    }
                    else
                    {
                        if (tgThuTu % 2 == 0)
                        {
                            strPG += string.Format("<td>{0}{1}</td>", strDoanTrang, TenHang);
                        }
                        else
                        {
                            strPG += string.Format("<td>{0}{1}</td>", strDoanTrang, TenHang);
                        }

                    }

                    strPG += string.Format("<td>{0}{1}</td>", CongThuc, strXauPopup);
                    strPG += string.Format("<td>{0}</td>", Heigh);
                    strPG += string.Format("<td>{0}</td>", BackGround);
                    strPG += string.Format("<td>{0}</td>", MauSac);
                    strPG += string.Format("<td align=\"center\">{0}</td>", FontSize);
                    strPG += string.Format("<td align=\"center\">{0}</td>", MyHtmlHelper.LabelCheckBox("", Bold.ToString(), ""));
                    strPG += string.Format("<td align=\"center\">{0}</td>", MyHtmlHelper.LabelCheckBox("", Italic.ToString(), ""));
                    strPG += string.Format("<td align=\"center\">{0}</td>", MyHtmlHelper.LabelCheckBox("", Underline.ToString(), ""));

                    if (tgThuTu % 2 == 0)
                    {
                        strPG += string.Format("<td style=\"background-color:#dff0fb;\">{0}</td>", strHanhDong);
                    }
                    else
                    {
                        strPG += string.Format("<td>{0}</td>", strHanhDong);
                    }

                    strPG += string.Format("</tr>");
                    strPG += strXauMenuCon;
                }
                vR = String.Format("{0}", strPG);
            }
            dt.Dispose();
            return vR;
        }
        
        public static string LayXauBangMauHangMau(string ParentID, string MaBangMau, string MaHangMauCha, int Cap, ref int ThuTu)
        {
            String vR = "";
            String SQL = string.Format("SELECT * FROM BC_HangMau WHERE iID_MaHangMau_Cha={0} ORDER BY iSTT, iID_MaHangMau", MaHangMauCha);
            DataTable dt = Connection.GetDataTable(SQL);

            if (dt.Rows.Count > 0)
            {
                int i, tgThuTu;

                string strPG = "", strXauMenuCon, strDoanTrang = "";

                for (i = 1; i <= Cap; i++)
                {
                    strDoanTrang += "&nbsp;&nbsp;";
                }
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    ThuTu++;
                    tgThuTu = ThuTu;
                    DataRow Row = dt.Rows[i];
                    strXauMenuCon = LayXauBangMauHangMau(ParentID,MaBangMau,Row["iID_MaHangMau"].ToString(), Cap + 1, ref ThuTu);

                    String MaHangMauHT;
                    String TenHang = "", CongThuc = "", BackGround = "", MauSac = "";
                    int Heigh = 0, FontSize = 0;
                    Boolean Bold = false;
                    Boolean Italic = false;
                    Boolean Underline = false;

                    MaHangMauHT = Convert.ToString(Row["iID_MaHangMau"]);
                    TenHang = Convert.ToString(Row["sTenHang"]);
                    CongThuc = Convert.ToString(Row["sCongThuc"]);
                    BackGround = Convert.ToString(Row["sBackGround"]);
                    MauSac = Convert.ToString(Row["sColor"]);
                    Heigh = Convert.ToInt32(Row["iHeight"]);
                    FontSize = Convert.ToInt32(Row["iFontSize"]);
                    Bold = Convert.ToBoolean(Row["bBold"]);
                    Italic = Convert.ToBoolean(Row["bItalic"]);
                    Underline = Convert.ToBoolean(Row["bUnderline"]);

                    String tgStyle = "";

                    if (Convert.ToInt16(Row["iHeight"]) > 0)
                    {
                        tgStyle += String.Format("height:{0}px;", Row["iHeight"]);
                    }
                    if (Convert.ToString(Row["sBackground"]) != "")
                    {
                        tgStyle += String.Format("background-color:{0};", Row["sBackground"]);
                    }
                    if (Convert.ToString(Row["sColor"]) != "")
                    {
                        tgStyle += String.Format("color: {0};", Row["sColor"]);
                    }
                    if (Convert.ToInt16(Row["iFontSize"]) > 0)
                    {
                        tgStyle += String.Format("font-size:{0}px;", Row["iFontSize"]);
                    }
                    if (Convert.ToBoolean(Row["bBold"]))
                    {
                        tgStyle += String.Format("font-weight:bold;");
                    }
                    if (Convert.ToBoolean(Row["bItalic"]))
                    {
                        tgStyle += String.Format("font-style:italic;");
                    }
                    if (Convert.ToBoolean(Row["bUnderline"]))
                    {
                        tgStyle += String.Format("text-decoration:underline;");
                    }


                    strPG += string.Format("<tr style=\"{0}\">", tgStyle);
                    strPG += string.Format("<td>{0}</td>", MaHangMauHT);
                    if (Cap == 0)
                    {
                        strPG += string.Format("<td>{0}{1}</td>", strDoanTrang, TenHang);
                    }
                    else
                    {
                        if (tgThuTu % 2 == 0)
                        {
                            strPG += string.Format("<td>{0}{1}</td>", strDoanTrang, TenHang);
                        }
                        else
                        {
                            strPG += string.Format("<td>{0}{1}</td>", strDoanTrang, TenHang);
                        }

                    }

                    strPG += string.Format("<td>{0}</td>", CongThuc);
                    strPG += string.Format("<td>{0}</td>", Heigh);
                    strPG += string.Format("<td>{0}</td>", BackGround);
                    strPG += string.Format("<td>{0}</td>", MauSac);
                    strPG += string.Format("<td align=\"center\">{0}</td>", FontSize);
                    strPG += string.Format("<td align=\"center\">{0}</td>", MyHtmlHelper.LabelCheckBox("", Bold.ToString(), ""));
                    strPG += string.Format("<td align=\"center\">{0}</td>", MyHtmlHelper.LabelCheckBox("", Italic.ToString(), ""));
                    strPG += string.Format("<td align=\"center\">{0}</td>", MyHtmlHelper.LabelCheckBox("", Underline.ToString(), ""));

                    SqlCommand cmd;
                    cmd = new SqlCommand("SELECT iID_MaHangMau FROM BC_BangMau_HangMau WHERE iID_MaBangMau = @iID_MaBangMau ORDER BY iSTT");
                    cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
                    DataTable dt1 = Connection.GetDataTable(cmd);
                    String tg="";
                    int j;
                    for (j = 0; j <= dt1.Rows.Count - 1; j++)
                    { 
                        if (Convert.ToString(Row["iID_MaHangMau"])==Convert.ToString(dt1.Rows[j]["iID_MaHangMau"]))
                        {
                            tg = "on";
                            break;
                        }
                    }

                    if (tgThuTu % 2 == 0)
                    {
                        strPG += string.Format("<td align=\"center\" width=\"5%\">{0}</td>", MyHtmlHelper.CheckBox(ParentID, tg, "iID_MaHangMau", "", String.Format("value='{0}'", Convert.ToString(dt.Rows[i]["iID_MaHangMau"]))));
                    }
                    else
                    {
                        strPG += string.Format("<td align=\"center\" width=\"5%\">{0}</td>", MyHtmlHelper.CheckBox(ParentID, tg, "iID_MaHangMau", "", String.Format("value='{0}'", Convert.ToString(dt.Rows[i]["iID_MaHangMau"]))));
                    }
                    strPG += string.Format("</tr>");
                    strPG += strXauMenuCon;
                }
                vR = String.Format("{0}", strPG);
            }
            dt.Dispose();
            return vR;
        }

        public static void ThemHangMauVaoBangMau(String MaBangMau, String MaHangMau, int MaHangMauCha)
        {
            String NewID = Globals.getNewGuid().ToString();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO BC_BangMau_HangMau(iID_MaTam,iID_MaBangMau,iID_MaHangMau, iID_MaHangMau_Cha) " +
                        "VALUES(@iID_MaTam,@iID_MaBangMau, @iID_MaHangMau, @iID_MaHangMau_Cha)";
            cmd.Parameters.AddWithValue("@iID_MaTam", NewID);
            cmd.Parameters.AddWithValue("@iID_MaBangMau", MaBangMau);
            cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
            cmd.Parameters.AddWithValue("@iID_MaHangMau_Cha", MaHangMauCha);
            Connection.UpdateDatabase(cmd);
            cmd.Dispose();
        }

        public static int Get_MahangMau_Cha(int MaHangMau)
        {
            int vR = 0;

            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM BC_HangMau WHERE iID_MaHangMau=@iID_MaHangMau";
            cmd.Parameters.AddWithValue("@iID_MaHangMau", MaHangMau);
            DataTable dt = Connection.GetDataTable(cmd,CThamSo.iKetNoi);
            cmd.Dispose();
            if (dt.Rows.Count > 0)
            {
                vR = Convert.ToInt32(dt.Rows[0]["iID_MaHangMau_Cha"]);
            }
            dt.Dispose();
            return vR;
        }
    }
}