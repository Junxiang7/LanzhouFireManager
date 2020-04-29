using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFighting.NET
{
    public class OperationEnt
    {
        public string Operator { get; set; }

        public string OperationContent { get; set; }

        public string OperationTime { get; set; }

        public string OperationName { get; set; }

        public int OperId { get; set; }
    }

    public class OperationInfoEnt
    {
        public List<OperationEnt> liOperation = new List<OperationEnt>();
    }
}
