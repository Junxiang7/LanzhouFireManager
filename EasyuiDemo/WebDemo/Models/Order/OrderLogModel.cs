using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Models
{
    public class OrderLogModel
    {
        public string ProductId { get; set; }

        public string Operator { get; set; }

        public string OperatingTime { get; set; }

        public string OperationalContent { get; set; }
    }
}