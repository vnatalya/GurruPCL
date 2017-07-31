using Xamarin.Forms;
using GurruPCL.CustomViews;
using Xamarin.Forms.Platform.Android;
using GurruPCL.Droid.CusomViews;

[assembly: ExportRenderer(typeof(GurruButton), typeof(GurruButtonRenderer))]
namespace GurruPCL.Droid.CusomViews
{
    public class GurruButtonRenderer : Xamarin.Forms.Platform.Android.AppCompat.ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.SetBackgroundResource(Resource.Drawable.GurruButtonSelector);
            }
        }
    }
}