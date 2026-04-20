using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using Samsung.OneUI.WinUI.Controls;

namespace Samsung.OneUI.WinUI.Converters;

internal class ChipsItemImageIconVisibilityConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, string language)
	{
		if (value != null && ChipsItemTemplate.Custom.Equals(value))
		{
			return Visibility.Visible;
		}
		return Visibility.Collapsed;
	}

	public object ConvertBack(object value, Type targetType, object parameter, string language)
	{
		throw new NotImplementedException();
	}
}
