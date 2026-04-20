using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Windows.Foundation.Collections;

namespace Samsung.OneUI.WinUI.Controls;

internal class CommandBarOverflowIconsManager
{
	private const double COLLAPSE_LABEL_MODE_PERCENTAGE = 1.1;

	private const double RIGHT_LABEL_MODE_PERCENTAGE = 1.1;

	private const int INITIAL_DISPATCHER_DELAY = 2000;

	private double? _initialButtonsWidth;

	private CommandBarDefaultLabelPosition _initialLabelPosition;

	private readonly CommandBar _commandBar;

	private readonly ItemsControl _itemsControl;

	public CommandBarOverflowIconsManager(ItemsControl itemsControl, CommandBarDefaultLabelPosition initialLabelPosition, CommandBar commandBar)
	{
		_itemsControl = itemsControl;
		_commandBar = commandBar;
		_initialLabelPosition = initialLabelPosition;
	}

	public void SetInitialLabelPosition(CommandBarDefaultLabelPosition initialLabelPosition)
	{
		_initialLabelPosition = initialLabelPosition;
	}

	public void Load()
	{
		if ((!_initialButtonsWidth.HasValue || _initialButtonsWidth == 0.0) && _itemsControl != null)
		{
			_itemsControl.SizeChanged += PrimaryItemsControl_SizeChanged;
		}
	}

	public void UpdateButtonLayout()
	{
		RefreshButtonsWidth();
		UpdateButtonsLabelPositionAndOverflow();
	}

	public void UpdateButtonsLabelPositionAndOverflow()
	{
		if (ButtonLabelIsInTheRightPosition() && ShouldChangeToCollapsedMode())
		{
			SwitchLabelPosition(CommandBarDefaultLabelPosition.Collapsed);
			StartUpdateOverflowThread();
		}
		else if (ButtonLabelIsInTheCollapsedMode() && ShouldChangeToLabelMode())
		{
			SwitchLabelPosition(CommandBarDefaultLabelPosition.Right);
			StartUpdateOverflowThread();
		}
		else
		{
			UpdateMoreOptionsOverflowItems();
		}
	}

	private void RefreshButtonsWidth()
	{
		_initialButtonsWidth = CalculateTotalButtonsWidth();
	}

	private bool ButtonLabelIsInTheCollapsedMode()
	{
		return _commandBar.DefaultLabelPosition == CommandBarDefaultLabelPosition.Collapsed;
	}

	private bool ButtonLabelIsInTheRightPosition()
	{
		return _commandBar.DefaultLabelPosition == CommandBarDefaultLabelPosition.Right;
	}

	private double CalculateTotalButtonsWidth()
	{
		double num = 0.0;
		IObservableVector<ICommandBarElement> primaryCommands = _commandBar.PrimaryCommands;
		if (primaryCommands != null)
		{
			foreach (ICommandBarElement item in primaryCommands)
			{
				num += GetButtonWidth(item);
			}
		}
		return num;
	}

	private double GetButtonWidth(ICommandBarElement item)
	{
		if (item is AppBarToggleButton || item is AppBarButton)
		{
			ButtonBase buttonBase = item as ButtonBase;
			if (Visibility.Visible.Equals(buttonBase.Visibility))
			{
				if (!double.IsNaN(buttonBase.ActualWidth) && buttonBase.ActualWidth > 0.0)
				{
					return buttonBase.ActualWidth;
				}
				if (!double.IsNaN(buttonBase.Width) && buttonBase.Width > 0.0)
				{
					return buttonBase.Width;
				}
			}
		}
		return 0.0;
	}

