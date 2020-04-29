
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireFighting.Tool
{
    public class TransformDataHelper
    {
        /// <summary>
        /// 将字符串转化为Int
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static int TransformToInt(string Str)
        {
            int Result = 0;
            if (string.IsNullOrEmpty(Str))
                return Result;
            if (int.TryParse(Str.Trim(), out Result))
            {
                return Result;
            }
            return 0;
        }
        public static bool TransformTobool(string Str)
        {
            bool Result = false;
            if (string.IsNullOrEmpty(Str))
                return Result;
            if (bool.TryParse(Str.Trim(), out Result))
            {
                return Result;
            }
            return false;
        }
        /// <summary>
        /// 字符串转化为decimal
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static decimal TransformToDecimal(string Str)
        {
            decimal Result = 0M;
            if (string.IsNullOrEmpty(Str))
                return Result;
            if (decimal.TryParse(Str.Trim(), out Result))
            {
                return Result;
            }
            return 0M;
        }
        /// <summary>
        /// 将小数值按指定的小数位数截断
        /// </summary>
        /// <param name="d">要截断的小数</param>
        /// <param name="s">小数位数，s大于等于0，小于等于28</param>
        /// <returns></returns>
        public static decimal ToFixed(decimal d, int s)
        {
            decimal sp = Convert.ToDecimal(Math.Pow(10, s));
            return Math.Truncate(d) + Math.Floor((d - Math.Truncate(d)) * sp) / sp;
        }
        /// <summary>
        /// 字符串转化为long
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static long TransformToLong(string Str)
        {
            long Result = 0;
            if (string.IsNullOrEmpty(Str))
                return Result;
            if (long.TryParse(Str.Trim(), out Result))
            {
                return Result;
            }
            return 0;
        }
        public static byte TransformToByte(string Str)
        {
            byte Result = 0;
            if (string.IsNullOrEmpty(Str))
                return Result;
            if (byte.TryParse(Str.Trim(), out Result))
            {
                return Result;
            }
            return 0;
        }
        public static short TransformToShort(string Str)
        {
            short Result = 0;
            if (string.IsNullOrEmpty(Str))
                return Result;
            if (short.TryParse(Str.Trim(), out Result))
            {
                return Result;
            }
            return 0;
        }
        /// <summary>
        /// 字符串转化为float
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static float TransformToFloat(string Str)
        {
            float Result = 0f;
            if (string.IsNullOrEmpty(Str))
                return Result;
            if (float.TryParse(Str.Trim(), out Result))
            {
                return Result;
            }
            return 0f;
        }
        /// <summary>
        /// 字符串转化为DateTime
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static DateTime TransformToDateTime(string Str)
        {
            DateTime Result = DateTime.MinValue;
            if (string.IsNullOrEmpty(Str))
                return Result;
            if (DateTime.TryParse(Str.Trim(), out Result))
            {
                return Result;
            }
            return DateTime.MinValue;
        }
    }
}
