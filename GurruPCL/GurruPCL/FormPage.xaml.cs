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
    public partial class FormPage : ContentPage
    {
        public FormPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            Container.WidthRequest = App.DeviceWidth;

            Initialize();
        }


        private void Initialize()
        {
            //Organization
            OrganizationName.Mandatory = true;
            OrganizationName.ErrorText = "Organization Name cannot be empty";

            ParentOrganization.DropdownTapped += DropdownTapped;

            BusinessPhone.EntryKeyboard = Keyboard.Telephone;
            
            BusinessType.ErrorText = "Business Type is required";

            Email.EntryKeyboard = Keyboard.Email;

            //Contact
            FirstName.Mandatory = true;
            FirstName.ErrorText = "First Name cannot be empty";

            LastName.Mandatory = true;
            LastName.ErrorText = "Last Name cannot be empty";

            ContactPhone.EntryKeyboard = Keyboard.Telephone;

            //Details
            SalesPerson.ErrorText = "Sales Person is required";

            SalesActivity.ErrorText = "Sales Person is required";

            Source.ErrorText = "Sales Person is required";
        }

        private async void DropdownTapped(object sender, EventArgs e)
        {
            var picker = new Picker();

            
        }

        private async void LogoutButton_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
            await Navigation.PushAsync(new LoginPage());
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void DetailsOfOpportunity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue) && e.NewTextValue.Length > 200)
                (sender as Editor).Text = e.OldTextValue ?? string.Empty;
        }

        private async void FooterButton_Clicked(object sender, EventArgs e)
        {
            if (sender == CreateButton)
                return;

            if (sender == QualifyButton)
                return;

            if (sender == CancelButton)
                await Navigation.PopAsync();
        }
    }
}