using FireFighting.BLL;
using FireFighting.NET;
using FireFighting.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDemo.Lib;
using WebDemo.Models;

namespace WebDemo.Controllers
{
    public class BaseController : Controller
    {
        protected LoginAccountInfo LoginInfo = null;
        protected string RedirectUrl_LostSession = string.Empty;
        protected const string key_controller = "controller";   //因为要抓取路由中的值，所以这里的值是不能动的
        protected const string key_action = "action";           //因为要抓取路由中的值，所以这里的值是不能动的
        // GET: Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //WriteLog.WriteTxt("基础过滤器日志", Request.Url.AbsoluteUri + "### Authority=" + Request.Url.Authority + " ### host=" + Request.Url.Host);
            //if (string.IsNullOrEmpty(Version))
            //    Version = "1.0";
            string Scheme = Request.Url.Scheme;         //请求的主机，若有端口则带端口，
            string Authority = Request.Url.Authority;   //http或https
            string Host = Request.Url.Host;             //请求的主机，不带端口（即便后面有端口也去掉）
            var controller = filterContext.RouteData.Values[key_controller];    //当前请求的Controller
            var action = filterContext.RouteData.Values[key_action];            //当前请求的Action
            string httpMethod = filterContext.HttpContext.Request.HttpMethod.ToLower();//客户端请求模式（get或post）
            string BaseUrl = string.Empty;
            //跳转域名请求时可能有端口，而网站中是只需要域名，不需要端口，这里就需要特殊处理

            BaseUrl = string.Format("{0}://{1}", Scheme, Authority);//基础地址（如 http://localhost:81）
            RedirectUrl_LostSession = string.Format("{0}/Home/Login", BaseUrl);   //丢失会话，重定向的地址

            #region 缓存设置
            ViewBag.BaseUrl = BaseUrl;  //获取基础路径
            //ViewBag.RedirectUrl_LostSession = RedirectUrl_LostSession;//会话丢失重定向的地址
            //ViewBag.Version = Version;
            #endregion

            #region 判断用户是否登录
            //取缓存中的登录信息
            LoginInfo = Session[SysConfig.AuthSaveKey] as LoginAccountInfo;
            if (LoginInfo == null)
            {
                HttpCookie cookie = filterContext.HttpContext.Request.Cookies["Login"];
                if (filterContext.HttpContext.Request.Cookies["Login"] != null)
                {
                    string username = cookie.Values["username"];
                    string pwd = cookie.Values["userpwd"];
                    LoginInfoManager UserInfo = new LoginInfoManager();
                    AccountInfoEnt accountInfo = UserInfo.SearchAccountInfo(username);
                    if (accountInfo.Password == pwd)
                    {
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
                    }
                }
                else
                {
                    filterContext.Result = new RedirectResult(RedirectUrl_LostSession);
                }
            }
            #endregion


            #region  权限判断

            if (LoginInfo != null)
            {
                bool HasCurPower = false;//是否具有当前模块的权限
                List<SysPowerInfoModel> liPowerInfo = LoginInfo.AccountInfoModel.SysPower;
                string RequestUrl = string.Format("/{0}/{1}/", controller, action).ToLower();
                if (liPowerInfo != null && liPowerInfo.Count > 0)
                {
                    foreach (SysPowerInfoModel powerInfo in liPowerInfo)
                    {
                        string nodeUrl = powerInfo.AccessPath;
                        if (string.IsNullOrEmpty(nodeUrl))
                        {
                            continue;
                        }

                        if (RequestUrl.ToLower().IndexOf(nodeUrl.ToLower()) > -1)    //若请求的是大菜单，由于大菜单是没得真实页面，则不做控制
                        {
                            HasCurPower = true;
                            break;
                        }
                    }
                }
                #region  无权限

                if (!HasCurPower)  //无权限
                {
                    string RedirectUrl = string.Empty;
                    RedirectUrl = BaseUrl + "/home/index";//加上前缀
                    filterContext.Result = new RedirectResult(RedirectUrl);
                    return;
                }

                #endregion
            }


            #endregion



            base.OnActionExecuting(filterContext);
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