using Android.Text;
using Xamarin.Forms.Platform.Android;

namespace GurruPCL.Droid.CusomViews
{
	public class GurruEntryRenderer : EntryRenderer
    {
		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null && Control != null)
            {
                Control.SetBackgroundDrawable(null);
                Control.InputType = InputTypes.TextFlagNoSuggestions | InputTypes.TextVariationVisiblePassword;
                Control.SetLayerType(Android.Views.LayerType.None, new Android.Graphics.Paint());
                Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
                Control.Background = null;
			}
        }
    }
}