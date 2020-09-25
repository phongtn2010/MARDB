using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public static class CLog
    {
        public static Boolean LuuCSDL = true;
        public static int iKetNoi_Log = 0;
        public static int Log_iServiceID = 0;
        public static String Log_FileName = "";
        public static String Log_sIP = "";
        private static int Log_nRow = 0;
        private static String Log_FileName_Ex = DateTime.Now.ToString("yyyyMMdd_HHmm");
        public static void WriteLog(String sMsg, int iLoaiLoi, int iTemp=0)
        {
            if (String.IsNullOrEmpty(Log_FileName) == false)
            {
                try
                {
                    Log_nRow++;
                    if(Log_nRow > 5000)
                    {
                        Log_FileName_Ex = DateTime.Now.ToString("yyyyMMdd_HHmm");
                        Log_nRow = 0;
                    }
                    String strFileName = String.Format(Log_FileName, Log_FileName_Ex);
                    StreamWriter sw = null;
                    sw = new StreamWriter(strFileName, true);
                    sw.WriteLine(DateTime.Now.ToString("dd/MM HH:mm:ss.fff") + ":" + sMsg);
                    sw.Flush();
                    sw.Close();

                    if (LuuCSDL)
                    {
                        SqlCommand cmd = new SqlCommand("AddLog");
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@iServiceID", Log_iServiceID);
                        cmd.Parameters.AddWithValue("@sIP", Log_sIP);
                        cmd.Parameters.AddWithValue("@sMsg", sMsg);
                        cmd.Parameters.AddWithValue("@iLoaiLoi", iLoaiLoi);
                        cmd.Parameters.AddWithValue("@iTemp", iTemp);
                        Connection.UpdateDatabase(cmd, iKetNoi_Log);
                        cmd.Dispose();
                    }
                }
                catch
                {

                }
                
            }
        }
    }
}
