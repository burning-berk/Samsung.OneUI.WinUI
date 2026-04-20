using System;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Converters;

internal class StringToSolidColorBrushConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, string language)
	{
		string colorString = string.Empty;
		if (value is string text && !string.IsNullOrEmpty(text))
		{
			colorString = text;
		}
		else if (parameter is SolidColorBrush result)
		{
			return result;
		}
		return ColorsHelpers.ConvertColorHex(colorString);
	}

	public object ConvertBack(object value, Type targetType, object parameter, string language)
	{
		throw new NotImplementedException();
	}
}
