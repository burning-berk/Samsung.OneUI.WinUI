using System;
using System.Collections.Generic;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.System;

namespace Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker;

[TemplatePart(Name = "PART_ColorPickerSwatched", Type = typeof(ColorPickerSwatched))]
[TemplatePart(Name = "PART_ColorPickerSpectrum", Type = typeof(ColorPickerSpectrum))]
[TemplatePart(Name = "PART_ColorPickerOption", Type = typeof(ColorPickerOption))]
[TemplatePart(Name = "PART_ColorPickerDescriptor", Type = typeof(ColorPickerDescriptor))]
[TemplatePart(Name = "PART_ColorList", Type = typeof(ColorPickerHistory))]
internal sealed class ColorPickerControl : Control
{
	private const string COLOR_PICKER_SWATCHED = "PART_ColorPickerSwatched";

	private const string COLOR_PICKER_SPECTRUM = "PART_ColorPickerSpectrum";

	private const string COLOR_PICKER_OPTION_NAME = "PART_ColorPickerOption";

	private const string COLOR_PICKER_DESCRIPTOR_NAME = "PART_ColorPickerDescriptor";

	private const string COLOR_LIST_NAME = "PART_ColorList";

	private const string RECENTLY_USED_COLOR_SUB_HEADER = "PART_ColorPickerRecentlyUsedColorSubHeader";

	private const string RECENTLY_USED_COLOR_RESOURCE = "DREAM_ST_HEADER_RECENTLY_USED_COLORS/Text";

	private ColorPickerHistory _colorListControl;

	private ColorPickerOption _colorPickerOptionControl;

	private ColorPickerSwatched _colorPickerSwatchedControl;

	private ColorPickerSpectrum _colorPickerSpectrumControl;

	private ColorPickerDescriptor _colorPickerDescriptorControl;

	private SubHeader _recentlyUsedColorSubheader;

