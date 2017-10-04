using System;
using System.Collections.Generic;
using iApprove.Enum;
using iApprove.Interface;
using iApprove.Model;
using iApprove.Repository;
using iApprove.Services;
using iApprove.View;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace iApprove
{
	public partial class App : Application
	{
		public static INetworkListener NetworkListener;
		public static INavigationService NavigationServiceInstance;
		public static INavigation CustomNavigation;
		public static ICommonUtility MyUtility;
		public static IDataSource MyApplicationDataSource;
		public static Geocoder geoCoder;
		public static Position lastiOSPosition;
		public static bool IsMasterDetailFlow = false;

		public App()
		{
            
			lastiOSPosition = new Position(-1, -1);
			InitializeComponent();
			geoCoder = new Geocoder();
			MyApplicationDataSource = new DataService(); //TODO: Datasource instance can be created based on ource /DB/FILE/SErvice
			NavigationServiceInstance = DependencyService.Get<INavigationService>();
			MyUtility = DependencyService.Get<ICommonUtility>();
			NavigationServiceInstance.CreatePageMap();

			//Init Database
			bool isFirstTimeLaunch = MyUtility.IsFirstTimeLaunch();
			IFileHandler fileHandler = DependencyService.Get<IFileHandler>();

			if (isFirstTimeLaunch)
			{
				//MyApplicationDataSource.CreateDatabase(fileHandler, MyUtility);
				MyUtility.SaveFirstTimeLaunch(false);
			}
			//MyApplicationDataSource.OpenDatabase(fileHandler, MyUtility);
			
			//TODO: Enable this for RT POC
			if (IsMasterDetailFlow)
			{
				MainPage = new MasterMainPage();
			}
			else			
            {
				// MainPage = new TestPage();
				NavigationServiceInstance.NavigateTo(Enum.PageName.IA_LOGIN, "", true);
			}
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}

		#region "Support Functions"
		private static ExceptionDatabase _exceptionDatabase;
		public static ExceptionDatabase ExceptionHandlerDatabase
		{
			get
			{
				if (_exceptionDatabase == null)
					_exceptionDatabase = new ExceptionDatabase();
				return _exceptionDatabase;
			}
			set { _exceptionDatabase = value; }
		}

		public static IPushNotificationHandler PushNotifiHandler { get; set; }

		#endregion
	}
}