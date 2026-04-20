using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Controls;

internal interface IMenuFlyoutItemsService
{
	IEnumerable<MenuFlyoutItemBase> ListItemDivider(IEnumerable<MenuFlyoutItemBase> items);

	void ConfigureItems(IEnumerable<MenuFlyoutItemBase> items);

	void SetIsMultilineItem(IEnumerable<MenuFlyoutItemBase> items, bool isMultilineItem);

	void AddActionInItems(IEnumerable<MenuFlyoutItemBase> items, Action<MenuFlyoutItemBase> action);

	List<T> FindAllMenuFlyoutItemsFromType<T>(IList<MenuFlyoutItemBase> items);

	MenuFlyoutSubItem FindSubMenuParent(MenuFlyoutItemBase submenu, IList<MenuFlyoutItemBase> items);

	void UpdateSubMenuMargin(MenuFlyoutPresenter flyoutPresenter, MenuFlyoutItemBase submenu, MenuFlyoutSubItem parent, double subMenuOverlappingMargin, UIElement content);
}
