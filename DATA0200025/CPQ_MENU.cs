using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DomainModel;
using DomainModel.Controls;

namespace DATA0200025
{
    public class CPQ_MENU
    {
        public static DataTable Get_Table_Menu_Cha(string iID_MaMenuItemCha)
        {
            DataTable vR = null;

            int iMaMenuCha = 0;
            if (String.IsNullOrEmpty(iID_MaMenuItemCha) == false)
            {
                iMaMenuCha = Convert.ToInt32(iID_MaMenuItemCha);
            }

            string SQL = "SELECT * FROM MENU_MenuItem WHERE iID_MaMenuItemCha = @iID_MaMenuItemCha ORDER BY tThuTu";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaMenuItemCha", iMaMenuCha);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();

            return vR;
        }

        public static DataTable Get_One_Table_Menu(string MaMenu)
        {
            DataTable vR = null;

            string SQL = "SELECT * FROM MENU_MenuItem WHERE iID_MaMenuItem = @iID_MaMenuItem";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaMenuItem", MaMenu);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();

            return vR;
        }

        public static int Insert(String iID_MaMenuItemCha, String sTen, String sURL)
        {
            int vR = -1;

            int iMaMenuCha = 0;
            if (String.IsNullOrEmpty(iID_MaMenuItemCha) == false)
            {
                iMaMenuCha = Convert.ToInt32(iID_MaMenuItemCha);
            }

            try
            {
                String SQL = "INSERT INTO MENU_MenuItem(iID_MaMenuItemCha, sTen, sURL) VALUES(@iID_MaMenuItemCha, @sTen, @sURL)";

                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@iID_MaMenuItemCha", iMaMenuCha);
                cmd.Parameters.AddWithValue("@sTen", sTen);
                cmd.Parameters.AddWithValue("@sURL", sURL);
                vR = Connection.UpdateDatabase(cmd, CThamSo.iKetNoi);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static int Update(int id, String iID_MaMenuItemCha, String sTen, String sURL)
        {
            int vR = -1;

            int iMaMenuCha = 0;
            if (String.IsNullOrEmpty(iID_MaMenuItemCha) == false)
            {
                iMaMenuCha = Convert.ToInt32(iID_MaMenuItemCha);
            }

            try
            {
                String SQL = "UPDATE MENU_MenuItem SET iID_MaMenuItemCha = @iID_MaMenuItemCha, sTen = @sTen, sURL = @sURL WHERE iID_MaMenuItem = @id";

                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@iID_MaMenuItemCha", iMaMenuCha);
                cmd.Parameters.AddWithValue("@sTen", sTen);
                cmd.Parameters.AddWithValue("@sURL", sURL);
                cmd.Parameters.AddWithValue("@id", id);
                vR = Connection.UpdateDatabase(cmd, CThamSo.iKetNoi);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static int Delete(int id)
        {
            int vR = -1;

            try
            {
                String SQL = "DELETE FROM MENU_MenuItem WHERE iID_MaMenuItem = @id";

                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@id", id);
                vR = Connection.UpdateDatabase(cmd, CThamSo.iKetNoi);
                cmd.Dispose();

                vR = 1;
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static int Sort(int id, int tThuTu)
        {
            int vR = -1;

            try
            {
                String SQL = "UPDATE MENU_MenuItem SET tThuTu = @tThuTu WHERE iID_MaMenuItem = @id";

                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@tThuTu", tThuTu);
                cmd.Parameters.AddWithValue("@id", id);
                vR = Connection.UpdateDatabase(cmd, CThamSo.iKetNoi);
                cmd.Dispose();

                vR = 1;
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static string LayXauMenu(string Path, string XauHanhDong, string XauSapXep, int MaMenuCha, int Cap, ref int ThuTu)
        {
            string vR = "";

            String SQL = string.Format("SELECT * FROM MENU_MenuItem WHERE iID_MaMenuItemCha={0} ORDER BY tThuTu, iID_MaMenuItem", MaMenuCha);
            DataTable dt = Connection.GetDataTable(SQL, CThamSo.iKetNoi);

            if (dt.Rows.Count > 0)
            {
                int i, tgThuTu;

                string strPG = "", url, strXauMenuCon, strDoanTrang = "";

                for (i = 1; i <= Cap; i++)
                {
                    strDoanTrang += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    ThuTu++;
                    tgThuTu = ThuTu;
                    DataRow Row = dt.Rows[i];
                    String strHanhDong = XauHanhDong.Replace("%23%23", Row["iID_MaMenuItem"].ToString());
                    strXauMenuCon = LayXauMenu(Path, XauHanhDong, XauSapXep, Convert.ToInt32(Row["iID_MaMenuItem"]), Cap + 1, ref ThuTu);

                    if (strXauMenuCon != "")
                    {
                        strHanhDong += XauSapXep.Replace("%23%23", Row["iID_MaMenuItem"].ToString());
                    }
                    url = "#";
                    if (dt.Rows[i]["sURL"] != DBNull.Value && string.IsNullOrEmpty((string)(Row["sURL"])) == false && (string)(Row["sURL"]) != "#")
                    {
                        url = Path + Row["sURL"];
                    }
                    strPG += string.Format("<tr>");
                    if (Cap == 0)
                    {
                        strPG += string.Format("<td style=\"background-color:#f4f9fd;\">{1}<b><a href=\"{0}\">{2}</a></b></td>", url, strDoanTrang, Row["sTen"]);
                        strPG += string.Format("<td style=\"background-color:#f4f9fd;\"><b><a href=\"{0}\">{1}</a></b></td>", url,  Row["sURL"]);
                    }
                    else
                    {
                        if (tgThuTu % 2 == 0)
                        {
                            strPG += string.Format("<td style=\"background-color:#dff0fb;\">{1}<a href=\"{0}\">{2}</a></td>", url, strDoanTrang, Row["sTen"]);
                            strPG += string.Format("<td style=\"background-color:#dff0fb;\"><a href=\"{0}\">{1}</a></td>", url, Row["sURL"]);
                        }
                        else
                        {
                            strPG += string.Format("<td>{1}<a href=\"{0}\">{2}</a></td>", url, strDoanTrang, Row["sTen"]);
                            strPG += string.Format("<td><a href=\"{0}\">{1}</a></td>", url, Row["sURL"]);
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

        public static string LayXauMenuItemCam(string MaLuat, int MaMenuCha, int Cap, ref int ThuTu)
        {
            int MaMenu;
            string vR = "";
            String SQL = string.Format("SELECT * FROM MENU_MenuItem WHERE iID_MaMenuItemCha={0} ORDER BY tThuTu, iID_MaMenuItem", MaMenuCha);
            DataTable dt = Connection.GetDataTable(SQL, CThamSo.iKetNoi);

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
                    String strTG;

                    MaMenu = Convert.ToInt32(Row["iID_MaMenuItem"]);
                    strXauMenuCon = LayXauMenuItemCam(MaLuat, MaMenu, Cap + 1, ref ThuTu);

                    int iCount_MenuCam = Select_Count_MenuCam(MaMenu, Convert.ToString(MaLuat));

                    if (iCount_MenuCam > 0)
                    {
                        strTG = String.Format("<input name=\"{0}\" value=\"{1}\" type=\"checkbox\" checked=\"checked\" >", "MenuItem_Cam", MaMenu);
                    }
                    else
                    {
                        strTG = String.Format("<input name=\"{0}\" value=\"{1}\" type=\"checkbox\" >", "MenuItem_Cam", MaMenu);
                    }
                    String classtr = "";
                    if (i % 2 == 0)
                    {
                        classtr = "class=\"alt\"";
                    }
                    strPG += string.Format("<tr " + classtr + ">");
                    if (tgThuTu % 2 == 0)
                    {
                        strPG += string.Format("<td style=\"padding: 3px 2px;\">{1}{0}</td>", Row["sTen"], strDoanTrang);
                    }
                    else
                    {
                        strPG += string.Format("<td style=\"padding: 3px 2px;\">{1}{0}</td>", Row["sTen"], strDoanTrang);
                    }
                    if (tgThuTu % 2 == 0)
                    {
                        strPG += string.Format("<td style=\"padding: 3px 2px;\" align=\"center\">{0}</td>", strTG);
                    }
                    else
                    {
                        strPG += string.Format("<td style=\"padding: 3px 2px;\" align=\"center\">{0}</td>", strTG);
                    }

                    strPG += string.Format("</tr>");
                    strPG += strXauMenuCon;
                }
                vR = String.Format("{0}", strPG);
            }
            dt.Dispose();
            return vR;
        }

        public static int Select_Count_MenuCam(int iID_MaMenuItem, string IID_MALUAT)
        {
            int vR = 0;

            string SQL = "SELECT Count(*) FROM PQ_MenuItem_Cam WHERE iID_MaMenuItem=@iID_MaMenuItem AND IID_MALUAT=@IID_MALUAT";

            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaMenuItem", iID_MaMenuItem);
            cmd.Parameters.AddWithValue("@IID_MALUAT", IID_MALUAT);
            vR = Convert.ToInt32(Connection.GetValue(cmd, 0, CThamSo.iKetNoi));
            cmd.Dispose();

            return vR;
        }

        public static int Insert_MenuCam(int iID_MaMenuItem, string iID_MaLuat)
        {
            int vR = -1;

            try
            {
                String SQL = "INSERT INTO PQ_MenuItem_Cam(iID_MaMenuItem, IID_MALUAT) VALUES(@iID_MaMenuItem, @IID_MALUAT)";

                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@iID_MaMenuItem", iID_MaMenuItem);
                cmd.Parameters.AddWithValue("@IID_MALUAT", iID_MaLuat);
                vR = Connection.UpdateDatabase(cmd, CThamSo.iKetNoi);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }
        public static int Delete_MenuCam(string iID_MaLuat)
        {
            int vR = -1;

            try
            {
                String SQL = "DELETE FROM PQ_MenuItem_Cam WHERE iID_MaLuat = @iID_MaLuat";

                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@iID_MaLuat", iID_MaLuat);
                vR = Connection.UpdateDatabase(cmd, CThamSo.iKetNoi);
                cmd.Dispose();

                vR = 1;
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static Boolean CoQuyenXemTheoMenu(String URL, String MaNguoiDung)
        {
            Boolean CoQuyen = true;
            if (URL.Length > 0)
            {
                URL = URL.Remove(0, 1);
            }
            URL = URL.ToLower();
            String MaNhomNguoiDung = CPQ_NGUOIDUNG.LayMaNhomNguoiDung(MaNguoiDung);

            String SQL = "SELECT sURL FROM MENU_MenuItem WHERE iID_MaMenuItem IN (SELECT iID_MaMenuItem FROM PQ_MenuItem_Cam WHERE IID_MALUAT IN " +
                         "(SELECT IID_MALUAT FROM PQ_NHOMNGUOIDUNG_LUAT WHERE IID_MANHOMNGUOIDUNG=@IID_MANHOMNGUOIDUNG))";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("IID_MANHOMNGUOIDUNG", MaNhomNguoiDung);
            DataTable dt = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();
            if (dt != null && dt.Rows.Count > 0)
            {
                String menuUrl = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    menuUrl = Convert.ToString(dt.Rows[i]["sURL"]).ToLower();
                    //if (URL.Equals(menuUrl) && menuUrl != "" || URL.IndexOf(menuUrl) >= 0)
                    if ((URL.Equals(menuUrl)) && menuUrl != "")
                    {
                        CoQuyen = false;
                        break;
                    }
                }
            }
            if (dt != null)
            {
                dt.Dispose();
            }
            return CoQuyen;
        }
    }
}
