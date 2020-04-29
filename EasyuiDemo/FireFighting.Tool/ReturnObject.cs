using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireFighting.Tool
{
    [Serializable]
    public class ReturnObject
    {
        #region 内置属性
        public  const string key_returnnum = "returnnum";         //返回数字
        public  const string key_returnmessage = "returnmessage"; //返回消息
        public  const string key_returndata = "returndata";       //返回的数据
        public  const string key_url = "url";                     //url跳转地址
        #endregion
        public static HashObject GetReturnObject(string msg = "") 
        {
            string[] keys = new string[] { key_returnnum, key_returnmessage, key_returndata, key_url };
            object[] values = new object[] { string.IsNullOrEmpty(msg) ? 0 : -1, msg, null, "" };
            return new HashObject(keys, values);
        }

        #region 内置方法
        /// <summary>
        /// 检查数据是否有效
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool Validata(HashObject obj) 
        { 
            bool isThrough = false;
            if (obj == null)
                return isThrough;
            if (!obj.Contains(key_returnnum) || string.IsNullOrEmpty(obj[key_returnnum].ToString()))
                return isThrough;

            if (Convert.ToInt32(obj[key_returnnum]) >= 0)
                isThrough = true;
            return isThrough;
        }

        public static string GetReturnMessage(HashObject obj) 
        {
            string msg = string.Empty;
            if (obj == null)
                return msg;

            if (!obj.Contains(key_returnmessage))
                return msg;

            msg = obj[key_returnmessage].ToString();
            return msg;
        }

        /// <summary>
        /// 获取对象中returndata的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T GetReturnData<T>(HashObject obj, object defaultValue = null)
        {
            object result = obj[key_returndata];
            if (result == null) 
            {
                result = defaultValue;
            }

            return (T)Convert.ChangeType(result, typeof(T));
        }

        public static string GetUrl(HashObject obj, string defaultValue = "") 
        {
            object result = obj[key_url];
            if (result == null)
                result = defaultValue;
            return result == null ? "" : result.ToString();
        }
        #endregion
    }
}