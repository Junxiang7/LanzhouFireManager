using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Models
{
    public class ExternalsearchModel
    {
        public string Ip { get; set; }

        public string ConditionStr { get; set; }

        public string Content { get; set; }

        public string CreateTime { get; set; }

        public string HidContent { get; set; }

        public int id { get; set; }
    }

    public class ExternalsearchInfoModel
    {
        public List<ExternalsearchModel> liExternalsearch = new List<ExternalsearchModel>();
    }
}