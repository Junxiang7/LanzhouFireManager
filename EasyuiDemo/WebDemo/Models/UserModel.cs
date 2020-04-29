using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Models
{
    public class UserModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPwd { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string LinkName { get; set; }
        /// <summary>
        /// 联系人手机
        /// </summary>
        public string LinkPhone { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public string Permissions { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime EffectiveTime { get; set; }

        /// <summary>
        /// 限制ip
        /// </summary>
        public List<string> Ip { get; set; }
    }
}