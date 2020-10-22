using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel;
using DomainModel.Controls;
using System.Data;
using System.Data.SqlClient;

namespace DATA0200025
{
    public class CMenu
    {
        public static string LayXauMenu(String Path, int MaMenuItemCha, int Cap)
        {
            string vR = "";
            String SQL = string.Format("SELECT * FROM MENU_MenuItem WHERE bHoatDong=1 AND iID_MaMenuItemCha={0} ORDER BY tThuTu, iID_MaMenuItem", MaMenuItemCha);
            DataTable dt = Connection.GetDataTable(SQL);
            if (dt.Rows.Count > 0)
            {
                int i, MaMenuItem;
                string strPD, strPG = "", strPC, url, strXauMenuCon;
                if (Cap == 0)
                {
                    strPD = "<div id=\"dropList\"><ul id=\"menu\">";
                }
                else if (Cap == 1)
                {
                    strPD = "";
                }
                else
                {
                    strPD = "<ul>";
                }
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    MaMenuItem = (int)(dt.Rows[i]["iID_MaMenuItem"]);
                    strXauMenuCon = LayXauMenu(Path, MaMenuItem, Cap + 1);
                    url = "#";
                    if (dt.Rows[i]["sURL"] != DBNull.Value && string.IsNullOrEmpty((string)(dt.Rows[i]["sURL"])) == false && (string)(dt.Rows[i]["sURL"]) != "#")
                    {
                        url = Path + dt.Rows[i]["sURL"];
                    }
                    if (Cap == 0)
                    {
                        strPG += string.Format("<li class=\"level1-li sub\"><a class=\"level1-a\" href=\"{0}\">{1}<!--[if gte IE 7]><!--></a><!--<![endif]-->", url, dt.Rows[i]["sTen"]);
                        strPG += string.Format("<!--[if lte IE 6]><table><tr><td><![endif]-->");
                        strPG += string.Format("<div class=\"listHolder col1\">");
                        strPG += string.Format("<div class=\"listCol\">");
                        strPG += strXauMenuCon;
                        strPG += string.Format("</div>");
                        strPG += string.Format("</div>");
                        strPG += string.Format("<!--[if lte IE 6]></td></tr></table></a><![endif]-->");
                        strPG += string.Format("</li>");
                    }
                    else if (Cap == 1)
                    {
                        strPG += string.Format("<h5><a href=\"{0}\">{1}</a></h5>", url, dt.Rows[i]["sTen"]);
                        strPG += strXauMenuCon;
                    }
                    else
                    {
                        strPG += string.Format("<li><a href=\"{0}\">{1}</a></li>", url, dt.Rows[i]["sTen"]);
                        strPG += strXauMenuCon;
                    }
                }
                if (Cap == 0)
                {
                    strPC = "</ul></div>";
                }
                else if (Cap == 1)
                {
                    strPC = "";
                }
                else
                {
                    strPC = "</ul>";
                }
                vR = String.Format("{0}{1}{2}", strPD, strPG, strPC);
            }
            dt.Dispose();
            return vR;
        }

