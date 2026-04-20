using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using Samsung.OneUI.WinUI.Controls;

namespace Samsung.OneUI.WinUI.Converters;

internal class ChipsItemIconStyleConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, string language)
	{
		if (value is ChipsItem chipsItem)
		{
			if (chipsItem.Label == ChipsItemTemplate.Tag)
			{
				return Application.Current.Resources["ChipsTagIcon"];
			}
			return chipsItem.IconSvgStyle;
		}
		return null;
	}

	public object ConvertBack(object value, Type targetType, object parameter, string language)
	{
		throw new NotImplementedException();
	}
}
