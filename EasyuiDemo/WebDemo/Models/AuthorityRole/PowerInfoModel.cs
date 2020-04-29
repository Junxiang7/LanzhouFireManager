
using FireFighting.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo
{
    [Serializable]
    public class PowerInfoModel
    {
        #region 属性

        /// <summary>
        /// 唯一ID
        /// </summary>
        public string PowerID { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        public string PNodeID { get; set; }

        /// <summary>
        /// 节点类型。0：大菜单1：子菜单3：内页4：按钮5：其他
        /// </summary>
        public int? NodeType { get; set; }
        /// <summary>
        /// 节点类型名称
        /// </summary>
        public string NodeTypeName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public string IsEnable { get;set;}

        /// <summary>
        /// 添加人
        /// </summary>
        public string AddUser { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public string AddTime { get; set; }

        /// <summary>
        /// 节点地址
        /// </summary>
        public string NodeUrl { get; set; }

        /// <summary>
        /// 是否是前端
        /// </summary>
        public int? IsForeground { get; set; }

        /// <summary>
        ///进入路径
        /// </summary>
        public string EnterUrl { get; set; }

        /// <summary>
        /// 节点权限码
        /// </summary>
        public string NodeCode { get; set; }
        #endregion

        #region 函数
        public PowerInfoModel() { }

        public PowerInfoModel(PowerInfoServerEnt pim) {
            this.PowerID = pim.PowerID;
            this.NodeName = pim.NodeName;
            this.PNodeID = pim.PNodeID;
            this.NodeType = pim.NodeType;
            this.NodeCode = pim.NodeCode;
            switch (this.NodeType)
            {
                case 0:
                    this.NodeTypeName = "菜单";
                    break;
                case 1:
                    this.NodeTypeName = "子菜单(页面)";
                    break;
                case 2:
                    this.NodeTypeName = "按钮";
                    break;
                case 3:
                    this.NodeTypeName = "按钮(页面)";
                    break;
                default:
                    this.NodeTypeName = "其他";
                    break;
            }
            this.IsForeground = pim.IsForeground;
            this.NodeUrl = pim.AccessPath;
            this.EnterUrl = pim.EnterUrl;
            this.AddUser = pim.AddAccount;
            this.AddTime = pim.AddTime.ToString("yyyy-MM-dd");
            this.IsEnable = pim.IsEnabled == 1 ? "启用" : "禁用";

        }

        public PowerInfoModel[] PowerList(PowerInfoServerEnt[] rRse)
        {
            List<PowerInfoModel> list = new List<PowerInfoModel>();
            PowerInfoModel rar = null;
            foreach (PowerInfoServerEnt rrs in rRse)
            {
                rar = new PowerInfoModel(rrs);
                list.Add(rar);
            }
            return list.ToArray();
        }

        #endregion
    }

    [Serializable]
    public class PowerInfoServerModel
    {
        private List<PowerInfoModel> PowerInfolist = new List<PowerInfoModel>();
        public List<PowerInfoModel> powerInfolist
        {
            get { return PowerInfolist; }
            set { PowerInfolist = value; }
        }

    }
}