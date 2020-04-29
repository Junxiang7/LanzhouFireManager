
using FireFighting.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Models
{
    public class AccountEntToModel
    {
        public AccountInfoModel GetAccountModel(AccountInfoEnt accountInfo)
        {
            AccountInfoModel accountInfoModel = new Models.AccountInfoModel();
            if (accountInfo != null)
            {

                accountInfoModel.Account = accountInfo.Account;
                accountInfoModel.AccountStatus = accountInfo.AccountStatus;
                accountInfoModel.lastIP = accountInfo.lastIP;
                accountInfoModel.lastTime = accountInfo.lastTime;
                accountInfoModel.AccountName = accountInfo.AccountName;
                accountInfoModel.EndTime = accountInfo.EndTime;
                accountInfoModel.StartTime = accountInfo.StartTime;
                accountInfoModel.UnlockPwd = accountInfo.UnlockPwd;
                accountInfoModel.LockStateText = "未锁定";
                if (accountInfoModel.UnlockPwd==1)
                {
                    accountInfoModel.LockStateText = "已锁定";
                }
                accountInfoModel.PwdErrorCount = accountInfo.PwdErrorCount;
                accountInfoModel.LoginCount = accountInfo.LoginCount;
                return accountInfoModel;
            }
            return accountInfoModel;
        }

        public AccountManagerInfo  ListAccountEntToModel(List<AccountInfoEnt> liaccountinfo)
        {
            AccountManagerInfo accountManagerInfo = new AccountManagerInfo();
            List<AccountInfoModel> liAccountInfoModel = new List<AccountInfoModel>();
            try
            {
                if(liaccountinfo!=null&& liaccountinfo.Count>0)
                {
                    for (int i = 0; i < liaccountinfo.Count; i++)
                    {
                        AccountInfoModel accountInfoModel = new Models.AccountInfoModel();
                        accountInfoModel.AccountStatus = liaccountinfo[i].AccountStatus;
                        if (accountInfoModel.AccountStatus == 0)
                        {
                            accountInfoModel.AccountStatusText = "正常";
                        }
                        else
                        {
                            accountInfoModel.AccountStatusText = "冻结";
                        }
                        accountInfoModel.Id = liaccountinfo[i].Id;
                        accountInfoModel.Account = liaccountinfo[i].Account;
                        accountInfoModel.lastIP = liaccountinfo[i].lastIP;
                        accountInfoModel.LoginCount = liaccountinfo[i].LoginCount;
                        accountInfoModel.lastTime = liaccountinfo[i].lastTime;
                        accountInfoModel.AccountName = liaccountinfo[i].AccountName;
                        accountInfoModel.UnlockPwd = liaccountinfo[i].UnlockPwd;
                        accountInfoModel.StartTime = liaccountinfo[i].StartTime;
                        accountInfoModel.EndTime = liaccountinfo[i].EndTime;
                        liAccountInfoModel.Add(accountInfoModel);
                    }
                    accountManagerInfo.LiAccountInfoModel = liAccountInfoModel;
                }
            }
            catch(Exception ex)
            {

            }
            return accountManagerInfo;
        }
    }

    public class AccountManagerInfo
    {
        private List<AccountInfoModel> liAccountInfoModel = new List<AccountInfoModel>();

        /// <summary>
        /// 用户信息
        /// </summary>
        public List<AccountInfoModel> LiAccountInfoModel
        {
            get { return liAccountInfoModel; }
            set { liAccountInfoModel = value; }
        }
        /// <summary>
        /// 总页数
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 分页
        /// </summary>
        public PageParameterModel PageParameterModel { get; set; }
    }
}