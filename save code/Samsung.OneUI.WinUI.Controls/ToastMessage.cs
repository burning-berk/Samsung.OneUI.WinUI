using System;
using Microsoft.UI.Xaml;

namespace Samsung.OneUI.WinUI.Controls;

[Obsolete("ToastMessage is deprecated, please use SnackBarMessage instead.")]
public class ToastMessage
{
	public string Message;

	public ToastDuration Duration;

	public FrameworkElement Target;
}