        public static string LayXauMenu(String Path, int sMenu_Active, String MaLuat, int MaMenuItemCha, int Cap)
        {
            string vR = "", Ten = "", ClassName;
            String SQL = string.Format("SELECT * FROM MENU_MenuItem WHERE bHoatDong=1 AND iID_MaMenuItemCha={0} ORDER BY tThuTu, iID_MaMenuItem", MaMenuItemCha);
            DataTable dt = Connection.GetDataTable(SQL);
            if (dt.Rows.Count > 0)
            {
                int MaMenuItem;
                int i;
                string strPD, strPG = "", strPC, url, urlMenu, strXauMenuCon;
                if (Cap == 0)
                {
                    strPD = "<ul class=\"sidebar-menu\">";
                }
                else if (Cap == 1)
                {
                    strPD = "";
                }
                else
                {
                    strPD = "<ul class=\"treeview-menu\">";
                }
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    MaMenuItem = (int)(dt.Rows[i]["iID_MaMenuItem"]);

                    SqlCommand cmd;
                    if (String.IsNullOrEmpty(MaLuat) == false)
                    {
                        cmd = new SqlCommand("SELECT Count(*) FROM PQ_MenuItem_Cam WHERE iID_MaMenuItem=@iID_MaMenuItem AND iID_MaLuat=@iID_MaLuat");
                        cmd.Parameters.AddWithValue("@iID_MaLuat", MaLuat);
                        cmd.Parameters.AddWithValue("@iID_MaMenuItem", MaMenuItem);
                    }
                    else
                    {
                        cmd = new SqlCommand("SELECT Count(*) FROM PQ_MenuItem_Cam WHERE iID_MaMenuItem=@iID_MaMenuItem");
                        cmd.Parameters.AddWithValue("@iID_MaMenuItem", MaMenuItem);
                    }

                    if (Convert.ToInt32(Connection.GetValue(cmd, 0)) == 0)
                    {
                        strXauMenuCon = LayXauMenu(Path, sMenu_Active, MaLuat, MaMenuItem, Cap + 1);

                        url = "#";
                        if (dt.Rows[i]["sURL"] != DBNull.Value && string.IsNullOrEmpty((string)(dt.Rows[i]["sURL"])) == false && (string)(dt.Rows[i]["sURL"]) != "#")
                        {
                            if (Convert.ToString(dt.Rows[i]["sURL"]).StartsWith("http://"))
                            {
                                if(Convert.ToString(dt.Rows[i]["sURL"]).IndexOf("?")>-1)
                                {
                                    url = Convert.ToString(dt.Rows[i]["sURL"]) + "&menu=" + Convert.ToString(dt.Rows[i]["iID_MaMenuItem"]);
                                }
                                else
                                {
                                    url = Convert.ToString(dt.Rows[i]["sURL"]) + "?menu=" + Convert.ToString(dt.Rows[i]["iID_MaMenuItem"]);
                                }
                            }
                            else
                            {
                                if (Convert.ToString(dt.Rows[i]["sURL"]).IndexOf("?") > -1)
                                {
                                    url = Path + dt.Rows[i]["sURL"] + "&menu=" + Convert.ToString(dt.Rows[i]["iID_MaMenuItem"]);
                                }
                                else
                                {
                                    url = Path + dt.Rows[i]["sURL"] + "?menu=" + Convert.ToString(dt.Rows[i]["iID_MaMenuItem"]);
                                }
                            }
                        }
                        Ten = Convert.ToString(dt.Rows[i]["sTen"]);

                        ClassName = "";
                        if (sMenu_Active == MaMenuItem)
                        {
                            ClassName = "active";   //active
                        }
                        else
                        {
                            ClassName = "";
                        }
                        if (Cap == 0)
                        {
                            if (url != "#")
                            {
                                strPG += string.Format("<li class=\"header\"><a href=\"{0}\">{1}</a></li>", url, Ten);
                            }
                            else
                            {
                                strPG += string.Format("<li class=\"header\">{0}</li>", Ten);
                            }
                            if (strXauMenuCon.Trim() != "")
                            {
                                strPG += strXauMenuCon;
                            }
                        }
                        else if (Cap == 1)
                        {
                            strPG += string.Format("<li class=\"treeview {0}\">", ClassName);
                            strPG += string.Format("<a href=\"{0}\"><i class=\"fa fa-file-text-o\"></i><span>{1}</span><i class=\"fa fa-angle-left pull-right\"></i></a>", url, Ten);
                            strPG += strXauMenuCon;
                            strPG += string.Format("</li>");
                        }
                        else
                        {
                            strPG += string.Format("<li><a href=\"{0}\"><i class='fa fa-chevron-circle-right'></i>{1}</a></li>", url, Ten);
                            strPG += strXauMenuCon;
                        }

                    }
                    cmd.Dispose();
                }
                if (Cap == 0)
                {
                    strPC = "</ul>";
                }
                else if (Cap == 1)
                {
                    strPC = "";
                }
                else
                {
                    strPC = "</ul>";
                }
                vR = String.Format("{0}{1}{2}", strPD, strPG, strPC);
            }
            dt.Dispose();
            return vR;
        }

        public static string LayXauMenu(String Path, String MaLuat, int MaMenuItemCha, int Cap, ref int MaxLength)
        {
            string vR = "", Ten = "", ClassName;
            int iTG = 0;
            String SQL = string.Format("SELECT * FROM MENU_MenuItem WHERE bHoatDong=1 AND iID_MaMenuItemCha={0} ORDER BY tThuTu, iID_MaMenuItem", MaMenuItemCha);
            DataTable dt = Connection.GetDataTable(SQL);
            if (dt.Rows.Count > 0)
            {
                int MaMenuItem;
                int i;
                string strPD, strPG = "", strPC, url, strXauMenuCon;
                if (Cap == 0)
                {
                    strPD = "<div id=\"dropList\"><ul id=\"menu\">";
                }
                else if (Cap == 1)
                {
                    strPD = "";
                }
                else
                {
                    strPD = "<ul>";
                }
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    MaMenuItem = (int)(dt.Rows[i]["iID_MaMenuItem"]);

                    SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM PQ_MenuItem_Cam WHERE iID_MaMenuItem=@iID_MaMenuItem AND iID_MaLuat=@iID_MaLuat");
                    cmd.Parameters.AddWithValue("@iID_MaLuat", MaLuat);
                    cmd.Parameters.AddWithValue("@iID_MaMenuItem", MaMenuItem);
                    if (Convert.ToInt32(Connection.GetValue(cmd, 0)) == 0)
                    {
                        iTG = 0;
                        strXauMenuCon = LayXauMenu(Path, MaLuat, MaMenuItem, Cap + 1, ref iTG);
                        url = "#";
                        if (dt.Rows[i]["sURL"] != DBNull.Value && string.IsNullOrEmpty((string)(dt.Rows[i]["sURL"])) == false && (string)(dt.Rows[i]["sURL"]) != "#")
                        {
                            if (Convert.ToString(dt.Rows[i]["sURL"]).StartsWith("http://"))
                            {
                                url = Convert.ToString(dt.Rows[i]["sURL"]);
                            }
                            else
                            {
                                url = Path + dt.Rows[i]["sURL"];
                            }
                        }
                        Ten = Convert.ToString(dt.Rows[i]["sTen"]);
                        if (Ten.Length > MaxLength) MaxLength = Ten.Length;
                        if (Cap == 0)
                        {
                            ClassName = "col1";
                            if (iTG >= 30)
                            {
                                ClassName = String.Format("col{0}", iTG / 15);
                            }
                            strPG += string.Format("<li class=\"level1-li sub\"><a class=\"level1-a\" href=\"{0}\">{1}<!--[if gte IE 7]><!--></a><!--<![endif]-->", url, Ten);
                            if (strXauMenuCon.Trim() != "")
                            {
                                strPG += string.Format("<!--[if lte IE 6]><table><tr><td><![endif]-->");
                                if (i < dt.Rows.Count - 1)
                                {
                                    strPG += string.Format("<div class=\"listHolder {0}\">", ClassName);
                                }
                                else
                                {
                                    strPG += string.Format("<div class=\"listHolder-phai {0}\">", ClassName);
                                }
                                strPG += string.Format("<div class=\"listCol\">");
                                strPG += strXauMenuCon;
                                strPG += string.Format("</div>");
                                strPG += string.Format("</div>");
                                strPG += string.Format("<!--[if lte IE 6]></td></tr></table></a><![endif]-->");
                            }
                            strPG += string.Format("</li>");
                        }
                        else if (Cap == 1)
                        {
                            strPG += string.Format("<h5><a href=\"{0}\">{1}</a></h5>", url, dt.Rows[i]["sTen"]);
                            strPG += strXauMenuCon;
                        }
                        else
                        {
                            strPG += string.Format("<li><a href=\"{0}\">{1}</a></li>", url, dt.Rows[i]["sTen"]);
                            strPG += strXauMenuCon;
                        }
                    }
                    cmd.Dispose();
                }
                if (Cap == 0)
                {
                    strPC = "</ul></div>";
                }
                else if (Cap == 1)
                {
                    strPC = "";
                }
                else
                {
                    strPC = "</ul>";
                }
                vR = String.Format("{0}{1}{2}", strPD, strPG, strPC);
            }
            dt.Dispose();
            return vR;
        }
    }
}
