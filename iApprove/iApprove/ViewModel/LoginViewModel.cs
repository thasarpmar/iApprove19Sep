using iApprove.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using iApprove.Model;
using iApprove.Network;
using iApprove.Enum;

namespace iApprove.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        #region "Properties"
        public INavigationService navigationService;
        public event PropertyChangedEventHandler PropertyChanged;
        public bool rememberMe;
        ISecureStorage secureStoragetInstance = DependencyService.Get<ISecureStorage>();

        string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                CheckLoginStatus();
                OnPropertyChanged("UserName");
            }
        }

		string _password;
		public string Password
		{
			get
			{
				return _password;
			}
			set
			{
				_password = value;
                CheckLoginStatus();
				OnPropertyChanged("Password");
			}
		}

        bool _rememberMe;
        public bool RememberMe
		{
			get
			{
				return _rememberMe;
			}
			set
			{
				_rememberMe = value;
                if(_rememberMe)
                {
                    secureStoragetInstance.StoreString("UserName",UserName); 
                    secureStoragetInstance.StoreString("Password", Password);
				}
				OnPropertyChanged("RememberMe");
			}
		}
		Color _loginButtonBackgroundColor;
		public Color LoginButtonBackgroundColor
		{
			get
			{
				return _loginButtonBackgroundColor;
			}
			set
			{
				_loginButtonBackgroundColor = value;

				OnPropertyChanged("LoginButtonBackgroundColor");
			}
		}


		private ImageSource _sortImageSource;
		public ImageSource SortImageSource
		{
			get { return this._sortImageSource; }
			set
			{
				this._sortImageSource = value;
				OnPropertyChanged("SortImageSource");
			}
		}

		Color _loginButtonTextColor;
		public Color LoginButtonTextColor
		{
			get
			{
				return _loginButtonTextColor;
			}
			set
			{
				_loginButtonTextColor = value;

				OnPropertyChanged("LoginButtonTextColor");
			}
		}


		#endregion

		#region "Events"
		public ICommand LoginCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand SortByIDTappedCommand { get; set; }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
    new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region "Events And Methods"

        /// <summary>
        /// Used to handle Login Button
        /// </summary>
        /// <param name="navService"></param>
        public LoginViewModel(INavigationService navService)
        {
            SortImageSource = "RememberMe.png";
            navigationService = navService;
            UserName = secureStoragetInstance.RetrieveString("UserName");
            Password = secureStoragetInstance.RetrieveString("Password");
			LoginButtonBackgroundColor = (Color)Application.Current.Resources["BannerColor"];
			LoginButtonTextColor = (Color)Application.Current.Resources["LoginInActiveTextColor"];
			this.LoginCommand = new Command((action) =>
                {
                    if (ValidateLogin()) 
                    {
                    //TODO: After login validation navigates to Home Page
                    //navigationService.NavigateTo(Enum.PageName.IA_HOME, "", false);
                    iApprove.Services.DataService service = new Services.DataService();
                    LoginModel loginmodel = new LoginModel();
                    loginmodel.UserName = UserName;
                    loginmodel.Password = Password;
                    SessionInfo sInfo = (service.Login(loginmodel)) as SessionInfo;
                       // service.LoginService();
                    ApplicationModel.Current.SetSession(sInfo);
                    App.NavigationServiceInstance.NavigateTo(Enum.PageName.IA_HOME, "", false);
					//App.NavigationServiceInstance.NavigateTo(Enum.PageName.IA_DEMOPAGE,"", false);
					//pp.NavigationServiceInstance.NavigateTo(Enum.PageName.IA_PENDINGREQUESTSDETAILS, 1, false);
					}
            });

            this.ClearCommand = new Command((action) =>
               {
                   //Clear entries
               },
               (condition) =>
               {
                   return true;
               });

            this.SortByIDTappedCommand = new Command(() =>
            {
                if (rememberMe == false)
                {
                    SortImageSource = "RememberMeSelected.png";
                    rememberMe = true;
                }
                else {

					SortImageSource = "RememberMe.png";
                    rememberMe = false;
                }
            });

        }

        //private HomeViewModel PrepareHomePageData(SessionInfo info)
        //{
        //    HomeViewModel homeVM = new HomeViewModel(navigationService)
        //    {
        //        DepartmentList = info.DepartmentList,  
        //        NotificationCount = info.NotificationCount,
        //        UserProfile = info.UserInfo                      
        //    };

        //    return homeVM;
        //}

        public bool ValidateLogin()
        {
			return !string.IsNullOrEmpty((UserName.Trim())) && !string.IsNullOrEmpty((Password.Trim())) && UserName != Password;
        }
		private void CheckLoginStatus()
		{
			if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
			{
				LoginButtonBackgroundColor = (Color)Application.Current.Resources["Primary"];
				LoginButtonTextColor = (Color)Application.Current.Resources["LoginActiveTextColor"];
			}
			else
			{
				LoginButtonBackgroundColor = (Color)Application.Current.Resources["BannerColor"];
				LoginButtonTextColor = (Color)Application.Current.Resources["LoginInActiveTextColor"];
			}

		}
       
        #endregion

    }
}
