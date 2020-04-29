
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FireFighting.NET
{
    [Serializable]
    public class PowerInfoServerEnt
   {
       
       #region 属性

        /// <summary>
        /// 唯一ID
        /// </summary>
        public string PowerID { get; set; }

       /// <summary>
       /// 节点权限码
       /// </summary>
       public string NodeCode { get; set; }


       /// <summary>
       /// 节点名称
       /// </summary>
       public string NodeName { get; set; }

       /// <summary>
       /// 父节点ID
       /// </summary>
       public string  PNodeID { get; set; }

        /// <summary>
        /// 权限路径
        /// </summary>
       public string AccessPath { get; set; }

        /// <summary>
        /// 节点类型。0：大菜单1：子菜单3：内页4：按钮5：其他
        /// </summary>
       public int? NodeType { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
       public int? IsEnabled { get; set; }

        /// <summary>
        /// 添加人员账号
        /// </summary>
       public string AddAccount { get; set; }

        /// <summary>
        /// 添加日期
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public int? IsDelete { get; set; }

        /// <summary>
        /// 是否是前端
        /// </summary>
        public int? IsForeground { get; set; }


        /// <summary>
        /// 验证签名
        /// </summary>
        public string SignStr { get; set; }

        /// <summary>
        ///进入路径
        /// </summary>
        public string EnterUrl { get; set; }


       #endregion
       
   }

    [Serializable]
    public class PowerInfolsEnt
    {
        private List<PowerInfoServerEnt> PowerInfolist = new List<PowerInfoServerEnt>();

        public List<PowerInfoServerEnt> powerInfolist
        {
            get { return PowerInfolist; }
            set { PowerInfolist = value; }
        }
    }
}
