
using FireFighting.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo
{
    public class RolePowerModelToEnt
    {
        /// <summary>
        /// 用户实体类转换
        /// </summary>
        /// <param name="RModel"></param>
        /// <returns></returns>
        public PowerInfoServerEnt ToRolePowerEnt(PowerInfoModel PModel)
        {
            PowerInfoServerEnt PserverEnt = new PowerInfoServerEnt();
            try
            {
                PserverEnt.PowerID = PModel.PowerID;
                PserverEnt.NodeName = PModel.NodeName;
                PserverEnt.PNodeID = PModel.PNodeID;
                PserverEnt.NodeType = PModel.NodeType;
            }
            catch (Exception)
            {

                throw;
            }
            return PserverEnt;
        }


        /// <summary>
        /// 转为Model类
        /// </summary>
        /// <param name="RoleEnt"></param>
        /// <returns></returns>
        public PowerInfoModel ToRolePowerModel(PowerInfoServerEnt PoleEnt)
        {
            PowerInfoModel Pmodel = new PowerInfoModel();
            try
            {
                Pmodel.PowerID = PoleEnt.PowerID;
                Pmodel.NodeName = PoleEnt.NodeName;
                Pmodel.PNodeID = PoleEnt.PNodeID;
                Pmodel.NodeType = PoleEnt.NodeType;
            }
            catch (Exception)
            {

                throw;
            }
            return Pmodel;
        }


        /// <summary>
        /// 转为list
        /// </summary>
        /// <param name="RoEnt"></param>
        /// <returns></returns>
        public PowerInfoServerModel ToListPoleInfoServerModle(PowerInfolsEnt PoEnt)
        {
            PowerInfoServerModel PserverModel = new PowerInfoServerModel();
            List<PowerInfoModel> LsPoInModel = new List<PowerInfoModel>();
            try
            {
                if (PoEnt != null && PoEnt.powerInfolist.Count() > 0)
                {
                    for (int i = 0; i < PoEnt.powerInfolist.Count(); i++)
                    {
                        PowerInfoModel Rmodel = new PowerInfoModel();
                        Rmodel.NodeName = PoEnt.powerInfolist[i].NodeName;
                        Rmodel.PowerID = PoEnt.powerInfolist[i].PowerID;
                        Rmodel.PNodeID = PoEnt.powerInfolist[i].PNodeID;
                        Rmodel.NodeType = PoEnt.powerInfolist[i].NodeType;

                        LsPoInModel.Add(Rmodel);
                    }
                    PserverModel.powerInfolist = LsPoInModel;
                }
                else
                {
                    return PserverModel;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return PserverModel;
        }
    }
}