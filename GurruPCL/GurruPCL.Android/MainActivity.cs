using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Tesseract;
using Tesseract.Droid;

using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Media;
using Android.Util;

namespace GurruPCL.Droid
{
    [Activity(Theme = "@style/MainTheme", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            RequestedOrientation = ScreenOrientation.Portrait | ScreenOrientation.Nosensor;

            App.DeviceHeight = Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density;
            App.DeviceWidth = Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density;

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            App.Container = new XLabs.Ioc.SimpleContainer();

            App.Container.Register<IDevice>(t => AndroidDevice.CurrentDevice);

            App.TesseractApi = new TesseractApi(Application, AssetsDeployment.OncePerVersion);

            if (!Resolver.IsSet)
                Resolver.SetResolver(App.Container.GetResolver());            
        }
    }
}

