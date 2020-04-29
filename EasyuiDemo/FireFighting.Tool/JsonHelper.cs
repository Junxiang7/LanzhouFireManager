
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
namespace FireFighting.Tool
{
    public static class JsonHelper
    {
        /// <summary>
        /// json序列化（非二进制方式）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string JsonSerializer<T>(T t)
        {
            JavaScriptSerializer jsonSerialize = new JavaScriptSerializer();
            return jsonSerialize.Serialize(t);
        }

        /// <summary>
        /// json反序列化（非二进制方式）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(string jsonString)
        {
            JavaScriptSerializer jsonSerialize = new JavaScriptSerializer();
            return (T)jsonSerialize.Deserialize<T>(jsonString);
        }


        /// <summary>
        /// JSON序列化(二进制方式，实体类使用[Serializable])
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        //public static string JsonSerializerIO<T>(T t)
        //{
        //    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        ser.WriteObject(ms, t);
        //        string jsonString = Encoding.UTF8.GetString(ms.ToArray());
        //        ms.Close();
        //        return jsonString;
        //    }
        //}

        /// <summary>
        /// JSON反序列化(二进制方法，实体类使用[Serializable])
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        //public static T JsonDeserializeIO<T>(string jsonString)
        //{
        //    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
        //    using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
        //    {
        //        T obj = (T)ser.ReadObject(ms);
        //        return obj;
        //    }
        //}


        /// <summary>
        /// 多条Json数据转换为泛型数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonArr">字符窜（格式为[{a:'',b:''},{a:'',b:''},{a:'',b:''}]）</param>
        /// <returns></returns>
        public static List<T> ConvertToList<T>(this string jsonArr)
        {
            if (!string.IsNullOrEmpty(jsonArr) && jsonArr.StartsWith("[") && jsonArr.EndsWith("]"))
            {
                Type t = typeof(T);
                var proPerties = t.GetProperties();
                List<T> list = new List<T>();
                string recive = jsonArr.Trim('[').Trim(']').Replace("'", "").Replace("\"", "");
                string[] reciveArr = recive.Replace("},{", "};{").Split(';');
                foreach (var item in reciveArr)
                {
                    T obj = ConvertToEntity<T>(item);
                    list.Add(obj);
                }
                return list;
            }
            return null;
        }

        /// <summary>
        /// 单条json数据转换为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str">字符窜（格式为{a:'',b:''}）</param>
        /// <returns></returns>
        private static T ConvertToEntity<T>(string str)
        {
            Type t = typeof(T);
            object obj = Activator.CreateInstance(t);
            var properties = t.GetProperties();
            string m = str.Trim('{').Trim('}');
            string[] arr = m.Split(',');
            for (int i = 0; i < arr.Count(); i++)
            {
                for (int k = 0; k < properties.Count(); k++)
                {
                    string Name = arr[i].Substring(0, arr[i].IndexOf(":"));
                    object Value = arr[i].Substring(arr[i].IndexOf(":") + 1);
                    if (properties[k].Name.Equals(Name))
                    {
                        if (properties[k].PropertyType.Equals(typeof(int)))
                        {
                            properties[k].SetValue(obj, Convert.ToInt32(Value), null);
                        }
                        if (properties[k].PropertyType.Equals(typeof(string)))
                        {
                            properties[k].SetValue(obj, Convert.ToString(Value), null);
                        }
                        if (properties[k].PropertyType.Equals(typeof(long)))
                        {
                            properties[k].SetValue(obj, Convert.ToInt64(Value), null);
                        }
                        if (properties[k].PropertyType.Equals(typeof(decimal)))
                        {
                            properties[k].SetValue(obj, Convert.ToDecimal(Value), null);
                        }
                        if (properties[k].PropertyType.Equals(typeof(double)))
                        {
                            properties[k].SetValue(obj, Convert.ToDouble(Value), null);
                        }
                        if (properties[k].PropertyType.Equals(typeof(Nullable<int>)))
                        {
                            properties[k].SetValue(obj, Convert.ToInt32(Value), null);
                        }
                        if (properties[k].PropertyType.Equals(typeof(Nullable<decimal>)))
                        {
                            properties[k].SetValue(obj, Convert.ToDecimal(Value), null);
                        }
                        if (properties[k].PropertyType.Equals(typeof(Nullable<long>)))
                        {
                            properties[k].SetValue(obj, Convert.ToInt64(Value), null);
                        }
                        if (properties[k].PropertyType.Equals(typeof(Nullable<double>)))
                        {
                            properties[k].SetValue(obj, Convert.ToDouble(Value), null);
                        }
                        if (properties[k].PropertyType.Equals(typeof(Nullable<DateTime>)))
                        {
                            properties[k].SetValue(obj, Convert.ToDateTime(Value), null);
                        }

                    }
                }

            }
            return (T)obj;
        }

        /// <summary>
        /// 转化序列化后JSON字符串中的日期格式
        /// </summary>
        /// <param name="jsonStr">序列化后的json字符串</param>
        /// <returns></returns>
        public static string JsonDateToStr(string jsonStr)
        {
            var str = Regex.Replace(jsonStr, @"\\/Date\((\d+)\)\\/", match =>
            {
                DateTime dt = new DateTime(1970, 1, 1);
                dt = dt.AddMilliseconds(long.Parse(match.Groups[1].Value));
                dt = dt.ToLocalTime();
                return dt.ToString("yyyy-MM-dd HH:mm:ss");
            });
            return str;

        }
    }
}
