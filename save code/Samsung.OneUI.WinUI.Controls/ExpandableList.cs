using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Samsung.OneUI.WinUI.Services.Animation;
using Samsung.OneUI.WinUI.Utils.Extensions;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.System;

namespace Samsung.OneUI.WinUI.Controls;

public class ExpandableList : TreeView
{
	private const string PART_ICON = "PART_Icon";

	private const int DEFAULT_TOTAL_ITEMS = -1;

	private const string PART_DIVIDER = "PART_Divider";

	private const string EXPANDABLE_LIST_BORDER = "ExpandableListBorder";

	private int _totalItems;

	private readonly IExpandableListAnimationService _animationService;

	private Border _expandableListBorder;

	public ExpandableList()
	{
		base.DefaultStyleKey = typeof(ExpandableList);
		base.GotFocus += ExpandableList_GotFocus;
		base.Loaded += ExpandableTreeView_Loaded;
		_totalItems = -1;
		if (_animationService == null)
		{
			_animationService = new ExpandableListAnimationService();
		}
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		TreeViewList treeViewList = UIExtensionsInternal.FindFirstChildOfType<TreeViewList>(this);
		if (treeViewList != null)
		{
			treeViewList.ContainerContentChanging += ExpandableTreeViewList_ContainerContentChanging;
		}
		_expandableListBorder = GetTemplateChild("ExpandableListBorder") as Border;
	}

	protected override void OnPreviewKeyDown(KeyRoutedEventArgs e)
	{
		if (IsInvalidNavigation(e))
		{
			e.Handled = true;
		}
		base.OnPreviewKeyDown(e);
	}

	private void ExpandableList_GotFocus(object sender, RoutedEventArgs e)
	{
		UpdateFocusVisualState(e.OriginalSource);
	}

	private void ExpandableTreeView_Loaded(object sender, RoutedEventArgs e)
	{
		if (base.ItemsSource is IEnumerable source)
		{
			_totalItems = source.Cast<object>().Count();
			ConfigureHeaderItemsForTabNavigation();
		}
		ChangeVisibilityControl(_totalItems);
		base.SizeChanged += ExpandableList_SizeChanged;
	}

	private void ExpandableList_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		if (_totalItems == -1 || !(base.ItemsSource is IEnumerable<object> source))
		{
			return;
		}
		int num = source.Count();
		if (_totalItems != num)
		{
			ExpandableListCollectionChanged();
			_totalItems = num;
		}
		for (int i = 0; i < source.Count(); i++)
		{
			object item = source.ElementAtOrDefault(i);
			bool flag = i == source.Count() - 1;
			ExpandableListItemHeader expandableListItemHeader = ContainerFromItem(item) as ExpandableListItemHeader;
			if ((object)expandableListItemHeader != null && flag)
			{
				if (expandableListItemHeader.IsExpanded)
				{
					SetLastItemDividerOpacity(flag, expandableListItemHeader, 1.0);
				}
				else
				{
					SetLastItemDividerOpacity(flag, expandableListItemHeader, 0.0);
				}
			}
		}
		ChangeVisibilityControl(_totalItems);
	}

	private void ChangeVisibilityControl(int totalItems)
	{
		if (!(_expandableListBorder == null))
		{
			_expandableListBorder.Visibility = ((totalItems == 0 || base.ItemsSource == null) ? Visibility.Collapsed : Visibility.Visible);
			UpdateLayout();
		}
	}

	private void ExpandableTreeViewList_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
	{
		SelectorItem itemContainer = args.ItemContainer;
		if (!(itemContainer is ExpandableListItemHeader) && itemContainer.Content is FrameworkElement frameworkElement && !args.InRecycleQueue)
		{
			_animationService.OpenAnimation(frameworkElement);
		}
	}

	private void UpdateFocusVisualState(object sender)
	{
		if (!(sender is ExpandableListItemHeader) && sender is TreeViewItem treeViewItem && (object)treeViewItem != null && treeViewItem.FocusState == FocusState.Keyboard)
		{
			treeViewItem.SetFocus();
		}
	}

	private void ExpandableListCollectionChanged()
	{
		UpdateLayout();
		ConfigureHeaderItemsForTabNavigation();
	}

	private void ConfigureHeaderItemsForTabNavigation()
	{
		if (!(base.ItemsSource is IEnumerable<object> source))
		{
			return;
		}
		int countLevel1Items = source.Count();
		for (int i = 0; i < source.Count(); i++)
		{
			object item = source.ElementAtOrDefault(i);
			bool isTheLastItem = i == source.Count() - 1;
			if (ContainerFromItem(item) is ExpandableListItemHeader header)
			{
				SetIconAutomationProperties(header, 1 + i, countLevel1Items);
				SetLastItemDividerOpacity(isTheLastItem, header, 0.0);
			}
		}
	}

	private static void SetLastItemDividerOpacity(bool isTheLastItem, ExpandableListItemHeader header, double lastItemOpacity)
	{
		Divider divider = UIExtensionsInternal.FindChildByName<Divider>("PART_Divider", header);
		if (!(divider == null))
		{
			divider.Opacity = (isTheLastItem ? lastItemOpacity : 1.0);
		}
	}

	private void SetIconAutomationProperties(ExpandableListItemHeader header, int position, int countLevel1Items)
	{
		Button button = UIExtensionsInternal.FindChildByName<Button>("PART_Icon", header);
		if (button != null && header != null)
		{
			string name = AutomationProperties.GetName(header);
			AutomationProperties.SetName(button, name);
			AutomationProperties.SetLevel(button, 1);
			AutomationProperties.SetPositionInSet(button, position);
			AutomationProperties.SetSizeOfSet(button, countLevel1Items);
		}
	}

	private bool IsInvalidNavigation(KeyRoutedEventArgs e)
	{
		if (e.Key != VirtualKey.Left)
		{
			return e.Key == VirtualKey.Right;
		}
		return true;
	}
}
