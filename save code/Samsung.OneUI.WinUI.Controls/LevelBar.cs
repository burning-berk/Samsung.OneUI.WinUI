using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Samsung.OneUI.WinUI.Controls.EventHandlers;
using Samsung.OneUI.WinUI.Controls.Inputs.Slider.LevelBar;
using Windows.System;

namespace Samsung.OneUI.WinUI.Controls;

public sealed class LevelBar : Control
{
	private const int MINIMUM_LEVEL = 2;

	private const string SLIDER = "slider";

	private LevelSlider sliderLevelBar;

	public static readonly DependencyProperty LevelsProperty = DependencyProperty.Register("Levels", typeof(int), typeof(LevelBar), new PropertyMetadata(8, OnLevelsPropertyChanged));

	public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(int), typeof(LevelBar), new PropertyMetadata(100));

	public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(int), typeof(LevelBar), new PropertyMetadata(0));

	public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(LevelBar), new PropertyMetadata(0.0, OnValuePropertyChanged));

	public static readonly DependencyProperty IsThumbToolTipEnabledProperty = DependencyProperty.Register("IsThumbToolTipEnabled", typeof(bool), typeof(LevelBar), new PropertyMetadata(true));

	public static readonly DependencyProperty ThumbToolTipValueConverterProperty = DependencyProperty.Register("ThumbToolTipValueConverter", typeof(IValueConverter), typeof(LevelBar), new PropertyMetadata(null));

	public int Levels
	{
		get
		{
			return (int)GetValue(LevelsProperty);
		}
		set
		{
			SetValue(LevelsProperty, value);
		}
	}

	public int Maximum
	{
		get
		{
			return (int)GetValue(MaximumProperty);
		}
		set
		{
			SetValue(MaximumProperty, value);
		}
	}

	public int Minimum
	{
		get
		{
			return (int)GetValue(MinimumProperty);
		}
		set
		{
			SetValue(MinimumProperty, value);
		}
	}

	public double Value
	{
		get
		{
			return (double)GetValue(ValueProperty);
		}
		set
		{
			SetValue(ValueProperty, value);
		}
	}

	public bool IsThumbToolTipEnabled
	{
		get
		{
			return (bool)GetValue(IsThumbToolTipEnabledProperty);
		}
		set
		{
			SetValue(IsThumbToolTipEnabledProperty, value);
		}
	}

	public IValueConverter ThumbToolTipValueConverter
	{
		get
		{
			return (IValueConverter)GetValue(ThumbToolTipValueConverterProperty);
		}
		set
		{
			SetValue(ThumbToolTipValueConverterProperty, value);
		}
	}

	public event EventHandler<SliderRangeBaseValueChangedEventArgs> ValueChanged;

	private static void OnLevelsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((LevelBar)d).UpdateLevels();
	}

	public LevelBar()
	{
		base.DefaultStyleKey = typeof(LevelBar);
		base.PreviewKeyDown += LevelBar_PreviewKeyDown;
	}

	private void LevelBar_PreviewKeyDown(object sender, KeyRoutedEventArgs e)
	{
		double stepFrequency = GetStepFrequency();
		if (e.Key == VirtualKey.Right || e.Key == VirtualKey.Up)
		{
			sliderLevelBar.Value += stepFrequency;
			Value = sliderLevelBar.Value;
		}
		else if (e.Key == VirtualKey.Left || e.Key == VirtualKey.Down)
		{
			sliderLevelBar.Value -= stepFrequency;
			Value = sliderLevelBar.Value;
		}
	}

	private double GetStepFrequency()
	{
		if (sliderLevelBar != null)
		{
			return sliderLevelBar.GetStepFrequency();
		}
		return (double)(Maximum - Minimum) / ((double)Levels - 1.0);
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		sliderLevelBar = (LevelSlider)GetTemplateChild("slider");
		if (sliderLevelBar != null)
		{
			sliderLevelBar.ValueChangedEvent += ValueChangedEvent;
			SetAutomationProperties(sliderLevelBar);
		}
	}

	private void ValueChangedEvent(object sender, RangeBaseValueChangedEventArgs e)
	{
		double value = Value;
		Value = sliderLevelBar.Value;
		if (value != Value)
		{
			SliderRangeBaseValueChangedEventArgs e2 = new SliderRangeBaseValueChangedEventArgs(value, Value);
			this.ValueChanged?.Invoke(this, e2);
		}
	}

	private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is LevelBar levelBar)
		{
			levelBar.UpdateLevelSliderValue();
		}
	}

	private void UpdateLevelSliderValue()
	{
		if (sliderLevelBar != null && Value != sliderLevelBar.Value)
		{
			sliderLevelBar.SetSliderValue(Value);
		}
	}

	private void UpdateLevels()
	{
		if (Levels < 2)
		{
			Levels = 2;
		}
	}

	private void SetAutomationProperties(LevelSlider sliderLevelBar)
	{
		AutomationProperties.SetAutomationId(sliderLevelBar, AutomationProperties.GetAutomationId(this) + "_LEVEL_SLIDER");
		AutomationProperties.SetName(sliderLevelBar, AutomationProperties.GetName(this));
		AutomationProperties.SetHelpText(sliderLevelBar, AutomationProperties.GetHelpText(this));
	}
}
