using System;
namespace iApprove
{
	public interface INetworkListener
	{
		void OnNetworkEnabled(bool status);
		void OnGPSEnabled(bool status);
	}
}
