using System;
using iApprove.CustomControl;
using iApprove.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace iApprove.iOS.Renderer
{
	public class CustomPickerRenderer : PickerRenderer
    {
		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Picker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var textField = (UITextField)Control;
				textField.TextColor = Color.FromHex("#3D846A").ToUIColor();
                textField.Layer.BorderColor = Color.FromHex("#1976D2").ToCGColor();
				textField.Layer.CornerRadius = 2.0f;
				textField.Layer.BorderWidth = 0.7f;
				textField.ClearButtonMode = UITextFieldViewMode.WhileEditing;

				/*UIImageView imageview = new UIImageView(new UIImage("icstatusfull.png"));
				imageview.Frame = new CoreGraphics.CGRect(textField.Frame.Width, 0, 30, 30);
				textField.AddSubview(imageview);*/
            }
        }
    }
}