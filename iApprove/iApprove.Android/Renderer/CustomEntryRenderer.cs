using iApprove.CustomControl;
using Xamarin.Forms;
using iApprove.Droid.Renderer;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace iApprove.Droid.Renderer
{
    public class CustomEntryRenderer: EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
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