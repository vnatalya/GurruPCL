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
            OrganizationName.ErrorText = "Organisation Name cannot be empty";
            OrganizationName.ValueText = ViewModel.CurrentForm.OrganizationName;

            ParentOrganization.DropdownTapped += DropdownTapped;
			ParentOrganization.ValueText = "Enter Parent Organisation";

            BusinessPhone.EntryKeyboard = Keyboard.Telephone;            
            BusinessPhone.ValueText = ViewModel.CurrentForm.BusinessPhone;

			BusinessType.ErrorText = "Business Type is required";
			BusinessType.ValueText = "Select Business Type...";
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
			SalesPerson.ValueText = "Select Person Person...";
			SalesPerson.DropdownTapped += DropdownTapped;

			SalesActivity.ErrorText = "Sales Acrivity is required";
			SalesActivity.ValueText = ViewModel.CurrentForm.SalesActivity?.Name;
			SalesActivity.DropdownTapped += DropdownTapped;

			Source.ErrorText = "Sourceis required";
			Source.ValueText = "Select Source...";
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
			{ 
				var sourceList = Enum.GetNames(typeof(Form.Source)).ToList();
				for (int i = 0; i < sourceList.Count; ++i)
					sourceList[i] = sourceList[i].Replace('_', ' ');
				picker.ItemsSource = sourceList;
			}

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
					var index = text.IndexOf("\n");
					var mail = text.Remove(0, index != -1 && text.Length > index + 1 ? index + 1 : 0);
                    ViewModel.CurrentForm.SalesPerson = ViewModel.SalesPersons.FirstOrDefault(x => x.Email.Equals(mail));
                }

				if (sender.Equals(Source))
					ViewModel.CurrentForm.FormSource = (Form.Source)picker.SelectedIndex + 1;

                f.ValueText = picker.ItemsSource[picker.SelectedIndex].ToString();
            };

            picker.Unfocused += (s, ea) =>
            {
                ((sender as GurruDropdownField).Parent as StackLayout).Children.Remove(picker);
            };

            ((sender as GurruDropdownField).Parent as StackLayout).Children.Add(picker);
			Device.BeginInvokeOnMainThread(() =>
			{
				picker.Focus();
			});

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
			if (sender == CancelButton)
			{
				await Navigation.PopAsync();
				return;
			}

			SetLoader(true);

            var res = new BaseResult();

			if (sender == CreateButton && SetAndValidateCurrentFormFields())
				res = await ViewModel.SaveCurrentFormAsync(false);

            if (sender == QualifyButton && SetAndValidateCurrentFormFields())
                res = await ViewModel.SaveCurrentFormAsync(true);

			SetLoader(false);

			if (res.Status != System.Net.HttpStatusCode.OK)
				await DisplayAlert(res.Title, res.Message, "Ok");
			else
				await DisplayAlert("Infornmation", "Form was successfully saved", "Ok");
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

			SalesPerson.Missing = ViewModel.CurrentForm.SalesPerson == null;
			BusinessType.Missing = ViewModel.CurrentForm.BusinessType == null;

            return (!OrganizationName.Missing || FirstName.Missing || LastName.Missing || 
                ViewModel.CurrentForm.SalesPerson == null || ViewModel.CurrentForm.SalesActivity == null || ViewModel.CurrentForm.BusinessType == null);
        }

		void SetLoader(bool show)
		{
			CancelButton.IsEnabled = !show;
			CreateButton.IsEnabled = !show;
			QualifyButton.IsEnabled = !show;

			Loader.IsVisible = show;
			Loader.IsRunning = show;
		}
    }
}