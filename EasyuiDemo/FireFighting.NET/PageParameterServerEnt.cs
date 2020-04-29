using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFighting.NET
{
    public class PageParameterServerEnt
    {
        public PageParameterServerEnt()
        {
            this.PageSize = 0;
            this.PageIndex = 0;
            this.TotalCount = 0;
            this.PageCount = 0;

        }
        /// <summary>
        /// 每页记录条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前页数
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 查询条件 只添加条件
        /// </summary>
        public string Where { get; set; }

        /// <summary>
        /// 排序规则  字段 ASC
        /// </summary>
        public string Orderby { get; set; }

        /// <summary>
        /// 查询字段
        /// </summary>
        public string Fields { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string Tables { get; set; }
    }
}
