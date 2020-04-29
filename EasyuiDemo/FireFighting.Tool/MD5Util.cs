using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace FireFighting.Tool
{
    public class MD5Util
    {
        public MD5Util()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /** 获取大写的MD5签名结果 */
        public static string GetMD5(string encypStr)
        {
            string retStr;
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();

            //创建md5对象
            byte[] inputBye;
            byte[] outputBye;

            //使用GB2312编码方式把字符串转化为字节数组．
            try
            {
                inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);
            }
            catch (Exception ex)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>()
                    {
                        {"参数", ex.Message.ToString() }
                    };
                inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);
            }
            outputBye = m5.ComputeHash(inputBye);

            retStr = System.BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToUpper();
            return retStr;
        }

        public static String GetMD5Hash(String MD5String)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] MD5Temp = System.Text.Encoding.UTF8.GetBytes(MD5String);
            MD5Temp = x.ComputeHash(MD5Temp);
            System.Text.StringBuilder StrTemp = new System.Text.StringBuilder();
            foreach (byte Res in MD5Temp)
            {
                StrTemp.Append(Res.ToString("x2").ToLower());//转化为小写
            }
            String password = StrTemp.ToString();
            return password;
        }

        /// <summary>
        /// MD5加密算法
        /// </summary>
        /// <param name="ClearText">需要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string MD5(string ClearText)
        {
            if (string.IsNullOrWhiteSpace(ClearText))
                return string.Empty;
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(ClearText.ToString());//将字符编码为一个字节序列 
            byte[] md5data = md5.ComputeHash(data);//计算data字节数组的哈希值 
            md5.Clear();
            string str = "";
            for (int i = 0; i < md5data.Length; i++)
            {
                str += md5data[i].ToString("x").PadLeft(2, '0');
            }
            return str;
        }
    }
}
