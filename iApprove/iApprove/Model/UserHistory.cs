using System;
using System.ComponentModel;
using SQLite;

namespace iApprove
{
	public class UserHistory
	{
		public UserHistory()
		{
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this,
						new PropertyChangedEventArgs(propertyName));
			}
		}

		private string incidentId;
		[PrimaryKey]
		public string IncidentID
		{
			get { return this.incidentId; }
			set
			{
				this.incidentId = value;
				OnPropertyChanged("IncidentID");
			}
		}
		private string incidentDate;
		public string IncidentDate
		{
			get { return this.incidentDate; }
			set
			{
				this.incidentDate = value;
				OnPropertyChanged("IncidentDate");
			}
		}
		private string inslocaiton;
		public string IncidentLocation
		{
			get { return this.inslocaiton; }
			set
			{
				this.inslocaiton = value;
				OnPropertyChanged("IncidentLocation");
			}
		}
		private string isEmailed;
		public string IsEmailed
		{
			get { return this.isEmailed; }
			set { this.isEmailed = value; }
		}
		private string isReportGen;
		public string IsReportGenerated
		{
			get { return this.isReportGen; }
			set { this.isReportGen = value; }
		}
		private string reportPath;
		public string ReportPath
		{
			get { return this.reportPath; }
			set { this.reportPath = value; }
		}
		private string incidentType;
		public string IncidentType
		{
			get { return this.incidentType; }
			set
			{
				this.incidentType = value;
				OnPropertyChanged("IncidentType");
			}
		}
		private string incidentLogo;
		public string IncidentLogo
		{
			get { return this.incidentLogo; }
			set
			{
               	this.incidentLogo = value;
				OnPropertyChanged("IncidentLogo");
			}
		}
	}
}
