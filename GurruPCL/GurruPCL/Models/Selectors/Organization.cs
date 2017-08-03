using Newtonsoft.Json;
using System;
namespace GurruPCL
{
	public class Organization
	{
		public Organization()
		{
		}

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public string BusinessPhone { get; set; }
		public string PrimaryEmail { get; set; }
		public string ABN { get; set; }
		public int ContactsCount { get; set; }
	}
}
