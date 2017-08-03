using GurruPCL.CustomViews;
using GurruPCL.Models;
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
    public partial class FormPage : ContentPage
    {
        static FormViewModel ViewModel { get { return FormViewModel.Instance; } }

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
            OrganizationName.ValueText = ViewModel.CurrentForm.OrganizationName;

            ParentOrganization.DropdownTapped += DropdownTapped;

            BusinessPhone.EntryKeyboard = Keyboard.Telephone;            
            BusinessType.ErrorText = "Business Type is required";
            BusinessPhone.ValueText = ViewModel.CurrentForm.BusinessPhone;

            BusinessType.DropdownTapped += DropdownTapped;

            Email.EntryKeyboard = Keyboard.Email;
            Email.ValueText = ViewModel.CurrentForm.Email;

            //Contact
            FirstName.Mandatory = true;
            FirstName.ErrorText = "First Name cannot be empty";

            LastName.Mandatory = true;
            LastName.ErrorText = "Last Name cannot be empty";

            ContactPhone.EntryKeyboard = Keyboard.Telephone;

            //Details
            SalesPerson.ErrorText = "Sales Person is required";
            SalesActivity.DropdownTapped += DropdownTapped;

            SalesActivity.ErrorText = "Sales Person is required";
            SalesPerson.DropdownTapped += DropdownTapped;

            Source.ErrorText = "Sales Person is required";
            Source.DropdownTapped += DropdownTapped;
        }

        private async void DropdownTapped(object sender, EventArgs e)
        {
            var picker = new Picker();
            var f = sender as GurruDropdownField;
            if (sender.Equals(ParentOrganization))
                picker.ItemsSource = ViewModel.Organizations.Select(o => o.Name).ToList();

            if (sender.Equals(BusinessType))
                picker.ItemsSource = ViewModel.BusinessTypes.Select(o => o.Name).ToList(); 

            if (sender.Equals(SalesActivity))
                picker.ItemsSource = ViewModel.SalesActivities.Select(o => o.Name).ToList();

            if (sender.Equals(SalesPerson))
            {
                var personsList = new List<string>();
                for (int i = 0; i < ViewModel.SalesPersons.Count; ++i)
                    personsList.Add(string.Format("{0} {1}\n{2}", ViewModel.SalesPersons[i].FirstName, ViewModel.SalesPersons[i].LastName, ViewModel.SalesPersons[i].Email));
                picker.ItemsSource = personsList;
            }

            if (sender.Equals(Source))
                return;//  picker.ItemsSource = ViewModel.Organizations.Select(o => o.Name).ToList();

            picker.SelectedIndexChanged += (s, ea) =>
            {
                var sth = picker.SelectedItem;
                ((sender as GurruDropdownField).Parent as StackLayout).Children.Remove(picker);

                if (sender.Equals(ParentOrganization))
                    ViewModel.CurrentForm.ParentOrganization = ViewModel.Organizations.FirstOrDefault(x => x.Name.Equals(picker.ItemsSource[picker.SelectedIndex]));

                if (sender.Equals(BusinessType))
                    ViewModel.CurrentForm.BusinessType = ViewModel.BusinessTypes.FirstOrDefault(x => x.Name.Equals(picker.ItemsSource[picker.SelectedIndex]));

                if (sender.Equals(SalesActivity))
                    ViewModel.CurrentForm.SalesActivity = ViewModel.SalesActivities.FirstOrDefault(x => x.Name.Equals(picker.ItemsSource[picker.SelectedIndex]));

                if (sender.Equals(SalesPerson))
                {
                    var text = picker.ItemsSource[picker.SelectedIndex].ToString();
                    var mail = text.Remove(0, text.IndexOf("\n") != -1 ? text.IndexOf("\n") : 0);
                    ViewModel.CurrentForm.SalesPerson = ViewModel.SalesPersons.FirstOrDefault(x => x.Email.Equals(mail));
                }

                var txt = picker.ItemsSource[picker.SelectedIndex].ToString();
                Device.BeginInvokeOnMainThread(() => {
                    f.ValueText = txt;
                });
            };

            picker.Unfocused += (s, ea) =>
            {
                ((sender as GurruDropdownField).Parent as StackLayout).Children.Remove(picker);
            };

            ((sender as GurruDropdownField).Parent as StackLayout).Children.Add(picker);
            picker.Focus();

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
            var res = new BaseResult();

            if (sender == CreateButton && SetAndValidateCurrentFormFields())
                res = await ViewModel.SaveCurrentFormAsync();

            if (sender == QualifyButton && SetAndValidateCurrentFormFields())
                res = await ViewModel.SaveCurrentFormAsync();

            if (sender == CancelButton)
                await Navigation.PopAsync();

            if (res.Status != System.Net.HttpStatusCode.OK)
                await DisplayAlert(res.Title, res.Message, "Ok");
        }

        private bool SetAndValidateCurrentFormFields()
        {
            //init
            ViewModel.CurrentForm.OrganizationName = OrganizationName.ValueText;
            ViewModel.CurrentForm.BusinessPhone = BusinessPhone.ValueText;
            ViewModel.CurrentForm.Email = Email.ValueText;

            ViewModel.CurrentForm.Contact = new Contact()
            {
                LastName = LastName.ValueText,
                FirstName = FirstName.ValueText,
                Phone = ContactPhone.ValueText,
            };

            ViewModel.CurrentForm.DetailOfOpportunity = DetailsOfOpportunity.Text;

            //validate
            OrganizationName.Missing = string.IsNullOrEmpty(OrganizationName.ValueText);
            FirstName.Missing = string.IsNullOrEmpty(FirstName.ValueText);
            LastName.Missing = string.IsNullOrEmpty(LastName.ValueText);

            return (!OrganizationName.Missing || FirstName.Missing || LastName.Missing || 
                ViewModel.CurrentForm.SalesPerson == null || ViewModel.CurrentForm.SalesActivity == null || ViewModel.CurrentForm.BusinessType == null);
        }
    }
}