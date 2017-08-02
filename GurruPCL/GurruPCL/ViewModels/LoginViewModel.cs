
using GurruPCL.Models;
using System.Threading.Tasks;

namespace GurruPCL.ViewModels
{
	public class LoginViewModel
	{
		static object locker = new object();

		private static LoginViewModel instance;
		public static LoginViewModel Instance
		{
			get
			{
				lock (locker)
				{
					if (instance == null)
						instance = new LoginViewModel();
					return instance;
				}
			}
		}

		public bool IsLoggedIn { get; set;}

		private bool rememberMe = true;
		public bool RememberMe 
		{ 
			get { return rememberMe; } 
			set { rememberMe = value; } 
		}

		public async Task<BaseResult> LoginAsync(string username, string password)
        {
			var result = await RequestSender.LoginAsync(username, password);
			IsLoggedIn = result.Status == System.Net.HttpStatusCode.OK;
            return result;
        }

        public void Logout()
        {
        }

    }
}
