using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NotebookDemo.WPF.Converter
{
	/// <summary>
	/// A converter that takes in a boolean and returns a <see cref="Visibility"/>.<br/>
	/// Returns <see cref="Visibility.Visible"/> for <see langword="true"/>; otherwise, <see cref="Visibility.Collapsed"/>.
	/// </summary>
	public class BoolToVIsibilityFalseToCollapsedConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return parameter == null
				? (bool)value ? Visibility.Visible : Visibility.Collapsed
				: (bool)value ? Visibility.Collapsed : Visibility.Visible;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
