using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFighting.NET
{
    public class SelectOrderEnt
    {
        public int OrderId { get; set; }

        public string ProductId { get; set; }

        public string CusCompany { get; set; }

        public int CusId { get; set; }

        public int OrderStatus { get; set; }

        public int NewOrderStatus { get; set; }

        public string OrderStatusTest { get; set; }

        public string CreateTime { get; set; }

        public string EncodingOrder { get; set; }

        public int hide { get; set; }

        public int KgNum { get; set; }

        public int OrderCount { get; set; }
    }

    public class SelectOrderInfoEnt
    {
        public List<SelectOrderEnt> liSelectOrder = new List<SelectOrderEnt>();
    }
}
