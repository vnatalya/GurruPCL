using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GurruPCL.Models.Results
{
    public class SalesPersonResult : BaseResult
    {
        public List<SalesPerson> Items { get; set; }
    }
}
