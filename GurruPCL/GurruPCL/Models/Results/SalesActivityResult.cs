﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GurruPCL.Models.Results
{
    public class SalesActivityResult : BaseResult
    {
        public List<SalesActivity> Items { get; set; }
    }
}
