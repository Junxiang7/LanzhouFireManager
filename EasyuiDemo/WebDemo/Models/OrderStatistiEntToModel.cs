using FireFighting.NET;
using FireFighting.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Models
{
    public class OrderStatistiEntToModel
    {

        public OrderStatistiInfoModel ListOrderStatistiEntToModel(OrderStatistiInfoEnt orderStatistiInfoEnt, ScraporderInfoEnt scraporderInfoEnt)
        {
            OrderStatistiInfoModel orderStatistiInfoModel = new Models.OrderStatistiInfoModel();
            List<OrderStatistiModel> liOrderStatisti = new List<Models.OrderStatistiModel>();
            try
            {
                if (orderStatistiInfoEnt != null && orderStatistiInfoEnt.liOrderStatisti.Count > 0)
                {
                    for (int i = 0; i < orderStatistiInfoEnt.liOrderStatisti.Count; i++)
                    {
                        OrderStatistiModel orderStatistiModel = new Models.OrderStatistiModel();

                        orderStatistiModel.CusCompany = orderStatistiInfoEnt.liOrderStatisti[i].CusCompany;
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

                        if (string.IsNullOrEmpty(orderStatistiInfoEnt.liOrderStatisti[i].ProductNum))  //已维修
                        {
                            orderStatistiModel.ProductNum = "0";
                        }
                        else
                        {
                            orderStatistiModel.ProductNum = orderStatistiInfoEnt.liOrderStatisti[i].ProductNum; 
                        }

                        if (scraporderInfoEnt != null && scraporderInfoEnt.liScraporder.Count > 0)
                        {
                            for (int j = 0; j < scraporderInfoEnt.liScraporder.Count; j++)
                            {
                                if (scraporderInfoEnt.liScraporder[j].EncodingOrder == orderStatistiModel.EncodingOrder&& orderStatistiInfoEnt.liOrderStatisti[i].KgNum.TrimEnd()== scraporderInfoEnt.liScraporder[j].KgNum.TrimEnd())
                                {
                                    orderStatistiModel.ScrapNum = scraporderInfoEnt.liScraporder[j].ScraporderNum.ToString();
                                    break;
                                }
                            }
                        }

                        if (string.IsNullOrEmpty(orderStatistiModel.ScrapNum))
                        {
                            orderStatistiModel.ScrapNum = "0";
                        }
                        orderStatistiModel.WaitRepair = (TransformDataHelper.TransformToInt(orderStatistiModel.EncodCount) - TransformDataHelper.TransformToInt(orderStatistiModel.ScrapNum) - TransformDataHelper.TransformToInt(orderStatistiModel.ProductNum)).ToString();
                        liOrderStatisti.Add(orderStatistiModel);
                        orderStatistiInfoModel.liOrderStatisti = liOrderStatisti;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return orderStatistiInfoModel;
        }
    }
}