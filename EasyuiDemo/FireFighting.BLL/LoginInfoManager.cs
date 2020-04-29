
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
    public class LoginInfoManager
    {
        /// <summary>
        /// 根据帐号获取用户信息
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public AccountInfoEnt SearchAccountInfo(string Account)
        {
            AccountInfoEnt AccountInfo = null;
            try
            {
                //LoginInfoServer loginInfoServer = new LoginInfoServer();
                LoginInfoServer LoginInfo = new LoginInfoServer();
                AccountInfo = LoginInfo.SearchAccountserver(Account);
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("LoginInfoManager", "SearchAccountInfo", ex.Message.ToString(), Account);
            }
            return AccountInfo;
        }

        /// <summary>
        /// 根据ID获取帐号信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AccountInfoEnt SearchAccountInfoByid(int id)
        {
            AccountInfoEnt AccountInfo = new AccountInfoEnt();
            try
            {
                //LoginInfoServer loginInfoServer = new LoginInfoServer();
                LoginInfoServer LoginInfo = new LoginInfoServer();
                AccountInfo = LoginInfo.SearchAccountByidserver(id);
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("LoginInfoManager", "SearchAccountInfoByid", ex.Message.ToString(), "");
            }
            return AccountInfo;
        }

        public void LogLoginInfo(string account, string Ip, string Message)
        {
            LoginInfoServer LoginInfo = new LoginInfoServer();
            try
            {
                LoginInfo.LogLoginInfoService(account, Ip, Message);
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("LoginInfoManager", "LogLoginInfo", ex.Message.ToString(), account);
            }
        }

        public void UnLockPwd(string account)
        {
            LoginInfoServer LoginInfo = new LoginInfoServer();
            LoginInfo.UnLockPwdService(account);
        }

        /// <summary>
        /// 判断密码是否正确
        /// </summary>
        /// <param name="WalletAccount"></param>
        /// <param name="PassWord"></param>
        /// <returns></returns>
        public string PassWordEncryption(string WalletAccount, string PassWord)
        {
            string Ultimate = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(WalletAccount) && !string.IsNullOrEmpty(PassWord))
                {
                    if (WalletAccount.Length > 1)
                    {
                        //string acc = WalletAccount.Substring(0, 2);
                        //string pass = acc + PassWord.Substring(0, 4) +  PassWord.Substring(4);
                        string pass = PassWord;
                        string string64 = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(pass));
                        string MdP = MD5Util.MD5(string64);
                        Ultimate = MD5Util.MD5(MdP);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("LoginInfoManager", "PassWordEncryption", ex.Message.ToString(), WalletAccount + "|" + PassWord);
            }

            return Ultimate;
        }
    }
}
