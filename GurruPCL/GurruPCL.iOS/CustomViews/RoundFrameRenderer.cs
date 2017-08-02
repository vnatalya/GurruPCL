using System;
using GurruPCL.CustomViews;
using GurruPCL.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RoundLightFrame), typeof(RoundLightFrameRenderer))]
namespace GurruPCL.iOS
{
	public class RoundLightFrameRenderer : VisualElementRenderer<StackLayout>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement == null)
			{
				Layer.CornerRadius = 5;
				BackgroundColor = ColorHelper.UIColorFromHex(0xffffff, 10);
			}
		}
	}
}
