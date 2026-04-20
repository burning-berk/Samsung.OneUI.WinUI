using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Automation.Provider;
using Microsoft.UI.Xaml.Controls;

namespace Samsung.OneUI.WinUI.Controls;

public class IconToggleButton : AppBarToggleButton, ICommandBarItemOverflowable
{
	private const string TEXT_LABEL_GRID_NAME = "TextLabelGrid";

	private Grid _buttonTextLabelGrid;

	private IconButtonTooltipVisibilityService _iconButtonTooltipVisibilityService;

	private IconButtonVisualStateService _iconButtonVisualStateService;

	private long _tokenOnLabelPropertyChanged;

	public static readonly DependencyProperty LabelVisibilityProperty = DependencyProperty.Register("LabelVisibility", typeof(Visibility), typeof(IconToggleButton), new PropertyMetadata(Visibility.Visible, OnLabelVisibilityPropertyChanged));

	public static readonly DependencyProperty IconSvgStyleProperty = DependencyProperty.Register("IconSvgStyle", typeof(Style), typeof(IconToggleButton), new PropertyMetadata(null));

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
		if (sender is IconToggleButton iconToggleButton)
		{
			iconToggleButton.UpdateLabelGridVisibility();
		}
	}

	public IconToggleButton()
	{
		base.DefaultStyleKey = typeof(IconButton);
	}

	protected override AutomationPeer OnCreateAutomationPeer()
	{
		return new IconToggleButtonCustomAutomationPeer(this);
	}

	protected override void OnApplyTemplate()
	{
		UnregisterEvents();
		_buttonTextLabelGrid = (Grid)GetTemplateChild("TextLabelGrid");
		_iconButtonTooltipVisibilityService = new IconButtonTooltipVisibilityService(this, this, _buttonTextLabelGrid);
		_iconButtonVisualStateService = new IconButtonVisualStateService(this, _buttonTextLabelGrid);
		RegisterEvents();
		UpdateLabelGridVisibility();
		base.OnApplyTemplate();
	}

	private void OnLabelPropertyChanged(DependencyObject sender, DependencyProperty dp)
	{
		if (sender is IconToggleButton)
		{
			UpdateLabelGridVisibility();
		}
	}

	private void RegisterEvents()
	{
		_tokenOnLabelPropertyChanged = RegisterPropertyChangedCallback(AppBarToggleButton.LabelProperty, OnLabelPropertyChanged);
		_iconButtonTooltipVisibilityService?.RegisterEvents();
		_iconButtonVisualStateService?.RegisterEvents();
	}

	private void UnregisterEvents()
	{
		UnregisterPropertyChangedCallback(AppBarToggleButton.LabelProperty, _tokenOnLabelPropertyChanged);
		_iconButtonTooltipVisibilityService?.UnregisterEvents();
		_iconButtonVisualStateService?.UnregisterEvents();
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
