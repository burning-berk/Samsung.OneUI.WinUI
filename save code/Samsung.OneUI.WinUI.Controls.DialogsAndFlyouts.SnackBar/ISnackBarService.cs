using System;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;

namespace Samsung.OneUI.WinUI.Controls.DialogsAndFlyouts.SnackBar;

public interface ISnackBarService
{
	void Show(string message, string buttonText, bool isShowButton, SnackBarDuration duration, XamlRoot xamlRoot, SnackBarOptions options = null, EventHandler<RoutedEventArgs> SnackBarButton_Clicked = null);

	Task ShowAsync(string message, string buttonText, bool isShowButton, SnackBarDuration duration, XamlRoot xamlRoot, SnackBarOptions options = null, EventHandler<RoutedEventArgs> SnackBarButton_Clicked = null);
}