	private void PrimaryItemsControl_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		UpdateButtonsLabelPositionAndOverflow();
	}

	private void StartUpdateOverflowThread()
	{
		new Thread((ThreadStart)delegate
		{
			UpdateAfterChanged();
		}).Start();
	}

	private void UpdateAfterChanged()
	{
		_commandBar.DispatcherQueue.EnqueueAsync(delegate
		{
			Task.Delay(2000);
			UpdateMoreOptionsOverflowItems();
		});
	}

	private bool ShouldChangeToCollapsedMode()
	{
		RefreshButtonsWidth();
		return _initialButtonsWidth * 1.1 > _commandBar.CurrentItemsMaxWidth;
	}

	private bool ShouldChangeToLabelMode()
	{
		if (!HasOverflowItems() && _initialButtonsWidth * 1.1 < _commandBar.CurrentItemsMaxWidth)
		{
			return _initialLabelPosition == CommandBarDefaultLabelPosition.Right;
		}
		return false;
	}

	private void SwitchLabelPosition(CommandBarDefaultLabelPosition newLabelPosition)
	{
		_commandBar.DefaultLabelPosition = newLabelPosition;
	}

	private bool HasOverflowItems()
	{
		if (_commandBar == null || _commandBar.PrimaryCommands == null)
		{
			return false;
		}
		return _commandBar.PrimaryCommands.FirstOrDefault((ICommandBarElement item) => IsInOverflowList(item)) != null;
	}

	private void UpdateMoreOptionsOverflowItems()
	{
		AddItemsToOverflow();
		RemoveItemsFromOverflow();
	}

	private void AddItemsToOverflow()
	{
		if (_commandBar.PrimaryCommands == null)
		{
			return;
		}
		foreach (ICommandBarElement primaryCommand in _commandBar.PrimaryCommands)
		{
			if (primaryCommand is UIElement { Visibility: Visibility.Collapsed })
			{
				break;
			}
			if (!IsInOverflowList(primaryCommand))
			{
				if (!ShouldBeInOverflowList())
				{
					break;
				}
				ChangeItemVisibility(primaryCommand, Visibility.Collapsed);
				AddMenuFlyoutItem(primaryCommand);
			}
		}
	}

	private void RemoveItemsFromOverflow()
	{
		if (_commandBar.PrimaryCommands == null)
		{
			return;
		}
		for (int num = _commandBar.PrimaryCommands.Count - 1; num >= 0; num--)
		{
			ICommandBarElement commandBarElement = _commandBar.PrimaryCommands[num];
			if (IsInOverflowList(commandBarElement))
			{
				if (!ShouldBeInMainList(commandBarElement))
				{
					break;
				}
				ChangeItemVisibility(commandBarElement, Visibility.Visible);
				RemoveMenuFlyoutItem(commandBarElement);
			}
		}
	}

	private void ChangeItemVisibility(ICommandBarElement item, Visibility visibility)
	{
		if (item is ButtonBase buttonBase)
		{
			buttonBase.Visibility = visibility;
		}
	}

	private bool ShouldBeInOverflowList()
	{
		if (_commandBar.DefaultLabelPosition == CommandBarDefaultLabelPosition.Right)
		{
			return false;
		}
		double widthVisibleItems = CalculateTotalVisibleButtonsWidth();
		return IsInAddToOverflowConditions(widthVisibleItems);
	}

	private bool ShouldBeInMainList(ICommandBarElement item)
	{
		if (_commandBar.DefaultLabelPosition == CommandBarDefaultLabelPosition.Right)
		{
			return true;
		}
		return IsInRemoveFromOverflowConditions(CalculateTotalVisibleButtonsWidth(), GetButtonWidth(item));
	}

	private bool IsInAddToOverflowConditions(double widthVisibleItems)
	{
		return widthVisibleItems > _commandBar.CurrentItemsMaxWidth;
	}

	private bool IsInRemoveFromOverflowConditions(double widthVisibleItems, double widthNextItem)
	{
		return widthVisibleItems + widthNextItem < _commandBar.CurrentItemsMaxWidth;
	}

	public double CalculateTotalVisibleButtonsWidth()
	{
		double num = 0.0;
		if (_commandBar.PrimaryCommands == null)
		{
			return num;
		}
		foreach (ICommandBarElement primaryCommand in _commandBar.PrimaryCommands)
		{
			if (primaryCommand is ButtonBase { Visibility: Visibility.Visible })
			{
				num += GetButtonWidth(primaryCommand);
			}
		}
		return num;
	}

	private void AddMenuFlyoutItem(ICommandBarElement commandBarElement)
	{
		if (commandBarElement is ICommandBarItemOverflowable commandBarItemOverflowable)
		{
			ListFlyoutItem item = new ListFlyoutItem(commandBarItemOverflowable)
			{
				Text = commandBarItemOverflowable.GetButtonLabel()
			};
			_commandBar.MoreOptionsOverflowItems.Add(item);
		}
	}

	private ListFlyoutItem GetItemInOverflowList(ICommandBarElement commandBarElement)
	{
		if (!(commandBarElement is ICommandBarItemOverflowable commandBarItemOverflowable))
		{
			return null;
		}
		foreach (MenuFlyoutItemBase moreOptionsOverflowItem in _commandBar.MoreOptionsOverflowItems)
		{
			if (moreOptionsOverflowItem is ListFlyoutItem listFlyoutItem && listFlyoutItem.CommandBarItemOverflowable == commandBarItemOverflowable)
			{
				return listFlyoutItem;
			}
		}
		return null;
	}

	private void RemoveMenuFlyoutItem(ICommandBarElement commandBarElement)
	{
		ListFlyoutItem itemInOverflowList = GetItemInOverflowList(commandBarElement);
		if (itemInOverflowList != null)
		{
			_commandBar.MoreOptionsOverflowItems.Remove(itemInOverflowList);
		}
	}

	private bool IsInOverflowList(ICommandBarElement commandBarElement)
	{
		return GetItemInOverflowList(commandBarElement) != null;
	}
}
