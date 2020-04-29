
using FireFighting.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireFighting.Models
{
    [Serializable]
    public class RoleInfo
    {

        public RoleInfo() { }

        public RoleInfo(RoleInfoServerEnt role) {
            this.RoleID = role.RoleId;
            this.RoleName = role.RoleName;
        }

        public RoleInfo[] RoleList(RoleInfoServerEnt[] listRole)
        {
            List<RoleInfo> list = new List<RoleInfo>();
            RoleInfo rar = null;
            foreach (RoleInfoServerEnt rrs in listRole)
            {
                rar = new RoleInfo(rrs);
                list.Add(rar);
            }
            return list.ToArray();
        }

        #region 属性

        /// <summary>
        ///角色ID
        /// </summary>
        public int? RoleID { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        #endregion
    }
}