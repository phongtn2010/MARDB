using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
using System.Data;

namespace DATA0200026
{
    public class BangModels_HamChung
    {
        public static int LayCS(DataTable dt, String Ma, String TenTruong)
        {
            int csH = -1, k;
            for (k = 0; k < dt.Rows.Count; k++)
            {
                if (Convert.ToString(dt.Rows[k][TenTruong]) == Ma)
                {
                    csH = k;
                    break;
                }
            }
            return csH;
        }

        public static int LayCSH(DataTable dtHang, String MaHang)
        {
            int csH = -1, k;
            for (k = 0; k < dtHang.Rows.Count; k++)
            {
                if (Convert.ToString(dtHang.Rows[k]["iID_MaHang"]) == MaHang)
                {
                    csH = k;
                    break;
                }
            }
            return csH;
        }

        public static int LayCSC(DataTable dtCot, String MaCot)
        {
            int csC = -1, k;
            for (k = 0; k < dtCot.Rows.Count; k++)
            {
                if (Convert.ToString(dtCot.Rows[k]["iID_MaCot"]) == MaCot)
                {
                    csC = k;
                    break;
                }
            }
            return csC;
        }
    }
}