using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using iApprove.Interface;
using iApprove.Model;

namespace iApprove.ViewModel
{
    public class NotificationsViewModel : INotifyPropertyChanged
    {

		#region "Properties"
		public INavigationService navigationService;
		public event PropertyChangedEventHandler PropertyChanged;
        #endregion

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this,
	        new PropertyChangedEventArgs(propertyName));
		}
		

        ObservableCollection<NotificationModel> _notificationsList;
		public ObservableCollection<NotificationModel> NotificationsList
		{
			get { return this._notificationsList; }
			set
			{
				this._notificationsList = value;
				OnPropertyChanged("NotificationsList");
			}
		}
		public NotificationsViewModel(INavigationService navService)
		{
			navigationService = navService;
            iApprove.Services.DataService ds = new Services.DataService();

            var requestList = ds.GetNotifications() as List<NotificationModel>;

			NotificationsList = new ObservableCollection<NotificationModel>(requestList);

		}
    }
}
