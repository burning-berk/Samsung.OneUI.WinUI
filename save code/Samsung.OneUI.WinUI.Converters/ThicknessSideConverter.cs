using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace Samsung.OneUI.WinUI.Converters;

internal class ThicknessSideConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, string language)
	{
		if (value is Thickness thickness && parameter is string text)
		{
			double left = 0.0;
			double top = 0.0;
			double right = 0.0;
			double bottom = 0.0;
			string[] array = text.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
			for (int i = 0; i < array.Length; i++)
			{
				switch (array[i].ToLower())
				{
				case "left":
					left = thickness.Left;
					break;
				case "top":
					top = thickness.Top;
					break;
				case "right":
					right = thickness.Right;
					break;
				case "bottom":
					bottom = thickness.Bottom;
					break;
				}
			}
			return new Thickness(left, top, right, bottom);
		}
		return value;
	}

	public object ConvertBack(object value, Type targetType, object parameter, string language)
	{
		throw new NotImplementedException();
	}
}
