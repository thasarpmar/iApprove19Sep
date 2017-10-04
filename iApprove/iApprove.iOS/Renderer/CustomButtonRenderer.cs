using iApprove.CustomControl;
using iApprove.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomButtonRenderer))]
namespace iApprove.iOS.Renderer
{
    public class CustomButtonRenderer: ButtonRenderer
    {
		protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				var btn = (UIButton)Control;
				Control.BackgroundColor = UIColor.Blue;
				Control.Layer.CornerRadius = 0.0f;
				Control.TitleLabel.TextColor = Color.White.ToUIColor();
				Control.TitleLabel.AdjustsFontSizeToFitWidth = true;
				Control.SetTitleColor(Color.FromHex("#90CAF9").ToUIColor(), UIControlState.Disabled);
				Control.SetTitleColor(Color.White.ToUIColor(), UIControlState.Normal);

				int colorIndex = 0;
				if (e.NewElement != null)
				{
					colorIndex = (int)((CustomButton)e.NewElement).ButtonType;
				}
				else if (e.OldElement != null) 
				{
					colorIndex = (int)((CustomButton)e.OldElement).ButtonType;
				}
				//404644
				//Color.FromHex("#B71C1C")
				Color[] btnColors = new Color[] {Color.FromHex("#2E2E2E"), Color.Gray,Color.Lime,Color.Orange,Color.Blue};
				Control.BackgroundColor = btnColors[colorIndex].ToUIColor();
			}
		}
    }
}