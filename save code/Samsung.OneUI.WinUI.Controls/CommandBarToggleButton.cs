using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Automation.Provider;
using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Controls;

[Obsolete("CommandBarToggleButton is deprecated, please use IconToggleButton instead.")]
public class CommandBarToggleButton : AppBarToggleButton, ICommandBarItemOverflowable
{
	private const string TEXT_LABEL_GRID_NAME = "TextLabelGrid";

	private Grid _buttonTextLabelGrid;

	private CommandBarButtonTooltipVisibilityService _commandBarButtonTooltipVisibilityService;

	private CommandBarButtonVisualStateService _commandBarButtonVisualStateService;

	private long _tokenOnLabelPropertyChanged;

	public static readonly DependencyProperty LabelVisibilityProperty = DependencyProperty.Register("LabelVisibility", typeof(Visibility), typeof(CommandBarToggleButton), new PropertyMetadata(Visibility.Visible, OnLabelVisibilityPropertyChanged));

	public static readonly DependencyProperty IconSvgStyleProperty = DependencyProperty.Register("IconSvgStyle", typeof(Style), typeof(CommandBarToggleButton), new PropertyMetadata(null));

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
		if (sender is CommandBarToggleButton commandBarToggleButton)
		{
			commandBarToggleButton.UpdateLabelGridVisibility();
		}
	}

	public CommandBarToggleButton()
	{
		base.DefaultStyleKey = typeof(CommandBarButton);
	}

	protected override void OnApplyTemplate()
	{
		UnregisterEvents();
		_buttonTextLabelGrid = (Grid)GetTemplateChild("TextLabelGrid");
		_commandBarButtonTooltipVisibilityService = new CommandBarButtonTooltipVisibilityService(this, this, _buttonTextLabelGrid);
		_commandBarButtonVisualStateService = new CommandBarButtonVisualStateService(this, _buttonTextLabelGrid);
		RegisterEvents();
		UpdateLabelGridVisibility();
		base.OnApplyTemplate();
	}

	private void OnLabelPropertyChanged(DependencyObject sender, DependencyProperty dp)
	{
		if (sender is CommandBarToggleButton)
		{
			UpdateLabelGridVisibility();
		}
	}

	private void RegisterEvents()
	{
		_tokenOnLabelPropertyChanged = RegisterPropertyChangedCallback(AppBarToggleButton.LabelProperty, OnLabelPropertyChanged);
		_commandBarButtonTooltipVisibilityService?.RegisterEvents();
		_commandBarButtonVisualStateService?.RegisterEvents();
	}

	private void UnregisterEvents()
	{
		UnregisterPropertyChangedCallback(AppBarToggleButton.LabelProperty, _tokenOnLabelPropertyChanged);
		_commandBarButtonTooltipVisibilityService?.UnregisterEvents();
		_commandBarButtonVisualStateService?.UnregisterEvents();
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
		(new ToggleButtonAutomationPeer(this).GetPattern(PatternInterface.Toggle) as IToggleProvider)?.Toggle();
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
