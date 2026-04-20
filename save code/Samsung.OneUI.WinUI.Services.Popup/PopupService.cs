using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.Foundation;

namespace Samsung.OneUI.WinUI.Services.Popup;

internal class PopupService : IPopupService
{
	public void CloseAllPopups(XamlRoot xamlRoot, Func<Microsoft.UI.Xaml.Controls.Primitives.Popup, bool> predicate = null)
	{
		foreach (Microsoft.UI.Xaml.Controls.Primitives.Popup openPopup in GetOpenPopups(xamlRoot, predicate))
		{
			openPopup.IsOpen = false;
		}
	}

	public void CloseSubMenuPopups(XamlRoot xamlRoot, Func<Microsoft.UI.Xaml.Controls.Primitives.Popup, bool> predicate = null)
	{
		List<Microsoft.UI.Xaml.Controls.Primitives.Popup> openPopups = GetOpenPopups(xamlRoot, predicate);
		for (int i = 0; i < openPopups.Count; i++)
		{
			if (openPopups.Count - 1 > i)
			{
				openPopups[i].IsOpen = false;
			}
		}
	}

	public void PreventBackdropPupups(XamlRoot xamlRoot, bool isBlur = false, Func<Microsoft.UI.Xaml.Controls.Primitives.Popup, bool> predicate = null)
	{
		foreach (Microsoft.UI.Xaml.Controls.Primitives.Popup openPopup in GetOpenPopups(xamlRoot, predicate))
		{
			openPopup.SystemBackdrop = (isBlur ? new DesktopAcrylicBackdrop() : null);
		}
	}

	public List<Microsoft.UI.Xaml.Controls.Primitives.Popup> GetOpenPopups(XamlRoot xamlRoot, Func<Microsoft.UI.Xaml.Controls.Primitives.Popup, bool> predicate = null)
	{
		if (predicate == null)
		{
			return VisualTreeHelper.GetOpenPopupsForXamlRoot(xamlRoot).ToList();
		}
		return VisualTreeHelper.GetOpenPopupsForXamlRoot(xamlRoot).Where(predicate).ToList();
	}

	public void SetPopupOffsets(FrameworkElement contentPresenter, FrameworkElement target, Point offset)
	{
		Microsoft.UI.Xaml.Controls.Primitives.Popup popup = contentPresenter?.Parent as Microsoft.UI.Xaml.Controls.Primitives.Popup;
		if (!(popup == null) && !(target == null) && !(target.XamlRoot == null))
		{
			Point contentPosition = target.TransformToVisual(target.XamlRoot.Content).TransformPoint(new Point(0f, 0f));
			ContentPresenter contentPresenter2 = UIExtensionsInternal.FindFirstChildOfType<ContentPresenter>(contentPresenter);
			if ((object)contentPresenter2 != null && contentPresenter2.RenderTransform is CompositeTransform compositeTransform)
			{
				compositeTransform.CenterX = contentPosition.X + offset.X;
				compositeTransform.CenterY = contentPosition.Y + offset.Y;
			}
			if (offset.X != 0.0)
			{
				popup.HorizontalOffset = contentPosition.X + offset.X;
			}
			else
			{
				popup.HorizontalOffset = contentPosition.X;
			}
			popup.VerticalOffset = contentPosition.Y + offset.Y;
			UpdateOffsetOutOfBounds(popup, contentPosition, offset);
		}
	}

	private void UpdateOffsetOutOfBounds(Microsoft.UI.Xaml.Controls.Primitives.Popup popup, Point contentPosition, Point offset)
	{
		FrameworkElement frameworkElement = popup.Child as FrameworkElement;
		double num = contentPosition.X + frameworkElement.ActualWidth + offset.X + frameworkElement.Margin.Left;
		if (IsLeftOutOfBounds(popup))
		{
			popup.HorizontalOffset = Math.Abs(frameworkElement.Margin.Left);
		}
		else if (IsRightOutOfBounds(frameworkElement, num))
		{
			popup.HorizontalOffset += frameworkElement.XamlRoot.Size.Width - num;
		}
		double num2 = contentPosition.Y + frameworkElement.ActualHeight + offset.Y + frameworkElement.Margin.Top;
		if (IsTopOutOfBounds(popup))
		{
			popup.VerticalOffset = Math.Abs(frameworkElement.Margin.Top);
		}
		else if (IsBottomOutOfBounds(frameworkElement, num2))
		{
			popup.VerticalOffset += frameworkElement.XamlRoot.Size.Height - num2;
		}
	}

	private bool IsLeftOutOfBounds(Microsoft.UI.Xaml.Controls.Primitives.Popup popup)
	{
		return popup.HorizontalOffset <= 0.0;
	}

	private bool IsTopOutOfBounds(Microsoft.UI.Xaml.Controls.Primitives.Popup popup)
	{
		return popup.VerticalOffset <= 0.0;
	}

	private bool IsRightOutOfBounds(FrameworkElement contentPresenter, double horizontalPosition)
	{
		if (contentPresenter.XamlRoot == null)
		{
			return false;
		}
		return horizontalPosition > contentPresenter.XamlRoot.Size.Width;
	}

	private bool IsBottomOutOfBounds(FrameworkElement contentPresenter, double verticalPosition)
	{
		if (contentPresenter.XamlRoot == null)
		{
			return false;
		}
		return verticalPosition > contentPresenter.XamlRoot.Size.Height;
	}
}
