using FireFighting.NET;
using FireFighting.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireFighting.API.Models
{
    public class OrderStatistiEntToModel
    {
        public OrderStatistiInfoModel ListOrderStatistiEntToModel(OrderStatistiInfoEnt orderStatistiInfoEnt, ScraporderInfoEnt scraporderInfoEnt)
        {
            OrderStatistiInfoModel orderStatistiInfoModel = new Models.OrderStatistiInfoModel();
            orderStatistiInfoModel.Status = 1;
            orderStatistiInfoModel.Msg = "【手提式灭干粉灭火器】";
            List<OrderStatistiModel> liOrderStatisti = new List<Models.OrderStatistiModel>();
            try
            {
                if (orderStatistiInfoEnt != null && orderStatistiInfoEnt.liOrderStatisti.Count > 0)
                {
                    for (int i = 0; i < orderStatistiInfoEnt.liOrderStatisti.Count; i++)
                    {
                        OrderStatistiModel orderStatistiModel = new Models.OrderStatistiModel();
                        if (i == 0)
                        {
                            orderStatistiInfoModel.CusCompany = orderStatistiInfoEnt.liOrderStatisti[i].CusCompany;
                        }

                        orderStatistiModel.KgNum = orderStatistiInfoEnt.liOrderStatisti[i].KgNum + " 公斤";
                        orderStatistiModel.EncodCount = orderStatistiInfoEnt.liOrderStatisti[i].EncodCount;
                        if ((TransformDataHelper.TransformToInt(orderStatistiModel.EncodCount) == 0))
                        {
                            continue;
                        }

                        if (string.IsNullOrEmpty(orderStatistiModel.CusCompany))
                        {
                            continue;
                        }
                        orderStatistiModel.EncodingOrder = orderStatistiInfoEnt.liOrderStatisti[i].EncodingOrder;
                        orderStatistiModel.ProductNum = orderStatistiInfoEnt.liOrderStatisti[i].ProductNum;  //已维修

                        if (scraporderInfoEnt != null && scraporderInfoEnt.liScraporder.Count > 0)
                        {
                            for (int j = 0; j < scraporderInfoEnt.liScraporder.Count; j++)
                            {
                                if (scraporderInfoEnt.liScraporder[j].EncodingOrder == orderStatistiModel.EncodingOrder)
                                {
                                    orderStatistiModel.ScrapNum = scraporderInfoEnt.liScraporder[j].ScraporderNum.ToString();
                                    break;
                                }
                            }
                        }

                        orderStatistiModel.WaitRepair = (TransformDataHelper.TransformToInt(orderStatistiModel.EncodCount) - TransformDataHelper.TransformToInt(orderStatistiModel.ScrapNum) - TransformDataHelper.TransformToInt(orderStatistiModel.ProductNum)).ToString();
                        liOrderStatisti.Add(orderStatistiModel);
                        orderStatistiInfoModel.liOrderStatisti = liOrderStatisti;
                    }
                }
            }
            catch (Exception ex)
            {
                return orderStatistiInfoModel;
            }
            if(orderStatistiInfoModel.liOrderStatisti.Count>0)
            {
                orderStatistiInfoModel.Status = 0;
            }
            return orderStatistiInfoModel;
        }
    }
}