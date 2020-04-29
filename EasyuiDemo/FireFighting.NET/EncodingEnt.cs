using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFighting.NET
{
    public class EncodingEnt
    {
        public int Id { get; set; }

        public string CusCompany { get; set; }

        public string CusLinkMan { get; set; }

        public string EncodingSatrt { get; set; }

        public string EncodingEnd { get; set; }

        public string CreateTime { get; set; }

        public int CusId { get; set; }

        public string EncodingOrder { get; set; }

        public int KgNum { get; set; }

        public int EncodeNum { get; set; }
    }

    public class EncodingEntInfo
    {
        public List<EncodingEnt> liEncoding = new List<EncodingEnt>();
    }
}
