using GurruPCL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GurruPCL
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        static int MaxChar = 50;
        static LoginViewModel ViewModel { get { return LoginViewModel.Instance; } }

        public LoginPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            Container.WidthRequest = App.DeviceWidth;
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue) && e.NewTextValue.Length > MaxChar)
                (sender as Entry).Text = e.OldTextValue ?? string.Empty;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            ViewModel.RememberMe = !ViewModel.RememberMe;
            RememberImage.Source = ViewModel.RememberMe ? "checked.png" : "unchecked.png";
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
			PasswordErrorFiled.IsVisible = string.IsNullOrEmpty(PasswordEntry.Text);
			UsernameErrorField.IsVisible = string.IsNullOrEmpty(UsernameEntry.Text);

			if (string.IsNullOrEmpty(UsernameEntry.Text) || string.IsNullOrEmpty(PasswordEntry.Text))
				return;

            SetLoader(true);

            var res = await ViewModel.LoginAsync(UsernameEntry.Text, PasswordEntry.Text);

            if (res.Status != System.Net.HttpStatusCode.OK)
            {
                await DisplayAlert(res.Title, res.Message, "Ok");
                SetLoader(false);
            }
            else
            {
                await Navigation.PushAsync(new MainPage());
                Navigation.RemovePage(this);
            }
        }

        void SetLoader(bool show)
        {
            Loader.IsRunning = show;
            Loader.IsVisible = show;
            LoginButton.IsEnabled = !show;
        }
    }
}