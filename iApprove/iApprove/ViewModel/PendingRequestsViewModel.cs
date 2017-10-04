using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using iApprove.Interface;
using iApprove.Model;
using iApprove.Services;
using Xamarin.Forms;
using System.Linq;

namespace iApprove.ViewModel
{
    public class PendingRequestsViewModel: INotifyPropertyChanged
    {
        
		#region "Properties"
		public INavigationService navigationService;
		public event PropertyChangedEventHandler PropertyChanged;
        public bool ascending;

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

		//this variable is created temporarily
        List<PendingRequests> _requestList;
        public List<PendingRequests> RequestList
		{
			get { return this._requestList; }
			set
			{
				this._requestList = value;
				OnPropertyChanged("RequestList");
			}
		}

        PendingRequests _selectedPendingRequest;
		public PendingRequests SelectedPendingRequest
		{
			get { return this._selectedPendingRequest; }
			set
			{
				this._selectedPendingRequest = value;
                App.NavigationServiceInstance.NavigateTo(Enum.PageName.IA_PENDINGREQUESTSDETAILS, _selectedPendingRequest, false);
				OnPropertyChanged("PendingRequestList");
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

		//      string _sortImageSource;
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

        SelectMultipleBasePage<PendingRequests> multiPage;
		#endregion

		#region "Events"        

		protected void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this,
					new PropertyChangedEventArgs(propertyName));
			}
		}
		
        public ICommand FilterCommand { get; set; }

		public ICommand SortCommand { get; set; }

        public ICommand SortByIDTappedCommand { get; set; }

        public ICommand SortByDateTappedCommand { get; set; }

        public ICommand SortByTypeTappedCommand { get; set; }

        public ICommand SortByNameTappedCommand { get; set; }

        //public ICommand SearchCommand { get; set; }

        #endregion

        #region "Methods"  
        public PendingRequestsViewModel()
        {
            
        }
        public PendingRequestsViewModel(INavigationService navService, object a)
        {
			//var navStack = App.CustomNavigation.NavigationStack;
			//var bindingContext = navStack[navStack.Count - 1].BindingContext;
			//if (bindingContext is HomeViewModel)
			//{
			//	((HomeViewModel)bindingContext).SelectedDept = null;
			//}

			navigationService = navService;

            string deptID = a.ToString();

            DataService service = new DataService();

            RequestList = service.GetPendingRequests(deptID) as List<PendingRequests>;

            PendingRequestList = new ObservableCollection<PendingRequests>(RequestList);

            this.FilterCommand = new Command(() =>
            {
                List<PendingRequests> pendingRequestList = new List<PendingRequests>(PendingRequestList);

                if (multiPage == null)
                {
                    multiPage = new SelectMultipleBasePage<PendingRequests>(pendingRequestList);
                }
                Application.Current.MainPage.Navigation.PushModalAsync(multiPage);
               // App.NavigationServiceInstance.NavigateTo(Enum.PageName.IA_PENDINGREQUESTSDETAILS, _selectedPendingRequest, false);
            });

            this.SortCommand = new Command(() =>
            {

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

            #region Search

            //this.SearchCommand = new Command(() =>
            //{
                
                
            //});
            #endregion


        }		
		#endregion
	}
}
