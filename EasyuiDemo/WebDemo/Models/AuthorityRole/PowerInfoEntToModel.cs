using FireFighting.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Models
{
    public class PowerInfoEntToModel
    {
        public List<SysPowerInfoModel> PowerInfoEntTo(SysRetResultEnt sysRetResult)
        {
            List<SysPowerInfoModel> liSysPowerInfo = new List<SysPowerInfoModel>();
            try
            {
                if (sysRetResult != null && sysRetResult.liSysPowerInfo != null && sysRetResult.liSysPowerInfo.Count > 0)
                {
                    for (int i = 0; i < sysRetResult.liSysPowerInfo.Count; i++)
                    {
                        SysPowerInfoModel powerInfoModel = new SysPowerInfoModel();
                        powerInfoModel.AccessPath = sysRetResult.liSysPowerInfo[i].AccessPath;
                        powerInfoModel.IsDefutle = sysRetResult.liSysPowerInfo[i].IsDefutle;
                        powerInfoModel.IsShow = sysRetResult.liSysPowerInfo[i].IsShow;
                        powerInfoModel.NodeName = sysRetResult.liSysPowerInfo[i].NodeName;
                        powerInfoModel.NodeNameEN = sysRetResult.liSysPowerInfo[i].NodeNameEN;
                        powerInfoModel.PNodeID = sysRetResult.liSysPowerInfo[i].PNodeID;
                        powerInfoModel.PowerID = sysRetResult.liSysPowerInfo[i].PowerID;
                        liSysPowerInfo.Add(powerInfoModel);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return liSysPowerInfo;
        }
    }
}