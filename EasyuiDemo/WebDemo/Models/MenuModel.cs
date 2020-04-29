using System.Collections.Generic;

namespace WebDemo.Models
{
    public class MenuModel
    {
        /// <summary>
        /// 菜单图标
        /// </summary>
        ///public string icon { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string menuname { get; set; }
        /// <summary>
        /// 菜单列表
        /// </summary>
        public List<MenuInfoModel> menus { get; set; }

    }
    /// <summary>
    /// URL路径
    /// </summary>
    public class MenuInfoModel
    {

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string menuname { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 是否显示:默认显示
        /// </summary>
        public bool IsShow { get; set; } = true;
        /// <summary>
        /// 默认权限
        /// </summary>
        public bool IsDefutle { get; set; } = false;

    }
}