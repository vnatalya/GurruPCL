using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GurruPCL.Models.Results
{
    public class ErrorResponse
    {
        [JsonProperty("error_description")]
        public string Desscription { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
