using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Windows.System;

namespace Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker;

[TemplatePart(Name = "PART_ColorPickerSwatchedButton", Type = typeof(ColorPickerOptionCustomButton))]
[TemplatePart(Name = "PART_ColorPickerSpectrumButton", Type = typeof(ColorPickerOptionCustomButton))]
internal sealed class ColorPickerOption : Control
{
	private const string COLOR_PICKER_SWATCHED_BUTTON_NAME = "PART_ColorPickerSwatchedButton";

	private const string COLOR_PICKER_SPECTRUM_BUTTON_NAME = "PART_ColorPickerSpectrumButton";

	private ColorPickerOptionCustomButton colorPickerSwatchedButton;

	private ColorPickerOptionCustomButton colorPickerSpectrumButton;

	public static readonly DependencyProperty IsColorPickerSwatchedSelectedProperty = DependencyProperty.Register("IsColorPickerSwatchedSelected", typeof(bool), typeof(ColorPickerOption), new PropertyMetadata(true));

	public bool IsColorPickerSwatchedSelected
	{
		get
		{
			return (bool)GetValue(IsColorPickerSwatchedSelectedProperty);
		}
		set
		{
			SetValue(IsColorPickerSwatchedSelectedProperty, value);
		}
	}

	public event EventHandler<bool> IsColorPickerSwatchedClickedEvent;

	public ColorPickerOption()
	{
		base.DefaultStyleKey = typeof(ColorPickerOption);
		base.Unloaded += ColorPickerOption_Unloaded;
		base.Loaded += ColorPickerOption_Loaded;
	}

	private void ColorPickerOption_Loaded(object sender, RoutedEventArgs e)
	{
		UpdateProperties();
	}

	private void ColorPickerOption_Unloaded(object sender, RoutedEventArgs e)
	{
		DisposeColorPickerOption();
	}

	private void DisposeColorPickerOption()
	{
		if (colorPickerSwatchedButton != null)
		{
			colorPickerSwatchedButton.Click -= ColorPickerSwatchedRadioButton_Click;
			colorPickerSwatchedButton.PreviewKeyDown -= ColorPickerSwatchedButton_PreviewKeyDown;
			colorPickerSwatchedButton = null;
		}
		if (colorPickerSpectrumButton != null)
		{
			colorPickerSpectrumButton.Click -= ColorPickerSpectrumRadioButton_Click;
			colorPickerSpectrumButton.PreviewKeyDown -= ColorPickerSpectrumButton_PreviewKeyDown;
			colorPickerSpectrumButton = null;
		}
	}

	public void UpdateProperties()
	{
		colorPickerSwatchedButton = GetTemplateChild("PART_ColorPickerSwatchedButton") as ColorPickerOptionCustomButton;
		if (colorPickerSwatchedButton != null)
		{
			colorPickerSwatchedButton.Click += ColorPickerSwatchedRadioButton_Click;
			colorPickerSwatchedButton.PreviewKeyDown += ColorPickerSwatchedButton_PreviewKeyDown;
		}
		colorPickerSpectrumButton = GetTemplateChild("PART_ColorPickerSpectrumButton") as ColorPickerOptionCustomButton;
		if (colorPickerSpectrumButton != null)
		{
			colorPickerSpectrumButton.Click += ColorPickerSpectrumRadioButton_Click;
			colorPickerSpectrumButton.PreviewKeyDown += ColorPickerSpectrumButton_PreviewKeyDown;
		}
		UpdateToggleButtonChecked();
	}

	internal void InitialFocus()
	{
		if (IsColorPickerSwatchedSelected)
		{
			colorPickerSwatchedButton?.Focus(FocusState.Programmatic);
		}
		else
		{
			colorPickerSpectrumButton?.Focus(FocusState.Programmatic);
		}
	}

	private void UpdateToggleButtonChecked()
	{
		if (colorPickerSpectrumButton != null && colorPickerSwatchedButton != null)
		{
			if (IsColorPickerSwatchedSelected)
			{
				colorPickerSwatchedButton.IsChecked = true;
				colorPickerSpectrumButton.IsChecked = false;
			}
			else
			{
				colorPickerSpectrumButton.IsChecked = true;
				colorPickerSwatchedButton.IsChecked = false;
			}
		}
	}

	private async void ColorPickerSpectrumButton_PreviewKeyDown(object sender, KeyRoutedEventArgs e)
	{
		if (e.Key == VirtualKey.Left || e.Key == VirtualKey.Up)
		{
			await FocusManager.TryFocusAsync(colorPickerSwatchedButton, FocusState.Keyboard);
		}
	}

	private async void ColorPickerSwatchedButton_PreviewKeyDown(object sender, KeyRoutedEventArgs e)
	{
		if (e.Key == VirtualKey.Right || e.Key == VirtualKey.Down)
		{
			await FocusManager.TryFocusAsync(colorPickerSpectrumButton, FocusState.Keyboard);
		}
	}

	private void ColorPickerSwatchedRadioButton_Click(object sender, RoutedEventArgs e)
	{
		IsColorPickerSwatchedSelected = true;
		UpdateToggleButtonChecked();
		this.IsColorPickerSwatchedClickedEvent?.Invoke(this, IsColorPickerSwatchedSelected);
	}

	private void ColorPickerSpectrumRadioButton_Click(object sender, RoutedEventArgs e)
	{
		IsColorPickerSwatchedSelected = false;
		UpdateToggleButtonChecked();
		this.IsColorPickerSwatchedClickedEvent?.Invoke(this, IsColorPickerSwatchedSelected);
	}
}
