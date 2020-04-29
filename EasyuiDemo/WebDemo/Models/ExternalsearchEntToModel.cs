using FireFighting.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Models
{
    public class ExternalsearchEntToModel
    {
        public ExternalsearchInfoModel ListExternalsearchEntToModel(List<ExternalsearchEnt> Externalsearchli)
        {
            ExternalsearchInfoModel externalsearchInfoModel = new Models.ExternalsearchInfoModel();
            List<ExternalsearchModel> liExternalsearch = new List<ExternalsearchModel>();
            try
            {
                if (Externalsearchli != null && Externalsearchli.Count > 0)
                {
                    for (int i = 0; i < Externalsearchli.Count; i++)
                    {
                        ExternalsearchModel externalsearchModel = new ExternalsearchModel();
                        externalsearchModel.ConditionStr = Externalsearchli[i].ConditionStr;
                        externalsearchModel.Content = Externalsearchli[i].Content;
                        if (!string.IsNullOrEmpty(externalsearchModel.Content) && externalsearchModel.Content.Length > 25)
                        {
                            externalsearchModel.Content = externalsearchModel.Content.Substring(0, 24) + "...";
                        }
                        externalsearchModel.HidContent = Externalsearchli[i].Content;
                        externalsearchModel.CreateTime = Externalsearchli[i].CreateTime;
                        externalsearchModel.Ip = Externalsearchli[i].Ip;
                        externalsearchModel.id = Externalsearchli[i].id;
                        liExternalsearch.Add(externalsearchModel);
                    }

                }
                externalsearchInfoModel.liExternalsearch = liExternalsearch;
            }
            catch (Exception ex)
            {

            }
            return externalsearchInfoModel;
        }
    }
}