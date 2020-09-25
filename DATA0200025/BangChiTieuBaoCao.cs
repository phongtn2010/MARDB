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
    public class BangChiTieuBaoCao:Bang
    {
        public BangChiTieuBaoCao()
            : base("BC_ChiTieuBaoCao")
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


            if (TenTruong.ToUpper() == "sTenChiTieu")
            {
                arrLoi.Add("err_sTenChiTieu", NgonNgu.LayXau("Phải nhập tên chỉ tiêu ."));
            }

            return true;
        }

        public static string LayXauMenu(string Path, string MaBaoCao, string XauHanhDong, string XauSapXep, int MaChiTieuCha, int Cap, ref int ThuTu)
        {
            string vR = "";
            String SQL = string.Format("SELECT * FROM BC_ChiTieuBaoCao WHERE iID_MaChiTieuCha={0} AND iID_MaBaoCao={1} ORDER BY iSTT, iID_MaChiTieu", MaChiTieuCha, MaBaoCao);
            DataTable dt = Connection.GetDataTable(SQL);

            if (dt.Rows.Count > 0)
            {
                int i, tgThuTu;

                string strPG = "", strXauMenuCon, strDoanTrang = "";

                for (i = 1; i <= Cap; i++)
                {
                    strDoanTrang += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    ThuTu++;
                    tgThuTu = ThuTu;
                    DataRow Row = dt.Rows[i];
                    String strHanhDong = XauHanhDong.Replace("%23%23", Row["iID_MaChiTieu"].ToString());
                    strXauMenuCon = LayXauMenu(Path, MaBaoCao, XauHanhDong, XauSapXep, (int)(Row["iID_MaChiTieu"]), Cap + 1, ref ThuTu);

                    if (strXauMenuCon != "")
                    {
                        strHanhDong += XauSapXep.Replace("%23%23", Row["iID_MaChiTieu"].ToString());
                    }
                    strPG += string.Format("<tr>");
                    if (Cap == 0)
                    {
                        strPG += string.Format("<td style=\"background-color:#f4f9fd;\">{0}<b>{1}</b></td>", strDoanTrang, Row["sTenChiTieu"]);
                    }
                    else
                    {
                        if (tgThuTu % 2 == 0)
                        {
                            strPG += string.Format("<td style=\"background-color:#dff0fb;\">{0}{1}</td>", strDoanTrang, Row["sTenChiTieu"]);
                        }
                        else
                        {
                            strPG += string.Format("<td>{0}{1}</td>",strDoanTrang, Row["sTenChiTieu"]);
                        }

                    }
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

        public static string LayChiTieuBaoCao(string MaBaoCao, int MaChiTieuCha, int Cap, ref int ThuTu)
        {
            String vR = "";
            String SQL = string.Format("SELECT * FROM BC_ChiTieuBaoCao WHERE iID_MaChiTieuCha={0} AND iID_MaBaoCao={1} ORDER BY iSTT, iID_MaChiTieu", MaChiTieuCha, MaBaoCao);
            DataTable dt = Connection.GetDataTable(SQL);
            if (dt.Rows.Count > 0)
            {
                int i, tgThuTu;

                string strPG = "", strXauMenuCon, strDoanTrang = "";

                for (i = 1; i <= Cap; i++)
                {
                    strDoanTrang += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    ThuTu++;
                    tgThuTu = ThuTu;
                    DataRow Row = dt.Rows[i];
                    strXauMenuCon = LayChiTieuBaoCao(MaBaoCao, (int)(Row["iID_MaChiTieu"]), Cap + 1, ref ThuTu);

                    strPG += string.Format("<tr>");
                    if (Cap == 0)
                    {
                        strPG += string.Format("<td style=\"background-color:#f4f9fd;\">{0}<b>{1}</b></td>", strDoanTrang, Row["sTenChiTieu"]);
                    }
                    else
                    {
                        if (tgThuTu % 2 == 0)
                        {
                            strPG += string.Format("<td style=\"background-color:#dff0fb;\">{0}{1}</td>", strDoanTrang, Row["sTenChiTieu"]);
                        }
                        else
                        {
                            strPG += string.Format("<td>{0}{1}</td>", strDoanTrang, Row["sTenChiTieu"]);
                        }

                    }
                    strPG += string.Format("<td>&nbsp;</td>");
                    strPG += string.Format("<td>&nbsp;</td>");
                    strPG += string.Format("<td>&nbsp;</td>");
                    strPG += string.Format("<td>&nbsp;</td>");
                    strPG += string.Format("<td>&nbsp;</td>");
                    strPG += string.Format("<td>&nbsp;</td>");
                    strPG += string.Format("<td>&nbsp;</td>");
                    strPG += string.Format("</tr>");
                    strPG += strXauMenuCon;
                }
                vR = String.Format("{0}", strPG);
            }
            dt.Dispose();
            return vR;
        }
    }
}