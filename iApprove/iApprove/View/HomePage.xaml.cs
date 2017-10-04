using System;
using System.Collections.Generic;
using iApprove.CustomControl;
using iApprove.Model;
using iApprove.ViewModel;
using Xamarin.Forms;

namespace iApprove.View
{
    public partial class HomePage : BaseContentPage
    {

        #region "Properties"
        private HomeViewModel vm;
        private object navParam;
        #endregion
        public HomePage()
        {
           
            InitPage();

        }
        public HomePage(object args)
        {
            iApproveNavBar.MyNavigationBar.InitHeader("iApprove", isMainPage:true,isNotificationCount:true);
            iApproveNavBar.MyNavigationBar.HamburgerCommand = new Command((param) =>
            {

            });
            this.navParam = args;
            InitPage();
        }


		private void LoadData()
		{
			List<DepartmentModel1> DepartmentList = new List<DepartmentModel1>
			{
				new DepartmentModel1(){ DeptName = "ITARAS", Count= 1 },
			   new DepartmentModel1(){ DeptName = "Admin" , Count= 2} ,
				new DepartmentModel1(){ DeptName = "Finance" , Count= 3} ,
			  new DepartmentModel1(){ DeptName = "HR" , Count= 4} ,
				new DepartmentModel1(){ DeptName = "LOREM" , Count= 5} ,
			   new DepartmentModel1(){ DeptName = "IPSUM" , Count= 6}
			};
			//grdView.ItemsSource = DepartmentList;
		}
    //    private void OnLabelClicked(object sender, EventArgs e)
    //    {
    //        try
    //        {
				//var entity = ((Label)sender);
        //        entity.BackgroundColor = Color.Transparent;
        //    }
        //    catch()
        //    {


        //    }
        //}


        private void InitPage()
        {
            InitializeComponent();

            //LoadData();

            vm = new HomeViewModel(App.NavigationServiceInstance);

            this.BindingContext = vm;
        }
    }
	public class DepartmentModel1
	{
		public string DeptName
		{
			get;
			set;
		}
		public int Count
		{
			get;
			set;
		}
	}
}