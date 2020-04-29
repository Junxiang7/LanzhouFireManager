using FireFighting.DAL;
using FireFighting.NET;
using FireFighting.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FireFighting.BLL
{
    public class OrderStatistiManager
    {
        /// <summary>
        /// 订单编码统计
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="EncodingOrder"></param>
        /// <param name="EffectiveTime_s"></param>
        /// <param name="EffectiveTime_e"></param>
        /// <param name="PageParameter"></param>
        /// <returns></returns>
        public OrderStatistiInfoEnt GetOrderStatisti(int Type, string EncodingOrder, string EffectiveTime_s, string EffectiveTime_e, ref PageParameterServerEnt PageParameter)
        {
            OrderStatistiServer OrderStatisti = new OrderStatistiServer();
            OrderStatistiInfoEnt OrderStatistiInfo = new OrderStatistiInfoEnt();
            if (Type == 1)  //代表平台内部查询
            {
                if (!string.IsNullOrEmpty(EncodingOrder))
                {
                    PageParameter.Where = " a.EncodingOrder='" + EncodingOrder + "'";
                }
                StringBuilder SbStr = new StringBuilder();
                if (string.IsNullOrEmpty(EffectiveTime_s))
                {
                    EffectiveTime_s = DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd");
                }
                if (string.IsNullOrEmpty(EffectiveTime_e))
                {
                    EffectiveTime_e = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (TransformDataHelper.TransformToDateTime(EffectiveTime_s) > TransformDataHelper.TransformToDateTime(EffectiveTime_e))
                {
                    EffectiveTime_s = DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd");
                    EffectiveTime_e = DateTime.Now.ToString("yyyy-MM-dd");
                }
                EffectiveTime_s = EffectiveTime_s + " 00:00:000";
                EffectiveTime_e = EffectiveTime_e + " 23:59:999";
                SbStr.Append(" CreateTime >'" + EffectiveTime_s + "' and CreateTime<'" + EffectiveTime_e + "' ");

                List<OrderStatistiEnt> orderStatistiInfo = OrderStatisti.SearchOrderStatistiData(SbStr.ToString(), ref PageParameter);
                OrderStatistiInfo.liOrderStatisti = orderStatistiInfo;
            }
            else if (Type == 2)  //外部查询
            {
                OrderStatistiInfo = OrderStatisti.GetOrderStatistiData(EncodingOrder);
            }
            return OrderStatistiInfo;

        }

        /// <summary>
        /// 报废统计
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="EncodingOrder"></param>
        /// <param name="EffectiveTime_s"></param>
        /// <param name="EffectiveTime_e"></param>
        /// <returns></returns>
        public ScraporderInfoEnt GetScraporderByNum(int Type, string EncodingOrder, string EffectiveTime_s, string EffectiveTime_e)
        {
            OrderStatistiServer OrderStatisti = new OrderStatistiServer();
            ScraporderInfoEnt scraporderInfoEnt = new ScraporderInfoEnt();
            try
            {
                string where = "";
                StringBuilder SbStr = new StringBuilder();
                if (Type == 1) //平台内部查询
                {
                    if (string.IsNullOrEmpty(EffectiveTime_s))
                    {
                        EffectiveTime_s = DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd");
                    }
                    if (string.IsNullOrEmpty(EffectiveTime_e))
                    {
                        EffectiveTime_e = DateTime.Now.ToString("yyyy-MM-dd");
                    }
                    if (TransformDataHelper.TransformToDateTime(EffectiveTime_s) > TransformDataHelper.TransformToDateTime(EffectiveTime_e))
                    {
                        EffectiveTime_s = DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd");
                        EffectiveTime_e = DateTime.Now.ToString("yyyy-MM-dd");
                    }
                    EffectiveTime_s = EffectiveTime_s + " 00:00:000";
                    EffectiveTime_e = EffectiveTime_e + " 23:59:999";
                    SbStr.Append(" CreateTime >'" + EffectiveTime_s + "' and CreateTime<'" + EffectiveTime_e + "' ");
                    where = SbStr.ToString();
                }

                if (!string.IsNullOrEmpty(EncodingOrder))
                {
                    if (!string.IsNullOrEmpty(where))
                    {
                        where += " and ";
                    }
                    where += " EncodingOrder='" + EncodingOrder + "'";
                }

                scraporderInfoEnt = OrderStatisti.GetScraporderData(where);
            }
            catch (Exception ex)
            {

            }
            return scraporderInfoEnt;
        }

        /// <summary>
        /// 外部查询使用
        /// </summary>
        /// <param name="externalSearch"></param>
        public void InsertExternal(ExternalSearch externalSearch)
        {
            OrderStatistiServer OrderStatisti = new OrderStatistiServer();
            OrderStatisti.InsertExternalService(externalSearch);
        }

        /// <summary>
        /// 外部使用验证查询
        /// </summary>
        /// <param name="Ip"></param>
        /// <param name="Condition"></param>
        /// <returns></returns>
        public List<ExternalSearch> GetOrderExternalInfo(string Ip, string Condition)
        {
            OrderStatistiServer OrderStatisti = new OrderStatistiServer();
            Regex rx = new Regex(@"((?:(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d))))");
            if (!rx.IsMatch(Ip))
            {
                Ip = "";
            }
            List<ExternalSearch> liexternalSearch = OrderStatisti.GetOrderExternal(Ip, Condition);
            return liexternalSearch;
        }

        public List<SelectOrderEnt> GetSearchOrder(string EncodingOrder)
        {
            List<SelectOrderEnt> liSelectOrder = new List<SelectOrderEnt>();
            OrderStatistiServer OrderStatisti = new OrderStatistiServer();

            liSelectOrder = OrderStatisti.GetSearchOrderService(EncodingOrder);
            return liSelectOrder;
        }
    }
}
