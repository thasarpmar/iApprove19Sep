using System;
using System.Collections.Generic;
using iApprove.CustomControl;
using iApprove.View.BasicInfo;
using iApprove.ViewModel;
using Xamarin.Forms;

namespace iApprove.View
{
    public partial class PendingRequestDetailsPage : BaseContentPage
    {
        void Handle_Tapped(object sender, System.EventArgs e)
         {
            //Label l = BasicInfoStkPnl.FindByName<Label>("BasicInfoLbl");
        }

        #region "Properties"
        private PendingRequestDetailsViewModel vm;
		private object navParam;
		#endregion		

		#region "Methods"
        public PendingRequestDetailsPage(object args)
		{
            navParam = args;
            InitPage();
		}
		public PendingRequestDetailsPage()
		{
			
			InitPage();
		}

		private void InitPage()
		{
            iApproveNavBar.MyNavigationBar.InitHeader("ITARAS Requsests", isMainPage: false);

			InitializeComponent();

            vm = new PendingRequestDetailsViewModel(App.NavigationServiceInstance, navParam);

			this.BindingContext = vm;
            CreateStackPanel();

            //BasicInfoContentView basicContentView = new BasicInfoContentView();

            //basicContentView.BindingContext = 

            //ContentGrd.Children.Add(new BasicInfoContentView());
		}
        public void CreateStackPanel()
        {
            StackLayout stcLayout = new StackLayout();
            stcLayout.Orientation = StackOrientation.Horizontal;
            var a = vm.TempList;

            foreach (var item in a)
            { 
                var Lbl = new Label { VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand,Text=item.Value,TextColor = Color.Blue };
                stcLayout.Children.Add(Lbl);
				
            }
            MainGrd.Children.Add(stcLayout);
            Grid.SetRow(stcLayout, 0);
        }

		

		#endregion
	}
}
