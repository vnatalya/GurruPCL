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
    public partial class PickerWithSearch : ContentPage
    {
        public PickerWithSearch()
        {
            InitializeComponent();
        }

        private void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}