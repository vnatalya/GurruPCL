using Xamarin.Forms;
using XLabs.Ioc;
using Tesseract;
using GurruPCL.ViewModels;

namespace GurruPCL
{
    public partial class App : Application
    {
        public static SimpleContainer Container;

        public static ITesseractApi TesseractApi;

        public static float DeviceHeight;
        public static float DeviceWidth;

        public App()
        {
            InitializeComponent();
            
            MainPage = new NavigationPage(LoginViewModel.Instance.IsLoggedIn ? new MainPage() as Page :  new LoginPage() as Page);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
