using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFighting.NET
{
    public class ScraporderEnt
    {
        public string EncodingOrder { get; set; }

        public int ScraporderNum { get; set; }

        public string KgNum { get; set; }
    }

    public class ScraporderInfoEnt
    {
        public List<ScraporderEnt> liScraporder = new List<ScraporderEnt>();
    }
}
