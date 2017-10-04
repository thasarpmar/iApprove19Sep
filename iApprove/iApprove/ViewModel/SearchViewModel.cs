using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using iApprove.Interface;
using iApprove.Model;
using iApprove.Services;
using Xamarin.Forms;

namespace iApprove.ViewModel
{
    public class SearchViewModel: INotifyPropertyChanged
    {
        public ICommand TextChangedCommand { get; set; }
		#region "Properties"
		public INavigationService navigationService;
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

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

		string _sortImageSource;
		public string SortImageSource
		{
			get { return this._sortImageSource; }
			set
			{
				this._sortImageSource = value;
				OnPropertyChanged("SortImageSource");
			}
		}

		protected void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this,
					new PropertyChangedEventArgs(propertyName));
			}
		}

		public ICommand SortByIDTappedCommand { get; set; }

		public ICommand SortByDateTappedCommand { get; set; }

		public ICommand SortByTypeTappedCommand { get; set; }

		public ICommand SortByNameTappedCommand { get; set; }


        public SearchViewModel(INavigationService navService)
        {
            //SearchTextVisibility = true;

			navigationService = navService;

			this.SortByIDTappedCommand = new Command(() =>
			{
				if (SortImageSource == "up-chevron.png")
				{
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
				else
				{
					//For sorting it from Lower RequestNo to Higher RequestNo
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
			});


			this.SortByDateTappedCommand = new Command(() =>
			{
				if (SortImageSource == "up-chevron.png")
				{
					//For sorting it from Higher RequestNo to Lower RequestNo
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
				else
				{
					//For sorting it from Lower RequestNo to Higher RequestNo
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

			});

			this.SortByTypeTappedCommand = new Command(() =>
			{
				//For sorting it based on alphabets that is a ,b ,c ...
				Helper.Instance.PendingRequestsList.Sort(delegate (PendingRequests x, PendingRequests y)
				{
					if (x.RequestType == null && y.RequestType == null) return 0;
					else if (x.RequestType == null) return -1;
					else if (y.RequestType == null) return 1;
					else return x.RequestType.CompareTo(y.RequestType);
				});
				PendingRequestList = new ObservableCollection<PendingRequests>(Helper.Instance.PendingRequestsList);
			});

			this.SortByNameTappedCommand = new Command(() =>
			{
				//For sorting it based on alphabets that is a ,b ,c ....
				Helper.Instance.PendingRequestsList.Sort(delegate (PendingRequests x, PendingRequests y)
				{
					if (x.RequestorName == null && y.RequestorName == null) return 0;
					else if (x.RequestorName == null) return -1;
					else if (y.RequestorName == null) return 1;
					else return x.RequestorName.CompareTo(y.RequestorName);
				});
				PendingRequestList = new ObservableCollection<PendingRequests>(Helper.Instance.PendingRequestsList);
			});
			
		
        }


    }
}




