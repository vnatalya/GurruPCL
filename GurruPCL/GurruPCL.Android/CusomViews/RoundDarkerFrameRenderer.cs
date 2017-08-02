using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using GurruPCL.CustomViews;
using GurruPCL.Droid.CusomViews;

[assembly: ExportRenderer(typeof(RoundDarkerFrame), typeof(RoundDarkerFrameRenderer))]

namespace GurruPCL.Droid.CusomViews
{
    public class RoundDarkerFrameRenderer : VisualElementRenderer<StackLayout>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
        {
            base.OnElementChanged(e);

            SetBackgroundResource(Resource.Drawable.RoundFrameDarkerDrawable);
        }
    }
}