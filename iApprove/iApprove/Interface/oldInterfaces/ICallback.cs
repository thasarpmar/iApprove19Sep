using System;
namespace iApprove
{
	public interface ICallback
	{
		void OnSuccess(ProcessStatus status);
		void OnFailure(ProcessStatus status);
	}
}
