using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace IndDictionary.Converters
{
	class BoolConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool temp=(!(bool)value);
			return temp;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool temp = (!(bool)value);
			return temp;
		}
	}
	class CheckBoxToString : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string temp=(bool)value? "Редактируется":"Редактировать";
			return temp;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool temp = (string)value=="Редактируется"? true:false;
			return temp;
		}
	}
	class DateToString : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((DateTime)value).ToString("dd.MM.yyyy");
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return DateTime.Parse(value as string);
		}
	}
}
