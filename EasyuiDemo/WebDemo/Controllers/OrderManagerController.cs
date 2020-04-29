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
    public class OrderManagerController : BaseController
    {
        // GET: OrderManager
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FirstWorkstation()
        {
            return View();
        }
        public JsonResult WorkstationOrder(string EffectiveTime_s, string EffectiveTime_e, int page = 1, int rows = 10, int OrderStatus = 0, int Type = 0, string ProductId = "", string EncodingOrder = "")
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
            OrderEntToModel OrderEToM = new Models.OrderEntToModel();
            OrderManager Ordermanager = new OrderManager();
            ProductId = SqlFilter.SuperFilter(ProductId);
            EncodingOrder = SqlFilter.SuperFilter(EncodingOrder);
            SelectOrderInfoEnt SelectOrderInfo = Ordermanager.GetSelectOrderInfo(OrderStatus, ProductId, EffectiveTime_s, EffectiveTime_e, EncodingOrder, Type, ref PageParameter);
            List<SelectOrder> liSelectOrder = OrderEToM.SelectOrderEntToModel(SelectOrderInfo);
            var jsondata = new { rows = liSelectOrder, total = PageParameter.TotalCount };
            return Json(jsondata);
        }

        public JsonResult GetCusInfo(int CusId)
        {
            OrderEntToModel OrderEToM = new Models.OrderEntToModel();
            OrderManager Ordermanager = new OrderManager();
            CusTomerEnt cusTomerEnt = Ordermanager.GetCusTomerInfo(CusId);
            CusTomerModel cusTomerModel = OrderEToM.CusTomerEntToModel(cusTomerEnt);
            return Json(cusTomerModel);
        }

        public JsonResult GetLogInfo(string ProductId)
        {
            OrderManager Ordermanager = new OrderManager();
            OrderEntToModel OrderEToM = new Models.OrderEntToModel();
            List<OrderLogModel> liOrderLog = new List<Models.OrderLogModel>();
            OrderLogInfoEnt OrderLogInfo = Ordermanager.GetOrderLogInfo(ProductId);
            liOrderLog = OrderEToM.OrderLogEntToModel(OrderLogInfo);
            return Json(liOrderLog);
        }
        public JsonResult UpdateStatus(int OrderStatus, int OrderId, string ProductId, int oldStatus)
        {
            OrderManager Ordermanager = new OrderManager();
            string Account = LoginInfo.AccountInfoModel.Account;
            string OperationalContent = "原状态：" + ((StatusEnum)oldStatus).ToString() + "改为：" + ((StatusEnum)OrderStatus).ToString();
            string OrderStatusText = ((StatusEnum)OrderStatus).ToString();
            int result = Ordermanager.UpdateStatusInfo(OrderStatus, OrderId, Account, ProductId, OperationalContent, OrderStatusText);
            if (OrderStatus == oldStatus)
            {
                result = 0;
            }
            var jsondata = new { Success = result };
            return Json(jsondata);
        }


        public ActionResult SecondWorkstation()
        {
            return View();
        }

        public ActionResult ThirdWorkstation()
        {
            return View();
        }

        public ActionResult FourthWorkstation()
        {
            return View();
        }

        public ActionResult SelectOrder()
        {
            return View();
        }

        public ActionResult OrderEntry()
        {
            return View();
        }

        /// <summary>
        /// 报废订单
        /// </summary>
        /// <returns></returns>
        public ActionResult SysScrapOrder()
        {
            return View();
        }

        /// <summary>
        /// 入库订单
        /// </summary>
        /// <returns></returns>
        public ActionResult SysInputOrder()
        {
            return View();
        }

        public JsonResult GetScrapInfo()
        {
            CusTomerManager cusTomerManager = new CusTomerManager();
            PageParameterServerEnt PageParameter = new PageParameterServerEnt();
            PageParameter.PageSize = 100;
            List<CusTomerEnt> liCusTomerEnt = cusTomerManager.GetCusTomerInfo("", ref PageParameter);
            return Json(liCusTomerEnt);
        }

        /// <summary>
        /// 选择确定 入库\销毁\维修
        /// </summary>
        /// <param name="Status"></param>
        /// <param name="InputNum"></param>
        /// <param name="Inputuser"></param>
        /// <returns></returns>
        public string ProductInput(string Status, string InputNum, string CusId, string EncodingOrder, string KgNum, string EncodeId)
        {
            string msg = string.Empty;
            OrderManager ordermanager = new OrderManager();
            int m_CusId = TransformDataHelper.TransformToInt(CusId); //TransformDataHelper.TransformToInt(Str_Inputuser[0].ToString());
            if (m_CusId <= 0)
            {
                return "无效的录入信息";
            }
            if (Status == "1001" || Status == "1006" || Status == "1007" || Status == "1008")
            {
                //string CusCompany = Str_Inputuser[1].ToString();
                string Account = LoginInfo.AccountInfoModel.Account;
                msg = ordermanager.AutProductInputInfo(m_CusId, Account, Status, InputNum, EncodingOrder, KgNum, EncodeId);
            }
            else
            {
                msg = "无效的状态信息";
            }
            return msg;
        }

        /// <summary>
        /// 批量导入
        /// </summary>
        /// <param name="SProductId"></param>
        /// <param name="EProductId"></param>
        /// <returns></returns>
        public string ProductBatchInput(string SProductId, string EProductId, string CusId, string EncodingOrder, string KgNum, string EncodeId)
        {
            string msg = string.Empty;
            OrderManager ordermanager = new OrderManager();
            int m_CusId = TransformDataHelper.TransformToInt(CusId);
            if (m_CusId <= 0)
            {
                return "无效的录入信息";
            }
            string Account = LoginInfo.AccountInfoModel.Account;
            msg = ordermanager.BatchAutProductInputInfo(m_CusId, Account, "1008", SProductId, EProductId, EncodingOrder, KgNum, EncodeId);
            return msg;
        }

        public JsonResult BatchAutKeyUpCode(string EncodingStart, string EncodingEnd)
        {
            string success = "False";
            string Message = string.Empty;
            int CusId = 0;
            string CusCompany = string.Empty;
            string EncodingOrder = string.Empty;
            int KgNum = 0;
            int EncodeId = 0;
            try
            {
                EncodingStart = SqlFilter.SuperFilter(EncodingStart.Trim());
                long m_EncodingStart = TransformDataHelper.TransformToLong(EncodingStart);
                EncodingEnd = SqlFilter.SuperFilter(EncodingEnd.Trim());
                long m_EncodingEnd = TransformDataHelper.TransformToLong(EncodingEnd);
                if (m_EncodingStart > 0 && m_EncodingEnd > 0 && m_EncodingEnd >= m_EncodingStart)
                {
                    EncodingManager encodingManager = new EncodingManager();
                    List<EncodingEnt> liEncodingEnt = encodingManager.GetEncodingData();
                    if (liEncodingEnt.Count > 0)
                    {
                        for (int i = 0; i < liEncodingEnt.Count; i++)
                        {
                            long a_EncodingSatrt = TransformDataHelper.TransformToLong(liEncodingEnt[i].EncodingSatrt);
                            long a_EncodingEnd = TransformDataHelper.TransformToLong(liEncodingEnt[i].EncodingEnd);
                            if ((a_EncodingSatrt <= m_EncodingStart && m_EncodingStart <= a_EncodingEnd) && (a_EncodingSatrt <= m_EncodingEnd && m_EncodingEnd <= a_EncodingEnd))
                            {
                                success = "True";
                                CusId = liEncodingEnt[i].CusId;
                                CusCompany = liEncodingEnt[i].CusCompany;
                                EncodingOrder = liEncodingEnt[i].EncodingOrder;
                                KgNum = liEncodingEnt[i].KgNum;
                                EncodeId = liEncodingEnt[i].Id;
                                break;
                            }
                        }
                        if (success == "False")
                        {
                            Message = "该范围区间未找到对应的商户";
                        }
                    }
                    else
                    {
                        Message = "未录入编码范围";
                    }
                }
                else
                {
                    Message = "无效的编码";
                }

            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("OrderManagerController", "BatchAutKeyUpCode", ex.Message.ToString(), EncodingStart + "|" + EncodingEnd);
                success = "False";
                Message = "异常操作";
            }
            var jsondata = new { Success = success, Message = Message, data = new { CusId = CusId, CusCompany = CusCompany, EncodingOrder = EncodingOrder, KgNum = KgNum, EncodeId = EncodeId } };
            return Json(jsondata);
        }

        /// <summary>
        /// 处理各工站数据
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public string OrderProcessInfo(string ProductId, string OrderStatus, string ProcessOrderStatus)
        {
            string Message = string.Empty;

            string Account = LoginInfo.AccountInfoModel.Account;
            try
            {
                OrderManager ordermanager = new OrderManager();
                Message = ordermanager.OrderProcessInfo(ProductId, Account, OrderStatus, ProcessOrderStatus);
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("OrderManagerController", "OrderProcessInfo", ex.Message.ToString(), ProductId);
                Message = "异常操作";
            }
            return Message;
        }

        /// <summary>
        /// 各工站处理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderProcess()
        {
            return View();
        }

        /// <summary>
        /// 入库失去焦点获取ID信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public JsonResult AutKeyUpCode(string code)
        {
            string success = "False";
            string Message = string.Empty;
            int CusId = 0;
            string CusCompany = string.Empty;
            string EncodingOrder = string.Empty;
            int KgNum = 0;
            int EncodeId = 0;
            try
            {
                string m_Code = SqlFilter.SuperFilter(code.Trim());
                long a_Code = TransformDataHelper.TransformToLong(m_Code);
                if (a_Code >= 0)
                {
                    EncodingManager encodingManager = new EncodingManager();
                    List<EncodingEnt> liEncodingEnt = encodingManager.GetEncodingData();
                    if (liEncodingEnt.Count > 0)
                    {
                        for (int i = 0; i < liEncodingEnt.Count; i++)
                        {
                            long a_EncodingSatrt = TransformDataHelper.TransformToLong(liEncodingEnt[i].EncodingSatrt);
                            long a_EncodingEnd = TransformDataHelper.TransformToLong(liEncodingEnt[i].EncodingEnd);
                            if (a_EncodingSatrt <= a_Code && a_Code <= a_EncodingEnd)
                            {
                                success = "True";
                                CusId = liEncodingEnt[i].CusId;
                                CusCompany = liEncodingEnt[i].CusCompany;
                                EncodingOrder = liEncodingEnt[i].EncodingOrder;
                                KgNum = liEncodingEnt[i].KgNum;
                                EncodeId = liEncodingEnt[i].Id;
                                break;
                            }
                        }
                        if (success == "False")
                        {
                            Message = "未找到对应的商户";
                        }
                    }
                    else
                    {
                        Message = "未录入编码范围";
                    }
                }
                else
                {
                    Message = "无效的编码";
                }

            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("OrderManagerController", "AutKeyUpCode", ex.Message.ToString(), code);
                success = "False";
                Message = "异常操作";
            }
            var jsondata = new { Success = success, Message = Message, data = new { CusId = CusId, CusCompany = CusCompany, EncodingOrder = EncodingOrder, KgNum = KgNum, EncodeId = EncodeId } };
            return Json(jsondata);
        }

        /// <summary>
        /// 各数据统计页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Statistics()
        {
            return View();
        }

        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="CusTomer"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="encodingOrder"></param>
        /// <param name="radio"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public JsonResult GetStatisticsData(string CusTomer, string StartTime, string EndTime, string encodingOrder, string radio, int page = 1, int rows = 10)
        {
            OrderManager Ordermanager = new OrderManager();
            PageParameterServerEnt PageParameter = new PageParameterServerEnt(); //服务器分页实例化
            CusTomer = SqlFilter.FilteSQLStr(CusTomer);
            encodingOrder = SqlFilter.FilteSQLStr(encodingOrder);
            List<StatisticsEnt> liStatistics = Ordermanager.GetStatisticsInfo(CusTomer, StartTime, EndTime, radio, encodingOrder, page = 1, rows = 10, ref PageParameter);
            StatisticsEntToModel StatisticsETM = new Models.StatisticsEntToModel();
            List<StatisticsModel> listatisticsModel = StatisticsETM.GetStatisticsToModel(liStatistics);
            var jsondata = new { rows = listatisticsModel, total = listatisticsModel.Count };
            return Json(jsondata);
        }
    }
}