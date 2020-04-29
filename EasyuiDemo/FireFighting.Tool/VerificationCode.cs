
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace FireFighting.Tool
{
    public class VerificationCode
    {
        public MemoryStream CreateCheckCodeImage(string CheckCode)
        {
            if (string.IsNullOrEmpty(CheckCode))
            {
                return null;
            }
            Bitmap img = new Bitmap((int)Math.Ceiling(CheckCode.Length * 11.5), 20);
            Graphics g = Graphics.FromImage(img);
            try
            {
                g.Clear(Color.White);
                PrivateFontCollection pfc = new PrivateFontCollection();
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, img.Width, img.Height), Color.Blue, Color.DarkTurquoise, 1.2f, true);
                Font font = new Font("Arial", 11, (FontStyle.Regular | FontStyle.Italic));
                g.DrawString(CheckCode, font, brush, 2, 2);
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, img.Width - 1, img.Height - 1);
                MemoryStream ms = new MemoryStream();
                img.Save(ms, ImageFormat.Gif);
                return ms;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                g.Dispose();
                img.Dispose();
            }

        }

        /// <summary>
        /// 获取随机数
        /// </summary>
        /// <returns></returns>
        public string GenerateCheckCode(string SessionCode)
        {
            System.Web.HttpContext.Current.Session[SessionCode] = null;
            int number;
            char code;
            string checkcode = string.Empty;
            Random ran = new Random();
            for (int i = 0; i < 5; i++)
            {
                number = ran.Next();
                if (number % 2 == 0)
                {
                    code = (char)('0' + (char)(number % 10));
                }
                else
                {
                    code = (char)('A' + (char)(number % 26));
                }
                checkcode += code.ToString();
            }
            System.Web.HttpContext.Current.Session[SessionCode] = checkcode;
            return checkcode;
        }

        //绘制图片
        public byte[] MakeImage(string str)
        {
            MemoryStream stream = new MemoryStream();
            Random rd = new Random();
            Bitmap bmp = new Bitmap((int)Math.Ceiling((str.Length * 14.5)), 26);  //150,40
            Graphics gra = Graphics.FromImage(bmp);
            //随机生成的五个数（以及他们的颜色，字体都是随机的  
            for (int i = 0; i < str.Length; i++)
            {
                Point p = new Point(i * 20, 0);
                string[] fonts = { "微软雅黑", "宋体", "黑体", "隶书", "仿宋" };
                Color[] colors = { Color.Red, Color.Green, Color.Blue, Color.Purple, Color.Black };
                gra.DrawString(str[i].ToString(), new Font(fonts[rd.Next(0, 5)], 20, FontStyle.Bold), new SolidBrush(colors[rd.Next(0, 5)]), p);
            }
            //随机生成的线条  
            for (int i = 0; i < 10; i++)
            {
                Point p1 = new Point(rd.Next(0, bmp.Width), rd.Next(0, bmp.Height));
                Point p2 = new Point(rd.Next(0, bmp.Width), rd.Next(0, bmp.Height));
                gra.DrawLine(new Pen(Brushes.Yellow), p1, p2);
            }
            //随机生成的点  
            for (int i = 0; i < 300; i++)
            {
                Point p = new Point(rd.Next(0, bmp.Width), rd.Next(0, bmp.Height));
                bmp.SetPixel(p.X, p.Y, Color.Black);
            }
            bmp.Save(stream, ImageFormat.Png);
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ContentType = "image/Png";
            HttpContext.Current.Response.BinaryWrite(stream.ToArray());
            gra.Dispose();
            bmp.Dispose();
            HttpContext.Current.Response.End();
            return stream.ToArray();
        }

        public byte[] GetVerifyCode(string checkCode)
        {
            //int codeW = 100;
            //int codeH = 40;
            //int fontSize = 14;
            ////string chkCode = string.Empty;
            ////颜色列表，用于验证码、噪线、噪点 
            //Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Purple, Color.Brown, Color.DarkBlue };
            ////字体列表，用于验证码 
            //string[] font = { "Times New Roman" };
            ////验证码的字符集，去掉了一些容易混淆的字符 
            ////char[] character = { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'd', 'e', 'f', 'h', 'k', 'm', 'n', 'r', 'x', 'y', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };
            //Random rnd = new Random();
            //////生成验证码字符串 
            ////for (int i = 0; i < 5; i++)
            ////{
            ////    chkCode += character[rnd.Next(character.Length)];
            ////}
            ////写入cookie、验证码加密        
            ////创建画布
            //Bitmap bmp = new Bitmap(codeW, codeH);
            //Graphics g = Graphics.FromImage(bmp);
            //g.Clear(Color.White);
            ////画噪线 
            //for (int i = 0; i < 3; i++)
            //{
            //    int x1 = rnd.Next(codeW);
            //    int y1 = rnd.Next(codeH);
            //    int x2 = rnd.Next(codeW);
            //    int y2 = rnd.Next(codeH);
            //    Color clr = color[rnd.Next(color.Length)];
            //    g.DrawLine(new Pen(clr), x1, y1, x2, y2);
            //}
            ////随机生成的点  
            //for (int i = 0; i < 70; i++)
            //{
            //    Point p = new Point(rnd.Next(0, bmp.Width), rnd.Next(0, bmp.Height));
            //    Color clr = color[rnd.Next(color.Length)];
            //    bmp.SetPixel(p.X, p.Y, clr);
            //}
            ////画验证码字符串 
            //for (int i = 0; i < chkCode.Length; i++)
            //{
            //    string fnt = font[rnd.Next(font.Length)];
            //    Font ft = new Font(fnt, fontSize);
            //    Color clr = color[rnd.Next(color.Length)];
            //    g.DrawString(chkCode[i].ToString(), ft, new SolidBrush(clr), (float)i * 18, (float)5);
            //}
            ////将验证码图片写入内存流，并将其以 "image/Png" 格式输出 
            //MemoryStream ms = new MemoryStream();
            //try
            //{
            //    bmp.Save(ms, ImageFormat.Png);
            //    return ms.ToArray();
            //}
            //catch (Exception)
            //{
            //    return null;
            //}
            //finally
            //{
            //    g.Dispose();
            //    bmp.Dispose();
            //}

            System.Drawing.Bitmap image = new System.Drawing.Bitmap((int)Math.Ceiling((checkCode.Length * 14.5)), 26);
            Graphics g = Graphics.FromImage(image);
            try
            {
                //生成随机生成器
                Random random = new Random();
                //清空图片背景色
                g.Clear(Color.White);

                System.Drawing.Text.PrivateFontCollection pfc = new System.Drawing.Text.PrivateFontCollection();
                pfc.AddFontFile(System.Web.HttpContext.Current.Server.MapPath("../Content/fonts/NewSuperMarioFontU.ttf"));
                Font font = new System.Drawing.Font(pfc.Families[0], 14, (System.Drawing.FontStyle.Regular | System.Drawing.FontStyle.Italic));
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkTurquoise, 1.2f, true);
                g.DrawString(checkCode, font, brush, 2, 2);


                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                try
                {
                    image.Save(ms, ImageFormat.Png);
                    return ms.ToArray();
                }
                catch (Exception)
                {
                    return null;
                }
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
    }
}
