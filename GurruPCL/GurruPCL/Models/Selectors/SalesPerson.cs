using Newtonsoft.Json;
using System;
namespace GurruPCL
{
	public class SalesPerson
	{
		public SalesPerson()
		{
		}

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }
		[JsonProperty("businessTypeId")]
        public Guid? BusinessTypeId { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
	}
}
