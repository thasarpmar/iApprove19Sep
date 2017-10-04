using System;
using Xamarin.Forms;

namespace iApprove
{
    public class RoundedBox: BoxView
    {
        public RoundedBox()
        {
        }
		public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create("CornerRadius", typeof(int), typeof(RoundedBox), null);

		public int CornerRadius
		{
			get { return (int)GetValue(CornerRadiusProperty); }
			set { SetValue(CornerRadiusProperty, value); }
		}
    }
}
