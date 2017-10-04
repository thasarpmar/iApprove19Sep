using System;
using CoreAnimation;
using iApprove.CustomControl;
using iApprove.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace iApprove.iOS.Renderer
{
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {              
				var textField = (UITextField)Control;
                //textField.TextColor = Color.FromHex("#3D846A").ToUIColor();
                //textField.Layer.BorderColor = Color.FromHex("#1976D2").ToCGColor();
                //textField.Layer.CornerRadius = 2.0f;
                //textField.Layer.BorderWidth = 0.7f;
                //textField.ClearButtonMode = UITextFieldViewMode.WhileEditing;
                //textField.Layer.BorderColor = Color.White.ToCGColor();
                //textField.Layer.BorderWidth = 0.0f;

                //textField.LeftView = new UIImageView(UIImage.FromFile("password_icon.png"));
                //textField.LeftViewMode = UITextFieldViewMode.Always;


				//var borderLayer = new CALayer();
				//borderLayer.MasksToBounds = true;
				//borderLayer.Frame = new CoreGraphics.CGRect(0f, Frame.Height / 2, Frame.Width, 1f);
                //borderLayer.BorderColor = UIColor.LightGray.CGColor;
				//borderLayer.BorderWidth = 1.0f;
				//textField.Layer.AddSublayer(borderLayer);
                textField.BorderStyle = UITextBorderStyle.None;
			}
        }
    }
}