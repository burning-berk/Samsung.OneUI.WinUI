using System;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;

namespace Samsung.OneUI.WinUI.Controls;

[Obsolete("IToastService is deprecated, please use ISnackBarService instead.")]
public interface IToastService
{
	[Obsolete("ShowToast is deprecated, please use Show instead.")]
	void ShowToast(string message, ToastDuration duration, XamlRoot xamlRoot, ToastOptions options = null);

	[Obsolete("ShowToastAsync is deprecated, please use Show instead.")]
	Task ShowToastAsync(string message, ToastDuration duration, XamlRoot xamlRoot, ToastOptions options = null);

	void Show(string message, ToastDuration duration, XamlRoot xamlRoot, ToastOptions options = null);

	Task ShowAsync(string message, ToastDuration duration, XamlRoot xamlRoot, ToastOptions options = null);
}
