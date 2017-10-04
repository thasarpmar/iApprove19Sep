using System;
using System.Globalization;
using Xamarin.Forms;

namespace iApprove.Converter
{
    public class PendingRequestAppend :IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return null;

			var count = value.ToString();
			return "Pending Requests - " + count;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return "";
		}

	}
}
