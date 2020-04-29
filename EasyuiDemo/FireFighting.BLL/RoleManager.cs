
using FireFighting.DAL;
using FireFighting.NET;
using FireFighting.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFighting.BLL
{
    public class RoleManager
    {
        public RetResultEnt GetPowerByUser(RoleAccountRelationServerEnt Rar, int PageSize, int CurrentPage)
        {
            RetResultEnt rt = new RetResultEnt();
            try
            {
                RoleRelationServer RoleRelation = new RoleRelationServer();
                rt = RoleRelation.GetRoleAccountInfo(Rar, PageSize, CurrentPage);
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("RoleManager", "GetPowerByUser", ex.Message.ToString(), "");
            }
            return rt;
        }

        public RetResultEnt GetRoleAllPowerInfo(int? RoleId, int? NodeType, int? PNodeID, int IsForeground)
        {
            RetResultEnt rt = new RetResultEnt();
            try
            {
                RoleRelationServer RoleRelation = new RoleRelationServer();
                rt = RoleRelation.GetRolePower(RoleId, NodeType, PNodeID, IsForeground);
            }
            catch (Exception ex)
            {
                ErrorLog.InsertError("RoleManager", "GetRoleAllPowerInfo", ex.Message.ToString(), "");
            }
            return rt;
        }

        public RetResultEnt GetPower(string Account)
        {
            RetResultEnt rt = new RetResultEnt();
            RoleAccountRelationServerEnt rrs = new RoleAccountRelationServerEnt();
            rrs.Account = Account;
            RoleRelationServer RoleRelation = new RoleRelationServer();
            RetResultEnt rel = RoleRelation.GetRoleAccountInfo(rrs, 10, 1);
            if (rel.Result)
            {
                int roleId = roleId = Convert.ToInt32(rel.RoleAccount[0].RoleId);
                rt = RoleRelation.GetRolePower(roleId, null, null, 3);
            }

            return rt;
        }

        public SysRetResultEnt GetSysPower(int Id)
        {
            RoleRelationServer RoleRelation = new RoleRelationServer();
            SysRetResultEnt sysRetResult = RoleRelation.GetSysPowerService(Id);
            return sysRetResult;
        }

        public SysRetResultEnt GetAllSysPower()
        {
            RoleRelationServer RoleRelation = new RoleRelationServer();
            SysRetResultEnt sysRetResult = RoleRelation.GetAllSysPowerService();
            return sysRetResult;

        }

        public List<SysPowerByAccount> GetSysPowerByAccount(int Id)
        {
            RoleRelationServer RoleRelation = new RoleRelationServer();
            return RoleRelation.GetSysPowerByAccountService(Id);
        }

        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="Id">用户ID</param>
        /// <param name="pids">权限路径ID 集合 ，分割</param>
        /// <param name="Type"> 0 新增用户添加权限  1 修改用户权限</param>
        /// <returns></returns>
        public int UpdateRoleInfo(int Id, string pids, int Type)
        {
            RoleRelationServer RoleRelation = new RoleRelationServer();
            return RoleRelation.UpdateRoleInfoService(Id, pids, Type);
        }
    }
}
