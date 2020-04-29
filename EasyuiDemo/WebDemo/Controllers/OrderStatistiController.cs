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
    public class OrderStatistiController : BaseController
    {
        // GET: OrderStatisti
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrderStatisti()
        {
            return View();
        }

        /// <summary>
        /// 订单统计获取数据
        /// </summary>
        /// <param name="EffectiveTime_s"></param>
        /// <param name="EffectiveTime_e"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="EncodingOrder"></param>
        /// <returns></returns>
        public JsonResult OrderStatistiListJson(string EffectiveTime_s, string EffectiveTime_e, int page = 1, int rows = 10, string EncodingOrder = "")
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
            EncodingOrder = SqlFilter.SuperFilter(EncodingOrder);
            OrderStatistiManager orderStatistiManager = new OrderStatistiManager();
            OrderStatistiEntToModel OrderStatistiETM = new OrderStatistiEntToModel();
            OrderStatistiInfoEnt orderStatistiInfoEnt = orderStatistiManager.GetOrderStatisti(1, EncodingOrder, EffectiveTime_s, EffectiveTime_e, ref PageParameter);
            ScraporderInfoEnt scraporderInfoEnt= orderStatistiManager.GetScraporderByNum(1, EncodingOrder, EffectiveTime_s, EffectiveTime_e);
            OrderStatistiInfoModel orderStatistiInfoModel = OrderStatistiETM.ListOrderStatistiEntToModel(orderStatistiInfoEnt, scraporderInfoEnt);
            var jsondata = new { rows = orderStatistiInfoModel.liOrderStatisti, total = PageParameter.TotalCount };

            return Json(jsondata);
        }
    }
}