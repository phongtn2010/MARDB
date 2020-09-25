using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel
{
    public class CDate
    {
        private static String _strXauChu = "dDmMyYhHsS";
        public static object TruNgay(string v1, string v2)
        {
            if (v1 == "" || v2 == "")
            {
                return "";
            }
            String[] arr1 = v1.Split(':');
            String[] arr2 = v2.Split(':');
            DateTime d1 = new DateTime(Convert.ToInt16(arr1[0]), Convert.ToInt16(arr1[1]), Convert.ToInt16(arr1[2]), Convert.ToInt16(arr1[3]), Convert.ToInt16(arr1[4]), Convert.ToInt16(arr1[5]));
            DateTime d2 = new DateTime(Convert.ToInt16(arr2[0]), Convert.ToInt16(arr2[1]), Convert.ToInt16(arr2[2]), Convert.ToInt16(arr2[3]), Convert.ToInt16(arr2[4]), Convert.ToInt16(arr2[5]));
            return (d1 - d2).TotalDays;
        }

        public static int LaySoNgayTrongThang(DateTime ThoiGian)
        {
            int vR = 0;
            int Thang = ThoiGian.Month;
            while (ThoiGian.Month == Thang)
            {
                vR++;
                ThoiGian = ThoiGian.AddDays(1);
            }
            return vR;
        }

        public static object LayNgayTuXau(string strValue, string strFormat)
        {
            Object vR = null;
            strValue = strValue.Trim();
            if(String.IsNullOrEmpty(strValue))
            {
                return null;
            }
            int i, Ngay = -1, Thang = -1, Nam = -1, Gio = -1, Phut = -1, Giay = -1;

            if (strFormat.IndexOf('y') == -1 && strFormat.IndexOf('Y') == -1)
            {
                Nam = 0;
            }
            if (strFormat.IndexOf('M') == -1)
            {
                Thang = 1;
            }
            if (strFormat.IndexOf('d') == -1 && strFormat.IndexOf('D') == -1)
            {
                Ngay = 1;
            }
            if (strFormat.IndexOf('h') == -1 && strFormat.IndexOf('H') == -1)
            {
                Gio = 0;
            }
            if (strFormat.IndexOf('m') == -1)
            {
                Phut = 0;
            }
            if (strFormat.IndexOf('s') == -1 && strFormat.IndexOf('S') == -1)
            {
                Giay = 0;
            }

            String strTG1 = "", strTG2;
            int GT = 0, j, cs0 = -1;
            for (i = 0; i < strFormat.Length; i++)
            {
                if (_strXauChu.IndexOf(strFormat[i]) >= 0)
                {
                    strTG1 += Convert.ToString(strFormat[i]);
                }
                if (_strXauChu.IndexOf(strFormat[i]) == -1 || i == strFormat.Length - 1)
                {
                    GT = 0;
                    strTG2 = "";
                    for (j = cs0 + 1; j < strValue.Length; j++)
                    {
                        if (strFormat[i] == strValue[j])
                        {
                            cs0 = j;
                            break;
                        }
                        else
                        {
                            strTG2 += strValue[j];
                        }
                    }
                    if (strTG2 != "")
                    {
                        GT = Convert.ToInt32(strTG2);
                    }
                    switch (strTG1)
                    {
                        case "dd":
                        case "d":
                            if (GT >= 0)
                            {
                                Ngay = GT;
                            }
                            break;
                        case "MM":
                        case "M":
                            if (GT >= 0)
                            {
                                Thang = GT;
                            }
                            break;
                        case "y":
                        case "yy":
                        case "yyyy":
                            if (GT >= 0)
                            {
                                Nam = GT;
                                if (Nam < 30)
                                {
                                    Nam = Nam + 2000;
                                }
                                else if (Nam < 100)
                                {
                                    Nam = Nam + 1900;
                                }
                            }
                            break;
                        case "HH":
                        case "H":
                            if (GT >= 0)
                            {
                                Gio = GT;
                            }
                            break;
                        case "mm":
                        case "m":
                            if (GT >= 0)
                            {
                                Phut = GT;
                            }
                            break;
                        case "ss":
                        case "s":
                            if (GT >= 0)
                            {
                                Giay = GT;
                            }
                            break;
                    }
                    strTG1 = "";
                }
            }
            if (Nam > 0 && Thang > 0 && Ngay > 0 && Gio >= 0 && Phut >= 0 && Giay >= 0)
            {
                vR = new DateTime(Nam, Thang, Ngay, Gio, Phut, Giay);
            }
            return vR;
        }

        #region DateTime
        public static string ConvertDateTime(string datetime)
        {
            return System.DateTime.Parse(datetime).ToString("'Ngày' dd 'tháng' MM 'năm' yyyy '('HH 'giờ' mm 'phút' ss 'giây)'");
        }
        public static string ConvertShortDateTime(string datetime)
        {
            return System.DateTime.Parse(datetime).ToString("dd '/' MM '/' yyyy ', 'HH ':' mm ' GMT+7 '");
        }
        public static string FormatDateTimeVN(string datetime)
        {
            return System.DateTime.Parse(datetime).ToString("dd '/' MM '/' yyyy");
        }
        public static string FormatDateTimeEN(string datetime)
        {
            return System.DateTime.Parse(datetime).ToString("MM '/' dd '/' yyyy");
        }
        public static string FullTime()
        {
            string myFullTime = string.Empty;
            myFullTime = System.DateTime.Now.Year.ToString() + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Day.ToString() + System.DateTime.Now.Hour.ToString() + System.DateTime.Now.Minute.ToString() + System.DateTime.Now.Millisecond.ToString();
            return myFullTime;
        }
        public static string FullDateTimeVN(System.DateTime datetime)
        {
            string strCurDT = ConvertShortDateTime(System.DateTime.Now.ToString());
            string mydt = datetime.DayOfWeek.ToString().Substring(0, 3);
            string Output = "";
            switch (mydt)
            {
                case "Mon":
                    Output = "Thứ hai, " + strCurDT;
                    break;
                case "Tue":
                    Output = "Thứ ba, " + strCurDT;
                    break;
                case "Wed":
                    Output = "Thứ tư, " + strCurDT;
                    break;
                case "Thu":
                    Output = "Thứ năm, " + strCurDT;
                    break;
                case "Fri":
                    Output = "Thứ sáu, " + strCurDT;
                    break;
                case "Sat":
                    Output = "Thứ bảy, " + strCurDT;
                    break;
                case "Sun":
                    Output = "Chủ nhật, " + strCurDT;
                    break;

            }
            return Output;
        }
        #endregion DateTime
    }
}
