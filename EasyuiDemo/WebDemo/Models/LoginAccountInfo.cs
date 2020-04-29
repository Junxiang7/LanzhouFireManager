using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Models
{
    public class LoginAccountInfo
    {
        /// <summary>
        /// 登录信息
        /// </summary>
        public AccountInfoModel AccountInfoModel { get; set; }

        /// <summary>
        /// 上级信息
        /// </summary>
        public OnAccountInfoModel OnAccountInfoModel { get; set; }
    }
}