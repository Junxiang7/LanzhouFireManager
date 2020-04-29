using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Models
{
    public class OperationModel
    {
        public string Operator { get; set; }

        public string OperationContent { get; set; }

        public string OperationTime { get; set; }

        public string OperationName { get; set; }

        public int OperId { get; set; }

        public string HidOperationContent { get; set; }
    }

    public class OperationInfoModel
    {
        public List<OperationModel> liOperation = new List<OperationModel>();
    }
}