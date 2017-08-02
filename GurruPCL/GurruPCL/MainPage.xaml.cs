using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Media;

namespace GurruPCL
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            Container.WidthRequest = App.DeviceWidth;
        }

        private async void AddLeadButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FormPage(), false);
        }

        private async void FromVCardButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (Media.MediaPicker.IsCameraAvailable)
                {
                    var options = new CameraMediaStorageOptions()
                    {
                        DefaultCamera = CameraDevice.Front,
                        SaveMediaOnCapture = true,
                        Directory = "YourAppName",
                        Name = string.Format("YourAppName_{0}", DateTime.Now.ToString("yyMMddhhmmss")),
                        MaxPixelDimension = 1024,
                        PercentQuality = 85
                    };

                    var mediaFile = await Media.MediaPicker.TakePhotoAsync(options);

                    SetLoader(true);

                    if (!App.TesseractApi.Initialized)
                    {
                        var res = await App.TesseractApi.Init("eng");
                    }

                    bool tooLongProccessing = false;

                    //Device.StartTimer(TimeSpan.FromMinutes(1), () =>
                    //{
                    //    tooLongProccessing = true;
                    //    SetLoader(false);
                    //    DisplayAlert("Error", "Sorry, couldn't parse the photo", "Ok");
                    //    return false;
                    //});

                    var res2 = await App.TesseractApi.SetImage(mediaFile.Path);

                    //if (tooLongProccessing)
                    //    return;

                    string text = App.TesseractApi.Text;


                    await Navigation.PushAsync(new FormPage(), false);                 
                }
                else
                {
                    await DisplayAlert("Error", "Camera is not awailable", "Ok");
                }
            }
            catch (TaskCanceledException)
            {
                SetLoader(false);
            }
            catch (Exception ex)
            {
                SetLoader(false);
                await DisplayAlert("Error", "An unexpected error has occured", "Ok");
            }
        }

        void SetLoader(bool show)
        {
            Loader.IsRunning = show;
            Loader.IsVisible = show;
            AddLeadButton.IsEnabled = !show;
            FromVCardButton.IsEnabled = !show;
        }

        public static class Media
        {
            static IMediaPicker mediaPicker = null;
            public static IMediaPicker MediaPicker
            {
                get
                {
                    if (mediaPicker == null)
                    {
                        var device = Resolver.Resolve<IDevice>();
                        mediaPicker = DependencyService.Get<IMediaPicker>() ?? device.MediaPicker;
                        if (mediaPicker == null) throw new NullReferenceException("MediaPicker DependencyService.Get error");
                    }

                    return mediaPicker;
                }
            }
        }

        private async void LogoutButton_Tapped(object sender, EventArgs e)
        {            
            await Navigation.PushAsync(new LoginPage());
            Navigation.RemovePage(this);
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}
