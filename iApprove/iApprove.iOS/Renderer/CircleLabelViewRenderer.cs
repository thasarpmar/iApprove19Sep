using System;
using iApprove.CustomControl;
using ProjectTemplate.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CircleLabelView), typeof(CircleLabelViewRenderer))]
namespace ProjectTemplate.iOS.Renderer
{
    public class CircleLabelViewRenderer:LabelRenderer
    {
        public CircleLabelViewRenderer()
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged(e);
			if (e.OldElement != null || Element == null)
			{ return; }
			CreateCircle();
		}
		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			if (e.PropertyName == VisualElement.HeightProperty.PropertyName ||
			  e.PropertyName == VisualElement.WidthProperty.PropertyName)
			{
				CreateCircle();
			}
		}
		private void CreateCircle()
		{
			try
			{
				double min = Math.Min(Element.Width, Element.Height);
				Control.Layer.CornerRadius = (float)(min / 2.0);
				Control.Layer.MasksToBounds = false;
                Control.Layer.BorderColor = Color.White.ToCGColor();
				Control.Layer.BorderWidth = 1;
				Control.ClipsToBounds = true;
				Control.ContentMode = UIViewContentMode.Center;
		}
			catch (Exception ex)
			{
			}
		}
    }
}
