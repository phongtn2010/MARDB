using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Configuration;

namespace DomainModel
{
    public class NgonNgu
    {
        public static string MaNgonNgu = "1";
        public static string MaNgonNguGoc = "1";
        public static string MaDate = "vi";
        public static DataTable dt = null;

        public const String strV = "AÁÀẢẠÃĂẮẰẲẶẴÂẦẤẨẬẪBCDĐEÉÈẺẸẼÊẾỀỂỆỄFGHIÍÌỈỊĨJKMLNOÓÒỎỌÕƠỚỜỞỢỠÔỐỒỔỘỖPQRSTUÚÙỦỤŨƯỨỪỬỰỮVXYÝỲỶỴỸZaáàảạãăắằẳặẵâấầẩậẫbcdđeéèẻẹẽêếềểệễfghiíìỉịĩjkmlnoóòỏọõơớờởợỡôốồổộỗpqrstuúùủụũưứừửựữvxyýỳỷỵỹz0123456789";
        public const String strD = "AAAAAAAAAAAAAAAAAABCDDEEEEEEEEEEEEFGHIIIIIIJKMLNOOOOOOOOOOOOOOOOOOPQRSTUUUUUUUUUUUUVXYYYYYYZaaaaaaaaaaaaaaaaaabcddeeeeeeeeeeeefghiiiiiijkmlnoooooooooooooooooopqrstuuuuuuuuuuuuvxyyyyyyz0123456789";
        public const String strE = "AAAAAAAAAAAAAAAAAABCDDEEEEEEEEEEEEFGHIIIIIIJKMLNOOOOOOOOOOOOOOOOOOPQRSTUUUUUUUUUUUUVXYYYYYYZAAAAAAAAAAAAAAAAAABCDDEEEEEEEEEEEEFGHIIIIIIJKMLNOOOOOOOOOOOOOOOOOOPQRSTUUUUUUUUUUUUVXYYYYYYZ0123456789";
        public const String strF = "aaaaaaaaaaaaaaaaaabcddeeeeeeeeeeeefghiiiiiijkmlnoooooooooooooooooopqrstuuuuuuuuuuuuvxyyyyyyzaaaaaaaaaaaaaaaaaabcddeeeeeeeeeeeefghiiiiiijkmlnoooooooooooooooooopqrstuuuuuuuuuuuuvxyyyyyyz0123456789";
        public const String strG = "AÁÀẢẠÃĂẮẰẲẶẴÂẦẤẨẬẪBCDĐEÉÈẺẸẼÊẾỀỂỆỄFGHIÍÌỈỊĨJKMLNOÓÒỎỌÕƠỚỜỞỢỠÔỐỒỔỘỖPQRSTUÚÙỦỤŨƯỨỪỬỰỮVXYÝỲỶỴỸZAÁÀẢẠÃĂẮẰẲẶẴÂẦẤẨẬẪBCDĐEÉÈẺẸẼÊẾỀỂỆỄFGHIÍÌỈỊĨJKMLNOÓÒỎỌÕƠỚỜỞỢỠÔỐỒỔỘỖPQRSTUÚÙỦỤŨƯỨỪỬỰỮVXYÝỲỶỴỸZ0123456789";
        public const String strH = "aáàảạãăắằẳặẵâấầẩậẫbcdđeéèẻẹẽêếềểệễfghiíìỉịĩjkmlnoóòỏọõơớờởợỡôốồổộỗpqrstuúùủụũưứừửựữvxyýỳỷỵỹzaáàảạãăắằẳặẵâấầẩậẫbcdđeéèẻẹẽêếềểệễfghiíìỉịĩjkmlnoóòỏọõơớờởợỡôốồổộỗpqrstuúùủụũưứừửựữvxyýỳỷỵỹz0123456789";
        public const String strT = "A¸µ¶¹·¨¾»¼Æ½©ÊÇÈËÉBCD§EÐÌÎÑÏ£ÕÒÓÖÔFGHIÝ×ØÞÜJKMLNOãßáäâ¥íêëîì¤èåæéçPQRSTUóïñôò¦øõöù÷VXYýúûþüZa¸µ¶¹·¨¾»¼Æ½©ÊÇÈËÉbcd®eÐÌÎÑÏªÕÒÓÖÔfghiÝ×ØÞÜjkmlnoãßáäâ¬íêëîì«èåæéçpqrstuóïñôò­øõöù÷vxyýúûþüz0123456789";

