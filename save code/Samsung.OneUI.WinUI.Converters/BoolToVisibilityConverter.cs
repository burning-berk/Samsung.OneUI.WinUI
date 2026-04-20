using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace Samsung.OneUI.WinUI.Converters;

internal class BoolToVisibilityConverter : IValueConverter
{
	private enum Parameters
	{
		Normal,
		Inverted
	}

	public object Convert(object value, Type targetType, object parameter, string language)
	{
		Visibility visibility = Visibility.Collapsed;
		if (value != null)
		{
			Parameters parameters = Parameters.Normal;
			if (parameter != null)
			{
				parameters = (Parameters)Enum.Parse(typeof(Parameters), (string)parameter);
			}
			visibility = ((parameters != Parameters.Inverted) ? ((!(bool)value) ? Visibility.Collapsed : Visibility.Visible) : (((bool)value) ? Visibility.Collapsed : Visibility.Visible));
		}
		return visibility;
	}

	public object ConvertBack(object value, Type targetType, object parameter, string language)
	{
		throw new NotImplementedException();
	}
}
