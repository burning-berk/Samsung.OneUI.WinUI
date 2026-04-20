using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media.Imaging;

namespace Samsung.OneUI.WinUI.Converters;

internal class ImageToVisibilityConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, string language)
	{
		Visibility visibility = ((!(value is BitmapImage bitmapImage)) ? ((!(value is Style)) ? Visibility.Collapsed : Visibility.Visible) : ((bitmapImage.UriSource == null) ? Visibility.Collapsed : Visibility.Visible));
		return visibility;
	}

	public object ConvertBack(object value, Type targetType, object parameter, string language)
	{
		throw new NotImplementedException();
	}
}
