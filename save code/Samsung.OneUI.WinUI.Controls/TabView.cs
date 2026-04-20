using System;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;

namespace Samsung.OneUI.WinUI.Controls;

public sealed class TabView : Pivot
{
	private const string PIVOT_HEADER = "Header";

	private const string PIVOT_STATIC_HEADER = "StaticHeader";

	private const string PIVOT_LAYOUT_ELEMENT_NAME = "HeaderGrid";

	private const string HEADER_CLIPPER_COMPONENT_NAME = "HeaderClipper";

	private const string NEXT_BUTTON_COMPONENT_NAME = "NextButtonTabView";

	private const string PREVIOUS_BUTTON_COMPONENT_NAME = "PreviousButtonTabView";

	private const string TAB_HEADER_SCROLL_VIEWER = "TabHeaderScrollViewer";

	private const int SCROLL_DENTAL = 30;

	private const int HEADER_ITEM_MIN_WIDTH = 32;

	private const int ITEM_HEADER_PADDING = 12;

	private const int MIN_VISIBLE_HEADER_ITEM_IN_FULL_MODE = 2;

	private const int DEFAULT_VISIBLE_HEADER_ITEM_IN_FULL_MODE = 0;

	private const string SELECTED_STRING_ID = "SS_SELECTED";

	private const string ITEM_ORDER_ID = "DREAM_P1SD_OF_P2SD_TBOPT";

	private Grid _pivotLayoutElement;

	private int _currSelectedIndex;

	private TabItem _currentItemSelected;

	private bool _selectionChangedByKeyboard;

	private ContentControl _headerClipper;

	private RepeatButton _nextButton;

	private RepeatButton _previousButton;

	private ScrollViewer _tabHeaderScrollViewer;

	private PivotHeaderPanel _headerPanel;

	private PivotHeaderPanel _staticHeaderPanel;

	private readonly string _selectedResource = "SS_SELECTED".GetLocalized();

	private readonly string _orderResource = "DREAM_P1SD_OF_P2SD_TBOPT".GetLocalized();

