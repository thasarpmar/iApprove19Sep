using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Input;
using iApprove.CustomControl;
using iApprove.Interface;
using iApprove.Model;
using iApprove.Services;
using iApprove.View;
using Xamarin.Forms;
using System.Linq;

namespace iApprove.ViewModel
{
    public class HomeViewModel:INotifyPropertyChanged
    {

        #region "Properties"
        public INavigationService navigationService;
        public event PropertyChangedEventHandler PropertyChanged;
        public bool ascending;


        public string ShowbyApplicationLbl
        {
            get { return "Show By Application"; }

        }  
		public string ShowbyRequestsLbl
		{
			get { return "Show By Requests"; }

		}


        private string _showByText;
        public string ShowByText
		{
			get { return this._showByText; }
			set
			{
				this._showByText = value;
				OnPropertyChanged("ShowByText");
			}
		}
		
        private ImageSource _showApplictationImage;
        public ImageSource ShowApplictationImage
        {
            get { return this._showApplictationImage; }
			set
			{
				this._showApplictationImage = value;
				OnPropertyChanged("ShowApplictationImage");
			}
        }
        private ImageSource _showRequestsImage;
		public ImageSource ShowRequestsImage
		{
            get { return this._showRequestsImage; }
            set {this._showRequestsImage=value;
                OnPropertyChanged("ShowRequestsImage");
            }
		}
        ObservableCollection<ExtendedDepartmentModel> _departmentList;
        public ObservableCollection<ExtendedDepartmentModel> DepartmentList
        {
            get { return this._departmentList; }
            set
            {
                this._departmentList = value;
                OnPropertyChanged("DepartmentList");
            }
        }


        private int _notificationCount;
        public int NotificationCount
		{
			get { return this._notificationCount; }
			set
			{
				this._notificationCount = value;
				OnPropertyChanged("NotificationCount");
			}
		}

		private UserModel _userProfile;
		public UserModel UserProfile
		{
			get { return this._userProfile; }
			set
			{
				this._userProfile = value;
				OnPropertyChanged("UserProfile");
			}
		}

		//variable used for storing all the requests
		ObservableCollection<PendingRequests> _pendingRequestList;
		public ObservableCollection<PendingRequests> PendingRequestList
		{
			get { return this._pendingRequestList; }
			set
			{
				this._pendingRequestList = value;
				OnPropertyChanged("PendingRequestList");
			}
		}

		//variable used for storing all the requests
		ObservableCollection<PendingRequests> _filteredPendingRequestList;
		public ObservableCollection<PendingRequests> FilteredPendingRequestList
		{
			get { return this._filteredPendingRequestList; }
			set
			{
				this._filteredPendingRequestList = value;
				OnPropertyChanged("FilteredPendingRequestList");
			}
		}


		private DepartmentModel _selectedDept;
		public DepartmentModel SelectedDept
		{
			get { return this._selectedDept; }
			set
			{
				this._selectedDept = value;
				OnPropertyChanged("SelectedDept");
                IsShowOptionsVisible = false;	
               
			}
		}
        private bool _isShowOptionsVisible;
        public bool IsShowOptionsVisible
        {
            get
            {
                return this._isShowOptionsVisible;
            }
            set
            {
				this._isShowOptionsVisible = value;
				OnPropertyChanged("IsShowOptionsVisible"); 
            }
        }

		private bool _isSortByIDVisible;
		public bool IsSortByIDVisible
		{
			get
			{
				return this._isSortByIDVisible;
			}
			set
			{
				this._isSortByIDVisible = value;
				OnPropertyChanged("IsSortByIDVisible");
			}
		}

		private bool _isSortByDateVisible;
		public bool IsSortByDateVisible
		{
			get
			{
				return this._isSortByDateVisible;
			}
			set
			{
				this._isSortByDateVisible = value;
				OnPropertyChanged("IsSortByDateVisible");
			}
		}

