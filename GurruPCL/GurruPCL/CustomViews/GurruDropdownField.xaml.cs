using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GurruPCL.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GurruDropdownField : ContentView
    {
        public EventHandler DropdownTapped;

		public string ValueText { set { ValueLabel.Text = value.Replace('_', ' '); } }

        public string ErrorText { set { ErrorLabel.Text = value; } }

		public bool Missing 
		{ 
			set 
			{
				Border.BackgroundColor = value ? Color.FromHex("#ef3f3f") : Color.FromHex("#cdcdcd");
				ErrorFiled.IsVisible = value; 
			} 
		}

        public GurruDropdownField()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
			Missing = false;

            if (DropdownTapped != null)
                DropdownTapped.Invoke(this, new EventArgs());
        }
    }
}