	public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(TabViewType), typeof(TabView), new PropertyMetadata(TabViewType.FullMode, OnTypePropertyChanged));

	public static readonly DependencyProperty MaxVisibleHeaderInViewportProperty = DependencyProperty.Register("MaxVisibleHeaderInViewport", typeof(int), typeof(TabView), new PropertyMetadata(0, OnMaxVisibleHeaderInViewPortChanged));

	public static readonly DependencyProperty HeaderClipperMarginProperty = DependencyProperty.Register("HeaderClipperMargin", typeof(Thickness), typeof(TabView), new PropertyMetadata(new Thickness(0.0, 0.0, 0.0, 6.0), OnHeaderClipperMarginPropertyChanged));

	public TabViewType Type
	{
		get
		{
			return (TabViewType)GetValue(TypeProperty);
		}
		set
		{
			SetValue(TypeProperty, value);
		}
	}

	public int MaxVisibleHeaderInViewport
	{
		get
		{
			return (int)GetValue(MaxVisibleHeaderInViewportProperty);
		}
		set
		{
			SetValue(MaxVisibleHeaderInViewportProperty, value);
		}
	}

	public Thickness HeaderClipperMargin
	{
		get
		{
			return (Thickness)GetValue(HeaderClipperMarginProperty);
		}
		set
		{
			SetValue(HeaderClipperMarginProperty, value);
		}
	}

	private static void OnTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is TabView tabView)
		{
			tabView.UpdateSizeHeaderItem();
		}
	}

	private static void OnMaxVisibleHeaderInViewPortChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is TabView { IsLoaded: not false } tabView)
		{
			tabView.SetValue(MaxVisibleHeaderInViewportProperty, (int)MakeClampedValue((int)e.NewValue, 2.0, tabView.Items.Count));
			tabView.UpdateSizeHeaderItem();
		}
	}

	public TabView()
	{
		base.DefaultStyleKey = typeof(TabView);
		base.Items.VectorChanged += Items_VectorChanged;
		base.GettingFocus += TabView_GettingFocus;
		base.LosingFocus += TabView_LosingFocus;
		base.PreviewKeyDown += TabView_PreviewKeyDownAsync;
		base.SelectionChanged += TabView_SelectionChanged;
		base.Tapped += TabView_Tapped;
		base.PointerPressed += TabView_PointerPressed;
		base.GotFocus += TabView_GotFocus;
		base.Unloaded += TabView_Unloaded;
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		_headerClipper = GetTemplateChild("HeaderClipper") as ContentControl;
		_nextButton = GetTemplateChild("NextButtonTabView") as RepeatButton;
		_previousButton = GetTemplateChild("PreviousButtonTabView") as RepeatButton;
		_tabHeaderScrollViewer = GetTemplateChild("TabHeaderScrollViewer") as ScrollViewer;
		_headerPanel = GetTemplateChild("Header") as PivotHeaderPanel;
		_staticHeaderPanel = GetTemplateChild("StaticHeader") as PivotHeaderPanel;
		_pivotLayoutElement = GetTemplateChild("HeaderGrid") as Grid;
		UnregisterEvents();
		RegisterEvents();
		InitializeLayout();
	}

	private static void OnHeaderClipperMarginPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is TabView tabView)
		{
			tabView.UpdateComponentLayout();
		}
	}

	private void TabView_Unloaded(object sender, RoutedEventArgs e)
	{
		UnregisterEvents();
	}

	private void TabView_PointerPressed(object sender, PointerRoutedEventArgs e)
	{
		_selectionChangedByKeyboard = false;
	}

	private void TabView_Tapped(object sender, TappedRoutedEventArgs e)
	{
		if (sender is TabView tabView)
		{
			GetSelectedTabItem(tabView)?.Unfocus();
		}
	}

	private void TabView_LosingFocus(UIElement sender, LosingFocusEventArgs args)
	{
		if (sender is TabView tabView)
		{
			GetSelectedTabItem(tabView)?.Unfocus();
		}
	}

	private void TabView_GotFocus(object sender, RoutedEventArgs args)
	{
		if (sender is TabView tabView)
		{
			TabItem selectedTabItem = GetSelectedTabItem(tabView);
			object focusedElement = FocusManager.GetFocusedElement();
			if (focusedElement != null && !(selectedTabItem == null) && (focusedElement.GetType() == selectedTabItem.GetType() || focusedElement.GetType() == GetType()))
			{
				selectedTabItem.RaiseNarratorEvent();
			}
		}
	}

	private void TabView_GettingFocus(UIElement sender, GettingFocusEventArgs args)
	{
		if (args.InputDevice != FocusInputDeviceKind.Mouse && args.InputDevice != FocusInputDeviceKind.Touch && args.InputDevice != FocusInputDeviceKind.Pen && sender is TabView tabView && args.OriginalSource == this)
		{
			GetSelectedTabItem(tabView)?.Focus();
			_selectionChangedByKeyboard = true;
		}
	}

	private void TabView_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (!(sender is TabView tabView))
		{
			return;
		}
		AdjustHeaderItemPosition(tabView);
		UpdateSelectedBadge(tabView);
		if (base.FocusState == FocusState.Unfocused)
		{
			return;
		}
		_currentItemSelected?.Unfocus();
		TabItem selectedTabItem = GetSelectedTabItem(tabView);
		if (selectedTabItem != null)
		{
			UnfocusAllItems(tabView, selectedTabItem);
			if (_selectionChangedByKeyboard)
			{
				selectedTabItem.Focus();
			}
		}
		_currentItemSelected = GetSelectedTabItem(tabView);
	}

	private void UpdateSelectedBadge(TabView tabView)
	{
		if (!(tabView == null))
		{
			UpdateStateSelectedBadge(_currSelectedIndex, IsSelected: false);
			UpdateStateSelectedBadge(base.SelectedIndex, IsSelected: true);
			_currSelectedIndex = tabView.SelectedIndex;
		}
	}

	private TabItem FindTabItem(int tabItemIndex)
	{
		if (tabItemIndex >= base.Items.Count)
		{
			return null;
		}
		return base.Items[tabItemIndex] as TabItem;
	}

	private void UpdateStateSelectedBadge(int tabItemIndex, bool IsSelected)
	{
		TabItem tabItem = FindTabItem(tabItemIndex);
		if (!(tabItem == null) && !(tabItem.NotificationBadge == null))
		{
			tabItem.NotificationBadge.IsSelected = IsSelected;
		}
	}

	private void AdjustHeaderItemPosition(TabView tabView)
	{
		if (!(_headerPanel == null))
		{
			PivotHeaderItem item = _headerPanel.Children[tabView.SelectedIndex] as PivotHeaderItem;
			PivotHeaderItem item2 = _staticHeaderPanel.Children[tabView.SelectedIndex] as PivotHeaderItem;
			MoveItemToViewPort(item);
			MoveItemToViewPort(item2);
		}
	}

	private void MoveItemToViewPort(PivotHeaderItem item)
	{
		if (!(item == null) && !(_tabHeaderScrollViewer == null))
		{
			Point point = item.TransformToVisual(_tabHeaderScrollViewer).TransformPoint(new Point(0f, 0f));
			double horizontalOffset = _tabHeaderScrollViewer.HorizontalOffset;
			double num = horizontalOffset + _tabHeaderScrollViewer.ViewportWidth;
			double num2 = point.X + horizontalOffset;
			double num3 = num2 + item.ActualWidth;
			if (num2 - _tabHeaderScrollViewer.Padding.Left < horizontalOffset)
			{
				DoMoveToleft(_tabHeaderScrollViewer, num2 - _tabHeaderScrollViewer.Padding.Left - HeaderClipperMargin.Left);
			}
			else if (num3 + _tabHeaderScrollViewer.Padding.Right > num)
			{
				DoMoveToRight(_tabHeaderScrollViewer, num3 - _tabHeaderScrollViewer.ViewportWidth - _tabHeaderScrollViewer.Padding.Right + HeaderClipperMargin.Right);
			}
		}
	}

	private void DoMoveToRight(ScrollViewer scrollViewer, double offset)
	{
		scrollViewer.ChangeView(offset, null, null, disableAnimation: false);
	}

	private void DoMoveToleft(ScrollViewer scrollViewer, double offset)
	{
		scrollViewer.ChangeView(offset, null, null, disableAnimation: false);
	}

	private void UnfocusAllItems(TabView tabView, TabItem selectedItem)
	{
		foreach (TabItem item in tabView.Items)
		{
			if (item != selectedItem)
			{
				item.Unfocus();
			}
		}
	}

	private void TabView_PreviewKeyDownAsync(object sender, KeyRoutedEventArgs e)
	{
		if (base.FocusState == FocusState.Unfocused)
		{
			return;
		}
		GetSelectedTabItem(this)?.Unfocus();
		if (e.Key == VirtualKey.Right)
		{
			if (IsLastTabItem())
			{
				GetLastTabItem().Focus();
				return;
			}
			GetNextTabItem().Focus();
			base.SelectedIndex++;
		}
		else if (e.Key == VirtualKey.Left)
		{
			if (IsFirstElement())
			{
				GetFirstTabItem().Focus();
				return;
			}
			GetPreviousTabItem().Focus();
			base.SelectedIndex--;
		}
	}

	private void Items_VectorChanged(IObservableVector<object> sender, IVectorChangedEventArgs @event)
	{
		UpdateLayout();
		UpdateItemsNarratorText();
		ChangeVisibilityArrowButton();
		UpdateSizeHeaderItem();
		AddEventHandlerForSelectedHeaderItem();
	}

	private static double MakeClampedValue(double value, double min, double max)
	{
		return Math.Max(Math.Min(value, max), min);
	}

	private PivotHeaderItem GetHeaderItem(int index, PivotHeaderPanel pivotHeaderPanel)
	{
		if (pivotHeaderPanel == null || index < 0 || index >= base.Items.Count)
		{
			return null;
		}
		return pivotHeaderPanel.Children[index] as PivotHeaderItem;
	}

	private double CalculateHeaderItem(double contentSize, int numberOfHeaderItems)
	{
		return (contentSize - (double)(24 * (numberOfHeaderItems + 1))) / (double)numberOfHeaderItems;
	}

	private void UpdateSizeHeaderItem()
	{
		base.DispatcherQueue.TryEnqueue(delegate
		{
			if (Type == TabViewType.FullMode)
			{
				UpdateHeaderItemInFullMode();
			}
			else if (Type == TabViewType.AdaptiveFullMode)
			{
				UpdateHeaderItemInAdaptiveFullMode();
			}
			else if (Type == TabViewType.FlexMode)
			{
				UpdateHeaderItemInFlexMode();
			}
		});
	}

	private void UpdateHeaderItemInAdaptiveFullMode()
	{
		if (_tabHeaderScrollViewer == null)
		{
			return;
		}
		ResetToDefaultMode();
		for (int i = 0; i < base.Items.Count; i++)
		{
			PivotHeaderItem headerItem = GetHeaderItem(i, _headerPanel);
			PivotHeaderItem headerItem2 = GetHeaderItem(i, _staticHeaderPanel);
			if (headerItem != null)
			{
				double minWidth = MakeClampedValue(headerItem.ActualWidth, 32.0, _tabHeaderScrollViewer.ViewportWidth / 2.0);
				headerItem.MinWidth = minWidth;
			}
			if (headerItem2 != null)
			{
				double minWidth2 = MakeClampedValue(headerItem2.ActualWidth, 56.0, _tabHeaderScrollViewer.ViewportWidth / 2.0);
				headerItem2.MinWidth = minWidth2;
			}
		}
	}

	private void UpdateHeaderItemInFlexMode()
	{
		for (int i = 0; i < base.Items.Count; i++)
		{
			PivotHeaderItem headerItem = GetHeaderItem(i, _headerPanel);
			PivotHeaderItem headerItem2 = GetHeaderItem(i, _staticHeaderPanel);
			if (headerItem != null)
			{
				headerItem.Width = double.NaN;
				headerItem.MinWidth = 0.0;
			}
			if (headerItem2 != null)
			{
				headerItem2.Width = double.NaN;
				headerItem2.MinWidth = 0.0;
			}
		}
	}

	private void UpdateHeaderItemInFullMode()
	{
		if (_pivotLayoutElement == null)
		{
			return;
		}
		ResetToDefaultMode();
		int numberOfHeaderItems = ((MaxVisibleHeaderInViewport != 0) ? MaxVisibleHeaderInViewport : base.Items.Count);
		double num = _pivotLayoutElement.ActualWidth - (HeaderClipperMargin.Right + HeaderClipperMargin.Left);
		double value = CalculateHeaderItem(num, numberOfHeaderItems);
		value = MakeClampedValue(value, 32.0, num / 2.0);
		int numberOfHeaderItems2 = (int)((num - 24.0) / (24.0 + value));
		if (num % value != 0.0)
		{
			value = CalculateHeaderItem(num, numberOfHeaderItems2) + 24.0;
		}
		for (int i = 0; i < base.Items.Count; i++)
		{
			PivotHeaderItem headerItem = GetHeaderItem(i, _headerPanel);
			PivotHeaderItem headerItem2 = GetHeaderItem(i, _staticHeaderPanel);
			if (headerItem != null)
			{
				headerItem.Width = value;
			}
			if (headerItem2 != null)
			{
				headerItem2.Width = value;
			}
		}
	}

	private void ResetToDefaultMode()
	{
		if (!(_tabHeaderScrollViewer == null))
		{
			UpdateHeaderItemInFlexMode();
			_tabHeaderScrollViewer.UpdateLayout();
		}
	}

	private void ChangeVisibilityArrowButton()
	{
		if (!(_nextButton == null) && !(_previousButton == null) && !IsShowArrowButton())
		{
			_nextButton.Visibility = Visibility.Collapsed;
			_previousButton.Visibility = Visibility.Collapsed;
		}
	}

	private void RegisterEvents()
	{
		if (!(_nextButton == null) && !(_previousButton == null) && !(_pivotLayoutElement == null))
		{
			_nextButton.Click += NextButton_Click;
			_previousButton.Click += PreviousButton_Click;
			_pivotLayoutElement.PointerEntered += PivotLayoutElement_PointerEntered;
			_pivotLayoutElement.PointerExited += PivotLayoutElement_PointerExited;
			_pivotLayoutElement.SizeChanged += TabHeaderScrollViewer_SizeChanged;
			AddEventHandlerForSelectedHeaderItem();
		}
	}

	private void InitializeLayout()
	{
		UpdateItemsNarratorText();
		UpdateComponentLayout();
		ChangeVisibilityArrowButton();
		MaxVisibleHeaderInViewport = (int)MakeClampedValue(MaxVisibleHeaderInViewport, 2.0, base.Items.Count);
	}

	private void TabHeaderScrollViewer_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		UpdateSizeHeaderItem();
		ChangeVisibilityArrowButton();
	}

	private void UnregisterEvents()
	{
		if (_nextButton == null || _previousButton == null || _pivotLayoutElement == null || _tabHeaderScrollViewer == null)
		{
			return;
		}
		_nextButton.Click -= NextButton_Click;
		_previousButton.Click -= PreviousButton_Click;
		_pivotLayoutElement.PointerEntered -= PivotLayoutElement_PointerEntered;
		_pivotLayoutElement.PointerExited -= PivotLayoutElement_PointerExited;
		_pivotLayoutElement.SizeChanged -= TabHeaderScrollViewer_SizeChanged;
		for (int i = 0; i < base.Items.Count; i++)
		{
			PivotHeaderItem headerItem = GetHeaderItem(i, _headerPanel);
			PivotHeaderItem headerItem2 = GetHeaderItem(i, _staticHeaderPanel);
			if (headerItem != null)
			{
				UnRegisteredPointerEvent(headerItem);
			}
			if (headerItem2 != null)
			{
				UnRegisteredPointerEvent(headerItem2);
			}
		}
	}

	private void UnRegisteredPointerEvent(PivotHeaderItem pivotHeaderItem)
	{
		if (!(pivotHeaderItem == null))
		{
			pivotHeaderItem.PointerEntered -= PivotHeaderItem_PointerEntered;
			pivotHeaderItem.PointerExited -= PivotHeaderItem_PointerExited;
			pivotHeaderItem.PointerPressed -= PivotHeaderItem_PointerPressed;
			pivotHeaderItem.PointerReleased -= PivotHeaderItem_PointerReleased;
		}
	}

	private void PivotLayoutElement_PointerExited(object sender, PointerRoutedEventArgs e)
	{
		if (!(_previousButton == null) && !(_nextButton == null))
		{
			_previousButton.Visibility = Visibility.Collapsed;
			_nextButton.Visibility = Visibility.Collapsed;
		}
	}

	private void PivotLayoutElement_PointerEntered(object sender, PointerRoutedEventArgs e)
	{
		if (!(_nextButton == null) && !(_previousButton == null) && !(_pivotLayoutElement == null) && IsShowArrowButton())
		{
			_nextButton.Visibility = Visibility.Visible;
			_previousButton.Visibility = Visibility.Visible;
		}
	}

	private bool IsShowArrowButton()
	{
		if (_tabHeaderScrollViewer == null || _pivotLayoutElement == null)
		{
			return false;
		}
		return _tabHeaderScrollViewer.ExtentWidth > _pivotLayoutElement.ActualWidth;
	}

	private void PreviousButton_Click(object sender, RoutedEventArgs e)
	{
		if (!(_tabHeaderScrollViewer == null))
		{
			_tabHeaderScrollViewer.ChangeView(-30.0 + _tabHeaderScrollViewer.HorizontalOffset, null, null, disableAnimation: false);
		}
	}

	private void NextButton_Click(object sender, RoutedEventArgs e)
	{
		if (!(_tabHeaderScrollViewer == null))
		{
			_tabHeaderScrollViewer.ChangeView(30.0 + _tabHeaderScrollViewer.HorizontalOffset, null, null, disableAnimation: false);
		}
	}

	private void UpdateItemsNarratorText()
	{
		foreach (TabItem item in base.Items.Cast<TabItem>())
		{
			string text = string.Format(_orderResource, base.Items.IndexOf(item) + 1, base.Items.Count);
			item.UpdateNarratorText(text + ". " + _selectedResource);
		}
	}

	private void AddEventHandlerForSelectedHeaderItem()
	{
		for (int i = 0; i < base.Items.Count; i++)
		{
			PivotHeaderItem headerItem = GetHeaderItem(i, _headerPanel);
			PivotHeaderItem headerItem2 = GetHeaderItem(i, _staticHeaderPanel);
			if (headerItem != null && headerItem.Tag == null)
			{
				RegisteredPointerEvent(headerItem);
			}
			if (headerItem2 != null && headerItem2.Tag == null)
			{
				RegisteredPointerEvent(headerItem2);
			}
		}
	}

	private void RegisteredPointerEvent(PivotHeaderItem pivotHeaderItem)
	{
		if (!(pivotHeaderItem == null))
		{
			pivotHeaderItem.Tag = true;
			UnRegisteredPointerEvent(pivotHeaderItem);
			pivotHeaderItem.PointerEntered += PivotHeaderItem_PointerEntered;
			pivotHeaderItem.PointerExited += PivotHeaderItem_PointerExited;
			pivotHeaderItem.PointerPressed += PivotHeaderItem_PointerPressed;
			pivotHeaderItem.PointerReleased += PivotHeaderItem_PointerReleased;
		}
	}

	private void PivotHeaderItem_PointerReleased(object sender, PointerRoutedEventArgs e)
	{
		PivotHeaderItem pivotHeaderItem = sender as PivotHeaderItem;
		bool valueOrDefault = pivotHeaderItem.Tag as bool? == true;
		UpdateSelectedBadge(pivotHeaderItem, valueOrDefault);
	}

	private void PivotHeaderItem_PointerPressed(object sender, PointerRoutedEventArgs e)
	{
		PivotHeaderItem pivotHeaderItem = sender as PivotHeaderItem;
		UpdateSelectedBadge(pivotHeaderItem, isSelected: false);
	}

	private int GetHeaderItemIndex(PivotHeaderItem pivotHeaderItem, PivotHeaderPanel pivotHeaderPanel)
	{
		if (pivotHeaderItem == null || pivotHeaderPanel == null)
		{
			return -1;
		}
		return pivotHeaderPanel.Children.IndexOf(pivotHeaderItem);
	}

	private void PivotHeaderItem_PointerExited(object sender, PointerRoutedEventArgs e)
	{
		PivotHeaderItem pivotHeaderItem = sender as PivotHeaderItem;
		UpdateSelectedBadge(pivotHeaderItem, isSelected: true);
	}

	private void PivotHeaderItem_PointerEntered(object sender, PointerRoutedEventArgs e)
	{
		PivotHeaderItem pivotHeaderItem = sender as PivotHeaderItem;
		UpdateSelectedBadge(pivotHeaderItem, isSelected: false);
		int num = ((GetHeaderItemIndex(pivotHeaderItem, _staticHeaderPanel) == -1) ? GetHeaderItemIndex(pivotHeaderItem, _headerPanel) : GetHeaderItemIndex(pivotHeaderItem, _staticHeaderPanel));
		pivotHeaderItem.Tag = num != base.SelectedIndex;
	}

	private void UpdateSelectedBadge(PivotHeaderItem pivotHeaderItem, bool isSelected)
	{
		if (!(pivotHeaderItem == null))
		{
			int num = ((GetHeaderItemIndex(pivotHeaderItem, _staticHeaderPanel) == -1) ? GetHeaderItemIndex(pivotHeaderItem, _headerPanel) : GetHeaderItemIndex(pivotHeaderItem, _staticHeaderPanel));
			TabItem tabItem = FindTabItem(num);
			if (!(tabItem == null) && !(tabItem.NotificationBadge == null) && num == base.SelectedIndex)
			{
				tabItem.NotificationBadge.IsSelected = isSelected;
			}
		}
	}

	private void UpdateComponentLayout()
	{
		UpdateHeaderClipperMargin();
		UpdateArrowButtonsMargin();
	}

	private void UpdateHeaderClipperMargin()
	{
		if (!(_headerClipper == null))
		{
			_headerClipper.Margin = HeaderClipperMargin;
		}
	}

	private void UpdateArrowButtonsMargin()
	{
		if (!(_nextButton == null) && !(_previousButton == null))
		{
			Thickness margin = new Thickness(0.0, HeaderClipperMargin.Top, 0.0, HeaderClipperMargin.Bottom);
			_nextButton.Margin = margin;
			_previousButton.Margin = margin;
		}
	}

	private TabItem GetPreviousTabItem()
	{
		return (TabItem)base.Items[base.SelectedIndex - 1];
	}

	private TabItem GetLastTabItem()
	{
		return (TabItem)base.Items[base.Items.Count - 1];
	}

	private TabItem GetNextTabItem()
	{
		return (TabItem)base.Items[base.SelectedIndex + 1];
	}

	private TabItem GetFirstTabItem()
	{
		return (TabItem)base.Items[0];
	}

	private bool IsFirstElement()
	{
		return base.SelectedIndex == 0;
	}

	private bool IsLastTabItem()
	{
		return base.SelectedIndex == base.Items.Count - 1;
	}

	private TabItem GetSelectedTabItem(TabView tabView)
	{
		return tabView.SelectedItem as TabItem;
	}
}
