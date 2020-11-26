using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace DomainModel
{
    public class Globals
    {
        #region "Biến"
        public static Boolean KieuSoVietNam = false;
        public static string URL_Path="";
        public static string Physical_Path = "";
        public static int PageSize= 10;
        public static int PageRange = 2;
        public static bool CoBanDo = true;
        public static Boolean CoHoiTruocKhiXoa = true;

        public static string api_bantinxml25 = "";
        public static string api_bantinxml26 = "";
        #endregion

        public static int NewGuid()
        {
            int vR = 0;
            String SQL = "Select Cast(Cast(newid() AS binary(3)) AS int) as NextID";
            vR = Convert.ToInt32(Connection.GetValue(SQL, 0));
            return vR;
        }

        private static Regex isGuid = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);

        public static bool IsGuid(string candidate)
        {
            bool isValid = false;

            if (candidate != null)
            {
                if (isGuid.IsMatch(candidate))
                {
                    isValid = true;
                }
            }
            return isValid;

        }

        public static Guid ParseGuid(string candidate)
        {
            if (candidate != null)
            {
                if (isGuid.IsMatch(candidate))
                {
                    return new Guid(candidate);
                }
            }
            return Guid.Empty;
        }

        public static Guid getNewGuid()
        {
            String SQL = "SELECT newid()";
            return (Guid)(Connection.GetValue(SQL, ""));
        }

        public static bool IsNumeric(object ValueToCheck)
        {
            double Dummy = 0;
            return double.TryParse(Convert.ToString(ValueToCheck), System.Globalization.NumberStyles.Any, null, out Dummy);
        }
    }
}
