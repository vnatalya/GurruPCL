using GurruPCL.Helpers;
using GurruPCL.ViewModels;
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
        MediaFile mediaFile;

        public MainPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            Container.WidthRequest = App.DeviceWidth;
        }

        private async void AddLeadButton_Clicked(object sender, EventArgs e)
        {
            SetLoader(true);
            var res = await FormViewModel.Instance.GetInitializedAsync();
            SetLoader(false);
            if (res.Status != System.Net.HttpStatusCode.OK)
                await DisplayAlert(res.Title, res.Message, "Ok");
            else
                await Navigation.PushAsync(new FormPage(), false);
        }

        private async void FromVCardButton_Clicked(object sender, EventArgs e)
        {
            var answear = await DisplayActionSheet("Choose action:", "Cancel", "", "Take photo", "Select existing picture");

            if (answear == null || answear.Equals("Cancel"))
                return;

            var res = answear.Equals("Take photo") ? await TakePhoto() : await SelectFromGallery();

            if (mediaFile == null)
                return;

            SetLoader(true);

            if (!App.TesseractApi.Initialized)
                res = await App.TesseractApi.Init("eng");

            bool tooLongProccessing = false;

            Device.StartTimer(TimeSpan.FromSeconds(40), () =>
            {
                tooLongProccessing = true;
                SetLoader(false);
                DisplayAlert("Error", "Sorry, couldn't parse the photo", "Ok");
                return false;
            });

            try
            {
                res = await App.TesseractApi.SetImage(mediaFile.Path);
                
                if (answear.Equals("Take photo"))
                    DependencyService.Get<IPhotoCleaner>().DeletePhoto(mediaFile.Path);
            }
            catch (Exception btm)
            {
                SetLoader(false);
                await DisplayAlert("Error", "Sorry, couldn't parse the photo", "Ok");
            }

            if (tooLongProccessing)
                return;

            var initResult = await FormViewModel.Instance.GetInitializedAsync();

            if (initResult.Status != System.Net.HttpStatusCode.OK)
            {
                SetLoader(false);
                await DisplayAlert(initResult.Title, initResult.Message, "Ok");
                return;
            }

            if (!string.IsNullOrEmpty(App.TesseractApi.Text))
            {
                FormViewModel.Instance.CurrentForm.Email = PhotoHelper.GetEmail(App.TesseractApi.Text);
                FormViewModel.Instance.CurrentForm.BusinessPhone = PhotoHelper.GetPhone(App.TesseractApi.Text);
            }

            await Navigation.PushAsync(new FormPage(), false);
        }

        async Task<bool> TakePhoto()
        {
            try
            {
                if (Media.MediaPicker.IsCameraAvailable)
                {
                    var options = new CameraMediaStorageOptions()
                    {
                        DefaultCamera = CameraDevice.Front,
                        SaveMediaOnCapture = true,
                        Directory = "Gurru",
                        Name = string.Format("Gurru_{0}", DateTime.Now.ToString("yyMMddhhmmss")),
                        MaxPixelDimension = 1024,
                        PercentQuality = 85
                    };

                    mediaFile = await Media.MediaPicker.TakePhotoAsync(options);
                }
                else
                {
                    await DisplayAlert("Error", "Camera is not awailable", "Ok");
                }
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "An unexpected error has occured", "Ok");
            }
            return true;
        }

        async Task<bool> SelectFromGallery()
        {
            try
            {
                mediaFile = await Media.MediaPicker.SelectPhotoAsync(new CameraMediaStorageOptions
                {
                    DefaultCamera = CameraDevice.Front,
                    MaxPixelDimension = 400
                });
            }
            catch (TaskCanceledException)
            {
            }
            catch (System.Exception ex)
            {
                await DisplayAlert("Error", "An unexpected error has occured", "Ok");
            }
            return true;
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
            LoginViewModel.Instance.Logout();

            App.Current.MainPage = new NavigationPage(new LoginPage());

            await Navigation.PopToRootAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}
