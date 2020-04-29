using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Models
{
    public class OrderStatistiModel
    {
        /// <summary>
        /// 总订单数
        /// </summary>
        public string EncodCount { get; set; }

        public string EncodingOrder { get; set; }

        public string CusCompany { get; set; }

        /// <summary>
        /// 已修
        /// </summary>
        public string ProductNum { get; set; }

        /// <summary>
        /// 报废数
        /// </summary>
        public string ScrapNum { get; set; }

        public string KgNum { get; set; }

        public string WaitRepair { get; set; }

    }
    public class OrderStatistiInfoModel
    {
        public List<OrderStatistiModel> liOrderStatisti = new List<OrderStatistiModel>();
    }
}