using FireFighting.NET;
using FireFighting.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Models
{
    public class OrderEntToModel
    {
        public List<SelectOrder> SelectOrderEntToModel(SelectOrderInfoEnt selectOrderInfoEnt)
        {
            List<SelectOrder> liSelectOrder = new List<Models.SelectOrder>();
            try
            {
                if (selectOrderInfoEnt != null && selectOrderInfoEnt.liSelectOrder != null && selectOrderInfoEnt.liSelectOrder.Count > 0)
                {
                    for (int i = 0; i < selectOrderInfoEnt.liSelectOrder.Count; i++)
                    {
                        SelectOrder selectOrder = new SelectOrder();
                        selectOrder.CusCompany = selectOrderInfoEnt.liSelectOrder[i].CusCompany;
                        selectOrder.CusId = selectOrderInfoEnt.liSelectOrder[i].CusId;
                        selectOrder.OrderId = selectOrderInfoEnt.liSelectOrder[i].OrderId;
                        selectOrder.OrderStatus = selectOrderInfoEnt.liSelectOrder[i].OrderStatus;

                        if (selectOrder.OrderStatus == 1001)
                        {
                            selectOrder.OrderStatusTest = "已经入库，等待充粉";
                        }
                        else if (selectOrder.OrderStatus == 1002)
                        {
                            selectOrder.OrderStatusTest = "已经充粉，等待组装";
                        }
                        else if (selectOrder.OrderStatus == 1003)
                        {
                            selectOrder.OrderStatusTest = "已经组装，等待贴标";
                        }
                        else if (selectOrder.OrderStatus == 1004)
                        {
                            selectOrder.OrderStatusTest = "已经贴标，操作完成";
                        }
                        else if (selectOrder.OrderStatus == 1005)
                        {
                            selectOrder.OrderStatusTest = "已经出库，操作完成";
                        }
                        else if (selectOrder.OrderStatus == 1006 || selectOrder.OrderStatus == 1009)
                        {
                            selectOrder.OrderStatusTest = "已经入库，订单报废";
                        }
                        else if (selectOrder.OrderStatus == 1007)
                        {
                            selectOrder.OrderStatusTest = "已经入库，等待组装";
                        }
                        else if (selectOrder.OrderStatus == 1008)
                        {
                            selectOrder.OrderStatusTest = "已经入库，等待贴标";
                        }
                        else
                        {
                            selectOrder.OrderStatusTest = "系统检查，无效订单";
                        }
                        selectOrder.ProductId = selectOrderInfoEnt.liSelectOrder[i].ProductId;
                        selectOrder.CreateTime = selectOrderInfoEnt.liSelectOrder[i].CreateTime.ToString();
                        selectOrder.EncodingOrder = selectOrderInfoEnt.liSelectOrder[i].EncodingOrder.ToString();
                        selectOrder.KgNum = selectOrderInfoEnt.liSelectOrder[i].KgNum;
                        liSelectOrder.Add(selectOrder);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return liSelectOrder;
        }

        public CusTomerModel CusTomerEntToModel(CusTomerEnt cusTomerEnt)
        {
            CusTomerModel cusTomerModel = null;
            try
            {
                if (cusTomerEnt != null)
                {
                    cusTomerModel = new CusTomerModel();
                    cusTomerModel.CusCompany = cusTomerEnt.CusCompany;
                    cusTomerModel.CusId = cusTomerEnt.CusId;
                    cusTomerModel.CusLinkMan = cusTomerEnt.CusLinkMan;
                    cusTomerModel.CusLinkPhone = cusTomerEnt.CusLinkPhone;
                    cusTomerModel.CusLinkTel = cusTomerEnt.CusLinkTel;
                }
            }
            catch (Exception ex)
            {

            }
            return cusTomerModel;
        }

        public List<OrderLogModel> OrderLogEntToModel(OrderLogInfoEnt orderLogInfoEnt)
        {
            List<OrderLogModel> liOrderLog = new List<Models.OrderLogModel>();
            try
            {
                if (orderLogInfoEnt != null && orderLogInfoEnt.liOrderLog != null && orderLogInfoEnt.liOrderLog.Count > 0)
                {
                    for (int i = 0; i < orderLogInfoEnt.liOrderLog.Count; i++)
                    {
                        OrderLogModel orderLogModel = new OrderLogModel();
                        orderLogModel.OperatingTime = orderLogInfoEnt.liOrderLog[i].OperatingTime.ToString();
                        orderLogModel.OperationalContent = orderLogInfoEnt.liOrderLog[i].OperationalContent;
                        orderLogModel.Operator = orderLogInfoEnt.liOrderLog[i].Operator;
                        orderLogModel.ProductId = orderLogInfoEnt.liOrderLog[i].ProductId;

                        liOrderLog.Add(orderLogModel);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return liOrderLog;
        }

        public CusTomerInfoModel ListCusTomerEntToModel(List<CusTomerEnt> liCusTomerEnt)
        {
            CusTomerInfoModel cusTomerInfoModel = new Models.CusTomerInfoModel();
            List<CusTomerModel> liCusTomer = new List<CusTomerModel>();
            try
            {
                if (liCusTomerEnt != null && liCusTomerEnt.Count > 0)
                {
                    for (int i = 0; i < liCusTomerEnt.Count; i++)
                    {
                        CusTomerModel cusTomerModel = new CusTomerModel();
                        cusTomerModel.CusId = liCusTomerEnt[i].CusId;
                        cusTomerModel.CusCompany = liCusTomerEnt[i].CusCompany;
                        cusTomerModel.CusLinkMan = liCusTomerEnt[i].CusLinkMan;
                        cusTomerModel.CusLinkPhone = liCusTomerEnt[i].CusLinkPhone;
                        cusTomerModel.CusLinkTel = liCusTomerEnt[i].CusLinkTel;
                        cusTomerModel.CreateTime = liCusTomerEnt[i].CreateTime.ToString();
                        liCusTomer.Add(cusTomerModel);
                    }

                }
                cusTomerInfoModel.liCusTomer = liCusTomer;
            }
            catch (Exception ex)
            {

            }
            return cusTomerInfoModel;
        }
    }
}