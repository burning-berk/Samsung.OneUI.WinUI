using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Samsung.OneUI.WinUI.Utils.Extensions;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.System;

namespace Samsung.OneUI.WinUI.Controls;

[Obsolete("NavigationRail is deprecated, please use NavigationView instead.")]
public class NavigationRail : Microsoft.UI.Xaml.Controls.NavigationView
{
	private const string CONTENT_GRID = "ContentGrid";

	private const string MENU_ITEMS_GRID_NAME = "MenuItemsGrid";

	private const string ROOT_SPLITVIEW = "RootSplitView";

	private const string SETTINGS_PANE_ITEM = "SettingsNavPaneItem";

	private const string RESPONSIVITY_STATES_GROUP = "ResponsivityStates";

	private const string MENU_ITEMS_HOST = "MenuItemsHost";

	private const string SMALL_BREAKPOINT_NAME = "SmallNavigationRailBreakpoint";

	private const string LARGE_BREAKPOINT_NAME = "LargeNavigationRailBreakpoint";

	private const string SETTINGS_STRING_TRANSLATION_ID = "DREAM_SETTINGS_BUTTON21";

	private const string DEFAULT_OPEN_PANE_LENGTH = "OneUINavigationRailOpenPaneLength";

	private const string DEFAULT_COMPACT_PANE_LENGTH = "OneUINavigationRailCompactPaneLength";

	private const string MENU_BUTTON = "TogglePaneButton";

	private const string PANE_AUTO_SUGGEST_BUTTON = "PaneAutoSuggestButton";

	private const double DEFAULT_OPACITY_VALUE = 1.0;

	private const double MINIMUM_OPACITY_VALUE = 0.15;

	private const double PANE_COLLAPSED_LENGTH = 0.0;

	private const double DEFAULT_BREAK_POINT_VALUE = 640.0;

	private VisualStateGroup _visualStateGroup;

	private Grid _contentGrid;

	private ScrollViewer _menuItemsGrid;

	private ItemsRepeater _menuItemsHost;

	private ScrollViewer _navigationListScrollViewer;

	private NavigationRailAnimationService _navigationRailAnimationService;

	internal SplitView rootSplitView;

	internal NavigationRailItem settingsNavPaneItem;

	internal double defaultOpenPaneLength;

	internal double defaultCompactPaneLength;

	private readonly Dictionary<DependencyProperty, long> _callbackTokens = new Dictionary<DependencyProperty, long>();

	public static readonly DependencyProperty IsPaneAutoCompactEnabledProperty = DependencyProperty.Register("IsPaneAutoCompactEnabled", typeof(bool), typeof(NavigationRail), new PropertyMetadata(true));

	public static readonly DependencyProperty IsInitialPaneOpenProperty = DependencyProperty.Register("IsInitialPaneOpen", typeof(bool?), typeof(NavigationRail), new PropertyMetadata(null));

	public static readonly DependencyProperty PaneToggleNotificationBadgeProperty = DependencyProperty.Register("PaneToggleNotificationBadge", typeof(BadgeBase), typeof(NavigationRail), new PropertyMetadata(null));

	public static readonly DependencyProperty SettingsNavigationItemNotificationBadgeProperty = DependencyProperty.Register("SettingsNavigationItemNotificationBadge", typeof(BadgeBase), typeof(NavigationRail), new PropertyMetadata(null));

	public static readonly DependencyProperty CollapseBreakPointProperty = DependencyProperty.Register("CollapseBreakPoint", typeof(double), typeof(NavigationRail), new PropertyMetadata(640.0, OnBreakPointChanged));

	public static readonly DependencyProperty ExpandBreakPointProperty = DependencyProperty.Register("ExpandBreakPoint", typeof(double), typeof(NavigationRail), new PropertyMetadata(640.0, OnBreakPointChanged));

	public bool IsPaneAutoCompactEnabled
	{
		get
		{
			return (bool)GetValue(IsPaneAutoCompactEnabledProperty);
		}
		set
		{
			SetValue(IsPaneAutoCompactEnabledProperty, value);
		}
	}

	public bool? IsInitialPaneOpen
	{
		get
		{
			return (bool?)GetValue(IsInitialPaneOpenProperty);
		}
		set
		{
			SetValue(IsInitialPaneOpenProperty, value);
		}
	}

	public BadgeBase PaneToggleNotificationBadge
	{
		get
		{
			return (BadgeBase)GetValue(PaneToggleNotificationBadgeProperty);
		}
		set
		{
			SetValue(PaneToggleNotificationBadgeProperty, value);
		}
	}

	public BadgeBase SettingsNavigationItemNotificationBadge
	{
		get
		{
			return (BadgeBase)GetValue(SettingsNavigationItemNotificationBadgeProperty);
		}
		set
		{
			SetValue(SettingsNavigationItemNotificationBadgeProperty, value);
		}
	}

