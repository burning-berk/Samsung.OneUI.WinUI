using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Samsung.OneUI.WinUI.Common.Exceptions;

namespace Samsung.OneUI.WinUI.Controls;

[Obsolete("ToastService is deprecated, please use SnackBarService instead.")]
public class ToastService : IToastService
{
	private readonly Dictionary<NullObject<FrameworkElement>, Toast> _toastByComponent = new Dictionary<NullObject<FrameworkElement>, Toast>();

	private static readonly Lazy<IToastService> _instance = new Lazy<IToastService>(() => new ToastService());

	public static IToastService Instance => _instance.Value;

	private void ToastValue_CompletedEvent(object sender, NullObject<FrameworkElement> e)
	{
		if (_toastByComponent.ContainsKey(e))
		{
			_toastByComponent.Remove(e);
		}
	}

	public void Show(string message, ToastDuration duration, XamlRoot xamlRoot, ToastOptions options = null)
	{
		FrameworkElement frameworkElement = options?.Target;
		if (!TryGetOpenedToast(frameworkElement, out var toast))
		{
			toast = new Toast();
			toast.CompletedEvent += ToastValue_CompletedEvent;
			_toastByComponent.Add(frameworkElement, toast);
		}
		toast.XamlRoot = xamlRoot;
		ThrowsXamlRootExceptionIfNull(toast.XamlRoot);
		double textWidth = GetTextWidth(options);
		toast.AdjustSizeAndTrimming(textWidth);
		toast.UpdateOffSetAppTitleBar(options?.VerticalAppTitleBarOffSet ?? 0.0);
		toast.UpdateVerticalOffSet(options?.VerticalOffSet ?? 0.0);
		toast.Show(message, duration, frameworkElement);
	}

	public Task ShowAsync(string message, ToastDuration duration, XamlRoot xamlRoot, ToastOptions options = null)
	{
		FrameworkElement frameworkElement = options?.Target;
		TaskCompletionSource taskCompletionSource = new TaskCompletionSource();
		if (!TryGetOpenedToast(frameworkElement, out var toast))
		{
			toast = new Toast();
			toast.CompletedEvent += delegate(object? sender, NullObject<FrameworkElement> e)
			{
				ToastValue_CompletedEvent(sender, e);
				taskCompletionSource.SetResult();
			};
			_toastByComponent.Add(frameworkElement, toast);
		}
		toast.XamlRoot = xamlRoot;
		ThrowsXamlRootExceptionIfNull(toast.XamlRoot);
		double textWidth = GetTextWidth(options);
		toast.AdjustSizeAndTrimming(textWidth);
		toast.UpdateOffSetAppTitleBar(options?.VerticalAppTitleBarOffSet ?? 0.0);
		toast.UpdateVerticalOffSet(options?.VerticalOffSet ?? 0.0);
		toast.Show(message, duration, frameworkElement);
		return taskCompletionSource.Task;
	}

	[Obsolete("ShowToast is deprecated, please use Show instead.")]
	public void ShowToast(string message, ToastDuration duration, XamlRoot xamlRoot, ToastOptions options = null)
	{
		Show(message, duration, xamlRoot, options);
	}

	[Obsolete("ShowToastAsync is deprecated, please use Show instead.")]
	public Task ShowToastAsync(string message, ToastDuration duration, XamlRoot xamlRoot, ToastOptions options = null)
	{
		return ShowAsync(message, duration, xamlRoot, options);
	}

	private bool TryGetOpenedToast(FrameworkElement target, out Toast toast)
	{
		Toast value;
		bool result = _toastByComponent.TryGetValue(target, out value);
		toast = value;
		return result;
	}

	private double GetTextWidth(ToastOptions options = null)
	{
		if (options != null && options.HasWidthValue())
		{
			return options.Width.Value;
		}
		return 0.0;
	}

	private void ThrowsXamlRootExceptionIfNull(XamlRoot xamlRoot)
	{
		if (xamlRoot == null)
		{
			throw new XamlRootNullReferenceException();
		}
	}
}
