using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Samsung.OneUI.WinUI.AttachedProperties;
using Samsung.OneUI.WinUI.Controls.EventHandlers;
using Samsung.OneUI.WinUI.Controls.Toolbar.Commandbar;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.UI.ViewManagement;

namespace Samsung.OneUI.WinUI.Controls;

public sealed class CommandBar : Microsoft.UI.Xaml.Controls.CommandBar
{
	private const string PART_BACK_BUTTON = "BackButton";

	private const string PRIMARY_ITEMS_CONTROL = "PrimaryItemsControl";

	private const string TITLE_CONTENT_CONTROL = "ContentControl";

	private const string TITLE_STYLE = "OneUICommandBarTitleStyle";

	private const string SUB_TITLE_TEXT_BLOCK = "SubtitleTextBlock";

	private const string TITLE_COLUMN_CONTROL = "ContentControlColumnDefinition";

	private const string PRIMARY_COLUMN_CONTROL = "PrimaryItemsControlColumnDefinition";

	private const string MORE_OPTIONS_NAME = "ListFlyoutItems";

	private const string MORE_OPTIONS_BUTTON = "MoreButton";

	private const string NOTIFICATION_BADGE_ROOT_GRID = "NotificationBadgeRootGrid";

	private const string TEXTBADGE_TEXTBLOCK_NAME = "TextBlockBadgeText";

	private const string TEXTBADGE_TEXTBLOCK_DEFAULT_TEXT = "New";

	private const string ALERTBADGE_TEXTBLOCK_DEFAULT_TEXT = "!";

	private const string NARRATOR_COMPLEMENT = "App bar";

	private const double ITEMS_MAX_WIDTH_PERCENTAGE = 0.6;

	internal const double TOOLTIP_VERTICAL_OFFSET = 10.0;

	private const double ITEMS_MAX_WIDTH_WITHOUT_CONTENT_PERCENTAGE = 0.85;

	private Button _backButton;

	private ItemsControl _itemsControl;

	private ContentControl _titleControl;

	private TextBlock _subTitleTextBlock;

	private ColumnDefinition _titleColumn;

	private ColumnDefinition _itemsColumn;

	private CommandBarOverflowIconsManager _commandBarOverflowIconsManager;

	private CommandBarBackButtonManager _commandBarBackButtonManager;

	private CommandBarListFlyoutManager _commandBarListFlyoutManager;

	private Button _moreOptionsButton;

	private Grid _notificationBadgeRootGrid;

	private TextBlock _notificationBadgeTextBlock;

	private readonly Dictionary<DependencyProperty, long> _callbackTokens = new Dictionary<DependencyProperty, long>();

	private readonly UISettings _uiSettings;

	public static readonly DependencyProperty BackButtonCommandProperty = DependencyProperty.Register("BackButtonCommand", typeof(ICommand), typeof(CommandBar), new PropertyMetadata(null));

	public static readonly DependencyProperty BackButtonCommandParameterProperty = DependencyProperty.Register("BackButtonCommandParameter", typeof(object), typeof(CommandBar), new PropertyMetadata(null));

	public static readonly DependencyProperty MoreOptionsItemsProperty = DependencyProperty.Register("MoreOptionsItems", typeof(ObservableCollection<MenuFlyoutItemBase>), typeof(CommandBar), new PropertyMetadata(new ObservableCollection<MenuFlyoutItemBase>()));

	public static readonly DependencyProperty IsBackButtonVisibleProperty = DependencyProperty.Register("IsBackButtonVisible", typeof(bool), typeof(CommandBar), new PropertyMetadata(true, OnIsBackButtonVisiblePropertyChanged));

	public static readonly DependencyProperty IsOptionsButtonVisibleProperty = DependencyProperty.Register("IsOptionsButtonVisible", typeof(bool), typeof(CommandBar), new PropertyMetadata(true));

	public static readonly DependencyProperty BackButtonTypeProperty = DependencyProperty.Register("BackButtonType", typeof(CommandBarBackButtonType), typeof(CommandBarBackButtonType), new PropertyMetadata(CommandBarBackButtonType.Default, BackButtonTypeChanged));

