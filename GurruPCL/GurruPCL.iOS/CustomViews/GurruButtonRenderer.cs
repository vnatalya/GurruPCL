using System;
using System.Linq;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using GurruPCL.CustomViews;
using GurruPCL.iOS;
using GurruPCL.iOS.CustomViews;
using UIKit;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(GurruButton), typeof(GurruButtonRenderer))]
namespace GurruPCL.iOS.CustomViews
{
	public class GurruButtonRenderer : Xamarin.Forms.Platform.iOS.ButtonRenderer
	{
		public override CGRect Frame
		{
			get
			{
				return base.Frame;
			}
			set
			{
				if (value.Width > 0 && value.Height > 0)
				{
					foreach (var layer in Control?.Layer.Sublayers.Where(layer => layer is CAGradientLayer))
						layer.Frame = new CGRect(0, 0, value.Width, value.Height);
				}
				base.Frame = value;
			}
		}

		protected override void OnElementChanged(Xamarin.Forms.Platform.iOS.ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged(e);

			this.Layer.CornerRadius = 5;

			CAGradientLayer highlightedLayer;
			CAGradientLayer normalLayer;

			normalLayer = new CAGradientLayer();
			normalLayer.Frame = this.Layer.Bounds;

			normalLayer.Colors = new CGColor[]
			{
				ColorHelper.CGColorFromHex(0x22b1d5),
				ColorHelper.CGColorFromHex(0x006278)
			};

			normalLayer.Locations = new NSNumber[] { 0.0f, 1.0f };

			normalLayer.CornerRadius = this.Layer.CornerRadius;

			highlightedLayer = new CAGradientLayer();
			highlightedLayer.Frame = this.Layer.Bounds;

			highlightedLayer.Colors = new CGColor[]
			{
				ColorHelper.CGColorFromHex(0x22b1d5),
				ColorHelper.CGColorFromHex(0x058caf)
			};

			highlightedLayer.Locations = new NSNumber[] { 0.0f, 1.0f };

			highlightedLayer.CornerRadius = this.Layer.CornerRadius;

			if (e.OldElement == null)
			{
				var layer = Control?.Layer.Sublayers.LastOrDefault();
				if (Control != null)
				{
					Control.Layer.InsertSublayerBelow(normalLayer, layer);
					Control.TouchDown += (sender, ev) => { 
						Control?.Layer.InsertSublayerBelow(highlightedLayer, layer); 
					};
					Control.TouchUpInside += (sender, ev) => { 
						Control?.Layer.InsertSublayerBelow(highlightedLayer, layer); };
					Control.TouchUpOutside += (sender, ev) => { 
						Control?.Layer.InsertSublayerBelow(normalLayer, layer); };

					Control.AllEvents += (sender, ev) => {
						if(Control != null)
							Control?.Layer.InsertSublayerBelow(highlightedLayer, layer);
						else
							Control?.Layer.InsertSublayerBelow(normalLayer, layer);
					};
				}
			}
		}
	}
}
