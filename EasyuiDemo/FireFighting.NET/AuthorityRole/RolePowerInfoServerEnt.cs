using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FireFighting.NET
{
    [Serializable]
    public class RolePowerInfoServerEnt
    {
        #region 属性
        /// <summary>
        /// 角色ID
        /// </summary>

        public int? RoleID { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        /// 
        public string RoleName { get; set; }
        /// <summary>
        /// 权限ID
        /// </summary>
        /// 
        public int PowerID { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        /// 
        public string NodeName { get; set; }
        /// <summary>
        /// 节点类型。0：大菜单1：子菜单3：内页4：按钮5：其他
        /// </summary>
        public int? NodeType { get; set; }
        //父节点的NodeID,如果是根节点就是Null
        public int? PNodeID { get; set; }

        /// <summary>
        /// 节点权限码
        /// </summary>
        public string NodeCode { get; set; }

        /// <summary>
        /// 添加人
        /// </summary>
        public string AddAccount { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }

       /// <summary>
       /// 是否删除
       /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 是否为前台节点
        /// </summary>
        public int IsForeground { get; set; }

        /// <summary>
        /// 权限路径
        /// </summary>
        public string AccessPath { get; set; }

        /// <summary>
        ///进入路径
        /// </summary>
        public string EnterUrl { get; set; }


        /// <summary>
        /// 备用字段2
        /// </summary>
        public string A2 { get; set; }

        /// <summary>
        /// 备用字段3
        /// </summary>
        public string A3 { get; set; }

        /// <summary>
        /// 备用字段4
        /// </summary>
        public string A4 { get; set; }

        /// <summary>
        /// 备用字段5
        /// </summary>
        public string A5 { get; set; }
        #endregion

    }
}
