
using FireFighting.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireFighting.Models
{
    public class RoleInfoModelToEnt
    {
        /// <summary>
        /// 用户实体类转换
        /// </summary>
        /// <param name="RModel"></param>
        /// <returns></returns>
        public RoleInfoServerEnt ToRoleInfoEnt(RoleInfoModel RModel) 
        {
            RoleInfoServerEnt RserverEnt = new RoleInfoServerEnt();
            try
            {
                RserverEnt.AddAccount = RModel.AddAccount;
                RserverEnt.AddTime = RModel.AddTime;
                RserverEnt.LastDateTime = RModel.LastDateTime;
                RserverEnt.RoleId = RModel.RoleId;
                RserverEnt.RoleName = RModel.RoleName;
            }
            catch (Exception)
            {
                
                throw;
            }
            return RserverEnt;
        }

        /// <summary>
        /// 转为Model类
        /// </summary>
        /// <param name="RoleEnt"></param>
        /// <returns></returns>
        public RoleInfoModel ToRoleInfoModel(RoleInfoServerEnt RoleEnt) 
        {
            RoleInfoModel Rmodel = new RoleInfoModel();
            try
            {
                Rmodel.AddAccount = RoleEnt.AddAccount;
                Rmodel.AddTime = RoleEnt.AddTime;
                Rmodel.LastDateTime = RoleEnt.LastDateTime;
                Rmodel.RoleId = RoleEnt.RoleId;
                Rmodel.RoleName = RoleEnt.RoleName;
            }
            catch (Exception)
            {
                
                throw;
            }
            return Rmodel;
        }

        /// <summary>
        /// 转为list
        /// </summary>
        /// <param name="RoEnt"></param>
        /// <returns></returns>
        public List<RoleInfoModel> ToListRoleInfoServerModle(RoleInfolsEnt RoEnt) 
        { 
            List<RoleInfoModel> LsRoInModel = new List<RoleInfoModel>();
            try
            {
                if (RoEnt != null && RoEnt.roleInfolist.Count() > 0) 
                {
                    for (int i = 0; i < RoEnt.roleInfolist.Count(); i++)
                    {
                        RoleInfoModel Rmodel = new RoleInfoModel();
                        Rmodel.AddAccount = RoEnt.roleInfolist[i].AddAccount;
                        Rmodel.AddTime = RoEnt.roleInfolist[i].AddTime;
                        Rmodel.LastDateTime = RoEnt.roleInfolist[i].LastDateTime;
                        Rmodel.RoleId = RoEnt.roleInfolist[i].RoleId;
                        Rmodel.RoleName = RoEnt.roleInfolist[i].RoleName;

                        LsRoInModel.Add(Rmodel);
                    } 
                }
                else 
                {
                    return LsRoInModel;
                }
            }
            catch (Exception)
            {                
                throw;
            }
            return LsRoInModel;
        }
    }
}