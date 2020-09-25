using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using System.CodeDom.Compiler;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace DomainModel
{
    public static class CString
    {
        public static long IP4ToInt(string addr)
        {
            // careful of sign extension: convert to uint first;
            // unsigned NetworkToHostOrder ought to be provided.
            return (long)(uint)IPAddress.NetworkToHostOrder(
                 (int)IPAddress.Parse(addr).Address);
        }

        public static string IP4ToAddr(long address)
        {
            return IPAddress.Parse(address.ToString()).ToString();
            // This also works:
            // return new IPAddress((uint) IPAddress.HostToNetworkOrder(
            //    (int) address)).ToString();
        }

        public static double IPAddressToNumber(string IPaddress)
        {
            int i;
            string[] arrDec;
            double num = 0;
            if (IPaddress == "")
            {
                return 0;
            }
            else
            {
                arrDec = IPaddress.Split('.');
                for (i = arrDec.Length - 1; i >= 0; i = i - 1)
                {
                    num += ((int.Parse(arrDec[i]) % 256) * Math.Pow(256, (3 - i)));
                }
                return num;
            }
        }

        public static long IP2Long(string ip)
        {
            string[] ipBytes;
            double num = 0;
            if (!string.IsNullOrEmpty(ip))
            {
                ipBytes = ip.Split('.');
                for (int i = ipBytes.Length - 1; i >= 0; i--)
                {
                    num += ((int.Parse(ipBytes[i]) % 256) * Math.Pow(256, (3 - i)));
                }
            }
            return (long)num;
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        public static string GetPublicIP()
        {
            String direction = "";
            WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
            using (WebResponse response = request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                direction = stream.ReadToEnd();
            }

            //Search for the ip in the html
            int first = direction.IndexOf("Address: ") + 9;
            int last = direction.LastIndexOf("</body>");
            direction = direction.Substring(first, last - first);

            return direction;
        }

        public static bool IsFieldExists(DataTable DT, string FieldName)
        {
            bool IsFieldExists = false;
            for (int I = 0; I < DT.Columns.Count; I++)
            {
                if (DT.Columns[I].ColumnName.ToUpper() == FieldName.ToUpper())
                    IsFieldExists = true;
            }
            return IsFieldExists;
        }

        public static DataTable JsonStringToDataTable(string jsonString)
        {
            DataTable dt = new DataTable();
            string[] jsonStringArray = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");
            List<string> ColumnsName = new List<string>();

            foreach (string jSA in jsonStringArray)
            {
                string[] jsonStringData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                foreach (string ColumnsNameData in jsonStringData)
                {
                    try
                    {
                        int idx = ColumnsNameData.IndexOf(":");
                        string ColumnsNameString = ColumnsNameData.Substring(0, idx - 1).Replace("\"", "");
                        if (!ColumnsName.Contains(ColumnsNameString))
                        {
                            ColumnsName.Add(ColumnsNameString);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
                    }
                }
                break;
            }

            foreach (string AddColumnName in ColumnsName)
            {
                dt.Columns.Add(AddColumnName);
            }

            foreach (string jSA in jsonStringArray)
            {
                string[] RowData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                DataRow nr = dt.NewRow();
                foreach (string rowData in RowData)
                {
                    try
                    {
                        int idx = rowData.IndexOf(":");
                        string RowColumns = rowData.Substring(0, idx - 1).Replace("\"", "");
                        string RowDataString = rowData.Substring(idx + 1).Replace("\"", "");
                        nr[RowColumns] = RowDataString;
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                dt.Rows.Add(nr);
            }
            return dt;
        }

        /// <summary>
        /// Remove HTML from string with Regex.
        /// </summary>
        public static string StripTagsRegex(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }

        /// <summary>
        /// ChuanXau
        /// </summary>
        /// <param name="S"></param>
        /// <returns></returns>
        public static String ChuanXau(String S)
        {
            S = S.Replace("  ", " ");
            S = S.Trim();
            return S;
        }
        /// <summary>
        /// Chèn một chuỗi vào chuỗi nguồn (tính từ trái hoặc phải)
        /// </summary>
        /// <param name="input"></param>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <param name="rightToLeft"></param>
        /// <returns></returns>
        public static string Insert_String(string input, string value, int startIndex, bool rightToLeft)
        {
            if (rightToLeft)
            {
                if (startIndex > input.Length)
                {
                    startIndex = 0;
                }
                else
                {
                    startIndex = input.Length - startIndex;
                }
            }
            else
            {
                if (startIndex > input.Length)
                {
                    startIndex = input.Length;
                }
            }
            return input.Insert(startIndex, value);
        }
        /// <summary>
        /// Xóa một chuỗi từ chuỗi nguồn
        /// </summary>
        /// <param name="input"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <param name="rightToLeft"></param>
        /// <returns></returns>
        public static string Remove_String(string input, int startIndex, int count, bool rightToLeft)
        {
            try
            {
                if (rightToLeft)
                {
                    if (startIndex > input.Length)
                        startIndex = 0;
                    else
                        startIndex = input.Length - startIndex - count;
                }
                else
                {
                    if (startIndex > input.Length)
                        startIndex = input.Length;
                }

                startIndex = startIndex < 0 ? 0 : startIndex;

                if (count > input.Length)
                    count = input.Length;
                else
                    count = count < 0 ? 0 : count;

                return input.Remove(startIndex, count);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int DemKyTu(String s, char c)
        {
            int vR = 0, i;
            for (i = 0; i < s.Length; i++)
            {
                if (s[i] == c)
                {
                    vR++;
                }
            }
            return vR;
        }

        public static string strServerURL(String url)
        {
            String vR = "";
            url = url.Replace("://", "###");
            vR = url.Split('/')[0];
            vR = vR.Replace("###", "://");
            return vR;
        }

        private static void ThemHamTruNgay(ref StringBuilder sb)
        {
            //Hàm trừ ngày
            sb.Append("private object TruNgay(string v1, string v2){ \n");
            sb.Append("if (v1 == \"\" || v2 == \"\") return \"\"; \n");
            sb.Append("String[] arr1 = v1.Split(':'); \n");
            sb.Append("String[] arr2 = v2.Split(':'); \n");
            sb.Append("if (arr1.Length < 6 || arr2.Length < 6) return \"\"; \n");
            sb.Append("DateTime d1 = new DateTime(Convert.ToInt16(arr1[0]), Convert.ToInt16(arr1[1]), Convert.ToInt16(arr1[2]), Convert.ToInt16(arr1[3]), Convert.ToInt16(arr1[4]), Convert.ToInt16(arr1[5])); \n");
            sb.Append("DateTime d2 = new DateTime(Convert.ToInt16(arr2[0]), Convert.ToInt16(arr2[1]), Convert.ToInt16(arr2[2]), Convert.ToInt16(arr2[3]), Convert.ToInt16(arr2[4]), Convert.ToInt16(arr2[5])); \n");
            sb.Append("return (d1-d2).TotalDays; \n");
            sb.Append("} \n");

            //sb.Append("Private Function TruNgay(v1 As String, v2 As String) As Object \n");
            //sb.Append("If (v1 = \"\" OR v2 == \"\") Return \"\" \n");
            //sb.Append("Dim arr1 As Array= v1.Split(\":\") \n");
            //sb.Append("Dim arr1 As Array= v2.Split(\":\") \n");
            //sb.Append("Dim d1 As new DateTime(Convert.ToInt16(arr1[0]), Convert.ToInt16(arr1[1]), Convert.ToInt16(arr1[2]), Convert.ToInt16(arr1[3]), Convert.ToInt16(arr1[4]), Convert.ToInt16(arr1[5])) \n");
            //sb.Append("Dim d2 As new DateTime(Convert.ToInt16(arr2[0]), Convert.ToInt16(arr2[1]), Convert.ToInt16(arr2[2]), Convert.ToInt16(arr2[3]), Convert.ToInt16(arr2[4]), Convert.ToInt16(arr2[5])) \n");
            //sb.Append("Return (d1-d2).TotalDays \n");
            //sb.Append("End Function \n");
        }

        private static void ThemHamDoiXau(ref StringBuilder sb)
        {
            sb.Append("private String DoiThanhXau(object v){ \n");
            sb.Append("return Convert.ToString(v); \n");
            sb.Append("} \n");

            //sb.Append("Private Function DoiThanhXau(v As object ) As String  \n");
            //sb.Append("return Convert.ToString(v) \n");
            //sb.Append("End Function \n");
        }

        private static void ThemHamDoiSo(ref StringBuilder sb)
        {
            sb.Append("private Double DoiThanhSo(object v){ \n");
            sb.Append("return Convert.ToDouble(v); \n");
            sb.Append("} \n");

            //sb.Append("Private Function DoiThanhSo(v As object ) As Double  \n");
            //sb.Append("return Convert.ToDouble(v) \n");
            //sb.Append("End Function \n");
        }

        public static Object Evaluate(string sExpression)
        {
            if (sExpression.StartsWith("="))
            {
                sExpression = sExpression.Substring(1);
            }
            String sExpression1 = sExpression.Replace("\"\"", "0");

            //VBCodeProvider vb = new VBCodeProvider();
            CSharpCodeProvider c = new CSharpCodeProvider();
            CompilerParameters cp = new CompilerParameters();

            cp.ReferencedAssemblies.Add("system.dll");

            cp.CompilerOptions = "/t:library";
            cp.GenerateInMemory = true;

            StringBuilder sb = new StringBuilder("");
            sb.Append("using System;\n");

            sb.Append("namespace CSCodeEvaler{ \n");
            sb.Append("public class CSCodeEvaler{ \n");
            //Hàm trừ ngày
            ThemHamTruNgay(ref sb);
            ThemHamDoiXau(ref sb);
            ThemHamDoiSo(ref sb);

            sb.Append("public object EvalCode(){\n");
            sb.Append("return " + sExpression1 + "; \n");
            sb.Append("} \n");
            sb.Append("} \n");
            sb.Append("}\n");

            CompilerResults cr = c.CompileAssemblyFromSource(cp, sb.ToString());
            if (cr.Errors.Count > 0)
            {
                sb = new StringBuilder("");
                sb.Append("using System;\n");

                sb.Append("namespace CSCodeEvaler{ \n");
                sb.Append("public class CSCodeEvaler{ \n");
                //Hàm trừ ngày
                ThemHamTruNgay(ref sb);
                ThemHamDoiXau(ref sb);
                ThemHamDoiSo(ref sb);

                sb.Append("public object EvalCode(){\n");
                sb.Append("return " + sExpression + "; \n");
                sb.Append("} \n");
                sb.Append("} \n");
                sb.Append("}\n");
                cr = c.CompileAssemblyFromSource(cp, sb.ToString());
                if (cr.Errors.Count > 0)
                {
                    throw new InvalidExpressionException(
                        string.Format("Error ({0}) evaluating: {1}",
                        cr.Errors[0].ErrorText, sExpression));
                }
            }

            System.Reflection.Assembly a = cr.CompiledAssembly;
            object o = a.CreateInstance("CSCodeEvaler.CSCodeEvaler");

            Type t = o.GetType();
            MethodInfo mi = t.GetMethod("EvalCode");

            object s = mi.Invoke(o, null);
            return s;
        }

        public static string LayTruongTrongCongThuc(ref String sCongThuc, String sGiaTriDien)
        {
            String vR = "";
            int cs1 = sCongThuc.IndexOf("[");
            int cs2 = sCongThuc.IndexOf("]");
            if (cs1 >= 0 && cs2 > cs1)
            {
                vR = sCongThuc.Substring(cs1, cs2 - cs1 + 1);
            }
            if (vR != "")
            {
                sCongThuc = sCongThuc.Replace(vR, sGiaTriDien);
            }
            return vR;
        }

        public static string SafeString(string str)
        {
            if ((str != string.Empty) && (str != null))
            {
                //str = str.ToLower();

                string[] strArray = new string[] {
                    "select", "drop", ";", "--", "insert", "delete", "xp_", "=", "tables", "colums", "schema.tables", "schema.colums", "where", "null", "cr", "lf", "1=1",
                    "SELECT", "DROP", ";", "--", "INSERT", "DELETE", "XP_", "=", "TABLES", "COLUMS", "SCHEMA.TABLES", "SCHEMA.COLUMS", "WHERE", "NULL", "CR", "LF"
                };

                for (int i = 0; i < strArray.Length; i++)
                {
                    str = str.Replace(strArray[i], string.Empty);
                }
                if (str.IndexOf("'") > -1)
                {
                    str = str.Replace("'", string.Empty);
                }
            }
            return str;
        }

        public static string RequestForm(string StrForm)
        {
            string strRequest = string.Empty;
            if (System.Web.HttpContext.Current.Request.Form[StrForm] != null)
            {
                strRequest = SafeString(System.Web.HttpContext.Current.Request.Form[StrForm].ToString());
            }
            else
            {
                strRequest = null;
            }
            return strRequest;
        }

        public static bool IsValidEmail(string email)
        {
            var r = new Regex(@"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$");

            return !string.IsNullOrEmpty(email) && r.IsMatch(email);
        }

        public static bool IsNumber(string pText)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(pText);
        }

        // Way 2
        public static string convertToUnSign2(string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                System.Globalization.UnicodeCategory uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }
            sb = sb.Replace('Đ', 'D');
            sb = sb.Replace('đ', 'd');
            return (sb.ToString().Normalize(NormalizationForm.FormD));
        }

        // Way 3
        public static string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static string convertToUnSign4(String s)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            string utf8ReturnString = encoder.GetString(bytes);

            return utf8ReturnString;
        }

        public static string DecodeFromUtf8(string utf8String)
        {
            // copy the string as UTF-8 bytes.
            byte[] utf8Bytes = new byte[utf8String.Length];
            for (int i = 0; i < utf8String.Length; ++i)
            {
                //Debug.Assert( 0 <= utf8String[i] && utf8String[i] <= 255, "the char must be in byte's range");
                utf8Bytes[i] = (byte)utf8String[i];
            }

            return Encoding.UTF8.GetString(utf8Bytes, 0, utf8Bytes.Length);
        }

        public static string DecodeFromSMSUnicode(string UnicodeString)
        {
            // copy the string as UTF-8 bytes.
            byte[] UnicodeBytes = new byte[UnicodeString.Length];
            for (int i = 0; i < UnicodeString.Length; ++i)
            {
                //Debug.Assert( 0 <= utf8String[i] && utf8String[i] <= 255, "the char must be in byte's range");
                UnicodeBytes[i] = (byte)UnicodeString[i];
            }

            for (int i = 0; i < UnicodeString.Length; i = i + 2)
            {
                byte tg = UnicodeBytes[i];
                UnicodeBytes[i] = UnicodeBytes[i + 1];
                UnicodeBytes[i + 1] = tg;
            }
            String vR = Encoding.Unicode.GetString(UnicodeBytes);
            return vR;
        }

        public static string UnicodeToASCII(string strMessage)
        {
            strMessage = Regex.Replace(strMessage, "[ÁÀẢẠÃĂẮẰẲẶẴÂẦẤẨẬẪÃÄÅ]", "A");
            strMessage = Regex.Replace(strMessage, "[áàảạãăắằẳặẵâấầẩậẫãäå]", "a");
            strMessage = Regex.Replace(strMessage, "[éèẻẹẽêếềểệễëð]", "e");
            strMessage = Regex.Replace(strMessage, "[ÉÈẺẸẼÊẾỀỂỆỄË]", "E");
            strMessage = Regex.Replace(strMessage, "[íìỉịĩîï]", "i");
            strMessage = Regex.Replace(strMessage, "[ÍÌỈỊĨÎÏ]", "I");
            strMessage = Regex.Replace(strMessage, "[óòỏọõơớờởợỡôốồổộỗöø]", "o");
            strMessage = Regex.Replace(strMessage, "[ÓÒỎỌÕƠỚỜỞỢỠÔỐỒỔỘỖÖØ]", "O");
            strMessage = Regex.Replace(strMessage, "[ÚÙỦỤŨƯỨỪỬỰỮÛÜǼ]", "U");
            strMessage = Regex.Replace(strMessage, "[úùủụũưứừửựữûüµǽ]", "u");
            strMessage = Regex.Replace(strMessage, "[ÝỲỶỴỸŸ]", "Y");
            strMessage = Regex.Replace(strMessage, "[ýỳỷỵỹÿ]", "y");
            strMessage = Regex.Replace(strMessage, "[š]", "s");
            strMessage = Regex.Replace(strMessage, "[Š]", "S");
            strMessage = Regex.Replace(strMessage, "[ñ]", "n");
            strMessage = Regex.Replace(strMessage, "[Ñ]", "N");
            strMessage = Regex.Replace(strMessage, "[ç]", "c");
            strMessage = Regex.Replace(strMessage, "[Ç]", "C");
            strMessage = Regex.Replace(strMessage, "[ž]", "z");
            strMessage = Regex.Replace(strMessage, "[Ž]", "Z");
            strMessage = Regex.Replace(strMessage, "[ÐĐ]", "D");
            strMessage = Regex.Replace(strMessage, "[đ]", "D");
            strMessage = Regex.Replace(strMessage, "[œ]", "oe");
            strMessage = Regex.Replace(strMessage, "[Œ]", "Oe");
            strMessage = Regex.Replace(strMessage, "[«»\u201C\u201D\u201E\u201F\u2033\u2036]", "\"");
            strMessage = Regex.Replace(strMessage, "[\u2026]", "...");
            return strMessage;
        }

        public static string UnicodeUnSign(string s)
        {
            const string uniChars = "àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ";
            const string koDauChars = "aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIOOOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU";

            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            string retVal = String.Empty;
            for (int i = 0; i < s.Length; i++)
            {
                int pos = uniChars.IndexOf(s[i].ToString());
                if (pos >= 0)
                    retVal += koDauChars[pos];
                else
                    retVal += s[i];
            }
            return retVal;
        }

        public static String SMS_XuLyKyTuDacBiet(Object oContent, int data_coding)
        {
            String sContent = "";
            if (String.IsNullOrEmpty(Convert.ToString(oContent)) == false)
            {
                sContent = Convert.ToString(oContent);
                if (data_coding == 8)
                {
                    sContent = DecodeFromSMSUnicode(sContent);
                }
                sContent = sContent.ToUpper();
                sContent = UnicodeToASCII(sContent);
                sContent = sContent.Replace(" ", "");

                if (sContent.IndexOf("\n") >= 0)
                {
                    String[] arrContent = sContent.Split('\n');
                    for (int i = 0; i < arrContent.Length; i++)
                    {
                        String sTGNoiDung = arrContent[i].Trim();
                        if (String.IsNullOrEmpty(sTGNoiDung) == false)
                        {
                            sContent = sTGNoiDung;
                            break;
                        }
                    }
                }
                for (int i = sContent.Length - 1; i >= 0; i--)
                {
                    if (!(('A' <= sContent[i] && sContent[i] <= 'Z') || ('0' <= sContent[i] && sContent[i] <= '9')))
                    {
                        sContent = sContent.Remove(i, 1);
                    }
                }
            }
            if (sContent.Length >= 17)
            {
                sContent = sContent.Substring(0, 17);
            }
            return sContent;
        }

        public static string ThemThe_br_VaoChuoi(string s, int length)
        {
            if (String.IsNullOrEmpty(s))
                throw new ArgumentNullException(s);
            var words = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words[0].Length > length)
                throw new ArgumentException("Từ đầu tiên dài hơn chuỗi cần cắt");
            var sb = new StringBuilder();
            foreach (var word in words)
            {
                if ((sb + word).Length > length)
                {
                    sb.Append("<br/>");
                    length += length;
                }
                sb.Append(word + " ");
            }
            return string.Format("{0}", sb.ToString().TrimEnd(' '));
        }

        public static string CatChuoi_BaCham(this string s, int length)
        {
            var sb = new StringBuilder();

            if (String.IsNullOrEmpty(s))
                return string.Format("{0}", sb.ToString().TrimEnd(' '));
            var words = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words[0].Length > length)
                return string.Format("{0}", sb.ToString().TrimEnd(' '));


            foreach (var word in words)
            {
                if ((sb + word).Length > length)
                    return string.Format("{0}...", sb.ToString().TrimEnd(' '));
                sb.Append(word + " ");
            }
            return string.Format("{0}", sb.ToString().TrimEnd(' '));
        }

        public static string CatChuoi_BaCham_Rieng(this string s, int length)
        {
            var sb = new StringBuilder();

            if (String.IsNullOrEmpty(s))
                return string.Format("{0}", sb.ToString().TrimEnd(' '));
            var words = s.Split(new[] { "::" }, StringSplitOptions.None);
            if (words[0].Length > length)
                return string.Format("{0}", sb.ToString().TrimEnd(' '));


            foreach (var word in words)
            {
                if ((sb + word).Length > length)
                    return string.Format("{0} ...", sb.ToString().TrimEnd(' '));
                
                sb.Append(word + "::");
            }
            return string.Format("{0}", sb.ToString().TrimEnd(' '));
        }


        public static string CatChuoi(this string s, int length)
        {
            if (String.IsNullOrEmpty(s))
                throw new ArgumentNullException(s);
            var words = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words[0].Length > length)
                throw new ArgumentException("Từ đầu tiên dài hơn chuỗi cần cắt");
            var sb = new StringBuilder();
            foreach (var word in words)
            {
                if ((sb + word).Length > length)
                    return string.Format("{0}", sb.ToString().TrimEnd(' '));
                sb.Append(word + "");
            }
            return string.Format("{0}", sb.ToString().TrimEnd(' '));
        }

        public static string CatChuoiKyTuXuongDong(this string s, int length)
        {
            if (String.IsNullOrEmpty(s))
                throw new ArgumentNullException(s);
            var words = s.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (words[0].Length > length)
                throw new ArgumentException("Từ đầu tiên dài hơn chuỗi cần cắt");
            var sb = new StringBuilder();
            foreach (var word in words)
            {
                if ((sb + word).Length > length)
                    return string.Format("{0}", sb.ToString().TrimEnd(' '));
                sb.Append(word + "\n");
            }
            return string.Format("{0}", sb.ToString().TrimEnd(' '));
        }

        public static string ProperCase(String s)
        {
            String vR = "";
            //Get the culture property of the thread.
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            //Create TextInfo object.
            TextInfo textInfo = cultureInfo.TextInfo;
            vR = textInfo.ToTitleCase(s);
            return HttpUtility.HtmlDecode(vR);
        }

        public static string ProperCase_ChuCaiHoaDauDong(String _vR)
        {
            string vR;
            String[] sTemp;
            String sTemp2 = "";
            int lTemp, x;
            _vR = _vR.ToLower();
            if (_vR.Length > 0)
            {
                sTemp = _vR.Split(' ');
                lTemp = sTemp.Length;// sTemp.Length;
                for (x = 0; x < lTemp; x++)
                {
                    if (sTemp[x] != "")
                    {
                        sTemp2 += Convert.ToString(Left(sTemp[x], 1).ToUpper()) + Convert.ToString(Mid(sTemp[x], 1)) + " ";
                    }
                }
                vR = sTemp2;
            }
            else
            {
                vR = _vR;
            }
            return vR;
        }

        public static string Left(string param, int length)
        {
            //we start at 0 since we want to get the characters starting from the
            //left and with the specified lenght and assign it to a variable
            string result = param.Substring(0, length);
            //return the result of the operation
            return result;
        }

        public static string Right(string param, int length)
        {
            //start at the index based on the lenght of the sting minus
            //the specified lenght and assign it a variable
            string result = param.Substring(param.Length - length, length);
            //return the result of the operation
            return result;
        }

        public static string Mid(string param, int startIndex, int length)
        {
            //start at the specified index in the string ang get N number of
            //characters depending on the lenght and assign it to a variable
            string result = param.Substring(startIndex, length);
            //return the result of the operation
            return result;
        }

        public static string Mid(string param, int startIndex)
        {
            //start at the specified index and return all characters after it
            //and assign it to a variable
            string result = param.Substring(startIndex);
            //return the result of the operation
            return result;
        }

        /// <summary>
        /// Lấy ra ngày đầu tiên trong tuần của ngày nhập vào 
        /// với Culture mặc định là Culture hiện tại
        /// </summary>
        /// <param name="dayInWeek">Ngày nhập vào</param>
        /// <returns>Ngày đầu tiên trong tuần</returns>
        public static DateTime GetFirstDayOfWeek(DateTime dayInWeek)
        {
            CultureInfo defaultCultureInfo = CultureInfo.CurrentCulture;
            return GetFirstDayOfWeek(dayInWeek, defaultCultureInfo);
        }
        /// <summary>
        /// Lấy ra ngày đầu tiên trong tuần của ngày nhập vào
        /// với một Culture cụ thể được truyền vào
        /// </summary>
        /// <param name="dayInWeek">Ngày nhập vào</param>
        /// <param name="cultureInfo">CultureInfo quy định các thông tin về Culture 
        /// ( định dạng ngày tháng, ngày bắt đầu trong tuần , ... )
        /// </param>
        /// <returns></returns>
        private static DateTime GetFirstDayOfWeek(DateTime dayInWeek, CultureInfo cultureInfo)
        {
            DayOfWeek firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
            {
                firstDayInWeek = firstDayInWeek.AddDays(-1);
            }
            return firstDayInWeek;
        }
        /// <summary>
        /// Lấy ra ngày đầu tiên trong tuần của ngày nhập vào
        /// với 1 giá trị cụ thể của enum DayOfWeek chỉ định 
        /// ngày bắt đầu tuần là thứ mấy
        /// </summary>
        /// <param name="dayInWeek">Ngày nhập vào</param>
        /// <param name="dayOfWeek">enum chỉ định thứ bắt đầu tuần</param>
        /// <returns></returns>
        private static DateTime GetFirstDayOfWeek(DateTime dayInWeek, DayOfWeek dayOfWeek)
        {
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != dayOfWeek)
            {
                firstDayInWeek = firstDayInWeek.AddDays(-1);
            }
            return firstDayInWeek;
        }
        /// <summary>
        /// Lấy ra ngày đầu tiên trong tháng có chứa 
        /// 1 ngày bất kỳ được truyền vào
        /// </summary>
        /// <param name="dtDate">Ngày nhập vào</param>
        /// <returns>Ngày đầu tiên trong tháng</returns>
        public static DateTime GetFirstDayOfMonth(DateTime dtInput)
        {
            DateTime dtResult = dtInput;
            dtResult = dtResult.AddDays((-dtResult.Day) + 1);
            return dtResult;
        }
        /// <summary>
        /// Lấy ra ngày đầu tiên trong tháng được truyền vào 
        /// là 1 số nguyên từ 1 đến 12
        /// </summary>
        /// <param name="iMonth">Thứ tự của tháng trong năm</param>
        /// <returns>Ngày đầu tiên trong tháng</returns>
        public static DateTime GetFirstDayOfMonth(int iMonth)
        {
            DateTime dtResult = new DateTime(DateTime.Now.Year, iMonth, 1);
            dtResult = dtResult.AddDays((-dtResult.Day) + 1);
            return dtResult;
        }
        /// <summary>
        /// Lấy ra ngày cuối cùng trong tháng có chứa 
        /// 1 ngày bất kỳ được truyền vào
        /// </summary>
        /// <param name="dtInput">Ngày nhập vào</param>
        /// <returns>Ngày cuối cùng trong tháng</returns>
        public static DateTime GetLastDayOfMonth(DateTime dtInput)
        {
            DateTime dtResult = dtInput;
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult;
        }
        /// <summary>
        /// Lấy ra ngày cuối cùng trong tháng được truyền vào
        /// là 1 số nguyên từ 1 đến 12
        /// </summary>
        /// <param name="iMonth"></param>
        /// <returns></returns>
        public static DateTime GetLastDayOfMonth(int iMonth)
        {
            DateTime dtResult = new DateTime(DateTime.Now.Year, iMonth, 1);
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult;
        }

        //Add css
        public static void AddCss(string path, Page page)
        {
            Literal cssFile = new Literal() { Text = @"<link href=""" + page.ResolveUrl(path) + @""" type=""text/css"" rel=""stylesheet"" />" };
            page.Header.Controls.Add(cssFile);
        }

        //Add js
        public static void AddJs(string path, Page page)
        {
            LiteralControl jsFile = new LiteralControl("<script type='text/javascript' src='" + path + "'></script>");
            page.Header.Controls.Add(jsFile);
        }

        //Lấy số ngẫu nhiên
        public static int RandomNumber(int min, int max)
        {
            Random random = new Random(); return random.Next(1, 1000);
        }

        //Lấy ngày giờ tại Server
        public static DateTime GetServerDateTime()
        {
            return DateTime.Now.ToUniversalTime().AddHours(7);
        }

        //Mã hóa thành chuỗi MD5
        public static string EncryptMD5(string sToEncrypt)
        {
            System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
            byte[] bytes = ue.GetBytes(sToEncrypt);

            // encrypt bytes
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashBytes = md5.ComputeHash(bytes);

            // Convert the encrypted bytes back to a string (base 16)
            string hashString = "";

            for (int i = 0; i < hashBytes.Length; i++)
            {
                hashString += Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
            }

            return hashString.PadLeft(32, '0');
        }

        //Chuẩn hóa chuỗi ký tự số
        public static string StandardNumber(string num)
        {
            num = num.Replace("`", "");
            num = num.Replace("-", "");
            num = num.Replace("=", "");
            num = num.Replace("~", "");
            num = num.Replace("!", "");
            num = num.Replace("@", "");
            num = num.Replace("#", "");
            num = num.Replace("$", "");
            num = num.Replace("%", "");
            num = num.Replace("^", "");
            num = num.Replace("&", "");
            num = num.Replace("*", "");
            num = num.Replace("(", "");
            num = num.Replace(")", "");
            num = num.Replace("_", "");
            num = num.Replace("+", "");
            num = num.Replace("[", "");
            num = num.Replace("]", "");
            num = num.Replace(";", "");
            num = num.Replace("'", "");
            num = num.Replace(",", "");
            num = num.Replace(".", "");
            num = num.Replace("/", "");
            num = num.Replace("{", "");
            num = num.Replace("}", "");
            num = num.Replace(":", "");
            num = num.Replace("<", "");
            num = num.Replace(">", "");
            num = num.Replace("?", "");
            return num;
        }

        //Chuẩn hóa chuỗi ký tự bảo mật
        public static string StandardString(string sContent)
        {
            sContent = sContent.Trim();
            sContent = sContent.Replace("<", "");
            sContent = sContent.Replace(">", "");
            sContent = sContent.Replace("crack", "*****");
            sContent = sContent.Replace("key", "***");
            sContent = sContent.Replace("<td>", "");
            sContent = sContent.Replace("</td>", "");
            sContent = sContent.Replace("<tr>", "");
            sContent = sContent.Replace("</tr>", "");
            sContent = sContent.Replace("<table>", "");
            sContent = sContent.Replace("</table>", "");
            sContent = sContent.Replace("<script", "");
            sContent = sContent.Replace("</script>", "");

            sContent = sContent.Replace("OR", "");
            sContent = sContent.Replace("ALTER", "");
            sContent = sContent.Replace("DROP", "");
            return sContent;
        }

        //Thay thế chữ cái có dấu thành không dấu
        public static string RemapInternationalCharToAscii(char c)
        {
            string s = c.ToString();
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        //Ghi lại Url thân thiện
        public static string RewriteUrlFriendly(string title)
        {
            if (title == null) return "";

            const int maxlen = 80;
            int len = title.Length;
            bool prevdash = false;
            var sb = new StringBuilder(len);
            char c;

            for (int i = 0; i < len; i++)
            {
                c = title[i];
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                {
                    sb.Append(c);
                    prevdash = false;
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    // tricky way to convert to lowercase
                    sb.Append((char)(c | 64));
                    prevdash = false;
                }
                else if (c == ' ' || c == ',' || c == '.' || c == '/' ||
                    c == '\\' || c == '-' || c == '_' || c == '=')
                {
                    if (!prevdash && sb.Length > 0)
                    {
                        sb.Append('-');
                        prevdash = true;
                    }
                }
                else if ((int)c >= 128)
                {
                    int prevlen = sb.Length;
                    sb.Append(RemapInternationalCharToAscii(c));
                    if (prevlen != sb.Length) prevdash = false;
                }
                if (i == maxlen) break;
            }

            if (prevdash)
                return sb.ToString().Substring(0, sb.Length - 1);
            else
                return sb.ToString();
        }

        //Viết lại chuỗi với ký tự in hoa đầu tiên
        public static string UpcaseFistChar(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        //Kiểm tra chuỗi chỉ chứa ký tự số int64
        public static bool IsNumericInt64(string num)
        {
            long n;
            return Int64.TryParse(num, out n);
        }

        //Kiểm tra chuỗi chỉ chứa ký tự số int32
        public static bool IsNumericInt32(string num)
        {
            int n;
            return Int32.TryParse(num, out n);
        }

        //Phát hiện và gắn Url trong chuỗi ký tự
        public static string DetectUrlText(string text)
        {
            var textDetect = Regex.Replace(text, @"((http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)", "<a target='_blank' href='$1'>$1</a>");
            return textDetect;
        }

        public static string StringToMoney(string input)
        {
            string str1 = "";
            string str2;
            if (input.Length > 3)
            {
                int num = input.Length % 3;
                if (num > 0)
                {
                    for (string str3 = input.Substring(num, input.Length - num); str3.Length / 3 > 0; str3 = str3.Substring(3, str3.Length - 3))
                        str1 = str1 + "," + str3.Substring(0, 3);
                    str2 = input.Substring(0, num) + str1;
                }
                else
                {
                    for (string str3 = input; str3.Length / 3 > 0; str3 = str3.Substring(3, str3.Length - 3))
                        str1 = str1 + "," + str3.Substring(0, 3);
                    str2 = str1.Substring(1, str1.Length - 1);
                }
            }
            else
                str2 = input;
            return str2;
        }

        public static string left(string inString, int inInt)
        {
            if (inInt > inString.Length)
                inInt = inString.Length;
            return inString.Substring(0, inInt);
        }

        public static string makeNiceDate(DateTime dDate)
        {
            return string.Format("{0:dd/MM/yy}", (object)dDate).Replace("/", "-");
        }

        public static string CheckBit(bool bstr)
        {
            return !bstr ? "No" : "Yes";
        }

        public static string testtom(int _data)
        {
            string str = "";
            if (_data > 1)
                str = Convert.ToString(_data);
            return str;
        }

        public static string land(string _data)
        {
            string str = "N/A";
            if (_data != "Country unknown")
            {
                str = _data;
                if (str == "United States of America")
                    str = "USA";
            }
            return str;
        }

        public static string DataExist(string _data)
        {
            return !(_data != "") ? "N/A" : _data;
        }

        public static string borndied(string born, string died)
        {
            string str = "";
            if (born == "0")
                born = "";
            if (died == "0")
                died = "";
            if (born != "" || died != "")
                str = " (" + born + "-" + died + ")";
            return str;
        }

        public static string navn(string fn, string mn, string ln)
        {
            string s = "";
            if (fn == "" && mn == "")
                s = ln;
            if (fn != "")
                s = ln + ", " + fn;
            if (mn != "")
                s = ln + ", " + fn + " " + mn;
            return HttpUtility.HtmlDecode(s);
        }

        public static string trimaartal(string _data)
        {
            _data = _data.Replace("1400 - ?", "15 <sup>th</sup> c.");
            _data = _data.Replace("1400 - 1500", "15 <sup>th</sup> c.");
            _data = _data.Replace("1500 - ?", "16 <sup>th</sup> c.");
            _data = _data.Replace("1500 - 1600", "16 <sup>th</sup> c.");
            _data = _data.Replace("1600 - ?", "17 <sup>th</sup> c.");
            _data = _data.Replace("1600 - 1700", "17 <sup>th</sup> c.");
            _data = _data.Replace("1700 - ?", "18 <sup>th</sup> c.");
            _data = _data.Replace("1700 - 1800", "18 <sup>th</sup> c.");
            _data = _data.Replace("1800 - ?", "19 <sup>th</sup> c.");
            _data = _data.Replace("1800 - 1900", "19 <sup>th</sup> c.");
            _data = _data.Replace("1900 - ?", "20 <sup>th</sup> c.");
            _data = _data.Replace("1900 - 2000", "20 <sup>th</sup> c.");
            return _data;
        }

        public static string changeletters(string s)
        {
            return s.Replace("o", "[¤1¤]").Replace("ö", "[¤2¤]").Replace("u", "[¤3¤]").Replace("ü", "[¤4¤]").Replace("y", "[¤5¤]").Replace("ÿ", "[¤6¤]").Replace("ø", "[¤7¤]").Replace("å", "[¤8¤]").Replace("e", "[¤9¤]").Replace("é", "[¤10¤]").Replace("a", "[¤11¤]").Replace("ä", "[¤12¤]").Replace("ó", "[¤13¤]").Replace("è", "[¤14¤]").Replace("v", "[¤15¤]").Replace("w", "[¤16¤]").Replace("c", "[¤17¤]").Replace("ç", "[¤18¤]").Replace("i", "[¤19¤]").Replace("+", "[¤20¤]").Replace("[¤1¤]", "[öoæøåóòõ]").Replace("[¤2¤]", "[öoæøåóòõ]").Replace("[¤3¤]", "[uüyÿ]").Replace("[¤4¤]", "[üuyÿ]").Replace("[¤5¤]", "[yÿ]").Replace("[¤6¤]", "[ÿy]").Replace("[¤7¤]", "[øoö]").Replace("[¤8¤]", "[åoöaä]").Replace("[¤9¤]", "[eéèêë]").Replace("[¤10¤]", "[eéèêë]").Replace("[¤11¤]", "[aäâàá]").Replace("[¤12¤]", "[aäâàá]").Replace("[¤13¤]", "[öoæøåóò]").Replace("[¤14¤]", "[eéèêë]").Replace("[¤15¤]", "[vw]").Replace("[¤16¤]", "[vw]").Replace("[¤17¤]", "[cç]").Replace("[¤18¤]", "[cç]").Replace("[¤19¤]", "[iíìîÏ]").Replace("[¤20¤]", "[a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z]");
        }

        public static List<T> ConvertDataTable<T>(this DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
    }

    public static class StringExtensions
    {
        /// <summary>
        /// Trả về phần đầu của chuỗi và đảm bảo đủ từ
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length">Số ký tự tối đa được cắt</param>
        /// <returns>String</returns>
        public static string CatChuoi(this string s, int length)
        {
            if (String.IsNullOrEmpty(s))
                throw new ArgumentNullException(s);
            var words = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words[0].Length > length)
                throw new ArgumentException("Từ đầu tiên dài hơn chuỗi cần cắt");
            var sb = new StringBuilder();
            foreach (var word in words)
            {
                if ((sb + word).Length > length)
                    return string.Format("{0}", sb.ToString().TrimEnd(' '));
                sb.Append(word + " ");
            }
            return string.Format("{0}", sb.ToString().TrimEnd(' '));
        }
        /// <summary>
        /// CatChuoiKyTuXuongDong
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string CatChuoiKyTuXuongDong(this string s, int length)
        {
            if (String.IsNullOrEmpty(s))
                throw new ArgumentNullException(s);
            var words = s.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (words[0].Length > length)
                throw new ArgumentException("Từ đầu tiên dài hơn chuỗi cần cắt");
            var sb = new StringBuilder();
            foreach (var word in words)
            {
                if ((sb + word).Length > length)
                    return string.Format("{0}", sb.ToString().TrimEnd('\n'));
                sb.Append(word + " ");
            }
            return string.Format("{0}", sb.ToString().TrimEnd('\n'));
        }
    }

    public static class VietString
    {
        // Fields
        private static String[] pattern = null;
        private static Char[] replaceChar = null;

        // Contructors
        static VietString()
        {
            pattern = new String[7];
            replaceChar = new Char[14];
            // Khởi tạo giá trị thay thế
            replaceChar[0] = 'a';
            replaceChar[1] = 'd';
            replaceChar[2] = 'e';
            replaceChar[3] = 'i';
            replaceChar[4] = 'o';
            replaceChar[5] = 'u';
            replaceChar[6] = 'y';
            replaceChar[7] = 'A';
            replaceChar[8] = 'D';
            replaceChar[9] = 'E';
            replaceChar[10] = 'I';
            replaceChar[11] = 'O';
            replaceChar[12] = 'U';
            replaceChar[13] = 'Y';
            //Mẫu cần thay thế tương ứng
            pattern[0] = "(á|à|ả|ã|ạ|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ)"; //letter a
            pattern[1] = "đ";   //letter d
            pattern[2] = "(é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ)"; //letter e
            pattern[3] = "(í|ì|ỉ|ĩ|ị)"; //letter i
            pattern[4] = "(ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ)"; //letter o
            pattern[5] = "(ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự)"; //letter u
            pattern[6] = "(ý|ỳ|ỷ|ỹ|ỵ)"; //letter y
        }

        /// <summary>
        /// Chuyển tiếng việt có dấu thành không dấu
        /// </summary>
        /// <param name="text">Chuỗi tiếng việt có dấu</param>
        /// <returns>Chuỗi tiếng việt không dấu</returns>
        //public unsafe static string RejectMarks(string text)
        //{
        //    fixed (Char* ptrChar = replaceChar)
        //    {
        //        for (int i = 0; i < pattern.Length; i++)
        //        {
        //            MatchCollection matchs = Regex.Matches(text, pattern[i], RegexOptions.IgnoreCase);
        //            foreach (Match m in matchs)
        //            {
        //                Char ch = Char.IsLower(m.Value[0]) ? *(ptrChar + i) : *(ptrChar + i + 7);
        //                text = text.Replace(m.Value[0], ch);
        //            }
        //        }
        //    }
        //    return text;
        //}

        //private static void Main()
        //{
        //    string test = VietString.RejectMarks("Chương trình đềmô chuyển tiếng việt có dấu về dạng không dấu sử dụng Regex");
        //    Console.WriteLine(test);
        //    Console.ReadLine();
        //}
    }
}
