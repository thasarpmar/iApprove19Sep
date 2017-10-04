using System;
using UIKit;
using Xamarin.Forms;
using iApprove.iOS.Renderer;
using Xamarin.Forms.Platform.iOS;
using CoreAnimation;
using CoreGraphics;
using iApprove.CustomControl;
using iApprove.iOS.DependencyService;
using System.Reflection;

[assembly: ExportRenderer(typeof(CustomNavigationPage), typeof(CustomNavigationPageRenderer))]
namespace iApprove.iOS.Renderer
{
    class CustomNavigationPageRenderer : NavigationRenderer
    {
        UIButton btnMenu;
        MenuView menu;

		public CustomNavigationPageRenderer()
		{
			this.NavigationBarHidden = true;
		}
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			try
			{
				//Naviation bar text style
				UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes() { TextColor = UIColor.White});
				//Navigation Bar background color
				UINavigationBar.Appearance.BarTintColor = Color.FromHex("#3D846A").ToUIColor();
				UINavigationBar.Appearance.TintColor = UIColor.White;
				UINavigationBar.Appearance.ShadowImage = new UIImage("menu.png");
				NavigationBar.BarStyle = UIBarStyle.Default;
				UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, true);
				UIApplication.SharedApplication.SetStatusBarHidden(false, false);
				NavigationItem.HidesBackButton = false;

				//Navigation bar Gradient Style
				#region NavigationBar Background
				/*var gradient = new CAGradientLayer();
				gradient.Frame = NavigationBar.Bounds;
				gradient.NeedsDisplayOnBoundsChange = true;
				gradient.MasksToBounds = true;
				gradient.Colors = new CGColor[] {  Color.FromHex("#E91E63").ToCGColor()}; //UIColor.FromRGB(248, 0, 0).CGColor, UIColor.FromRGB(143, 0, 0).CGColor };            
				UIGraphics.BeginImageContext(gradient.Bounds.Size);
				gradient.RenderInContext(UIGraphics.GetCurrentContext());
				UIImage backImage = UIGraphics.GetImageFromCurrentImageContext();
				UIGraphics.EndImageContext();                               
				UINavigationBar.Appearance.SetBackgroundImage(backImage, UIBarMetrics.Default);  */

				//var v = NavigationItem.BackBarButtonItem;
				//var bounds = NavigationBar.Bounds;


				#endregion

				#region Title and logo
				nfloat margin = 90;
				UIImageView imageview = new UIImageView(new UIImage("menu.png"));
				imageview.SizeToFit();

				nfloat y = 4; //(nfloat)((NavigationBar.Frame.Height - imageview.Frame.Height) / 2);
				nfloat x = NavigationBar.Bounds.Width - margin;
				imageview.Frame = new CoreGraphics.CGRect(x, y, 30, 30);

				UILabel menuName = new UILabel() { Text = "Menu", TextColor = UIColor.White };
				menuName.Font = UIFont.BoldSystemFontOfSize(8);

				//For to solve menu adjustment in iOS 6 plus.
				//DeviceIdentifier result = IdentifyDevice();
				//if (result == DeviceIdentifier.iPhone6Plus)
				// menuName.Frame = new CoreGraphics.CGRect(NavigationBar.Frame.Width - 55, (nfloat)(NavigationBar.Frame.Height - 20), 25, 20);
				//else 
				menuName.Frame = new CoreGraphics.CGRect(NavigationBar.Frame.Width - 36, (nfloat)(NavigationBar.Frame.Height - 20), 25, 20);
				#endregion

				//menu = new MenuView();          
				////Button for displaying Menu.
				//btnMenu = new UIButton();
				//btnMenu.Frame = new CGRect(NavigationBar.Frame.Width - 38, 0, 38, 42);
				////btnMenu.Layer.BorderColor = (UIColor.White).CGColor;
				////btnMenu.Layer.BorderWidth = 1;
				//var menuview = new UIImageView(new UIImage("Icon-Small.png"));
				//menuview.ClipsToBounds = true;
				//menuview.Frame = new CGRect(btnMenu.Frame.Width - 34, 7, 18, 20);
				//btnMenu.AddSubview(menuview);
				//btnMenu.TouchUpInside += TouchUpInside;

				#region VerticalBar
				UIView verticalBar = new UIView();
				verticalBar.BackgroundColor = UIColor.FromRGBA(255, 255, 255, 220);
				verticalBar.Frame = new CoreGraphics.CGRect(NavigationBar.Frame.Width - 50, 2, 0.5f, NavigationBar.Frame.Height - 2);
				#endregion


				#region addToNavigaitonBar
				//NavigationBar.AddSubview(imageview);               
				//NavigationBar.AddSubview(menuName);
				//NavigationBar.AddSubviews(verticalBar);
				//NavigationBar.AddSubviews(verticalBar,btnMenu);            
				#endregion
			}
			catch (Exception e)
			{
			}
        }

        private void TouchUpInside(object sender, EventArgs e)
        {
            
        }
    }
}