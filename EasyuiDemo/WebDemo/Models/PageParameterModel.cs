using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Models
{
    public class PageParameterModel
    {
        public PageParameterModel()
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
    }
}