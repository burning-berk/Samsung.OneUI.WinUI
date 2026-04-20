using System.Threading.Tasks;
using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Samsung.OneUI.WinUI.Utils.Extensions;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.System;
using Windows.UI.ViewManagement;

namespace Samsung.OneUI.WinUI.Controls;

public sealed class TabItem : PivotItem
{
	private const string TAB_HEADER_ITEM_NOTIFICATION_BADGE = "TabHeaderItemNotificationBadge";

	private const double MAX_TEXT_SIZE_SCALE = 1.5;

	private const double PIVOT_HEADER_ITEM_DEFAULT_MIN_HEIGHT = 36.0;

	private const double ROOT_GRID_DEFAULT_MIN_HEIGHT = 36.0;

	private const double DEFAULT_MAX_HEIGHT = 44.0;

	private const int ITEM_HEADER_PADDING = 12;

	private bool _isFocus;

	private string _tabItemNarratorText;

	private AutomationPeer _automationPeer;

	private ScrollViewer _scrollViewer;

	private PivotHeaderItem _pivotHeaderItem;

	private Grid _grid;

	private Grid _rootGrid;

	private TextBlock _textBlock;

	private ContentControl _contentControl;

	private readonly UISettings _uiSettings;

	private ToolTip _toolTipTextBlock;

	public static readonly DependencyProperty NotificationBadgeProperty = DependencyProperty.Register("NotificationBadge", typeof(BadgeBase), typeof(TabItem), new PropertyMetadata(null));

	public bool SelectedByKeyboard { get; private set; }

	public BadgeBase NotificationBadge
	{
		get
		{
			return (BadgeBase)GetValue(NotificationBadgeProperty);
		}
		set
		{
			SetValue(NotificationBadgeProperty, value);
		}
	}

	public TabItem()
	{
		_tabItemNarratorText = string.Empty;
		_uiSettings = new UISettings();
		base.DefaultStyleKey = typeof(TabItem);
		base.Loaded += TabItem_Loaded;
		base.KeyDown += TabItem_KeyDown;
		base.SizeChanged += TabItem_SizeChanged;
		base.Unloaded += TabItem_Unloaded;
	}

	protected override AutomationPeer OnCreateAutomationPeer()
	{
		_automationPeer = base.OnCreateAutomationPeer();
		return _automationPeer;
	}

	private async void TabItem_Loaded(object sender, RoutedEventArgs e)
	{
		_pivotHeaderItem = base.KeyTipTarget as PivotHeaderItem;
		if (_pivotHeaderItem == null)
		{
			return;
		}
		_scrollViewer = UIExtensionsInternal.GetVisualParent<ScrollViewer>(this);
		_grid = UIExtensionsInternal.FindFirstChildOfType<Grid>(_pivotHeaderItem);
		_rootGrid = ((_grid != null) ? UIExtensionsInternal.FindFirstChildOfType<Grid>(_grid) : null);
		_contentControl = ((_rootGrid != null) ? UIExtensionsInternal.FindFirstChildOfType<ContentControl>(_rootGrid) : null);
		if (!(_scrollViewer == null) && !(_grid == null) && !(_rootGrid == null) && !(_contentControl == null) && !(base.XamlRoot == null))
		{
			UIExtensionsInternal.ExecuteWhenLoaded(_contentControl, delegate
			{
				ApplyTextTrimmingToString(_contentControl);
			});
			base.XamlRoot.Content.PointerMoved += Window_PointerMoved;
			_scrollViewer.SizeChanged += ScrollViewer_SizeChanged;
			await ApplySizeTabItem();
			AddNotificationBadgeInTabHeader();
		}
	}

	private void Window_PointerMoved(object sender, PointerRoutedEventArgs e)
	{
		ToolTip toolTip = _pivotHeaderItem?.GetToolTip() as ToolTip;
		if (toolTip != null && toolTip.IsOpen && FocusManager.GetFocusedElement(base.XamlRoot) is TabView && _isFocus)
		{
			CloseToolTip();
		}
	}

	private void TabItem_KeyDown(object sender, KeyRoutedEventArgs e)
	{
		if (e.Key == VirtualKey.Space || e.Key == VirtualKey.Enter)
		{
			SelectedByKeyboard = true;
		}
	}

