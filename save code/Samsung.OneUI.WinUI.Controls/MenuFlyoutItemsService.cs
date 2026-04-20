using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.Foundation;

namespace Samsung.OneUI.WinUI.Controls;

internal class MenuFlyoutItemsService : IMenuFlyoutItemsService
{
	private const string MENU_FLYOUT_CONTENT_ROOT_NAME = "ContentRoot";

	private const string TEXT_BLOCK_MENU_FLYOUT_NAME = "TextBlock";

	private const int CORNER_RADIUS_OUT_STROKE_VALUE = 16;

	public IEnumerable<MenuFlyoutItemBase> ListItemDivider(IEnumerable<MenuFlyoutItemBase> items)
	{
		List<MenuFlyoutItemBase> list = new List<MenuFlyoutItemBase>();
		foreach (MenuFlyoutItemBase item in items)
		{
			if (item is ListFlyoutSeparator)
			{
				list.Add(item);
			}
			else if (item is MenuFlyoutSubItem menuFlyoutSubItem)
			{
				list.AddRange(ListItemDivider(menuFlyoutSubItem.Items));
			}
		}
		return list;
	}

	public void ConfigureItems(IEnumerable<MenuFlyoutItemBase> items)
	{
		AddActionInItems(items.Where((MenuFlyoutItemBase item) => !(item is ListFlyoutSeparator)), delegate(MenuFlyoutItemBase item)
		{
			item.Loaded += Item_Loaded;
			ConfigureSubItemsLoadedEventToSub(item);
			UpdateToggleSubItemMargin(item, items);
		});
	}

	public void SetIsMultilineItem(IEnumerable<MenuFlyoutItemBase> items, bool isMultilineItem)
	{
		AddActionInItems(items, delegate(MenuFlyoutItemBase item)
		{
			TextBlock textBlock = UIExtensionsInternal.FindChildByName<TextBlock>("TextBlock", item);
			if (!(textBlock == null))
			{
				textBlock.TextWrapping = ((!isMultilineItem) ? TextWrapping.NoWrap : TextWrapping.Wrap);
			}
		});
	}

	public void AddActionInItems(IEnumerable<MenuFlyoutItemBase> items, Action<MenuFlyoutItemBase> action)
	{
		foreach (MenuFlyoutItemBase item in items)
		{
			if (item is MenuFlyoutSubItem menuFlyoutSubItem)
			{
				AddActionInItems(menuFlyoutSubItem.Items, action);
			}
			action(item);
		}
	}

	public List<T> FindAllMenuFlyoutItemsFromType<T>(IList<MenuFlyoutItemBase> items)
	{
		List<T> list = items.OfType<T>().ToList();
		foreach (MenuFlyoutItemBase item in items)
		{
			if (item is MenuFlyoutSubItem menuFlyoutSubItem && menuFlyoutSubItem.Items.Any())
			{
				list.AddRange(FindAllMenuFlyoutItemsFromType<T>(menuFlyoutSubItem.Items));
			}
		}
		return list;
	}

	public MenuFlyoutSubItem FindSubMenuParent(MenuFlyoutItemBase submenu, IList<MenuFlyoutItemBase> items)
	{
		return FindAllMenuFlyoutItemsFromType<MenuFlyoutSubItem>(items).Find((MenuFlyoutSubItem subitem) => subitem.Items.FirstOrDefault()?.Equals(submenu) ?? false);
	}

	public void UpdateSubMenuMargin(MenuFlyoutPresenter flyoutPresenter, MenuFlyoutItemBase submenu, MenuFlyoutSubItem parent, double subMenuOverlappingMargin, UIElement content)
	{
		Point point = submenu.TransformToVisual(content).TransformPoint(new Point(0f, 0f));
		Point point2 = parent.TransformToVisual(content).TransformPoint(new Point(0f, 0f));
		bool flag = point.X > point2.X;
		flyoutPresenter.Margin = new Thickness(flag ? (0.0 - subMenuOverlappingMargin) : subMenuOverlappingMargin, 0.0, 0.0, 0.0);
	}

	private void UpdateToggleSubItemMargin(MenuFlyoutItemBase item, IEnumerable<MenuFlyoutItemBase> items)
	{
		if (item is MenuFlyoutSubItem && items.Any((MenuFlyoutItemBase a) => a is ListFlyoutToggle))
		{
			Grid grid = UIExtensionsInternal.FindChildByName<Grid>("ContentRoot", item);
			if (grid != null)
			{
				grid.Margin = new Thickness(-30.0, 0.0, 0.0, 0.0);
			}
		}
	}

	private void ConfigureSubItemsLoadedEventToSub(MenuFlyoutItemBase item)
	{
		if (item is MenuFlyoutSubItem menuFlyoutSubItem)
		{
			ConfigureItems(menuFlyoutSubItem.Items);
		}
	}

	private void SetItemsCornerRadiusByOrder(MenuFlyoutItemBase item)
	{
		ItemsControl itemsControl = ItemsControl.ItemsControlFromItemContainer(item);
		if (!(itemsControl == null))
		{
			UIElement firstVisibleItem = itemsControl.GetFirstVisibleItem();
			UIElement lastVisibleUIElement = itemsControl.GetLastVisibleUIElement();
			if (item == firstVisibleItem)
			{
				SetFirstItemCornerRadius(item, itemsControl);
			}
			else if (item == lastVisibleUIElement)
			{
				item.CornerRadius = new CornerRadius(0.0, 0.0, 16.0, 16.0);
			}
			else
			{
				item.CornerRadius = new CornerRadius(0.0);
			}
		}
	}

	private void SetFirstItemCornerRadius(MenuFlyoutItemBase itemBase, ItemsControl itemsControl)
	{
		if (itemsControl.IndexFromContainer(itemBase) == itemsControl.Items.Count() - 1)
		{
			itemBase.CornerRadius = new CornerRadius(16.0);
		}
		else
		{
			itemBase.CornerRadius = new CornerRadius(16.0, 16.0, 0.0, 0.0);
		}
	}

	private void Item_Loaded(object sender, RoutedEventArgs e)
	{
		if (sender is MenuFlyoutItemBase itemsCornerRadiusByOrder)
		{
			SetItemsCornerRadiusByOrder(itemsCornerRadiusByOrder);
		}
	}
}
