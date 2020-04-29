
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FireFighting.NET
{
    [Serializable]
    public class RolePowerRelationServerEnt
    {
        #region 属性

        /// <summary>
        /// 唯一主键ID
        /// </summary>
        public int? RpRId { get; set; }

        /// <summary>
        /// 主键ID
        /// </summary>
        public string PowerId { get; set; }


        /// <summary>
        /// 节点权限码
        /// </summary>
        public string NodeCode { get; set; }


        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; set; }


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
        /// 权限路径
        /// </summary>
        public string AccessPath { get; set; }


        #endregion
    }
}
