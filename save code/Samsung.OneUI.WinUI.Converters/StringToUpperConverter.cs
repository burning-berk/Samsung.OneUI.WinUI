using System;
using Microsoft.UI.Xaml.Data;

namespace Samsung.OneUI.WinUI.Converters;

internal class StringToUpperConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, string language)
	{
		string result = string.Empty;
		if (!string.IsNullOrEmpty((string)value))
		{
			result = ((string)value).ToUpper();
		}
		return result;
	}

	public object ConvertBack(object value, Type targetType, object parameter, string language)
	{
		throw new NotImplementedException();
	}
}
