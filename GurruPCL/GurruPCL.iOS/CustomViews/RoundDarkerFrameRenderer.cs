using Xamarin.Forms;
using GurruPCL.CustomViews;
using Xamarin.Forms.Platform.iOS;
using GurruPCL.Droid.CusomViews;
using GurruPCL.iOS;

[assembly: ExportRenderer(typeof(RoundDarkerFrame), typeof(RoundDarkerFrameRenderer))]

namespace GurruPCL.Droid.CusomViews
{
	public class RoundDarkerFrameRenderer : VisualElementRenderer<StackLayout>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement == null)
			{
				Layer.CornerRadius = 5;
				BackgroundColor = ColorHelper.UIColorFromHex(0xcdcdcd);
			}
		}
	}
}