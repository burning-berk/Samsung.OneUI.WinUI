using System;
using System.Linq;
using System.Text.RegularExpressions;
using CommunityToolkit.WinUI;
using CommunityToolkit.WinUI.Helpers;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Samsung.OneUI.WinUI.Utils.Helpers;
using Windows.UI.ViewManagement;

namespace Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker;

[TemplatePart(Name = "PART_HexCodeTexBox", Type = typeof(ColorPickerTextBox))]
[TemplatePart(Name = "PART_RedColorTextBox", Type = typeof(ColorPickerTextBox))]
[TemplatePart(Name = "PART_GreenColorTextBox", Type = typeof(ColorPickerTextBox))]
[TemplatePart(Name = "PART_BlueColorTextBox", Type = typeof(ColorPickerTextBox))]
internal sealed class ColorPickerDescriptor : Control
{
	private const string HEX_CODE_TEXTBOX_NAME = "PART_HexCodeTexBox";

	private const string RED_COLOR_TEXTBOX_NAME = "PART_RedColorTextBox";

	private const string GREEN_COLOR_TEXTBOX_NAME = "PART_GreenColorTextBox";

	private const string BLUE_COLOR_TEXTBOX_NAME = "PART_BlueColorTextBox";

	private const string HEXADECIMAL_COLOR_START_SYMBOL = "#";

	private const string BRIGHT_COLOR_VISUAL_STATE = "BrightColor";

	private const string NORMAL_COLOR_VISUAL_STATE = "NormalColor";

	private const double BRIGHT_COLOR_SATURATION_THRESHOLD = 0.35;

	private const double BRIGHT_COLOR_VALUE_THRESHOLD = 0.8;

	private const string DEFAULT_TEXT_SCALE_STATE = "DefaultTextScale";

	private const string MEDIUM_TEXT_SCALE_STATE = "MediumTextScale";

	private const string LARGE_TEXT_SCALE_STATE = "LargeTextScale";

	private const double MEDIUM_TEXT_SCALE_BREAKPOINT = 1.3;

	private const double LARGE_TEXT_SCALE_BREAKPOINT = 1.7;

	private const double MAX_WIDTH_BREAKPOINT = 492.0;

	private ColorPickerTextBox _hexCodeTexBox;

	private ColorPickerTextBox _redColorTextBox;

	private ColorPickerTextBox _greenColorTextBox;

	private ColorPickerTextBox _blueColorTextBox;

	private int _lastRedValue;

	private int _lastGreenValue;

	private int _lastBlueValue;

	public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register("SelectedColor", typeof(Brush), typeof(ColorPickerDescriptor), new PropertyMetadata(new SolidColorBrush(Colors.Black), OnSelectedColorPropertyChanged));

