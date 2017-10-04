using System;
using SQLite;

namespace iApprove
{
	public class UserInfoModel
	{
		public UserInfoModel()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
	}
}
