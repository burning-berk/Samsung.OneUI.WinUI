using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace Samsung.OneUI.WinUI.Converters;

internal class StringToVisibilityConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, string language)
	{
		Visibility visibility = Visibility.Collapsed;
		if (value != null)
		{
			visibility = (string.IsNullOrEmpty((string)value) ? Visibility.Collapsed : Visibility.Visible);
		}
		return visibility;
	}

	public object ConvertBack(object value, Type targetType, object parameter, string language)
	{
		throw new NotImplementedException();
	}
}
