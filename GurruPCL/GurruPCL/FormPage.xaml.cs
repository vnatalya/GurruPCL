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
            OrganizationName.Completed += NaligateNextField;

            ParentOrganization.DropdownTapped += DropdownTapped;
			ParentOrganization.ValueText = "Enter Parent Organisation";

            BusinessPhone.EntryKeyboard = Keyboard.Telephone;            
            BusinessPhone.ValueText = ViewModel.CurrentForm.BusinessPhone;
            BusinessPhone.Completed += NaligateNextField;

            BusinessType.ErrorText = "Business Type is required";
			BusinessType.ValueText = "Select Business Type...";
            BusinessType.DropdownTapped += DropdownTapped;

            Email.EntryKeyboard = Keyboard.Email;
            Email.ValueText = ViewModel.CurrentForm.Email;
            Email.Completed += NaligateNextField;

            //Contact
            FirstName.Mandatory = true;
            FirstName.ErrorText = "First Name cannot be empty";
            FirstName.Completed += NaligateNextField;

            LastName.Mandatory = true;
            LastName.ErrorText = "Last Name cannot be empty";
            LastName.Completed += NaligateNextField;

            ContactPhone.EntryKeyboard = Keyboard.Telephone;
            ContactPhone.Completed += NaligateNextField;

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

        private void NaligateNextField(object sender, EventArgs e)
        {
            if (sender == OrganizationName  )
                BusinessPhone.StartEditing();

            if (sender == BusinessPhone)
                Email.StartEditing();

            if (sender == Email)
                FirstName.StartEditing();

            if (sender == FirstName)
                LastName.StartEditing();

            if (sender == LastName)
                ContactPhone.StartEditing();

            if (sender == ContactPhone)
                DetailsOfOpportunity.Focus();
        }

        private void DropdownTapped(object sender, EventArgs e)
        {
            Scroll.IsEnabled = false;

            var picker = new Picker();

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
                sourceList.Sort();
				for (int i = 1; i < sourceList.Count; ++i)
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

                (sender as GurruDropdownField).ValueText = picker.ItemsSource[picker.SelectedIndex].ToString();

                if (sender.Equals(Source))
                {
                    (sender as GurruDropdownField).ValueText = (string)picker.SelectedItem;
                    var name = ((string)picker.SelectedItem).Replace(' ', '_');
                    ViewModel.CurrentForm.FormSource = (Form.Source) Enum.Parse(typeof(Form.Source), name); 
                }


                Scroll.IsEnabled = true;
            };

            picker.Unfocused += (s, ea) =>
            {
                Scroll.IsEnabled = true;
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
            LoginViewModel.Instance.Logout();

            App.Current.MainPage = new NavigationPage(new LoginPage());

            await Navigation.PopToRootAsync();
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

            var res = new BaseResult();

            var missingView = SetAndValidateCurrentFormFields();
            if (missingView != null)
            {
                await Scroll.ScrollToAsync(0, missingView.Y, true);
                return;
            }

			SetLoader(true);

            res = await ViewModel.SaveCurrentFormAsync(sender == QualifyButton);

			SetLoader(false);

			if (res.Status != System.Net.HttpStatusCode.OK)
				await DisplayAlert(res.Title, res.Message, "Ok");
			else
				await DisplayAlert("Infornmation", "Form was successfully saved", "Ok");
        }

        private View SetAndValidateCurrentFormFields()
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

            return OrganizationName.Missing ? OrganizationName : ViewModel.CurrentForm.BusinessType == null ? BusinessType : 
                FirstName.Missing ? FirstName : LastName.Missing ? (View)LastName : 
                ViewModel.SalesPersons == null ? SalesPerson : ViewModel.CurrentForm.FormSource == Form.Source.None ? Source: null;
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