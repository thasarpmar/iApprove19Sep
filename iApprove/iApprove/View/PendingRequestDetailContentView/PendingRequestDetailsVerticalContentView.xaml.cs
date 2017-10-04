using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.ComponentModel;
using iApprove.ViewModel;

namespace iApprove.View.PendingRequestDetailContentView
{
    public partial class PendingRequestDetailsVerticalContentView : ContentView, INotifyPropertyChanged
    {
        public PendingRequestDetailsVerticalContentView()
        {
            InitializeComponent();
        }

		//private PendingRequestDetailModel _pendingRequestDetModel;
		//public PendingRequestDetailModel PendingRequestDetModel
		//{
		//	get
		//	{
		//		return this._pendingRequestDetModel;
		//	}
		//	set
		//	{
		//		this._pendingRequestDetModel = value;
		//		if (value == null)
		//		{
		//			this._pendingRequestDetModel = new PendingRequestDetailModel();
		//		}
		//		this.BindingContext = PendingRequestDetModel;
		//		OnPropertyChanged("PendingRequestDetModel");
		//	}
		//}
		private PendingRequestDetailsViewModel _pendingRequestDetModel;
		public PendingRequestDetailsViewModel PendingRequestDetModel
		{
			get
			{
				return this._pendingRequestDetModel;
			}
			set
			{
				this._pendingRequestDetModel = value;
				if (value == null)
				{
					this._pendingRequestDetModel = new PendingRequestDetailsViewModel();
				}
				this.BindingContext = PendingRequestDetModel;
				OnPropertyChanged("PendingRequestDetModel");
			}
		}

        public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
    }
}
