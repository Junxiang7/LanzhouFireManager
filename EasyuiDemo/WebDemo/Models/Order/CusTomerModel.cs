using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Models
{
    public class CusTomerModel
    {
        public int CusId { get; set; }

        public string CusCompany { get; set; }

        public string CusLinkMan { get; set; }

        public string CusLinkTel { get; set; }

        public string CusLinkPhone { get; set; }

        public string CreateTime { get; set; }
    }

    public class CusTomerInfoModel
    {
        public List<CusTomerModel> liCusTomer = new List<CusTomerModel>();
    }
}