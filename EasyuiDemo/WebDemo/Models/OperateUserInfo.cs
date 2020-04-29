using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Models
{
    public class OperateUserInfo
    {
        public string Account { get; set; }

        public OperateUserInfo(LoginAccountInfo loginInfo)
        {
            this.Account = loginInfo.AccountInfoModel.Account;
        }
    }
}