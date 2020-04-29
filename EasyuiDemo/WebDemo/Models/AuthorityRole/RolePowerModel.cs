
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireFighting.Models
{
    [Serializable]
    public class RolePowerModel
    {
        /// <summary>
        /// 角色id
        /// </summary>
        public int? RoleId { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 权限主键Id
        /// </summary>
        public int PowerID { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; set; }
        /// <summary>
        /// 节点类型
        /// </summary>
        public int? NodeType { get; set; }
        /// <summary>
        /// 父节点ID
        /// </summary>
        public int? PNodeID { get; set; }



    }

    [Serializable]
    public class RolePowerServerModel
    {
        private List<RolePowerModel> Porderlist = new List<RolePowerModel>();
        public List<RolePowerModel> porderlist
        {
            get { return Porderlist; }
            set { Porderlist = value; }
        }
    }
}