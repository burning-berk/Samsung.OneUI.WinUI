using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Samsung.OneUI.WinUI.Common;
using Samsung.OneUI.WinUI.Controls.EventHandlers;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.System;

namespace Samsung.OneUI.WinUI.Controls;

public sealed class ChipsItem : GridViewItem
{
	private const string PART_ACTION_BUTTON = "PART_ActionButton";

	private const string CONTENT_GRID = "ContentGrid";

	private const string STATE_NORMAL = "Normal";

	private const string STATE_SELECTED = "Selected";

	private const string STATE_PRESSED_SELECTED = "PressedSelected";

	private const string STATE_POINTEROVER = "ChipsItemPointerOver";

	private const string STATE_STATE_POINTEROVER_SELECTED = "ChipsItemPointerOverSelected";

	private const string ICON_STATE_POINTEROVER_SELECTED = "ChipIconPointerOverSelected";

	private const string ICON_STATE_POINTERPRESSED_SELECTED = "ChipIconPointerPressedSelected";

	private const string ICON_STATE_POINTEROVER = "ChipIconPointerOver";

	private const string ICON_STATE_POINTEROVER_PRESSED = "ChipIconPointerPressed";

	private const string STATE_ENABLED = "Enabled";

	private const string STATE_DISABLED = "Disabled";

	private const string STATE_SELECTED_DISABLED = "SelectedDisabled";

	private const string STATE_GROUP_DISABLED_STATES = "DisabledStates";

	private const string STATE_SELECTED_FOCUS_STATES = "SelectedFocus";

	private const string STATE_FOCUS = "ChipItemFocused";

	private const string STATE_GROUP_COMMON_STATES = "CommonStates";

	private const string CHIP_TOOLTIP = "ChipItemToolTip";

	private const string CHIP_IMAGE_ICON = "ChipImageIcon";

	private Button _actionButton;

	private VisualStateGroup _disabledStatesGroup;

	private VisualStateGroup _commonStatesGroup;

	private bool _isActionClickButtonActioned;

	private ImageIcon _imageIcon;