	public static readonly DependencyProperty PreviousSelectedColorProperty = DependencyProperty.Register("PreviousSelectedColor", typeof(Brush), typeof(ColorPickerDescriptor), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

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

	public SolidColorBrush PreviousSelectedColor
	{
		get
		{
			return (SolidColorBrush)GetValue(PreviousSelectedColorProperty);
		}
		set
		{
			SetValue(PreviousSelectedColorProperty, value);
		}
	}

	public event EventHandler<SolidColorBrush> ColorChangedEvent;

	private static void OnSelectedColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ColorPickerDescriptor colorPickerDescriptor)
		{
			colorPickerDescriptor.UpdateColorState();
		}
	}

	public ColorPickerDescriptor()
	{
		base.DefaultStyleKey = typeof(ColorPickerDescriptor);
		base.Loaded += ColorPickerDescriptor_Loaded;
		base.Unloaded += ColorPickerDescriptor_Unloaded;
	}

	protected override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		UpdateProperties();
	}

	public void UpdateProperties()
	{
		DisposeColorPickerDescriptor();
		_hexCodeTexBox = (ColorPickerTextBox)GetTemplateChild("PART_HexCodeTexBox");
		if (_hexCodeTexBox != null)
		{
			_hexCodeTexBox.TextChanged += HexCodeTexBox_TextChanged;
			_hexCodeTexBox.LostFocus += HexCodeTexBox_LostFocus;
			_hexCodeTexBox.BeforeTextChanging += HEX_beforeChanging;
			_hexCodeTexBox.IsTextBoxLoaded = true;
		}
		_redColorTextBox = (ColorPickerTextBox)GetTemplateChild("PART_RedColorTextBox");
		if (_redColorTextBox != null)
		{
			_redColorTextBox.TextChanged += RedColorTextBox_TextChanged;
			_redColorTextBox.LostFocus += RedColorTextBox_LostFocus;
			_redColorTextBox.GettingFocus += RedColorTextBox_GettingFocus;
			_redColorTextBox.BeforeTextChanging += RGBTextBox_beforeChanging;
			_redColorTextBox.IsTextBoxLoaded = true;
		}
		_greenColorTextBox = (ColorPickerTextBox)GetTemplateChild("PART_GreenColorTextBox");
		if (_greenColorTextBox != null)
		{
			_greenColorTextBox.TextChanged += GreenColorTextBox_TextChanged;
			_greenColorTextBox.LostFocus += GreenColorTextBox_LostFocus;
			_greenColorTextBox.GettingFocus += GreenColorTextBox_GettingFocus;
			_greenColorTextBox.BeforeTextChanging += RGBTextBox_beforeChanging;
			_greenColorTextBox.IsTextBoxLoaded = true;
		}
		_blueColorTextBox = (ColorPickerTextBox)GetTemplateChild("PART_BlueColorTextBox");
		if (_blueColorTextBox != null)
		{
			_blueColorTextBox.TextChanged += BlueColorTextBox_TextChanged;
			_blueColorTextBox.LostFocus += BlueColorTextBox_LostFocus;
			_blueColorTextBox.GettingFocus += BlueColorTextBox_GettingFocus;
			_blueColorTextBox.BeforeTextChanging += RGBTextBox_beforeChanging;
			_blueColorTextBox.IsTextBoxLoaded = true;
		}
		SetColorDescriptions();
	}

	public void ChangeSelectedColor(SolidColorBrush brush)
	{
		if (!(brush == SelectedColor))
		{
			SelectedColor = brush;
			SetColorDescriptions();
		}
	}

	private void DisposeColorPickerDescriptor()
	{
		if (_hexCodeTexBox != null)
		{
			_hexCodeTexBox.TextChanged -= HexCodeTexBox_TextChanged;
			_hexCodeTexBox.LostFocus -= HexCodeTexBox_LostFocus;
			_hexCodeTexBox.BeforeTextChanging -= HEX_beforeChanging;
			_hexCodeTexBox.IsTextBoxLoaded = false;
			_hexCodeTexBox = null;
		}
		if (_redColorTextBox != null)
		{
			_redColorTextBox.TextChanged -= RedColorTextBox_TextChanged;
			_redColorTextBox.LostFocus -= RedColorTextBox_LostFocus;
			_redColorTextBox.GettingFocus -= RedColorTextBox_GettingFocus;
			_redColorTextBox.BeforeTextChanging -= RGBTextBox_beforeChanging;
			_redColorTextBox.IsTextBoxLoaded = false;
			_redColorTextBox = null;
		}
		if (_greenColorTextBox != null)
		{
			_greenColorTextBox.TextChanged -= GreenColorTextBox_TextChanged;
			_greenColorTextBox.LostFocus -= GreenColorTextBox_LostFocus;
			_greenColorTextBox.GettingFocus -= GreenColorTextBox_GettingFocus;
			_greenColorTextBox.BeforeTextChanging -= RGBTextBox_beforeChanging;
			_greenColorTextBox.IsTextBoxLoaded = false;
			_greenColorTextBox = null;
		}
		if (_blueColorTextBox != null)
		{
			_blueColorTextBox.TextChanged -= BlueColorTextBox_TextChanged;
			_blueColorTextBox.LostFocus -= BlueColorTextBox_LostFocus;
			_blueColorTextBox.GettingFocus -= BlueColorTextBox_GettingFocus;
			_blueColorTextBox.BeforeTextChanging -= RGBTextBox_beforeChanging;
			_blueColorTextBox.IsTextBoxLoaded = false;
			_blueColorTextBox = null;
		}
	}

	private void UpdateColorState()
	{
		if (IsColorBright(SelectedColor) || IsColorBright(PreviousSelectedColor))
		{
			VisualStateManager.GoToState(this, "BrightColor", useTransitions: false);
		}
		else
		{
			VisualStateManager.GoToState(this, "NormalColor", useTransitions: false);
		}
	}

	private bool IsColorBright(SolidColorBrush solidColorBrush)
	{
		HsvColor hsvColor = solidColorBrush.Color.ToHsv();
		if (hsvColor.S < 0.35)
		{
			return hsvColor.V > 0.8;
		}
		return false;
	}

	private void SetColorDescriptions()
	{
		if (!(_hexCodeTexBox == null) && !(_redColorTextBox == null) && !(_greenColorTextBox == null) && !(_blueColorTextBox == null))
		{
			if (SelectedColor == null)
			{
				_hexCodeTexBox.Text = PreviousSelectedColor.Color.ToString().Remove(1, 2);
				_redColorTextBox.Text = PreviousSelectedColor.Color.R.ToString();
				_greenColorTextBox.Text = PreviousSelectedColor.Color.G.ToString();
				_blueColorTextBox.Text = PreviousSelectedColor.Color.B.ToString();
			}
			else
			{
				_hexCodeTexBox.Text = SelectedColor.Color.ToString().Remove(1, 2);
				_redColorTextBox.Text = SelectedColor.Color.R.ToString();
				_greenColorTextBox.Text = SelectedColor.Color.G.ToString();
				_blueColorTextBox.Text = SelectedColor.Color.B.ToString();
			}
		}
	}

	private bool ValidateHexCodeWritting(string hexCode)
	{
		if (!string.IsNullOrEmpty(hexCode))
		{
			return Regex.IsMatch(hexCode, "^#([A-Fa-f0-9]{0,6}|[A-Fa-f0-9]{0,3})$");
		}
		return false;
	}

	private bool ValidateHexCode(string hexCode)
	{
		if (!string.IsNullOrEmpty(hexCode) && hexCode.Length == 7 && hexCode.StartsWith("#"))
		{
			return Regex.IsMatch(hexCode, "^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$");
		}
		return false;
	}

	private void UpdateColor(int alpha, int red, int green, int blue)
	{
		SolidColorBrush solidColorBrush = ColorsHelpers.CreateFromChannels(alpha, red, green, blue);
		if (!(SelectedColor.Color == solidColorBrush.Color))
		{
			SelectedColor = solidColorBrush;
			this.ColorChangedEvent?.Invoke(this, SelectedColor);
			SetColorDescriptions();
		}
	}

	private void SetNewColorFromHexCodeTextBox(string hexCode)
	{
		SolidColorBrush solidColorBrush = ColorsHelpers.ConvertColorHex(hexCode.Insert(1, SelectedColor.Color.ToString().Substring(1, 2)));
		if (!(SelectedColor.Color == solidColorBrush.Color))
		{
			SelectedColor = solidColorBrush;
			this.ColorChangedEvent?.Invoke(this, SelectedColor);
		}
	}

	private bool RangeValidateColor(string rgbText)
	{
		if (string.IsNullOrWhiteSpace(rgbText))
		{
			return true;
		}
		if (int.Parse(rgbText) >= 0)
		{
			return int.Parse(rgbText) <= 255;
		}
		return false;
	}

	private bool IsNonNullNumber(string rgbText)
	{
		int result;
		if (!string.IsNullOrEmpty(rgbText))
		{
			return int.TryParse(rgbText, out result);
		}
		return false;
	}

	private void HandleInvalidInputRGB(ColorPickerTextBox textBox, string defaultEmptyValue)
	{
		textBox.Text = defaultEmptyValue;
	}

	private void UpdateTextBoxSize()
	{
		UISettings uISettings = new UISettings();
		if (uISettings.TextScaleFactor >= 1.7)
		{
			VisualStateManager.GoToState(this, "LargeTextScale", useTransitions: false);
		}
		else if (uISettings.TextScaleFactor >= 1.3)
		{
			VisualStateManager.GoToState(this, "MediumTextScale", useTransitions: false);
		}
		else
		{
			VisualStateManager.GoToState(this, "DefaultTextScale", useTransitions: false);
		}
	}

	private void ColorPickerDescriptor_Unloaded(object sender, RoutedEventArgs e)
	{
		DisposeColorPickerDescriptor();
	}

	private void HexCodeTexBox_LostFocus(object sender, RoutedEventArgs e)
	{
		string text = ((ColorPickerTextBox)sender).Text;
		if (ValidateHexCode(text))
		{
			SetNewColorFromHexCodeTextBox(text);
		}
		SetColorDescriptions();
	}

	private void HexCodeTexBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		if (sender is TextBox { Text: var text } && ValidateHexCode(text))
		{
			SetNewColorFromHexCodeTextBox(text);
		}
	}

	private void HEX_beforeChanging(object sender, TextBoxBeforeTextChangingEventArgs args)
	{
		args.Cancel = !ValidateHexCodeWritting(args.NewText);
	}

	private void RGBTextBox_beforeChanging(object sender, TextBoxBeforeTextChangingEventArgs args)
	{
		args.Cancel = args.NewText.Any((char c) => !char.IsDigit(c));
		if (!args.Cancel)
		{
			args.Cancel = !RangeValidateColor(args.NewText);
		}
	}

	private void RedColorTextBox_LostFocus(object sender, RoutedEventArgs e)
	{
		string text = ((ColorPickerTextBox)sender).Text;
		if (IsNonNullNumber(text))
		{
			if (RangeValidateColor(text))
			{
				UpdateColor(SelectedColor.Color.A, int.Parse(text), int.Parse(_greenColorTextBox.Text), int.Parse(_blueColorTextBox.Text));
			}
			else
			{
				_redColorTextBox.Text = _lastRedValue.ToString();
			}
		}
		else
		{
			_redColorTextBox.Text = _lastRedValue.ToString();
		}
	}

	private void RedColorTextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		string text = ((TextBox)sender).Text;
		if (IsNonNullNumber(text) && RangeValidateColor(text))
		{
			UpdateColor(SelectedColor.Color.A, int.Parse(text), int.Parse(_greenColorTextBox.Text), int.Parse(_blueColorTextBox.Text));
		}
		else
		{
			HandleInvalidInputRGB(_redColorTextBox, string.Empty);
		}
	}

	private void RedColorTextBox_GettingFocus(UIElement sender, GettingFocusEventArgs args)
	{
		_lastRedValue = int.Parse(_redColorTextBox.Text);
	}

	private void GreenColorTextBox_LostFocus(object sender, RoutedEventArgs e)
	{
		string text = ((ColorPickerTextBox)sender).Text;
		if (IsNonNullNumber(text))
		{
			if (RangeValidateColor(text))
			{
				UpdateColor(SelectedColor.Color.A, int.Parse(_redColorTextBox.Text), int.Parse(text), int.Parse(_blueColorTextBox.Text));
			}
			else
			{
				_greenColorTextBox.Text = _lastGreenValue.ToString();
			}
		}
		else
		{
			_greenColorTextBox.Text = _lastGreenValue.ToString();
		}
	}

	private void GreenColorTextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		string text = ((TextBox)sender).Text;
		if (IsNonNullNumber(text) && RangeValidateColor(text))
		{
			UpdateColor(SelectedColor.Color.A, int.Parse(_redColorTextBox.Text), int.Parse(text), int.Parse(_blueColorTextBox.Text));
		}
		else
		{
			HandleInvalidInputRGB(_greenColorTextBox, string.Empty);
		}
	}

	private void GreenColorTextBox_GettingFocus(UIElement sender, GettingFocusEventArgs args)
	{
		_lastGreenValue = int.Parse(_greenColorTextBox.Text);
	}

	private void BlueColorTextBox_LostFocus(object sender, RoutedEventArgs e)
	{
		string text = ((ColorPickerTextBox)sender).Text;
		if (IsNonNullNumber(text))
		{
			if (RangeValidateColor(text))
			{
				UpdateColor(SelectedColor.Color.A, int.Parse(_redColorTextBox.Text), int.Parse(_greenColorTextBox.Text), int.Parse(text));
			}
			else
			{
				_blueColorTextBox.Text = _lastBlueValue.ToString();
			}
		}
		else
		{
			_blueColorTextBox.Text = _lastBlueValue.ToString();
		}
	}

	private void BlueColorTextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		string text = ((TextBox)sender).Text;
		if (IsNonNullNumber(text) && RangeValidateColor(text))
		{
			UpdateColor(SelectedColor.Color.A, int.Parse(_redColorTextBox.Text), int.Parse(_greenColorTextBox.Text), int.Parse(text));
		}
		else
		{
			HandleInvalidInputRGB(_blueColorTextBox, string.Empty);
		}
	}

	private void BlueColorTextBox_GettingFocus(UIElement sender, GettingFocusEventArgs args)
	{
		_lastBlueValue = int.Parse(_blueColorTextBox.Text);
	}

	private void ColorPickerDescriptor_Loaded(object sender, RoutedEventArgs e)
	{
		UpdateMargin();
		UpdateColorState();
		UpdateTextBoxSize();
	}

	private void UpdateMargin()
	{
		if (base.ActualWidth >= 492.0)
		{
			base.Margin = new Thickness(-12.0, 10.0, -12.0, 0.0);
		}
		else
		{
			base.Margin = new Thickness(0.0, 10.0, 0.0, 0.0);
		}
	}
}