	public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register("SelectedColor", typeof(SolidColorBrush), typeof(ColorPickerControl), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

	public static readonly DependencyProperty IsColorPickerSwatchedSelectedProperty = DependencyProperty.Register("IsColorPickerSwatchedSelected", typeof(bool), typeof(ColorPickerControl), new PropertyMetadata(true));

	public static readonly DependencyProperty SwatchedVisibilityProperty = DependencyProperty.Register("SwatchedVisibility", typeof(Visibility), typeof(ColorPickerControl), new PropertyMetadata(Visibility.Collapsed));

	public static readonly DependencyProperty SpectrumVisibilityProperty = DependencyProperty.Register("SpectrumVisibility", typeof(Visibility), typeof(ColorPickerControl), new PropertyMetadata(Visibility.Visible));

	public static readonly DependencyProperty ThemeProperty = DependencyProperty.Register("Theme", typeof(ElementTheme), typeof(ColorPickerControl), new PropertyMetadata(ElementTheme.Default));

	public static readonly DependencyProperty RecentColorsProperty = DependencyProperty.Register("RecentColors", typeof(List<ColorInfo>), typeof(ColorPickerControl), new PropertyMetadata(null));

	public double? AlphaSliderValue { get; set; }

	public bool IsColorPickerAlphaSliderEditable { get; set; }

	public bool IsAlphaSliderVisible { get; set; }

	public bool IsSaturationSliderVisible { get; set; }

	public string SelectedColorDescription { get; private set; }

	public SolidColorBrush SelectedColor
	{
		get
		{
			return (SolidColorBrush)GetValue(SelectedColorProperty);
		}
		set
		{
			SetValue(SelectedColorProperty, value);
		}
	}

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

	public Visibility SwatchedVisibility
	{
		get
		{
			return (Visibility)GetValue(SwatchedVisibilityProperty);
		}
		set
		{
			SetValue(SwatchedVisibilityProperty, value);
		}
	}

	public Visibility SpectrumVisibility
	{
		get
		{
			return (Visibility)GetValue(SpectrumVisibilityProperty);
		}
		set
		{
			SetValue(SpectrumVisibilityProperty, value);
		}
	}

	public ElementTheme Theme
	{
		get
		{
			return (ElementTheme)GetValue(ThemeProperty);
		}
		set
		{
			SetValue(ThemeProperty, value);
		}
	}

	public List<ColorInfo> RecentColors
	{
		get
		{
			return (List<ColorInfo>)GetValue(RecentColorsProperty);
		}
		set
		{
			SetValue(RecentColorsProperty, value);
		}
	}

	public ColorPickerControl()
	{
		base.DefaultStyleKey = typeof(ColorPickerControl);
		base.Unloaded += ColorPickerControl_Unloaded;
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		UpdateProperties();
	}

	protected override void OnProcessKeyboardAccelerators(ProcessKeyboardAcceleratorEventArgs args)
	{
		base.OnProcessKeyboardAccelerators(args);
		if (args.Key == VirtualKey.Left || args.Key == VirtualKey.Right || args.Key == VirtualKey.Up || args.Key == VirtualKey.Down)
		{
			args.Handled = true;
		}
	}

	public void UpdateProperties()
	{
		DisposeColorPickerControl();
		SwatchedVisibility = ((!IsColorPickerSwatchedSelected) ? Visibility.Collapsed : Visibility.Visible);
		SpectrumVisibility = (IsColorPickerSwatchedSelected ? Visibility.Collapsed : Visibility.Visible);
		_colorPickerOptionControl = GetTemplateChild("PART_ColorPickerOption") as ColorPickerOption;
		if (_colorPickerOptionControl != null)
		{
			_colorPickerOptionControl.IsColorPickerSwatchedClickedEvent += ColorPickerOptionControl_IsColorPickerSwatchedClickedEvent;
			_colorPickerOptionControl.IsColorPickerSwatchedSelected = IsColorPickerSwatchedSelected;
			_colorPickerOptionControl.UpdateProperties();
		}
		_colorListControl = GetTemplateChild("PART_ColorList") as ColorPickerHistory;
		if (_colorListControl != null)
		{
			_colorListControl.ColorChangedEvent += ColorList_ColorChanged;
			_colorListControl.RecentColors = RecentColors;
			_colorListControl.UpdateProperties();
		}
		_colorPickerSwatchedControl = (ColorPickerSwatched)GetTemplateChild("PART_ColorPickerSwatched");
		if (_colorPickerSwatchedControl != null)
		{
			_colorPickerSwatchedControl.SelectedColor = SelectedColor;
			_colorPickerSwatchedControl.ColorChangedEvent += ColorPickerSwatchedControl_ColorChanged;
			_colorPickerSwatchedControl.AlphaSliderValue = AlphaSliderValue;
			_colorPickerSwatchedControl.IsAlphaSliderVisible = IsAlphaSliderVisible;
			_colorPickerSwatchedControl.IsColorPickerAlphaSliderEditable = IsColorPickerAlphaSliderEditable;
			_colorPickerSwatchedControl.UpdateProperties();
		}
		_colorPickerSpectrumControl = (ColorPickerSpectrum)GetTemplateChild("PART_ColorPickerSpectrum");
		if (_colorPickerSpectrumControl != null)
		{
			_colorPickerSpectrumControl.Color = SelectedColor.Color;
			_colorPickerSpectrumControl.ColorChanged += ColorPickerSpectrum_ColorChanged;
			_colorPickerSpectrumControl.LostFocusEvent += ColorPickerSpectrum_LostFocusEvent;
			_colorPickerSpectrumControl.AlphaSliderValue = AlphaSliderValue;
			_colorPickerSpectrumControl.IsAlphaSliderVisible = IsAlphaSliderVisible;
			_colorPickerSpectrumControl.IsSaturationSliderVisible = IsSaturationSliderVisible;
			_colorPickerSpectrumControl.IsColorPickerAlphaSliderEditable = IsColorPickerAlphaSliderEditable;
			_colorPickerSpectrumControl.UpdateProperties();
		}
		_colorPickerDescriptorControl = GetTemplateChild("PART_ColorPickerDescriptor") as ColorPickerDescriptor;
		if (_colorPickerDescriptorControl != null)
		{
			_colorPickerDescriptorControl.PreviousSelectedColor = SelectedColor;
			_colorPickerDescriptorControl.ColorChangedEvent += ColorPickerDescriptor_ColorChanged;
			_colorPickerDescriptorControl.SelectedColor = SelectedColor;
			_colorPickerDescriptorControl.UpdateProperties();
		}
		_recentlyUsedColorSubheader = GetTemplateChild("PART_ColorPickerRecentlyUsedColorSubHeader") as SubHeader;
		UpdateRecentlyUsedColorSubHeaderText();
		base.RequestedTheme = Theme;
	}

	public void UpdateColorHistory()
	{
		ColorInfo selectedColor = ColorPickerSwatchedDefaultColors.GetInstance().TryGetColorInfoFromDefaultList(SelectedColor.Color.ToString(), SelectedColorDescription);
		_colorListControl?.InsertColorHistory(selectedColor);
		RecentColors = _colorListControl?.RecentColors;
	}

	private void ColorPickerControl_Unloaded(object sender, RoutedEventArgs e)
	{
		DisposeColorPickerControl();
	}

	private void ColorPickerSpectrum_LostFocusEvent(object sender, EventArgs e)
	{
		SetSelectedColorDescription(_colorPickerSpectrumControl.SelectedColorDescription);
	}

	private void ColorPickerSpectrum_ColorChanged(Microsoft.UI.Xaml.Controls.ColorPicker sender, ColorChangedEventArgs args)
	{
		SetSelectedColor(new SolidColorBrush(args.NewColor));
		SetSelectedColorDescription(GetColorPickerSpectrumSelectedColorDescription());
		UpdateDescriptorSelectedColor();
	}

	private void ColorPickerSwatchedControl_ColorChanged(object sender, SolidColorBrush e)
	{
		SetSelectedColor(e);
		SetSelectedColorDescription(GetColorPickerSwatchedSelectedColorDescription());
		UpdateDescriptorSelectedColor();
	}

	private void ColorList_ColorChanged(object sender, SolidColorBrush e)
	{
		SetSelectedColor(e);
		SetSelectedColorDescription(_colorListControl?.SelectedColorDescription);
		UpdateSwatchedOrSpectrumSelectedColor();
	}

	private void ColorPickerDescriptor_ColorChanged(object sender, SolidColorBrush e)
	{
		SetSelectedColor(e);
		UpdateSwatchedOrSpectrumSelectedColor();
	}

	private void ColorPickerOptionControl_IsColorPickerSwatchedClickedEvent(object sender, bool e)
	{
		IsColorPickerSwatchedSelected = e;
		SwatchedVisibility = ((!IsColorPickerSwatchedSelected) ? Visibility.Collapsed : Visibility.Visible);
		SpectrumVisibility = (IsColorPickerSwatchedSelected ? Visibility.Collapsed : Visibility.Visible);
		UpdateSwatchedOrSpectrumSelectedColor();
	}

	private void UpdateSwatchedOrSpectrumSelectedColor()
	{
		if (IsColorPickerSwatchedSelected)
		{
			UpdateSwatchedSelectedColor();
		}
		else
		{
			UpdateSpectrumSelectedColor();
		}
	}

	private void UpdateDescriptorSelectedColor()
	{
		_colorPickerDescriptorControl?.ChangeSelectedColor(SelectedColor);
	}

	private void UpdateSwatchedSelectedColor()
	{
		if (_colorPickerSwatchedControl != null)
		{
			_colorPickerSwatchedControl.SelectedColorDescription = SelectedColorDescription;
			_colorPickerSwatchedControl.SelectedColor = SelectedColor;
			_colorPickerSwatchedControl.UpdateSwatchedSelection();
		}
	}

	private void UpdateSpectrumSelectedColor()
	{
		if (_colorPickerSpectrumControl != null)
		{
			_colorPickerSpectrumControl.SelectedColorDescription = SelectedColorDescription;
			_colorPickerSpectrumControl.Color = SelectedColor.Color;
		}
	}

	private void SetSelectedColor(SolidColorBrush selectedColor)
	{
		SelectedColor = selectedColor;
	}

	private void SetSelectedColorDescription(string description)
	{
		SelectedColorDescription = description;
	}

	private void DisposeColorPickerControl()
	{
		if (_colorPickerOptionControl != null)
		{
			_colorPickerOptionControl.IsColorPickerSwatchedClickedEvent -= ColorPickerOptionControl_IsColorPickerSwatchedClickedEvent;
			_colorPickerOptionControl = null;
		}
		if (_colorPickerDescriptorControl != null)
		{
			_colorPickerDescriptorControl.ColorChangedEvent -= ColorPickerDescriptor_ColorChanged;
			_colorPickerDescriptorControl = null;
		}
		if (_colorListControl != null)
		{
			_colorListControl.ColorChangedEvent -= ColorList_ColorChanged;
			_colorListControl = null;
		}
		if (_colorPickerSwatchedControl != null)
		{
			_colorPickerSwatchedControl.ColorChangedEvent -= ColorPickerSwatchedControl_ColorChanged;
			_colorPickerSwatchedControl = null;
		}
		if (_colorPickerSpectrumControl != null)
		{
			_colorPickerSpectrumControl.ColorChanged -= ColorPickerSpectrum_ColorChanged;
			_colorPickerSpectrumControl.LostFocusEvent -= ColorPickerSpectrum_LostFocusEvent;
			_colorPickerSpectrumControl = null;
		}
		_recentlyUsedColorSubheader = null;
	}

	private string GetColorPickerSwatchedSelectedColorDescription()
	{
		return _colorPickerSwatchedControl?.GetSelectedColor().Item2?.Description ?? GetColorPickerSpectrumSelectedColorDescription();
	}

	private string GetColorPickerSpectrumSelectedColorDescription()
	{
		return _colorPickerSpectrumControl?.GetSelectedColorDescription(_colorPickerSpectrumControl);
	}

	private void UpdateRecentlyUsedColorSubHeaderText()
	{
		if (!(_recentlyUsedColorSubheader == null))
		{
			_recentlyUsedColorSubheader.HeaderText = "DREAM_ST_HEADER_RECENTLY_USED_COLORS/Text".GetLocalized();
		}
	}

	internal void InitialFocus()
	{
		_colorPickerOptionControl?.InitialFocus();
	}
}
