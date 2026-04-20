using System;
using Microsoft.UI.Xaml.Data;
using Samsung.OneUI.WinUI.Utils.Helpers;
using WinRT;
using WinRT.Samsung_OneUI_WinUIVtableClasses;

namespace Samsung.OneUI.WinUI.Converters;

[WinRTRuntimeClassName("Microsoft.UI.Xaml.Data.IValueConverter")]
[WinRTExposedType(typeof(Samsung_OneUI_WinUI_Converters_DatePickerTextScaleSizeConverterWinRTTypeDetails))]
internal class DatePickerTextScaleSizeConverter : IValueConverter
{
	private const double TEXT_SCALE_BREAKPOINT = 1.4;

	private const int GRID_ITEM_STANDARD_WIDTH = 42;

	private const int GRID_ITEM_SCALED_WIDTH = 44;

	private const int GRID_ITEM_STANDARD_HEIGHT = 42;

	private const int GRID_ITEM_SCALED_HEIGHT = 32;

	public object Convert(object value, Type targetType, object parameter, string language)
	{
		if (parameter != null && parameter.ToString() == "Width")
		{
			return TextScaleHelper.EqualOrGreaterThan(1.4) ? 42 : 44;
		}
		if (parameter != null && parameter.ToString() == "Height")
		{
			return TextScaleHelper.EqualOrGreaterThan(1.4) ? 42 : 32;
		}
		return value;
	}

	public object ConvertBack(object value, Type targetType, object parameter, string language)
	{
		throw new NotImplementedException();
	}
}
