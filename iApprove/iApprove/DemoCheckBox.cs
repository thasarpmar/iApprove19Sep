using System;
using Xamarin.Forms;

namespace iApprove
{
    public class DemoCheckBox :BoxView
    {
        public DemoCheckBox()
        {
        }
		public static readonly BindableProperty ItemTemplateProperty =
			BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(DemoCheckBox), default(DataTemplate));

		public DataTemplate ItemTemplate
		{
			get { return (DataTemplate)GetValue(ItemTemplateProperty); }
			set { SetValue(ItemTemplateProperty, value); }
		}
    }
}
