using FireFighting.BLL;
using FireFighting.NET;
using FireFighting.Tool;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDemo.Models;

namespace WebDemo.Controllers
{
    public class SearchController : Controller
    {
        private static readonly string BaseUrl = "http://qcwxapi.gsxlxf.com:8096/api/ExternalQuery/ExternalSearch";
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult SearchInfo()
        {
            return View();
        }

        /// <summary>
        /// 外部查询入口 返回展示
        /// </summary>
        /// <param name="EncodeOrder"></param>
        /// <param name="Time"></param>
        /// <returns></returns>
        public JsonResult GetFireInfo(string EncodeOrder, string Time)
        {
            OrderStatistiInfo orderStatistiInfo = new OrderStatistiInfo();
            string Result = string.Empty;
            try
            {
                FromEncode fromEncode = new Models.FromEncode();
                if (string.IsNullOrEmpty(EncodeOrder.Trim()))
                {
                    var Emptydatacatch = new { rows = "", CusCompany = orderStatistiInfo.CusCompany, Msg = orderStatistiInfo.Msg, ErrorMsg = "无效的查询" };
                    return Json(Emptydatacatch);
                }
                fromEncode.EncodeOrder = SqlFilter.SuperFilter(EncodeOrder);
                fromEncode.Time = Time;
                //string paramter = JsonConvert.SerializeObject(fromEncode);
                //orderStatistiInfo = JsonConvert.DeserializeObject<OrderStatistiInfo>(PostData.PostDataApi(BaseUrl, paramter));
                orderStatistiInfo = ExternalSearch(fromEncode);
                if (orderStatistiInfo.SearchType == "1")
                {
                    var jsondata = new { rows = orderStatistiInfo.liOrderStatisti, CusCompany = orderStatistiInfo.CusCompany, Msg = orderStatistiInfo.Msg, ErrorMsg = orderStatistiInfo.ErrorMsg, SearchType = orderStatistiInfo.SearchType };
                    return Json(jsondata);
                }
                else if (orderStatistiInfo.SearchType == "2")
                {
                    var jsondataOrder = new { rows = orderStatistiInfo.liSelectOrder, CusCompany = orderStatistiInfo.CusCompany, Msg = orderStatistiInfo.Msg, ErrorMsg = orderStatistiInfo.ErrorMsg, SearchType = orderStatistiInfo.SearchType };
                    return Json(jsondataOrder);
                }
                else
                {
                    var jsondataOrder = new { rows = "", CusCompany = orderStatistiInfo.CusCompany, Msg = orderStatistiInfo.Msg, ErrorMsg = orderStatistiInfo.ErrorMsg };
                    return Json(jsondataOrder);
                }
            }
            catch (Exception ex)
            {
                var jsondatacatch = new { rows = "", CusCompany = orderStatistiInfo.CusCompany, Msg = orderStatistiInfo.Msg, ErrorMsg = "查询异常" };
                return Json(jsondatacatch);
            }
        }