	public static readonly DependencyProperty MoreOptionsBadgeProperty = DependencyProperty.Register("MoreOptionsBadge", typeof(BadgeBase), typeof(CommandBar), new PropertyMetadata(null, OnMoreOptionsBadgeChanged));

	public static readonly DependencyProperty MoreOptionsHorizontalOffsetProperty = DependencyProperty.Register("MoreOptionsHorizontalOffset", typeof(double), typeof(CommandBar), new PropertyMetadata(0.0, OnMoreOptionsFlyoutContentChanged));

	public static readonly DependencyProperty MoreOptionsVerticalOffsetProperty = DependencyProperty.Register("MoreOptionsVerticalOffset", typeof(double), typeof(CommandBar), new PropertyMetadata(0.0, OnMoreOptionsVerticalOffsetPropertyChanged));

	public static readonly DependencyProperty MoreOptionsPlacementProperty = DependencyProperty.Register("MoreOptionsPlacement", typeof(FlyoutPlacementMode?), typeof(CommandBar), new PropertyMetadata(null));

	public static readonly DependencyProperty MoreOptionsToolTipContentProperty = DependencyProperty.Register("MoreOptionsToolTipContent", typeof(string), typeof(CommandBar), new PropertyMetadata(string.Empty, OnMoreOptionsToolTipContentPropertyChanged));

	internal static readonly DependencyProperty BackButtonToolTipContentProperty = DependencyProperty.Register("BackButtonToolTipContent", typeof(string), typeof(CommandBar), new PropertyMetadata(string.Empty, OnBackButtonToolTipContentPropertyChanged));

	public static readonly DependencyProperty SubtitleProperty = DependencyProperty.Register("SubtitleText", typeof(string), typeof(CommandBar), new PropertyMetadata("", UpdateNarratorSubAppBar));

	public static readonly DependencyProperty IsSubtitleVisibleProperty = DependencyProperty.Register("IsSubtitleVisible", typeof(bool), typeof(CommandBar), new PropertyMetadata(false, OnChanged));

	public double CurrentItemsMaxWidth { get; private set; }

	public ObservableCollection<MenuFlyoutItemBase> MoreOptionsOverflowItems { get; internal set; }

	public ICommand BackButtonCommand
	{
		get
		{
			return (ICommand)GetValue(BackButtonCommandProperty);
		}
		set
		{
			SetValue(BackButtonCommandProperty, value);
		}
	}

	public object BackButtonCommandParameter
	{
		get
		{
			return GetValue(BackButtonCommandParameterProperty);
		}
		set
		{
			SetValue(BackButtonCommandParameterProperty, value);
		}
	}

	public ObservableCollection<MenuFlyoutItemBase> MoreOptionsItems
	{
		get
		{
			return (ObservableCollection<MenuFlyoutItemBase>)GetValue(MoreOptionsItemsProperty);
		}
		set
		{
			SetValue(MoreOptionsItemsProperty, value);
		}
	}

	public bool IsBackButtonVisible
	{
		get
		{
			return (bool)GetValue(IsBackButtonVisibleProperty);
		}
		set
		{
			SetValue(IsBackButtonVisibleProperty, value);
		}
	}

	public bool IsOptionsButtonVisible
	{
		get
		{
			return (bool)GetValue(IsOptionsButtonVisibleProperty);
		}
		set
		{
			SetValue(IsOptionsButtonVisibleProperty, value);
		}
	}

	public CommandBarBackButtonType BackButtonType
	{
		get
		{
			return (CommandBarBackButtonType)GetValue(BackButtonTypeProperty);
		}
		set
		{
			SetValue(BackButtonTypeProperty, value);
		}
	}

	public BadgeBase MoreOptionsBadge
	{
		get
		{
			return (BadgeBase)GetValue(MoreOptionsBadgeProperty);
		}
		set
		{
			SetValue(MoreOptionsBadgeProperty, value);
		}
	}

	public double MoreOptionsHorizontalOffset
	{
		get
		{
			return (double)GetValue(MoreOptionsHorizontalOffsetProperty);
		}
		set
		{
			SetValue(MoreOptionsHorizontalOffsetProperty, value);
		}
	}

