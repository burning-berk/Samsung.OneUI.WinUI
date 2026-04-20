using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Samsung.OneUI.WinUI.Common;
using Samsung.OneUI.WinUI.Controls.Inputs.ToggleSwitch;
using Samsung.OneUI.WinUI.Controls.Selectors;
using Samsung.OneUI.WinUI.Utils.Extensions;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

public class ToggleSwitch : Control
{
	private const string TEMPLATE_NAME = "ToggleSwitch";

	private const string GRID_CONTAINER = "GridContainer";

	private const string PRESSED_STATE = "Pressed";

	private const string ON_STATE = "On";

	private const string OFF_STATE = "Off";

	private Microsoft.UI.Xaml.Controls.ToggleSwitch _toggleSwitch;

	public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(ToggleSwitchType), typeof(ToggleSwitch), new PropertyMetadata(ToggleSwitchType.Default));

	public static readonly DependencyProperty OnContentProperty = DependencyProperty.Register("OnContent", typeof(string), typeof(ToggleSwitch), new PropertyMetadata(string.Empty));

	public static readonly DependencyProperty OffContentProperty = DependencyProperty.Register("OffContent", typeof(string), typeof(ToggleSwitch), new PropertyMetadata(string.Empty));

	public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof(string), typeof(ToggleSwitch), new PropertyMetadata(string.Empty));

	public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(ToggleSwitch), new PropertyMetadata(null));

	public static readonly DependencyProperty OnContentTemplateProperty = DependencyProperty.Register("OnContentTemplate", typeof(DataTemplate), typeof(ToggleSwitch), new PropertyMetadata(null));

	public static readonly DependencyProperty OffContentTemplateProperty = DependencyProperty.Register("OffContentTemplate", typeof(DataTemplate), typeof(ToggleSwitch), new PropertyMetadata(null));

	public static readonly DependencyProperty IsOnProperty = DependencyProperty.Register("IsOn", typeof(bool), typeof(ToggleSwitch), new PropertyMetadata(false, OnIsOnChanged));

	public new static readonly DependencyProperty StyleProperty = DependencyProperty.Register("Style", typeof(Style), typeof(ToggleSwitch), new PropertyMetadata(null, OnStyleChange));

	public ToggleSwitchType Type
	{
		get
		{
			return (ToggleSwitchType)GetValue(TypeProperty);
		}
		set
		{
			SetValue(TypeProperty, value);
		}
	}

	public string OnContent
	{
		get
		{
			return (string)GetValue(OnContentProperty);
		}
		set
		{
			SetValue(OnContentProperty, value);
		}
	}

	public string OffContent
	{
		get
		{
			return (string)GetValue(OffContentProperty);
		}
		set
		{
			SetValue(OffContentProperty, value);
		}
	}

	public string Header
	{
		get
		{
			return (string)GetValue(HeaderProperty);
		}
		set
		{
			SetValue(HeaderProperty, value);
		}
	}

	public DataTemplate HeaderTemplate
	{
		get
		{
			return (DataTemplate)GetValue(HeaderTemplateProperty);
		}
		set
		{
			SetValue(HeaderTemplateProperty, value);
		}
	}

	public DataTemplate OnContentTemplate
	{
		get
		{
			return (DataTemplate)GetValue(OnContentTemplateProperty);
		}
		set
		{
			SetValue(OnContentTemplateProperty, value);
		}
	}

	public DataTemplate OffContentTemplate
	{
		get
		{
			return (DataTemplate)GetValue(OffContentTemplateProperty);
		}
		set
		{
			SetValue(OffContentTemplateProperty, value);
		}
	}

	public bool IsOn
	{
		get
		{
			return (bool)GetValue(IsOnProperty);
		}
		set
		{
			SetValue(IsOnProperty, value);
		}
	}

	public new Style Style
	{
		get
		{
			return (Style)GetValue(StyleProperty);
		}
		set
		{
			SetValue(StyleProperty, value);
		}
	}

	public event RoutedEventHandler Toggled;

	private static void OnStyleChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ToggleSwitch toggleSwitch)
		{
			toggleSwitch.OnStylePropertyChanged();
		}
	}

	public void OnStylePropertyChanged()
	{
		if (_toggleSwitch != null)
		{
			_toggleSwitch.Style = Style;
		}
	}

	public ToggleSwitch()
	{
		base.IsEnabledChanged += ToggleSwitch_IsEnabledChanged;
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		_toggleSwitch = GetTemplateChild("ToggleSwitch") as Microsoft.UI.Xaml.Controls.ToggleSwitch;
		if (_toggleSwitch != null)
		{
			if (_toggleSwitch.IsLoaded)
			{
				HandleInnerToggleLoaded(_toggleSwitch);
			}
			else
			{
				_toggleSwitch.Loaded += InnerToggleSwitch_Loaded;
			}
			AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(OnPointerPressEvent), handledEventsToo: true);
		}
	}

	private void OnPointerPressEvent(object sender, PointerRoutedEventArgs e)
	{
		UIExtensionsInternal.FindChildByName<Grid>("GridContainer", _toggleSwitch)?.ValidadeAllAnimationVisualState();
	}

	private void InnerToggleSwitch_Loaded(object sender, RoutedEventArgs e)
	{
		if (!(_toggleSwitch == null))
		{
			_toggleSwitch.Loaded -= InnerToggleSwitch_Loaded;
			HandleInnerToggleLoaded(_toggleSwitch);
		}
	}

	private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
	{
		if (sender is Microsoft.UI.Xaml.Controls.ToggleSwitch)
		{
			IsOn = _toggleSwitch.IsOn;
		}
		this.Toggled?.Invoke(this, e);
	}

	private static void OnIsOnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ToggleSwitch toggleSwitch)
		{
			toggleSwitch.SetToggleSwitch();
		}
	}

	private void ToggleSwitch_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		if (_toggleSwitch != null)
		{
			_toggleSwitch.IsEnabled = base.IsEnabled;
		}
	}

	private void GridContainer_PointerReleased(object sender, PointerRoutedEventArgs e)
	{
		FrameworkElement frameworkElement = e.OriginalSource as FrameworkElement;
		if (sender is FrameworkElement frameworkElement2 && frameworkElement.Parent is FrameworkElement frameworkElement3 && frameworkElement2.Name == frameworkElement3.Name)
		{
			IsOn = !IsOn;
		}
	}

	private void GridContainer_PointerPressed(object sender, PointerRoutedEventArgs e)
	{
		if (_toggleSwitch != null)
		{
			VisualStateManager.GoToState(_toggleSwitch, "Pressed", useTransitions: false);
		}
	}

	private void HandleInnerToggleLoaded(Microsoft.UI.Xaml.Controls.ToggleSwitch toggleSwitch)
	{
		if (!(toggleSwitch == null))
		{
			toggleSwitch.Style = ((Style == null) ? new ToggleSwitchStyleSelector(Type).SelectStyle() : Style);
			HandleIsTabStopWhenStart();
			SetToggledEvent();
			SetToggleSwitch();
			toggleSwitch.IsEnabled = base.IsEnabled;
		}
	}

	private void SetToggledEvent()
	{
		Grid grid = UIExtensionsInternal.FindChildByName<Grid>("GridContainer", _toggleSwitch);
		if (grid != null)
		{
			grid.PointerPressed -= GridContainer_PointerPressed;
			grid.PointerReleased -= GridContainer_PointerReleased;
			grid.PointerPressed += GridContainer_PointerPressed;
			grid.PointerReleased += GridContainer_PointerReleased;
		}
		if (_toggleSwitch != null)
		{
			_toggleSwitch.Toggled -= ToggleSwitch_Toggled;
			_toggleSwitch.Toggled += ToggleSwitch_Toggled;
		}
		DpiChangedTo100StateTrigger.XamlRoot = base.XamlRoot;
		DpiChangedTo125StateTrigger.XamlRoot = base.XamlRoot;
		DpiChangedTo150StateTrigger.XamlRoot = base.XamlRoot;
		DpiChangedTo175StateTrigger.XamlRoot = base.XamlRoot;
	}

	private void SetToggleSwitch()
	{
		if (!(_toggleSwitch == null))
		{
			if (!base.IsLoaded)
			{
				SetToggleSwitchKnobOnOffPosition();
			}
			if (_toggleSwitch.IsOn != IsOn)
			{
				_toggleSwitch.IsOn = IsOn;
			}
		}
	}

	private void SetToggleSwitchKnobOnOffPosition()
	{
		if (IsOn)
		{
			VisualStateManager.GoToState(_toggleSwitch, "On", useTransitions: false);
		}
		else
		{
			VisualStateManager.GoToState(_toggleSwitch, "Off", useTransitions: false);
		}
	}

	private void HandleIsTabStopWhenStart()
	{
		if (_toggleSwitch != null)
		{
			if (base.IsTabStop)
			{
				_toggleSwitch.IsTabStop = true;
				base.IsTabStop = false;
			}
			else
			{
				_toggleSwitch.IsTabStop = false;
			}
		}
	}
}
