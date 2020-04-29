using FireFighting.BLL;
using FireFighting.NET;
using FireFighting.Tool;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDemo.Models;

namespace WebDemo.Lib
{
    public class UserAuthList
    {
        protected Dictionary<string, MenuModel> MenuListData { get; set; } = new Dictionary<string, MenuModel>();
        public UserAuthList()
        {



            try
            {

                //var DataJson = System.IO.File.ReadAllText(Utils.GetMapPath("/DataBase/Menu.json"));

                var temp_obj = GetAllAuthListToModel(); //JsonConvert.DeserializeObject<Dictionary<string, MenuModel>>(DataJson);
                if (temp_obj != null)
                {

                    MenuListData = temp_obj;
                }
                else
                {
                    MenuListData.Add("Sys", new MenuModel()
                    {
                        menuname = "系统",
                        menus = new List<MenuInfoModel>()
                        {

                            new MenuInfoModel()
                            {
                                menuname ="首页",
                                url="/",
                                IsShow =false ,
                                IsDefutle=true

                            },new MenuInfoModel()
                            {
                                menuname ="首页",
                                url="/home/Index",
                                IsShow =false,
                                IsDefutle=true

                            }
                        }
                    });
                }

            }
            catch
            {

            }
        }

        private Dictionary<string, MenuModel> GetAllAuthListToModel()
        {
            Dictionary<string, MenuModel> dicMenu = new Dictionary<string, Models.MenuModel>();
            try
            {
                RoleManager roleManager = new RoleManager();
                SysRetResultEnt sysRetResult = roleManager.GetAllSysPower();
                dicMenu = GetMenuEntToModel(sysRetResult);
            }
            catch (Exception ex)
            {

            }
            return dicMenu;

        }

        private Dictionary<string, MenuModel> GetMenuEntToModel(SysRetResultEnt sysRetResult)
        {
            Dictionary<string, MenuModel> dicMenu = new Dictionary<string, Models.MenuModel>();
            try
            {
                if (sysRetResult != null && sysRetResult.liSysPowerInfo != null && sysRetResult.liSysPowerInfo.Count > 0)
                {
                    for (int i = 0; i < sysRetResult.liSysPowerInfo.Count; i++)
                    {
                        MenuModel menuModel = new MenuModel();
                        List<MenuInfoModel> limenuInfo = new List<MenuInfoModel>();
                        string En = string.Empty; ;
                        if (sysRetResult.liSysPowerInfo[i].PNodeID == 0)
                        {
                            En = sysRetResult.liSysPowerInfo[i].NodeNameEN;
                            menuModel.menuname = sysRetResult.liSysPowerInfo[i].NodeName;
                        }
                        for (int j = 0; j < sysRetResult.liSysPowerInfo.Count; j++)
                        {
                            if (sysRetResult.liSysPowerInfo[i].PowerID == sysRetResult.liSysPowerInfo[j].PNodeID)
                            {
                                MenuInfoModel menuInfoModel = new MenuInfoModel();
                                menuInfoModel.IsDefutle = sysRetResult.liSysPowerInfo[j].IsDefutle;
                                menuInfoModel.IsShow = sysRetResult.liSysPowerInfo[j].IsShow;
                                menuInfoModel.menuname = sysRetResult.liSysPowerInfo[j].NodeName;
                                menuInfoModel.url = sysRetResult.liSysPowerInfo[j].AccessPath;
                                limenuInfo.Add(menuInfoModel);
                                for (int w = 0; w < sysRetResult.liSysPowerInfo.Count; w++)
                                {
                                    if(sysRetResult.liSysPowerInfo[j].PowerID== sysRetResult.liSysPowerInfo[w].PNodeID)
                                    {
                                        MenuInfoModel m_menuInfoModel = new MenuInfoModel();
                                        m_menuInfoModel.IsDefutle = sysRetResult.liSysPowerInfo[w].IsDefutle;
                                        m_menuInfoModel.IsShow = sysRetResult.liSysPowerInfo[w].IsShow;
                                        m_menuInfoModel.menuname = sysRetResult.liSysPowerInfo[w].NodeName;
                                        m_menuInfoModel.url = sysRetResult.liSysPowerInfo[w].AccessPath;
                                        limenuInfo.Add(m_menuInfoModel);
                                    }
                                }
                            }
                        }
                        if (sysRetResult.liSysPowerInfo[i].PNodeID == 0)
                        {
                            menuModel.menus = limenuInfo;
                            dicMenu.Add(En, menuModel);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return dicMenu;
        }


        /// <summary>
        /// 获取所有权限名称
        /// </summary>
        /// <returns></returns>
        public  Dictionary<string, MenuModel> GetAllAuthList()
        {
            var dic = new Dictionary<string, MenuModel>();

            foreach (var i in MenuListData)
            {
                dic.Add(i.Key, new MenuModel()
                {
                    //icon = i.Value.icon,
                    menuname = i.Value.menuname,
                    menus = new List<MenuInfoModel>()
                });
                foreach (var d in i.Value.menus)
                {
                    if (!dic[i.Key].menus.Exists(x => x.menuname == d.menuname))
                    {
                        dic[i.Key].menus.Add(d);
                    }
                }
                if (dic[i.Key].menus.Count == 0)
                {
                    dic.Remove(i.Key);
                }
            }
            return dic;
        }
        /// <summary>
        /// 显示菜单
        /// </summary>
        public  List<MenuModel> MenuList(AccountInfoModel userAccount)
        {

            List<MenuModel> m_AuthList = GetAuthList(userAccount);
            var temp = new List<MenuModel>();
            foreach (var i in m_AuthList)
            {
                MenuModel obj = new MenuModel();
                //obj.icon = i.icon;

                obj.menuname = i.menuname;
                obj.menus = new List<MenuInfoModel>();
                obj.menus.AddRange(i.menus.FindAll(d => d.IsShow));
                //obj.menus.AddRange(i.menus.FindAll(d => d.IsDefutle));
                if (obj.menus.Count > 0)
                {
                    temp.Add(obj);
                }

            }
            return temp;


        }
        /// <summary>
        /// 获取授权目录
        /// </summary>
        /// <returns></returns>
        public  List<MenuModel> GetAuthList(AccountInfoModel userAccount)
        {
            List<MenuModel> m_AuthList = null;
            if (HttpContext.Current.Session["UserAuthList_Cache"] != null)
            {
                m_AuthList = (List<MenuModel>)HttpContext.Current.Session["UserAuthList_Cache"];

            }
            else
            {
                m_AuthList = new List<MenuModel>();
                foreach (string str in userAccount.Permissions.Split('|'))
                {
                    string key = str.Substring(0, str.IndexOf(":"));
                    if (MenuListData.ContainsKey(key))
                    {

                        string[] pages = str.Substring(str.IndexOf(":") + 1).Split(',');
                        MenuModel obj = new MenuModel();
                        //obj.icon = MenuListData[key].icon;
                        obj.menuname = MenuListData[key].menuname;
                        obj.menus = new List<MenuInfoModel>();
                        foreach (string pagestr in pages)
                        {
                            obj.menus.AddRange(MenuListData[key].menus.FindAll(o => o.menuname == pagestr));
                        }
                        if (obj.menus.Count > 0)
                        {
                            m_AuthList.Add(obj);
                        }
                    }
                }
                HttpContext.Current.Session["UserAuthList_Cache"] = m_AuthList;
            }
            return m_AuthList;
        }
    }
}