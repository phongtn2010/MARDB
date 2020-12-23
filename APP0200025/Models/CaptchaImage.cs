using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace APP0200025.Models
{
    public class CaptchaImage
    {
        private FontFamily fontFamily;
        private int height;
        private Bitmap image;
        private Random random;
        private string text;
        private int width;

        public CaptchaImage(int width, int height, FontFamily fontFamily)
        {
            this.random = new Random();
            this.width = width;
            this.height = height;
            this.fontFamily = fontFamily;
        }

        public CaptchaImage(string s, int width, int height, FontFamily fontFamily)
        {
            this.random = new Random();
            this.text = s;
            this.width = width;
            this.height = height;
            this.fontFamily = fontFamily;
        }

        public string CreateRandomText(int Length)
        {
            string str = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ1234567890";
            char[] chArray = new char[Length];
            Random random = new Random();
            for (int i = 0; i < Length; i++)
            {
                chArray[i] = str[random.Next(0, str.Length)];
            }
            return new string(chArray);
        }

        public void GenerateImage()
        {
            SizeF ef;
            Font font;
            Bitmap image = new Bitmap(this.width, this.height, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(image);
            graphics.PageUnit = GraphicsUnit.Pixel;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, this.width, this.height);
            HatchBrush brush = new HatchBrush(HatchStyle.Shingle, Color.LightGray, Color.White);
            graphics.FillRectangle(brush, rect);
            float emSize = rect.Height + 1;
            do
            {
                emSize--;
                font = new Font(this.fontFamily.Name, emSize, GraphicsUnit.Pixel);
                ef = graphics.MeasureString(this.text, font);
            }
            while (ef.Width > rect.Width);
            StringFormat format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            GraphicsPath path = new GraphicsPath();
            path.AddString(this.text, font.FontFamily, (int)font.Style, font.Size, rect, format);
            float num2 = 4f;
            PointF[] destPoints = new PointF[] { new PointF(((float)this.random.Next(rect.Width)) / num2, ((float)this.random.Next(rect.Height)) / num2), new PointF(rect.Width - (((float)this.random.Next(rect.Width)) / num2), ((float)this.random.Next(rect.Height)) / num2), new PointF(((float)this.random.Next(rect.Width)) / num2, rect.Height - (((float)this.random.Next(rect.Height)) / num2)), new PointF(rect.Width - (((float)this.random.Next(rect.Width)) / num2), rect.Height - (((float)this.random.Next(rect.Height)) / num2)) };
            Matrix matrix = new Matrix();
            matrix.Translate(0f, 0f);
            path.Warp(destPoints, rect, matrix, WarpMode.Perspective, 0f);
            brush = new HatchBrush(HatchStyle.Shingle, Color.Black, Color.Black);
            graphics.FillPath(brush, path);
            int num3 = Math.Max(rect.Width, rect.Height);
            for (int i = 0; i < ((int)(((float)(rect.Width * rect.Height)) / 30f)); i++)
            {
                int x = this.random.Next(rect.Width);
                int y = this.random.Next(rect.Height);
                int width = this.random.Next(num3 / 50);
                int height = this.random.Next(num3 / 50);
                graphics.FillEllipse(brush, x, y, width, height);
            }
            font.Dispose();
            brush.Dispose();
            graphics.Dispose();
            this.image = image;
        }

        public void SetText(string text)
        {
            this.text = text;
        }

        public Bitmap Image
        {
            get
            {
                return this.image;
            }
        }
    }
}