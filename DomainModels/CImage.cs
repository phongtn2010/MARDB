using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace DomainModel
{
    public class CImage
    {
        public static int FixWidthThums = 200;
        public static int FixHeightThums = 200;
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

        public static void SaveImage(System.Drawing.Image originalImg, String ImageServerPath,
                                    String Path, String PathThums, String PathRealThums,
                                    ref String FileName, ref String FileNameThums, ref String FileNameRealThums,
                                    ref int Width, ref int Height,
                                    int WidthThums, int HeightThums,
                                    ref int WidthRealThums, ref int HeightRealThums)
        {
            String sDuoiAnh = GetImageFormat_Type(originalImg);

            DateTime TG = DateTime.Now;
            String subPath = TG.ToString("yyyy/MM/dd");
            String subName = TG.ToString("HHmmssfff") + sDuoiAnh;
            String newPath = String.Format("{0}/{1}", Path, subPath);
            String newPathThums = String.Format("{0}/{1}", PathThums, subPath);
            String newRealPathThums = String.Format("{0}/{1}", PathRealThums, subPath);
            String newFN = String.Format("{0}/{1}", newPath, subName);
            String newFNThums = String.Format("{0}/{1}", newPathThums, subName);
            String newFNRealThums = String.Format("{0}/{1}", newRealPathThums, subName);
            CreateDirectory(ImageServerPath + newPath);
            CreateDirectory(ImageServerPath + newPathThums);
            CreateDirectory(ImageServerPath + newRealPathThums);
            Width = -1;
            Height = -1;
            if (WidthThums == -1)
            {
                WidthThums = FixWidthThums;
                HeightThums = FixHeightThums;
            }
            if (WidthRealThums == -1)
            {
                WidthRealThums = FixWidthThums;
                HeightRealThums = FixHeightThums;
            }
            SaveFile(originalImg, ImageServerPath, newFN, ref Width, ref Height);
            SaveFileThums(originalImg, ImageServerPath, newFNThums, WidthThums, HeightThums);
            SaveFile(originalImg, ImageServerPath, newFNRealThums, ref WidthRealThums, ref HeightRealThums);
            FileName = newFN;
            FileNameThums = newFNThums;
            FileNameRealThums = newFNRealThums;
        }

        public static double GetScale(int Width, int Height, int realWidth, int realHeight)
        {
            if (Width > 0 && Height > 0)
            {
                double sX = (double)(realWidth) / Width;
                double sY = (double)(realHeight) / Height;
                return (sX > sY) ? sX : sY;
            }
            return -1;
        }

        private static void SaveFile(System.Drawing.Image originalImg, String ImageServerPath, String FileName, ref int Width, ref int Height)
        {
            double Scale = GetScale(Width, Height, originalImg.Width, originalImg.Height);
            int newWidth, newHeight, x0 = 0, y0 = 0;

            newHeight = originalImg.Height;
            newWidth = originalImg.Width;
            if (Scale > 0)
            {
                newHeight = (int)(originalImg.Height / Scale);
                newWidth = (int)(originalImg.Width / Scale);
            }

            //string logo = Server.MapPath("~/Content/images/logo.png");

            Bitmap img = new Bitmap(newWidth, newHeight);

            //Create a graphics object
            Graphics gr_dest = Graphics.FromImage(img);

            //Re-draw the image to the specified height and width
            gr_dest.DrawImage(originalImg, x0, y0, newWidth, newHeight);

            Width = newWidth;
            Height = newHeight;

            img.Save(ImageServerPath + FileName, ImageFormat.Png);  //ImageFormat.Jpeg
        }

        public static double GetScale1(int Width, int Height, int realWidth, int realHeight)
        {
            if (Width > 0 && Height > 0)
            {
                double sX = (double)(realWidth) / Width;
                double sY = (double)(realHeight) / Height;
                return (sX < sY) ? sX : sY;
            }
            return -1;
        }

        private static void SaveFileThums(System.Drawing.Image originalImg, String ImageServerPath, String FileName, int Width, int Height)
        {
            double Scale = GetScale1(Width, Height, originalImg.Width, originalImg.Height);
            int newWidth, newHeight, x0 = 0, y0 = 0;

            newHeight = originalImg.Height;
            newWidth = originalImg.Width;
            if (Scale > 0)
            {
                newHeight = (int)(originalImg.Height / Scale);
                newWidth = (int)(originalImg.Width / Scale);
            }

            Bitmap img = new Bitmap(Width, Height);

            x0 = (Width - newWidth) / 2;
            y0 = (Height - newHeight) / 2;
            //Create a graphics object
            Graphics gr_dest = Graphics.FromImage(img);
            gr_dest.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //Re-draw the image to the specified height and width
            gr_dest.DrawImage(originalImg, x0, y0, newWidth, newHeight);
            gr_dest.Dispose();

            img.Save(ImageServerPath + FileName, ImageFormat.Png);
        }

        public static ImageFormat GetImageFormat(System.Drawing.Image img)
        {
            if (img.RawFormat.Equals(ImageFormat.Jpeg))
                return ImageFormat.Jpeg;
            if (img.RawFormat.Equals(ImageFormat.Bmp))
                return ImageFormat.Bmp;
            if (img.RawFormat.Equals(ImageFormat.Png))
                return ImageFormat.Png;
            if (img.RawFormat.Equals(ImageFormat.Emf))
                return ImageFormat.Emf;
            if (img.RawFormat.Equals(ImageFormat.Exif))
                return ImageFormat.Exif;
            if (img.RawFormat.Equals(ImageFormat.Gif))
                return ImageFormat.Gif;
            if (img.RawFormat.Equals(ImageFormat.Icon))
                return ImageFormat.Icon;
            if (img.RawFormat.Equals(ImageFormat.MemoryBmp))
                return ImageFormat.MemoryBmp;
            if (img.RawFormat.Equals(ImageFormat.Tiff))
                return ImageFormat.Tiff;
            else
                return ImageFormat.Wmf;
        }

        public static String GetImageFormat_Type(System.Drawing.Image img)
        {
            String vR = "";
            if (img.RawFormat.Equals(ImageFormat.Jpeg))
                vR = ".jpg";
            else if (img.RawFormat.Equals(ImageFormat.Bmp))
                vR = ".bmp";
            else if (img.RawFormat.Equals(ImageFormat.Png))
                vR = ".png";
            else if (img.RawFormat.Equals(ImageFormat.Emf))
                vR = ".emf";
            else if (img.RawFormat.Equals(ImageFormat.Exif))
                vR = ".exif";
            else if (img.RawFormat.Equals(ImageFormat.Gif))
                vR = ".gif";
            else if (img.RawFormat.Equals(ImageFormat.Icon))
                vR = ".icon";
            else if (img.RawFormat.Equals(ImageFormat.MemoryBmp))
                vR = ".MemoryBmp";
            else if (img.RawFormat.Equals(ImageFormat.Tiff))
                vR = ".tiff";
            else
                vR = ".wmf";

            return vR;
        }

        public static void TinhToanKichThuocTuDong(int Width, int Height, int tWidth, int tHeight, ref int iWidth, ref int iHeight)
        {
            if (Width == Height)
            {
                iWidth = tWidth;
                iHeight = tHeight;
            }
            else if (Width < Height)
            {
                double iTyLe = (double)(Height * 1.0 / tHeight * 1.0);

                iWidth = (int)(Width / iTyLe);
                iHeight = tHeight;
            }
            else if (Width > Height)
            {
                double iTyLe = (double)(Width * 1.0 / tWidth * 1.0);

                iWidth = tWidth;
                iHeight = (int)(Height / iTyLe);
            }
        }
    }
}
