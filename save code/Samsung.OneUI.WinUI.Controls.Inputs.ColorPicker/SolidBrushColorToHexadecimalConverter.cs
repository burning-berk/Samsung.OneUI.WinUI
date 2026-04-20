using System;
using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;

namespace Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker;

internal class SolidBrushColorToHexadecimalConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, string language)
	{
		if (value == null)
		{
			return Colors.Gray.ToString();
		}
		return ((SolidColorBrush)value).Color.ToString();
	}

	public object ConvertBack(object value, Type targetType, object parameter, string language)
	{
		throw new NotImplementedException();
	}
}
