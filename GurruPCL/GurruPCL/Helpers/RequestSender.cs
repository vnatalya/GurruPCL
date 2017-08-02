using System;
using System.Threading.Tasks;
using GurruPCL.Models;
using Plugin.Connectivity;

namespace GurruPCL
{
	public static class RequestSender
	{
		public static bool IsInternetAwailable { get { return CrossConnectivity.Current.IsConnected; } }

		public static async Task<BaseResult> LoginAsync(string username, string password)
		{
			return new BaseResult();
		}

		public static async Task<BaseResult> GetBusinessTypesAsync(string username, string password)
		{
			return new BaseResult();
		}

		public static async Task<BaseResult> GetOrganizationsAsync()
		{
			return new BaseResult();
		}

		public static async Task<BaseResult> GetSalesActivityAsync()
		{
			return new BaseResult();
		}

		public static async Task<BaseResult> GetSalesPersonsAsync()
		{
			return new BaseResult();
		}
	}
}
