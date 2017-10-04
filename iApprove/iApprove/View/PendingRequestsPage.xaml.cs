using System;
using System.Collections.Generic;
using System.Windows.Input;
using iApprove.CustomControl;
using iApprove.ViewModel;
using Xamarin.Forms;


namespace iApprove.View
{
    public partial class PendingRequestsPage : BaseContentPage
    {

        #region "Properties"
        private PendingRequestsViewModel vm;
        private object navParam;
        public ICommand SearchCommand { get; set; }
        #endregion

        #region "Methods"
        public PendingRequestsPage(object a)
        {
            

            this.navParam = a;
            InitPage();

        }
		public PendingRequestsPage()
		{

			InitPage();

		}
        private void InitPage()
        {
            InitializeComponent();
            iApproveNavBar.MyNavigationBar.InitHeader("Requests", isMainPage: false, isSearchEnabled: true);
            vm = new PendingRequestsViewModel(App.NavigationServiceInstance,navParam);

            this.BindingContext = vm;

        }
        #endregion
    }
}
