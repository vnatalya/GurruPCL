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

        public string ValueText { set { ErrorLabel.Text = value; } }

        public string ErrorText { set { ErrorLabel.Text = value; } }

        public GurruDropdownField()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (DropdownTapped != null)
                DropdownTapped.Invoke(this, new EventArgs());
        }
    }
}