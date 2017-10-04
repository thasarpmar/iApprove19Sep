using iApprove.CustomControl;
using iApprove.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]
namespace iApprove.iOS.Renderer
{
    public class CustomEditorRenderer : EditorRenderer
    {
		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
				var textField = (UITextView)Control;
				textField.TextColor = Color.FromHex("#3D846A").ToUIColor();
                textField.Layer.BorderColor = Color.FromHex("#1976D2").ToCGColor();
				textField.Layer.CornerRadius = 2.0f;
				textField.Layer.BorderWidth = 0.7f;
            }
        }
    }
}