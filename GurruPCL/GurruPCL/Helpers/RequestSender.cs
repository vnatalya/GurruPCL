using System;
using System.Threading.Tasks;
using GurruPCL.Models;
using Plugin.Connectivity;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using ModernHttpClient;
using GurruPCL.Models.Results;
using System.Collections.Generic;
using System.Net.Http.Headers;
using static GurruPCL.Models.Results.OrganizationResult;

namespace GurruPCL
{
	public static class RequestSender
	{
		public static bool IsInternetAwailable { get { return CrossConnectivity.Current.IsConnected; } }

        private static object obj = new object();
        private static HttpClient client;
        public static HttpClient Client
        {
            get
            {
                lock (obj)
                {
                    if (client == null)
                    {
                        client = new System.Net.Http.HttpClient(new NativeMessageHandler());
                        client.BaseAddress = new Uri("https://premierdev.gurru.com.au");
                    }
                    return client;
                }
            }
        }        

        public static async Task<LoginResult> LoginAsync(string username, string password)
		{
            var result = new LoginResult();

            if (!IsInternetAwailable)
            {
                result.Title = "No internet";
                result.Message = "Action is not possible as there is no internet connection";
                return result;
            }

            var content = new StringContent("grant_type=password&username=wtAdmin&password=Aa123456!", Encoding.UTF8);
            HttpResponseMessage response = await Client.PostAsync("token", content);

            result.Status = response.StatusCode;
            var stringResponse = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<ErrorResponse>(stringResponse);
                result.Title = error.Error;
                result.Message = error.Desscription;
            }
            else
            {
                result.User = JsonConvert.DeserializeObject<User>(stringResponse);
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(result.User.TokenType, result.User.AccessToken);
            }
            return result;
		}

		public static async Task<BusinessTypeResult> GetBusinessTypesAsync()
		{
            var result = new BusinessTypeResult();

            if (!IsInternetAwailable)
            {
                result.Title = "No internet";
                result.Message = "Action is not possible as there is no internet connection";
                return result;
            }

            HttpResponseMessage response = await Client.GetAsync("api/BusinessTypes/GetAll");

            result.Status = response.StatusCode;
            var stringResponse = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<ErrorResponse>(stringResponse);
                result.Title = string.IsNullOrEmpty(error.Error) ? "Error" : error.Error;
                result.Message = string.IsNullOrEmpty(error.Desscription) ? "An error has occured" : error.Desscription;
            }
            else
                result.Items = JsonConvert.DeserializeObject<List<BusinessType>>(stringResponse);

            return result;
        }

		public static async Task<OrganizationResult> GetOrganizationsAsync()
		{
            var result = new OrganizationResult();

            if (!IsInternetAwailable)
            {
                result.Title = "No internet";
                result.Message = "Action is not possible as there is no internet connection";
                return result;
            }

            HttpResponseMessage response = await Client.GetAsync("api/Organisations/GetAll?take=5&skip=0&page=1&pageSize=5&filter%5Blogic%5D=and&filter%5Bfilters%5D%5B0%5D%5Blogic%5D=or&filter%5Bfilters%5D%5B1%5D%5Bfield%5D=parentOrganisationId&filter%5Bfilters%5D%5B1%5D%5Boperator%5D=eq&filter%5Bfilters%5D%5B1%5D%5Bvalue%5D=)");

            result.Status = response.StatusCode;
            var stringResponse = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<ErrorResponse>(stringResponse);
                result.Title = string.IsNullOrEmpty(error.Error) ? "Error" : error.Error;
                result.Message = string.IsNullOrEmpty(error.Desscription) ? "An error has occured" : error.Desscription;
            }
            else
            {
                var array = JsonConvert.DeserializeObject<OrganizationArray>(stringResponse);

                result.Items = new List<Organization>();

                for(int i = 0; i < array.OrganizationArrayItem?.Length; ++ i)
                    result.Items.Add(array.OrganizationArrayItem[i]);
            }

            return result;
        }

		public static async Task<SalesActivityResult> GetSalesActivityAsync()
		{
            var result = new SalesActivityResult();

            if (!IsInternetAwailable)
            {
                result.Title = "No internet";
                result.Message = "Action is not possible as there is no internet connection";
                return result;
            }

            HttpResponseMessage response = await Client.GetAsync("api/SalesActivities/GetAll");

            result.Status = response.StatusCode;
            var stringResponse = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<ErrorResponse>(stringResponse);
                result.Title = string.IsNullOrEmpty(error.Error) ? "Error" : error.Error;
                result.Message = string.IsNullOrEmpty(error.Desscription) ? "An error has occured" : error.Desscription;
            }
            else
                result.Items = JsonConvert.DeserializeObject<List<SalesActivity>>(stringResponse);

            return result;
        }

		public static async Task<SalesPersonResult> GetSalesPersonsAsync()
		{
            var result = new SalesPersonResult ();

            if (!IsInternetAwailable)
            {
                result.Title = "No internet";
                result.Message = "Action is not possible as there is no internet connection";
                return result;
            }

            HttpResponseMessage response = await Client.GetAsync("api/Users/GetAllByRole?roleNames=Sales,Approvers&_=1501719298586)");

            result.Status = response.StatusCode;
            var stringResponse = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<ErrorResponse>(stringResponse);
                result.Title = string.IsNullOrEmpty(error.Error) ? "Error" : error.Error;
                result.Message = string.IsNullOrEmpty(error.Desscription) ? "An error has occured" : error.Desscription;
            }
            else
                result.Items = JsonConvert.DeserializeObject<List<SalesPerson>>(stringResponse);

            return result;

        }

        public static async Task<SourceResult> GetSourcesAsync()
        {
            var result = new SourceResult();

            if (!IsInternetAwailable)
            {
                result.Title = "No internet";
                result.Message = "Action is not possible as there is no internet connection";
                return result;
            }

            HttpResponseMessage response = await Client.GetAsync("api/Users/GetAllByRole?roleNames=Sales,Approvers&_=1501719298586)");

            result.Status = response.StatusCode;
            var stringResponse = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<ErrorResponse>(stringResponse);
                result.Title = string.IsNullOrEmpty(error.Error) ? "Error" : error.Error;
                result.Message = string.IsNullOrEmpty(error.Desscription) ? "An error has occured" : error.Desscription;
            }
            else
                result.Items = JsonConvert.DeserializeObject<List<Source>>(stringResponse);

            return result;

        }
    }
}
