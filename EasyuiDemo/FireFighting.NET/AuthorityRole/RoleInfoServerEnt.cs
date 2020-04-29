
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FireFighting.NET
{
    [Serializable]
  public  class RoleInfoServerEnt
  {
      #region 属性

      /// <summary>
      /// 角色ID
      /// </summary>
      public int? RoleId { get; set; }

      /// <summary>
      /// 角色名称
      /// </summary>
      public string RoleName { get; set; }

      /// <summary>
      /// 角色描述
      /// </summary>
      public string RoleDesc { get; set; }

      /// <summary>
      /// 是否启用
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
      /// 最后修改的时间
      /// </summary>
      public DateTime LastDateTime { get;set;}

      /// <summary>
      /// 是否删除
      /// </summary>
      public int? IsDelete { get; set; }

      /// <summary>
      /// 验证签名
      /// </summary>
      public string SignStr { get; set; }



      /// <summary>
      /// 跟踪ID
      /// </summary>
      public string StrTrackID { get; set; }

      #endregion
  }

    [Serializable]
  public class RoleInfolsEnt
    {
        private List<RoleInfoServerEnt> RoleInfolist = new List<RoleInfoServerEnt>();

        public List<RoleInfoServerEnt> roleInfolist
        {
            get { return RoleInfolist; }
            set { RoleInfolist = value; }
        }
    }
}
