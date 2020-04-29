using FireFighting.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Models
{
    public class StatisticsEntToModel
    {
        public List<StatisticsModel> GetStatisticsToModel(List<StatisticsEnt> LiStatisticsEnt)
        {
            List<StatisticsModel> liStatisticsModel = new List<Models.StatisticsModel>();
            try
            {
                if(LiStatisticsEnt!=null&& LiStatisticsEnt.Count>0)
                {
                    for (int i = 0; i < LiStatisticsEnt.Count; i++)
                    {
                        StatisticsModel statisticsModel = new StatisticsModel();
                        statisticsModel.CusTomer = LiStatisticsEnt[i].CusTomer;
                        statisticsModel.FirstWorkstation = LiStatisticsEnt[i].FirstWorkstation;
                        statisticsModel.ScrapNum = LiStatisticsEnt[i].ScrapNum;
                        statisticsModel.SecondWorkstation = LiStatisticsEnt[i].SecondWorkstation;
                        statisticsModel.ThirdWorkstation = LiStatisticsEnt[i].ThirdWorkstation;
                        statisticsModel.FourthWorkstation = LiStatisticsEnt[i].FourthWorkstation;
                        statisticsModel.QueryTime= LiStatisticsEnt[i].QueryTime;
                        statisticsModel.EncodingOrder = LiStatisticsEnt[i].EncodingOrder;
                        statisticsModel.Kg_1= LiStatisticsEnt[i].Kg_1;
                        statisticsModel.Kg_2 = LiStatisticsEnt[i].Kg_2;
                        statisticsModel.Kg_3 = LiStatisticsEnt[i].Kg_3;
                        statisticsModel.Kg_4 = LiStatisticsEnt[i].Kg_4;
                        statisticsModel.Kg_5 = LiStatisticsEnt[i].Kg_5;
                        statisticsModel.Kg_8 = LiStatisticsEnt[i].Kg_8;
                        statisticsModel.Kg_20 = LiStatisticsEnt[i].Kg_20;
                        statisticsModel.Kg_35 = LiStatisticsEnt[i].Kg_35;
                        statisticsModel.Kg_50 = LiStatisticsEnt[i].Kg_50;
                        statisticsModel.SumKG = LiStatisticsEnt[i].SumKG;

                        liStatisticsModel.Add(statisticsModel);
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return liStatisticsModel;
        }
    }
}