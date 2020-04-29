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
    public class OrderManager
    {
        /// <summary>
        /// 订单查询
        /// </summary>
        /// <param name="OrderStatus"></param>
        /// <param name="ProductId"></param>
        /// <param name="EffectiveTime_s"></param>
        /// <param name="EffectiveTime_e"></param>
        /// <param name="EncodingOrder"></param>
        /// <param name="PageParameter"></param>
        /// <returns></returns>
        public SelectOrderInfoEnt GetSelectOrderInfo(int OrderStatus, string ProductId, string EffectiveTime_s, string EffectiveTime_e, string EncodingOrder, int Type, ref PageParameterServerEnt PageParameter)
        {
            SelectOrderInfoEnt SelectOrderInfo = new SelectOrderInfoEnt();
            OrderManagerServer orderManagerServer = new OrderManagerServer();
            try
            {
                StringBuilder SbStr = new StringBuilder();
                if (string.IsNullOrEmpty(EffectiveTime_s))
                {
                    EffectiveTime_s = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                }
                if (string.IsNullOrEmpty(EffectiveTime_e))
                {
                    EffectiveTime_e = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (TransformDataHelper.TransformToDateTime(EffectiveTime_s) > TransformDataHelper.TransformToDateTime(EffectiveTime_e))
                {
                    EffectiveTime_s = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                    EffectiveTime_e = DateTime.Now.ToString("yyyy-MM-dd");
                }
                EffectiveTime_s = EffectiveTime_s + " 00:00:000";
                EffectiveTime_e = EffectiveTime_e + " 23:59:999";
                SbStr.Append(" CreateTime >'" + EffectiveTime_s + "' and CreateTime<'" + EffectiveTime_e + "' ");
                if (OrderStatus != 0)
                {
                    string m_OrderStatus = OrderStatus.ToString();
                    if (OrderStatus == 1006)
                    {
                        m_OrderStatus = "1006,1009";   //入库报废和手工报废
                    }
                    SbStr.Append(" AND OrderStatus in (" + m_OrderStatus + ") ");
                }
                if (!string.IsNullOrEmpty(ProductId))
                {
                    SbStr.Append(" AND ProductId='" + ProductId + "' ");
                }
                if (!string.IsNullOrEmpty(EncodingOrder))
                {
                    SbStr.Append(" AND EncodingOrder='" + EncodingOrder + "' ");
                }
                SbStr.Append(" AND Del=0 ");
                if (Type==0)  //0 查询当前状态   1 查询所有状态
                {
                    SbStr.Append(" and Hide=0 ");
                }

                string sqlwhere = SbStr.ToString();
                PageParameter.Where = sqlwhere;
                SelectOrderInfo = orderManagerServer.GetSelectOrderInfoservice(ref PageParameter);
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("OrderManager", "GetSelectOrderInfo", ex.Message.ToString(), ProductId);
            }

            return SelectOrderInfo;
        }

        /// <summary>
        /// 根据ID获取客户信息
        /// </summary>
        /// <param name="CusId"></param>
        /// <returns></returns>
        public CusTomerEnt GetCusTomerInfo(int CusId)
        {
            OrderManagerServer orderManagerServer = new OrderManagerServer();
            return orderManagerServer.GetCusTomerService(CusId);
        }

        /// <summary>
        /// 根据商品ID获取订单日志
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public OrderLogInfoEnt GetOrderLogInfo(string ProductId)
        {
            OrderManagerServer orderManagerServer = new OrderManagerServer();
            return orderManagerServer.GetOrderLogService(ProductId);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="OrderStatus"></param>
        /// <param name="OrderId"></param>
        /// <param name="Account"></param>
        /// <param name="ProductId"></param>
        /// <param name="OperationalContent"></param>
        /// <param name="OrderStatusText"></param>
        /// <returns></returns>
        public int UpdateStatusInfo(int OrderStatus, int OrderId, string Account, string ProductId, string OperationalContent, string OrderStatusText)
        {
            OrderManagerServer orderManagerServer = new OrderManagerServer();
            return orderManagerServer.UpdateStatusService(OrderStatus, OrderId, Account, ProductId, OperationalContent, OrderStatusText);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SelectOrderEnt"></param>
        /// <param name="Account"></param>
        /// <returns></returns>
        public int ProductInputInfo(SelectOrderEnt SelectOrderEnt, string Account)
        {
            OrderManagerServer orderManagerServer = new OrderManagerServer();
            return orderManagerServer.ProductInputService(SelectOrderEnt, Account);
        }

        /// <summary>
        /// s商品第一工站
        /// </summary>
        /// <param name="m_CusId"></param>
        /// <param name="Account"></param>
        /// <param name="Status"></param>
        /// <param name="InputNum"></param>
        /// <param name="EncodingOrder"></param>
        /// <param name="KgNum"></param>
        /// <param name="EncodeId"></param>
        /// <returns></returns>
        public string AutProductInputInfo(int m_CusId, string Account, string Status, string InputNum, string EncodingOrder, string KgNum, string EncodeId)
        {
            SelectOrderEnt selectOrderEnt = new SelectOrderEnt();
            EncodingManager encodingManager = new BLL.EncodingManager();
            string msg = string.Empty;
            try
            {
                CusTomerManager cusTomerManager = new CusTomerManager();
                CusTomerEnt cusTomerEnt = cusTomerManager.GetCusTomerById(m_CusId);
                if (cusTomerEnt == null)
                {
                    msg = "无效的录入信息";
                    return msg;
                }

                EncodingEnt encodingEnt = encodingManager.GetEncodingByEncodeId(EncodeId);
                //EncodingEnt encodingEnt = encodingManager.GetEncodingByOrder(EncodingOrder);
                if (encodingEnt == null)
                {
                    msg = "无效的编码";
                    return msg;
                }
                if (encodingEnt.CusId != m_CusId || encodingEnt.KgNum != TransformDataHelper.TransformToInt(KgNum))
                {
                    msg = "无效的信息";
                    return msg;
                }
                long m_sorder = TransformDataHelper.TransformToLong(encodingEnt.EncodingSatrt);
                long m_eorder = TransformDataHelper.TransformToLong(encodingEnt.EncodingEnd);
                long m_InputNum = TransformDataHelper.TransformToLong(InputNum);
                if (m_sorder <= m_InputNum && m_InputNum <= m_eorder)
                {
                    selectOrderEnt.OrderStatus = TransformDataHelper.TransformToInt(Status);
                    selectOrderEnt.ProductId = SqlFilter.SqlStrFilter(InputNum);
                    selectOrderEnt.CusId = m_CusId;
                    selectOrderEnt.CusCompany = cusTomerEnt.CusCompany;
                    selectOrderEnt.EncodingOrder = EncodingOrder;
                    selectOrderEnt.KgNum = TransformDataHelper.TransformToInt(KgNum);
                    SelectOrderEnt input_selectOrder = GetselectOrder(InputNum);
                    if (input_selectOrder != null)
                    {
                        msg = "该商品已经被操作";
                        return msg;
                    }

                    int result = ProductInputInfo(selectOrderEnt, Account);
                    if (result > 0)
                    {
                        msg = "操作成功";
                    }
                    else
                    {
                        msg = "操作失败";
                    }
                }
                else
                {
                    msg = "无效的范围";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("OrderManager", "AutProductInputInfo", ex.Message.ToString(), Account);
                msg = "操作异常";
            }
            return msg;
        }

        /// <summary>
        /// s商品第一工站
        /// </summary>
        /// <param name="m_CusId"></param>
        /// <param name="Account"></param>
        /// <param name="Status"></param>
        /// <param name="InputNum"></param>
        /// <param name="EncodingOrder"></param>
        /// <param name="KgNum"></param>
        /// <param name="EncodeId"></param>
        /// <returns></returns>
        public string BatchAutProductInputInfo(int m_CusId, string Account, string Status, string SProductId, string EProductId, string EncodingOrder, string KgNum, string EncodeId)
        {
            EncodingManager encodingManager = new BLL.EncodingManager();
            string msg = string.Empty;
            try
            {
                CusTomerManager cusTomerManager = new CusTomerManager();
                CusTomerEnt cusTomerEnt = cusTomerManager.GetCusTomerById(m_CusId);
                if (cusTomerEnt == null)
                {
                    msg = "无效的录入信息";
                    return msg;
                }

                EncodingEnt encodingEnt = encodingManager.GetEncodingByEncodeId(EncodeId);
                //EncodingEnt encodingEnt = encodingManager.GetEncodingByOrder(EncodingOrder);
                if (encodingEnt == null)
                {
                    msg = "无效的编码";
                    return msg;
                }
                if (encodingEnt.CusId != m_CusId || encodingEnt.KgNum != TransformDataHelper.TransformToInt(KgNum))
                {
                    msg = "无效的信息";
                    return msg;
                }
                long m_sorder = TransformDataHelper.TransformToLong(encodingEnt.EncodingSatrt);
                long m_eorder = TransformDataHelper.TransformToLong(encodingEnt.EncodingEnd);
                long m_SInputNum = TransformDataHelper.TransformToLong(SProductId);
                long m_EInputNum = TransformDataHelper.TransformToLong(EProductId);
                if (m_EInputNum < m_SInputNum)
                {
                    msg = "ID录入区间有误";
                    return msg;
                }
                if ((m_sorder <= m_SInputNum && m_SInputNum <= m_eorder) && (m_sorder <= m_EInputNum && m_EInputNum <= m_eorder))
                {
                    string StrProductId = string.Empty;
                    int Total = TransformDataHelper.TransformToInt((m_EInputNum - m_SInputNum + 1).ToString());
                    if (Total > 25)
                    {
                        msg = "ID数大于25，核实后重新录入";
                        return msg;
                    }
                    for (int i = 0; i < Total; i++)
                    {
                        SelectOrderEnt selectOrderEnt = new SelectOrderEnt();
                        selectOrderEnt.OrderStatus = TransformDataHelper.TransformToInt(Status);
                        selectOrderEnt.ProductId = SqlFilter.SqlStrFilter(m_SInputNum.ToString());
                        selectOrderEnt.CusId = m_CusId;
                        selectOrderEnt.CusCompany = cusTomerEnt.CusCompany;
                        selectOrderEnt.EncodingOrder = EncodingOrder;
                        selectOrderEnt.KgNum = TransformDataHelper.TransformToInt(KgNum);
                        SelectOrderEnt input_selectOrder = GetselectOrder(m_SInputNum.ToString());
                        if (input_selectOrder != null)
                        {
                            StrProductId += m_SInputNum + ",";
                            continue;
                            //msg = "该商品已经被操作";
                            //return msg;
                        }
                        int result = ProductInputInfo(selectOrderEnt, Account);
                        if (result <= 0)
                        {
                            StrProductId += m_SInputNum + ",";
                        }
                        m_SInputNum++;
                        if (m_SInputNum > m_EInputNum)
                        {
                            break;
                        }
                    }
                    if (string.IsNullOrEmpty(StrProductId))
                    {
                        msg = "操作成功";
                    }
                    else
                    {
                        msg = "操作失败订单:" + StrProductId.TrimEnd(',');
                        WriteLog.WriteTxt("BatchInput", "StrProductId");
                    }
                }
                else
                {
                    msg = "无效的范围";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("OrderManager", "AutProductInputInfo", ex.Message.ToString(), Account);
                msg = "操作异常";
            }
            return msg;
        }

        /// <summary>
        /// 根据商品ID查询订单
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public SelectOrderEnt GetselectOrder(string ProductId)
        {
            OrderManagerServer orderManagerServer = new OrderManagerServer();
            return orderManagerServer.GetselectOrder(ProductId);
        }

        /// <summary>
        /// 各工站处理
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="Account"></param>
        /// <param name="OrderStatus"></param>
        /// <param name="ProcessOrderStatus"></param>
        /// <returns></returns>
        public string OrderProcessInfo(string ProductId, string Account, string OrderStatus, string ProcessOrderStatus)
        {
            string message = string.Empty;
            int OldOrderStatus = 0;
            try
            {
                OrderManagerServer orderManagerServer = new OrderManagerServer();
                //SelectOrderEnt m_selectOrderEnt = GetselectOrder(ProductId);
                List<SelectOrderEnt> liSelectOrder = orderManagerServer.GetSelectOrderList(ProductId);
                if (liSelectOrder == null || liSelectOrder.Count == 0)
                {
                    return "该产品还未入库操作";
                }
                SelectOrderEnt SelectOrderEnt = new SelectOrderEnt();
                List<SelectOrderEnt> m_liSelectOrder = new List<SelectOrderEnt>();
                if (OrderStatus == "1002")
                {
                    #region 操作第二工站
                    m_liSelectOrder = liSelectOrder.Where(a => a.OrderStatus == 1001).ToList();
                    if (m_liSelectOrder.Count == 1 && liSelectOrder.Count == 1)
                    {
                        if (ProcessOrderStatus == "1002" || ProcessOrderStatus == "1009")
                        {
                            SelectOrderEnt.OrderStatus = TransformDataHelper.TransformToInt(ProcessOrderStatus);  //新增
                            OldOrderStatus = m_liSelectOrder[0].OrderStatus;   //修改
                            SelectOrderEnt.EncodingOrder = m_liSelectOrder[0].EncodingOrder;
                            SelectOrderEnt.ProductId = m_liSelectOrder[0].ProductId;
                            SelectOrderEnt.CusId = m_liSelectOrder[0].CusId;
                            SelectOrderEnt.CusCompany = m_liSelectOrder[0].CusCompany;
                            SelectOrderEnt.KgNum = m_liSelectOrder[0].KgNum;
                        }
                        else
                        {
                            return "无效的处理状态";
                        }
                    }
                    else
                    {
                        return "该商品已被操作";
                    }
                    #endregion
                }
                else if (OrderStatus == "1003")
                {
                    #region 操作第三工站
                    m_liSelectOrder = liSelectOrder.Where(a => a.OrderStatus == 1002 || a.OrderStatus == 1007).ToList();
                    if (m_liSelectOrder.Count == 1 && m_liSelectOrder[0].hide == 0)
                    {
                        if (ProcessOrderStatus == "1003" || ProcessOrderStatus == "1009")
                        {
                            SelectOrderEnt.OrderStatus = TransformDataHelper.TransformToInt(ProcessOrderStatus);  //新增
                            OldOrderStatus = m_liSelectOrder[0].OrderStatus;   //修改
                            SelectOrderEnt.EncodingOrder = m_liSelectOrder[0].EncodingOrder;
                            SelectOrderEnt.ProductId = m_liSelectOrder[0].ProductId;
                            SelectOrderEnt.CusId = m_liSelectOrder[0].CusId;
                            SelectOrderEnt.CusCompany = m_liSelectOrder[0].CusCompany;
                            SelectOrderEnt.KgNum = m_liSelectOrder[0].KgNum;
                        }
                        else
                        {
                            return "无效的处理状态";
                        }
                    }
                    else
                    {
                        return "该商品已被操作或还未进入上一个工作站";
                    }
                    #endregion
                }
                else if (OrderStatus == "1004")
                {
                    #region 操作第四工站
                    m_liSelectOrder = liSelectOrder.Where(a => a.OrderStatus == 1003 || a.OrderStatus == 1008).ToList();
                    if (m_liSelectOrder.Count == 1 && m_liSelectOrder[0].hide == 0)
                    {
                        SelectOrderEnt.OrderStatus = 1004;  //新增
                        OldOrderStatus = m_liSelectOrder[0].OrderStatus;   //修改
                        SelectOrderEnt.EncodingOrder = m_liSelectOrder[0].EncodingOrder;
                        SelectOrderEnt.ProductId = m_liSelectOrder[0].ProductId;
                        SelectOrderEnt.CusId = m_liSelectOrder[0].CusId;
                        SelectOrderEnt.CusCompany = m_liSelectOrder[0].CusCompany;
                        SelectOrderEnt.KgNum = m_liSelectOrder[0].KgNum;
                    }
                    else
                    {
                        return "该商品已被操作或还未进入上一个工作站";
                    }
                    #endregion
                }
                else
                {
                    return "无对应工站操作";
                }
                int result = orderManagerServer.OrderProcessService(SelectOrderEnt, Account, OldOrderStatus);
                if (result > 0)
                {
                    message = "操作成功";
                    if (SelectOrderEnt.OrderStatus == 1004)
                    {
                        message = "操作成功:[" + SelectOrderEnt.ProductId + "]完成出库，商户【" + SelectOrderEnt.CusCompany + "】";
                    }
                    return message;
                }
                else
                {
                    return "操作异常";
                }
            }
            catch (Exception ex)
            {
                message = "操作异常";
            }
            return message;
        }

        #region 统计
        /// <summary>
        /// 统计各处理
        /// </summary>
        /// <param name="CusTomer"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="radio"></param>
        /// <param name="encodingOrder"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="PageParameter"></param>
        /// <returns></returns>
        public List<StatisticsEnt> GetStatisticsInfo(string CusTomer, string StartTime, string EndTime, string radio, string encodingOrder, int page, int rows, ref PageParameterServerEnt PageParameter)
        {
            OrderManagerServer orderManagerServer = new OrderManagerServer();
            List<StatisticsEnt> liStatistics = new List<StatisticsEnt>();
            try
            {
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
                StringBuilder SbStr = new StringBuilder();
                if (string.IsNullOrEmpty(StartTime))
                {
                    StartTime = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (string.IsNullOrEmpty(EndTime))
                {
                    EndTime = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (TransformDataHelper.TransformToDateTime(StartTime) > TransformDataHelper.TransformToDateTime(EndTime))
                {
                    StartTime = DateTime.Now.ToString("yyyy-MM-dd");
                    EndTime = DateTime.Now.ToString("yyyy-MM-dd");
                }
                string m_StartTime = StartTime;
                string m_EndTime = EndTime;
                StartTime = StartTime + " 00:00:000";
                EndTime = EndTime + " 23:59:999";
                SbStr.Append(" CreateTime >'" + StartTime + "' and CreateTime<'" + EndTime + "' ");
                SbStr.Append("  and Del=0 ");
                if (radio == "0")  //计算每天
                {
                    PageParameter.Where = SbStr.ToString();
                    List<StatisticsDayEnt> listatisticsDay = new List<StatisticsDayEnt>();
                    listatisticsDay = orderManagerServer.GetStatisticsDayService(ref PageParameter);
                    if (listatisticsDay != null && listatisticsDay.Count > 0)
                    {
                        liStatistics = liStatisticsDayToEnt(listatisticsDay, m_StartTime, m_EndTime);
                    }
                }
                else if (radio == "1")  //根据用户生成数据
                {
                    if (!string.IsNullOrEmpty(CusTomer))
                    {
                        SbStr.Append(" and CusCompany like '%" + CusTomer + "%' ");
                    }
                    if (!string.IsNullOrEmpty(encodingOrder))
                    {
                        SbStr.Append(" and EncodingOrder ='" + encodingOrder + "' ");
                    }
                    PageParameter.Where = SbStr.ToString();
                    List<StatisticsDayEnt> listatisticsDay = new List<StatisticsDayEnt>();
                    listatisticsDay = orderManagerServer.GetStatisticsInfoService(ref PageParameter);
                    //Dictionary<string, List<StatisticsDayEnt>> dicStatisticsDay = orderManagerServer.GetStatisticsService(ref PageParameter);
                    if (listatisticsDay != null && listatisticsDay.Count > 0)
                    {
                        liStatistics = liStatisticsToEnt(listatisticsDay);
                    }
                }
                else if (radio == "2")
                {
                    if (!string.IsNullOrEmpty(CusTomer))
                    {
                        SbStr.Append(" and CusCompany like '%" + CusTomer + "%' ");
                    }
                    if (!string.IsNullOrEmpty(encodingOrder))
                    {
                        SbStr.Append(" and EncodingOrder ='" + encodingOrder + "' ");
                    }
                    PageParameter.Where = SbStr.ToString();
                    List<StatisticsDayEnt> listatisticsDay = new List<StatisticsDayEnt>();
                    listatisticsDay = orderManagerServer.GetStatisticsByKgService(ref PageParameter);
                    if (listatisticsDay.Count > 0)
                    {
                        liStatistics = liStatisticsByKg(listatisticsDay);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("OrderManager", "GetStatisticsInfo", ex.Message.ToString(), CusTomer);
            }
            return liStatistics;
        }

        public List<StatisticsEnt> liStatisticsDayToEnt(List<StatisticsDayEnt> listatisticsDay, string StatrTime, string EndTime)
        {
            List<StatisticsEnt> liStatistics = new List<StatisticsEnt>();

            try
            {
                StatisticsEnt statistics = new StatisticsEnt();
                statistics.QueryTime = StatrTime + "/" + EndTime;
                for (int i = 0; i < listatisticsDay.Count; i++)
                {

                    if (listatisticsDay[i].OrderStatus == "1001" || listatisticsDay[i].OrderStatus == "1006" || listatisticsDay[i].OrderStatus == "1007" || listatisticsDay[i].OrderStatus == "1008")
                    {
                        statistics.FirstWorkstation += listatisticsDay[i].OrderStatusCount;
                    }
                    else if (listatisticsDay[i].OrderStatus == "1002")
                    {
                        statistics.SecondWorkstation = listatisticsDay[i].OrderStatusCount;
                    }
                    else if (listatisticsDay[i].OrderStatus == "1003")
                    {
                        statistics.ThirdWorkstation = listatisticsDay[i].OrderStatusCount;
                    }
                    else if (listatisticsDay[i].OrderStatus == "1004")
                    {
                        statistics.FourthWorkstation = listatisticsDay[i].OrderStatusCount;
                    }
                    if (listatisticsDay[i].OrderStatus == "1006" || listatisticsDay[i].OrderStatus == "1009")
                    {
                        statistics.ScrapNum += listatisticsDay[i].OrderStatusCount;
                    }

                }
                liStatistics.Add(statistics);
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("OrderManager", "liStatisticsDayToEnt", ex.Message.ToString(), "");
            }
            return liStatistics;
        }

        public List<StatisticsEnt> liStatisticsByKg(List<StatisticsDayEnt> listatisticsDay)
        {

            List<StatisticsEnt> liStatistics = new List<StatisticsEnt>();
            try
            {
                for (int i = 0; i < listatisticsDay.Count; i++)
                {
                    int SumKG = 0;
                    StatisticsEnt statistics = new StatisticsEnt();
                    List<StatisticsEnt> liExist = liStatistics.Where(a => a.CusId == listatisticsDay[i].CusId && a.EncodingOrder == listatisticsDay[i].EncodingOrder).ToList();
                    if (liExist.Count > 0)  //代表存在
                    {
                        continue;
                    }
                    List<StatisticsDayEnt> m_liStatistics = listatisticsDay.Where(a => a.CusId == listatisticsDay[i].CusId && a.EncodingOrder == listatisticsDay[i].EncodingOrder).ToList();
                    for (int j = 0; j < m_liStatistics.Count; j++)
                    {
                        statistics.CusId = m_liStatistics[j].CusId;
                        statistics.CusTomer = m_liStatistics[j].CusCompany;
                        statistics.EncodingOrder = m_liStatistics[j].EncodingOrder;
                        if (m_liStatistics[j].KgNum == 1)
                        {
                            statistics.Kg_1 += m_liStatistics[j].KgNumCount;
                            SumKG += m_liStatistics[j].KgNumCount * 1;
                        }
                        else if (m_liStatistics[j].KgNum == 2)
                        {
                            statistics.Kg_2 += m_liStatistics[j].KgNumCount;
                            SumKG += m_liStatistics[j].KgNumCount * 2;
                        }
                        else if (m_liStatistics[j].KgNum == 3)
                        {
                            statistics.Kg_3 += m_liStatistics[j].KgNumCount;
                            SumKG += m_liStatistics[j].KgNumCount * 3;
                        }
                        else if (m_liStatistics[j].KgNum == 4)
                        {
                            statistics.Kg_4 += m_liStatistics[j].KgNumCount;
                            SumKG += m_liStatistics[j].KgNumCount * 4;
                        }
                        else if (m_liStatistics[j].KgNum == 5)
                        {
                            statistics.Kg_5 += m_liStatistics[j].KgNumCount;
                            SumKG += m_liStatistics[j].KgNumCount * 5;
                        }
                        else if (m_liStatistics[j].KgNum == 8)
                        {
                            statistics.Kg_8 += m_liStatistics[j].KgNumCount;
                            SumKG += m_liStatistics[j].KgNumCount * 8;
                        }
                        else if (m_liStatistics[j].KgNum == 20)
                        {
                            statistics.Kg_20 += m_liStatistics[j].KgNumCount;
                            SumKG += m_liStatistics[j].KgNumCount * 20;
                        }
                        else if (m_liStatistics[j].KgNum == 35)
                        {
                            statistics.Kg_35 += m_liStatistics[j].KgNumCount;
                            SumKG += m_liStatistics[j].KgNumCount * 35;
                        }
                        else if (m_liStatistics[j].KgNum == 50)
                        {
                            statistics.Kg_50 += m_liStatistics[j].KgNumCount;
                            SumKG += m_liStatistics[j].KgNumCount * 50;
                        }
                    }
                    statistics.SumKG = SumKG;
                    liStatistics.Add(statistics);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("OrderManager", "liStatisticsToEnt", ex.Message.ToString(), "");
            }

            return liStatistics;
        }

        public List<StatisticsEnt> liStatisticsToEnt(List<StatisticsDayEnt> listatisticsDay)
        {
            List<StatisticsEnt> liStatistics = new List<StatisticsEnt>();
            try
            {
                for (int i = 0; i < listatisticsDay.Count; i++)
                {
                    StatisticsEnt statistics = new StatisticsEnt();
                    List<StatisticsEnt> liExist = liStatistics.Where(a => a.CusId == listatisticsDay[i].CusId && a.EncodingOrder == listatisticsDay[i].EncodingOrder).ToList();
                    if (liExist.Count > 0)  //代表存在
                    {
                        continue;
                    }
                    List<StatisticsDayEnt> m_liStatistics = listatisticsDay.Where(a => a.CusId == listatisticsDay[i].CusId && a.EncodingOrder == listatisticsDay[i].EncodingOrder).ToList();
                    for (int j = 0; j < m_liStatistics.Count; j++)
                    {
                        statistics.CusId = m_liStatistics[j].CusId;
                        statistics.CusTomer = m_liStatistics[j].CusCompany;
                        statistics.EncodingOrder = m_liStatistics[j].EncodingOrder;
                        if (m_liStatistics[j].OrderStatus == "1001" || m_liStatistics[j].OrderStatus == "1006" || m_liStatistics[j].OrderStatus == "1007" || m_liStatistics[j].OrderStatus == "1008")
                        {
                            statistics.FirstWorkstation += m_liStatistics[j].OrderStatusCount;
                        }
                        else if (m_liStatistics[j].OrderStatus == "1002")
                        {
                            statistics.SecondWorkstation = m_liStatistics[j].OrderStatusCount;
                        }
                        else if (m_liStatistics[j].OrderStatus == "1003")
                        {
                            statistics.ThirdWorkstation = m_liStatistics[j].OrderStatusCount;
                        }
                        else if (m_liStatistics[j].OrderStatus == "1004")
                        {
                            statistics.FourthWorkstation = m_liStatistics[j].OrderStatusCount;
                        }
                        if (m_liStatistics[j].OrderStatus == "1006" || m_liStatistics[j].OrderStatus == "1009")
                        {
                            statistics.ScrapNum = m_liStatistics[j].OrderStatusCount;
                        }
                    }
                    liStatistics.Add(statistics);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("OrderManager", "liStatisticsToEnt", ex.Message.ToString(), "");
            }

            return liStatistics;
        }

        public List<StatisticsEnt> liStatisticsDicToEnt(Dictionary<string, List<StatisticsDayEnt>> dicStatistics)
        {
            List<StatisticsEnt> liStatistics = new List<StatisticsEnt>();
            try
            {
                foreach (var item in dicStatistics)
                {
                    if (item.Key == "1001")
                    {
                        for (int i = 0; i < item.Value.Count; i++)
                        {
                            StatisticsEnt statistics = new StatisticsEnt();
                            statistics.FirstWorkstation = item.Value[i].OrderStatusCount;
                            statistics.CusTomer = item.Value[i].CusCompany;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("OrderManager", "liStatisticsDicToEnt", ex.Message.ToString(), "");
            }
            return liStatistics;
        }

        #endregion
    }
}
