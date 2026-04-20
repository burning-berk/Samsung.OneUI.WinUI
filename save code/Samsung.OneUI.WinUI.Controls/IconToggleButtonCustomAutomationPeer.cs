using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Automation.Provider;
using Microsoft.UI.Xaml.Controls;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls;

internal class IconToggleButtonCustomAutomationPeer : AppBarToggleButtonAutomationPeer, IInvokeProvider
{
	private const string BUTTON_CONTROL_TYPE = "SS_BUTTON_TTS/Text";

	private const string APP_BAR_TOGGLE_BUTTON = "App bar toggle button";

	private const string TOGGLE_STATE_ON = "PC_ST_STATUS_ON_M_SWITCH/Content";

	private const string TOGGLE_STATE_OFF = "PC_ST_STATUS_OFF_M_SWITCH/Content";

	private const string DOUBLE_TAB_TO_TOGGLE = "DREAM_DOUBLE_TAP_TO_TOGGLE_TBBODY";

	private const string ORDER_KEY = "DREAM_P1SD_OF_P2SD_TBOPT";

	private readonly string _buttonControlType = "SS_BUTTON_TTS/Text".GetLocalized();

	private readonly string _stateOn = "PC_ST_STATUS_ON_M_SWITCH/Content".GetLocalized();

	private readonly string _stateOff = "PC_ST_STATUS_OFF_M_SWITCH/Content".GetLocalized();

	private readonly string _doubleTabToToggle = "DREAM_DOUBLE_TAP_TO_TOGGLE_TBBODY".GetLocalized();

	private readonly string _order = "DREAM_P1SD_OF_P2SD_TBOPT".GetLocalized();

	private readonly IconToggleButton _iconToggleButton;

	private readonly bool _isCommandBarChild;

	public IconToggleButtonCustomAutomationPeer(IconToggleButton owner)
		: base(owner)
	{
		_iconToggleButton = owner;
		if (_iconToggleButton != null)
		{
			UnregisterEvents();
			_isCommandBarChild = IsCommandBarChild();
			RegisterEvents();
		}
	}

	protected override string GetNameCore()
	{
		if (_iconToggleButton == null)
		{
			return base.GetNameCore();
		}
		if (_iconToggleButton.LabelVisibility == Visibility.Collapsed || string.IsNullOrWhiteSpace(_iconToggleButton.Label))
		{
			return GetAlternativeText();
		}
		return _iconToggleButton.Label;
	}

	protected override string GetLocalizedControlTypeCore()
	{
		if (_isCommandBarChild)
		{
			return GetToggleButtonNameInCommandBar();
		}
		return _buttonControlType;
	}

	protected override object GetPatternCore(PatternInterface patternInterface)
	{
		return patternInterface switch
		{
			PatternInterface.Toggle => null, 
			PatternInterface.Invoke => this, 
			_ => base.GetPatternCore(patternInterface), 
		};
	}

	public void Invoke()
	{
		if (_iconToggleButton != null)
		{
			_iconToggleButton.IsChecked = !_iconToggleButton.IsChecked;
			OnNotificationToggleButtonName();
		}
	}

	private void IconToggleButton_Click(object sender, RoutedEventArgs e)
	{
		OnNotificationToggleButtonName();
	}

	private void RegisterEvents()
	{
		_iconToggleButton.Click += IconToggleButton_Click;
	}

	private void UnregisterEvents()
	{
		_iconToggleButton.Click -= IconToggleButton_Click;
	}

	private bool IsCommandBarChild()
	{
		return UIExtensionsInternal.GetVisualParent<CommandBar>(_iconToggleButton) != null;
	}

	private void OnNotificationToggleButtonName()
	{
		string displayString = GetNameCore() + ", " + _buttonControlType;
		if (_isCommandBarChild)
		{
			displayString = $"{GetNameCore()}, {GetToggleButtonNameInCommandBar()}, {string.Format(_order, GetPositionInSet(), GetSizeOfSet())}";
		}
		RaiseNotificationEvent(AutomationNotificationKind.ActionCompleted, AutomationNotificationProcessing.MostRecent, displayString, Guid.NewGuid().ToString());
	}

	private string GetToggleButtonNameInCommandBar()
	{
		return string.Concat(" App bar toggle button," + " " + (_iconToggleButton.IsChecked.Value ? _stateOn : _stateOff) + ",", " ", _doubleTabToToggle);
	}

	private string GetAlternativeText()
	{
		string automationId = AutomationProperties.GetAutomationId(_iconToggleButton);
		if (!string.IsNullOrEmpty(automationId))
		{
			return automationId;
		}
		string name = AutomationProperties.GetName(_iconToggleButton);
		if (!string.IsNullOrEmpty(name))
		{
			return name;
		}
		if ((ToolTipService.GetToolTip(_iconToggleButton) as ToolTip)?.Content is string result)
		{
			return result;
		}
		return string.Empty;
	}
}
