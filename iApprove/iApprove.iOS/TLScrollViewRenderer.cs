using System;
using iApprove;
using ProjectTemplate.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(XScrollView), typeof(TLScrollViewRenderer))]

namespace ProjectTemplate.iOS
{
	public class TLScrollViewRenderer : ScrollViewRenderer
	{
		public TLScrollViewRenderer()
		{
		}
		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);

			var element = e.NewElement as XScrollView;
			element?.Render();
		}

	}
}
