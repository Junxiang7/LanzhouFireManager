using FireFighting.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Models
{
    public class SelectOrder
    {
        public int OrderId { get; set; }

        public string ProductId { get; set; }

        public string CusCompany { get; set; }

        public int CusId { get; set; }

        public int OrderStatus { get; set; }

        public string OrderStatusTest { get; set; }

        public string CreateTime { get; set; }

        public string EncodingOrder { get; set; }

        public int KgNum { get; set; }
    }

}