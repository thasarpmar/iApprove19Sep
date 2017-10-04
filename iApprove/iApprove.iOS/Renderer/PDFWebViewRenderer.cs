using System.IO;
using System.Net;
using Foundation;
using iApprove.CustomControl;
using iApprove.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;


[assembly: ExportRenderer(typeof(PDFWebView), typeof(PDFWebViewRenderer))]
namespace iApprove.iOS
{
public class PDFWebViewRenderer : ViewRenderer<PDFWebView, UIWebView>
{
	protected override void OnElementChanged(ElementChangedEventArgs<PDFWebView> e)
	{
		base.OnElementChanged(e);

		if (Control == null)
		{
			SetNativeControl(new UIWebView());
		}
		if (e.OldElement != null)
		{
			// Cleanup
		}
		if (e.NewElement != null)
		{
			var customWebView = Element as PDFWebView;
			UpdateContent();
			Control.ScalesPageToFit = true;
		}
	}

	protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		base.OnElementPropertyChanged(sender, e);

		if (e.PropertyName == PDFWebView.PathProperty.PropertyName)
		{
			UpdateContent();
		}
	}

	private void UpdateContent()
	{
		var customWebView = Element as PDFWebView;

		if (customWebView != null && customWebView.Path != null)
		{
			Control.LoadRequest(new NSUrlRequest(new NSUrl(customWebView.Path, false)));
            }
        }
    }
}