        /// <summary>
        /// 外部查询调用
        /// </summary>
        /// <param name="fromEncode"></param>
        /// <returns></returns>
        public OrderStatistiInfo ExternalSearch(FromEncode fromEncode)
        {
            OrderStatistiInfo orderStatisti = new OrderStatistiInfo();
            orderStatisti.Status = 1;
            string Remsg = string.Empty;
            OrderStatistiManager OrderStatisti = new OrderStatistiManager();
            PageParameterServerEnt PageParameter = new PageParameterServerEnt();
            string m_Content = "";
            if (fromEncode != null)
            {
                try
                {
                    fromEncode.EncodeOrder = SqlFilter.SqlStrFilter(fromEncode.EncodeOrder.Trim());
                    if (!string.IsNullOrEmpty(fromEncode.EncodeOrder))
                    {
                        List<ExternalSearch> liExternalSearch = OrderStatisti.GetOrderExternalInfo(GetIP(), fromEncode.EncodeOrder);
                        if (liExternalSearch.Count > 8)
                        {
                            orderStatisti.ErrorMsg = "查询频率过快，请稍后再试！";
                            return orderStatisti;
                        }
                        if (fromEncode.EncodeOrder.Length == 8)
                        {
                            OrderStatistiInfoEnt orderStatistiInfo = OrderStatisti.GetOrderStatisti(2, fromEncode.EncodeOrder, "", "", ref PageParameter);
                            ScraporderInfoEnt scraporderInfo = OrderStatisti.GetScraporderByNum(2, fromEncode.EncodeOrder, "", "");

                            orderStatisti = OrderStatistiBy(orderStatistiInfo, scraporderInfo);
                            orderStatisti.SearchType = "1";
                            if (orderStatisti.liOrderStatisti.Count > 0)
                            {
                                m_Content = JsonHelper.JsonSerializer<OrderStatistiInfo>(orderStatisti);
                            }
                        }
                        else if (fromEncode.EncodeOrder.Length == 13)
                        {
                            orderStatisti.Msg = "【手提式干粉灭火器】";
                            orderStatisti.SearchType = "2";
                            List<SelectOrderEnt> liselectOrder = OrderStatisti.GetSearchOrder(fromEncode.EncodeOrder);
                            if (liselectOrder.Count > 0)
                            {
                                m_Content = JsonHelper.JsonSerializer<List<SelectOrderEnt>>(liselectOrder);
                                orderStatisti.Status = 0;
                                orderStatisti.CusCompany = liselectOrder[0].CusCompany;
                            }
                            else
                            {
                                orderStatisti.ErrorMsg = "未查询到信息";
                            }
                            orderStatisti.liSelectOrder = liselectOrder;
                        }
                        else
                        {
                            orderStatisti.ErrorMsg = "无效的查询编号！";
                            return orderStatisti;
                        }

                        ExternalSearch externalSearch = new FireFighting.NET.ExternalSearch();
                        externalSearch.Condition = fromEncode.EncodeOrder;
                        externalSearch.Ip = GetIP();
                        externalSearch.Content = m_Content;
                        OrderStatisti.InsertExternal(externalSearch);
                        //OrderStatistiEntToModel orderStatistiETM = new Models.OrderStatistiEntToModel();
                        //OrderStatistiInfoModel orderStatistiInfoModel = orderStatistiETM.ListOrderStatistiEntToModel(orderStatistiInfo, scraporderInfo);
                        //Remsg = JsonHelper.JsonSerializer<OrderStatistiInfoModel>(orderStatistiInfoModel);
                    }
                }
                catch (Exception ex)
                {
                    orderStatisti.ErrorMsg = "未查询到信息！";
                    return orderStatisti;
                }
            }

            return orderStatisti;
        }

        public OrderStatistiInfo OrderStatistiBy(OrderStatistiInfoEnt orderStatistiInfoEnt, ScraporderInfoEnt scraporderInfoEnt)
        {
            OrderStatistiInfo orderStatistiInfos = new OrderStatistiInfo();
            orderStatistiInfos.Status = 1;
            orderStatistiInfos.Msg = "【手提式干粉灭火器】";
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
                        orderStatistiEnt.CusCompany = orderStatistiInfoEnt.liOrderStatisti[i].CusCompany;
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

                        if (string.IsNullOrEmpty(orderStatistiInfoEnt.liOrderStatisti[i].ProductNum))
                        {
                            orderStatistiEnt.ProductNum = "0";
                        }
                        else
                        {
                            orderStatistiEnt.ProductNum = orderStatistiInfoEnt.liOrderStatisti[i].ProductNum;  //已维修
                        }


                        if (scraporderInfoEnt != null && scraporderInfoEnt.liScraporder.Count > 0)
                        {
                            for (int j = 0; j < scraporderInfoEnt.liScraporder.Count; j++)
                            {
                                if (scraporderInfoEnt.liScraporder[j].EncodingOrder == orderStatistiEnt.EncodingOrder && orderStatistiInfoEnt.liOrderStatisti[i].KgNum.TrimEnd() == scraporderInfoEnt.liScraporder[j].KgNum.TrimEnd())
                                {
                                    orderStatistiEnt.ScrapNum = scraporderInfoEnt.liScraporder[j].ScraporderNum.ToString();
                                    break;
                                }
                            }
                        }
                        if (string.IsNullOrEmpty(orderStatistiEnt.ScrapNum))
                        {
                            orderStatistiEnt.ScrapNum = "0";
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
            else
            {
                orderStatistiInfos.ErrorMsg = "未查询到信息";
            }
            return orderStatistiInfos;
        }

        private string GetIP()
        {
            string ip = string.Empty;
            if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"]))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);
            if (string.IsNullOrEmpty(ip))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            return ip;
        }
    }
}
