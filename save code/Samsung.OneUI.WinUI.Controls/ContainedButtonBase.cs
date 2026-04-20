using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.ViewManagement;

namespace Samsung.OneUI.WinUI.Controls;

public class ContainedButtonBase : Button
{
	private const string OUT_STROKE_FOCUS_BORDER = "OutStroke";

	private const int HIGH_CONTRAST_COMPENSATION = 2;

	private const string PROGRESS_ENABLED_VISUAL_STATE = "ProgressEnabled";

	private const string PROGRESS_DISABLED_VISUAL_STATE = "ProgressDisabled";

	private const string DISABLED_VISUAL_STATE = "Disabled";

	private Border _outStrokeFocusBorder;

	private readonly AccessibilitySettings _accessibilitySettings;

	public static readonly DependencyProperty IsProgressEnabledProperty = DependencyProperty.Register("IsProgressEnabled", typeof(bool), typeof(ContainedButtonBase), new PropertyMetadata(false, OnIsProgressEnabledChanged));

	public bool IsProgressEnabled
	{
		get
		{
			return (bool)GetValue(IsProgressEnabledProperty);
		}
		set
		{
			SetValue(IsProgressEnabledProperty, value);
		}
	}

	public ContainedButtonBase()
	{
		base.Loaded += ContainedButtonBase_Loaded;
		base.Unloaded += ContainedButtonBase_Unloaded;
		base.IsEnabledChanged += ProgressButton_IsEnabledChanged;
		_accessibilitySettings = new AccessibilitySettings();
	}

	private void ContainedButtonBase_Loaded(object sender, RoutedEventArgs e)
	{
		base.ActualThemeChanged += ContainedButtonBase_ActualThemeChanged;
		UpdateOutStrokeFocusCornerRadius();
	}

	private void ContainedButtonBase_Unloaded(object sender, RoutedEventArgs e)
	{
		base.ActualThemeChanged -= ContainedButtonBase_ActualThemeChanged;
	}

	private void ContainedButtonBase_ActualThemeChanged(FrameworkElement sender, object args)
	{
		UpdateOutStrokeFocusCornerRadius();
	}

	private void UpdateOutStrokeFocusCornerRadius()
	{
		if (!(_outStrokeFocusBorder == null))
		{
			if (_accessibilitySettings.HighContrast)
			{
				_outStrokeFocusBorder.CornerRadius = new CornerRadius(base.CornerRadius.TopLeft + 2.0);
			}
			else
			{
				_outStrokeFocusBorder.CornerRadius = base.CornerRadius;
			}
		}
	}

	private void ProgressButton_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		ProgressVisualStateChange();
	}

	private static void OnIsProgressEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ContainedButtonBase containedButtonBase)
		{
			containedButtonBase.ProgressVisualStateChange();
		}
	}

	private void ProgressVisualStateChange(bool useTransition = false)
	{
		if (base.IsEnabled && IsProgressEnabled)
		{
			ApplyState("ProgressEnabled", useTransition);
			base.IsTabStop = false;
		}
		else
		{
			ApplyState("ProgressDisabled", useTransition);
			base.IsTabStop = true;
		}
		if (!base.IsEnabled)
		{
			ApplyState("Disabled", useTransition);
		}
	}

	private void ApplyState(string state, bool useTransition)
	{
		VisualStateManager.GoToState(this, state, useTransition);
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		_outStrokeFocusBorder = (Border)GetTemplateChild("OutStroke");
		UpdateOutStrokeFocusCornerRadius();
	}
}
