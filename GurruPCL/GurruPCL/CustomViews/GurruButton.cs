using Xamarin.Forms;

namespace GurruPCL.CustomViews
{
    public class GurruButton : Button
    {
        public GurruButton() :base()
        {
            TextColor = Color.White;
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
        }
    }
}
