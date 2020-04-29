using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFighting.NET
{
    public class AccountInfoEnt
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

        public string AccountName { get; set; }

        /// <summary>
        /// 帐号
        /// </summary>
        public string Account { get; set; }

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

        public int PwdErrorCount { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public int UnlockPwd { get; set; }
    }

    public class AccountInfoServerEnt
    {
        private List<AccountInfoEnt> liAccountInfo = new List<AccountInfoEnt>();

        public List<AccountInfoEnt> LiAccountInfo
        {
            get
            {
                return liAccountInfo;
            }

            set
            {
                liAccountInfo = value;
            }
        }
    }
}
