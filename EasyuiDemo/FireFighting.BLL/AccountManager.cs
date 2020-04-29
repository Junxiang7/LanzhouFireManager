
using FireFighting.DAL;
using FireFighting.NET;
using FireFighting.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFighting.BLL
{
    public class AccountManager
    {
        /// <summary>
        /// 用户查询
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="PageParameter"></param>
        /// <returns></returns>
        public List<AccountInfoEnt> getaccountInfo(string Account, ref PageParameterServerEnt PageParameter)
        {
            List<AccountInfoEnt> liaccount = new List<AccountInfoEnt>();
            AccountManagerServer accountManager = new AccountManagerServer();
            StringBuilder SbStr = new StringBuilder();
            SbStr.Append(" 1=1 ");
            if (!string.IsNullOrEmpty(Account))
            {
                SbStr.Append(" AND Account like '%" + Account + "%'");
            }
            string sqlwhere = SbStr.ToString();
            PageParameter.Where = sqlwhere;
            if (PageParameter.PageIndex < 1)
            {
                PageParameter.PageIndex = 1;
            }
            if (PageParameter.PageSize < 10)
            {
                PageParameter.PageSize = 10;
            }

            liaccount = accountManager.SearchAccountManagerInfo(ref PageParameter);
            return liaccount;
        }

        /// <summary>
        /// 根据帐号查询用户信息
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public AccountInfoEnt SearchAccountManagerinfo(string Account)
        {
            AccountInfoEnt accountInfoEnt = null;
            try
            {
                AccountManagerServer accountManager = new AccountManagerServer();
                accountInfoEnt = accountManager.SearchAccountinfo(Account);
            }
            catch (Exception ex)
            {

            }
            return accountInfoEnt;
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public int updateStatus(int Id, int Status)
        {
            AccountManagerServer accountManager = new AccountManagerServer();
            int result = 0;
            try
            {
                result = accountManager.updateStatusServer(Id, Status);
            }
            catch(Exception ex)
            {

            }
            return result;
        }


        public int UpUnlockPwd(int Id, int Status)
        {
            AccountManagerServer accountManager = new AccountManagerServer();
            int result = 0;
            try
            {
                result = accountManager.UpUnlockPwdServer(Id, Status);
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        /// <summary>
        /// 初始化密码
        /// </summary>
        /// <returns></returns>
        public int CSHPWD(int Id,string pwd)
        {
            AccountManagerServer accountManager = new AccountManagerServer();
            int result = 0;
            try
            {
                result = accountManager.CSHPWDServer(Id, pwd);
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("AccountManager", "CSHPWD", ex.Message.ToString(), "");
            }
            return result;
        }


        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="accountInfo"></param>
        /// <returns></returns>
        public int AddUserInfo(AccountInfoEnt accountInfo)
        {
            AccountManagerServer accountManager = new AccountManagerServer();
            int result = 0;
            try
            {
                result = accountManager.AddUserInfoService(accountInfo);
            }
            catch(Exception ex)
            {
                ErrorLog.InsertError("AccountManager", "AddUserInfo", ex.Message.ToString(), "");
            }
            return result;
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="AccountName"></param>
        /// <param name="EffectiveTime_s"></param>
        /// <param name="EffectiveTime_e"></param>
        /// <returns></returns>
        public int UpdateUserInfo(int Id,string AccountName,string EffectiveTime_s,string EffectiveTime_e)
        {
            AccountManagerServer accountManager = new AccountManagerServer();
            int result = 0;
            try
            {
                result= accountManager.UpdateUserInfoService(Id,  AccountName,  EffectiveTime_s,  EffectiveTime_e);
            }
            catch(Exception ex)
            {
                ErrorLog.InsertError("AccountManager", "UpdateUserInfo", ex.Message.ToString(), AccountName);
            }
            return result;
        }

        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public AccountInfoEnt SearchAccountById(int Id)
        {
            AccountInfoEnt accountInfoEnt = null;
            try
            {
                AccountManagerServer accountManager = new AccountManagerServer();
                accountInfoEnt = accountManager.SearchAccountByIdService(Id);
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("AccountManager", "SearchAccountById", ex.Message.ToString(), "");
            }
            return accountInfoEnt;
        }

         
    }
}
