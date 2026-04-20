using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;

namespace Samsung.OneUI.WinUI.Controls;

internal class DropdownScrollViewerService
{
	private const int TOP_MARGIN_WHEN_OUT_OF_BOUNDS = 6;

	private const int DEFAULT_MARGIN_WHEN_OUT_OF_BOUNDS = 10;

	private readonly ScrollViewer _dropdownVerticalScrollViewer;

	private readonly RelativePanel _dropdownListRelativePanel;

	internal DropdownScrollViewerService(ScrollViewer dropdownVerticalScrollViewer, RelativePanel dropdownListRelativePanel)
	{
		_dropdownVerticalScrollViewer = dropdownVerticalScrollViewer;
		_dropdownListRelativePanel = dropdownListRelativePanel;
	}

	internal void UpdateMarginWhenOutOfBounds(Rect dropdownScreenPosition, DropdownList dropdownList, Popup popup)
	{
		double num = Math.Min(_dropdownVerticalScrollViewer.ActualHeight, _dropdownVerticalScrollViewer.MaxHeight);
		if (IsLeftOutOfBounds(dropdownScreenPosition.X))
		{
			popup.HorizontalOffset = (double)dropdownList.HorizontalOffset - dropdownScreenPosition.X + 10.0;
		}
		else if (IsRightOutOfBounds(dropdownScreenPosition.X))
		{
			popup.HorizontalOffset = (double)dropdownList.HorizontalOffset - (dropdownScreenPosition.X + _dropdownVerticalScrollViewer.ActualWidth + 10.0 - _dropdownListRelativePanel.XamlRoot.Size.Width);
		}
		if (IsTopOutOfBounds(dropdownScreenPosition.Y))
		{
			popup.VerticalOffset = (double)dropdownList.VerticalOffset - dropdownScreenPosition.Y + 6.0;
		}
		else if (IsBottomOutOfBounds(dropdownScreenPosition.Y, num))
		{
			popup.VerticalOffset = (double)dropdownList.VerticalOffset - (dropdownScreenPosition.Y + num + 10.0 - _dropdownListRelativePanel.XamlRoot.Size.Height);
		}
	}

	internal void UpdateHeights(DropdownListViewService dropdownListViewService, int verticalOffset = 0)
	{
		if (_dropdownListRelativePanel.XamlRoot != null)
		{
			_dropdownVerticalScrollViewer.MaxHeight = _dropdownListRelativePanel.XamlRoot.Size.Height - 6.0 - 10.0 - (double)verticalOffset;
			_dropdownVerticalScrollViewer.Height = Math.Min(_dropdownVerticalScrollViewer.ActualHeight, _dropdownVerticalScrollViewer.MaxHeight);
			dropdownListViewService?.UpdateMaxHeight(_dropdownVerticalScrollViewer.MaxHeight);
		}
	}

	internal Rect GetRectMaskBasedOnPosition()
	{
		FrameworkElement dropdownVerticalScrollViewer = _dropdownVerticalScrollViewer;
		GeneralTransform generalTransform = dropdownVerticalScrollViewer.TransformToVisual(null);
		Point point = generalTransform.TransformPoint(new Point(0f, 0f));
		Point point2 = generalTransform.TransformPoint(new Point(dropdownVerticalScrollViewer.ActualWidth, dropdownVerticalScrollViewer.ActualHeight));
		return new Rect(point, point2);
	}

	internal void ResetHeights()
	{
		if (_dropdownVerticalScrollViewer != null)
		{
			_dropdownVerticalScrollViewer.Height = double.NaN;
			_dropdownVerticalScrollViewer.MaxHeight = double.PositiveInfinity;
		}
	}

	private bool IsLeftOutOfBounds(double horizontalPosition)
	{
		return horizontalPosition - 10.0 < 10.0;
	}

	private bool IsRightOutOfBounds(double horizontalPosition)
	{
		bool result = false;
		if (_dropdownListRelativePanel.XamlRoot != null)
		{
			result = horizontalPosition + _dropdownVerticalScrollViewer.ActualWidth + 10.0 >= _dropdownListRelativePanel.XamlRoot.Size.Width - 10.0;
		}
		return result;
	}

	private static bool IsTopOutOfBounds(double verticalPosition)
	{
		return verticalPosition - 6.0 < 6.0;
	}

	private bool IsBottomOutOfBounds(double verticalPosition, double height)
	{
		bool result = false;
		if (_dropdownListRelativePanel.XamlRoot != null)
		{
			result = verticalPosition + height + 10.0 >= _dropdownListRelativePanel.XamlRoot.Size.Height - 10.0;
		}
		return result;
	}
}
