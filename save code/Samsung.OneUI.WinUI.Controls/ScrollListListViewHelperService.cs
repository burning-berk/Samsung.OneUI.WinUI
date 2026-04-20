using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace Samsung.OneUI.WinUI.Controls;

internal class ScrollListListViewHelperService
{
	internal void AddVisibleItemsToList(ListView listView, List<ListViewItem> visibleItems, int currentIndex)
	{
		ListViewItem listViewItem = listView.ContainerFromIndex(currentIndex) as ListViewItem;
		if (listViewItem != null && IsUserVisible(listViewItem, listView))
		{
			visibleItems.Add(listViewItem);
		}
	}

	internal double GetListViewItemOffset(FrameworkElement listViewItem, ListView listView)
	{
		double y = listViewItem.TransformToVisual(listView).TransformPoint(default(Point)).Y;
		double actualHeight = listViewItem.ActualHeight;
		double num = (listView.ActualHeight - actualHeight) / 2.0;
		return y - num;
	}

	internal int IncreaseIndexOrSetFirst(ListView listView)
	{
		if (listView.SelectedIndex < listView.Items.Count - 1)
		{
			return listView.SelectedIndex + 1;
		}
		return 0;
	}

	internal int DecreaseIndexOrSetLast(ListView listView)
	{
		if (listView.SelectedIndex <= 0)
		{
			return listView.Items.Count - 1;
		}
		return listView.SelectedIndex - 1;
	}

	private bool IsUserVisible(FrameworkElement element, FrameworkElement container)
	{
		Rect rect = element.TransformToVisual(container).TransformBounds(new Rect(0.0, 0.0, element.ActualWidth, element.ActualHeight));
		Rect rect2 = new Rect(0.0, 0.0, container.ActualWidth, container.ActualHeight);
		rect2.Intersect(rect);
		bool result = false;
		if (!rect2.IsEmpty)
		{
			result = rect2.Height != 0.0;
		}
		return result;
	}
}
