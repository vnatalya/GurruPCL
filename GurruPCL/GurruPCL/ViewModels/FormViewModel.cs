
namespace GurruPCL.ViewModels
{
    public class FormViewModel
    {
        static object locker;

        private static FormViewModel instance;
        public static FormViewModel Instance
        {
            get
            {
                lock (locker)
                {
                    if (instance == null)
                        instance = new FormViewModel();
                    return instance;
                }
            }
        }

        public async void GetInitializedAsync()
        {

        }

    }
}
