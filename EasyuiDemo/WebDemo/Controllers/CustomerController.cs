using FireFighting.BLL;
using FireFighting.NET;
using FireFighting.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDemo.Models;

namespace WebDemo.Controllers
{
    public class CustomerController : BaseController
    {
        OperateUserInfo oppo = null;
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 客户展示页面
        /// </summary>
        /// <returns></returns>
        public ActionResult CustomerInput()
        {
            return View();
        }

        /// <summary>
        /// 客户查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="CusCompany"></param>
        /// <returns></returns>
        public JsonResult UserListJson(int page = 1, int rows = 10, string CusCompany = "")
        {
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
            CusCompany = SqlFilter.SuperFilter(CusCompany);
            CusTomerManager cusTomerManager = new CusTomerManager();
            OrderEntToModel OrderETM = new OrderEntToModel();
            List<CusTomerEnt> cusTomerli = cusTomerManager.GetCusTomerInfo(CusCompany, ref PageParameter);
            CusTomerInfoModel cusTomerInfoModel = new Models.CusTomerInfoModel();
            cusTomerInfoModel = OrderETM.ListCusTomerEntToModel(cusTomerli);
            var jsondata = new { rows = cusTomerInfoModel.liCusTomer, total = PageParameter.TotalCount };
            return Json(jsondata);
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
        public string AddOrEditUser(int CusId, string CusCompany, string CusLinkMan, string CusLinkTel, string CusLinkPhone, int UpType)
        {
            string OperationContent = "";
            int result = 0;
            CusTomerManager cusTomerManager = new CusTomerManager();
            if (UpType == 2)  //代表
            {
                if (CusId <= 0)
                {
                    return "删除信息有误";
                }
                OperationContent = "删除客户 CusId：" + CusId;
                result = cusTomerManager.DeleteCusTomer(CusId);
            }
            else  //添加
            {
                if (CusId != 0)
                {
                    if (CusId <= 0)
                    {
                        return "修改信息有误";
                    }
                    OperationContent = "修改客户信息 CusId：" + CusId + "|CusLinkMan:" + CusLinkMan + "|CusLinkTel:" + CusLinkTel + "|CusLinkPhone:" + CusLinkPhone;
                    result = cusTomerManager.UpdateCusTomer(CusId, CusLinkMan, CusLinkTel, CusLinkPhone);
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(CusLinkMan))
                    {
                        return "联系人不能为空";
                    }
                    if (string.IsNullOrWhiteSpace(CusCompany))
                    {
                        return "公司不能为空";
                    }
                    CusCompany = SqlFilter.SuperFilter(CusCompany.Trim());
                    CusLinkMan = SqlFilter.SuperFilter(CusLinkMan.Trim());
                    CusLinkTel = SqlFilter.SuperFilter(CusLinkTel.Trim());
                    CusLinkPhone = SqlFilter.SuperFilter(CusLinkPhone.Trim());
                    CusTomerEnt cusTomerEnt = cusTomerManager.SearchCusTomerinfo(CusCompany);
                    if (cusTomerEnt != null)
                    {
                        return "公司已经存在";
                    }
                    try
                    {
                        cusTomerEnt = new CusTomerEnt();
                        cusTomerEnt.CusCompany = CusCompany;
                        cusTomerEnt.CusLinkMan = CusLinkMan;
                        cusTomerEnt.CusLinkPhone = CusLinkPhone;
                        cusTomerEnt.CusLinkTel = CusLinkTel;
                        result = cusTomerManager.AddCusTomer(cusTomerEnt);
                        OperationContent = "添加客户信息 CusCompany：" + CusCompany + "|CusLinkMan:" + CusLinkMan + "|CusLinkTel:" + CusLinkTel + "|CusLinkPhone:" + CusLinkPhone;
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.InsertError("CustomerController", "AddOrEditUser", ex.Message.ToString(), "");
                        return "新增信息异常";
                    }
                }
            }
            if (result > 0)
            {
                oppo = new OperateUserInfo(LoginInfo);
                ErrorLog.InsertOperation(oppo.Account, OperationContent, 0, "客户管理");
                return "操作成功";
            }
            else
            {
                return "操作失败，联系管理员";
            }
        }

    }
}