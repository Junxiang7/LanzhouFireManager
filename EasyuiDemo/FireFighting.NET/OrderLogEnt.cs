using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFighting.NET
{
    public class OrderLogEnt
    {
        public string ProductId { get; set; }

        public string Operator { get; set; }

        public DateTime OperatingTime { get; set; }

        public string OperationalContent { get; set; }
    }

    public class OrderLogInfoEnt
    {
        public List<OrderLogEnt> liOrderLog = new List<OrderLogEnt>();
    }
}
