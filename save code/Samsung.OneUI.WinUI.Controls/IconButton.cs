using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Automation.Provider;
using Microsoft.UI.Xaml.Controls;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

public class IconButton : AppBarButton, ICommandBarItemOverflowable
{
	private const string DISABLED_STATE = "Disabled";

	private const string NORMAL_STATE = "Normal";

	private const string TEXT_LABEL_GRID_NAME = "TextLabelGrid";

	private const string SVG_CONTENT_CONTROL_NAME = "SvgContentControl";

	private const string BUTTON_CONTROL_TYPE = "SS_BUTTON_TTS/Text";

	private readonly string _buttonControlType = "SS_BUTTON_TTS/Text".GetLocalized();

	private IconButtonTooltipVisibilityService _iconButtonTooltipVisibilityService;

	private IconButtonVisualStateService _iconButtonVisualStateService;

	private Grid _buttonTextLabelGrid;

	private ContentControl _buttonSvgContentControl;

	private long _tokenLabelPropertyChanged;

	private bool _isCommandBarChild;

	public static readonly DependencyProperty LabelVisibilityProperty = DependencyProperty.Register("LabelVisibility", typeof(Visibility), typeof(IconButton), new PropertyMetadata(Visibility.Visible, OnLabelVisibilityPropertyChanged));

	public static readonly DependencyProperty IconSvgStyleProperty = DependencyProperty.Register("IconSvgStyle", typeof(Style), typeof(IconButton), new PropertyMetadata(null));

	public Visibility LabelVisibility
	{
		get
		{
			return (Visibility)GetValue(LabelVisibilityProperty);
		}
		set
		{
			SetValue(LabelVisibilityProperty, value);
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

	private static void OnLabelVisibilityPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
	{
		if (sender is IconButton iconButton)
		{
			iconButton.UpdateLabelGridVisibility();
			iconButton.UpdateAccessibilityProperties();
		}
	}

	public IconButton()
	{
		base.DefaultStyleKey = typeof(IconButton);
	}

	protected override void OnApplyTemplate()
	{
		UnregisterEvents();
		_buttonSvgContentControl = (ContentControl)GetTemplateChild("SvgContentControl");
		_buttonTextLabelGrid = (Grid)GetTemplateChild("TextLabelGrid");
		_iconButtonTooltipVisibilityService = new IconButtonTooltipVisibilityService(this, this, _buttonTextLabelGrid);
		_iconButtonVisualStateService = new IconButtonVisualStateService(this, _buttonTextLabelGrid);
		_isCommandBarChild = IsCommandBarChild();
		RegisterEvents();
		UpdateLabelGridVisibility();
		UpdateAccessibilityProperties();
		base.OnApplyTemplate();
	}

	private void OnLabelPropertyChanged(DependencyObject sender, DependencyProperty dp)
	{
		if (sender is IconButton)
		{
			UpdateLabelGridVisibility();
			UpdateAccessibilityProperties();
		}
	}

	private void IconButton_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		if (sender is IconButton)
		{
			UpdateSvgIconVisualState();
		}
	}

	private void RegisterEvents()
	{
		base.IsEnabledChanged += IconButton_IsEnabledChanged;
		_tokenLabelPropertyChanged = RegisterPropertyChangedCallback(AppBarButton.LabelProperty, OnLabelPropertyChanged);
		_iconButtonTooltipVisibilityService?.RegisterEvents();
		_iconButtonVisualStateService?.RegisterEvents();
	}

	private void UnregisterEvents()
	{
		base.IsEnabledChanged -= IconButton_IsEnabledChanged;
		UnregisterPropertyChangedCallback(AppBarButton.LabelProperty, _tokenLabelPropertyChanged);
		_iconButtonTooltipVisibilityService?.UnregisterEvents();
		_iconButtonVisualStateService?.UnregisterEvents();
	}

	private void UpdateSvgIconVisualState()
	{
		if (!(_buttonSvgContentControl == null))
		{
			VisualStateManager.GoToState(_buttonSvgContentControl, base.IsEnabled ? "Normal" : "Disabled", useTransitions: false);
		}
	}

	private void UpdateLabelGridVisibility()
	{
		if (!(_buttonTextLabelGrid == null))
		{
			_buttonTextLabelGrid.Visibility = ((string.IsNullOrWhiteSpace(base.Label) || LabelVisibility == Visibility.Collapsed) ? Visibility.Collapsed : Visibility.Visible);
		}
	}

	private void UpdateAccessibilityProperties()
	{
		string value = ((LabelVisibility != Visibility.Collapsed && !string.IsNullOrWhiteSpace(base.Label)) ? base.Label : GetAlternativeText());
		AutomationProperties.SetName(this, value);
		if (!_isCommandBarChild)
		{
			AutomationProperties.SetLocalizedControlType(this, _buttonControlType);
		}
	}

	private bool IsCommandBarChild()
	{
		return UIExtensionsInternal.GetVisualParent<CommandBar>(this) != null;
	}

	private string GetAlternativeText()
	{
		string automationId = AutomationProperties.GetAutomationId(this);
		if (!string.IsNullOrEmpty(automationId))
		{
			return automationId;
		}
		string name = AutomationProperties.GetName(this);
		if (!string.IsNullOrEmpty(name))
		{
			return name;
		}
		if ((ToolTipService.GetToolTip(this) as ToolTip)?.Content is string result)
		{
			return result;
		}
		return string.Empty;
	}

	public void PerformButtonClick()
	{
		(new ButtonAutomationPeer(this).GetPattern(PatternInterface.Invoke) as IInvokeProvider)?.Invoke();
	}

	public string GetButtonLabel()
	{
		return base.Label;
	}

	public Visibility GetButtonLabelVisibility()
	{
		return LabelVisibility;
	}
}
