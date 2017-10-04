using System;
using Xamarin.Forms;

namespace iApprove.CustomControl
{
	public class BaseContentPage : ContentPage
	{
		private Page page;
		bool IsMenuSet = false;
		public BaseContentPage()
		{
			BackgroundColor = Color.White;
            NavigationPage.SetHasNavigationBar(this, false);
            this.ControlTemplate = new ControlTemplate(typeof(iApproveNavBar));
		}
		public void SetChildPage(Page childPage)
		{
			this.page = childPage;
		}
		private object sourceData = "";
		public void SetSourceData<T>(T viewModel) where T : new()
		{
			this.sourceData = (T)(object)viewModel;
		}
		public T GetSourceData<T>() where T : new()
		{
			return (T)(object)this.sourceData;
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (Device.OS == TargetPlatform.Android)
			{
				/*Device.BeginInvokeOnMainThread(() =>
				{
					this.Animate("anim", (s) => Layout(new Rectangle(((1 - s) * Width),
														  AnchorY, Width, Height)),
								 16, 300, Easing.Linear, null, null);
				});
				      */
			}
		}
		private Enum.PageName currentPageName;
		public Enum.PageName CurrentPageName
		{
			get { return currentPageName; }
			set { currentPageName = value; }
		}
		//protected override void OnChildAdded(Element child)
		//{
		//	base.OnChildAdded(child);
		//	/*if (Device.OS == TargetPlatform.Android)
		//	{
		//		if (!IsMenuSet)
		//		{
		//			Xamarin.Forms.View view = Content;
		//			Content = new Grid
		//			{
		//				Children = {
		//				new Grid{
		//					Children={view}
		//				},
		//				new Grid{
		//					Children= {new MenuOptionView()}
		//				}

		//				 }
		//			};
		//			IsMenuSet = true;
		//		}
		//	}*/

		//}
	}
}