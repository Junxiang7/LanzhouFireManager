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
    [Authorization]
    public class SysToolsController : BaseController
    {
        /// <summary>
        /// 获取操作日志
        /// </summary>
        /// <returns></returns>
        public ActionResult OperatorLog(string OperatorDate = "")
        {
            ViewBag.DataLog = "";
            try
            {

                //操作日期
                if (OperatorDate != "")
                {
                    if (System.IO.File.Exists(Utils.GetMapPath("/data/" + DateTime.Parse(OperatorDate).ToString("yyyy-MM-dd") + ".txt")))
                    {
                        ViewBag.DataLog_Date = OperatorDate;
                        ViewBag.DataLog = System.IO.File.ReadAllText(Utils.GetMapPath("/data/" + DateTime.Parse(OperatorDate).ToString("yyyy-MM-dd") + ".txt"));
                    }

                }
            }
            catch
            {

            }
            return View();
        }

        /// <summary>
        /// 用户操作日志
        /// </summary>
        /// <param name="EffectiveTime_s"></param>
        /// <param name="EffectiveTime_e"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="Operator"></param>
        /// <returns></returns>
        public JsonResult UserListJsonOperator(string EffectiveTime_s, string EffectiveTime_e, int page = 1, int rows = 10, string Operator = "")
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
            Operator = SqlFilter.SuperFilter(Operator);
            SysToolsManager sysToolsManager = new SysToolsManager();
            OperationEntToModel OperationETM = new OperationEntToModel();
            List<OperationEnt> operationli = sysToolsManager.GetOperatorInfo(Operator, EffectiveTime_s, EffectiveTime_e, ref PageParameter);
            OperationInfoModel operationInfoModel = new Models.OperationInfoModel();
            operationInfoModel = OperationETM.ListOperationEntToModel(operationli);
            var jsondata = new { rows = operationInfoModel.liOperation, total = PageParameter.TotalCount };
            return Json(jsondata);
        }

        public ActionResult Operator()
        {
            return View();
        }

        /// <summary>
        /// 外部查询日志
        /// </summary>
        /// <param name="EffectiveTime_s"></param>
        /// <param name="EffectiveTime_e"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="Ip"></param>
        /// <returns></returns>
        public JsonResult UserListJsonExternalsearch(string EffectiveTime_s, string EffectiveTime_e, int page = 1, int rows = 10, string Ip = "")
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
            Ip = SqlFilter.SuperFilter(Ip);
            SysToolsManager sysToolsManager = new SysToolsManager();
            ExternalsearchEntToModel ExternalsearchETM = new ExternalsearchEntToModel();
            List<ExternalsearchEnt> Externalsearchli = sysToolsManager.GetExternalsearchInfo(Ip, EffectiveTime_s, EffectiveTime_e, ref PageParameter);
            ExternalsearchInfoModel externalsearchInfoModel = new Models.ExternalsearchInfoModel();
            externalsearchInfoModel = ExternalsearchETM.ListExternalsearchEntToModel(Externalsearchli);
            var jsondata = new { rows = externalsearchInfoModel.liExternalsearch, total = PageParameter.TotalCount };
            return Json(jsondata);
        }

        public ActionResult Externalsearch()
        {
            return View();
        }
    }
}