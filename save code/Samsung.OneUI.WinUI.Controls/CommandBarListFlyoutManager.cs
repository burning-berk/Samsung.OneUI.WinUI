using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

internal class CommandBarListFlyoutManager
{
	private const string DEFAULT_MORE_OPTIONS_CONTENT = "IDS_PENUP_OPT_MORE_OPTIONS/Content";

	private const double TOOLTIP_VERTICAL_OFFSET = 10.0;

	private readonly Button _backButton;

	private readonly CommandBar _commandBar;

	private readonly Button _moreOptionsButton;

	private readonly ListFlyout _listFlyoutMoreOptions;

	private readonly ToolTip _moreOptionsToolTip = new ToolTip
	{
		IsTabStop = false,
		VerticalOffset = 10.0
	};

	public CommandBarListFlyoutManager(ListFlyout listFlyoutMoreOptions, Button moreOptionsButton, Button backButton, CommandBar commandBar)
	{
		_commandBar = commandBar;
		_commandBar.SizeChanged += CommandBar_SizeChanged;
		_backButton = backButton;
		_moreOptionsButton = moreOptionsButton;
		if (_moreOptionsButton != null)
		{
			_moreOptionsButton.Click += MoreOptionsButton_Click;
			SetAutomationPropertiesName();
		}
		_listFlyoutMoreOptions = listFlyoutMoreOptions;
		if (_listFlyoutMoreOptions != null)
		{
			_listFlyoutMoreOptions.Closed += ListFlyoutMoreOptions_Closed;
		}
	}

	public void ChangeListflyoutPosition()
	{
		if (!(_listFlyoutMoreOptions == null))
		{
			_listFlyoutMoreOptions.HorizontalOffset = _commandBar.MoreOptionsHorizontalOffset;
			_listFlyoutMoreOptions.VerticalOffset = _commandBar.MoreOptionsVerticalOffset;
		}
	}

	public void Load()
	{
		AssignEvents();
		ChangeListflyoutPosition();
		UpdateMoreOptionsToolTipContent();
		_listFlyoutMoreOptions.MoreOptionsTabRequested += MoreOptionsTabRequestedAsync;
		AddMoreOptionItems(_listFlyoutMoreOptions, _commandBar.MoreOptionsItems, _commandBar.MoreOptionsOverflowItems);
		UpdateMoreOptionsToolTip();
	}

	public void UpdateMoreOptionsToolTipContent()
	{
		if (string.IsNullOrEmpty(_commandBar.MoreOptionsToolTipContent))
		{
			_moreOptionsToolTip.Content = "IDS_PENUP_OPT_MORE_OPTIONS/Content".GetLocalized();
		}
		else
		{
			_moreOptionsToolTip.Content = _commandBar.MoreOptionsToolTipContent;
		}
	}

	public void OnMoreOptionItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		AddMoreOptionItems(_listFlyoutMoreOptions, _commandBar.MoreOptionsItems, _commandBar.MoreOptionsOverflowItems);
	}

	private void SetAutomationPropertiesName()
	{
		_moreOptionsButton.SetValue(AutomationProperties.NameProperty, "IDS_PENUP_OPT_MORE_OPTIONS/Content".GetLocalized());
	}

	private void MoreOptionsButton_Click(object sender, RoutedEventArgs e)
	{
		UpdateMoreOptionsToolTip();
	}

	private void ListFlyoutMoreOptions_Closed(object sender, object e)
	{
		UpdateMoreOptionsToolTip();
	}

	private async void MoreOptionsTabRequestedAsync(object sender, EventArgs e)
	{
		if (!(_backButton == null))
		{
			await FocusManager.TryFocusAsync(_backButton, FocusState.Keyboard);
		}
	}

	private void UpdateMoreOptionsToolTip()
	{
		if (_moreOptionsButton != null)
		{
			SetAutomationPropertiesName();
			ToolTipService.SetToolTip(_moreOptionsButton, null);
			UpdateMoreOptionsToolTipContent();
			ToolTipService.SetToolTip(_moreOptionsButton, _moreOptionsToolTip);
		}
	}

	private void AddMoreOptionItems(ListFlyout listFlyout, ObservableCollection<MenuFlyoutItemBase> items, ObservableCollection<MenuFlyoutItemBase> overflowItems)
	{
		if (listFlyout != null)
		{
			listFlyout.Items.Clear();
			AddItemsToTheFlyoutList(listFlyout, items);
			AddItemsToTheFlyoutList(listFlyout, overflowItems);
		}
	}

	private void AddMoreOptionItemsChanged(ListFlyout listFlyout, ObservableCollection<MenuFlyoutItemBase> items, ObservableCollection<MenuFlyoutItemBase> overflowItems)
	{
		if (listFlyout != null)
		{
			RemoveItemsOutsideList(listFlyout, overflowItems);
			AddItemsToTheFlyoutList(listFlyout, items);
		}
	}

	private void AddMoreOptionItemsOverflowChanged(ListFlyout listFlyout, ObservableCollection<MenuFlyoutItemBase> items, ObservableCollection<MenuFlyoutItemBase> overflowItems)
	{
		if (listFlyout != null)
		{
			RemoveItemsOutsideList(listFlyout, items);
			AddItemsToTheFlyoutList(listFlyout, overflowItems);
		}
	}

	private void AddItemsToTheFlyoutList(ListFlyout listFlyout, ObservableCollection<MenuFlyoutItemBase> overflowItems)
	{
		foreach (MenuFlyoutItemBase overflowItem in overflowItems)
		{
			listFlyout.Items.Add(overflowItem);
		}
	}

	private void RemoveItemsOutsideList(ListFlyout listFlyout, ObservableCollection<MenuFlyoutItemBase> items)
	{
		List<MenuFlyoutItemBase> list = new List<MenuFlyoutItemBase>();
		foreach (MenuFlyoutItemBase item in listFlyout.Items)
		{
			if (!items.Contains(item))
			{
				list.Add(item);
			}
		}
		foreach (MenuFlyoutItemBase item2 in list)
		{
			listFlyout.Items.Remove(item2);
		}
	}

	private void AssignEvents()
	{
		_commandBar.MoreOptionsItems.CollectionChanged += OnMoreOptionItemsChanged;
		_commandBar.MoreOptionsOverflowItems.CollectionChanged += OnMoreOptionOverflowItemsChanged;
	}

	private void OnMoreOptionOverflowItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		AddMoreOptionItemsOverflowChanged(_listFlyoutMoreOptions, _commandBar.MoreOptionsItems, _commandBar.MoreOptionsOverflowItems);
	}

	private void CommandBar_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		UpdateMoreOptionsToolTip();
	}
}
