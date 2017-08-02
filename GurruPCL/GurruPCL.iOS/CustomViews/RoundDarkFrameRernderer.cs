using System;
using GurruPCL.CustomViews;
using GurruPCL.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RoundDarkFrame), typeof(RoundDarkFrameRernderer))]
namespace GurruPCL.iOS
{
	public class RoundDarkFrameRernderer : VisualElementRenderer<StackLayout>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement == null)
			{
				Layer.CornerRadius = 5;
				BackgroundColor = ColorHelper.UIColorFromHex(0xcdcdcd, 100);
			}
		}
	}
}