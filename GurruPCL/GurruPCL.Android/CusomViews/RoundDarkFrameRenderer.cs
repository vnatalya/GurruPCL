using Xamarin.Forms;
using GurruPCL.CustomViews;
using Xamarin.Forms.Platform.Android;
using GurruPCL.Droid.CusomViews;

[assembly: ExportRenderer(typeof(RoundDarkFrame), typeof(RoundDarkFrameRenderer))]

namespace GurruPCL.Droid.CusomViews
{
    public class RoundDarkFrameRenderer : VisualElementRenderer<StackLayout>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
        {
            base.OnElementChanged(e);

            SetBackgroundResource(Resource.Drawable.RoundDarkFrameDrawable);
        }
    }
}