        public static String BoDauTiengViet(String TuGoc)
        {
            String vR = TuGoc;
            int i, cs;

            for (i = 0; i < vR.Length - 1; i++)
            {
                cs = strV.IndexOf(vR[i]);
                if (cs >= 0)
                {
                    vR = vR.Replace(strV[cs], strD[cs]);
                }
            }
            return vR;
        }
        public static String ChuyenUnicodeSangTCVN3(String TuGoc)
        {
            String vR = TuGoc;
            int i, cs;

            for (i = 0; i < vR.Length - 1; i++)
            {
                cs = strV.IndexOf(vR[i]);
                if (cs >= 0)
                {
                    vR = vR.Replace(strV[cs], strT[cs]);
                }
            }
            return vR;
        }

        public static String ChuyenTCVN3SangUnicode(String TuGoc)
        {
            String vR = TuGoc;
            int i, cs;

            for (i = 0; i < vR.Length - 1; i++)
            {
                cs = strT.IndexOf(vR[i]);
                if (cs >= 0)
                {
                    vR = vR.Replace(strT[cs], strV[cs]);
                }
            }
            return vR;
        }

        public static void KhoiTaoDuLieu()
        {
            if (dt != null) dt.Dispose();
            string SQL = "SELECT sTuGoc, sTu FROM DC_DanhMucTu WHERE iID_MaNgonNgu=@iID_MaNgonNgu";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaNgonNgu", MaNgonNgu);
            dt = Connection.GetDataTable(cmd);
            cmd.Dispose();
        }

        public static string LayXau(string TuGoc)
        {
            string vR = "";
            if (MaNgonNgu != "")
            {
                bool ok = false;
                if (dt == null) KhoiTaoDuLieu();
                if (dt == null) return TuGoc;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["sTuGoc"].ToString() == TuGoc)
                    {
                        ok = true;
                        vR = row["sTu"].ToString();
                    }
                }
                if (!ok)
                {
                    vR = TuGoc;
                    if (MaNgonNguGoc == MaNgonNgu)
                    {
                        ThemXau(TuGoc, TuGoc, MaNgonNguGoc);
                        KhoiTaoDuLieu();
                    }
                }
            }
            return vR;
        }

        public static string LayXauChuHoa(string TuGoc)
        {
            return LayXau(TuGoc);
        }

        public static string LayXauKhongDauTiengViet(string TuGoc)
        {
            string vR = LayXau(TuGoc);
            int i, cs;

            for (i = 0; i <= vR.Length - 1; i++)
            {
                cs = strV.IndexOf(vR[i]);
                if (cs >= 0)
                {
                    vR = vR.Replace(strV[cs], strD[cs]);
                }
            }
            return vR;
        }

        public static string LayXauKhongDau_ChuNho(string TuGoc)
        {
            string vR = LayXau(TuGoc);
            int i, cs;

            for (i = 0; i <= vR.Length - 1; i++)
            {
                cs = strV.IndexOf(vR[i]);
                if (cs >= 0)
                {
                    vR = vR.Replace(strV[cs], strF[cs]);
                }
            }
            return vR;
        }

        public static void ThemXau(string TuGoc, string Tu, string MaNgonNgu)
        {
            if (MaNgonNgu != "")
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@sTu", Tu);
                cmd.Parameters.AddWithValue("@sTuGoc", TuGoc);
                cmd.Parameters.AddWithValue("@iID_MaNgonNgu", MaNgonNgu);
                Connection.InsertRecord("DC_DanhMucTu", cmd);
                cmd.Dispose();
            }
        }

        public static String GetValueWebConfig(String Key, Boolean isMapPath)
        {
            String str = string.Empty;
            if (isMapPath == true)
            {
                str = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[Key]);
            }
            else {
                str = System.Configuration.ConfigurationManager.AppSettings[Key];
            }
            return str;
        }

        public static Hashtable GetLanguage(String TableName) {
            Hashtable ht = new Hashtable();
            DataSet ds = new DataSet();

            if (TableName != null && TableName != "") {
                ds.ReadXml(GetValueWebConfig("LanguagePath", true));
                DataTable dt = ds.Tables[TableName];
                DataRow R = dt.Rows[0];
                int i;
                for (i = 0; i < dt.Rows.Count; i++) {
                    ht.Add(dt.Columns[i].ColumnName, R[i]);
                }
            }

            return ht;
        }

        public static Hashtable GetLanguage(String LangCode, String Root, String TableName)
        {
            Hashtable ht = new Hashtable();
            DataSet ds = new DataSet();

            if (TableName != null && TableName != "")
            {
                //ds.ReadXml(Root + "XML/" + LangCode + ".xml");
                ds.ReadXml(Root);
                DataTable dt = ds.Tables[TableName];
                DataRow R = dt.Rows[0];
                int i;
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    ht.Add(dt.Columns[i].ColumnName, R[i]);
                }
            }

            return ht;
        }
    }
}
