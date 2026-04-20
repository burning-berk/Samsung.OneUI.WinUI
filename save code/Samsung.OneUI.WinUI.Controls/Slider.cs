using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;

namespace Samsung.OneUI.WinUI.Controls;

public class Slider : SliderBase
{
	private const double DEFAULT_MAXIMUM_VALUE = double.MaxValue;

	private const double DEFAULT_MINIMUM_VALUE = double.MinValue;

	public static readonly DependencyProperty ShockValueProperty = DependencyProperty.Register("ShockValue", typeof(int), typeof(Slider), new PropertyMetadata(int.MinValue, OnShockValuePropertyChanged));

	public static readonly DependencyProperty ShockValueTypeProperty = DependencyProperty.Register("ShockValueType", typeof(ShockValueType), typeof(Slider), new PropertyMetadata(ShockValueType.GreaterOrEqualThan, OnShockValueTypePropertyChanged));

	public static readonly DependencyProperty MaximumValueProperty = DependencyProperty.Register("MaximumValue", typeof(double), typeof(Slider), new PropertyMetadata(double.MaxValue, OnMaximumValuePropertyChanged));

	public static readonly DependencyProperty MinimumValueProperty = DependencyProperty.Register("MinimumValue", typeof(double), typeof(Slider), new PropertyMetadata(double.MinValue, OnMinimumValuePropertyChanged));

	public int ShockValue
	{
		get
		{
			return (int)GetValue(ShockValueProperty);
		}
		set
		{
			SetValue(ShockValueProperty, value);
		}
	}

	public ShockValueType ShockValueType
	{
		get
		{
			return (ShockValueType)GetValue(ShockValueTypeProperty);
		}
		set
		{
			SetValue(ShockValueTypeProperty, value);
		}
	}

	public double MaximumValue
	{
		get
		{
			return (double)GetValue(MaximumValueProperty);
		}
		set
		{
			ValidateMinMaxValues(MinimumValue, value);
			SetValue(MaximumValueProperty, value);
		}
	}

	public double MinimumValue
	{
		get
		{
			return (double)GetValue(MinimumValueProperty);
		}
		set
		{
			ValidateMinMaxValues(value, MaximumValue);
			SetValue(MinimumValueProperty, value);
		}
	}

	private static void OnShockValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is Slider slider)
		{
			slider.UpdateLayoutShockArea();
		}
	}

	private static void OnShockValueTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is Slider slider)
		{
			slider.UpdateLayoutShockArea();
		}
	}

	private static void OnMaximumValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is Slider slider)
		{
			slider.ValidateMinMaxValues(slider.MinimumValue, slider.MaximumValue);
			slider.UpdateValue();
			slider.SetRectanglesMaxSize();
		}
	}

	private static void OnMinimumValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is Slider slider)
		{
			slider.ValidateMinMaxValues(slider.MinimumValue, slider.MaximumValue);
			slider.UpdateValue();
			slider.SetRectanglesMaxSize();
		}
	}

	protected override void InitEvents()
	{
		base.ValueChanged += ValueChangedEvent;
		base.SizeChanged += Slider_SizeChanged;
		RegisterPropertyChangedCallback(RangeBase.MaximumProperty, OnSliderPropertyChanged);
		RegisterPropertyChangedCallback(RangeBase.MinimumProperty, OnSliderPropertyChanged);
		SetRectanglesMaxSize();
	}

	protected override void SetDefaultStyleKey()
	{
		base.DefaultStyleKey = typeof(Slider);
	}

	public override void RefreshLayout()
	{
		UpdateLayout();
		UpdateLayoutShockArea();
		SetRectanglesMaxSize();
	}

	private void ValidateMinMaxValues(double minValue, double maxValue)
	{
		if (maxValue < minValue)
		{
			throw new ArgumentOutOfRangeException("maxValue", "MaximumValue cannot be less than MinimumValue.");
		}
		if (minValue > maxValue)
		{
			throw new ArgumentOutOfRangeException("minValue", "MinimumValue cannot be greater than MaximumValue.");
		}
	}

	private void OnSliderPropertyChanged(DependencyObject sender, DependencyProperty dp)
	{
		SetRectanglesMaxSize();
	}

	private void ValueChangedEvent(object sender, RangeBaseValueChangedEventArgs e)
	{
		UpdateLayoutShockArea(e.NewValue);
		UpdateValue();
	}

	private void UpdateValue()
	{
		if (base.Value > MaximumValue)
		{
			base.Value = MaximumValue;
		}
		else if (base.Value < MinimumValue)
		{
			base.Value = MinimumValue;
		}
	}

	private void Slider_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		UpdateDecreaseRectVariables();
		SetRectanglesMaxSize();
	}

	private void SetRectanglesMaxSize()
	{
		double thumbSize = GetThumbSize(base.Type);
		if (_horizontalDecreaseRect != null && base.Orientation == Orientation.Horizontal)
		{
			_horizontalDecreaseRect.MaxWidth = ((MaximumValue != double.MaxValue) ? ((base.ActualWidth - thumbSize) * MaximumValue / base.Maximum) : _horizontalDecreaseRectMaxWidth);
			_horizontalDecreaseRect.MinWidth = ((MinimumValue != double.MinValue) ? ((base.ActualWidth - thumbSize) * MinimumValue / base.Maximum) : _horizontalDecreaseRectMinWidth);
		}
		else if (_verticalDecreaseRect != null)
		{
			_verticalDecreaseRect.MaxHeight = ((MaximumValue != double.MaxValue) ? ((base.ActualHeight - thumbSize) * MaximumValue / base.Maximum) : _verticalDecreaseRectMaxHeight);
			_verticalDecreaseRect.MinHeight = ((MinimumValue != double.MinValue) ? ((base.ActualHeight - thumbSize) * MinimumValue / base.Maximum) : _verticalDecreaseRectMinHeight);
		}
	}

	private double GetThumbSize(SliderType type)
	{
		return type switch
		{
			SliderType.Type1 => 18.0, 
			SliderType.Type2 => 12.0, 
			SliderType.Ghost => 12.0, 
			_ => 18.0, 
		};
	}

	private void UpdateLayoutShockArea()
	{
		UpdateLayoutShockArea(base.Value);
	}

	private void UpdateLayoutShockArea(double sliderValue)
	{
		if (ShockValue != int.MinValue)
		{
			isShockArea = CheckShockArea(sliderValue);
		}
		else
		{
			isShockArea = false;
		}
		UpdatePointerState();
		SetBehaviorSliderType();
	}

	private bool CheckShockArea(double sliderValue)
	{
		if (SliderType.Ghost.Equals(base.Type))
		{
			return false;
		}
		if (ShockValueType == ShockValueType.LessOrEqualThan)
		{
			return sliderValue <= (double)ShockValue;
		}
		return sliderValue >= (double)ShockValue;
	}
}
