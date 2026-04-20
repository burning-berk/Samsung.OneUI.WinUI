using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

internal class DropdownListViewService
{
	private const int ROUNDED_CORNER_RADIUS = 16;

	private readonly DropdownList _dropdownList;

	private readonly ListView _listView;

	private bool _isShowListTitle;

	internal DropdownListViewService(DropdownList dropdownList, ListView listView)
	{
		_dropdownList = dropdownList;
		_listView = listView;
	}

	internal void UpdateIsEnabledProperty()
	{
		if (!(_listView == null))
		{
			_listView.IsEnabled = _dropdownList.IsListEnabled;
		}
	}

	internal void UpdateSelectedItemProperty()
	{
		if (_listView != null && _listView.Items != null)
		{
			_listView.SelectedItem = _dropdownList.SelectedItem;
		}
	}

	internal void UpdateSelectedIndexProperty()
	{
		if (_listView != null && _listView.Items != null && _listView.Items.Count > _dropdownList.SelectedIndex)
		{
			_listView.SelectedIndex = _dropdownList.SelectedIndex;
		}
	}

	internal void PopulateItemsSource()
	{
		if (_listView == null || _dropdownList.Items == null)
		{
			return;
		}
		DropdownList dropdownList = _dropdownList;
		if (dropdownList.ItemsSource == null)
		{
			IList list = (dropdownList.ItemsSource = new List<object>());
		}
		foreach (object item in _dropdownList.Items)
		{
			string itemContent = GetItemContent(item);
			if (itemContent != null)
			{
				_dropdownList.ItemsSource.Add(itemContent);
			}
		}
		UpdateListViewItemsSource();
	}

	internal void UpdateListViewItemsSource()
	{
		_listView.ItemsSource = _dropdownList.ItemsSource;
	}

	internal void LoadItemsLayout()
	{
		InvokeActionForItems(delegate(ListViewItem listViewItem)
		{
			UpdateItemCornerRadius(listViewItem);
		});
	}

	internal void InvokeActionForItems(Action<ListViewItem> action)
	{
		if (_listView != null && _listView.Items != null)
		{
			for (int i = 0; i < _listView.Items.Count; i++)
			{
				ListViewItem obj = _listView.ContainerFromIndex(i) as ListViewItem;
				action(obj);
			}
		}
	}

	internal void SetListTitleVisibility(bool isShowListTitle)
	{
		_isShowListTitle = isShowListTitle;
	}

	internal void UpdateMaxHeight(double maxHeight)
	{
		_listView.MaxHeight = maxHeight;
	}

	private void UpdateItemCornerRadius(ListViewItem listViewItem)
	{
		if (listViewItem == null)
		{
			return;
		}
		ItemsControl itemsControl = ItemsControl.ItemsControlFromItemContainer(listViewItem);
		if (itemsControl != null)
		{
			UIElement firstVisibleItem = itemsControl.GetFirstVisibleItem();
			UIElement lastVisibleUIElement = itemsControl.GetLastVisibleUIElement();
			itemsControl.IndexFromContainer(listViewItem);
			if (listViewItem == firstVisibleItem)
			{
				UpdateFirstItemCornerRadius();
			}
			else if (listViewItem == lastVisibleUIElement)
			{
				UpdateLastItemCornerRadius(listViewItem);
			}
			else
			{
				UpdateMiddleItemCornerRadius(listViewItem);
			}
		}
	}

	private void UpdateFirstItemCornerRadius()
	{
		if (_listView == null)
		{
			return;
		}
		ListViewItem listViewItem = _listView.GetFirstVisibleItem() as ListViewItem;
		if (!(listViewItem == null))
		{
			if (_listView.Items.Count == 1)
			{
				listViewItem.CornerRadius = new CornerRadius(16 * Convert.ToInt32(!_isShowListTitle), 16 * Convert.ToInt32(!_isShowListTitle), 16.0, 16.0);
			}
			else
			{
				listViewItem.CornerRadius = new CornerRadius(16 * Convert.ToInt32(!_isShowListTitle), 16 * Convert.ToInt32(!_isShowListTitle), 0.0, 0.0);
			}
		}
	}

	private void UpdateMiddleItemCornerRadius(ListViewItem listViewItem)
	{
		if (listViewItem != null)
		{
			listViewItem.CornerRadius = new CornerRadius(0.0);
		}
	}

	private void UpdateLastItemCornerRadius(ListViewItem listViewItem)
	{
		if (listViewItem != null)
		{
			listViewItem.CornerRadius = new CornerRadius(0.0, 0.0, 16.0, 16.0);
		}
	}

	private string GetItemContent(object item)
	{
		if (item is TextBlock textBlock)
		{
			return textBlock.Text;
		}
		if (item is string result)
		{
			return result;
		}
		return null;
	}
}
