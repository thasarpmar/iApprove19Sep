using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using iApprove.Interface;
using iApprove.Model;
using iApprove.Services;
using iApprove.View.PendingRequestDetailContentView;

namespace iApprove.ViewModel
{
    public class PendingRequestDetailsViewModel: INotifyPropertyChanged
    {
		#region "Properties"
        public INavigationService navigationService;

        BasicInfo _requestBasicInfo;
		public BasicInfo RequestBasicInfo
		{
			get
			{
				return _requestBasicInfo;
			}
			set
			{
				_requestBasicInfo = value;
				OnPropertyChanged("RequestBasicInfo");
			}
		}

        ObservableCollection<SubRequest> _subRequestsList;
		public ObservableCollection<SubRequest> SubRequestsList
		{
			get
			{
				return _subRequestsList;
			}
			set
			{
				_subRequestsList = value;
				OnPropertyChanged("SubRequestsList");
			}
		}

        History _requestHistory;
		public History RequestHistory
		{
			get
			{
				return _requestHistory;
			}
			set
			{
				_requestHistory = value;
				OnPropertyChanged("RequestHistory");
			}
		}

        CommentsModel _requestComments;
		public CommentsModel RequestComments
		{
			get
			{
				return _requestComments;
			}
			set
			{
				_requestComments = value;
				OnPropertyChanged("RequestComments");
			}
		}
        string _contentViewHeader;
        public string ContentViewHeader
		{
			get
			{
				return _contentViewHeader;
			}
			set
			{
				_contentViewHeader = value;
				OnPropertyChanged("ContentViewHeader");
			}
		}

        #endregion

		#region "Events"        
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		#region "Methods"  
		protected void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this,
					new PropertyChangedEventArgs(propertyName));
			}
		}		
        public PendingRequestDetailsViewModel()
        {
            
        }
        public PendingRequestDetailsViewModel(INavigationService navService, object param)
		{
			
			navigationService = navService;

			DataService service = new DataService();

            PendingRequests pendingRequest = new PendingRequests();

            //pendingRequest = param as PendingRequests;

            string departmentIdentifier = "1";

            //PendingRequestDetails requestDetails = service.GetPendingRequestDetails(departmentIdentifier, pendingRequest.RequestNo) as PendingRequestDetails;
            var a = service.GetPendingRequestDetails(departmentIdentifier);

            TempList = ((PendingRequestDetailModel)a).data;
            //RequestBasicInfo = requestDetails

			//PendingRequestList = new ObservableCollection<PendingRequests>(requestList);

		}
        List<Datum> _tempList;
        public List<Datum> TempList
		{
			get
			{
				return _tempList;
			}
			set
			{
				_tempList = value;
				OnPropertyChanged("TempList");
			}
		}
		#endregion
	}

	
}
