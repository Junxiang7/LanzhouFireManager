

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo
{
   [Serializable]
    public class PageResult
    {
        /// <summary>
        /// 请求是否成功
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 页面上第二个数据集
        /// </summary>
        public object Data1 { get; set; }

        /// <summary>
        /// 失败的信息
        /// </summary>
        public string Msg { get; set; }

       /// <summary>
       /// 用户姓名
       /// </summary>
        public string UserName { get; set; }

       /// <summary>
       /// 总页数
       /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 被选中的权限ID 和
        /// </summary>
        public int PowerSum { get; set; }
       /// <summary>
       /// 添加人
       /// </summary>
        public string AddAccount { get; set; }
    }
}