	public double CollapseBreakPoint
	{
		get
		{
			return (double)GetValue(CollapseBreakPointProperty);
		}
		set
		{
			SetValue(CollapseBreakPointProperty, Math.Max(value, 640.0));
		}
	}

	public double ExpandBreakPoint
	{
		get
		{
			return (double)GetValue(ExpandBreakPointProperty);
		}
		set
		{
			SetValue(ExpandBreakPointProperty, Math.Max(value, 640.0));
		}
	}

	private static void OnBreakPointChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is NavigationRail navigationRail && navigationRail.ExpandBreakPoint < navigationRail.CollapseBreakPoint)
		{
			navigationRail.ExpandBreakPoint = navigationRail.CollapseBreakPoint;
		}
	}

	public NavigationRail()
	{
		base.DefaultStyleKey = typeof(NavigationRail);
		base.Loaded += NavigationRail_Loaded;
		base.Unloaded += NavigationRail_Unloaded;
	}

	protected override void OnApplyTemplate()
	{
		rootSplitView = GetTemplateChild("RootSplitView") as SplitView;
		_contentGrid = GetTemplateChild("ContentGrid") as Grid;
		_menuItemsHost = GetTemplateChild("MenuItemsHost") as ItemsRepeater;
		if (_menuItemsHost != null)
		{
			_menuItemsHost.AddHandler(UIElement.PointerReleasedEvent, new PointerEventHandler(Menuhost_PointerReleased), handledEventsToo: true);
			_menuItemsHost.KeyDown += MenuItemsGrid_KeyHandle;
			_menuItemsHost.KeyUp += MenuItemsGrid_KeyHandle;
		}
		_menuItemsGrid = (ScrollViewer)GetTemplateChild("MenuItemsGrid");
		if (_menuItemsGrid != null)
		{
			_menuItemsGrid.ManipulationDelta += MenuItemsHost_ManipulationDelta;
		}
		settingsNavPaneItem = (NavigationRailItem)GetTemplateChild("SettingsNavPaneItem");
		if (settingsNavPaneItem != null)
		{
			ApplySettingsTranslationString();
			settingsNavPaneItem.Tapped += SettingsItem_Tapped;
			settingsNavPaneItem.KeyDown += SettingsNavPaneItem_KeyDown;
		}
		_visualStateGroup = (VisualStateGroup)GetTemplateChild("ResponsivityStates");
		if (IsPaneAutoCompactEnabled && _visualStateGroup != null)
		{
			_visualStateGroup.CurrentStateChanged += VisualStateGroup_CurrentStateChanged;
		}
		defaultOpenPaneLength = (double)Application.Current.Resources["OneUINavigationRailOpenPaneLength"];
		defaultCompactPaneLength = (double)Application.Current.Resources["OneUINavigationRailCompactPaneLength"];
		AssignMenuItemHostScrollEvent();
		base.OnApplyTemplate();
	}

	private void SetOpenPaneLengthValue()
	{
		if (!(rootSplitView == null))
		{
			base.OpenPaneLength = ((!base.IsPaneVisible) ? 0.0 : ((!base.IsPaneOpen) ? defaultCompactPaneLength : defaultOpenPaneLength));
			base.CompactPaneLength = ((!base.IsPaneVisible) ? 0.0 : defaultCompactPaneLength);
		}
	}

	private void NavigationRail_Loaded(object sender, RoutedEventArgs e)
	{
		_navigationRailAnimationService = new NavigationRailAnimationService(this);
		DetermineIsPaneOpen();
		SetOpenPaneLengthValue();
		RegisterPropertyChanged();
		AssignScrollViewerEvents();
		ManageToolTipByDisplayMode();
	}

	private void DetermineIsPaneOpen()
	{
		if (IsExpandedLeft() && IsPaneAutoCompactEnabled)
		{
			base.IsPaneOpen = !IsSmallBreakpointResponsivityState();
		}
		if (IsInitialPaneOpen.HasValue)
		{
			base.IsPaneOpen = IsInitialPaneOpen.Value;
		}
	}

	private void RegisterPropertyChanged()
	{
		_callbackTokens[Microsoft.UI.Xaml.Controls.NavigationView.IsPaneOpenProperty] = RegisterPropertyChangedCallback(Microsoft.UI.Xaml.Controls.NavigationView.IsPaneOpenProperty, delegate
		{
			if (base.IsPaneOpen)
			{
				RemoveToolTipOfNavigationRail();
			}
			else
			{
				AddToolTipForNavigationRail();
			}
		});
		_callbackTokens[Microsoft.UI.Xaml.Controls.NavigationView.IsPaneVisibleProperty] = RegisterPropertyChangedCallback(Microsoft.UI.Xaml.Controls.NavigationView.IsPaneVisibleProperty, delegate
		{
			DetermineIsPaneOpen();
			SetOpenPaneLengthValue();
		});
		_callbackTokens[IsPaneAutoCompactEnabledProperty] = RegisterPropertyChangedCallback(IsPaneAutoCompactEnabledProperty, delegate
		{
			ManageRegisterEventBasedOnPaneAutoCompact();
		});
	}

	private void ManageRegisterEventBasedOnPaneAutoCompact()
	{
		if (_visualStateGroup != null)
		{
			if (IsPaneAutoCompactEnabled)
			{
				_visualStateGroup.CurrentStateChanged -= VisualStateGroup_CurrentStateChanged;
				_visualStateGroup.CurrentStateChanged += VisualStateGroup_CurrentStateChanged;
			}
			else
			{
				_visualStateGroup.CurrentStateChanged -= VisualStateGroup_CurrentStateChanged;
			}
		}
	}

	private void ManageToolTipByDisplayMode()
	{
		switch (base.PaneDisplayMode)
		{
		case NavigationViewPaneDisplayMode.Left:
			AddToolTipForNavigationRail();
			break;
		case NavigationViewPaneDisplayMode.Top:
			if (ContainerFromMenuItem(base.SettingsItem) is NavigationViewItem targetElement)
			{
				SetToolTip(targetElement, settingsNavPaneItem.Content);
			}
			break;
		default:
			AddToolTipForNavigationRail();
			break;
		}
	}

	private void AddToolTipForNavigationRail()
	{
		ExecuteActionOnElement("TogglePaneButton", delegate(Button element)
		{
			SetToolTip(element, GetDefaultContentToolTip(element));
		});
		ExecuteActionOnElement("PaneAutoSuggestButton", delegate(Button element)
		{
			SetToolTip(element, GetDefaultContentToolTip(element));
		});
		SetToolTip(settingsNavPaneItem, settingsNavPaneItem.Content);
		base.MenuItems.OfType<NavigationRailItem>().ToList().ForEach(delegate(NavigationRailItem item)
		{
			SetToolTip(item, item.Content);
		});
	}

	private void RemoveToolTipOfNavigationRail()
	{
		ExecuteActionOnElement("TogglePaneButton", delegate(Button element)
		{
			SetToolTip(element, GetDefaultContentToolTip(element));
		});
		ToolTipService.SetToolTip(settingsNavPaneItem, null);
		base.MenuItems.OfType<NavigationRailItem>().ToList().ForEach(delegate(NavigationRailItem item)
		{
			ToolTipService.SetToolTip(item, null);
		});
	}

	private void ExecuteActionOnElement<T>(string elementName, Action<T> action) where T : FrameworkElement
	{
		if (GetTemplateChild(elementName) is T obj)
		{
			action(obj);
		}
	}

	private void SetToolTip(DependencyObject targetElement, object contentToolTip)
	{
		ToolTipService.SetToolTip(targetElement, new ToolTip
		{
			Content = contentToolTip
		});
	}

	private object GetDefaultContentToolTip(FrameworkElement element)
	{
		return (ToolTipService.GetToolTip(element) as Microsoft.UI.Xaml.Controls.ToolTip)?.Content;
	}

	private void NavigationRail_Unloaded(object sender, RoutedEventArgs e)
	{
		_navigationListScrollViewer.PointerWheelChanged -= NavigationListScrollViewer_PointerWheelChanged;
		if (_visualStateGroup != null)
		{
			_visualStateGroup.CurrentStateChanged -= VisualStateGroup_CurrentStateChanged;
		}
		foreach (KeyValuePair<DependencyProperty, long> callbackToken in _callbackTokens)
		{
			UnregisterPropertyChangedCallback(callbackToken.Key, callbackToken.Value);
		}
	}

	private void AssignScrollViewerEvents()
	{
		if (!(_menuItemsGrid == null))
		{
			_navigationListScrollViewer = _menuItemsGrid.GetScrollViewer();
			if (!(_navigationListScrollViewer == null))
			{
				_navigationListScrollViewer.PointerWheelChanged -= NavigationListScrollViewer_PointerWheelChanged;
				_navigationListScrollViewer.PointerWheelChanged += NavigationListScrollViewer_PointerWheelChanged;
			}
		}
	}

	private void AssignMenuItemHostScrollEvent()
	{
		if (_menuItemsGrid != null)
		{
			_menuItemsGrid.ManipulationDelta += MenuItemsHost_ManipulationDelta;
		}
	}

	internal void RemoveShadowFromSplitViewPane(SplitView splitView)
	{
		splitView.Pane.Translation = Vector3.Zero;
	}

	private void Menuhost_PointerReleased(object sender, PointerRoutedEventArgs e)
	{
		if ((object)settingsNavPaneItem != null)
		{
			settingsNavPaneItem.IsSelected = false;
		}
	}

	private void ApplySettingsTranslationString()
	{
		string localized = "DREAM_SETTINGS_BUTTON21".GetLocalized();
		if (!string.IsNullOrWhiteSpace(localized))
		{
			settingsNavPaneItem.Content = localized;
		}
	}

	private void NavigationListScrollViewer_PointerWheelChanged(object sender, PointerRoutedEventArgs e)
	{
		double num = e.GetCurrentPoint(_navigationListScrollViewer).Properties.MouseWheelDelta;
		double value = _navigationListScrollViewer.VerticalOffset - num;
		_navigationListScrollViewer.ChangeView(null, value, null);
	}

	private void MenuItemsHost_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
	{
		if (IsPenOrTouch(e) && (IsOpenedPaneScrollingAllowed() || IsCompactedPaneScroollingAllowed()))
		{
			double value = _navigationListScrollViewer.VerticalOffset - e.Delta.Translation.Y;
			_navigationListScrollViewer.ChangeView(null, value, null, disableAnimation: true);
		}
	}

	private bool IsPenOrTouch(ManipulationDeltaRoutedEventArgs e)
	{
		if (e.PointerDeviceType != PointerDeviceType.Pen)
		{
			return e.PointerDeviceType == PointerDeviceType.Touch;
		}
		return true;
	}

	private ScrollViewer GetScrollViewer(DependencyObject depObj)
	{
		if (depObj is ScrollViewer)
		{
			return depObj as ScrollViewer;
		}
		for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
		{
			DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
			ScrollViewer scrollViewer = GetScrollViewer(child);
			if (scrollViewer != null)
			{
				return scrollViewer;
			}
		}
		return null;
	}

	private void VisualStateGroup_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
	{
		if (!(_contentGrid == null) && IsPaneAutoCompactEnabled && base.IsPaneVisible)
		{
			UpdateResponsivity();
		}
	}

	private void SettingsNavPaneItem_KeyDown(object sender, KeyRoutedEventArgs e)
	{
		if (e.Key == VirtualKey.Enter || e.Key == VirtualKey.Space)
		{
			SelectSettingsButton();
		}
	}

	private void SettingsItem_Tapped(object sender, TappedRoutedEventArgs e)
	{
		SelectSettingsButton();
	}

	private void MenuItemsGrid_KeyHandle(object sender, KeyRoutedEventArgs e)
	{
		if ((object)settingsNavPaneItem != null && (e.Key == VirtualKey.Enter || e.Key == VirtualKey.Space))
		{
			settingsNavPaneItem.IsSelected = false;
		}
	}

	private void SelectSettingsButton()
	{
		base.SelectedItem = base.SettingsItem;
		settingsNavPaneItem.IsSelected = true;
	}

	private void UpdateResponsivity()
	{
		if (IsExpandedLeft())
		{
			if (IsLargeBreakpointResponsivityState())
			{
				_navigationRailAnimationService.RunOpenPaneAnimation();
				_navigationRailAnimationService.ConfigureOpacityAnimation(0.15, 1.0);
			}
			if (IsSmallBreakpointResponsivityState())
			{
				_navigationRailAnimationService.RunCompactPaneAnimation();
				_navigationRailAnimationService.ConfigureOpacityAnimation(1.0, 0.15);
			}
		}
	}

	private bool IsSmallBreakpointResponsivityState()
	{
		if (_visualStateGroup == null || _visualStateGroup.CurrentState == null)
		{
			return false;
		}
		return _visualStateGroup.CurrentState.Name == "SmallNavigationRailBreakpoint";
	}

	private bool IsLargeBreakpointResponsivityState()
	{
		if (_visualStateGroup == null || _visualStateGroup.CurrentState == null)
		{
			return false;
		}
		return _visualStateGroup.CurrentState.Name == "LargeNavigationRailBreakpoint";
	}

	private bool IsExpandedLeft()
	{
		if (base.DisplayMode == NavigationViewDisplayMode.Expanded)
		{
			return base.PaneDisplayMode == NavigationViewPaneDisplayMode.Left;
		}
		return false;
	}

	private bool IsOpenedPaneScrollingAllowed()
	{
		if (base.IsPaneOpen)
		{
			return rootSplitView.OpenPaneLength.Equals(defaultOpenPaneLength);
		}
		return false;
	}

	private bool IsCompactedPaneScroollingAllowed()
	{
		if (base.IsPaneOpen.Equals(obj: false))
		{
			return rootSplitView.CompactPaneLength.Equals(defaultCompactPaneLength);
		}
		return false;
	}
}
