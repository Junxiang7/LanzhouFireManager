using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Lib
{
    public class SysConfig
    {
        /// <summary>
        /// 授权登陆地址
        /// </summary>
        public static string AuthUrl { get; set; } = "/Home/Login";
        /// <summary>
        /// 主页
        /// </summary>
        public static string MainUrl { get; set; } = "/";
        /// <summary>
        /// 用户登陆SessionKey
        /// </summary>
        public static string AuthSaveKey { get; set; } = "FireFighting";
    }
}