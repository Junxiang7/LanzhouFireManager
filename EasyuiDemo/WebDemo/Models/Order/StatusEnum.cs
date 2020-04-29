using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Models
{
    public enum StatusEnum
    {
        已经入库等待充粉 = 1001,
        已经充粉等待组装 = 1002,
        已经组装等待贴标 = 1003,
        已经贴标操作完成 = 1004,
    }
}