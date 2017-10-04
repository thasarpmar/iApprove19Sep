using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;
using System.Reflection;
using System;

namespace iApprove.CustomControl
{
	public class CustomNavigationPage : NavigationPage
	{
		public CustomNavigationPage(Page content, bool isMenuVisible=true)
			: base(content)
		{
			
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
		}
		protected override void OnChildAdded(Element child)
		{
			base.OnChildAdded(child);
		}
		private void MenuAction()
		{
			
		}
	}
}