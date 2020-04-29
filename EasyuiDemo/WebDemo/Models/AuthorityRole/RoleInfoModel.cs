
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireFighting.Models
{
     [Serializable]
    public class RoleInfoModel
    {
         /// <summary>
         /// 角色ID
         /// </summary>
        public int? RoleId { get; set; }
         /// <summary>
         /// 角色名称
         /// </summary>
        public string RoleName { get; set; }

         /// <summary>
        /// 添加人员
         /// </summary>
        public string AddAccount { get; set; }
         /// <summary>
         /// 最后修改日期
         /// </summary>
        public DateTime LastDateTime { get; set; }
         /// <summary>
        /// 添加时间
         /// </summary>
        public DateTime AddTime { get; set; }
    
    }

    [Serializable]
     public class RoleInfoServerModel 
     {
        //当前用户角色
        private List<RoleInfoModel> Rorderlist = new List<RoleInfoModel>();
        public List<RoleInfoModel> rorderlist
         {
             get { return Rorderlist; }
             set { Rorderlist = value; }
         }

        //下属用户角色
        private List<RoleInfoModel> Rorderonlist = new List<RoleInfoModel>();
        public List<RoleInfoModel> rorderonlist
        {
            get { return Rorderonlist; }
            set { Rorderonlist = value; }
        }
     }
}