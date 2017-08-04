using Xamarin.Forms;

namespace GurruPCL.CustomViews
{
    public class AutoresizeEditor : Editor
    {
        public AutoresizeEditor()
        {
            this.TextChanged += AutoresizeEditor_TextChanged;
        }

        private void AutoresizeEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.InvalidateMeasure();
        }
    }
}
