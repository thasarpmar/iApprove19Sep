using iApprove.CustomControl;
using Xamarin.Forms;
using iApprove.Droid.Renderer;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace iApprove.Droid.Renderer
{
	public class CustomPickerRenderer: PickerRenderer
    {
		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Picker> e)
        {
            base.OnElementChanged(e);

            if(Control != null)
            {
				Control.SetBackgroundResource(Resource.Drawable.edittext_normal);
				Control.SetTextColor(Color.FromHex("#3D846A").ToAndroid());
            }
        }
    }
}