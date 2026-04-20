using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using Samsung.OneUI.WinUI.Utils.Extensions;

namespace Samsung.OneUI.WinUI.Converters;

internal class CornerRadiusCornersConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, string language)
	{
		if (value is CornerRadius cornerRadius && parameter is string corner)
		{
			return cornerRadius.SetCorners(corner);
		}
		return value;
	}

	public object ConvertBack(object value, Type targetType, object parameter, string language)
	{
		throw new NotImplementedException();
	}
}