		private bool _isSortByTypeVisible;
		public bool IsSortByTypeVisible
		{
			get
			{
				return this._isSortByTypeVisible;
			}
			set
			{
				this._isSortByTypeVisible = value;
				OnPropertyChanged("IsSortByTypeVisible");
			}
		}


		private bool _isSortByNameVisible;
		public bool IsSortByNameVisible
		{
			get
			{
				return this._isSortByNameVisible;
			}
			set
			{
				this._isSortByNameVisible = value;
				OnPropertyChanged("IsSortByNameVisible");
			}
		}

		//for showing/hiding department and request list
		private bool _isDepartmentListVisible;
		public bool IsDepartmentListVisible
		{
			get
			{
				return this._isDepartmentListVisible;
			}
			set
			{
				this._isDepartmentListVisible = value;
				OnPropertyChanged("IsDepartmentListVisible");
			}
		}
		private bool _isRequestListVisible;
		public bool IsRequestListVisible
		{
			get
			{
				return this._isRequestListVisible;
			}
			set
			{
				this._isRequestListVisible = value;
				OnPropertyChanged("IsRequestListVisible");
			}
		}
		//string _sortImageSource;
		//public string SortImageSource
		//{
		//	get { return this._sortImageSource; }
		//	set
		//	{
		//		this._sortImageSource = value;
		//		OnPropertyChanged("SortImageSource");
		//	}
		//}

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

		public Assembly SvgAssembly
		{
            get { return typeof(App).GetTypeInfo().Assembly; }
		}

		public string CoolMaskSvgPath
		{
			get { return "iApprove.RadioButton_Selected.svg"; }
		}

     


		Color _showTextByApp;
		public Color ShowTextByApp
		{
			get
			{
				return _showTextByApp;
			}
			set
			{
				_showTextByApp = value;

				OnPropertyChanged("ShowTextByApp");
			}
		}

		Color _showTextByReq;
		public Color ShowTextByReq
		{
			get
			{
				return _showTextByReq;
			}
			set
			{
				_showTextByReq = value;

				OnPropertyChanged("ShowTextByReq");
			}
		}

		#endregion

		#region "Events"   


        public ICommand OnShowOptionsPressed { get; set; }
        public ICommand ShowByApplication { get; set; }
        public ICommand ShowByRequest{ get; set; }
		public ICommand FilterCommand { get; set; }

		public ICommand SortByIDTappedCommand { get; set; }

		public ICommand SortByDateTappedCommand { get; set; }

		public ICommand SortByTypeTappedCommand { get; set; }

		public ICommand SortByNameTappedCommand { get; set; }

       




		protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
        //List<KeyValuePair<string,List>>
        SelectMultipleBasePage<ExtendedDepartmentModel> multiPage;



