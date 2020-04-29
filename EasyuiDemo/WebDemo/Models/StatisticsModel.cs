using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Models
{
    public class StatisticsModel
    {
        public string CusTomer { get; set; }

        public int FirstWorkstation { get; set; }

        public int SecondWorkstation { get; set; }

        public int ThirdWorkstation { get; set; }

        public int FourthWorkstation { get; set; }

        public int ScrapNum { get; set; }

        public string QueryTime { get; set; }

        public string EncodingOrder { get; set; }

        /// <summary>
        /// 1，2，3，4，5，8，20，35，50
        /// </summary>
        public int Kg_1 { get; set; }
        public int Kg_2 { get; set; }
        public int Kg_3 { get; set; }
        public int Kg_4 { get; set; }
        public int Kg_5 { get; set; }
        public int Kg_8 { get; set; }

        public int Kg_20 { get; set; }
        public int Kg_35 { get; set; }

        public int Kg_50 { get; set; }

        public int SumKG { get; set; }
    }

    public class StatisticsInfoModel
    {
        public List<StatisticsModel> liStatistics = new List<StatisticsModel>();
    }
}