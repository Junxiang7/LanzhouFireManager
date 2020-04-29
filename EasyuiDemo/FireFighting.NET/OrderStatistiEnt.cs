using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFighting.NET
{
    public class OrderStatistiEnt
    {
        public string EncodCount { get; set; }

        public string EncodingOrder { get; set; }

        public string CusCompany { get; set; }

        public string ProductNum { get; set; }

        public string KgNum { get; set; }

        public string ScrapNum { get; set; }

        public string WaitRepair { get; set; }
    }

    public class OrderStatistiInfoEnt
    {
        public List<OrderStatistiEnt> liOrderStatisti = new List<OrderStatistiEnt>();
    }

    public class OrderStatistiInfo
    {
        public int Status { get; set; }
        public string CusCompany { get; set; }

        public string Msg { get; set; }

        public string ErrorMsg { get; set; }

        /// <summary>
        /// 1 八位数查询  2  十三位数查询
        /// </summary>
        public string SearchType { get; set; }

        public List<OrderStatistiEnt> liOrderStatisti = new List<OrderStatistiEnt>();

        public List<SelectOrderEnt> liSelectOrder = new List<SelectOrderEnt>();
    }
}
