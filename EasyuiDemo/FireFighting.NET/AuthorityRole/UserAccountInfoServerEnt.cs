
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FireFighting.NET
{
    [Serializable]
    public class UserAccountInfoServerEnt
    {
        #region  属性

        /// <summary>
        /// 用户账号
        /// </summary>
        public string WalletAccount { get; set; }

        /// <summary>
        /// 父级账号guid
        /// </summary>
        public string MainUserId { get; set; }

        /// <summary>
        /// 父级账号
        /// </summary>
        public string MainAccount { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string LinkPhone { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }


        /// <summary>
        /// 公司职位
        /// </summary>
        public string CompanyPosition { get; set; }

        /// <summary>
        /// 添加人员账号
        /// </summary>
        public string AddAccount { get; set; }

        /// <summary>
        /// 添加日期
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDelete { get; set; }


        #endregion

    }
}
