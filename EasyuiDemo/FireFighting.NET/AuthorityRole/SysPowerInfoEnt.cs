﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFighting.NET
{
    public class SysPowerInfoEnt
    {
        public int PowerID { get; set; }

        public string NodeName { get; set; }

        public string NodeNameEN { get; set; }

        public int PNodeID { get; set; }

        public string AccessPath { get; set; }

        public bool IsShow { get; set; }

        public bool IsDefutle { get; set; }
    }
}
