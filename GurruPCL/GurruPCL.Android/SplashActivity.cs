using Android.App;
using Android.Content;
using Android.OS;
using Android.Content.PM;

namespace GurruPCL.Droid
{
    [Activity(MainLauncher = true, NoHistory = true, Theme = "@style/SplashTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : Android.Support.V7.App.AppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            RequestedOrientation = ScreenOrientation.Portrait;
        }

        protected override void OnResume()
        {
            base.OnResume();
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }
    }
}