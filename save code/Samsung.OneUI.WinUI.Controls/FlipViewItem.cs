using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Windows.System;

namespace Samsung.OneUI.WinUI.Controls;

public class FlipViewItem : Microsoft.UI.Xaml.Controls.FlipViewItem
{
	private readonly Orientation _orientation;

	public FlipViewItem(Orientation orientation)
	{
		_orientation = orientation;
		base.DefaultStyleKey = typeof(FlipViewItem);
	}

	protected override void OnKeyDown(KeyRoutedEventArgs e)
	{
		if (IsInvalidKeyPressed(e))
		{
			e.Handled = true;
		}
		base.OnKeyDown(e);
	}

	private bool IsInvalidKeyPressed(KeyRoutedEventArgs e)
	{
		if (!IsInvalidHorizontalNavigation(e))
		{
			return IsInvalidVerticalNavigation(e);
		}
		return true;
	}

	private bool IsInvalidVerticalNavigation(KeyRoutedEventArgs e)
	{
		if (_orientation == Orientation.Vertical)
		{
			if (e.Key != VirtualKey.Left)
			{
				return e.Key == VirtualKey.Right;
			}
			return true;
		}
		return false;
	}

	private bool IsInvalidHorizontalNavigation(KeyRoutedEventArgs e)
	{
		if (_orientation == Orientation.Horizontal)
		{
			if (e.Key != VirtualKey.Up)
			{
				return e.Key == VirtualKey.Down;
			}
			return true;
		}
		return false;
	}
}
