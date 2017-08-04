using Xamarin.Forms.Platform.iOS;
using GurruPCL.iOS.CustomViews;
using Xamarin.Forms;
using GurruPCL.CustomViews;

[assembly: ExportRenderer(typeof(GurruEntry), typeof(GurruEntryRenderer))]
namespace GurruPCL.iOS.CustomViews 
{
    public class GurruEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.BorderStyle = UIKit.UITextBorderStyle.None;
            }
        }
    }
}