using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFighting.NET
{
    public class ExternalsearchEnt
    {
        public string Ip { get; set; }

        public string ConditionStr { get; set; }

        public string Content { get; set; }

        public string CreateTime { get; set; }

        public int id { get; set; }
    }

    public class ExternalsearchInfoEnt
    {
        public List<ExternalsearchEnt> liExternalsearch = new List<ExternalsearchEnt>();
    }
}
