using System;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace Samsung.OneUI.WinUI.Converters;

internal class ColorWithTransparencyConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, string language)
	{
		Color? color = null;
		if (value is SolidColorBrush solidColorBrush)
		{
			color = solidColorBrush.Color;
		}
		else if (value is Color value2)
		{
			color = value2;
		}
		if (color.HasValue && parameter is string s && double.TryParse(s, out var result))
		{
			Color value3 = color.Value;
			return Color.FromArgb((byte)(result * 255.0), value3.R, value3.G, value3.B);
		}
		return value;
	}

	public object ConvertBack(object value, Type targetType, object parameter, string language)
	{
		throw new NotImplementedException();
	}
}
