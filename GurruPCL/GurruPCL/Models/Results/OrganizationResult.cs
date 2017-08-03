using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GurruPCL.Models.Results
{
    public class OrganizationResult : BaseResult
    {
        public List<Organization> Items { get; set; }

        public class OrganizationArray
        {
            [JsonProperty("data")]
            public Organization[] OrganizationArrayItem { get; set; }
        }
    }
}
