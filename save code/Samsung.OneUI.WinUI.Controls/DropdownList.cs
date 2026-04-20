using System;
using System.Collections;
using System.Threading.Tasks;
using CommunityToolkit.WinUI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Samsung.OneUI.WinUI.Controls.Selectors;
using Samsung.OneUI.WinUI.Services.Animation;
using Samsung.OneUI.WinUI.Utils.Extensions;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.Foundation;
using Windows.System;

namespace Samsung.OneUI.WinUI.Controls;

public sealed class DropdownList : ItemsControl
{
	private const string ROOT_GRID = "RootGrid";

	private const string PART_CONTENT_PRESENT_NAME = "ContentPresenter";

	private const string PART_HEADER_NAME = "HeaderContentPresenter";

	private const string PART_POPUP = "Popup";

	private const string DROPDOWN_LIST_VIEW = "PART_DropdownListView";

	private const string DROPDOWN_LIST_RELATIVE_PANEL_NAME = "DropdownListRelativePanel";

	private const string DROPDOWN_LIST_SCROLL_VIEWER_NAME = "ScrollViewer";

	private const string DROPDOWN_VERTICAL_SCROLL_VIEWER = "DropdownVerticalScrollViewer";

	private const string LISTVIEWITEM_TEXTBLOCK = "ContentPresenter";

	private const string PART_POPUP_SCALE_TRANSFORM = "PopupScaleTransform";

	private const string COLLAPSED_STRING_ID = "DREAM_COLLAPSED_M_STATUS_TBOPT";

	private const string DROPDOWNLIST_CONTROL_STRING_ID = "GS_DROPDOWN_LIST_TTS";

	private const double POSITIVE_INFINITY_NUMBER = double.PositiveInfinity;

	private DropdownCustomControl _contentPresenter;

	private Grid _rootGrid;

	private ContentPresenter _headerContent;

	private Popup _popup;

	private ScrollViewer _scrollViewer;

	private CompositeTransform _popupScaleTransform;

	private readonly IFlyoutAnimationService _flyoutAnimation;

	private DropdownListViewService _dropdownListViewService;

	private DropdownScrollViewerService _dropdownScrollViewerService;

	private OverlayPopupService _overlayPopupService;

	private DropdownListViewCustom _listView;

	private RelativePanel _dropdownListRelativePanel;

	private bool _isAnimationRunning;

	private DropdownListVisualStateService _visualStateService;

	private DispatcherTimer _dispatcherTimers;

	private bool _wasPointerPressed;

	private bool _isOpenedByKeyboard;

	public static readonly DependencyProperty IsListEnabledProperty = DependencyProperty.Register("IsListEnabled", typeof(bool), typeof(DropdownList), new PropertyMetadata(true, OnIsListEnabledPropertyChanged));

	public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register("SelectedIndex", typeof(int), typeof(DropdownList), new PropertyMetadata(-1, OnSelectedIndexPropertyChanged));

