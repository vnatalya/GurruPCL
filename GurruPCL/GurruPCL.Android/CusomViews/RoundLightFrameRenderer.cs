using Xamarin.Forms;
using GurruPCL.CustomViews;
using Xamarin.Forms.Platform.Android;
using GurruPCL.Droid.CustomViews;

[assembly: ExportRenderer(typeof(RoundLightFrame), typeof(RoundLightFrameRenderer))]

namespace GurruPCL.Droid.CustomViews
{
    public class RoundLightFrameRenderer : VisualElementRenderer<StackLayout>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
        {
            base.OnElementChanged(e);

            SetBackgroundResource(Resource.Drawable.RoundFrameLightDrawable);
            Background.SetAlpha(200);
        }
    }
}