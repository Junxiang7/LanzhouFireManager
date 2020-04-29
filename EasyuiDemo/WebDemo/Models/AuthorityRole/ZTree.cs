

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireFighting.Models
{
    [Serializable]
    public class ZTree
    {
        /// <summary>
        /// 树节点的唯一标示
        /// </summary>
        public string TId { get; set; }

        /// <summary>
        /// 节点父级ID
        /// </summary>
        public string ParentTId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 节点是否被选中
        /// </summary>
        public bool IsChecked { get; set; }

        /// <summary>
        /// 是否为前台节点
        /// </summary>
        public int? IsForeground { get; set; }
    }
}