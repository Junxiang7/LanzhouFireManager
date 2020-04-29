
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FireFighting.NET
{
    [Serializable]
    public class RoleAccountRelationServerEnt
    {
        #region 属性

        /// <summary>
        /// 唯一主键ID 
        /// </summary>
        public int? RarID { get; set; }

        /// <summary>
        /// keyid
        /// </summary>
        public string KeyId { get; set; }

        /// <summary>
        /// 用户号
        /// </summary>
        public string WalletKey { get; set; }

        /// <summary>
        /// 账户ID
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string LinkPhone { get; set; }

        /// <summary>
        /// 账号类型
        /// </summary>
        public int? UserType { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

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
        /// 是否可用
        /// </summary>
        public int? IsEnable { get; set; }

        /// <summary>
        /// 验证签名
        /// </summary>
        public string SignStr { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public int PlaType { get; set; }

        #endregion
    }

    [Serializable]
    public class RoleAccountRelationEnt
    {
        private List<RoleAccountRelationServerEnt> RoleAccountRelationlist = new List<RoleAccountRelationServerEnt>();

        public List<RoleAccountRelationServerEnt> roleAccountRelationlistlist
        {
            get { return RoleAccountRelationlist; }
            set { RoleAccountRelationlist = value; }
        }
    }
}
