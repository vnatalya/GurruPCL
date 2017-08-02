
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

        public bool RememberMe { get; set; }

        public async Task<BaseResult> LoginAsync()
        {
            var result = new BaseResult();
            return result;
        }

        public void Logout()
        {
        }

    }
}
