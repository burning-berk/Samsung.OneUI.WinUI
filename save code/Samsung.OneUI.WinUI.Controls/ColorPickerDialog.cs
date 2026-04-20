using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using Samsung.OneUI.WinUI.Controls.Inputs.ColorPicker;
using Samsung.OneUI.WinUI.Services;
using Windows.Foundation;
using Windows.System;
using Windows.UI.ViewManagement;
using WinRT;
using WinRT.Samsung_OneUI_WinUIVtableClasses;

namespace Samsung.OneUI.WinUI.Controls;

[WinRTRuntimeClassName("Microsoft.UI.Xaml.IUIElementOverrides")]
[WinRTExposedType(typeof(Samsung_OneUI_WinUI_Controls_BGBlurWinRTTypeDetails))]
public sealed class ColorPickerDialog : UserControl, IComponentConnector
{
	private const int COLOR_PICKER_ALPHA_CONTAINER_HEIGHT = 34;

	private const int DIALOG_VERTICAL_WIDTH = 364;

	private const int DIALOG_VERTICAL_HEIGHT = 639;

	private const int BUTTON_VERTICAL_WIDTH = 140;

	private const int DIALOG_VERTICAL_HEIGHT_HIGH_CONTRAST = 629;

	private const int DIALOG_VERTICAL_MEDIUM_WIDTH = 444;

	private const int DIALOG_VERTICAL_LARGE_WIDTH = 544;

	private const double MEDIUM_TEXT_SCALE_BREAKPOINT = 1.3;

	private const double LARGE_TEXT_SCALE_BREAKPOINT = 1.7;

	private const double DIALOG_VIEWBOX_SIZE_DEFAULT_VALUE = double.NaN;

	private SolidColorBrush _selectedColor;

	private List<ColorInfo> _recentColors;

	private bool _isColorPickerSwatchedSelected;

	private double? _alphaSliderValue;

	private bool _isColorPickerAlphaSliderEditable;

	private bool _isAlphaSliderVisible;

	private bool _isSaturationSliderVisible;

	private AccessibilitySettings _accessibilitySettings;

	private Control _previousFocusedObject;

	private readonly UIElementClickService _popupBorderClickService;

	public static readonly DependencyProperty SelectedColorDescriptionProperty = DependencyProperty.Register("SelectedColorDescription", typeof(string), typeof(ColorPickerDialog), new PropertyMetadata(null));

