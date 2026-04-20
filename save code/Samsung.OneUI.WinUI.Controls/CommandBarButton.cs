using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Automation.Provider;
using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Controls;

[Obsolete("CommandBarButton is deprecated, please use IconButton instead.")]
public class CommandBarButton : AppBarButton, ICommandBarItemOverflowable
{
	private const string DISABLED_STATE = "Disabled";

	private const string NORMAL_STATE = "Normal";

	private const string TEXT_LABEL_GRID_NAME = "TextLabelGrid";

	private const string SVG_CONTENT_CONTROL_NAME = "SvgContentControl";

	private CommandBarButtonTooltipVisibilityService _commandBarButtonTooltipVisibilityService;

	private CommandBarButtonVisualStateService _commandBarButtonVisualStateService;

	private Grid _buttonTextLabelGrid;

	private ContentControl _buttonSvgContentControl;

	private long _tokenLabelPropertyChanged;

	public static readonly DependencyProperty LabelVisibilityProperty = DependencyProperty.Register("LabelVisibility", typeof(Visibility), typeof(CommandBarButton), new PropertyMetadata(Visibility.Visible, OnLabelVisibilityPropertyChanged));

	public static readonly DependencyProperty IconSvgStyleProperty = DependencyProperty.Register("IconSvgStyle", typeof(Style), typeof(CommandBarButton), new PropertyMetadata(null));

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
		if (sender is CommandBarButton commandBarButton)
		{
			commandBarButton.UpdateLabelGridVisibility();
		}
	}

	public CommandBarButton()
	{
		base.DefaultStyleKey = typeof(CommandBarButton);
	}

	protected override void OnApplyTemplate()
	{
		UnregisterEvents();
		_buttonSvgContentControl = (ContentControl)GetTemplateChild("SvgContentControl");
		_buttonTextLabelGrid = (Grid)GetTemplateChild("TextLabelGrid");
		_commandBarButtonTooltipVisibilityService = new CommandBarButtonTooltipVisibilityService(this, this, _buttonTextLabelGrid);
		_commandBarButtonVisualStateService = new CommandBarButtonVisualStateService(this, _buttonTextLabelGrid);
		RegisterEvents();
		UpdateLabelGridVisibility();
		base.OnApplyTemplate();
	}

	private void OnLabelPropertyChanged(DependencyObject sender, DependencyProperty dp)
	{
		if (sender is CommandBarButton)
		{
			UpdateLabelGridVisibility();
		}
	}

	private void CommandBarButton_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		if (sender is CommandBarButton)
		{
			UpdateSvgIconVisualState();
		}
	}

	private void RegisterEvents()
	{
		base.IsEnabledChanged += CommandBarButton_IsEnabledChanged;
		_tokenLabelPropertyChanged = RegisterPropertyChangedCallback(AppBarButton.LabelProperty, OnLabelPropertyChanged);
		_commandBarButtonTooltipVisibilityService?.RegisterEvents();
		_commandBarButtonVisualStateService?.RegisterEvents();
	}

	private void UnregisterEvents()
	{
		base.IsEnabledChanged -= CommandBarButton_IsEnabledChanged;
		UnregisterPropertyChangedCallback(AppBarButton.LabelProperty, _tokenLabelPropertyChanged);
		_commandBarButtonTooltipVisibilityService?.UnregisterEvents();
		_commandBarButtonVisualStateService?.UnregisterEvents();
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
