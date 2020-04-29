using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FireFighting.Tool
{
    public class PostData
    {
        /// <summary>
        /// psot 请求API 接口
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="paramter">序列化以后的参数</param>
        /// <returns></returns>
        public static string PostDataApi(string url, string paramter)
        {
            try
            {
                DateTime dtStart = System.DateTime.Now;
                HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(url);
                myReq.Method = "POST";
                myReq.Timeout = 60000 * 60;
                byte[] byteArray = Encoding.UTF8.GetBytes(paramter);
                myReq.ContentType = "application/json";
                Stream dataStream = myReq.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
                dataStream = HttpWResp.GetResponseStream();
                var result = "";
                using (StreamReader myStreamReader = new StreamReader(dataStream, Encoding.UTF8))
                {
                    result = myStreamReader.ReadToEnd();
                    DateTime dtEnd = System.DateTime.Now;
                    TimeSpan ts = dtEnd.Subtract(dtStart);
                    Console.WriteLine(ts);
                    myStreamReader.Close();
                }
                dataStream.Close();
                HttpWResp.Close();
                return result;
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }
    }
}
