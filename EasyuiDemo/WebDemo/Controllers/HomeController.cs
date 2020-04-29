using FireFighting.BLL;
using FireFighting.NET;
using FireFighting.Tool;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDemo.Lib;
using WebDemo.Models;

namespace WebDemo.Controllers
{
    [Authorization]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            LoginAccountInfo AccountInfo = (LoginAccountInfo)Session[SysConfig.AuthSaveKey];
            UserAuthList userAuthList = new Lib.UserAuthList();
            ViewBag.UserAccount = AccountInfo.AccountInfoModel;
            ViewBag.UserMenus = JsonConvert.SerializeObject(userAuthList.MenuList(ViewBag.UserAccount));


            return View();
        }


        /// <summary>
        /// 登录页面验证码展示
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult CheckCodes()
        {
            VerificationCode ver = new VerificationCode();
            return File(ver.GetVerifyCode(ver.GenerateCheckCode("LoginCheckeCode")).ToArray(), "image/Jpeg");
        }


        /// <summary>
        /// 获取IP
        /// </summary>
        /// <returns></returns>
        private string GetIP()
        {
            string ip = string.Empty;
            if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"]))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);
            if (string.IsNullOrEmpty(ip))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            return ip;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private string UserLogin(string login, string password, string verifyCode)
        {
            string ip = GetIP();
            try
            {
                string account = SqlFilter.SuperFilter(login.Trim());
                string pwd = SqlFilter.SuperFilter(password.Trim());
                #region 参数验证
                if (string.IsNullOrEmpty(account))
                {
                    return ("请输入登录用户名");
                }
                else if (string.IsNullOrEmpty(pwd))
                {
                    return ("请输入登录密码");
                }
                else if (string.IsNullOrEmpty(verifyCode))
                {
                    return ("请输入验证码");
                }
                else if (verifyCode.ToLower() != (Session["LoginCheckeCode"] == null ? string.Empty : Session["LoginCheckeCode"].ToString().Trim().ToLower()))
                {
                    return ("验证码错误，请重新输入！");
                }
                #endregion
                try
                {
                    LoginInfoManager UserInfo = new LoginInfoManager();
                    AccountInfoEnt accountInfo = null;
                    accountInfo = UserInfo.SearchAccountInfo(account);
                    if (accountInfo != null)
                    {
                        if (accountInfo.AccountStatus == 1)
                        {
                            UserInfo.LogLoginInfo(account, GetIP(), "该账号已被禁用");
                            return ("该账号已被禁用");
                        }
                        //if (accountInfo.UnlockPwd == 1)
                        //{
                        //    UserInfo.LogLoginInfo(account, GetIP(), "该账号已被锁定");
                        //    return ("该账号已被锁定");
                        //}
                        if (DateTime.Now > TransformDataHelper.TransformToDateTime(accountInfo.EndTime))
                        {
                            UserInfo.LogLoginInfo(account, GetIP(), "该账号已超过生效时间");
                            return ("该账号已超过生效时间");
                        }
                        string pass = UserInfo.PassWordEncryption(SqlFilter.SuperFilter(account).Trim(), pwd);
                        if (pass == accountInfo.Password)
                        {
                            #region 获取登录信息
                            Session[SysConfig.AuthSaveKey] = null;
                            LoginAccountInfo loginAccount = new LoginAccountInfo();              //登录对象
                            AccountEntToModel accountetm = new Models.AccountEntToModel();
                            RoleManager rm = new RoleManager();

                            PowerInfoEntToModel powerInfoEntTo = new PowerInfoEntToModel();
                            List<SysPowerInfoModel> liPowerInfo = new List<Models.SysPowerInfoModel>();
                            SysRetResultEnt sysRetResult = rm.GetSysPower(accountInfo.Id);
                            liPowerInfo = powerInfoEntTo.PowerInfoEntTo(sysRetResult);
                            AccountInfoModel accountInfoModel = accountetm.GetAccountModel(accountInfo);

                            loginAccount.AccountInfoModel = accountInfoModel;
                            loginAccount.AccountInfoModel.SysPower = liPowerInfo;
                            loginAccount.AccountInfoModel.Permissions = GetSysPermissions(liPowerInfo);
                            DateTime tmCur = DateTime.Now;
                            if (tmCur.Hour < 8 || tmCur.Hour > 18)
                            {
                                loginAccount.AccountInfoModel.CurrentGreetings = "晚上好";
                            }
                            else if (tmCur.Hour >= 8 && tmCur.Hour < 12)
                            {
                                loginAccount.AccountInfoModel.CurrentGreetings = "上午好";
                            }
                            else
                            {
                                loginAccount.AccountInfoModel.CurrentGreetings = "下午好";
                            }

                            Session[SysConfig.AuthSaveKey] = loginAccount;
                            HttpRuntime.Cache.Insert(Session.SessionID, loginAccount, null, DateTime.Now.AddYears(1), TimeSpan.Zero);
                            UserInfo.LogLoginInfo(account, GetIP(), "登录成功");
                            #endregion
                            HttpCookie cookie = new HttpCookie("Login");
                            cookie.Values.Set("username", account);
                            cookie.Values.Set("userpwd", pass);
                            cookie.Expires.AddMonths(1);
                            Response.SetCookie(cookie);
                            Response.Cookies.Add(cookie);

                            return ("登录成功");
                        }
                        else
                        {
                            UserInfo.UnLockPwd(account);
                            UserInfo.LogLoginInfo(account, GetIP(), "帐号或者密码错误");
                            return ("帐号或者密码错误");
                        }
                    }
                    else
                    {
                        return ("帐号或者密码错误");

                    }
                }
                catch (Exception ex)
                {
                    ErrorLog.InsertError("HomeController", "UserLogin", ex.Message.ToString(), "");
                    return ("帐号或者密码错误");
                }
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("HomeController", "UserLogin", ex.Message.ToString(), "");
                return ("用户登录出错，请联系管理员！");
            }

        }

        [AllowAnonymous]
        public ActionResult Login()
        {

            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string login, string password, string CodeImage)
        {
            return Content(UserLogin(login, password, CodeImage));

        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>  
        [AllowAnonymous]
        public ActionResult LoginOut()
        {
            Session[SysConfig.AuthSaveKey] = null;
            Session["UserAuthList_Cache"] = null;
            HttpCookie aCookie;
            string cookieName;
            int limit = Request.Cookies.Count;
            if (limit > 0)
            {
                for (int i = 0; i < limit; i++)
                {
                    cookieName = Request.Cookies[i].Name;
                    aCookie = new HttpCookie(cookieName);
                    aCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(aCookie);
                }
            }
            return new RedirectResult(SysConfig.AuthUrl);
        }

        private List<PowerInfoModel> GetPowerInfo(RetResultEnt rre)
        {
            List<PowerInfoModel> pims = new List<PowerInfoModel>();
            try
            {
                if (rre.Result)
                {
                    PowerInfoModel pim;
                    foreach (var item in rre.RolePower)
                    {
                        pim = new PowerInfoModel();
                        pim.NodeName = item.NodeName;
                        pim.NodeType = item.NodeType;
                        pim.PowerID = item.PowerID.ToString();
                        pim.PNodeID = item.PNodeID.ToString();
                        pim.NodeUrl = item.AccessPath;
                        pim.IsForeground = item.IsForeground;
                        pim.EnterUrl = item.EnterUrl;
                        pim.NodeCode = item.NodeCode;
                        pims.Add(pim);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return pims;
        }

        private string GetPermissions(List<PowerInfoModel> pims)
        {
            //Sys:首页|SysManager:用户管理,用户设置|SysTools:超级密码,修改订单Admin,操作日志,Eterm日志|Eterm:DetrTN,DetrTNH,SendCmd
            string Strpims = string.Empty;
            try
            {
                if (pims.Count > 0)
                {
                    for (int i = 0; i < pims.Count; i++)
                    {
                        string StrSub = string.Empty;
                        for (int j = 0; j < pims.Count; j++)
                        {
                            if (pims[j].PNodeID == pims[i].PowerID && pims[j].NodeType == 1)
                            {
                                StrSub += pims[j].NodeName + ",";
                            }
                        }
                        if (pims[i].NodeType == 0)
                        {
                            Strpims += pims[i].NodeName + "|" + StrSub;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("HomeController", "GetPermissions", ex.Message.ToString(), "");
            }
            Strpims = "Sys:首页|SysManager:用户管理,用户设置|SysTools:超级密码,修改订单Admin,操作日志,Eterm日志|Eterm:DetrTN,DetrTNH,SendCmd|Order:第一工站,第二工站,第三工站,第四工站,订单管理,订单查询,客户录入,客户详情|System:统计";
            return Strpims;
        }

        private string GetSysPermissions(List<SysPowerInfoModel> lisysPowerInfo)
        {
            string Str = string.Empty;
            try
            {
                if (lisysPowerInfo != null && lisysPowerInfo.Count > 0)
                {
                    for (int i = 0; i < lisysPowerInfo.Count; i++)
                    {
                        string Strpim = string.Empty; ;
                        if (lisysPowerInfo[i].PNodeID == 0)
                        {
                            Strpim = lisysPowerInfo[i].NodeNameEN + ":";
                        }
                        for (int j = 0; j < lisysPowerInfo.Count; j++)
                        {
                            if (lisysPowerInfo[i].PowerID == lisysPowerInfo[j].PNodeID)
                            {
                                Strpim += lisysPowerInfo[j].NodeName + ",";
                                for (int w = 0; w < lisysPowerInfo.Count; w++)
                                {
                                    if (lisysPowerInfo[j].PowerID == lisysPowerInfo[w].PNodeID)
                                    {
                                        Strpim += lisysPowerInfo[w].NodeName + ",";
                                    }
                                }
                            }
                        }
                        if (lisysPowerInfo[i].PNodeID == 0)
                        {
                            Str += Strpim.TrimEnd(',') + "|";
                        }

                    }
                }
                else
                {
                    Str = "Sys:首页";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("HomeController", "GetSysPermissions", ex.Message.ToString(), "");
            }
            return Str.TrimEnd('|');
        }
    }
}