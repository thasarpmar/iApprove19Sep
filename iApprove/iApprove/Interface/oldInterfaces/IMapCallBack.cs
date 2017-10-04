using System;
using System.Collections.ObjectModel;
using iApprove.Model;

namespace iApprove.Interface
{
	public interface IMapCallBack
	{
		void CreatePin(double latitude, double longitude,string title, string address);
	}
}
