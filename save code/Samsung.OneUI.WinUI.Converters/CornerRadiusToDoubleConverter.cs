using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace Samsung.OneUI.WinUI.Converters;

internal class CornerRadiusToDoubleConverter : IValueConverter
{
	public ICornerRadiusRoundingStrategyConvertion ConvertionRoundingStrategy { get; set; }

	public object Convert(object value, Type targetType, object parameter, string language)
	{
		if (value is CornerRadius cornerRadius)
		{
			if (ConvertionRoundingStrategy == null)
			{
				ICornerRadiusRoundingStrategyConvertion cornerRadiusRoundingStrategyConvertion = (ConvertionRoundingStrategy = new MaxNumberCornerRadiusRoundingStrategy());
			}
			return ConvertionRoundingStrategy.Convert(cornerRadius);
		}
		return value;
	}

	public object ConvertBack(object value, Type targetType, object parameter, string language)
	{
		throw new NotImplementedException();
	}
}
