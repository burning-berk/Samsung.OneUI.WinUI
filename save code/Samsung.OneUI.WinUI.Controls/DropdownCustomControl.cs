using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using Samsung.OneUI.WinUI.Utils.Extensions;

namespace Samsung.OneUI.WinUI.Controls;

[ContentProperty(Name = "Content")]
internal sealed class DropdownCustomControl : Control
{
	private const string STATE_NORMAL = "Normal";

	private const string STATE_POINTER_OVER = "PointerOver";

	private const string STATE_PRESSED = "Pressed";

	private const string STATE_DISABLED = "Disabled";

	private const string STATE_FOCUSED = "Focused";

	private const string STATE_UNFOCUSED = "Unfocused";

	private const string ROOT_GRID = "RootGrid";

	private bool _isPointerOver;

	private bool _isPointerPressed;

	private Grid _rootGrid;

	private DropdownCustomControlAutomationPeer _dropdownCustomControlAutomationPeer;

	public static DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(object), typeof(DropdownCustomControl), new PropertyMetadata(null, OnContentPropertyChanged));

	public new static readonly DependencyProperty IsEnabledProperty = DependencyProperty.Register("IsEnabled", typeof(bool), typeof(DropdownCustomControl), new PropertyMetadata(true, OnIsEnabledPropertyChanged));

	public static readonly DependencyProperty ArrowColorProperty = DependencyProperty.Register("ArrowColor", typeof(SolidColorBrush), typeof(DropdownCustomControl), new PropertyMetadata(null));

	public object Content
	{
		get
		{
			return GetValue(ContentProperty);
		}
		set
		{
			SetValue(ContentProperty, value);
		}
	}

	public new bool IsEnabled
	{
		get
		{
			return (bool)GetValue(IsEnabledProperty);
		}
		set
		{
			SetValue(IsEnabledProperty, value);
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

	public event EventHandler AutomationInvokeRequested;

	private static void OnContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is DropdownCustomControl dropdownCustomControl)
		{
			dropdownCustomControl.ApplyTextTrimmingToString();
		}
	}

	private static void OnIsEnabledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is DropdownCustomControl dropdownCustomControl)
		{
			dropdownCustomControl.UpdateVisualState();
		}
	}

	public DropdownCustomControl()
	{
		base.DefaultStyleKey = typeof(DropdownCustomControl);
		base.Loaded += DropdownCustomControl_Loaded;
		base.GettingFocus += DropdownCustomControl_GettingFocus;
		base.LosingFocus += DropdownCustomControl_LosingFocus;
		base.SizeChanged += DropdownCustomControl_SizeChanged;
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		_rootGrid = GetTemplateChild("RootGrid") as Grid;
	}

	protected override void OnPointerEntered(PointerRoutedEventArgs e)
	{
		_isPointerOver = true;
		UpdateVisualState();
	}

	protected override void OnPointerExited(PointerRoutedEventArgs e)
	{
		_isPointerOver = false;
		_isPointerPressed = false;
		UpdateVisualState();
	}

	protected override void OnPointerPressed(PointerRoutedEventArgs e)
	{
		_isPointerPressed = true;
		UpdateVisualState();
	}

	protected override void OnPointerReleased(PointerRoutedEventArgs e)
	{
		_isPointerPressed = false;
		UpdateVisualState();
	}

	protected override AutomationPeer OnCreateAutomationPeer()
	{
		if (_dropdownCustomControlAutomationPeer == null)
		{
			_dropdownCustomControlAutomationPeer = new DropdownCustomControlAutomationPeer(this);
		}
		return _dropdownCustomControlAutomationPeer;
	}

	private void DropdownCustomControl_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		UpdateToolTip();
	}

	private void DropdownCustomControl_Loaded(object sender, RoutedEventArgs e)
	{
		UpdateVisualState();
	}

	private void DropdownCustomControl_GettingFocus(UIElement sender, GettingFocusEventArgs args)
	{
		if (args.InputDevice == FocusInputDeviceKind.Keyboard)
		{
			VisualStateManager.GoToState(this, "Focused", useTransitions: true);
		}
	}

	private void DropdownCustomControl_LosingFocus(UIElement sender, LosingFocusEventArgs args)
	{
		if (args.InputDevice == FocusInputDeviceKind.Keyboard || args.InputDevice == FocusInputDeviceKind.Mouse)
		{
			VisualStateManager.GoToState(this, "Unfocused", useTransitions: true);
		}
	}

	private void UpdateVisualState()
	{
		if (!IsEnabled)
		{
			VisualStateManager.GoToState(this, "Disabled", useTransitions: false);
		}
		else if (_isPointerPressed)
		{
			VisualStateManager.GoToState(this, "Pressed", useTransitions: false);
		}
		else if (_isPointerOver)
		{
			VisualStateManager.GoToState(this, "PointerOver", useTransitions: false);
		}
		else
		{
			VisualStateManager.GoToState(this, "Normal", useTransitions: false);
		}
	}

	private void ApplyTextTrimmingToString()
	{
		if (Content is IFormattable || Content is string)
		{
			TextBlock textBlock = new TextBlock();
			textBlock.TextTrimming = TextTrimming.CharacterEllipsis;
			textBlock.Text = Content.ToString();
			Content = textBlock;
			UpdateToolTip();
		}
	}

	internal void UpdateToolTip()
	{
		UpdateToolTipContent(_rootGrid);
	}

	internal void UpdateToolTipContent(Grid grid)
	{
		Microsoft.UI.Xaml.Controls.ToolTip toolTip = grid?.GetToolTip();
		if (Content is TextBlock textBlock && toolTip != null)
		{
			textBlock.UpdateLayout();
			toolTip.Content = textBlock.Text;
			toolTip.Visibility = ((!textBlock.IsTextTrimmed) ? Visibility.Collapsed : Visibility.Visible);
			ToolTipService.SetToolTip(this, toolTip);
		}
	}

	internal void CloseToolTip()
	{
		_rootGrid?.GetToolTip()?.CloseToolTip();
	}

	internal void ExecuteAutomationRequest()
	{
		this.AutomationInvokeRequested?.Invoke(this, EventArgs.Empty);
	}
}
