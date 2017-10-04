using System;
using System.Collections.Generic;
using iApprove.Interface;
using Xamarin.Forms.Maps;

namespace iApprove.CustomControl
{
	public class CustomMap : Map
	{
		public List<CustomPin> CustomPins { get; set; }

		public IMapHandler MapHandler { get; set; }

		public IMapCallBack MapCallBack { get; set; }
	} 
}
