using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Models
{
    public class AccountInfoModel
    {

        public int Id { get; set; }
        private Int32 accountStatus = 0;
        /// <summary>
        /// 账号状态   0 冻结  1 启用
        /// </summary>
        public Int32 AccountStatus
        {
            get { return accountStatus; }
            set { accountStatus = value; }
        }

        public string AccountStatusText { get; set; }

        /// <summary>
        /// 帐号
        /// </summary>
        public string Account { get; set; }

        public string AccountName { get; set; }



        /// <summary>
        /// 上级帐号
        /// </summary>
        public string OnAccount { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public string lastTime { get; set; }

        /// <summary>
        /// 最后一次登录的IP
        /// </summary>
        public string lastIP { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        public Int32 LoginCount { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public int UnlockPwd { get; set; }

        /// <summary>
        /// 锁定状态文本
        /// </summary>
        public string LockStateText { get; set; }

        public int PwdErrorCount { get; set; }

        /// <summary>
        /// 权限集合
        /// </summary>
        private List<PowerInfoModel> power = new List<PowerInfoModel>();

        public List<PowerInfoModel> Power
        {
            get { return power; }
            set { power = value; }
        }

        private List<SysPowerInfoModel> sysPower = new List<SysPowerInfoModel>();

        public List<SysPowerInfoModel> SysPower
        {
            get { return sysPower; }
            set { sysPower = value; }
        }

        /// <summary>
        /// 当前问候语
        /// </summary>
        public string CurrentGreetings { get; set; }


        public string Permissions { get; set; }
    }

    /// <summary>
    /// 上级Model
    /// </summary>
    public class OnAccountInfoModel
    {
        public AccountInfoModel OnAccountInfo { get; set; }
    }


}