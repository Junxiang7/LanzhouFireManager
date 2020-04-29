using FireFighting.API.Models;
using FireFighting.BLL;
using FireFighting.NET;
using FireFighting.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FireFighting.API.Controllers
{
    public class ExternalQueryController : ApiController
    {
        [ActionName("ExternalSearch")]
        [HttpPost]
        public OrderStatistiInfo ExternalSearch(FromEncode fromEncode)
        {
            OrderStatistiInfo orderStatisti = new OrderStatistiInfo() ;
            string Remsg = string.Empty;
            OrderStatistiManager OrderStatisti = new OrderStatistiManager();
            PageParameterServerEnt PageParameter = new PageParameterServerEnt();
            if (fromEncode != null)
            {
                try
                {
                    fromEncode.EncodeOrder = SqlFilter.SqlStrFilter(fromEncode.EncodeOrder.Trim());
                    if (!string.IsNullOrEmpty(fromEncode.EncodeOrder))
                    {
                        OrderStatistiInfoEnt orderStatistiInfo = OrderStatisti.GetOrderStatisti(2, fromEncode.EncodeOrder, "", "", ref PageParameter);
                        ScraporderInfoEnt scraporderInfo = OrderStatisti.GetScraporderByNum(2, fromEncode.EncodeOrder, "", "");

                        orderStatisti = OrderStatistiBy(orderStatistiInfo, scraporderInfo);

                        //OrderStatistiEntToModel orderStatistiETM = new Models.OrderStatistiEntToModel();
                        //OrderStatistiInfoModel orderStatistiInfoModel = orderStatistiETM.ListOrderStatistiEntToModel(orderStatistiInfo, scraporderInfo);
                        //Remsg = JsonHelper.JsonSerializer<OrderStatistiInfoModel>(orderStatistiInfoModel);
                    }
                }
                catch (Exception ex)
                {
                    return orderStatisti;
                }
            }

            return orderStatisti;
        }

        public OrderStatistiInfo OrderStatistiBy(OrderStatistiInfoEnt orderStatistiInfoEnt, ScraporderInfoEnt scraporderInfoEnt)
        {
            OrderStatistiInfo orderStatistiInfos = new OrderStatistiInfo();
            orderStatistiInfos.Status = 1;
            orderStatistiInfos.Msg = "【手提式灭干粉灭火器】";
            List<OrderStatistiEnt> liOrderStatisti = new List<OrderStatistiEnt>();
            try
            {
                if (orderStatistiInfoEnt != null && orderStatistiInfoEnt.liOrderStatisti.Count > 0)
                {
                    for (int i = 0; i < orderStatistiInfoEnt.liOrderStatisti.Count; i++)
                    {
                        OrderStatistiEnt orderStatistiEnt = new OrderStatistiEnt();
                        if (i == 0)
                        {
                            orderStatistiInfos.CusCompany = orderStatistiInfoEnt.liOrderStatisti[i].CusCompany;
                        }

                        orderStatistiEnt.KgNum = orderStatistiInfoEnt.liOrderStatisti[i].KgNum + " 公斤";
                        orderStatistiEnt.EncodCount = orderStatistiInfoEnt.liOrderStatisti[i].EncodCount;
                        if ((TransformDataHelper.TransformToInt(orderStatistiEnt.EncodCount) == 0))
                        {
                            continue;
                        }

                        if (string.IsNullOrEmpty(orderStatistiEnt.CusCompany))
                        {
                            continue;
                        }
                        orderStatistiEnt.EncodingOrder = orderStatistiInfoEnt.liOrderStatisti[i].EncodingOrder;
                        orderStatistiEnt.ProductNum = orderStatistiInfoEnt.liOrderStatisti[i].ProductNum;  //已维修

                        if (scraporderInfoEnt != null && scraporderInfoEnt.liScraporder.Count > 0)
                        {
                            for (int j = 0; j < scraporderInfoEnt.liScraporder.Count; j++)
                            {
                                if (scraporderInfoEnt.liScraporder[j].EncodingOrder == orderStatistiEnt.EncodingOrder)
                                {
                                    orderStatistiEnt.ScrapNum = scraporderInfoEnt.liScraporder[j].ScraporderNum.ToString();
                                    break;
                                }
                            }
                        }

                        orderStatistiEnt.WaitRepair = (TransformDataHelper.TransformToInt(orderStatistiEnt.EncodCount) - TransformDataHelper.TransformToInt(orderStatistiEnt.ScrapNum) - TransformDataHelper.TransformToInt(orderStatistiEnt.ProductNum)).ToString();
                        liOrderStatisti.Add(orderStatistiEnt);
                        orderStatistiInfos.liOrderStatisti = liOrderStatisti;
                    }
                }
            }
            catch (Exception ex)
            {
                return orderStatistiInfos;
            }
            if (orderStatistiInfos.liOrderStatisti.Count > 0)
            {
                orderStatistiInfos.Status = 0;
            }
            return orderStatistiInfos;
        }
    }
}
