using System;
namespace ProjectTemplate
{
	public class PurchaseOrderModel
	{
		public PurchaseOrderModel()
		{
		}

		private string poNumber;
		public string PONumber
		{
			get { return this.poNumber; }
			set { this.poNumber = value; }
		}

		private string releaseNo;
		public string ReleaseNo
		{
			get { return this.releaseNo; }
			set { this.releaseNo = value; }
		}

		private string date;
		public string Date
		{
			get { return this.date; }
			set { this.date = value; }
		}

		private string due;
		public string Due
		{
			get { return this.due; }
			set { this.due = value; }
		}

		private string poStatus;
		public string POStatus
		{
			get { return this.poStatus; }
			set { this.poStatus = value; }
		}

		private string delivetTo;
		public string DelivetTo
		{
			get { return this.delivetTo; }
			set { this.delivetTo = value; }
		}

		private string invoiceTo;
		public string InvoiceTo
		{
			get { return this.invoiceTo; }
			set { this.invoiceTo = value; }
		}
	}
}