	public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register("SelectedColor", typeof(SolidColorBrush), typeof(ColorPickerDialog), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

	public static readonly DependencyProperty IsColorPickerSwatchedSelectedProperty = DependencyProperty.Register("IsColorPickerSwatchedSelected", typeof(bool), typeof(ColorPickerDialog), new PropertyMetadata(true));

	public static readonly DependencyProperty PickedColorsProperty = DependencyProperty.Register("PickedColors", typeof(List<string>), typeof(ColorPickerDialog), new PropertyMetadata(null));

	public static readonly DependencyProperty AlphaSliderValueProperty = DependencyProperty.Register("AlphaSliderValue", typeof(double), typeof(ColorPickerDialog), new PropertyMetadata(null));

	public static readonly DependencyProperty IsColorPickerAlphaSliderEditableProperty = DependencyProperty.Register("IsColorPickerAlphaSliderEditable", typeof(bool), typeof(ColorPickerDialog), new PropertyMetadata(true));

	public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool), typeof(ColorPickerDialog), new PropertyMetadata(false, OnIsOpenPropertyChanged));

	public static readonly DependencyProperty IsAlphaSliderVisibleProperty = DependencyProperty.Register("IsAlphaSliderVisible", typeof(bool), typeof(ColorPickerDialog), new PropertyMetadata(true));

	public static readonly DependencyProperty IsSaturationSliderVisibleProperty = DependencyProperty.Register("IsSaturationSliderVisible", typeof(bool), typeof(ColorPickerDialog), new PropertyMetadata(true));

	public static readonly DependencyProperty IsDialogViewBoxEnabledProperty = DependencyProperty.Register("isDialogViewBoxEnabled", typeof(bool), typeof(ColorPickerDialog), new PropertyMetadata(false));

	public static readonly DependencyProperty DialogViewBoxWidthProperty = DependencyProperty.Register("DialogViewBoxWidth", typeof(double), typeof(ColorPickerDialog), new PropertyMetadata(double.NaN, OnDialogViewBoxWidthPropertyChanged));

	public static readonly DependencyProperty DialogViewBoxHeightProperty = DependencyProperty.Register("DialogViewBoxHeight", typeof(double), typeof(ColorPickerDialog), new PropertyMetadata(double.NaN, OnDialogViewBoxHeightPropertyChanged));

	public static readonly DependencyProperty RecentColorsProperty = DependencyProperty.Register("RecentColors", typeof(List<ColorInfo>), typeof(ColorPickerDialog), new PropertyMetadata(null));

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private Grid Grid;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private Popup Popup;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private Border PopupBorder;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private Viewbox DialogViewBox;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private Grid DialogSpace;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private ColorPickerControl ColorPickerControl;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private Grid CommandSpace;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private FlatButton OneUIPrimaryActionButton;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private Border BorderDivisor;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private FlatButton OneUISecondaryActionButton;

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	private bool _contentLoaded;

	public string SelectedColorDescription
	{
		get
		{
			return (string)GetValue(SelectedColorDescriptionProperty);
		}
		private set
		{
			SetValue(SelectedColorDescriptionProperty, value);
		}
	}

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

	[Obsolete("This property is Deprecated, please use RecentColors instead. Now this property is defined to only get values and will be removed soon.", false)]
	public List<string> PickedColors
	{
		get
		{
			if (RecentColors == null)
			{
				return null;
			}
			return RecentColors.Select((ColorInfo color) => color?.ColorBrush?.Color.ToString())?.ToList();
		}
	}

	public double? AlphaSliderValue
	{
		get
		{
			return (double?)GetValue(AlphaSliderValueProperty);
		}
		set
		{
			SetValue(AlphaSliderValueProperty, value);
		}
	}

	public bool IsColorPickerAlphaSliderEditable
	{
		get
		{
			return (bool)GetValue(IsColorPickerAlphaSliderEditableProperty);
		}
		set
		{
			SetValue(IsColorPickerAlphaSliderEditableProperty, value);
		}
	}

	public bool IsOpen
	{
		get
		{
			return (bool)GetValue(IsOpenProperty);
		}
		set
		{
			SetValue(IsOpenProperty, value);
		}
	}

	public bool IsAlphaSliderVisible
	{
		get
		{
			return (bool)GetValue(IsAlphaSliderVisibleProperty);
		}
		set
		{
			SetValue(IsAlphaSliderVisibleProperty, value);
		}
	}

	public bool IsSaturationSliderVisible
	{
		get
		{
			return (bool)GetValue(IsSaturationSliderVisibleProperty);
		}
		set
		{
			SetValue(IsSaturationSliderVisibleProperty, value);
		}
	}

	public bool isDialogViewBoxEnabled
	{
		get
		{
			return (bool)GetValue(IsDialogViewBoxEnabledProperty);
		}
		set
		{
			SetValue(IsDialogViewBoxEnabledProperty, value);
		}
	}

	public double DialogViewBoxWidth
	{
		get
		{
			return (double)GetValue(DialogViewBoxWidthProperty);
		}
		set
		{
			SetValue(DialogViewBoxWidthProperty, value);
		}
	}

	public double DialogViewBoxHeight
	{
		get
		{
			return (double)GetValue(DialogViewBoxHeightProperty);
		}
		set
		{
			SetValue(DialogViewBoxHeightProperty, value);
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

	public event TypedEventHandler<ColorPickerDialog, RoutedEventArgs> DoneButtonClick;

	public event TypedEventHandler<ColorPickerDialog, RoutedEventArgs> CancelButtonClick;

	private static void OnIsOpenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ColorPickerDialog colorPickerDialog)
		{
			colorPickerDialog.ConfigureColorPicker();
		}
	}

	private static void OnDialogViewBoxWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ColorPickerDialog colorPickerDialog)
		{
			colorPickerDialog.ValidateNonNegativeDoubleProperty(e.Property, (double)e.NewValue);
		}
	}

	private static void OnDialogViewBoxHeightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ColorPickerDialog colorPickerDialog)
		{
			colorPickerDialog.ValidateNonNegativeDoubleProperty(e.Property, (double)e.NewValue);
		}
	}

	public ColorPickerDialog()
	{
		InitializeComponent();
		base.DefaultStyleKey = typeof(ColorPicker);
		base.Loaded += ColorPicker_Loaded;
		base.Unloaded += ColorPicker_Unloaded;
		_popupBorderClickService = new UIElementClickService(PopupBorder);
	}

	private void ValidateNonNegativeDoubleProperty(DependencyProperty property, double newValue)
	{
		if (GetValue(property) is double && newValue < 0.0)
		{
			SetValue(property, double.NaN);
		}
	}

	private void UpdateViewBoxLayout()
	{
		if (isDialogViewBoxEnabled)
		{
			DialogViewBox.Stretch = Stretch.Uniform;
			DialogViewBox.Width = DialogViewBoxWidth;
			DialogViewBox.Height = DialogViewBoxHeight;
		}
		else
		{
			DialogViewBox.Stretch = Stretch.None;
			DialogViewBox.Width = double.NaN;
			DialogViewBox.Height = double.NaN;
		}
	}

	private void UpdateLightDismiss()
	{
		if (Popup != null && _popupBorderClickService != null)
		{
			UIElementClickService popupBorderClickService = _popupBorderClickService;
			popupBorderClickService.Clicked = (RoutedEventHandler)Delegate.Remove(popupBorderClickService.Clicked, new RoutedEventHandler(PopupBorderClickService_Clicked));
			UIElementClickService popupBorderClickService2 = _popupBorderClickService;
			popupBorderClickService2.Clicked = (RoutedEventHandler)Delegate.Combine(popupBorderClickService2.Clicked, new RoutedEventHandler(PopupBorderClickService_Clicked));
			Popup.IsLightDismissEnabled = true;
		}
	}

	private void ConfigureColorPicker()
	{
		if (IsOpen)
		{
			UpdatePopupPosition();
			Popup.IsOpen = true;
			LoadColorPickerProperties();
		}
		else
		{
			ClosePopup();
		}
	}

	private void LoadColorPickerProperties()
	{
		_selectedColor = ((SelectedColor == null) ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(SelectedColor.Color));
		_recentColors = RecentColors;
		_isColorPickerSwatchedSelected = IsColorPickerSwatchedSelected;
		_alphaSliderValue = AlphaSliderValue;
		_isColorPickerAlphaSliderEditable = IsColorPickerAlphaSliderEditable;
		_isAlphaSliderVisible = IsAlphaSliderVisible;
		_isSaturationSliderVisible = IsSaturationSliderVisible;
		_accessibilitySettings = new AccessibilitySettings();
		ColorPickerControl.SelectedColor = _selectedColor;
		ColorPickerControl.RecentColors = _recentColors;
		ColorPickerControl.IsColorPickerSwatchedSelected = _isColorPickerSwatchedSelected;
		ColorPickerControl.Theme = base.RequestedTheme;
		ColorPickerControl.AlphaSliderValue = _alphaSliderValue;
		ColorPickerControl.IsColorPickerAlphaSliderEditable = _isColorPickerAlphaSliderEditable;
		ColorPickerControl.IsSaturationSliderVisible = _isSaturationSliderVisible;
		ColorPickerControl.IsAlphaSliderVisible = _isAlphaSliderVisible;
		UpdateDialogSize();
		OneUIPrimaryActionButton.Width = 140.0;
		OneUISecondaryActionButton.Width = 140.0;
		UpdateViewBoxLayout();
		ColorPickerControl.UpdateProperties();
		if (_previousFocusedObject == null)
		{
			_previousFocusedObject = FocusManager.GetFocusedElement(base.XamlRoot) as Control;
		}
		ColorPickerControl.InitialFocus();
	}

	private bool IsInHighContrastMode()
	{
		if (_accessibilitySettings != null)
		{
			return _accessibilitySettings.HighContrast;
		}
		return false;
	}

	private void UpdatePopupPosition()
	{
		if (Popup.XamlRoot != null)
		{
			Size size = Popup.XamlRoot.Size;
			Rect rect = new Rect(0.0, 0.0, size.Width, size.Height);
			Point point = PopupBorder.TransformToVisual(Popup.XamlRoot.Content).TransformPoint(new Point(0f, 0f));
			Popup.Margin = new Thickness(0.0 - point.X, 0.0 - point.Y, 0.0, 0.0);
			PopupBorder.Width = rect.Width;
			PopupBorder.Height = rect.Height;
		}
	}

	private void ClosePopup()
	{
		Popup.IsOpen = false;
	}

	private void FocusAtPreviousFocusedElement(FocusState focusState = FocusState.Programmatic)
	{
		_previousFocusedObject?.Focus(focusState);
	}

	private void PrimaryActionButtonClick(RoutedEventArgs e)
	{
		ColorPickerControl.UpdateColorHistory();
		SelectedColor = ColorPickerControl.SelectedColor;
		SelectedColorDescription = ColorPickerControl.SelectedColorDescription;
		IsColorPickerSwatchedSelected = ColorPickerControl.IsColorPickerSwatchedSelected;
		RecentColors = ColorPickerControl.RecentColors;
		this.DoneButtonClick?.Invoke(this, e);
		ClosePopup();
	}

	private void SecondaryActionButtonClick(RoutedEventArgs e)
	{
		this.CancelButtonClick?.Invoke(this, e);
		ClosePopup();
	}

	private void ConfigureFocusToPreviousFocusedElement(FocusState lastInteractedElementFocusState)
	{
		FocusAtPreviousFocusedElement((lastInteractedElementFocusState != FocusState.Keyboard) ? FocusState.Pointer : FocusState.Keyboard);
	}

	private FocusState GetOriginalSourceFocusState(RoutedEventArgs e)
	{
		FocusState result = FocusState.Pointer;
		if (e.OriginalSource is Control control)
		{
			result = control.FocusState;
		}
		return result;
	}

	private void UpdateDialogSize()
	{
		double num = (IsInHighContrastMode() ? 629 : 639);
		DialogSpace.MinHeight = num - (double)((!_isAlphaSliderVisible) ? 34 : 0);
		double textScaleFactor = new UISettings().TextScaleFactor;
		if (textScaleFactor >= 1.7)
		{
			DialogSpace.Width = 544.0;
		}
		else if (textScaleFactor >= 1.3)
		{
			DialogSpace.Width = 444.0;
		}
		else
		{
			DialogSpace.Width = 364.0;
		}
	}

	private void PopupBorderClickService_Clicked(object sender, RoutedEventArgs e)
	{
		ClosePopup();
		FocusAtPreviousFocusedElement(FocusState.Pointer);
	}

	private void PrimaryButton_Click(object sender, RoutedEventArgs e)
	{
		FocusState originalSourceFocusState = GetOriginalSourceFocusState(e);
		PrimaryActionButtonClick(e);
		ConfigureFocusToPreviousFocusedElement(originalSourceFocusState);
	}

	private void SecondaryButton_Click(object sender, RoutedEventArgs e)
	{
		FocusState originalSourceFocusState = GetOriginalSourceFocusState(e);
		SecondaryActionButtonClick(e);
		ConfigureFocusToPreviousFocusedElement(originalSourceFocusState);
	}

	private void ColorPicker_Unloaded(object sender, RoutedEventArgs e)
	{
		if (ColorPickerControl != null)
		{
			ColorPickerControl.Loaded -= ColorPickerControl_Loaded;
			ColorPickerControl.PreviewKeyDown -= ColorPickerControl_PreviewKeyDown;
		}
		if (_popupBorderClickService != null)
		{
			UIElementClickService popupBorderClickService = _popupBorderClickService;
			popupBorderClickService.Clicked = (RoutedEventHandler)Delegate.Remove(popupBorderClickService.Clicked, new RoutedEventHandler(PopupBorderClickService_Clicked));
		}
		if (Popup != null)
		{
			Popup.Closed -= Popup_Closed;
			Popup.Opened -= Popup_Opened;
		}
	}

	private void ColorPicker_Loaded(object sender, RoutedEventArgs e)
	{
		Popup.Opened += Popup_Opened;
		Popup.Closed += Popup_Closed;
		ColorPickerControl.Loaded += ColorPickerControl_Loaded;
		ColorPickerControl.PreviewKeyDown += ColorPickerControl_PreviewKeyDown;
	}

	private void OneUIPrimaryActionButton_ProcessKeyboardAccelerators(UIElement sender, ProcessKeyboardAcceleratorEventArgs args)
	{
		if (args.Key == VirtualKey.Left || args.Key == VirtualKey.Up || args.Key == VirtualKey.Down)
		{
			args.Handled = true;
		}
	}

	private void OneUISecondaryActionButton_ProcessKeyboardAccelerators(UIElement sender, ProcessKeyboardAcceleratorEventArgs args)
	{
		if (args.Key == VirtualKey.Right || args.Key == VirtualKey.Up || args.Key == VirtualKey.Down)
		{
			args.Handled = true;
		}
	}

	private void ColorPickerControl_Loaded(object sender, RoutedEventArgs e)
	{
		ConfigureColorPicker();
	}

	private void Popup_Closed(object sender, object e)
	{
		IsOpen = false;
		Popup.IsLightDismissEnabled = false;
		_previousFocusedObject = null;
		if (_popupBorderClickService != null)
		{
			UIElementClickService popupBorderClickService = _popupBorderClickService;
			popupBorderClickService.Clicked = (RoutedEventHandler)Delegate.Remove(popupBorderClickService.Clicked, new RoutedEventHandler(PopupBorderClickService_Clicked));
		}
	}

	private void Popup_Opened(object sender, object e)
	{
		UpdateLightDismiss();
		ColorPickerControl.InitialFocus();
	}

	private void ColorPickerControl_PreviewKeyDown(object sender, KeyRoutedEventArgs e)
	{
		if (e.Key == VirtualKey.Escape)
		{
			ClosePopup();
			FocusAtPreviousFocusedElement(FocusState.Keyboard);
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("ms-appx:///Samsung.OneUI.WinUI/Controls/DialogsAndFlyouts/ColorPicker/ColorPickerDialog.xaml");
			Application.LoadComponent(this, resourceLocator, ComponentResourceLocation.Nested);
		}
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	public void Connect(int connectionId, object target)
	{
		switch (connectionId)
		{
		case 2:
			Grid = target.As<Grid>();
			break;
		case 3:
			Popup = target.As<Popup>();
			break;
		case 4:
			PopupBorder = target.As<Border>();
			break;
		case 5:
			DialogViewBox = target.As<Viewbox>();
			break;
		case 6:
			DialogSpace = target.As<Grid>();
			break;
		case 7:
			ColorPickerControl = target.As<ColorPickerControl>();
			break;
		case 8:
			CommandSpace = target.As<Grid>();
			break;
		case 9:
			OneUIPrimaryActionButton = target.As<FlatButton>();
			OneUIPrimaryActionButton.Click += PrimaryButton_Click;
			OneUIPrimaryActionButton.ProcessKeyboardAccelerators += OneUIPrimaryActionButton_ProcessKeyboardAccelerators;
			break;
		case 10:
			BorderDivisor = target.As<Border>();
			break;
		case 11:
			OneUISecondaryActionButton = target.As<FlatButton>();
			OneUISecondaryActionButton.Click += SecondaryButton_Click;
			OneUISecondaryActionButton.ProcessKeyboardAccelerators += OneUISecondaryActionButton_ProcessKeyboardAccelerators;
			break;
		}
		_contentLoaded = true;
	}

	[GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2504")]
	[DebuggerNonUserCode]
	public IComponentConnector GetBindingConnector(int connectionId, object target)
	{
		return null;
	}
}
