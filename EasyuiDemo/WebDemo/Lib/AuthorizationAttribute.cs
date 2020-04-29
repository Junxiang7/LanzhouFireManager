using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;                  
using WebDemo.Models;

namespace WebDemo.Lib
{
    /// <summary>
    /// 表示需要用户登录才可以使用的特性
    /// <para>如果不需要处理用户登录，则请指定AllowAnonymousAttribute属性</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizationAttribute : FilterAttribute, IAuthorizationFilter
    {
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
        /// 处理用户登录
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext == null)
            {
                throw new Exception("此特性只适合于Web应用程序使用！");
            }
            else
            {

                if (filterContext.HttpContext.Session == null)
                {
                    throw new Exception("服务器Session不可用！");
                }
                else if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) && !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                {
                    //没有登陆
                    if (filterContext.HttpContext.Session[SysConfig.AuthSaveKey] == null)
                    {

                        filterContext.Result = new RedirectResult(SysConfig.AuthUrl);
                    }
                    else
                    {
                        var UserAccount = (LoginAccountInfo)filterContext.HttpContext.Session[SysConfig.AuthSaveKey];
                        //当前URL
                        var durl = filterContext.HttpContext.Request.Url.LocalPath.ToLower();
                        UserAuthList userAuthList = new Lib.UserAuthList();
                        //判断是否有登陆权限
                        if (!userAuthList.GetAuthList(UserAccount.AccountInfoModel).Exists(x => x.menus.Exists(d => d.url.ToLower() == durl)))
                        {
                            filterContext.Result = new ContentResult() { Content = "当前操作没有授权" };
                            HttpContext.Current.Session["UserAuthList_Cache"] = null;
                        }
                        else
                        {

                            //filterContext.HttpContext.Request.QueryString
                            string avgs = "";
                            foreach (string key in filterContext.HttpContext.Request.QueryString.Keys)
                            {
                                avgs += string.Format("&{0}={1}", key, filterContext.HttpContext.Request[key]);
                            }
                            foreach (string key in filterContext.HttpContext.Request.Form.Keys)
                            {
                                avgs += string.Format("&{0}={1}", key, filterContext.HttpContext.Request[key]);
                            }                                    
                            Utils.CreatrPath(Utils.GetMapPath("/data/" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt"));
                            StringBuilder StrText = new StringBuilder();
                            StrText.Append("时间:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            StrText.Append("\r\n操作人:" + UserAccount.AccountInfoModel.Account);
                            StrText.Append("\r\n操作IP:" + GetIP());
                            StrText.Append("\r\n请求地址:" + filterContext.HttpContext.Request.Url.LocalPath);
                            StrText.Append("\r\n参数:" + avgs);
                            StrText.Append("\r\n=================================================================================\r\n");
                            using (StreamWriter sw = new StreamWriter(Utils.GetMapPath("/data/" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt"), true))
                            {
                                sw.WriteLine(StrText.ToString());
                            }
                        }
                    }
                }
            }
        }
    }
}