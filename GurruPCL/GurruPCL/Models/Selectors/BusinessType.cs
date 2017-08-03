using Newtonsoft.Json;
using System;
namespace GurruPCL
{
	public class BusinessType
	{
		public BusinessType()
		{
		}

        [JsonProperty("id")]
		public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
	}
}
