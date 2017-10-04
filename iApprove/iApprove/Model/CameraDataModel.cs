using iApprove.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace iApprove.Model
{
	public class CameraDataModel
	{
		public string FileDir { set; get; }
		public string FileName { set; get; }
		public CarViewType CarViewType { get; set; }
		public Button ClickedButton { get; set; }
	}
}