        #region "Methods"       
        public HomeViewModel(INavigationService navService)
        {
           ShowTextByApp  = (Color)Application.Current.Resources["Primary"];
            ShowTextByReq = (Color)Application.Current.Resources["SubTextForeColor"];
            IsShowOptionsVisible = false;
            navigationService = navService;
            ShowByText = "Show by Application";
            ShowApplictationImage = "RBSelected.png";
            ShowRequestsImage = "RB.png";
            IsDepartmentListVisible = true;
            IsRequestListVisible = false;

            SessionInfo sessionInfo = ApplicationModel.Current.GetSession();

            List<ExtendedDepartmentModel> extendedDept = new List<ExtendedDepartmentModel>();

            foreach( var item in sessionInfo.DepartmentList)
            {
                ExtendedDepartmentModel a  = new ExtendedDepartmentModel{DepartmentModel = item};
                extendedDept.Add(a);
            }

           

            DepartmentList = new ObservableCollection<ExtendedDepartmentModel>(extendedDept);
			NotificationCount = sessionInfo.NotificationCount;
			UserProfile = sessionInfo.UserInfo;

			//this part is used for binding all the pending requests
			DataService service = new DataService();

			Helper.Instance.PendingRequestsList = service.GetPendingRequests("1") as List<PendingRequests>;

            PendingRequestList = new ObservableCollection<PendingRequests>(Helper.Instance.PendingRequestsList);

            //this part is for showing and hiding the filter section
            this.OnShowOptionsPressed = new Command((action) => 
            { 
                IsShowOptionsVisible = !IsShowOptionsVisible; 
            });

            //this part is used for showing the listview for requests
            this.ShowByRequest = new Command((action) =>
            {
                ShowTextByApp = (Color)Application.Current.Resources["SubTextForeColor"]; 
                ShowTextByReq = (Color)Application.Current.Resources["Primary"];
                ShowByText = "Show by Requests";
                IsDepartmentListVisible = false;
                IsRequestListVisible = true;
				ShowApplictationImage = "RB.png";
				ShowRequestsImage = "RBSelected.png";
                IsShowOptionsVisible = false;
                iApproveNavBar.MyNavigationBar.InitHeader("iApprove", isMainPage: true, isSearchEnabled: true, isNoIcons: false);
            });

            //this part is used to show the departments
            this.ShowByApplication = new Command((action) => 
            {
				ShowTextByApp = (Color)Application.Current.Resources["Primary"];
				ShowTextByReq = (Color)Application.Current.Resources["SubTextForeColor"];
                ShowByText = "Show by Application";
                IsDepartmentListVisible = true;
                IsRequestListVisible = false;
				ShowApplictationImage = "RBSelected.png";
				ShowRequestsImage = "RB.png";
                IsShowOptionsVisible = false;
				iApproveNavBar.MyNavigationBar.InitHeader("iApprove", isMainPage: true, isSearchEnabled: false, isNoIcons: false);
            });
			this.FilterCommand = new Command((action) =>
			{
                //var items = new List<CheckItem>();
                //items.Add(new CheckItem { Name = "Xamarin.com" });
                //items.Add(new CheckItem { Name = "Twitter" });
                //items.Add(new CheckItem { Name = "Facebook" });
                //items.Add(new CheckItem { Name = "Internet ad" });
                //items.Add(new CheckItem { Name = "Online article" });
                //items.Add(new CheckItem { Name = "Magazine ad" });
                //items.Add(new CheckItem { Name = "Friends" });
                //items.Add(new CheckItem { Name = "At work" });



                List<ExtendedDepartmentModel> departmentList = new List<ExtendedDepartmentModel>(DepartmentList);

                if (multiPage == null)
                {
                    multiPage = new SelectMultipleBasePage<ExtendedDepartmentModel>(departmentList);
                }
                Application.Current.MainPage.Navigation.PushModalAsync(multiPage);
			});


			//Below functions are used for sorting
			this.SortByIDTappedCommand = new Command(() =>
			{
                IsSortByDateVisible = false;
                IsSortByNameVisible = false;
                IsSortByTypeVisible = false;
                if (ascending == false)
                {
                    ascending = true;
                    IsSortByIDVisible = true;
                    SortImageSource = "Sortdown.png";
                    // For sorting it from Lower RequestNo to Higher RequestNo
                    for (int i = PendingRequestList.Count - 1; i >= 0; i--)
                    {
                        for (int j = 1; j <= i; j++)
                        {
                            if (Convert.ToInt64(PendingRequestList[j - 1].RequestNo) > Convert.ToInt64(PendingRequestList[j].RequestNo))
                            {
                                PendingRequestList.Move(j, j - 1);
                            }
                        }
                    }

                }

                else

                {
                    ascending = false;
                    IsSortByIDVisible = true;
                    SortImageSource = "Sortup.png";
                    //For sorting it from Higher RequestNo to Lower RequestNo
                    for (int i = PendingRequestList.Count - 1; i >= 0; i--)
                    {
                        for (int j = 1; j <= i; j++)
                        {
                            if (Convert.ToInt64(PendingRequestList[j - 1].RequestNo) < Convert.ToInt64(PendingRequestList[j].RequestNo))
                            {
                                PendingRequestList.Move(j, j - 1);
                            }
                        }
                    }
                }
                 

			});


			this.SortByDateTappedCommand = new Command(() =>
			{
                IsSortByIDVisible = false;
                IsSortByNameVisible = false;
                IsSortByTypeVisible = false;
                if (ascending == false)
                {
                    ascending = true;
                    IsSortByDateVisible = true;

                    SortImageSource = "Sortdown.png";

                    for (int i = PendingRequestList.Count - 1; i >= 0; i--)
                    {
                        for (int j = 1; j <= i; j++)
                        {
                            if (Convert.ToDateTime(PendingRequestList[j - 1].DateReceived) > Convert.ToDateTime(PendingRequestList[j].DateReceived))
                            {
                                PendingRequestList.Move(j, j - 1);
                            }
                        }
                    }

                }

                else
				{
					ascending = false;
					IsSortByDateVisible = true;
					SortImageSource = "Sortup.png";
					for (int i = PendingRequestList.Count - 1; i >= 0; i--)
					{
						for (int j = 1; j <= i; j++)
						{
							if (Convert.ToDateTime(PendingRequestList[j - 1].DateReceived) < Convert.ToDateTime(PendingRequestList[j].DateReceived))
							{
								PendingRequestList.Move(j, j - 1);
							}
						}
					}
				}


			});

			this.SortByTypeTappedCommand = new Command(() =>
			{

                IsSortByIDVisible = false;
                IsSortByDateVisible = false;
                IsSortByNameVisible = false;
                IsSortByTypeVisible = true;

                if (ascending == false)
                {
                    ascending = true;
					SortImageSource = "Sortdown.png";
                    var list = PendingRequestList.OrderBy(x => x.RequestType).ToList();
					PendingRequestList = new ObservableCollection<PendingRequests>(list);
                }
                else
                {
                    ascending = false;
                    SortImageSource = "Sortup.png";
                      var list = PendingRequestList.OrderByDescending(x => x.RequestType).ToList();
					PendingRequestList = new ObservableCollection<PendingRequests>(list);
                }
               
			});

			this.SortByNameTappedCommand = new Command(() =>
			{
				IsSortByIDVisible = false;
				IsSortByDateVisible = false;
                IsSortByNameVisible = true;
                IsSortByTypeVisible = false;

				if (ascending == false)
				{
					ascending = true;
					SortImageSource = "Sortdown.png";
                    var list = PendingRequestList.OrderBy(x => x.RequestorName).ToList();
					PendingRequestList = new ObservableCollection<PendingRequests>(list);
				}
				else
				{
					ascending = false;
					SortImageSource = "Sortup.png";
                    var list = PendingRequestList.OrderByDescending(x => x.RequestorName).ToList();
					PendingRequestList = new ObservableCollection<PendingRequests>(list);
				}
			});

			
		}

		

		#endregion
	}

    public class ExtendedDepartmentModel:INotifyPropertyChanged
    {
		private DepartmentModel _departmentModel;

        public event PropertyChangedEventHandler PropertyChanged;

        public DepartmentModel DepartmentModel
		{
			get
			{
				return this._departmentModel;
			}
			set
			{
				this._departmentModel = value;
				OnPropertyChanged("DepartmentModel");
			}
		}

		public ICommand DepartmentTappedCommand { get; set; }


        public ExtendedDepartmentModel() {
            this.DepartmentTappedCommand = new Command((obj) =>

			{
				App.NavigationServiceInstance.NavigateTo(Enum.PageName.IA_PENDINGREQUESTS, 1, false);
			});
        }

		protected void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this,
					new PropertyChangedEventArgs(propertyName));
			}
		}
    }



}








