using System;
using Microsoft.UI.Xaml.Data;

namespace Samsung.OneUI.WinUI.Converters;

internal class IntToEnumConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, string language)
	{
		if (value is int value2 && targetType.IsEnum)
		{
			return Enum.ToObject(targetType, value2);
		}
		return value;
	}

	public object ConvertBack(object value, Type targetType, object parameter, string language)
	{
		throw new NotImplementedException();
	}
}
