using System;
using System.ComponentModel;
using iApprove;
using iApprove.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RoundedBox), typeof(RoundedBoxRenderer))]
namespace iApprove.iOS
{
    public class RoundedBoxRenderer: BoxRenderer
    {
        public RoundedBoxRenderer()
        {
        }
		protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
		{
			base.OnElementChanged(e);
			if (Element != null)
			{
				Layer.MasksToBounds = true;
				UpdateCornerRadius(Element as RoundedBox);
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			if (e.PropertyName == RoundedBox.CornerRadiusProperty.PropertyName)
			{
				UpdateCornerRadius(Element as RoundedBox);
			}
		}

		void UpdateCornerRadius(RoundedBox box)
		{
			Layer.CornerRadius = 7;
            box.WidthRequest = 14;
            box.HeightRequest = 14;
		}
    }
}