	public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(DropdownList), new PropertyMetadata(null, OnSelectedItemPropertyChanged));

	public new static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IList), typeof(DropdownList), new PropertyMetadata(null, OnItemsSourcePropertyChanged));

	public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof(object), typeof(DropdownList), new PropertyMetadata(null, OnHeaderPropertyChanged));

	public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register("Placeholder", typeof(object), typeof(DropdownList), new PropertyMetadata(null, OnPlaceholderTextPropertyChanged));

	public static readonly DependencyProperty ListTitleProperty = DependencyProperty.Register("ListTitle", typeof(string), typeof(DropdownList), new PropertyMetadata(null));

	public static readonly DependencyProperty ListTitleVisibilityProperty = DependencyProperty.Register("ListTitleVisibility", typeof(Visibility), typeof(DropdownList), new PropertyMetadata(Visibility.Collapsed));

	public static readonly DependencyProperty AppTitleBarHeightOffsetProperty = DependencyProperty.Register("AppTitleBarHeightOffset", typeof(int), typeof(DropdownList), new PropertyMetadata(0));

	public static readonly DependencyProperty VerticalOffsetProperty = DependencyProperty.Register("VerticalOffset", typeof(int), typeof(DropdownList), new PropertyMetadata(10));

	public static readonly DependencyProperty HorizontalOffsetProperty = DependencyProperty.Register("HorizontalOffset", typeof(int), typeof(DropdownList), new PropertyMetadata(0));

	public static readonly DependencyProperty DropdownPopupAlignmentProperty = DependencyProperty.Register("DropdownPopupAlignment", typeof(HorizontalAlignment), typeof(DropdownList), new PropertyMetadata(HorizontalAlignment.Left));

	public static readonly DependencyProperty ArrowColorProperty = DependencyProperty.Register("ArrowColor", typeof(SolidColorBrush), typeof(DropdownList), new PropertyMetadata(null));

	public static readonly DependencyProperty IsMultilineItemProperty = DependencyProperty.Register("IsMultilineItem", typeof(bool), typeof(DropdownList), new PropertyMetadata(false));

	public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(DropdownListType), typeof(DropdownList), new PropertyMetadata(DropdownListType.Default, OnTypeChanged));

	public static readonly DependencyProperty IsBlurProperty = DependencyProperty.Register("IsBlur", typeof(bool), typeof(DropdownList), new PropertyMetadata(true));

	public bool IsListEnabled
	{
		get
		{
			return (bool)GetValue(IsListEnabledProperty);
		}
		set
		{
			SetValue(IsListEnabledProperty, value);
		}
	}

	public int SelectedIndex
	{
		get
		{
			return (int)GetValue(SelectedIndexProperty);
		}
		set
		{
			SetValue(SelectedIndexProperty, value);
		}
	}

	public object SelectedItem
	{
		get
		{
			return GetValue(SelectedItemProperty);
		}
		set
		{
			SetValue(SelectedItemProperty, value);
		}
	}

	public new IList ItemsSource
	{
		get
		{
			return (IList)GetValue(ItemsSourceProperty);
		}
		set
		{
			SetValue(ItemsSourceProperty, value);
		}
	}

	public object Header
	{
		get
		{
			return GetValue(HeaderProperty);
		}
		set
		{
			SetValue(HeaderProperty, value);
		}
	}

	public object Placeholder
	{
		get
		{
			return GetValue(PlaceholderProperty);
		}
		set
		{
			SetValue(PlaceholderProperty, value);
		}
	}

	public string ListTitle
	{
		get
		{
			return (string)GetValue(ListTitleProperty);
		}
		set
		{
			SetValue(ListTitleProperty, value);
		}
	}

	public Visibility ListTitleVisibility
	{
		get
		{
			return (Visibility)GetValue(ListTitleVisibilityProperty);
		}
		set
		{
			SetValue(ListTitleVisibilityProperty, value);
		}
	}

	public int AppTitleBarHeightOffset
	{
		get
		{
			return (int)GetValue(AppTitleBarHeightOffsetProperty);
		}
		set
		{
			SetValue(AppTitleBarHeightOffsetProperty, value);
		}
	}

	public int VerticalOffset
	{
		get
		{
			return (int)GetValue(VerticalOffsetProperty);
		}
		set
		{
			SetValue(VerticalOffsetProperty, value);
		}
	}

	public int HorizontalOffset
	{
		get
		{
			return (int)GetValue(HorizontalOffsetProperty);
		}
		set
		{
			SetValue(HorizontalOffsetProperty, value);
		}
	}

	public HorizontalAlignment DropdownPopupAlignment
	{
		get
		{
			return (HorizontalAlignment)GetValue(DropdownPopupAlignmentProperty);
		}
		set
		{
			SetValue(DropdownPopupAlignmentProperty, value);
		}
	}

	public SolidColorBrush ArrowColor
	{
		get
		{
			return (SolidColorBrush)GetValue(ArrowColorProperty);
		}
		set
		{
			SetValue(ArrowColorProperty, value);
		}
	}

	public bool IsMultilineItem
	{
		get
		{
			return (bool)GetValue(IsMultilineItemProperty);
		}
		set
		{
			SetValue(IsMultilineItemProperty, value);
		}
	}

	public DropdownListType Type
	{
		get
		{
			return (DropdownListType)GetValue(TypeProperty);
		}
		set
		{
			SetValue(TypeProperty, value);
		}
	}

	public bool IsBlur
	{
		get
		{
			return (bool)GetValue(IsBlurProperty);
		}
		set
		{
			SetValue(IsBlurProperty, value);
		}
	}

	public event SelectionChangedEventHandler SelectionChanged;

	private static void OnIsListEnabledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is DropdownList dropdownList)
		{
			dropdownList._dropdownListViewService?.UpdateIsEnabledProperty();
		}
	}

	private static void OnSelectedIndexPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is DropdownList dropdownList)
		{
			dropdownList._dropdownListViewService?.UpdateSelectedIndexProperty();
		}
	}

	private static void OnSelectedItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is DropdownList dropdownList)
		{
			dropdownList._dropdownListViewService?.UpdateSelectedItemProperty();
			dropdownList.SetNarratorContent();
		}
	}

	private static void OnItemsSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is DropdownList dropdownList)
		{
			dropdownList._dropdownListViewService?.UpdateListViewItemsSource();
		}
	}

	private static void OnHeaderPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is DropdownList dropdownList)
		{
			dropdownList.UpdateHeader();
		}
	}

	private static void OnPlaceholderTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is DropdownList dropdownList)
		{
			dropdownList.UpdatePlaceholderText();
		}
	}

	private static void OnTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is DropdownList dropdownList)
		{
			dropdownList.Style = new DropdownStyleSelector(dropdownList.Type).SelectStyle();
			dropdownList.AddConfiguration();
		}
	}

	public DropdownList()
	{
		base.DefaultStyleKey = typeof(DropdownList);
		_flyoutAnimation = new FlyoutAnimationService();
	}

	private void VisualStateService_GettingFocus(object sender, GettingFocusEventArgs args)
	{
		if (Type == DropdownListType.SubAppBar)
		{
			_contentPresenter?.UpdateToolTipContent(_rootGrid);
		}
	}

	private void VisualStateService_PointerEntered(object sender, PointerRoutedEventArgs e)
	{
		if (Type == DropdownListType.SubAppBar)
		{
			_contentPresenter?.UpdateToolTipContent(_rootGrid);
		}
	}

	private void VisualStateService_PointerPressed(object sender, PointerRoutedEventArgs e)
	{
		_wasPointerPressed = true;
	}

	private void VisualStateService_PointerReleased(object sender, PointerRoutedEventArgs e)
	{
		if (_wasPointerPressed)
		{
			_rootGrid.GetToolTip()?.CloseToolTip();
			OpenPopup();
			_wasPointerPressed = false;
		}
	}

	private void VisualStateService_PointerExited(object sender, PointerRoutedEventArgs e)
	{
		_wasPointerPressed = false;
	}

	private void OverlayPopup_PointerPressed(object sender, PointerRoutedEventArgs e)
	{
		ClosePopup();
	}

	private void ListView_Loaded(object sender, RoutedEventArgs e)
	{
		if (_listView != null)
		{
			_listView.UpdateListTitleText(ListTitle);
			_listView.UpdateListTitleVisibility(ListTitleVisibility);
			_scrollViewer = UIExtensionsInternal.FindChildByName<ScrollViewer>("ScrollViewer", _listView);
			ScrollViewer scrollViewer = GetTemplateChild("DropdownVerticalScrollViewer") as ScrollViewer;
			if (scrollViewer != null)
			{
				_dropdownScrollViewerService = new DropdownScrollViewerService(scrollViewer, _dropdownListRelativePanel);
			}
		}
		PopupOpened(sender, e);
	}

	private void ListView_Unloaded(object sender, RoutedEventArgs e)
	{
		PopupClosed(sender, e);
	}

	private void ContentPresenter_PointerPressed(object sender, PointerRoutedEventArgs e)
	{
		_wasPointerPressed = true;
	}

	private void ContentPresenter_PointerReleased(object sender, PointerRoutedEventArgs e)
	{
		if (_wasPointerPressed)
		{
			_contentPresenter?.CloseToolTip();
			OpenPopup();
			_wasPointerPressed = false;
		}
	}

	private void ContentPresenter_PointerExited(object sender, PointerRoutedEventArgs e)
	{
		_wasPointerPressed = false;
	}

	private void ContentPresenter_PreviewKeyDown(object sender, KeyRoutedEventArgs e)
	{
		if (e.Key == VirtualKey.Enter || e.Key == VirtualKey.Space)
		{
			_isOpenedByKeyboard = true;
			OpenPopup();
		}
		base.OnPreviewKeyDown(e);
	}

	private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (_listView != null && _contentPresenter != null)
		{
			SelectedIndex = _listView.SelectedIndex;
			SelectedItem = _listView.SelectedItem;
			if (_listView.ContainerFromItem(SelectedItem) != null)
			{
				_contentPresenter.Content = (_listView.ContainerFromItem(SelectedItem) as ListViewItem)?.Content ?? Placeholder;
			}
			else
			{
				_contentPresenter.Content = GetSelectedItemContent() ?? Placeholder;
			}
		}
		this.SelectionChanged?.Invoke(sender, e);
	}

	private void ListViewItem_PreviewKeyDown(object sender, KeyRoutedEventArgs e)
	{
		if (e.Key == VirtualKey.Enter || e.Key == VirtualKey.Space)
		{
			PressedEnterOrSpaceKeys(sender);
		}
		else if (e.Key == VirtualKey.Down || e.Key == VirtualKey.Up)
		{
			PressedUpOrDownKeys(sender);
		}
		else if (e.Key == VirtualKey.Escape)
		{
			ClosePopup(isClosingFromKeyboard: true);
		}
	}

	private void PopupOpened(object sender, object e)
	{
		_popupScaleTransform.CenterX = _popup.HorizontalOffset;
		_popupScaleTransform.CenterY = _popup.VerticalOffset;
		if (sender is Popup popup)
		{
			popup.VerticalOffset = VerticalOffset;
			popup.HorizontalOffset = HorizontalOffset;
		}
		if (_listView != null && _listView.Items != null)
		{
			if (_listView.SelectedItem != null)
			{
				(_listView.ContainerFromIndex(_listView.SelectedIndex) as ListViewItem)?.Focus(_isOpenedByKeyboard ? FocusState.Keyboard : FocusState.Programmatic);
			}
			else
			{
				_listView.Focus(FocusState.Programmatic);
			}
			_dropdownListViewService?.SetListTitleVisibility(_listView.ValidateShowListTitle(ListTitleVisibility));
			_dropdownListViewService?.LoadItemsLayout();
			_dropdownListViewService?.InvokeActionForItems(delegate(ListViewItem listViewItem)
			{
				UpdateItemTextBehavior(listViewItem);
				AddListViewItemEvents(listViewItem);
			});
			_popup.UpdateLayout();
		}
		if (_dropdownListRelativePanel.XamlRoot != null)
		{
			Rect rectMaskBasedOnPosition = _dropdownScrollViewerService.GetRectMaskBasedOnPosition();
			_dropdownScrollViewerService.UpdateHeights(_dropdownListViewService, AppTitleBarHeightOffset);
			UpdateMargins(rectMaskBasedOnPosition);
		}
		UpdateTransformCenterPosition();
		_flyoutAnimation.OpenAnimation(_popup, _popupScaleTransform.CenterX, _popupScaleTransform.CenterY);
	}

	private void PopupClosed(object sender, object e)
	{
		_dropdownScrollViewerService?.ResetHeights();
		_dropdownListViewService?.InvokeActionForItems(RemoveListViewItemEvents);
		_dropdownListViewService?.UpdateMaxHeight(double.PositiveInfinity);
		UpdateToolTip();
	}

	private void ListView_ItemClick(object sender, ItemClickEventArgs e)
	{
		ClosePopup();
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		_contentPresenter = GetTemplateChild("ContentPresenter") as DropdownCustomControl;
		_rootGrid = GetTemplateChild("RootGrid") as Grid;
		_headerContent = GetTemplateChild("HeaderContentPresenter") as ContentPresenter;
		_popup = GetTemplateChild("Popup") as Popup;
		_dropdownListRelativePanel = GetTemplateChild("DropdownListRelativePanel") as RelativePanel;
		_popupScaleTransform = GetTemplateChild("PopupScaleTransform") as CompositeTransform;
		_listView = GetTemplateChild("PART_DropdownListView") as DropdownListViewCustom;
		CreateScrollDispatcherTimer();
		if (_dropdownListViewService == null)
		{
			_dropdownListViewService = new DropdownListViewService(this, _listView);
		}
		_dropdownListViewService.PopulateItemsSource();
		if (_visualStateService == null && _rootGrid != null)
		{
			_visualStateService = new DropdownListVisualStateService();
			DropdownListVisualStateService visualStateService = _visualStateService;
			visualStateService.PointerPressed = (EventHandler<PointerRoutedEventArgs>)Delegate.Combine(visualStateService.PointerPressed, new EventHandler<PointerRoutedEventArgs>(VisualStateService_PointerPressed));
			DropdownListVisualStateService visualStateService2 = _visualStateService;
			visualStateService2.PointerReleased = (EventHandler<PointerRoutedEventArgs>)Delegate.Combine(visualStateService2.PointerReleased, new EventHandler<PointerRoutedEventArgs>(VisualStateService_PointerReleased));
			DropdownListVisualStateService visualStateService3 = _visualStateService;
			visualStateService3.PointerEntered = (EventHandler<PointerRoutedEventArgs>)Delegate.Combine(visualStateService3.PointerEntered, new EventHandler<PointerRoutedEventArgs>(VisualStateService_PointerEntered));
			DropdownListVisualStateService visualStateService4 = _visualStateService;
			visualStateService4.PointerExited = (EventHandler<PointerRoutedEventArgs>)Delegate.Combine(visualStateService4.PointerExited, new EventHandler<PointerRoutedEventArgs>(VisualStateService_PointerExited));
			DropdownListVisualStateService visualStateService5 = _visualStateService;
			visualStateService5.GettingFocus = (EventHandler<GettingFocusEventArgs>)Delegate.Combine(visualStateService5.GettingFocus, new EventHandler<GettingFocusEventArgs>(VisualStateService_GettingFocus));
		}
		AddConfiguration();
		if (_overlayPopupService == null)
		{
			_overlayPopupService = new OverlayPopupService(base.XamlRoot);
			OverlayPopupService overlayPopupService = _overlayPopupService;
			overlayPopupService.CloseOverlayPopupEvent = (EventHandler<PointerRoutedEventArgs>)Delegate.Combine(overlayPopupService.CloseOverlayPopupEvent, new EventHandler<PointerRoutedEventArgs>(OverlayPopup_PointerPressed));
		}
	}

	private void UpdateToolTip()
	{
		_contentPresenter?.UpdateToolTip();
	}

	private void UpdateItemTextBehavior(ListViewItem listViewItem)
	{
		if (!(listViewItem == null))
		{
			TextBlock textBlock = UIExtensionsInternal.FindChildByName<TextBlock>("ContentPresenter", listViewItem);
			if (!(textBlock == null))
			{
				ConfigureTextWrapping(textBlock);
				ConfigureTextTrimmingBehavior(listViewItem, textBlock);
			}
		}
	}

	private void ConfigureTextWrapping(TextBlock textBlock)
	{
		textBlock.TextWrapping = ((!IsMultilineItem) ? TextWrapping.NoWrap : TextWrapping.Wrap);
	}

	private void ConfigureTextTrimmingBehavior(ListViewItem listViewItem, TextBlock textBlock)
	{
		UpdateToolTipForTrimmedText(listViewItem, textBlock);
		TypedEventHandler<TextBlock, IsTextTrimmedChangedEventArgs> textTrimmedChangedHandler = delegate
		{
			UpdateToolTipForTrimmedText(listViewItem, textBlock);
		};
		RoutedEventHandler textBlockUnloadedHandler = null;
		textBlockUnloadedHandler = delegate
		{
			textBlock.IsTextTrimmedChanged -= textTrimmedChangedHandler;
			textBlock.Unloaded -= textBlockUnloadedHandler;
			textTrimmedChangedHandler = null;
		};
		textBlock.IsTextTrimmedChanged += textTrimmedChangedHandler;
		textBlock.Unloaded += textBlockUnloadedHandler;
	}

	private void UpdateToolTipForTrimmedText(ListViewItem listViewItem, TextBlock textBlock)
	{
		if (textBlock.IsTextTrimmed && !string.IsNullOrEmpty(textBlock.Text))
		{
			ToolTipService.SetToolTip(listViewItem, new ToolTip
			{
				Content = textBlock.Text
			});
		}
		else
		{
			ToolTipService.SetToolTip(listViewItem, null);
		}
	}

	private void PressedEnterOrSpaceKeys(object sender)
	{
		if (sender is ListViewItem listViewItem)
		{
			DropdownListViewCustom listView = _listView;
			if ((object)listView != null && listView.Items.Count > 0 && (_listView.SelectedItem != listViewItem.Content || _listView.SelectedItem != listViewItem.DataContext))
			{
				_listView.SelectedItem = listViewItem.DataContext ?? listViewItem.Content;
				ClosePopup(isClosingFromKeyboard: true);
			}
		}
	}

	private void ClosePopup(bool isClosingFromKeyboard = false)
	{
		if (_isAnimationRunning || !_popup.IsOpen)
		{
			return;
		}
		_isAnimationRunning = true;
		_flyoutAnimation.CloseAnimation(_popup, delegate
		{
			if (!(_popup == null))
			{
				_popup.IsOpen = false;
				_overlayPopupService.CloseOverlayPopup();
				_isAnimationRunning = false;
				if (isClosingFromKeyboard)
				{
					_contentPresenter?.Focus(FocusState.Keyboard);
				}
			}
		});
	}

	private void PressedUpOrDownKeys(object sender)
	{
		if (sender is ListViewItem listViewItem && _listView != null && _listView.Items.Count > 0)
		{
			listViewItem.Focus(FocusState.Keyboard);
		}
	}

	private void AddListViewItemEvents(ListViewItem listViewItem)
	{
		if (listViewItem != null)
		{
			listViewItem.PreviewKeyDown += ListViewItem_PreviewKeyDown;
		}
	}

	private void RemoveListViewItemEvents(ListViewItem listViewItem)
	{
		if (listViewItem != null)
		{
			listViewItem.PreviewKeyDown -= ListViewItem_PreviewKeyDown;
		}
	}

	internal void SetNarratorContent()
	{
		if (!(_contentPresenter == null))
		{
			string text = SetNullToStringEmpty(Header) + " ";
			text = ((SelectedItem != null) ? (text + SelectedItem.ToString()) : (text + SetNullToStringEmpty(Placeholder)));
			string value = $"{text}, {GetTranslation("GS_DROPDOWN_LIST_TTS")}, {GetTranslation("DREAM_COLLAPSED_M_STATUS_TBOPT")}".Trim(',');
			AutomationProperties.SetName(_contentPresenter, value);
		}
	}

	private string SetNullToStringEmpty(object property)
	{
		if (property == null)
		{
			return string.Empty;
		}
		return property.ToString();
	}

	private void UpdateMargins(Rect dropdownScreenPosition)
	{
		UpdatePopupMargin();
		_dropdownScrollViewerService.UpdateMarginWhenOutOfBounds(dropdownScreenPosition, this, _popup);
	}

	private void UpdatePopupMargin()
	{
		if (VerifyHorizontalAlignment(HorizontalAlignment.Right))
		{
			_popup.Margin = new Thickness(_dropdownListRelativePanel.ActualWidth - _scrollViewer.ActualWidth, 0.0, 0.0, 0.0);
		}
		else if (VerifyHorizontalAlignment(HorizontalAlignment.Center))
		{
			_popup.Margin = new Thickness((_dropdownListRelativePanel.ActualWidth - _scrollViewer.ActualWidth) / 2.0, 0.0, 0.0, 0.0);
		}
		else
		{
			_popup.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
		}
	}

	private bool VerifyHorizontalAlignment(HorizontalAlignment horizontalAlignment)
	{
		if (_dropdownListRelativePanel != null && _scrollViewer != null)
		{
			return DropdownPopupAlignment == horizontalAlignment;
		}
		return false;
	}

	private void AddConfiguration()
	{
		if (_listView != null)
		{
			_listView.SelectionChanged += ListView_SelectionChanged;
			_listView.IsItemClickEnabled = true;
			_listView.ItemClick += ListView_ItemClick;
		}
		if (_contentPresenter != null)
		{
			_contentPresenter.PointerPressed += ContentPresenter_PointerPressed;
			_contentPresenter.PreviewKeyDown += ContentPresenter_PreviewKeyDown;
			_contentPresenter.PointerReleased += ContentPresenter_PointerReleased;
			_contentPresenter.PointerExited += ContentPresenter_PointerExited;
			_contentPresenter.AutomationInvokeRequested += ContentPresenter_Invoked;
			if (_listView != null && _listView.SelectedItem != null)
			{
				_contentPresenter.Content = GetSelectedItemContent();
			}
			_wasPointerPressed = false;
		}
		if (_rootGrid != null && Type == DropdownListType.SubAppBar)
		{
			_visualStateService.AddConfiguration(this, _rootGrid);
		}
		if (_popup != null && _listView != null)
		{
			_listView.Loaded += ListView_Loaded;
			_listView.Unloaded += ListView_Unloaded;
		}
		UpdateHeader();
	}

	private void ContentPresenter_Invoked(object sender, EventArgs e)
	{
		OpenPopup();
	}

	private object GetSelectedItemContent()
	{
		if (!(_listView.SelectedItem is TextBlock))
		{
			return _listView.SelectedItem?.ToString();
		}
		return _listView.SelectedItem;
	}

	private void UpdateHeader()
	{
		if (_headerContent != null)
		{
			if (!string.IsNullOrWhiteSpace(Header?.ToString()))
			{
				_headerContent.Visibility = Visibility.Visible;
				_headerContent.Content = Header;
			}
			else
			{
				_headerContent.Visibility = Visibility.Collapsed;
			}
			SetNarratorContent();
		}
	}

	private void UpdatePlaceholderText()
	{
		if (_contentPresenter != null && _listView != null && _listView.SelectedItem == null)
		{
			_contentPresenter.Content = Placeholder;
			SetNarratorContent();
		}
	}

	private void CreateScrollDispatcherTimer()
	{
		if (_dispatcherTimers == null)
		{
			_dispatcherTimers = new DispatcherTimer();
		}
		DispatcherTimer dispatcherTimer = new DispatcherTimer();
		dispatcherTimer.Tick += async delegate
		{
			dispatcherTimer.Stop();
			await BringListItemIntoViewAsync();
		};
		_dispatcherTimers = dispatcherTimer;
	}

	internal async Task BringListItemIntoViewAsync()
	{
		Microsoft.UI.Dispatching.DispatcherQueue forCurrentThread = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
		if (!(forCurrentThread != null))
		{
			return;
		}
		await forCurrentThread.EnqueueAsync(delegate
		{
			object selectedItem = _listView.SelectedItem;
			if (selectedItem != null)
			{
				FrameworkElement obj = (FrameworkElement)_listView.ContainerFromItem(selectedItem);
				if (obj == null)
				{
					_listView.ScrollIntoView(selectedItem);
				}
				obj?.StartBringIntoView();
			}
		});
	}

	private void UpdateTransformCenterPosition()
	{
		if (!(_popupScaleTransform == null))
		{
			_popupScaleTransform.CenterX = _popup.HorizontalOffset;
			if (DropdownPopupAlignment == HorizontalAlignment.Right)
			{
				_popupScaleTransform.CenterX = base.ActualWidth + _popup.HorizontalOffset;
			}
			else if (DropdownPopupAlignment == HorizontalAlignment.Center)
			{
				_popupScaleTransform.CenterX = base.ActualWidth / 2.0 + _popup.HorizontalOffset;
			}
			_popupScaleTransform.CenterY = _popup.VerticalOffset;
		}
	}

	private string GetTranslation(string resourceKey)
	{
		return ResourceExtensions.GetLocalized(resourceKey);
	}

	public void OpenPopup()
	{
		if (!_isAnimationRunning)
		{
			if (_popup != null)
			{
				_overlayPopupService.OpenOverlayPopup();
				_popup.IsOpen = true;
				_popup.HorizontalOffset = HorizontalOffset;
				_popup.VerticalOffset = VerticalOffset;
				_dropdownListViewService?.UpdateMaxHeight(_dropdownListRelativePanel.XamlRoot.Size.Height);
			}
			_dispatcherTimers?.Start();
		}
	}

	public void ClosePopup()
	{
		ClosePopup(false);
		_isOpenedByKeyboard = false;
	}
}
