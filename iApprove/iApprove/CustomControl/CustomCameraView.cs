using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iApprove.CustomControl
{
	public class CustomCameraView : Xamarin.Forms.Frame
	{
		public CustomCameraView()
		{

		}

		public ICommand CaptureCommand { get; set; }

		public ICommand PhotoCallBack { get; set; }
	}
}
