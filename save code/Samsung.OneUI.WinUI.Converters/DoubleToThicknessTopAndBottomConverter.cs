using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace Samsung.OneUI.WinUI.Converters;

internal class DoubleToThicknessTopAndBottomConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, string language)
	{
		if (value is double num)
		{
			return new Thickness(0.0, num, 0.0, num);
		}
		return new Thickness(0.0);
	}

	public object ConvertBack(object value, Type targetType, object parameter, string language)
	{
		throw new NotImplementedException();
	}
}
