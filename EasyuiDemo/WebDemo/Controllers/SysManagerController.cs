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
    public class SysManagerController : BaseController
    {
        OperateUserInfo oppo = null;

        #region 用户管理
        // GET: SysManager
        public ActionResult UserList()
        {
            //string UserDataJson = System.IO.File.ReadAllText(Server.MapPath("~/DataBase/User.json"));
            //ViewBag.UserList = JsonConvert.DeserializeObject<List<UserModel>>(UserDataJson);

            UserAuthList userAuthList = new UserAuthList();
            ViewBag.AllAuthList = userAuthList.GetAllAuthList();
            return View();
        }

        /// <summary>
        /// 用户查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public JsonResult UserListJson(int page = 1, int rows = 10, string UserName = "")
        {
            HashObject objReturn = ReturnObject.GetReturnObject();
            PageParameterServerEnt PageParameter = new PageParameterServerEnt(); //服务器分页实例化
            PageParameter.PageSize = rows;
            if (page <= 0)
            {
                PageParameter.PageIndex = 1;
            }
            else
            {
                PageParameter.PageIndex = page;
            }
            if (rows <= 10)
            {
                PageParameter.PageSize = 10;
            }
            else
            {
                PageParameter.PageSize = rows;
            }
            UserName = SqlFilter.SuperFilter(UserName.Trim());
            AccountManager accountManager = new AccountManager();
            List<AccountInfoEnt> accountInfoli = accountManager.getaccountInfo(UserName, ref PageParameter);
            AccountEntToModel Accountetm = new Models.AccountEntToModel();
            AccountManagerInfo AccountInfomodel = new Models.AccountManagerInfo();
            AccountInfomodel = Accountetm.ListAccountEntToModel(accountInfoli);
            AccountInfomodel.Total = accountInfoli.Count(); //PageParameter.PageCount;

            var jsondata = new { rows = AccountInfomodel.LiAccountInfoModel, total = PageParameter.TotalCount };

            return Json(jsondata);// JsonConvert.SerializeObject(jsondata);
        }

        /// <summary>
        /// 增加或者修改账号
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="UserName"></param>
        /// <param name="UserPwd"></param>
        /// <param name="LinkName"></param>
        /// <param name="LinkPhone"></param>
        /// <param name="EffectiveTime"></param>    
        /// <param name="Autlist">权限</param>
        /// <returns></returns>
        public string AddOrEditUser(int Id, string UserName, string AccountName, string EffectiveTime_s, string EffectiveTime_e, int UpType, string Status = "")
        {
            string OperationContent = "";
            string Message = string.Empty;
            try
            {
                int result = 0;
                AccountManager accountManager = new AccountManager();
                if (Status != "")
                {
                    if (Id <= 0)
                    {
                        return "操作信息有误";
                    }
                    if (UpType == 1)  //  用户状态
                    {
                        result = accountManager.updateStatus(Id, TransformDataHelper.TransformToInt(Status));
                        OperationContent = "更改用户状态：Id:" + Id;
                    }
                    else if (UpType == 2)   //账号锁定 暂时不用
                    {
                        result = accountManager.UpUnlockPwd(Id, TransformDataHelper.TransformToInt(Status));
                    }
                    else if (UpType == 3)  //初始化密码
                    {
                        LoginInfoManager UserInfo = new LoginInfoManager();
                        AccountInfoEnt AccountInfo = UserInfo.SearchAccountInfoByid(Id);
                        string pwd = UserInfo.PassWordEncryption(SqlFilter.SuperFilter(AccountInfo.Account).Trim(), AccountInfo.Account);
                        result = accountManager.CSHPWD(Id, pwd);
                        OperationContent = "初始化密码：Id:" + Id + "|AccountInfo.Account:" + AccountInfo.Account;
                    }
                    if (result > 0)
                    {
                        Message = "修改成功";
                    }
                    else
                    {
                        Message = "修改失败";
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(AccountName))
                    {
                        return "用户名称必填";
                    }
                    if (string.IsNullOrWhiteSpace(EffectiveTime_s))
                    {
                        return "开始有效期必填";
                    }
                    if (string.IsNullOrWhiteSpace(EffectiveTime_e))
                    {
                        return "结束有效期必填";
                    }
                    UserName = SqlFilter.SuperFilter(UserName.Trim());
                    AccountName = SqlFilter.SuperFilter(AccountName.Trim());
                    EffectiveTime_s = EffectiveTime_s.Trim();
                    EffectiveTime_e = EffectiveTime_e.Trim();
                    //if (TransformDataHelper.TransformToDateTime(EffectiveTime_s) < DateTime.Now || TransformDataHelper.TransformToDateTime(EffectiveTime_e) < DateTime.Now || TransformDataHelper.TransformToDateTime(EffectiveTime_s) >= TransformDataHelper.TransformToDateTime(EffectiveTime_e))
                    //{
                    //    return "当前设置的时间信息有误";
                    //}
                    if (TransformDataHelper.TransformToDateTime(EffectiveTime_s) >= TransformDataHelper.TransformToDateTime(EffectiveTime_e))
                    {
                        return "当前设置的时间信息有误";
                    }
                    if (Id == 0)
                    {
                        if (string.IsNullOrWhiteSpace(UserName) || UserName.Length < 5)
                        {
                            return "用户名必填或者长度大于等于5";
                        }
                        AccountInfoEnt AccountInfo = accountManager.SearchAccountManagerinfo(UserName);
                        if (AccountInfo != null)
                        {
                            return "账号存在";
                        }
                        try
                        {
                            LoginInfoManager UserInfo = new LoginInfoManager();
                            AccountInfo = new AccountInfoEnt();
                            AccountInfo.Account = UserName;
                            AccountInfo.AccountName = AccountName;
                            AccountInfo.AccountStatus = 0;
                            AccountInfo.UnlockPwd = 0;
                            AccountInfo.PwdErrorCount = 5;
                            AccountInfo.StartTime = EffectiveTime_s;
                            AccountInfo.EndTime = EffectiveTime_e;
                            AccountInfo.Password = UserInfo.PassWordEncryption(SqlFilter.SuperFilter(UserName).Trim(), UserName);
                            result = accountManager.AddUserInfo(AccountInfo);
                            OperationContent = "新增用户: UserName" + UserName;
                            if (result > 0)
                            {
                                AccountInfo = accountManager.SearchAccountManagerinfo(UserName);
                                RoleManager roleManager = new FireFighting.BLL.RoleManager();
                                int Result = roleManager.UpdateRoleInfo(AccountInfo.Id, "1,2,3", 0);
                                Message = "修改成功";
                            }
                            else
                            {
                                Message = "添加异常，联系管理员";
                            }

                        }
                        catch (Exception ex)
                        {
                            Message = "新增账号异常";
                        }
                    }
                    else
                    {
                        result = accountManager.UpdateUserInfo(Id, AccountName, EffectiveTime_s, EffectiveTime_e);
                        if (result > 0)
                        {
                            Message = "修改成功";
                        }
                        else
                        {
                            Message = "保存失败";
                        }
                        OperationContent = "修改用户信息: AccountName" + AccountName;
                    }
                }
                oppo = new OperateUserInfo(LoginInfo);
                ErrorLog.InsertOperation(oppo.Account, OperationContent, 0, "用户管理");
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("SysManagerController", "AddOrEditUser", ex.Message.ToString(), "");
            }
            return Message;
        }

        #endregion 用户管理

        #region 权限设置

        public JsonResult RoleSetInfo(int Id)
        {
            RoleManager roleManager = new FireFighting.BLL.RoleManager();
            SysRetResultEnt sysRetResult = new SysRetResultEnt();
            SysRetResultEnt sysAllRetResult = new SysRetResultEnt();
            AllResult allResult = new Models.AllResult();
            List<ZTree> TreeLists = new List<ZTree>();
            List<ZTree> AllTreeLists = new List<ZTree>();
            List<SysPowerInfoModel> liSysPowerInfo = new List<SysPowerInfoModel>();
            if (Id <= 0)
            {
                return Json(liSysPowerInfo);
            }
            sysRetResult = roleManager.GetSysPower(Id);  //获取ID权限
            sysAllRetResult = roleManager.GetAllSysPower();  //获取所有权限

            if (sysRetResult != null)
            {
                ZTree TreeList;
                foreach (var item in sysRetResult.liSysPowerInfo)
                {
                    //if (item.PNodeID != 0 && !item.IsShow)
                    //{
                    //    continue;
                    //}
                    TreeList = new Models.ZTree();
                    TreeList.Name = item.NodeName;
                    TreeList.ParentTId = item.PNodeID.ToString();
                    TreeList.TId = item.PowerID.ToString();
                    TreeLists.Add(TreeList);
                }
            }
            if (sysAllRetResult != null)
            {
                ZTree TreeList;
                foreach (var item in sysAllRetResult.liSysPowerInfo)
                {
                    //if (item.PNodeID != 0 && !item.IsShow)
                    //{
                    //    continue;
                    //}
                    TreeList = new Models.ZTree();
                    TreeList.Name = item.NodeName;
                    TreeList.ParentTId = item.PNodeID.ToString();
                    TreeList.TId = item.PowerID.ToString();
                    AllTreeLists.Add(TreeList);
                }
            }

            foreach (var AllPower in AllTreeLists)
            {
                foreach (var RolePower in TreeLists)
                {
                    if (AllPower.TId == RolePower.TId)
                    {
                        AllPower.IsChecked = true;
                    }
                }
            }

            allResult.Data = AllTreeLists;
            return Json(allResult);
        }

        public string RoleSave(int Id, string pids)
        {
            string Str = "";
            try
            {
                if (Id <= 0)
                {
                    return "无效的修改";
                }
                AccountManager accountManager = new AccountManager();
                AccountInfoEnt AccountInfo = accountManager.SearchAccountById(Id);
                if (AccountInfo == null)
                {
                    return "无效的修改";
                }
                string pid = pids.TrimEnd(',');
                RoleManager roleManager = new FireFighting.BLL.RoleManager();
                //List<SysPowerByAccount> lisysPowerByAccount= roleManager.GetSysPowerByAccount(UID);  //获取ID权限

                int Result = roleManager.UpdateRoleInfo(Id, pid, 1);
                if (Result > 0)
                {
                    Str = "操作成功";
                }

            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("SysManagerController", "RoleSave", ex.Message.ToString(), "");
            }
            return Str;
        }
        #endregion

        public ActionResult AccountInfo()
        {
            AccountInfoModel accountInfoModel = LoginInfo.AccountInfoModel;
            return View(accountInfoModel);
        }

        /// <summary>
        /// 个人密码维护
        /// </summary>
        /// <param name="oldpwd"></param>
        /// <param name="newpwd"></param>
        /// <param name="anewpwd"></param>
        /// <returns></returns>
        public string AutPwdInfo(string oldpwd, string newpwd, string anewpwd)
        {
            try
            {
                oldpwd = SqlFilter.SuperFilter(oldpwd.Trim());
                newpwd = SqlFilter.SuperFilter(newpwd.Trim());
                anewpwd = SqlFilter.SuperFilter(anewpwd.Trim());
                AccountManager accountManager = new AccountManager();
                LoginInfoManager UserInfo = new LoginInfoManager();
                if (newpwd.Length <= 5)
                {
                    return "密码长度过短";
                }
                AccountInfoEnt accountInfoEnt = UserInfo.SearchAccountInfo(LoginInfo.AccountInfoModel.Account);
                if (accountInfoEnt != null)
                {
                    string m_oldpwd = UserInfo.PassWordEncryption(LoginInfo.AccountInfoModel.Account, SqlFilter.SuperFilter(oldpwd).Trim());
                    if (m_oldpwd == accountInfoEnt.Password)
                    {
                        if (m_oldpwd == newpwd)
                        {
                            return "原密码不能和新密码一致";
                        }
                        if (newpwd == anewpwd)
                        {
                            string m_newpwd = UserInfo.PassWordEncryption(LoginInfo.AccountInfoModel.Account, SqlFilter.SuperFilter(newpwd).Trim());
                            int Result = accountManager.CSHPWD(accountInfoEnt.Id, m_newpwd);
                            if (Result > 0)
                            {
                                string OperationContent = "修改自己帐号密码";
                                oppo = new OperateUserInfo(LoginInfo);
                                ErrorLog.InsertOperation(oppo.Account, OperationContent, 0, "个人信息");
                                return "修改成功";
                            }
                            else
                            {
                                return "修改失败";
                            }
                        }
                        else
                        {
                            return "新密码和确认密码不一致";
                        }
                    }
                    else
                    {
                        return "原密码错误";
                    }
                }
                else
                {
                    return "当前帐号信息有误";
                }

            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("SysManagerController", "AutPwdInfo", ex.Message.ToString(), "");
                return "操作异常";
            }
        }
    }
}