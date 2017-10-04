using System;
using System.Reflection;
using CoreGraphics;
using iApprove.iOS;
using UIKit;
using Xamarin.Forms.Platform.iOS;

[assembly: Xamarin.Forms.ExportRenderer(typeof(Xamarin.Forms.ContentPage), typeof(ContentPageRenderer))]
namespace iApprove.iOS
{
	public class ContentPageRenderer : PageRenderer
	{
		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			try
			{
				var navigationItem = this.NavigationController.TopViewController.NavigationItem;
				foreach (var rightItem in navigationItem.RightBarButtonItems)
				{
					var button = new UIButton(new CGRect(0, 0, 27, 50));
					button.SetImage(rightItem.Image, UIControlState.Normal);

					FieldInfo fi = rightItem.GetType().GetField("clicked", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
					Delegate del = (Delegate)fi.GetValue(rightItem);
					button.TouchUpInside += (EventHandler)del;

					rightItem.CustomView = button;
				}
			}
			catch (Exception exp)
			{
			}
		}
	}
}
