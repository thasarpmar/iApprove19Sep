using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using iApprove.Model;
using iApprove.View;
using iApprove.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace iApprove.CustomControl
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class iApproveNavBar : ContentView
	{
        
        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            var navStack = App.CustomNavigation.NavigationStack;
            var bindingContext = navStack[navStack.Count - 1].BindingContext;

            if (bindingContext is PendingRequestsViewModel)
            {
                var pVM = bindingContext as PendingRequestsViewModel;
                pVM.PendingRequestList = new ObservableCollection<PendingRequests>(Helper.Instance.SearchByRequests(e.NewTextValue));
            }
            else if(bindingContext is HomeViewModel)
            {
                var hVM = bindingContext as HomeViewModel;
                hVM.PendingRequestList = new ObservableCollection<PendingRequests>(Helper.Instance.SearchByRequests(e.NewTextValue));
				
            }

        }

        public static iApproveNavBar MyNavigationBar { get; set; }
		public iApproveNavBar()
		{
			InitializeComponent();
			MyNavigationBar = this;
			if (Device.OS == TargetPlatform.iOS)
			{
				MainContent.Padding = new Thickness(0, 20, 0, 0);
				MainContent.HeightRequest = 70;
				HeaderParentGrid.RowDefinitions[0].Height = 70;
			}

		}

		//Events
		//Methods
		public void OnBackButtonPressed(object sender, EventArgs args)
		{
			if (App.CustomNavigation != null)
			{
                if(App.CustomNavigation.ModalStack.Count>0)  
                {
                    App.CustomNavigation.PopModalAsync();
                }
                else
                    App.CustomNavigation.PopAsync();              
			}
		}
		public void OnHamburgerButtonPressed(object sender, EventArgs args)
		{
            //Show Hamburger Menu Here
            if(HamburgerCommand!=null)
            {
                //HamburgerCommand.Execute(null);
                userProfileSection.IsVisible = !userProfileSection.IsVisible;
                if(userProfileSection.IsVisible)
                {
                    menuHamburger.Source = "Close.png"; 
                }
                else
                {
                    menuHamburger.Source = "Menubar.png";

				}
            }
		}
	
		public void OnNotificationButtonPressed(object sender, EventArgs args)
		{
			//Show Notifications Screen
			//iApprove.Services.DataService service = new Services.DataService();
            App.NavigationServiceInstance.NavigateTo(Enum.PageName.IA_NOTIFICATIONS, "", false);
		}
		public void OnSearchButtonPressed(object sender, EventArgs args)
		{
            //if(SearchCommand !=null){

            //    SearchTxtBar.IsVisible = true;
            //    HeaderTitle.IsVisible = false;
            //menuHamburger.IsVisible = false;
            //menuBack.IsVisible = false;
            //menuNotification.IsVisible = false;
            //menuSearch.IsVisible = false;

            //App.NavigationServiceInstance.NavigateTo(Enum.PageName.IA_SEARCH, "", false);
			Application.Current.MainPage.Navigation.PushModalAsync(new Search());
				//SearchCommand.Execute(SearchTxtBar.Text);
				//}
		}


		public void OnLogoutPressed(object sender, EventArgs args)
		{
            LogOffPng.Source = "Logoff_Onclick.png";
            LogOffText.TextColor = Color.FromHex("#00465B");

		}

        //public void OnHelpPressed(object sender, EventArgs args)
        //{
        //	LogOffPng.Source = "HelpOnclick.png";
        //	LogOffText.TextColor = Color.FromHex("#00465B");

        //}

        public void InitHeader(string title, bool isMainPage = false, bool isSearchEnabled = false, bool isNoIcons = false, bool isNotificationCount = false)
        {
            HeaderTitle.Text = title;
            HeaderTitle.HorizontalTextAlignment = TextAlignment.Start;
            HeaderTitle.IsVisible = true;

            if (isNoIcons)
            {
                menuNotification.IsVisible = false;
                menuSearch.IsVisible = false;
                menuBack.IsVisible = false;
                menuHamburger.IsVisible = false;
                HeaderTitle.HorizontalTextAlignment = TextAlignment.Center;
                return;
            }

                //Right Side icons visible/invisible
                menuNotification.IsVisible = isMainPage;
                menuSearch.IsVisible = isSearchEnabled;

                //Left Side icons visible/invisible
                menuBack.IsVisible = !isMainPage;
                menuHamburger.IsVisible = isMainPage;

                //TODO: make null all command
                userProfileSection.IsVisible = false;
                //    if (fullSearch == true){


			//         SearchTxtBar.IsVisible = false; 
			//}
			// SearchTxtBar.IsVisible = false; 
			//    NotificationCountLabel.IsVisible = false;
		}
		public ICommand SearchCommand { get; set; }
		public ICommand SearchTextEntryCommand { get; set; }
        public ICommand HamburgerCommand { get; set; }
	}
}
