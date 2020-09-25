using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web;
using System.Drawing;
namespace DomainModel
{
    public class CFiles
    {
        public static Boolean CreateDirectory(String Dir)
        {
            if (Directory.Exists(Dir) == false)
            {
                try
                {
                    Directory.CreateDirectory(Dir);
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return false;
        }

        public static void SaveFileUpload(String ServerPath, String Path, HttpPostedFileBase file, ref String FileName)
        {
            DateTime TG = DateTime.Now;
            String subPath = TG.ToString("yyyy/MM/dd");
            String subName = TG.ToString("HHmmssfff") + "_" + CString.convertToUnSign3(file.FileName.Replace("-","")).Replace(" ","_");
            String newPath = String.Format("{0}/{1}", Path, subPath);
            String newFN = String.Format("{0}/{1}", newPath, subName);
            CreateDirectory(ServerPath + newPath);
            file.SaveAs(ServerPath + newFN);
            FileName = newFN;
        }

        public static void SaveFileUploadNoFolder(String ServerPath, String Path, HttpPostedFileBase file, ref String FileName)
        {
            DateTime TG = DateTime.Now;
            String subPath = TG.ToString("yyyyMMdd");
            String subName = CString.convertToUnSign3(file.FileName.Replace("-", "")).Replace(" ", "_");
            String newPath = String.Format("{0}/{1}", Path, subPath);
            String newFN = String.Format("{0}/{1}", newPath, subName);
            CreateDirectory(ServerPath + newPath);
            file.SaveAs(ServerPath + newFN);
            FileName = newFN;
        }
        /// <summary>
        /// Upload file qua FTP
        /// </summary>
        /// <param name="sLink">ftp://125.212.225.66/</param>
        /// <param name="sUser">tivi247_ftp</param>
        /// <param name="sPass">No90@thing</param>
        /// <param name="ServerPath"></param>
        /// <param name="Path"></param>
        /// <param name="file"></param>
        /// <param name="FileName"></param>
        public static void SaveFileUpload_Ftp(String sLink, String sUser, String sPass, String ServerPath, String Path, HttpPostedFileBase file, ref String FileName)
        {
            DateTime TG = DateTime.Now;
            String subPath = TG.ToString("yyyy/MM/dd");
            String subName = TG.ToString("HHmmssfff") + "_" + CString.convertToUnSign3(file.FileName.Replace("-", "")).Replace(" ", "_");
            String newPath = String.Format("{0}/{1}", Path, subPath);
            String newFN = String.Format("{0}/{1}", newPath, subName);

            CreateDirectory(ServerPath + newPath);

            ftp ftpClient = new ftp(@"" + sLink + "", sUser, sPass);

            //ftp ftpClient = new ftp(@"ftp://125.212.225.66/", "tivi247_ftp", "No90@thing");

            ftpClient.upload("Video", @"D:\TEST\Nobita.mp4");

            ftpClient = null;
            //file.SaveAs(ServerPath + newFN);
            FileName = newFN;
        }
    }
}
