using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Newtonsoft.Json;

namespace iApprove.Model
{
    public class SessionInfo:INotifyPropertyChanged
    {
        public SessionInfo()
        {
    }
        private ObservableCollection<DepartmentModel> _departmentList;

        public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this,
					new PropertyChangedEventArgs(propertyName));
			}
		}

        [JsonProperty("Departments")]
        public ObservableCollection<DepartmentModel> DepartmentList
        {
            get { return _departmentList; }
            set { _departmentList = value;
                OnPropertyChanged("DepartmentList");
            }
        }

        [JsonProperty("NotificationCount")]
        public int NotificationCount
        {
            get;
            set;
        }

        [JsonProperty("UserProfile")]
        public UserModel UserInfo
        {
            get;
            set;
        }
    }
}
