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
    public partial class GurruEditField : ContentView
    {
        static int MaxChar = 50;

        public EventHandler Completed;

        public string ValueText
        {
            get { return EditText.Text; }
            set { EditText.Text = value; }
        }
        
        public string ErrorText { set { ErrorLabel.Text = value; } }
        
        public bool Mandatory { get; set; }

        private bool missing;
        public bool Missing
        {
            get { return missing; }
            set
            {
                if (!Mandatory)
                    return;

                missing = value;
                Border.BackgroundColor = missing ? Color.FromHex("#ef3f3f") : Color.FromHex("#cdcdcd");
                ErrorFiled.IsVisible = missing;
            }
        }

        public Keyboard EntryKeyboard { set { EditText.Keyboard = value; } }

        public GurruEditField()
        {
            InitializeComponent();
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Missing = string.IsNullOrEmpty(e.NewTextValue);

            if (!string.IsNullOrEmpty(e.NewTextValue) && e.NewTextValue.Length > MaxChar)
                (sender as Entry).Text = e.OldTextValue ?? string.Empty;
        }

        private void EditText_Completed(object sender, EventArgs e)
        {
            if (Completed != null)
                Completed.Invoke(this, new EventArgs());
        }

        public void StartEditing()
        {
            EditText.Focus();
        }
    }
}