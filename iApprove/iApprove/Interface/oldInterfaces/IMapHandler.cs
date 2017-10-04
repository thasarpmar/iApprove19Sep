using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using iApprove.Model;

namespace iApprove.Interface
{
	public interface IMapHandler
	{
		void UpdateLocation(double latitude, double longitude, string address="");
	}
}
