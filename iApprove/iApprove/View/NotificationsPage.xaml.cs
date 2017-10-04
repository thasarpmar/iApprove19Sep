using System;
using System.Collections.Generic;
using iApprove.CustomControl;
using iApprove.ViewModel;
using Xamarin.Forms;

namespace iApprove.View
{
    public partial class NotificationsPage : BaseContentPage
    {
		private NotificationsViewModel vm;
		private object navParam;
        public NotificationsPage()
        {
            
            InitPage();
        }

		public NotificationsPage(object args)
		{
			
            navParam = args;
			InitPage();
		}


		private void InitPage()
		{
            iApproveNavBar.MyNavigationBar.InitHeader("Notifications", false);
			InitializeComponent();

			vm = new NotificationsViewModel(App.NavigationServiceInstance);

			this.BindingContext = vm;
		}
    }
}
