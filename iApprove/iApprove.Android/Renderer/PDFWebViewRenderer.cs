using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using iApprove.CustomControl;
using System.ComponentModel;
using System.Net;
using iApprove.Droid.Renderer;

[assembly: ExportRenderer(typeof(PDFWebView), typeof(PDFWebViewRenderer))]

namespace iApprove.Droid.Renderer
{
	public class PDFWebViewRenderer : WebViewRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				Control.Settings.AllowUniversalAccessFromFileURLs = true;
				UpdateContent();
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
				Control.LoadUrl(string.Format("file:///android_asset/pdfjs/web/viewer.html?file={0}", string.Format("file:///{0}", WebUtility.UrlEncode(customWebView.Path))));
			}
		}
	}
}