	private async void TabItem_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		if (e.NewSize.Width != e.PreviousSize.Width)
		{
			await ApplySizeTabItem();
		}
	}

	private async void ScrollViewer_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		if (e.NewSize.Width != e.PreviousSize.Width)
		{
			await ApplySizeTabItem();
		}
		SetPivotHeaderItemSize();
	}

	private void TabItem_Unloaded(object sender, RoutedEventArgs e)
	{
		base.KeyDown -= TabItem_KeyDown;
		base.SizeChanged -= TabItem_SizeChanged;
		if (base.XamlRoot != null && base.XamlRoot.Content != null)
		{
			base.XamlRoot.Content.PointerMoved -= Window_PointerMoved;
		}
		_scrollViewer.SizeChanged -= ScrollViewer_SizeChanged;
		if (_textBlock != null)
		{
			_textBlock.IsTextTrimmedChanged -= TextBlockTrimmedChanged;
		}
	}

	public void Focus()
	{
		if (_pivotHeaderItem != null)
		{
			VisualStateManager.GoToState(_pivotHeaderItem, "Focused", useTransitions: true);
			_isFocus = true;
			UpdateToolTip(isFocus: true);
			RaiseNarratorEvent();
		}
	}

	public void Unfocus()
	{
		if (_pivotHeaderItem != null)
		{
			VisualStateManager.GoToState(_pivotHeaderItem, "Unfocused", useTransitions: true);
			_isFocus = false;
			UpdateToolTip(isFocus: false);
		}
	}

	private void ApplyTextTrimmingToString(ContentControl contentControl)
	{
		if (contentControl.Content is string text)
		{
			TextBlock textBlock = new TextBlock();
			textBlock.TextTrimming = TextTrimming.CharacterEllipsis;
			textBlock.TextWrapping = TextWrapping.NoWrap;
			textBlock.Text = text;
			base.Header = textBlock;
			_textBlock = textBlock;
			_toolTipTextBlock = new ToolTip
			{
				IsTabStop = false
			};
			_textBlock.IsTextTrimmedChanged += TextBlockTrimmedChanged;
			SetPivotHeaderItemSize();
		}
	}

	private void SetPivotHeaderItemSize()
	{
		if (_pivotHeaderItem != null && _rootGrid != null)
		{
			_pivotHeaderItem.MinHeight = 36.0;
			_rootGrid.MinHeight = 36.0;
			_pivotHeaderItem.MaxHeight = ((_uiSettings.TextScaleFactor <= 1.5) ? 36.0 : 44.0);
			_rootGrid.MaxHeight = ((_uiSettings.TextScaleFactor <= 1.5) ? 36.0 : 44.0);
		}
	}

	private void TextBlockTrimmedChanged(TextBlock sender, IsTextTrimmedChangedEventArgs args)
	{
		if ((object)sender != null)
		{
			AddToolTip(sender);
		}
	}

	private void AddToolTip(TextBlock textBlock)
	{
		if (!(textBlock == null) && !(_pivotHeaderItem == null) && !(_toolTipTextBlock == null))
		{
			_toolTipTextBlock.Content = textBlock.Text;
			_toolTipTextBlock.Visibility = ((!textBlock.IsTextTrimmed) ? Visibility.Collapsed : Visibility.Visible);
			if (textBlock.IsTextTrimmed)
			{
				ToolTipService.SetToolTip(textBlock, _toolTipTextBlock);
				ToolTipService.SetToolTip(_pivotHeaderItem, _toolTipTextBlock);
			}
		}
	}

	private void UpdateToolTip(bool isFocus)
	{
		ToolTip toolTip = _pivotHeaderItem?.GetToolTip() as ToolTip;
		if (toolTip != null)
		{
			toolTip.IsOpen = isFocus;
		}
	}

	private void CloseToolTip()
	{
		ToolTip toolTip = _pivotHeaderItem?.GetToolTip() as ToolTip;
		if (toolTip != null)
		{
			toolTip.IsOpen = false;
		}
	}

	private async Task ApplySizeTabItem()
	{
		if (!(_rootGrid != null) || !(_pivotHeaderItem != null) || !(_scrollViewer != null))
		{
			return;
		}
		await base.DispatcherQueue.EnqueueAsync(delegate
		{
			ScrollViewer scrollViewer = _scrollViewer;
			double maxWidth = (((object)scrollViewer != null && scrollViewer.ActualWidth > 0.0) ? (_scrollViewer.ActualWidth / 2.0) : 0.0);
			_rootGrid.MaxWidth = maxWidth;
			_pivotHeaderItem.MaxWidth = maxWidth;
			if (_textBlock != null)
			{
				_textBlock.MaxWidth = maxWidth;
			}
		});
	}

	private void AddNotificationBadgeInTabHeader()
	{
		ContentControl contentControl = UIExtensionsInternal.FindChildByName<ContentControl>("TabHeaderItemNotificationBadge", _pivotHeaderItem);
		if (contentControl != null)
		{
			contentControl.Content = NotificationBadge;
		}
		if (NotificationBadge != null && _rootGrid != null && _rootGrid.ColumnDefinitions.Count >= 3)
		{
			_rootGrid.ColumnDefinitions[0].Width = new GridLength(24.0);
			_rootGrid.ColumnDefinitions[2].Width = new GridLength(24.0);
		}
	}

	internal void UpdateNarratorText(string tabItemPositon)
	{
		_tabItemNarratorText = tabItemPositon;
	}

	internal void RaiseNarratorEvent()
	{
		if (!(_automationPeer == null))
		{
			string tabHeaderTextNarrator = GetTabHeaderTextNarrator();
			if (!string.IsNullOrWhiteSpace(tabHeaderTextNarrator))
			{
				_automationPeer.RaiseNotificationEvent(AutomationNotificationKind.Other, AutomationNotificationProcessing.MostRecent, tabHeaderTextNarrator + ". " + _tabItemNarratorText, tabHeaderTextNarrator);
			}
		}
	}

	private string GetTabHeaderTextNarrator()
	{
		string name = AutomationProperties.GetName(this);
		if (!string.IsNullOrWhiteSpace(name))
		{
			return name;
		}
		if (_pivotHeaderItem == null)
		{
			return string.Empty;
		}
		if (_pivotHeaderItem.Content is string result)
		{
			return result;
		}
		if (_pivotHeaderItem.Content is TextBlock textBlock)
		{
			return textBlock.Text;
		}
		return string.Empty;
	}
}
