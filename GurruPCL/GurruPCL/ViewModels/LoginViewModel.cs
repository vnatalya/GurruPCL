
using GurruPCL.Models;
using System.Threading.Tasks;

namespace GurruPCL.ViewModels
{
	public class LoginViewModel
	{
		static object locker = new object();

        public string Password { get; set; }
        public string Username { get; set; }

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

        public User CurrentUser { get; set; }

		public async Task<BaseResult> LoginAsync(string username, string password)
        {
			var result = await RequestSender.LoginAsync(username, password);

            if (result.Status == System.Net.HttpStatusCode.OK)
            {
                CurrentUser = result.User;
                if (rememberMe)
                {
                    Password = password;
                    Username = username;
                }
            }

			IsLoggedIn = result.Status == System.Net.HttpStatusCode.OK;
            return result;
        }

        public void Logout()
        {
        }

    }
}
