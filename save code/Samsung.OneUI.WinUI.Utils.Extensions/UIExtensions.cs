using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace Samsung.OneUI.WinUI.Utils.Extensions;

public static class UIExtensions
{
	public static ScrollViewer GetScrollViewer(this DependencyObject element)
	{
		if (element is ScrollViewer result)
		{
			return result;
		}
		for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
		{
			ScrollViewer scrollViewer = VisualTreeHelper.GetChild(element, i).GetScrollViewer();
			if (!(scrollViewer == null))
			{
				return scrollViewer;
			}
		}
		return null;
	}

	public static ToolTip GetToolTip(this UIElement grid)
	{
		if (grid == null)
		{
			return null;
		}
		return ToolTipService.GetToolTip(grid) as ToolTip;
	}

	public static void CloseToolTip(this ToolTip tooltip)
	{
		if (!(tooltip == null))
		{
			tooltip.IsOpen = false;
		}
	}
}
