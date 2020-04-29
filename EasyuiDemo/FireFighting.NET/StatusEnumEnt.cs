using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFighting.NET
{
    public enum StatusEnumEnt
    {
        已经入库等待充粉 = 1001,
        已经充粉等待组装 = 1002,
        已经组装等待贴标 = 1003,
        已经贴标完成出库 = 1004,
        已经入库订单报废 = 1006,
        已经入库等待组装 = 1007,
        已经入库等待贴标 = 1008,
        已经入库人工报废 = 1009,
    }
}
