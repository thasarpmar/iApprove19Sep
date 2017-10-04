using System;
using System.Globalization;
using Xamarin.Forms;

namespace iApprove.Converter
{
    public class TextToImageConverter: IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null) return "";

			var a = value.ToString();

			if (a.Trim() == "FINANCE")
			{
				return "Finance.png";
			}
			else if (a.Trim() == "ITARAS")
			{
				return "ITARAS.png";
			}
			else if (a.Trim() == "HR")
			{
				return "HR.png";
			}
			else if (a.Trim() == "PDx")
			{
				return "PDx.png";
			}
			else if (a.Trim() == "CAR")
			{
				return "CAR.png";
			}
			else if (a.Trim() == "LOREMIPSUM")
			{
				return "LoremIpsum.png";
			}

			else if (a.Trim() == "IPSUM")
			{
				return "Ipsum.png";
			}
			else
			{
				return null;
			}

		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return "";
		}

	}
}
