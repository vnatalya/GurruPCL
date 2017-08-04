
using GurruPCL.Models;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GurruPCL.ViewModels
{
	public class LoginViewModel
	{
		static object locker = new object();

        static string PASSWORD = "PASSWORD";
        static string USERNAME = "USERNAME";
        static string IS_LOGGED_IN = "ISLOGGEDIN";
        static string REMEMBER_ME = "REMEMBERME";
        static string ACCESS_TOKEN = "ACCESSTOKEN";
        static string TOKEN_SCHEME = "SCHEME";

        public string Password
        {
            get
            {
                return Application.Current.Properties.ContainsKey(PASSWORD) ? Application.Current.Properties[PASSWORD] as string : string.Empty;
            }
            set
            {
                if (Application.Current.Properties.ContainsKey(PASSWORD))
                    Application.Current.Properties.Remove(PASSWORD);
                Application.Current.Properties.Add(PASSWORD, value);
            }
        }

        public string Username
        {
            get
            {
                return Application.Current.Properties.ContainsKey(USERNAME) ? Application.Current.Properties[USERNAME] as string : string.Empty;
            }
            set
            {
                if (Application.Current.Properties.ContainsKey(USERNAME))
                    Application.Current.Properties.Remove(USERNAME);
                Application.Current.Properties.Add(USERNAME, value);
            }
        }

        public string TokenScheme
        {
            get
            {
                return Application.Current.Properties.ContainsKey(TOKEN_SCHEME) ? Application.Current.Properties[TOKEN_SCHEME] as string : string.Empty;
            }
            set
            {
                if (Application.Current.Properties.ContainsKey(TOKEN_SCHEME))
                    Application.Current.Properties.Remove(TOKEN_SCHEME);
                Application.Current.Properties.Add(TOKEN_SCHEME, value);
            }
        }

        public string AccessToken
        {
            get
            {
                return Application.Current.Properties.ContainsKey(ACCESS_TOKEN) ? Application.Current.Properties[ACCESS_TOKEN] as string : string.Empty;
            }
            set
            {
                if (Application.Current.Properties.ContainsKey(ACCESS_TOKEN))
                    Application.Current.Properties.Remove(ACCESS_TOKEN);
                Application.Current.Properties.Add(ACCESS_TOKEN, value);
            }
        }

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

		public bool IsLoggedIn
        {
            get
            {
                return Application.Current.Properties.ContainsKey(IS_LOGGED_IN) && (bool)Application.Current.Properties[IS_LOGGED_IN];
            }
            set
            {
                if (Application.Current.Properties.ContainsKey(IS_LOGGED_IN))
                    Application.Current.Properties.Remove(IS_LOGGED_IN);
                Application.Current.Properties.Add(IS_LOGGED_IN, value);
            }
        }
        
		public bool RememberMe 
		{
            get
            {
                return !Application.Current.Properties.ContainsKey(REMEMBER_ME) || (bool)Application.Current.Properties[REMEMBER_ME];
            }
            set
            {
                if (Application.Current.Properties.ContainsKey(REMEMBER_ME))
                    Application.Current.Properties.Remove(REMEMBER_ME);
                Application.Current.Properties.Add(REMEMBER_ME, value);
            }
		}
        
		public async Task<BaseResult> LoginAsync()
        {
			var result = await RequestSender.LoginAsync(Username, Password);

            if (result.Status == System.Net.HttpStatusCode.OK)
            {
                AccessToken = result.User.AccessToken;
                TokenScheme = result.User.TokenType;
            }

			IsLoggedIn = result.Status == System.Net.HttpStatusCode.OK;
            return result;
        }

        public void Logout()
        {
            IsLoggedIn = false;
        }
    }
}