	public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(ChipsItem), new PropertyMetadata(string.Empty, OnTitlePropertyChanged));

	public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(ChipsItemTemplate), typeof(ChipsItem), new PropertyMetadata(ChipsItemTemplate.Default, null));

	public static readonly DependencyProperty IdProperty = DependencyProperty.Register("Id", typeof(string), typeof(ChipsItem), new PropertyMetadata(string.Empty));

	public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(ChipsItemType), typeof(ChipsItem), new PropertyMetadata(ChipsItemType.Default, null));

	public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Image", typeof(ImageSource), typeof(ChipsItem), new PropertyMetadata(null, OnIconChange));

	public static readonly DependencyProperty IconSvgStyleProperty = DependencyProperty.Register("IconSvgStyle", typeof(Style), typeof(ChipsItem), new PropertyMetadata(null, null));

	public string Title
	{
		get
		{
			return (string)GetValue(TitleProperty);
		}
		set
		{
			SetValue(TitleProperty, value);
		}
	}

	public ChipsItemTemplate Label
	{
		get
		{
			return (ChipsItemTemplate)GetValue(LabelProperty);
		}
		set
		{
			SetValue(LabelProperty, value);
		}
	}

	public string Id
	{
		get
		{
			return (string)GetValue(IdProperty);
		}
		set
		{
			SetValue(IdProperty, value);
		}
	}

	public ChipsItemType Type
	{
		get
		{
			return (ChipsItemType)GetValue(TypeProperty);
		}
		set
		{
			SetValue(TypeProperty, value);
		}
	}

	public ImageSource Icon
	{
		get
		{
			return (ImageSource)GetValue(IconProperty);
		}
		set
		{
			SetValue(IconProperty, value);
		}
	}

	public Style IconSvgStyle
	{
		get
		{
			return (Style)GetValue(IconSvgStyleProperty);
		}
		set
		{
			SetValue(IconSvgStyleProperty, value);
		}
	}

	public event EventHandler ActionRequest;

	public event EventHandler<Samsung.OneUI.WinUI.Controls.EventHandlers.ItemClickEventArgs> Click;

	private static void OnIconChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ChipsItem chipsItem)
		{
			chipsItem.UpdateImageIcon();
		}
	}

	public ChipsItem()
	{
		base.DefaultStyleKey = typeof(ChipsItem);
		base.Loaded += ChipsItem_Loaded;
		base.Unloaded += ChipsItem_Unloaded;
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		DpiChangedTo100StateTrigger.XamlRoot = base.XamlRoot;
		DpiChangedTo125StateTrigger.XamlRoot = base.XamlRoot;
		DpiChangedTo150StateTrigger.XamlRoot = base.XamlRoot;
		DpiChangedTo175StateTrigger.XamlRoot = base.XamlRoot;
		UnregisterEvents();
		_actionButton = GetTemplateChild("PART_ActionButton") as Button;
		_disabledStatesGroup = GetTemplateChild("DisabledStates") as VisualStateGroup;
		_commonStatesGroup = GetTemplateChild("CommonStates") as VisualStateGroup;
		_imageIcon = GetTemplateChild("ChipImageIcon") as ImageIcon;
		RegisterEvents();
		UpdateImageIcon();
	}

	protected override void OnKeyUp(KeyRoutedEventArgs e)
	{
		if (e.Key == VirtualKey.Enter || e.Key == VirtualKey.Space)
		{
			base.OnKeyUp(e);
			HandleMouseInteractionInternalButton();
			this.Click?.Invoke(this, new Samsung.OneUI.WinUI.Controls.EventHandlers.ItemClickEventArgs
			{
				ClickedItem = this
			});
		}
	}

	protected override void OnKeyDown(KeyRoutedEventArgs e)
	{
		if (e.Key == VirtualKey.Enter || e.Key == VirtualKey.Space)
		{
			base.OnKeyDown(e);
			HandleKeyDownInteractionWhenPressed();
		}
	}

	protected override void OnTapped(TappedRoutedEventArgs e)
	{
		if (!_isActionClickButtonActioned)
		{
			base.OnTapped(e);
			this.Click?.Invoke(this, new Samsung.OneUI.WinUI.Controls.EventHandlers.ItemClickEventArgs
			{
				ClickedItem = this
			});
		}
		_isActionClickButtonActioned = false;
		HandleMouseInteractionWhenActionGridPointerOver();
	}

	public Grid GetItemContent()
	{
		return UIExtensionsInternal.FindChildByName<Grid>("ContentGrid", this);
	}

	private void ChipsItem_Loaded(object sender, RoutedEventArgs e)
	{
		if (GetTemplateChild("ChipItemToolTip") is ToolTip value)
		{
			ToolTipService.SetToolTip(this, value);
		}
		RegisterEvents();
	}

	private void ChipsItem_Unloaded(object sender, RoutedEventArgs e)
	{
		UnregisterEvents();
	}

	private static void OnTitlePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ChipsItem chipsItem)
		{
			chipsItem.SetValue(AutomationProperties.NameProperty, chipsItem.Title);
		}
	}

	private void InvokeActionButtonRequestEvent()
	{
		this.ActionRequest?.Invoke(this, new EventArgs());
	}

	private void ActionButton_Click(object sender, RoutedEventArgs e)
	{
		_isActionClickButtonActioned = true;
		InvokeActionButtonRequestEvent();
	}

	private void RegisterEvents()
	{
		base.PointerEntered += ChipsItem_PointerEntered;
		base.GotFocus += ChipsItem_GotFocus;
		base.LostFocus += ChipsItem_LostFocus;
		if (_actionButton != null)
		{
			_actionButton.AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(ActionButton_PointerPressed), handledEventsToo: true);
			_actionButton.Click += ActionButton_Click;
			_actionButton.PointerEntered += ActionButton_PointerEntered;
			_actionButton.PointerExited += ActionButton_PointerExited;
		}
		if (_disabledStatesGroup != null)
		{
			_disabledStatesGroup.CurrentStateChanged += DisabledStatesGroup_CurrentStateChanged;
		}
		if (_commonStatesGroup != null)
		{
			_commonStatesGroup.CurrentStateChanged += CommonStatesGroup_CurrentStateChanged;
		}
	}

	private void UnregisterEvents()
	{
		base.PointerEntered -= ChipsItem_PointerEntered;
		base.GotFocus -= ChipsItem_GotFocus;
		base.LostFocus -= ChipsItem_LostFocus;
		if (_actionButton != null)
		{
			_actionButton.RemoveHandler(UIElement.PointerPressedEvent, new PointerEventHandler(ActionButton_PointerPressed));
			_actionButton.Click -= ActionButton_Click;
			_actionButton.PointerEntered -= ActionButton_PointerEntered;
			_actionButton.PointerExited -= ActionButton_PointerExited;
		}
		if (_disabledStatesGroup != null)
		{
			_disabledStatesGroup.CurrentStateChanged -= DisabledStatesGroup_CurrentStateChanged;
		}
		if (_commonStatesGroup != null)
		{
			_commonStatesGroup.CurrentStateChanged -= CommonStatesGroup_CurrentStateChanged;
		}
	}

	private void ForceRefreshVisualState(string resetStateName, string refreshStateName, bool useTransition = false)
	{
		VisualStateManager.GoToState(this, resetStateName, useTransition);
		VisualStateManager.GoToState(this, refreshStateName, useTransition);
	}

	private void UpdateImageIcon()
	{
		if (_imageIcon != null && Icon != null)
		{
			_imageIcon.Source = Icon;
		}
	}

	private void CommonStatesGroup_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
	{
		if (e.NewState.Name == "Normal" && !base.IsEnabled)
		{
			ForceRefreshVisualState("Enabled", "Disabled");
		}
	}

	private void DisabledStatesGroup_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
	{
		if (e.NewState.Name == "Enabled" && base.IsSelected)
		{
			ForceRefreshVisualState("Normal", "Selected");
		}
		else if (e.NewState.Name == "Disabled" && base.IsSelected)
		{
			VisualStateManager.GoToState(this, "SelectedDisabled", useTransitions: false);
		}
	}

	private void ChipsItem_PointerEntered(object sender, PointerRoutedEventArgs e)
	{
		HandleMouseInteractionWhenActionGridPointerOver();
	}

	private void ChipsItem_GotFocus(object sender, RoutedEventArgs e)
	{
		if (!(base.Style == null) && base.FocusState == FocusState.Keyboard)
		{
			VisualStateManager.GoToState(this, base.IsSelected ? "SelectedFocus" : "ChipItemFocused", useTransitions: true);
		}
	}

	private void ChipsItem_LostFocus(object sender, RoutedEventArgs e)
	{
		HandleMouseInteractionInternalButton();
	}

	private void ActionButton_PointerExited(object sender, PointerRoutedEventArgs e)
	{
		HandleMouseInteractionWhenActionGridPointerOver();
	}

	private void ActionButton_PointerPressed(object sender, PointerRoutedEventArgs e)
	{
		HandleMouseInteractionInternalButton();
		HandleMouseInteractionWhenIconPointerPressed();
	}

	private void ActionButton_PointerEntered(object sender, PointerRoutedEventArgs e)
	{
		HandleMouseInteractionInternalButton();
		HandleMouseInteractionWhenIconPointerOver();
	}

	private void HandleMouseInteractionWhenIconPointerOver()
	{
		if (!(base.Style == null))
		{
			VisualStateManager.GoToState(_actionButton, (base.IsSelected && Type == ChipsItemType.Default) ? "ChipIconPointerOverSelected" : "ChipIconPointerOver", useTransitions: true);
		}
	}

	private void HandleMouseInteractionWhenIconPointerPressed()
	{
		if (!(base.Style == null))
		{
			VisualStateManager.GoToState(_actionButton, (base.IsSelected && Type == ChipsItemType.Default) ? "ChipIconPointerPressedSelected" : "ChipIconPointerPressed", useTransitions: true);
		}
	}

	private void HandleMouseInteractionInternalButton()
	{
		if (!(base.Style == null))
		{
			VisualStateManager.GoToState(this, base.IsSelected ? "Selected" : "Normal", useTransitions: true);
		}
	}

	private void HandleMouseInteractionWhenActionGridPointerOver()
	{
		if (!(base.Style == null))
		{
			VisualStateManager.GoToState(this, base.IsSelected ? "ChipsItemPointerOverSelected" : "ChipsItemPointerOver", useTransitions: true);
		}
	}

	private void HandleKeyDownInteractionWhenPressed()
	{
		if (!(base.Style == null))
		{
			VisualStateManager.GoToState(this, base.IsSelected ? "PressedSelected" : "ChipIconPointerPressed", useTransitions: true);
		}
	}
}
