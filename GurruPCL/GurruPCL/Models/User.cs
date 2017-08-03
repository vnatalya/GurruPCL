using Newtonsoft.Json;
using System;
namespace GurruPCL
{
	public class User
	{
		public User()
		{
		}
        
		public string Username { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("id")]
        public Guid Id { get; set; }

    }
}
