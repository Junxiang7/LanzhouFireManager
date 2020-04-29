using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FireFighting.Tool
{
    public class WriteLog
    {
        /// <summary>
        /// 日志文本记录
        /// </summary>
        /// <param name="patch"></param>
        /// <param name="txt"></param>
        public static void WriteTxt(string patch, string txt)
        {
            try
            {
                DateTime now = DateTime.Now;
                string FileName = now.ToString("yyyy-MM-dd HH") + ".txt";
                if (!Directory.Exists(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + patch + "/" + now.ToString("yyyy-MM-dd")))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + patch + "/" + now.ToString("yyyy-MM-dd"));
                }
                System.IO.File.AppendAllText(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + patch + "/" + now.ToString("yyyy-MM-dd") + "/" + FileName, "==================================================" + now.ToString("HH:mm:ss") + "\r\n" + txt);
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }
    }
}