	public double MoreOptionsVerticalOffset
	{
		get
		{
			return (double)GetValue(MoreOptionsVerticalOffsetProperty);
		}
		set
		{
			SetValue(MoreOptionsVerticalOffsetProperty, value);
		}
	}

	[Obsolete("MoreOptionsPlacement is deprecated, please use MoreOptionsVerticalOffset and MoreOptionsHorizontalOffset instead.")]
	public FlyoutPlacementMode? MoreOptionsPlacement
	{
		get
		{
			return (FlyoutPlacementMode?)GetValue(MoreOptionsPlacementProperty);
		}
		set
		{
			SetValue(MoreOptionsPlacementProperty, value);
		}
	}

	public string MoreOptionsToolTipContent
	{
		get
		{
			return (string)GetValue(MoreOptionsToolTipContentProperty);
		}
		set
		{
			SetValue(MoreOptionsToolTipContentProperty, value);
		}
	}

	internal string BackButtonToolTipContent
	{
		get
		{
			return (string)GetValue(BackButtonToolTipContentProperty);
		}
		set
		{
			SetValue(BackButtonToolTipContentProperty, value);
		}
	}

	public string SubtitleText
	{
		get
		{
			return (string)GetValue(SubtitleProperty);
		}
		set
		{
			SetValue(SubtitleProperty, value);
		}
	}

	public bool IsSubtitleVisible
	{
		get
		{
			return (bool)GetValue(IsSubtitleVisibleProperty);
		}
		set
		{
			SetValue(IsSubtitleVisibleProperty, value);
		}
	}

	public event EventHandler BackButtonEvent;

	private static void OnIsBackButtonVisiblePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
	{
		if (sender is CommandBar commandBar)
		{
			commandBar.BackButtonVisible();
		}
	}

	private static void OnMoreOptionsBadgeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
	{
		if (sender is CommandBar commandBar)
		{
			commandBar.UpdateNotificationBadge(e.NewValue);
		}
	}

	private static void OnMoreOptionsFlyoutContentChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
	{
		if (sender is CommandBar commandBar)
		{
			commandBar._commandBarListFlyoutManager?.ChangeListflyoutPosition();
		}
	}

	private static void OnMoreOptionsVerticalOffsetPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
	{
		if (sender is CommandBar commandBar)
		{
			commandBar._commandBarListFlyoutManager?.ChangeListflyoutPosition();
		}
	}

	private static void OnMoreOptionsToolTipContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is CommandBar commandBar)
		{
			commandBar._commandBarListFlyoutManager?.UpdateMoreOptionsToolTipContent();
		}
	}

	private static void OnBackButtonToolTipContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is CommandBar commandBar)
		{
			commandBar._commandBarBackButtonManager?.UpdateStringResourcesForBackButton();
		}
	}

	private static void UpdateNarratorSubAppBar(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is CommandBar commandBar)
		{
			string textValueElement = GetTextValueElement(commandBar.Content);
			commandBar.SetNarrator(textValueElement, e.NewValue.ToString());
		}
	}

	private static void OnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is CommandBar commandBar)
		{
			commandBar.UpdateSubTitleTextBlockVisibility();
			commandBar.UpdateNarrator();
		}
	}

	public CommandBar()
	{
		base.Loaded += CommandBar_Loaded;
		MoreOptionsItems = new ObservableCollection<MenuFlyoutItemBase>();
		MoreOptionsOverflowItems = new ObservableCollection<MenuFlyoutItemBase>();
		base.SizeChanged += CommandBar_SizeChanged;
		_uiSettings = new UISettings();
		_uiSettings.TextScaleFactorChanged += async delegate
		{
			await UiSettings_TextScaleFactorChanged();
		};
		ApplyDefaultLabelPositionIfNotSupported();
		base.Unloaded += CommandBar_Unloaded;
	}

	public void UpdateDefaultLabelPositionMode(CommandBarDefaultLabelPosition newInitialPosition)
	{
		base.DefaultLabelPosition = newInitialPosition;
		_commandBarOverflowIconsManager?.SetInitialLabelPosition(newInitialPosition);
		_commandBarOverflowIconsManager?.UpdateButtonsLabelPositionAndOverflow();
	}

	public void UpdateTooltipsContent()
	{
		_commandBarBackButtonManager?.UpdateStringResourcesForBackButton();
		_commandBarListFlyoutManager?.UpdateMoreOptionsToolTipContent();
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		InitializeBackButton();
		InitializeListFlyout();
		InitializeOverflowIcons();
		_titleControl = GetTemplateChild("ContentControl") as ContentControl;
		_subTitleTextBlock = GetTemplateChild("SubtitleTextBlock") as TextBlock;
		_titleColumn = GetTemplateChild("ContentControlColumnDefinition") as ColumnDefinition;
		_itemsColumn = GetTemplateChild("PrimaryItemsControlColumnDefinition") as ColumnDefinition;
		_notificationBadgeRootGrid = GetTemplateChild("NotificationBadgeRootGrid") as Grid;
		UpdateNotificationBadge(MoreOptionsBadge);
		UpdateTitleContentControlVisibility();
		UpdateSubTitleTextBlockVisibility();
		UpdateNarrator();
		UpdateTitleContentControl();
		RegisterProperties();
	}

	protected override AutomationPeer OnCreateAutomationPeer()
	{
		return new CommandBarAutomationPeer(this);
	}

	private static string GetTextValueElement(object content)
	{
		if (!(content is TextBlock { Text: var text }))
		{
			if (content is string result)
			{
				return result;
			}
			return string.Empty;
		}
		return text;
	}

	private void UpdateNarrator()
	{
		string textValueElement = GetTextValueElement(base.Content);
		string textValueElement2 = GetTextValueElement(_subTitleTextBlock);
		SetNarrator(textValueElement, textValueElement2);
	}

	private void SetNarrator(string title, string subTitle)
	{
		string text = "";
		if (_titleControl != null && _titleControl.Visibility == Visibility.Visible)
		{
			text = title;
		}
		if (_subTitleTextBlock != null && _subTitleTextBlock.Visibility == Visibility.Visible)
		{
			text = text + " " + subTitle;
		}
		AutomationProperties.SetName(this, text);
		AutomationProperties.SetLocalizedControlType(this, "App bar");
	}

	private void RegisterProperties()
	{
		RegisterCallback(Microsoft.UI.Xaml.Controls.CommandBar.DefaultLabelPositionProperty, DefaultLabelPositionPropertyChanged);
		RegisterCallback(Microsoft.UI.Xaml.Controls.CommandBar.IsDynamicOverflowEnabledProperty, IsDynamicOverflowEnabledPropertyChanged);
		RegisterCallback(ContentControl.ContentProperty, ContentPropertyChanged);
		RegisterCallback(UIElement.VisibilityProperty, VisibilityPropertyChanged);
	}

	private void RegisterCallback(DependencyProperty property, DependencyPropertyChangedCallback callback)
	{
		if (_callbackTokens.TryGetValue(property, out var value))
		{
			UnregisterPropertyChangedCallback(property, value);
		}
		_callbackTokens[property] = RegisterPropertyChangedCallback(property, callback);
	}

	private void UnregisterProperties()
	{
		foreach (KeyValuePair<DependencyProperty, long> callbackToken in _callbackTokens)
		{
			UnregisterPropertyChangedCallback(callbackToken.Key, callbackToken.Value);
		}
	}

	private void InitializeOverflowIcons()
	{
		_itemsControl = GetTemplateChild("PrimaryItemsControl") as ItemsControl;
		if (_itemsControl != null)
		{
			_itemsControl.Loaded += ItemsControl_Loaded;
		}
		_commandBarOverflowIconsManager = new CommandBarOverflowIconsManager(_itemsControl, base.DefaultLabelPosition, this);
		base.IsDynamicOverflowEnabled = false;
		_commandBarOverflowIconsManager.Load();
	}

	private void ItemsControl_Loaded(object sender, RoutedEventArgs e)
	{
		UpdateLayout();
		CalculateItemsMaxWidth();
		_commandBarOverflowIconsManager?.UpdateButtonLayout();
	}

	private void InitializeBackButton()
	{
		_backButton = GetTemplateChild("BackButton") as Button;
		_commandBarBackButtonManager = new CommandBarBackButtonManager(_backButton, this);
		_commandBarBackButtonManager.Load();
	}

	private void InitializeListFlyout()
	{
		ListFlyout listFlyout = GetTemplateChild("ListFlyoutItems") as ListFlyout;
		listFlyout.MoreOptionClosing = (EventHandler<FlyoutBaseClosingEventArgs>)Delegate.Combine(listFlyout.MoreOptionClosing, new EventHandler<FlyoutBaseClosingEventArgs>(MoreOptionClosing));
		_moreOptionsButton = GetTemplateChild("MoreButton") as Button;
		_commandBarListFlyoutManager = new CommandBarListFlyoutManager(listFlyout, _moreOptionsButton, _backButton, this);
		_commandBarListFlyoutManager.Load();
	}

	private void UpdateNotificationBadge(object newValue)
	{
		if (!(newValue is NumberBadge numberBadge))
		{
			if (!(newValue is DotBadge))
			{
				if (!(newValue is TextBadge badgeBase))
				{
					if (newValue is AlertBadge badgeBase2)
					{
						AdjustMarginNotificationBadge(badgeBase2);
					}
				}
				else
				{
					AdjustMarginNotificationBadge(badgeBase);
				}
			}
			else
			{
				AdjustMarginDotBadge();
			}
		}
		else
		{
			AdjustMarginNotificationBadge(numberBadge);
			numberBadge.ValueChanged -= NumberBadge_ValueChanged;
			numberBadge.ValueChanged += NumberBadge_ValueChanged;
		}
	}

	private void AdjustMarginNotificationBadge(BadgeBase badgeBase)
	{
		string text;
		if (!(badgeBase is NumberBadge { Value: var value }))
		{
			if (!(badgeBase is TextBadge))
			{
				if (!(badgeBase is AlertBadge))
				{
					throw new InvalidOperationException("Tipo desconhecido de badgeBase");
				}
				text = "!";
			}
			else
			{
				text = ((_notificationBadgeTextBlock != null) ? _notificationBadgeTextBlock.Text : "New");
			}
		}
		else
		{
			text = value.ToString();
		}
		string text2 = text;
		if (text2.Length > 0 && _notificationBadgeRootGrid != null)
		{
			_notificationBadgeRootGrid.Margin = ((text2.Length == 1) ? new Thickness(18.0, 2.0, 0.0, 0.0) : new Thickness(3.0, 2.0, 0.0, 0.0));
		}
	}

	private void AdjustMarginDotBadge()
	{
		if (_notificationBadgeRootGrid != null)
		{
			_notificationBadgeRootGrid.Margin = new Thickness(24.0, 0.0, 0.0, 5.0);
		}
	}

	private void BackButtonVisible()
	{
		_commandBarBackButtonManager?.GoToStateBackButtonVisibility();
	}

	private void DefaultLabelPositionPropertyChanged(DependencyObject sender, DependencyProperty dp)
	{
		ApplyDefaultLabelPositionIfNotSupported();
	}

	private void ApplyDefaultLabelPositionIfNotSupported()
	{
		if (base.DefaultLabelPosition == CommandBarDefaultLabelPosition.Bottom)
		{
			UpdateDefaultLabelPositionMode(CommandBarDefaultLabelPosition.Right);
		}
	}

	private static void BackButtonTypeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
	{
		if (sender is CommandBar commandBar)
		{
			commandBar._commandBarBackButtonManager?.AssignBackButtonClickEvent();
			commandBar._commandBarBackButtonManager?.ApplyBackButtonStyle();
		}
	}

	private async Task UiSettings_TextScaleFactorChanged()
	{
		await base.DispatcherQueue.EnqueueAsync(delegate
		{
			_commandBarOverflowIconsManager?.UpdateButtonLayout();
		});
	}

	private void UpdateTitleContentControl()
	{
		if (!(_titleControl == null) && base.Content is string text)
		{
			TextBlock textBlock = new TextBlock
			{
				Style = (Application.Current.Resources["OneUICommandBarTitleStyle"] as Style),
				Text = text
			};
			Tooltip.SetTextTrimmedEnabled(textBlock, value: true);
			_titleControl.Content = textBlock;
		}
	}

	protected override void OnContentChanged(object oldContent, object newContent)
	{
		base.OnContentChanged(oldContent, newContent);
		TextBlock subTitleTextBlock = _subTitleTextBlock;
		if ((object)subTitleTextBlock != null && newContent is TextBlock textBlock)
		{
			SetNarrator(textBlock.Text, subTitleTextBlock.Text);
		}
	}

	private void CommandBar_Loaded(object sender, RoutedEventArgs e)
	{
		_commandBarOverflowIconsManager?.Load();
		if (MoreOptionsBadge != null)
		{
			MoreOptionsBadge.Loaded += MoreOptionsBadge_Loaded;
		}
	}

	private void CommandBar_Unloaded(object sender, RoutedEventArgs e)
	{
		UnregisterProperties();
	}

	private void MoreOptionClosing(object sender, FlyoutBaseClosingEventArgs e)
	{
		base.IsOpen = false;
	}

	private void NumberBadge_ValueChanged(object sender, NumberBadgeValueChangedEventArgs e)
	{
		if (sender is NumberBadge badgeBase)
		{
			AdjustMarginNotificationBadge(badgeBase);
		}
	}

	private void CommandBar_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		UpdateTitleContentControlVisibility();
		UpdateSubTitleTextBlockVisibility();
		AdjustCommandBarGridsSize();
		CalculateItemsMaxWidth();
		_commandBarOverflowIconsManager.UpdateButtonsLabelPositionAndOverflow();
	}

	private void AdjustCommandBarGridsSize()
	{
		if (!(_titleColumn == null) && !(_itemsColumn == null))
		{
			if (base.Content != null)
			{
				SetColumnsSize(GridUnitType.Star, GridUnitType.Auto);
			}
			else
			{
				SetColumnsSize(GridUnitType.Auto, GridUnitType.Star);
			}
		}
	}

	private void SetColumnsSize(GridUnitType contentGridUnitType, GridUnitType itemsGridUnitType)
	{
		_titleColumn.Width = new GridLength(1.0, contentGridUnitType);
		_itemsColumn.Width = new GridLength(1.0, itemsGridUnitType);
	}

	private void CalculateItemsMaxWidth()
	{
		if (!(_titleColumn == null) && !(_itemsColumn == null))
		{
			if (base.Content != null)
			{
				CurrentItemsMaxWidth = (_titleColumn.ActualWidth + _itemsControl.ActualWidth) * 0.6;
			}
			else
			{
				CurrentItemsMaxWidth = base.ActualWidth * 0.85;
			}
		}
	}

	private void VisibilityPropertyChanged(DependencyObject sender, DependencyProperty dp)
	{
		if (sender is CommandBar { Visibility: Visibility.Visible })
		{
			_commandBarOverflowIconsManager?.UpdateButtonLayout();
		}
	}

	private void ContentPropertyChanged(DependencyObject sender, DependencyProperty dp)
	{
		UpdateTitleContentControl();
		AdjustCommandBarGridsSize();
		CalculateItemsMaxWidth();
	}

	private void IsDynamicOverflowEnabledPropertyChanged(DependencyObject sender, DependencyProperty dp)
	{
		if (base.IsDynamicOverflowEnabled)
		{
			base.IsDynamicOverflowEnabled = false;
		}
	}

	internal void InvokeBackButtonEvent()
	{
		this.BackButtonEvent?.Invoke(this, EventArgs.Empty);
	}

	private void MoreOptionsBadge_Loaded(object sender, RoutedEventArgs e)
	{
		if (sender is TextBadge startNode)
		{
			_notificationBadgeTextBlock = UIExtensionsInternal.FindChildByName<TextBlock>("TextBlockBadgeText", startNode);
			UpdateNotificationBadge(MoreOptionsBadge);
		}
	}

	private void UpdateTitleContentControlVisibility()
	{
		if (!(_titleControl == null))
		{
			_titleControl.Visibility = ((_titleControl.Content == null) ? Visibility.Collapsed : Visibility.Visible);
		}
	}

	private void UpdateSubTitleTextBlockVisibility()
	{
		if (!(_subTitleTextBlock == null))
		{
			_subTitleTextBlock.Visibility = ((string.IsNullOrEmpty(_subTitleTextBlock.Text) || !IsSubtitleVisible) ? Visibility.Collapsed : Visibility.Visible);
		}
	}
}
