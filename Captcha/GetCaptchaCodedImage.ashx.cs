using System;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Collections.Generic;
using NewApp.Models;

namespace NewApp.Captcha
{
    /// <summary>
    /// Summary description for GetCaptchaCodedImage
    /// </summary>
    public class GetCaptchaCodedImage : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string strCaptchaCode = context.Session["CaptchaCode"].ToString();
            if (string.IsNullOrEmpty(strCaptchaCode))
            {
                context.Response.End();
            }

            Font objFont = new Font("Consolas", 30, FontStyle.Regular);
            Bitmap objBmp = new Bitmap(1, 1);
            Graphics objGraphics = Graphics.FromImage(objBmp);
            int width = (int)objGraphics.MeasureString(strCaptchaCode, objFont).Width;
            int height = (int)objGraphics.MeasureString(strCaptchaCode, objFont).Height;
            objBmp = new Bitmap(objBmp, width, height);
            objGraphics = Graphics.FromImage(objBmp);
            objGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            objGraphics.Clear(Color.Transparent);
            objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            objGraphics.DrawString(strCaptchaCode, objFont, Brushes.Black, 3, 3);
            Random rnd = new Random();
            int x;
            int y;
            int i;
            for (i = 1; i <= 200; i++)
            {
                x = rnd.Next(objBmp.Width);
                y = rnd.Next(objBmp.Height);
                objBmp.SetPixel(x, y, Color.Black);
            }

            objGraphics.DrawLine(Pens.Black, 0, rnd.Next(objBmp.Height), objBmp.Width, rnd.Next(objBmp.Height));
            objGraphics.DrawLine(Pens.Black, 0, rnd.Next(objBmp.Height), objBmp.Width, rnd.Next(objBmp.Height));
            MemoryStream ms = new MemoryStream();
            objBmp.Save(ms, ImageFormat.Png);
            byte[] bmpBytes = ms.GetBuffer();
            context.Response.ContentType = "image/png";
            context.Response.BinaryWrite(bmpBytes);
            objBmp.Dispose();
            objFont.Dispose();
            objGraphics.Dispose();
            ms.Close();
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}