using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFighting.NET
{
    public class RetResultEnt
    {
        #region 属性

        /// <summary>
        /// 处理结果 true 成功 false 失败
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 权限节点对象集合
        /// </summary>
        public List<PowerInfoServerEnt> Power { set; get; }

        /// <summary>
        /// 客户端实体对象
        /// </summary>
        public object RetObj { get; set; }

        public List<RoleAccountRelationServerEnt> RoleAccount { get; set; }

        public List<RoleInfoServerEnt> Role { get; set; }

        public List<RolePowerInfoServerEnt> RolePower { get; set; }

        /// <summary>
        /// 角色权限关系集合
        /// </summary>
        public List<RolePowerRelationServerEnt> RpRs { get; set; }


        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }


        /// <summary>
        /// 数据量
        /// </summary>
        public int Count { get; set; }


        #endregion
    }
}
