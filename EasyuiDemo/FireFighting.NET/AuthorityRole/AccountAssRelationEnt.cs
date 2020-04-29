using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FireFighting.NET
{
    [Serializable]
    public class AccountAssRelationEnt
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string KeyId { get; set; }

        /// <summary>
        /// 账户Id（登录账号）
        /// </summary>
        public string AccounId { get; set; }

        /// <summary>
        /// 账户名称
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 关联账户用户号
        /// </summary>
        public string AssociationUserID { get; set; }

        /// <summary>
        /// 关联账户id（登录账号）
        /// </summary>
        public string AssociationAccounId { get; set; }

        /// <summary>
        /// 关联账户名称
        /// </summary>
        public string AssociationAccountName { get; set; }

        /// <summary>
        /// 添加人员
        /// </summary>
        public string AddAccount { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 关联状态
        /// </summary>
        public int AssociationStatus { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public int IsForbidden { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDelete { get; set; }

        /// <summary>
        /// 验证签名
        /// </summary>
        public string SignStr { get; set; }



        public string A1 { get; set; }

        public string A2 { get; set; }

        public string A3 { get; set; }

        public string A4 { get; set; }

        public string A5 { get; set; }

        public string StrTrackID { get; set; }
    }
    [Serializable]
    public class AccountAssRelaServerEnt
    {
        private List<AccountAssRelationEnt> LiAccountAssRelation = new List<AccountAssRelationEnt>();
        public List<AccountAssRelationEnt> liAccountAssRelation
        {
            get { return LiAccountAssRelation; }
            set { LiAccountAssRelation = value; }
        }
    }
}
