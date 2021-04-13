using System;
using System.Globalization;
using System.Windows.Data;

namespace NotebookDemo.WPF.Converter
{
	/// <summary>
	/// A converter that takes in a boolean and returns the inverted value.<br/>
	/// </summary>
	public class BoolInvertConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (bool)value == false;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
