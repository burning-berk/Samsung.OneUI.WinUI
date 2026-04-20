using System;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;

namespace Samsung.OneUI.WinUI.Converters;

internal class ScrollModeToBoolConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, string language)
	{
		bool flag = false;
		if (value is ScrollMode scrollMode)
		{
			flag = ScrollMode.Disabled.Equals(scrollMode);
		}
		return flag;
	}

	public object ConvertBack(object value, Type targetType, object parameter, string language)
	{
		throw new NotImplementedException();
	}
}
