using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace Samsung.OneUI.WinUI.Controls.Converters;

internal class OpacityToVisibilityConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, string language)
	{
		return (value == null || !(value is double num) || !(num > 0.0)) ? Visibility.Collapsed : Visibility.Visible;
	}

	public object ConvertBack(object value, Type targetType, object parameter, string language)
	{
		return value;
	}
}
