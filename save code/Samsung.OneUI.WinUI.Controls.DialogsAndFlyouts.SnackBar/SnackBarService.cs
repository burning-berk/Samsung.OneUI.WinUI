using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Samsung.OneUI.WinUI.Common.Exceptions;

namespace Samsung.OneUI.WinUI.Controls.DialogsAndFlyouts.SnackBar;

public class SnackBarService : ISnackBarService
{
	private readonly Dictionary<NullObject<FrameworkElement>, SnackBar> _snackBarByComponent = new Dictionary<NullObject<FrameworkElement>, SnackBar>();

	private static readonly Lazy<ISnackBarService> _instance = new Lazy<ISnackBarService>(() => new SnackBarService());

	public static ISnackBarService Instance => _instance.Value;

	private void SnackBarValue_CompletedEvent(object sender, NullObject<FrameworkElement> e)
	{
		if (_snackBarByComponent.ContainsKey(e))
		{
			_snackBarByComponent.Remove(e);
		}
	}

	public void Show(string message, string buttonText, bool isShowButton, SnackBarDuration duration, XamlRoot xamlRoot, SnackBarOptions options = null, EventHandler<RoutedEventArgs> SnackBarButton_Clicked = null)
	{
		FrameworkElement frameworkElement = options?.Target;
		if (!TryGetOpenedSnackBar(frameworkElement, out var snackBar))
		{
			snackBar = new SnackBar();
			snackBar.CompletedEvent += SnackBarValue_CompletedEvent;
			snackBar.Click += SnackBarButton_Clicked;
			_snackBarByComponent.Add(frameworkElement, snackBar);
		}
		snackBar.XamlRoot = xamlRoot;
		ThrowsXamlRootExceptionIfNull(snackBar.XamlRoot);
		double textWidth = GetTextWidth(options);
		snackBar.AdjustSizeAndTrimming(textWidth);
		snackBar.UpdateOffSetAppTitleBar(options?.VerticalAppTitleBarOffSet ?? 0.0);
		snackBar.UpdateVerticalOffSet(options?.VerticalOffSet ?? 0.0);
		snackBar.Show(message, buttonText, isShowButton, duration, frameworkElement);
	}

	public Task ShowAsync(string message, string buttonText, bool isShowButton, SnackBarDuration duration, XamlRoot xamlRoot, SnackBarOptions options = null, EventHandler<RoutedEventArgs> SnackBarButton_Clicked = null)
	{
		FrameworkElement frameworkElement = options?.Target;
		TaskCompletionSource taskCompletionSource = new TaskCompletionSource();
		if (!TryGetOpenedSnackBar(frameworkElement, out var snackBar))
		{
			snackBar = new SnackBar();
			snackBar.CompletedEvent += delegate(object? sender, NullObject<FrameworkElement> e)
			{
				SnackBarValue_CompletedEvent(sender, e);
				taskCompletionSource.SetResult();
			};
			snackBar.Click += SnackBarButton_Clicked;
			_snackBarByComponent.Add(frameworkElement, snackBar);
		}
		snackBar.XamlRoot = xamlRoot;
		ThrowsXamlRootExceptionIfNull(snackBar.XamlRoot);
		double textWidth = GetTextWidth(options);
		snackBar.AdjustSizeAndTrimming(textWidth);
		snackBar.UpdateOffSetAppTitleBar(options?.VerticalAppTitleBarOffSet ?? 0.0);
		snackBar.UpdateVerticalOffSet(options?.VerticalOffSet ?? 0.0);
		snackBar.Show(message, buttonText, isShowButton, duration, frameworkElement);
		return taskCompletionSource.Task;
	}

	private bool TryGetOpenedSnackBar(FrameworkElement target, out SnackBar snackBar)
	{
		SnackBar value;
		bool result = _snackBarByComponent.TryGetValue(target, out value);
		snackBar = value;
		return result;
	}

	private double GetTextWidth(SnackBarOptions options = null)
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
