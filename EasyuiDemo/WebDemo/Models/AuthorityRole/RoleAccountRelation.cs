
using FireFighting.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireFighting.Models
{
    [Serializable]
    public class RoleAccountRelation
    {
        #region 函数

        public RoleAccountRelation() { }
        public RoleAccountRelation(RoleAccountRelationServerEnt rRse) {
            this.RarID = rRse.RarID;
            this.RoleId = rRse.RoleId;
            this.Account = rRse.Account == "" ? "-" : rRse.Account;
            this.RoleName = rRse.RoleName==""?"-":rRse.RoleName;
            this.AddTime = rRse.AddTime.ToString("yyyy-MM-dd HH:mm");
         }
        public RoleAccountRelation[] RoleAccount(RoleAccountRelationServerEnt[] rRse)
        {
            List<RoleAccountRelation> list = new List<RoleAccountRelation>();
            RoleAccountRelation rar = null;
            foreach (RoleAccountRelationServerEnt rrs in rRse)
            {
                rar = new RoleAccountRelation(rrs);
                list.Add(rar);
            }
            return list.ToArray();
        }

        #endregion

        #region 属性

        /// <summary>
        /// 主键ID
        /// </summary>
        public int? RarID { get; set; }

        public string KeyID { get; set; }

        /// <summary>
       /// 账号
       /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }



        /// <summary>
        /// 添加时间
        /// </summary>
        public string AddTime { get; set; }


        #endregion

    }
}