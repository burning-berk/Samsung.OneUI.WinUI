using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Shapes;
using Samsung.OneUI.WinUI.Utils.Helpers;

namespace Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker;

[TemplatePart(Name = "ThirdDimensionSlider", Type = typeof(ColorPickerSliderCustom))]
[TemplatePart(Name = "ThirdDimensionTextBox", Type = typeof(TextBlock))]
internal sealed class ColorPickerSpectrum : Microsoft.UI.Xaml.Controls.ColorPicker
{
	private const string COLOR_PICKER_SLIDER_NAME = "ThirdDimensionSlider";

	private const string COLOR_PICKER_TEXT_SLIDER_NAME = "ThirdDimensionTextBox";

	private const string COLOR_PICKER_SLIDER_ALPHA_NAME = "AlphaSlider";

	private const string COLOR_PICKER_TEXT_SLIDER_ALPHA_NAME = "AlphaTextBox";

	private const string COLOR_PICKER_SLIDER_SATURATION_CONTAINER_NAME = "SaturationSliderContainer";

	private const string COLOR_PICKER_SLIDER_ALPHA_CONTAINER_NAME = "AlphaSliderContainer";

	private const string COLOR_PICKER_SELECTION_ELLIPSE_NAME = "SelectionEllipse";

	private const string HUE_STRING_ID = "DREAM_HUE_M_COLOR_BUTTON10";

	private const string SATURATION_STRING_ID = "DREAM_SATURATION_TBOPT";

	private const string BRIGHTNESS_STRING_ID = "DREAM_BRIGHTNESS_M_COLOR_TBOPT";

	private const string OPACITY_STRING_ID = "DREAM_IDLE_OPT_OPACITY/Text";

	private ColorPickerSliderCustom _colorPickerSlider;

	private ColorPickerSliderCustom _colorPickerAlphaSlider;

	private TextBlock _colorPickerTextSlider;

	private TextBlock _colorPickerTextAlphaSlider;

	public double? AlphaSliderValue { get; set; }

	public bool IsColorPickerAlphaSliderEditable { get; set; }

	public new bool IsAlphaSliderVisible { get; set; }

	public bool IsSaturationSliderVisible { get; set; }

	public string SelectedColorDescription { get; set; }

	public event EventHandler LostFocusEvent;

	public ColorPickerSpectrum()
	{
		base.DefaultStyleKey = typeof(ColorPickerSpectrum);
		base.Unloaded += ColorPickerSpectrum_Unloaded;
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		UpdateProperties();
	}

	public void UpdateProperties()
	{
		DisposeColorPickerSpectrum();
		_colorPickerSlider = (ColorPickerSliderCustom)GetTemplateChild("ThirdDimensionSlider");
		if (_colorPickerSlider != null)
		{
			string value = $"{"DREAM_HUE_M_COLOR_BUTTON10".GetLocalized()}, {"DREAM_SATURATION_TBOPT".GetLocalized()}, {"DREAM_BRIGHTNESS_M_COLOR_TBOPT".GetLocalized()}";
			AutomationProperties.SetName(_colorPickerSlider, value);
			_colorPickerSlider.ValueChanged += Slider_ValueChanged;
		}
		Grid grid = (Grid)GetTemplateChild("AlphaSliderContainer");
		if (grid != null)
		{
			grid.Visibility = ((!IsAlphaSliderVisible) ? Visibility.Collapsed : Visibility.Visible);
		}
		Grid grid2 = (Grid)GetTemplateChild("SaturationSliderContainer");
		if (grid2 != null)
		{
			grid2.Visibility = ((!IsSaturationSliderVisible) ? Visibility.Collapsed : Visibility.Visible);
		}
		_colorPickerAlphaSlider = (ColorPickerSliderCustom)GetTemplateChild("AlphaSlider");
		if (_colorPickerAlphaSlider != null)
		{
			AutomationProperties.SetName(_colorPickerAlphaSlider, "DREAM_IDLE_OPT_OPACITY/Text".GetLocalized());
			if (AlphaSliderValue.HasValue)
			{
				_colorPickerAlphaSlider.Value = AlphaSliderValue.Value;
			}
			_colorPickerAlphaSlider.ValueChanged += Slider_Alpha_ValueChanged;
			_colorPickerAlphaSlider.IsEnabled = IsColorPickerAlphaSliderEditable;
		}
		_colorPickerTextAlphaSlider = (TextBlock)GetTemplateChild("AlphaTextBox");
		if (_colorPickerTextAlphaSlider != null && _colorPickerAlphaSlider != null)
		{
			UpdateTextSlider(_colorPickerTextAlphaSlider, _colorPickerAlphaSlider.Value);
		}
		_colorPickerTextSlider = (TextBlock)GetTemplateChild("ThirdDimensionTextBox");
		if (_colorPickerTextSlider != null && _colorPickerSlider != null)
		{
			UpdateTextSlider(_colorPickerTextSlider, _colorPickerSlider.Value);
		}
		base.LostFocus += ColorPickerSpectrum_LostFocus;
	}

	public string GetSelectedColorDescription(Microsoft.UI.Xaml.Controls.ColorPicker colorPicker)
	{
		Ellipse ellipse = UIExtensionsInternal.FindChildByName<Ellipse>("SelectionEllipse", colorPicker);
		if (ellipse != null && ToolTipService.GetToolTip(ellipse) is Microsoft.UI.Xaml.Controls.ToolTip toolTip)
		{
			return toolTip.Content.ToString();
		}
		return null;
	}

	private void ColorPickerSpectrum_Unloaded(object sender, RoutedEventArgs e)
	{
		DisposeColorPickerSpectrum();
	}

	private void ColorPickerSpectrum_LostFocus(object sender, RoutedEventArgs e)
	{
		if (sender is Microsoft.UI.Xaml.Controls.ColorPicker colorPicker)
		{
			string selectedColorDescription = GetSelectedColorDescription(colorPicker);
			SetSelectedColorDescription(selectedColorDescription);
			this.LostFocusEvent(this, EventArgs.Empty);
		}
	}

	private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
	{
		UpdatePercentageValue(_colorPickerTextSlider, e);
	}

	private void Slider_Alpha_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
	{
		UpdatePercentageValue(_colorPickerTextAlphaSlider, e);
	}

	private void UpdatePercentageValue(TextBlock textBlock, RangeBaseValueChangedEventArgs e)
	{
		if (!(e == null))
		{
			UpdateTextSlider(textBlock, e.NewValue);
		}
	}

	private static void UpdateTextSlider(TextBlock textBlock, double value)
	{
		string arg = Convert.ToInt32(value).ToString();
		textBlock.Text = $"{arg}";
	}

	private void SetSelectedColorDescription(string text)
	{
		SelectedColorDescription = text;
	}

	private void DisposeColorPickerSpectrum()
	{
		_colorPickerTextSlider = null;
		if (_colorPickerSlider != null)
		{
			_colorPickerSlider.ValueChanged -= Slider_ValueChanged;
			_colorPickerSlider = null;
		}
		if (_colorPickerAlphaSlider != null)
		{
			_colorPickerAlphaSlider.ValueChanged -= Slider_Alpha_ValueChanged;
			_colorPickerAlphaSlider = null;
		}
		_colorPickerTextAlphaSlider = null;
		_colorPickerTextSlider = null;
		base.LostFocus -= ColorPickerSpectrum_LostFocus;
	}
}
