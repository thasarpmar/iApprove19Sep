using System;
namespace iApprove
{
	public class ProcessStatus
	{
		public enum StatusCodeEnum
		{
			Success,
			Failed,
			Interuppted
		}

		private int statusCode;
		public int StatusCode
		{
			get { return this.statusCode; }
			set { this.statusCode = value; }
		}

		private string statusMessage;
		public string StatusMessage
		{
			get { return this.statusMessage; }
			set { this.statusMessage = value; }
		}
	}
}
