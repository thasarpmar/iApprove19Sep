using System;
namespace iApprove
{
	public class AuthenticationResult
	{
		public AuthenticationResult()
		{
		}

		private int statusCode;
		public int StatusCode
		{
			get { return this.statusCode;}
			set { this.statusCode = value; }
		}

		private string statusMessage;
		public string StatusMessage
		{
			get { return this.statusMessage; }
			set { this.statusMessage = value; }
		}

		private UserInfoModel userInfo;
		public UserInfoModel UserInfo
		{
			get { return this.userInfo; }
			set { this.userInfo = value; }
		}
	}
}
