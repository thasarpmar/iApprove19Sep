using System;
using System.Collections.Generic;
using iApprove.ViewModel;
using Xamarin.Forms;

namespace iApprove.View.PendingRequestDetailContentView
{
    public partial class PendingRequestsDetailsHorizontalContentView : ContentView
    {
        public PendingRequestsDetailsHorizontalContentView()
        {
            InitializeComponent();
            //this.BindingContext= 
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
		//public static readonly DependencyProperty ItemSource = DependencyProperty.Register(
		// "State", typeof(Boolean), typeof(PendingRequestsDetailsHorizontalContentView), new PropertyMetadata(false));
		//public Boolean State
		//{
		//	get { return (Boolean)this.GetValue(StateProperty); }
		//	set { this.SetValue(StateProperty, value); }
		//}
		
    }
}
