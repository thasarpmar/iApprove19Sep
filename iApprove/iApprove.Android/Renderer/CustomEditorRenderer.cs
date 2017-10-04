using iApprove.CustomControl;
using Xamarin.Forms;
using iApprove.Droid.Renderer;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]
namespace iApprove.Droid.Renderer
{
	public class CustomEditorRenderer: EditorRenderer
    {
		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Editor> e)
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